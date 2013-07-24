using System;
using System.Collections.Generic;
using System.Web;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Profiles;
using LinkMe.Apps.Agents.Profiles.Commands;
using LinkMe.Apps.Agents.Profiles.Queries;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Referrals;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.PhoneNumbers.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Partners.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.Integration.LinkedIn;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Users;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Security
{
    public class AccountsManager
        : IAccountsManager
    {
        private readonly ILoginAuthenticationCommand _loginAuthenticationCommand;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IDevAuthenticationManager _devAuthenticationManager;
        private readonly IMemberAccountsCommand _memberAccountsCommand;
        private readonly IEmployerAccountsCommand _employerAccountsCommand;
        private readonly IOrganisationsCommand _organisationsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IPhoneNumbersQuery _phoneNumbersQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;
        private readonly IPartnersQuery _partnersQuery;
        private readonly ICookieManager _cookieManager;
        private readonly IReferralsManager _referralsManager;
        private readonly IProfilesCommand _profilesCommand;
        private readonly IProfilesQuery _profilesQuery;

        public AccountsManager(ILoginAuthenticationCommand loginAuthenticationCommand, IAuthenticationManager authenticationManager, IDevAuthenticationManager devAuthenticationManager, IMemberAccountsCommand memberAccountsCommand, IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand, ILoginCredentialsQuery loginCredentialsQuery, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IPhoneNumbersQuery phoneNumbersQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery, IPartnersQuery partnersQuery, ICookieManager cookieManager, IReferralsManager referralsManager, IProfilesCommand profilesCommand, IProfilesQuery profilesQuery)
        {
            _loginAuthenticationCommand = loginAuthenticationCommand;
            _authenticationManager = authenticationManager;
            _devAuthenticationManager = devAuthenticationManager;
            _memberAccountsCommand = memberAccountsCommand;
            _employerAccountsCommand = employerAccountsCommand;
            _organisationsCommand = organisationsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
            _phoneNumbersQuery = phoneNumbersQuery;
            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;
            _partnersQuery = partnersQuery;
            _cookieManager = cookieManager;
            _referralsManager = referralsManager;
            _profilesCommand = profilesCommand;
            _profilesQuery = profilesQuery;
        }

        AuthenticationResult IAccountsManager.TryAutoLogIn(HttpContextBase context)
        {
            var credentials = _cookieManager.ParsePersistantUserCookie(context);
            if (string.IsNullOrEmpty(credentials.LoginId) || string.IsNullOrEmpty(credentials.Password))
                return new AuthenticationResult { Status = AuthenticationStatus.Failed };

            // Authenticate.

            var result = _loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = credentials.LoginId, Password = credentials.Password });

            switch (result.Status)
            {
                case AuthenticationStatus.Authenticated:

                    // Automatically log in.

                    result.Status = AuthenticationStatus.AuthenticatedAutomatically;
                    _authenticationManager.LogIn(context, result.User, result.Status);
                    break;

                default:

                    // If it didn't work then ensure the cookies are removed.

                    _cookieManager.DeletePersistantUserCookie(context);
                    break;
            }

            return result;
        }

        AuthenticationResult IAccountsManager.LogIn(HttpContextBase context, Login login)
        {
            // Process the post to check validations etc.

            login.Prepare();
            login.Validate();

            // Authenticate.

            var result = _loginAuthenticationCommand.AuthenticateUser(new LoginCredentials { LoginId = login.LoginId, PasswordHash = LoginCredentials.HashToString(login.Password) });

            switch (result.Status)
            {
                case AuthenticationStatus.Authenticated:
                case AuthenticationStatus.AuthenticatedMustChangePassword:
                case AuthenticationStatus.AuthenticatedWithOverridePassword:
                case AuthenticationStatus.Deactivated:

                    // Log in.

                    _authenticationManager.LogIn(context, result.User, result.Status);

                    // Remember me.

                    if (login.RememberMe)
                        _cookieManager.CreatePersistantUserCookie(context, result.User.UserType, new LoginCredentials { LoginId = login.LoginId, Password = login.Password }, result.Status);
                    else
                        _cookieManager.DeletePersistantUserCookie(context);

                    // Vertical.

                    SetVertical(result.User);
                    break;
            }

            // Also log them in as a dev if they used the override password.

            if (result.Status == AuthenticationStatus.AuthenticatedWithOverridePassword)
                _devAuthenticationManager.LogIn(context);

            return result;
        }

        void IAccountsManager.LogOut(HttpContextBase context)
        {
            // Maintain the vertical.

            Vertical vertical = null;
            var verticalId = ActivityContext.Current.Vertical.Id;
            if (verticalId != null)
                vertical = _verticalsQuery.GetVertical(verticalId.Value);

            // Clean out remember me and any external authentication cookie.

            _cookieManager.DeletePersistantUserCookie(context);
            _cookieManager.DeleteExternalCookie(context, vertical == null ? null : vertical.ExternalCookieDomain);

            // Log out.

            _authenticationManager.LogOut(context);

            // Clean up the session but don't abandon it.

            context.Session.Clear();

            // Reset the vertical.

            if (vertical != null)
                ActivityContext.Current.Set(vertical);
        }

        Member IAccountsManager.Join(HttpContextBase context, MemberAccount account, AccountLoginCredentials accountCredentials, bool requiresActivation)
        {
            account.Prepare();
            account.Validate();

            accountCredentials.Prepare();
            accountCredentials.Validate();

            // Check for an existing login.

            if (_loginCredentialsQuery.DoCredentialsExist(new LoginCredentials { LoginId = accountCredentials.LoginId }))
                throw new DuplicateUserException();

            // Create the member.

            var member = CreateMember(account, requiresActivation);

            var credentials = new LoginCredentials
            {
                LoginId = accountCredentials.LoginId,
                PasswordHash = LoginCredentials.HashToString(accountCredentials.Password),
            };

            _memberAccountsCommand.CreateMember(member, credentials, GetMemberAffiliateId());

            // Log the user in.

            _authenticationManager.LogIn(context, member, AuthenticationStatus.Authenticated);

            // Initialise.

            _referralsManager.CreateReferral(context.Request, member.Id);
            InitialiseMemberProfile(member.Id);
            return member;
        }

        Employer IAccountsManager.Join(HttpContextBase context, EmployerAccount account, AccountLoginCredentials accountCredentials)
        {
            accountCredentials.Prepare();
            accountCredentials.Validate();

            // Check for an existing login.

            if (_loginCredentialsQuery.DoCredentialsExist(new LoginCredentials { LoginId = accountCredentials.LoginId }))
                throw new DuplicateUserException();

            return Join(
                context,
                account,
                e => _employerAccountsCommand.CreateEmployer(e, new LoginCredentials { LoginId = accountCredentials.LoginId, PasswordHash = LoginCredentials.HashToString(accountCredentials.Password) }));
        }

        Employer IAccountsManager.Join(HttpContextBase context, EmployerAccount account, LinkedInProfile profile)
        {
            return Join(
                context,
                account,
                e => _employerAccountsCommand.CreateEmployer(e, profile));
        }

        private Employer Join(HttpContextBase context, EmployerAccount account, Action<Employer> createEmployer)
        {
            account.Prepare();
            account.Validate();

            // Create the organisation.

            var organisation = CreateOrganisation(account);

            // Create the employer.

            var employer = new Employer
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                EmailAddress = new EmailAddress { Address = account.EmailAddress, IsVerified = true },
                PhoneNumber = _phoneNumbersQuery.GetPhoneNumber(account.PhoneNumber, ActivityContext.Current.Location.Country),
                Organisation = organisation,
                SubRole = account.SubRole,
                Industries = _industriesQuery.GetIndustries(account.IndustryIds),
            };

            createEmployer(employer);

            // Log the user in.

            _authenticationManager.LogIn(context, employer, AuthenticationStatus.Authenticated);

            // Initialise.

            _referralsManager.CreateReferral(context.Request, employer.Id);
            InitialiseEmployerProfile(employer.Id);
            return employer;
        }

        private Organisation CreateOrganisation(EmployerAccount account)
        {
            var organisation = new Organisation
            {
                Name = account.OrganisationName,
                AffiliateId = GetEmployerAffiliateId(),
                Address = new Address {Location = _locationQuery.ResolveLocation(GetCurrentCountry(), account.Location)},
            };
            _organisationsCommand.CreateOrganisation(organisation);
            return organisation;
        }

        private Guid? GetMemberAffiliateId()
        {
            var community = GetCurrentCommunity();
            if (community != null && community.HasMembers)
                return community.Id;
            return null;
        }

        private Guid? GetEmployerAffiliateId()
        {
            var community = GetCurrentCommunity();
            if (community != null && community.HasOrganisations)
                return community.Id;
            return null;
        }

        private Member CreateMember(MemberAccount account, bool requiresActivation)
        {
            var member = new Member
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = account.EmailAddress, IsVerified = !requiresActivation } },
                Address = new Address { Location = new LocationReference() },
                VisibilitySettings = new VisibilitySettings(),
                IsActivated = !requiresActivation
            };

            _locationQuery.ResolveLocation(member.Address.Location, GetCurrentCountry());
            return member;
        }

        private Community GetCurrentCommunity()
        {
            // Get the current community from the context.

            var id = ActivityContext.Current.Community.Id;
            return id != null ? _communitiesQuery.GetCommunity(id.Value) : null;
        }

        private static Country GetCurrentCountry()
        {
            // Get the current community from the context.

            return ActivityContext.Current.Location.Country;
        }

        private void SetVertical(IRegisteredUser user)
        {
            switch (user.UserType)
            {
                case UserType.Member:
                case UserType.Custodian:
                    SetUserVertical(user);
                    break;

                case UserType.Employer:
                    SetEmployerVertical(user);
                    break;
            }
        }

        private void SetUserVertical(IRegisteredUser user)
        {
            // Only try to set the current vertical if it hasn't already been set.
            // This ensures that if a user comes in through a vertical channel then
            // the web site remains within that vertical channel, even if the user
            // is associated with a different vertical.

            var communityContext = ActivityContext.Current.Community;
            if (communityContext.IsSet)
                return;

            // Check whether the member has an affiliate.
            // If so, then set the request context to it.

            var affiliateId = user.AffiliateId;
            if (affiliateId == null)
                return;

            var vertical = _verticalsQuery.GetVertical(affiliateId.Value);
            if (vertical != null)
                ActivityContext.Current.Set(vertical);
        }

        private void SetEmployerVertical(IUser user)
        {
            var employer = user as IEmployer;
            if (employer == null)
                return;

            // There is one special case that remains from previous work: Autopeople.

            if (SetPartnerEmployerVertical(employer.Id))
                return;

            var communityContext = ActivityContext.Current.Community;
            if (communityContext.IsSet)
                return;

            // Check whether the employer has a community.
            // If so, then set the request context to it.

            var affiliateId = employer.Organisation.AffiliateId;
            if (affiliateId == null)
                return;

            // The community must support it.

            var community = _communitiesQuery.GetCommunity(affiliateId.Value);
            if (community == null)
                return;

            var vertical = _verticalsQuery.GetVertical(affiliateId.Value);
            if (vertical != null)
                ActivityContext.Current.Set(vertical);
        }

        private bool SetPartnerEmployerVertical(Guid employerId)
        {
            var partner = _partnersQuery.GetPartner(employerId);
            if (partner != null)
            {
                // Set the context.

                var community = _communitiesQuery.GetCommunity(partner.Name);
                if (community != null)
                {
                    var vertical = _verticalsQuery.GetVertical(community.Id);
                    if (vertical != null)
                    {
                        ActivityContext.Current.Set(vertical);
                        return true;
                    }
                }
            }

            return false;
        }

        private void InitialiseMemberProfile(Guid memberId)
        {
            var profile = _profilesQuery.GetMemberProfile(memberId) ?? new MemberProfile();
            profile.UpdateStatusReminder.Hide = true;
            profile.UpdatedTermsReminder.Hide = true;
            _profilesCommand.UpdateMemberProfile(memberId, profile);
        }

        private void InitialiseEmployerProfile(Guid employerId)
        {
            var profile = _profilesQuery.GetEmployerProfile(employerId) ?? new EmployerProfile();
            profile.UpdatedTermsReminder.Hide = true;
            _profilesCommand.UpdateEmployerProfile(employerId, profile);
        }
    }
}

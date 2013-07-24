using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Asp.Modules;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Configuration;
using LinkMe.Web.Domain.Roles.Affiliations.Verticals;

namespace LinkMe.Web.Modules
{
    public class VerticalsModule
        : HttpModule, IRequiresSessionState
    {
        private static readonly EventSource EventSource = new EventSource<VerticalsModule>();

        private const string Vertical = "Vertical";

        private IVerticalsCommand _verticalsCommand;
        private IAuthenticationManager _authenticationManager;
        private ICookieManager _cookieManager;
        private IExternalAuthenticationCommand _externalAuthenticationCommand;
        private IUserAccountsCommand _userAccountsCommand;
        private IMemberAccountsCommand _memberAccountsCommand;
        private IMemberAffiliationsCommand _memberAffiliationsCommand;
        private ILocationQuery _locationQuery;
        private string _nonVerticalHost;

        protected override void OnInit()
        {
            base.OnInit();

            _verticalsCommand = Container.Current.Resolve<IVerticalsCommand>();
            _authenticationManager = Container.Current.Resolve<IAuthenticationManager>();
            _cookieManager = Container.Current.Resolve<ICookieManager>();
            _externalAuthenticationCommand = Container.Current.Resolve<IExternalAuthenticationCommand>();
            _userAccountsCommand = Container.Current.Resolve<IUserAccountsCommand>();
            _memberAccountsCommand = Container.Current.Resolve<IMemberAccountsCommand>();
            _memberAffiliationsCommand = Container.Current.Resolve<IMemberAffiliationsCommand>();
            _locationQuery = Container.Current.Resolve<ILocationQuery>();

            _nonVerticalHost = ApplicationContext.Instance.GetProperty("website.linkme.host");
        }

        protected override void OnBeginRequest()
        {
            const string method = "OnBeginRequest";

            var clientUrl = GetClientUrl();

            // Use the host to determine if a vertical needs to be set.

            var host = clientUrl.Host;

            if (string.Compare(host, _nonVerticalHost, StringComparison.InvariantCultureIgnoreCase) == 0)
                return;

            var vertical = _verticalsCommand.GetVerticalByHost(host);
            if (vertical == null)
                return;

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Vertical found to correspond to host.", Event.Arg("host", host), Event.Arg("vertical", vertical));

            // If it is deleted then need to redirect.

            var url = vertical.GetDeletedRedirectUrl(clientUrl, _nonVerticalHost);
            if (url != null)
                NavigationManager.RedirectPermanently(url);

            // If it matches by an alternative host then need to redirect to the original host.

            url = vertical.GetAlternativeHostRedirectUrl(clientUrl);
            if (url != null)
                NavigationManager.RedirectPermanently(url);

            // Store the vertical for now.

            HttpContext.Current.Items[Vertical] = vertical;
        }

        protected override void OnAuthenticateRequest()
        {
            var vertical = HttpContext.Current.Items[Vertical] as Vertical;
            if (vertical == null || !vertical.RequiresExternalLogin)
                return;

            // Grab the information from the cookies.

            var context = new HttpContextWrapper(HttpContext.Current);
            var userData = _cookieManager.ParseExternalCookie(context);
            if (userData == null)
            {
                // If they are already logged in then let them through, else get them to log in externally.

                if (context.Request.IsAuthenticated)
                    return;
                RedirectToExternalLoginUrl(vertical.ExternalLoginUrl);
            }

            // The user may be logged in but they have flipped between the external site and this one changing some details.
            // Could do some checking to streamline this process a little but that would mean playing with the standard authentication mechanism.
            // For now just log in the user anew each request.

            var user = AuthenticateUser(vertical.Id, userData);
            if (user != null)
                _authenticationManager.LogIn(context, user, AuthenticationStatus.Authenticated);
            else
                RedirectToExternalLoginUrl(vertical.ExternalLoginUrl);
        }

        private IRegisteredUser AuthenticateUser(Guid verticalId, ExternalUserData userData)
        {
            var result = _externalAuthenticationCommand.AuthenticateUser(new ExternalCredentials {ProviderId = verticalId, ExternalId = userData.ExternalId});
            switch (result.Status)
            {
                case AuthenticationStatus.Authenticated:
                    return UpdateMember(result.User, verticalId, userData);

                case AuthenticationStatus.Deactivated:

                    // Shouldn't be deactivated so activate them now.

                    _userAccountsCommand.ActivateUserAccount(result.User, result.User.Id);
                    return UpdateMember(result.User, verticalId, userData);

                case AuthenticationStatus.Failed:

                    // Haven't seen this person before so create them an account.

                    return CreateMember(verticalId, userData);

                default:

                    // Nothing that can be done here.

                    return null;
            }
        }

        private IRegisteredUser CreateMember(Guid verticalId, ExternalUserData userData)
        {
            // User does not exist, create them now making sure they are activated.

            var member = new Member
            {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = userData.EmailAddress, IsVerified = true } },
                Address = new Address { Location = _locationQuery.ResolveLocation(ActivityContext.Current.Location.Country, null) },
                VisibilitySettings = new VisibilitySettings(),
                IsActivated = true,
            };

            _memberAccountsCommand.CreateMember(member, new ExternalCredentials { ProviderId = verticalId, ExternalId = userData.ExternalId }, verticalId);
            return member;
        }

        private IRegisteredUser UpdateMember(IRegisteredUser user, Guid verticalId, ExternalUserData userData)
        {
            if (user is Member)
            {
                // User exists, but their details may have changed.

                var member = (Member) user;
                if (member.FirstName != userData.FirstName || member.LastName != userData.LastName || member.GetBestEmailAddress().Address != userData.EmailAddress)
                {
                    member.FirstName = userData.FirstName;
                    member.LastName = userData.LastName;
                    member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = userData.EmailAddress, IsVerified = true } };
                    _memberAccountsCommand.UpdateMember(member);
                }

                // Associate them with the vertical if needed.

                if (verticalId != user.AffiliateId)
                    _memberAffiliationsCommand.SetAffiliation(user.Id, verticalId);
            }

            return user;
        }

        private void RedirectToExternalLoginUrl(string externalLoginUrl)
        {
            // Give the external site a chance to come back to this spot by including a return url.

            var url = new Url(externalLoginUrl, new ReadOnlyQueryString(Apps.Asp.Constants.ReturnUrlParameter, GetClientUrl().AbsoluteUri));
            NavigationManager.Redirect(url, true);
        }

        protected override void OnPreRequestHandlerExecute()
        {
            // Set everything up. Need to wait until now to ensure that eg the Session is set up because the context may be using that.

            var vertical = HttpContext.Current.Items[Vertical] as Vertical;
            if (vertical != null)
                ActivityContext.Current.Set(vertical);
        }
    }
}
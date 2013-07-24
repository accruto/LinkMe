using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Apps.Agents.Users.Members.Commands
{
    public class MemberAccountsCommand
        : IMemberAccountsCommand
    {
        private readonly IMembersCommand _membersCommand;
        private readonly IMembersQuery _membersQuery;
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IExternalCredentialsCommand _externalCredentialsCommand;
        private readonly ICandidatesCommand _candidatesCommand;
        private readonly IMemberAffiliationsCommand _memberAffiliationsCommand;

        public MemberAccountsCommand(IMembersCommand membersCommand, IMembersQuery membersQuery, ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery, IExternalCredentialsCommand externalCredentialsCommand, ICandidatesCommand candidatesCommand, IMemberAffiliationsCommand memberAffiliationsCommand)
        {
            _membersCommand = membersCommand;
            _membersQuery = membersQuery;
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _externalCredentialsCommand = externalCredentialsCommand;
            _candidatesCommand = candidatesCommand;
            _memberAffiliationsCommand = memberAffiliationsCommand;
        }

        void IMemberAccountsCommand.CreateMember(Member member, LoginCredentials credentials, Guid? affiliateId)
        {
            // Check login credentials.

            if (_loginCredentialsQuery.DoCredentialsExist(credentials))
                throw new DuplicateUserException();

            // Set some defaults.

            member.IsEnabled = true;
            member.AffiliateId = affiliateId;
            PrepareEmailAddresses(member);

            // Save.

            CreateMember(member, affiliateId);
            _loginCredentialsCommand.CreateCredentials(member.Id, credentials);

            // Fire events.

            var handlers = MemberCreated;
            if (handlers != null)
                handlers(this, new MemberCreatedEventArgs(member.Id));
        }

        void IMemberAccountsCommand.CreateMember(Member member, ExternalCredentials credentials, Guid? affiliateId)
        {
            // Set some defaults.

            member.IsEnabled = true;
            member.AffiliateId = affiliateId;
            PrepareEmailAddresses(member);

            // Save.

            CreateMember(member, affiliateId);
            _externalCredentialsCommand.CreateCredentials(member.Id, credentials);

            // Fire events.

            var handlers = MemberCreated;
            if (handlers != null)
                handlers(this, new MemberCreatedEventArgs(member.Id));
        }

        void IMemberAccountsCommand.UpdateMember(Member member)
        {
            // Keep track of changes.

            var originalMember = _membersQuery.GetMember(member.Id);
            var originalCredentials = _loginCredentialsQuery.GetCredentials(member.Id);

            PrepareEmailAddresses(member, originalMember);

            // Save.

            _membersCommand.UpdateMember(member);

            // Because the email address is also the loginId that may need to be updated.
            // (Actually should only really update if the email has changed but there is an implementation quirk
            // where the member IsEnabled, IsActivated is stored in the same flags column as the credentials MustChangePassword).

            UpdateCredentials(member.Id, originalCredentials, member.EmailAddresses[0].Address);

            // Fire an event.

            var handlers = MemberUpdated;
            if (handlers != null)
                handlers(this, new EventArgs<Member>(member));
        }

        void IMemberAccountsCommand.CreateCredentials(Member member, LoginCredentials credentials)
        {
            // Change the email address and deactivate the user.

            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = credentials.LoginId, IsVerified = false } };
            var originalMember = _membersQuery.GetMember(member.Id);
            PrepareEmailAddresses(member, originalMember);
            member.IsActivated = false;
            _membersCommand.UpdateMember(member);

            // Remove any affiliation.

            var affiliateId = member.AffiliateId;
            member.AffiliateId = null;
            _memberAffiliationsCommand.SetAffiliation(member.Id, null);

            // Remove any external credentials.

            if (affiliateId != null)
                _externalCredentialsCommand.DeleteCredentials(member.Id, affiliateId.Value);

            // Create some credentials.

            _loginCredentialsCommand.CreateCredentials(member.Id, credentials);
        }

        [Publishes(PublishedEvents.MemberCreated)]
        public event EventHandler<MemberCreatedEventArgs> MemberCreated;

        [Publishes(PublishedEvents.MemberUpdated)]
        public event EventHandler<EventArgs<Member>> MemberUpdated;

        private void CreateMember(Member member, Guid? affiliateId)
        {
            _membersCommand.CreateMember(member);

            var candidate = new Candidate
            {
                Id = member.Id,
                Status = Defaults.CandidateStatus,
                DesiredJobTypes = Defaults.DesiredJobTypes,
                RelocationPreference = Defaults.RelocationPreference,
            };
            _candidatesCommand.CreateCandidate(candidate);

            if (affiliateId != null)
                _memberAffiliationsCommand.SetAffiliation(member.Id, affiliateId.Value);
        }

        private void UpdateCredentials(Guid memberId, LoginCredentials credentials, string emailAddress)
        {
            // Check that they have login credentials first because they may only have other types of credentials.

            if (credentials != null)
            {
                credentials.LoginId = emailAddress;
                _loginCredentialsCommand.UpdateCredentials(memberId, credentials, memberId);
            }
        }

        private static void PrepareEmailAddresses(IMember member, IMember originalMember)
        {
            // All new email addresses should be marked as not verified.

            if (member.EmailAddresses != null)
            {
                if (originalMember.EmailAddresses.IsNullOrEmpty())
                {
                    PrepareEmailAddresses(member);
                }
                else
                {
                    // Only new email addresses should be updated.

                    foreach (var emailAddress in member.EmailAddresses)
                    {
                        EmailAddress foundEmailAddress = null;
                        foreach (var originalEmailAddress in originalMember.EmailAddresses)
                        {
                            if (emailAddress.Address.Equals(originalEmailAddress.Address, StringComparison.InvariantCultureIgnoreCase))
                            {
                                // Found it, maintain the verified status.

                                emailAddress.IsVerified = originalEmailAddress.IsVerified;
                                foundEmailAddress = originalEmailAddress;
                                break;
                            }
                        }

                        // All new email addresses are considered not verified to start with.

                        if (foundEmailAddress == null)
                            emailAddress.IsVerified = false;
                    }
                }
            }
        }

        private static void PrepareEmailAddresses(IMember member)
        {
            // If the user is activated when created it is assumed that their emails have already been verified.

            if (member.EmailAddresses != null)
            {
                foreach (var emailAddress in member.EmailAddresses)
                    emailAddress.IsVerified = member.IsActivated;
            }
        }
    }
}

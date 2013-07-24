using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Donations;
using LinkMe.Domain.Donations.Queries;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views;

namespace LinkMe.Apps.Agents.Users.Members.Handlers
{
    public class FriendsHandler
        : IFriendsHandler
    {
        private readonly IEmailsCommand _emailsCommand;
        private readonly IMembersQuery _membersQuery;
        private readonly IMemberContactsQuery _memberContactsQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly IDonationsQuery _donationsQuery;
        private readonly IEmailVerificationsCommand _emailVerificationsCommand;
        private readonly IEmailVerificationsQuery _emailVerificationsQuery;

        public FriendsHandler(IEmailsCommand emailsCommand, IMembersQuery membersQuery, IMemberContactsQuery memberContactsQuery, ICandidatesQuery candidatesQuery, IResumesQuery resumesQuery, IDonationsQuery donationsQuery, IEmailVerificationsCommand emailVerificationsCommand, IEmailVerificationsQuery emailVerificationsQuery)
        {
            _emailsCommand = emailsCommand;
            _membersQuery = membersQuery;
            _memberContactsQuery = memberContactsQuery;
            _candidatesQuery = candidatesQuery;
            _resumesQuery = resumesQuery;
            _donationsQuery = donationsQuery;
            _emailVerificationsCommand = emailVerificationsCommand;
            _emailVerificationsQuery = emailVerificationsQuery;
        }

        void IFriendsHandler.OnInvitationSent(Invitation invitation)
        {
            // Get the member who is sending the invitation.

            var inviter = _membersQuery.GetMember(invitation.InviterId);
            if (inviter != null)
            {
                if (invitation is FriendInvitation)
                    OnFriendInvitationSent(inviter, (FriendInvitation)invitation);
                else if (invitation is RepresentativeInvitation)
                    OnRepresentativeInvitationSent(inviter, (RepresentativeInvitation)invitation);
            }
        }

        void IFriendsHandler.OnInvitationAccepted(Invitation invitation)
        {
            var inviter = _membersQuery.GetMember(invitation.InviterId);
            var invitee = _membersQuery.GetMember(invitation.InviteeId.Value);

            if (inviter != null && invitee != null)
            {
                if (invitation is FriendInvitation)
                    _emailsCommand.TrySend(new FriendInvitationConfirmationEmail(inviter, invitee));
                else if (invitation is RepresentativeInvitation)
                    _emailsCommand.TrySend(new RepresentativeInvitationConfirmationEmail(inviter, invitee));
            }
        }

        private void OnFriendInvitationSent(Member inviter, FriendInvitation invitation)
        {
            // Extra information.

            var view = new PersonalView(inviter, PersonalContactDegree.FirstDegree, PersonalContactDegree.Public);

            IList<Job> currentJobs = null;
            if (view.CanAccess(PersonalVisibility.CurrentJobs))
            {
                var candidate = _candidatesQuery.GetCandidate(view.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
                currentJobs = resume == null ? null : resume.CurrentJobs;
            }

            var contactCount = view.CanAccess(PersonalVisibility.FriendsList) ? _memberContactsQuery.GetFirstDegreeContacts(inviter.Id).Count : 0;

            if (invitation.InviteeId == null)
            {
                // Non-member.

                DonationRequest donationRequest = null;
                DonationRecipient donationRecipient = null;

                if (invitation.DonationRequestId != null)
                {
                    donationRequest = _donationsQuery.GetRequest(invitation.DonationRequestId.Value);
                    if (donationRequest != null)
                        donationRecipient = _donationsQuery.GetRecipient(donationRequest.RecipientId);
                }

                var email = new ContactInvitationEmail(inviter, invitation, donationRequest, donationRecipient, currentJobs, contactCount);
                _emailsCommand.TrySend(email);
            }
            else
            {
                // Existing member.

                var to = _membersQuery.GetMember(invitation.InviteeId.Value);
                if (to != null)
                {
                    var activation = GetEmailVerification(to, invitation);
                    var email = new FriendInvitationEmail(to, inviter, invitation, activation, currentJobs, contactCount);
                    _emailsCommand.TrySend(email);
                }
            }
        }

        private void OnRepresentativeInvitationSent(ICommunicationUser inviter, RepresentativeInvitation invitation)
        {
            // Currently can only send to other members.

            if (invitation.InviteeId != null)
            {
                // Existing member.

                var to = _membersQuery.GetMember(invitation.InviteeId.Value);
                if (to != null)
                {
                    var activation = GetEmailVerification(to, invitation);
                    var email = new RepresentativeInvitationEmail(to, inviter, invitation, activation);
                    _emailsCommand.TrySend(email);
                }
            }
        }

        private EmailVerification GetEmailVerification(IMember to, Invitation invitation)
        {
            EmailVerification emailVerification = null;

            if (!to.IsActivated)
            {
                // Find an existing activation or create a new one.

                emailVerification = _emailVerificationsQuery.GetEmailVerification(to.Id, invitation.InviteeEmailAddress);
                if (emailVerification == null)
                {
                    emailVerification = new EmailVerification { EmailAddress = to.GetBestEmailAddress().Address, UserId = to.Id };
                    _emailVerificationsCommand.CreateEmailVerification(emailVerification);
                }
            }

            return emailVerification;
        }
    }
}

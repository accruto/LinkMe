using System;
using LinkMe.Domain.Donations;
using LinkMe.Domain.Donations.Commands;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Networking.Queries;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Roles.Representatives.Queries;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Domain.Users.Members.Friends.Commands
{
    public class MemberFriendsCommand
        : IMemberFriendsCommand
    {
        private readonly INetworkingCommand _networkingCommand;
        private readonly INetworkingInvitationsCommand _networkingInvitationsCommand;
        private readonly INetworkingInvitationsQuery _networkingInvitationsQuery;
        private readonly IRepresentativesCommand _representativesCommand;
        private readonly IRepresentativeInvitationsCommand _representativeInvitationsCommand;
        private readonly IRepresentativeInvitationsQuery _representativeInvitationsQuery;
        private readonly IDonationsCommand _donationsCommand;
        private readonly int _invitationResendIntervalDays;
        private readonly int _dailySendLimit;

        public MemberFriendsCommand(INetworkingCommand networkingCommand, INetworkingInvitationsCommand networkingInvitationsCommand, INetworkingInvitationsQuery networkingInvitationsQuery, IRepresentativesCommand representativesCommand, IRepresentativeInvitationsCommand representativeInvitationsCommand, IRepresentativeInvitationsQuery representativeInvitationsQuery, IDonationsCommand donationsCommand, int invitationResendIntervalDays, int dailySendLimit)
        {
            _networkingCommand = networkingCommand;
            _networkingInvitationsCommand = networkingInvitationsCommand;
            _networkingInvitationsQuery = networkingInvitationsQuery;
            _representativesCommand = representativesCommand;
            _representativeInvitationsCommand = representativeInvitationsCommand;
            _representativeInvitationsQuery = representativeInvitationsQuery;
            _donationsCommand = donationsCommand;
            _invitationResendIntervalDays = invitationResendIntervalDays;
            _dailySendLimit = dailySendLimit;
        }

        void IMemberFriendsCommand.DeleteFriend(Guid fromId, Guid toId)
        {
            // Need to delete any representative relationship as well.

            _representativesCommand.DeleteRepresentative(fromId, toId);
            _representativesCommand.DeleteRepresentative(toId, fromId);

            _networkingCommand.DeleteFirstDegreeLink(fromId, toId);
        }

        void IMemberFriendsCommand.DeleteRepresentative(Guid representeeId, Guid representativeId)
        {
            _representativesCommand.DeleteRepresentative(representeeId, representativeId);
        }

        void IMemberFriendsCommand.IgnoreFriend(Guid fromId, Guid toId)
        {
            _networkingCommand.IgnoreSecondDegreeLink(fromId, toId);
        }

        void IMemberFriendsCommand.AcceptInvitation(Guid inviteeId, FriendInvitation invitation)
        {
            _networkingInvitationsCommand.AcceptInvitation(inviteeId, invitation);

            // Since the invitee is accepting the invitation make them friends.

            _networkingCommand.CreateFirstDegreeLink(invitation.InviterId, inviteeId);

            // Fire an event.

            var handlers = InvitationAccepted;
            if (handlers != null)
                handlers(this, new EventArgs<Invitation>(invitation));
        }

        void IMemberFriendsCommand.RejectInvitation(FriendInvitation invitation)
        {
            _networkingInvitationsCommand.RejectInvitation(invitation);
        }

        void IMemberFriendsCommand.AcceptInvitation(Guid inviteeId, RepresentativeInvitation invitation)
        {
            _representativeInvitationsCommand.AcceptInvitation(inviteeId, invitation);

            // Since the invitee is accepting the invitation make them a representative.

            _representativesCommand.CreateRepresentative(invitation.InviterId, inviteeId);

            // Also, make them friends.

            _networkingCommand.CreateFirstDegreeLink(invitation.InviterId, inviteeId);

            // Fire an event.

            var handlers = InvitationAccepted;
            if (handlers != null)
                handlers(this, new EventArgs<Invitation>(invitation));
        }

        void IMemberFriendsCommand.RejectInvitation(RepresentativeInvitation invitation)
        {
            _representativeInvitationsCommand.RejectInvitation(invitation);
        }

        bool IMemberFriendsCommand.CanSendInvitation(FriendInvitation invitation)
        {
            return CanSend(invitation);
        }

        DateTime IMemberFriendsCommand.GetAllowedSendingTime(FriendInvitation invitation)
        {
            return GetAllowedSendingTime(invitation);
        }

        void IMemberFriendsCommand.SendInvitation(FriendInvitation invitation)
        {
            CheckNetworkingSendLimit(invitation.InviterId);

            // Send this invitation.

            _networkingInvitationsCommand.SendInvitation(invitation);

            // Create the donation for the invitation.

            if (invitation.DonationRequestId != null)
                _donationsCommand.CreateDonation(new Donation { Id = invitation.Id, RequestId = invitation.DonationRequestId.Value });

            // Fire an event.

            var handlers = InvitationSent;
            if (handlers != null)
                handlers(this, new EventArgs<Invitation>(invitation));
        }

        bool IMemberFriendsCommand.CanSendInvitation(RepresentativeInvitation invitation)
        {
            return CanSend(invitation);
        }

        DateTime IMemberFriendsCommand.GetAllowedSendingTime(RepresentativeInvitation invitation)
        {
            return GetAllowedSendingTime(invitation);
        }

        void IMemberFriendsCommand.SendInvitation(RepresentativeInvitation invitation)
        {
            CheckRepresentativeSendLimit(invitation.InviterId);

            // Send this invitation.

            _representativeInvitationsCommand.SendInvitation(invitation);

            // Fire an event.

            var handlers = InvitationSent;
            if (handlers != null)
                handlers(this, new EventArgs<Invitation>(invitation));
        }

        [Publishes(PublishedEvents.InvitationSent)]
        public event EventHandler<EventArgs<Invitation>> InvitationSent;

        [Publishes(PublishedEvents.InvitationAccepted)]
        public event EventHandler<EventArgs<Invitation>> InvitationAccepted;

        private bool CanSend(Request invitation)
        {
            switch (invitation.Status)
            {
                case RequestStatus.Accepted:
                    return false;

                case RequestStatus.Declined:
                    return false;

                case RequestStatus.NotSent:
                    return true;

                case RequestStatus.Pending:
                    return invitation.LastSentTime < DateTime.Now.AddDays(-1 * _invitationResendIntervalDays);

                default:
                    return false;
            }
        }

        private DateTime GetAllowedSendingTime(Request invitation)
        {
            return CanSend(invitation) ? DateTime.Now : invitation.LastSentTime.GetValueOrDefault().AddDays(_invitationResendIntervalDays);
        }

        private void CheckNetworkingSendLimit(Guid memberId)
        {
            var count = _networkingInvitationsQuery.GetInvitationCount(memberId, new DayRange(DateTime.Now.Date));

            // Check whether the current count + the new member puts them over the limit.

            if (count >= _dailySendLimit)
                throw new DailyLimitException();
        }

        private void CheckRepresentativeSendLimit(Guid memberId)
        {
            var count = _representativeInvitationsQuery.GetInvitationCount(memberId, new DayRange(DateTime.Now.Date));

            // Check whether the current count + the new member puts them over the limit.

            if (count >= _dailySendLimit)
                throw new DailyLimitException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Donations.Queries;
using LinkMe.Domain.Roles.Networking.Queries;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Roles.Representatives.Queries;

namespace LinkMe.Domain.Users.Members.Friends.Queries
{
    public class MemberFriendsQuery
        : IMemberFriendsQuery
    {
        private readonly INetworkingInvitationsQuery _networkingInvitationsQuery;
        private readonly IRepresentativeInvitationsQuery _representativeInvitationsQuery;
        private readonly IDonationsQuery _donationsQuery;

        public MemberFriendsQuery(INetworkingInvitationsQuery networkingInvitationsQuery, IRepresentativeInvitationsQuery representativeInvitationsQuery, IDonationsQuery donationsQuery)
        {
            _networkingInvitationsQuery = networkingInvitationsQuery;
            _representativeInvitationsQuery = representativeInvitationsQuery;
            _donationsQuery = donationsQuery;
        }

        FriendInvitation IMemberFriendsQuery.GetFriendInvitation(Guid id)
        {
            return GetInvitation(_networkingInvitationsQuery.GetInvitation<FriendInvitation>(id));
        }

        FriendInvitation IMemberFriendsQuery.GetFriendInvitation(Guid inviterId, Guid inviteeId)
        {
            return GetInvitation(_networkingInvitationsQuery.GetInvitation<FriendInvitation>(inviterId, inviteeId));
        }

        FriendInvitation IMemberFriendsQuery.GetFriendInvitation(Guid inviterId, string inviteeEmailAddress)
        {
            return GetInvitation(_networkingInvitationsQuery.GetInvitation<FriendInvitation>(inviterId, inviteeEmailAddress));
        }

        RepresentativeInvitation IMemberFriendsQuery.GetRepresentativeInvitation(Guid id)
        {
            return _representativeInvitationsQuery.GetInvitation(id);
        }

        RepresentativeInvitation IMemberFriendsQuery.GetRepresentativeInvitation(Guid inviterId, Guid inviteeId)
        {
            return _representativeInvitationsQuery.GetInvitation(inviterId, inviteeId);
        }

        IList<FriendInvitation> IMemberFriendsQuery.GetFriendInvitations(Guid inviteeId, string inviteeEmailAddress)
        {
            return GetInvitations(_networkingInvitationsQuery.GetInvitations<FriendInvitation>(inviteeId, inviteeEmailAddress));
        }

        IList<FriendInvitation> IMemberFriendsQuery.GetFriendInvitations(Guid inviterId)
        {
            return GetInvitations(_networkingInvitationsQuery.GetInvitations<FriendInvitation>(inviterId));
        }

        IList<RepresentativeInvitation> IMemberFriendsQuery.GetRepresentativeInvitations(Guid inviteeId, string inviteeEmailAddress)
        {
            return _representativeInvitationsQuery.GetInvitations(inviteeId, inviteeEmailAddress);
        }

        RepresentativeInvitation IMemberFriendsQuery.GetRepresentativeInvitationByInviter(Guid inviterId)
        {
            return _representativeInvitationsQuery.GetRepresentativeInvitationByInviter(inviterId);
        }

        private FriendInvitation GetInvitation(FriendInvitation invitation)
        {
            if (invitation == null)
                return null;

            // Need to get the donation.

            var donation  = _donationsQuery.GetDonation(invitation.Id);
            if (donation != null)
                invitation.DonationRequestId = donation.RequestId;
            return invitation;
        }

        private IList<FriendInvitation> GetInvitations(IList<FriendInvitation> invitations)
        {
            var donations = _donationsQuery.GetDonations(from i in invitations select i.Id);
            foreach (var invitation in invitations)
            {
                var invitationId = invitation.Id;
                var donation = (from d in donations where d.Id == invitationId select d).SingleOrDefault();
                if (donation != null)
                    invitation.DonationRequestId = donation.RequestId;
            }

            return invitations;
        }
    }
}
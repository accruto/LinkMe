using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Representatives;

namespace LinkMe.Domain.Users.Members.Friends.Queries
{
    public interface IMemberFriendsQuery
    {
        FriendInvitation GetFriendInvitation(Guid id);
        FriendInvitation GetFriendInvitation(Guid inviterId, Guid inviteeId);
        FriendInvitation GetFriendInvitation(Guid inviterId, string inviteeEmailAddress);

        RepresentativeInvitation GetRepresentativeInvitation(Guid id);
        RepresentativeInvitation GetRepresentativeInvitation(Guid inviterId, Guid inviteeId);

        IList<FriendInvitation> GetFriendInvitations(Guid inviteeId, string inviteeEmailAddress);
        IList<FriendInvitation> GetFriendInvitations(Guid inviterId);

        IList<RepresentativeInvitation> GetRepresentativeInvitations(Guid inviteeId, string inviteeEmailAddress);
        RepresentativeInvitation GetRepresentativeInvitationByInviter(Guid inviterId);
    }
}
using System;
using LinkMe.Domain.Roles.Representatives;

namespace LinkMe.Domain.Users.Members.Friends.Commands
{
    public interface IMemberFriendsCommand
    {
        void DeleteFriend(Guid fromId, Guid toId);
        void DeleteRepresentative(Guid representeeId, Guid representativeId);

        void IgnoreFriend(Guid fromId, Guid toId);

        void AcceptInvitation(Guid inviteeId, FriendInvitation invitation);
        void RejectInvitation(FriendInvitation invitation);

        void AcceptInvitation(Guid inviteeId, RepresentativeInvitation invitation);
        void RejectInvitation(RepresentativeInvitation invitation);

        bool CanSendInvitation(FriendInvitation invitation);
        DateTime GetAllowedSendingTime(FriendInvitation invitation);
        void SendInvitation(FriendInvitation invitation);

        bool CanSendInvitation(RepresentativeInvitation invitation);
        DateTime GetAllowedSendingTime(RepresentativeInvitation invitation);
        void SendInvitation(RepresentativeInvitation invitation);
    }
}
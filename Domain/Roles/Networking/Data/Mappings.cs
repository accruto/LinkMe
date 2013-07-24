using System;
using LinkMe.Domain.Requests.Data;
using LinkMe.Domain.Roles.Invitations.Data;

namespace LinkMe.Domain.Roles.Networking.Data
{
    internal enum NetworkMatchCategory : byte
    {
        CommonFriends = 1,
        Colleagues = 2,
        Group = 3,
        WorkedInDesiredJob = 4,
        Interests = 5
    }

    internal partial class UserToUserRequestEntity
        : IUserToUserRequestEntity
    {
    }

    internal partial class NetworkInvitationEntity
        : IInvitationEntity
    {
        IUserToUserRequestEntity IInvitationEntity.UserToUserRequestEntity
        {
            get { return UserToUserRequestEntity; }
        }
    }

    internal static class Mappings
    {
        public static NetworkLinkEntity CreateNetworkLinkEntity(Guid fromId, Guid toId, DateTime time)
        {
            return new NetworkLinkEntity
            {
                addedTime = time,
                fromNetworkerId = fromId,
                toNetworkerId = toId,
            };
        }

        public static IgnoredNetworkMatchEntity CreateIgnoredNetworkMatchEntity(Guid fromId, Guid toId)
        {
            return new IgnoredNetworkMatchEntity
            {
                id = Guid.NewGuid(),
                category = (byte) NetworkMatchCategory.CommonFriends,
                ignorerId = fromId,
                ignoredId = toId,
                time = DateTime.Now
            };
        }

        public static NetworkInvitationEntity Map(this NetworkingInvitation invitation)
        {
            return new NetworkInvitationEntity
            {
                UserToUserRequestEntity = invitation.MapTo<UserToUserRequestEntity>(),
                inviterId = invitation.InviterId,
                inviteeId = invitation.InviteeId,
                inviteeEmailAddress = invitation.InviteeEmailAddress,
            };
        }

        public static void MapTo(this NetworkingInvitation invitation, NetworkInvitationEntity entity)
        {
            invitation.MapTo(entity.UserToUserRequestEntity);
            entity.inviterId = invitation.InviterId;
            entity.inviteeId = invitation.InviteeId;
            entity.inviteeEmailAddress = invitation.InviteeEmailAddress;
        }
    }
}

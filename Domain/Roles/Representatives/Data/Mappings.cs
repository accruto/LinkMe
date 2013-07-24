using LinkMe.Domain.Requests.Data;
using LinkMe.Domain.Roles.Invitations.Data;

namespace LinkMe.Domain.Roles.Representatives.Data
{
    internal partial class UserToUserRequestEntity
        : IUserToUserRequestEntity
    {
    }

    internal partial class RepresentativeInvitationEntity
        : IInvitationEntity
    {
        IUserToUserRequestEntity IInvitationEntity.UserToUserRequestEntity
        {
            get { return UserToUserRequestEntity; }
        }
    }

    internal static class Mappings
    {
        public static RepresentativeInvitationEntity Map(this RepresentativeInvitation invitation)
        {
            return new RepresentativeInvitationEntity
            {
                UserToUserRequestEntity = invitation.MapTo<UserToUserRequestEntity>(),
                inviterId = invitation.InviterId,
                inviteeId = invitation.InviteeId,
                inviteeEmailAddress = invitation.InviteeEmailAddress,
            };
        }

        public static void MapTo(this RepresentativeInvitation invitation, RepresentativeInvitationEntity entity)
        {
            invitation.MapTo(entity.UserToUserRequestEntity);
            entity.inviterId = invitation.InviterId;
            entity.inviteeId = invitation.InviteeId;
            entity.inviteeEmailAddress = invitation.InviteeEmailAddress;
        }
    }
}

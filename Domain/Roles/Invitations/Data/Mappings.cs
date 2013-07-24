using System;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Requests.Data;

namespace LinkMe.Domain.Roles.Invitations.Data
{
    public interface IInvitationEntity
    {
        IUserToUserRequestEntity UserToUserRequestEntity { get; }
        Guid inviterId { get; set; }
        Guid? inviteeId { get; set; }
        string inviteeEmailAddress { get; set; }
    }

    public interface IInvitationFactory
    {
        Invitation CreateInvitation();
    }

    internal class InvitationFactory<TInvitation>
        : IInvitationFactory
        where TInvitation : Invitation, new()
    {
        public Invitation CreateInvitation()
        {
            return new TInvitation();
        }
    }

    public static class Mappings
    {
        public static Invitation Map(this IInvitationEntity entity, IInvitationFactory factory)
        {
            var invitation = factory.CreateInvitation();
            entity.UserToUserRequestEntity.MapTo(invitation);
            invitation.InviterId = entity.inviterId;
            invitation.InviteeId = entity.inviteeId;
            invitation.InviteeEmailAddress = entity.inviteeEmailAddress;
            return invitation;
        }
    }
}

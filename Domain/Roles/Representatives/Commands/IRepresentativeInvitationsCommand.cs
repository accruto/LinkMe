using System;

namespace LinkMe.Domain.Roles.Representatives.Commands
{
    public interface IRepresentativeInvitationsCommand
    {
        void CreateInvitation(RepresentativeInvitation invitation);
        void UpdateInvitation(RepresentativeInvitation invitation);

        void SendInvitation(RepresentativeInvitation invitation);
        void AcceptInvitation(Guid inviteeId, RepresentativeInvitation invitation);
        void RejectInvitation(RepresentativeInvitation invitation);
    }
}
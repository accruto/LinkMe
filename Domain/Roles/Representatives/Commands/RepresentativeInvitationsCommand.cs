using System;
using LinkMe.Domain.Roles.Invitations.Commands;

namespace LinkMe.Domain.Roles.Representatives.Commands
{
    public class RepresentativeInvitationsCommand
        : InvitationsCommand<RepresentativeInvitation>, IRepresentativeInvitationsCommand
    {
        public RepresentativeInvitationsCommand(IRepresentativesRepository repository, int invitationAccessDays)
            : base(repository, invitationAccessDays)
        {
        }

        void IRepresentativeInvitationsCommand.CreateInvitation(RepresentativeInvitation invitation)
        {
            CreateInvitation(invitation);
        }

        void IRepresentativeInvitationsCommand.UpdateInvitation(RepresentativeInvitation invitation)
        {
            UpdateInvitation(invitation);
        }

        void IRepresentativeInvitationsCommand.SendInvitation(RepresentativeInvitation invitation)
        {
            // There can only be one active invitation so revoke all others.

            RevokeInvitations(invitation.InviterId);

            // Send the new invitation.

            SendInvitation(invitation);
        }

        void IRepresentativeInvitationsCommand.RejectInvitation(RepresentativeInvitation invitation)
        {
            RejectInvitation(invitation);
        }

        void IRepresentativeInvitationsCommand.AcceptInvitation(Guid inviteeId, RepresentativeInvitation invitation)
        {
            AcceptInvitation(inviteeId, invitation);
        }
    }
}
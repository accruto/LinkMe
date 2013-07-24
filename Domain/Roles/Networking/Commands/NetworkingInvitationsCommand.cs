using System;
using LinkMe.Domain.Roles.Invitations.Commands;

namespace LinkMe.Domain.Roles.Networking.Commands
{
    public class NetworkingInvitationsCommand
        : InvitationsCommand<NetworkingInvitation>, INetworkingInvitationsCommand
    {
        public NetworkingInvitationsCommand(INetworkingRepository repository, int invitationAccessDays)
            : base(repository, invitationAccessDays)
        {
        }

        void INetworkingInvitationsCommand.CreateInvitation(NetworkingInvitation invitation)
        {
            CreateInvitation(invitation);
        }

        void INetworkingInvitationsCommand.UpdateInvitation(NetworkingInvitation invitation)
        {
            UpdateInvitation(invitation);
        }

        void INetworkingInvitationsCommand.SendInvitation(NetworkingInvitation invitation)
        {
            SendInvitation(invitation);
        }

        void INetworkingInvitationsCommand.RejectInvitation(NetworkingInvitation invitation)
        {
            RejectInvitation(invitation);
        }

        void INetworkingInvitationsCommand.AcceptInvitation(Guid inviteeId, NetworkingInvitation invitation)
        {
            AcceptInvitation(inviteeId, invitation);
        }
    }
}
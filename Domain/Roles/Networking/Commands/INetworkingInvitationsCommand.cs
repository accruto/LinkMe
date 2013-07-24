using System;

namespace LinkMe.Domain.Roles.Networking.Commands
{
    public interface INetworkingInvitationsCommand
    {
        void CreateInvitation(NetworkingInvitation invitation);
        void UpdateInvitation(NetworkingInvitation invitation);

        void AcceptInvitation(Guid inviteeId, NetworkingInvitation invitation);
        void RejectInvitation(NetworkingInvitation invitation);

        void SendInvitation(NetworkingInvitation invitation);
    }
}
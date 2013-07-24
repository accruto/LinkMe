using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Networking.Queries
{
    public interface INetworkingInvitationsQuery
    {
        TInvitation GetInvitation<TInvitation>(Guid id) where TInvitation : NetworkingInvitation, new();
        TInvitation GetInvitation<TInvitation>(Guid inviterId, Guid inviteeId) where TInvitation : NetworkingInvitation, new();
        TInvitation GetInvitation<TInvitation>(Guid inviterId, string inviteeEmailAddress) where TInvitation : NetworkingInvitation, new();

        IList<TInvitation> GetInvitations<TInvitation>(Guid inviteeId, string inviteeEmailAddress) where TInvitation : NetworkingInvitation, new();
        IList<TInvitation> GetInvitations<TInvitation>(Guid inviterId) where TInvitation : NetworkingInvitation, new();
        int GetInvitationCount(Guid inviterId, DateTimeRange dateTimeRange);
    }
}

using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Invitations.Queries;

namespace LinkMe.Domain.Roles.Networking.Queries
{
    public class NetworkingInvitationsQuery
        : InvitationsQuery<NetworkingInvitation>, INetworkingInvitationsQuery
    {
        public NetworkingInvitationsQuery(INetworkingRepository repository, int invitationAccessDays)
            : base(repository, invitationAccessDays)
        {
        }

        TInvitation INetworkingInvitationsQuery.GetInvitation<TInvitation>(Guid id)
        {
            return GetInvitation<TInvitation>(id);
        }

        TInvitation INetworkingInvitationsQuery.GetInvitation<TInvitation>(Guid inviterId, Guid inviteeId)
        {
            return GetInvitation<TInvitation>(inviterId, inviteeId);
        }

        TInvitation INetworkingInvitationsQuery.GetInvitation<TInvitation>(Guid inviterId, string inviteeEmailAddress)
        {
            return GetInvitation<TInvitation>(inviterId, inviteeEmailAddress);
        }

        IList<TInvitation> INetworkingInvitationsQuery.GetInvitations<TInvitation>(Guid inviterId)
        {
            return GetInvitations<TInvitation>(inviterId);
        }

        int INetworkingInvitationsQuery.GetInvitationCount(Guid inviterId, DateTimeRange dateTimeRange)
        {
            return GetInvitationCount(inviterId, dateTimeRange);
        }

        IList<TInvitation> INetworkingInvitationsQuery.GetInvitations<TInvitation>(Guid inviteeId, string inviteeEmailAddress)
        {
            return GetInvitations<TInvitation>(inviteeId, inviteeEmailAddress);
        }
    }
}
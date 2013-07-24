using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Invitations.Queries;

namespace LinkMe.Domain.Roles.Representatives.Queries
{
    public class RepresentativeInvitationsQuery
        : InvitationsQuery<RepresentativeInvitation>, IRepresentativeInvitationsQuery
    {
        public RepresentativeInvitationsQuery(IRepresentativesRepository repository, int invitationAccessDays)
            : base(repository, invitationAccessDays)
        {
        }

        RepresentativeInvitation IRepresentativeInvitationsQuery.GetInvitation(Guid id)
        {
            return GetInvitation<RepresentativeInvitation>(id);
        }

        RepresentativeInvitation IRepresentativeInvitationsQuery.GetInvitation(Guid inviterId, Guid inviteeId)
        {
            return GetInvitation<RepresentativeInvitation>(inviterId, inviteeId);
        }

        RepresentativeInvitation IRepresentativeInvitationsQuery.GetRepresentativeInvitationByInviter(Guid inviterId)
        {
            var invitations = GetInvitations<RepresentativeInvitation>(inviterId);

            // Should be only 1.

            return invitations.Count == 0 ? null : invitations[0];
        }

        IList<RepresentativeInvitation> IRepresentativeInvitationsQuery.GetInvitations(Guid inviteeId, string inviteeEmailAddress)
        {
            return GetInvitations<RepresentativeInvitation>(inviteeId, inviteeEmailAddress);
        }

        int IRepresentativeInvitationsQuery.GetInvitationCount(Guid inviterId, DateTimeRange dateTimeRange)
        {
            return GetInvitationCount(inviterId, dateTimeRange);
        }
    }
}
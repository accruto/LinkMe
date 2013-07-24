using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Representatives.Queries
{
    public interface IRepresentativeInvitationsQuery
    {
        RepresentativeInvitation GetInvitation(Guid id);
        RepresentativeInvitation GetInvitation(Guid inviterId, Guid inviteeId);
        RepresentativeInvitation GetRepresentativeInvitationByInviter(Guid inviterId);
        IList<RepresentativeInvitation> GetInvitations(Guid inviteeId, string emailAddress);
        int GetInvitationCount(Guid inviterId, DateTimeRange dateTimeRange);
    }
}
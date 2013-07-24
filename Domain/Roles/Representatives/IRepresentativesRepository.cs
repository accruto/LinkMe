using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Invitations;

namespace LinkMe.Domain.Roles.Representatives
{
    public interface IRepresentativesRepository
        : IInvitationsRepository<RepresentativeInvitation>
    {
        void CreateRepresentative(Guid representeeId, Guid representativeId);
        void DeleteRepresentative(Guid representeeId, Guid representativeId);

        Guid? GetRepresentativeId(Guid representeeId);
        IDictionary<Guid, Guid> GetRepresentativeIds(IEnumerable<Guid> representeeIds);

        IList<Guid> GetRepresenteeIds(Guid representativeId);
        IList<Guid> GetRepresenteeIds(Guid representativeId, IEnumerable<Guid> representeeIds);
    }
}

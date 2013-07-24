using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Representatives.Queries
{
    public interface IRepresentativesQuery
    {
        Guid? GetRepresentativeId(Guid representeeId);
        IDictionary<Guid, Guid> GetRepresentativeIds(IEnumerable<Guid> representeeIds);

        IList<Guid> GetRepresenteeIds(Guid representativeId);
        IList<Guid> GetRepresenteeIds(Guid representativeId, IEnumerable<Guid> representeesId);
    }
}
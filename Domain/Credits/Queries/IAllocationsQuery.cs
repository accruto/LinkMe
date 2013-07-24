using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Credits.Queries
{
    public interface IAllocationsQuery
    {
        Allocation GetAllocation(Guid id);
        IList<Allocation> GetAllocations(IEnumerable<Guid> ids);

        IList<Allocation> GetAllocationsByOwnerId(Guid ownerId);
        IList<Allocation> GetAllocationsByOwnerId<T>(Guid ownerId) where T : Credit;
        IDictionary<Guid, IList<Allocation>> GetAllocationsByOwnerId(IEnumerable<Guid> ownerIds);

        IList<Allocation> GetAllocationsByReferenceId(Guid referenceId);

        IList<Allocation> GetActiveAllocations(Guid ownerId);
        IList<Allocation> GetActiveAllocations<T>(Guid ownerId) where T : Credit;
        IList<Allocation> GetActiveAllocations(Guid ownerId, Guid creditId);
        IDictionary<Guid, IList<Allocation>> GetActiveAllocations(IEnumerable<Guid> ownerIds, IEnumerable<Guid> creditIds);

        IDictionary<Guid, IList<Allocation>> GetExpiringAllocations(Guid creditId, DateTime expiryDate);
    }
}
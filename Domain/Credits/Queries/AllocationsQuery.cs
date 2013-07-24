using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Credits.Queries
{
    public class AllocationsQuery
        : IAllocationsQuery
    {
        private readonly ICreditsRepository _repository;
        private readonly ICreditsQuery _creditsQuery;

        public AllocationsQuery(ICreditsRepository repository, ICreditsQuery creditsQuery)
        {
            _repository = repository;
            _creditsQuery = creditsQuery;
        }

        Allocation IAllocationsQuery.GetAllocation(Guid id)
        {
            return _repository.GetAllocation(id);
        }

        IList<Allocation> IAllocationsQuery.GetAllocations(IEnumerable<Guid> ids)
        {
            return _repository.GetAllocations(ids);
        }

        IList<Allocation> IAllocationsQuery.GetAllocationsByOwnerId(Guid ownerId)
        {
            return _repository.GetAllocationsByOwnerId(ownerId);
        }

        IList<Allocation> IAllocationsQuery.GetAllocationsByOwnerId<T>(Guid ownerId)
        {
            return _repository.GetAllocationsByOwnerId(ownerId, _creditsQuery.GetCredit<T>().Id);
        }

        IDictionary<Guid, IList<Allocation>> IAllocationsQuery.GetAllocationsByOwnerId(IEnumerable<Guid> ownerIds)
        {
            return _repository.GetAllocationsByOwnerId(ownerIds);
        }

        IList<Allocation> IAllocationsQuery.GetAllocationsByReferenceId(Guid referenceId)
        {
            return _repository.GetAllocationsByReferenceId(referenceId);
        }

        IList<Allocation> IAllocationsQuery.GetActiveAllocations(Guid ownerId)
        {
            return _repository.GetActiveAllocations(ownerId);
        }

        IList<Allocation> IAllocationsQuery.GetActiveAllocations<T>(Guid ownerId)
        {
            return _repository.GetActiveAllocations(ownerId, _creditsQuery.GetCredit<T>().Id);
        }

        IList<Allocation> IAllocationsQuery.GetActiveAllocations(Guid ownerId, Guid creditId)
        {
            return _repository.GetActiveAllocations(ownerId, creditId);
        }

        IDictionary<Guid, IList<Allocation>> IAllocationsQuery.GetActiveAllocations(IEnumerable<Guid> ownerIds, IEnumerable<Guid> creditIds)
        {
            return _repository.GetActiveAllocations(ownerIds, creditIds);
        }

        IDictionary<Guid, IList<Allocation>> IAllocationsQuery.GetExpiringAllocations(Guid creditId, DateTime expiryDate)
        {
            return _repository.GetExpiringAllocations(creditId, expiryDate);
        }
    }
}
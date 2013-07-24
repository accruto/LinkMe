using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Credits
{
    public interface ICreditsRepository
    {
        IList<Credit> GetCredits();

        void CreateAllocation(Allocation allocation);
        void UpdateAllocation(Allocation allocation);
        void UpdateAllocations(IEnumerable<Allocation> allocations);

        Allocation GetAllocation(Guid id);
        IList<Allocation> GetAllocations(IEnumerable<Guid> ids);

        IList<Allocation> GetAllocationsByOwnerId(Guid ownerId);
        IList<Allocation> GetAllocationsByOwnerId(Guid ownerId, Guid creditId);
        IDictionary<Guid, IList<Allocation>> GetAllocationsByOwnerId(IEnumerable<Guid> ownerIds);

        IList<Allocation> GetAllocationsByReferenceId(Guid referenceId);

        IList<Allocation> GetActiveAllocations(Guid ownerId);
        IList<Allocation> GetActiveAllocations(Guid ownerId, Guid creditId);
        IDictionary<Guid, IList<Allocation>> GetActiveAllocations(IEnumerable<Guid> ownerIds, IEnumerable<Guid> creditIds);

        IDictionary<Guid, IList<Allocation>> GetExpiringAllocations(Guid creditId, DateTime expiryDate);

        void CreateExercisedCredit(ExercisedCredit credit);
        void CreateExercisedCredits(IEnumerable<ExercisedCredit> credits);

        bool HasExercisedCredit(Guid creditId, HierarchyPath hierarchyPath, Guid exercisedOnId);
        IList<Guid> HasExercisedCredits(Guid[] creditIds, HierarchyPath hierarchyPath);
        IList<Guid> HasExercisedCredits(Guid[] creditIds, HierarchyPath hierarchyPath, IEnumerable<Guid> exercisedOnIds);

        ExercisedCredit GetExercisedCredit(Guid id);
        IList<ExercisedCredit> GetExercisedCredits(Guid creditId, HierarchyPath hierarchyPath);
        IList<ExercisedCredit> GetExercisedCredits(Guid creditId, HierarchyPath hierarchyPath, IEnumerable<Guid> exercisedOnIds);

        IList<ExercisedCredit> GetExercisedCredits(Guid allocationId);
        IList<ExercisedCredit> GetExercisedCreditsByExerciserId(Guid exercisedById, DateTimeRange timeRange);
        IList<ExercisedCredit> GetExercisedCreditsByOwnerId(Guid ownerId, DateTimeRange timeRange);
    }
}

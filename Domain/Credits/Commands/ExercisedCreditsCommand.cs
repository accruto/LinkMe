using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Domain.Credits.Commands
{
    public class ExercisedCreditsCommand
        : IExercisedCreditsCommand
    {
        private readonly ICreditsRepository _repository;

        public ExercisedCreditsCommand(ICreditsRepository repository)
        {
            _repository = repository;
        }

        Guid? IExercisedCreditsCommand.ExerciseCredit(Guid creditId, HierarchyPath hierarchyPath, bool adjustAllocation, Guid exercisedById, Guid? exercisedOnId, Guid? referenceId)
        {
            return ExerciseCredits(creditId, hierarchyPath, adjustAllocation, exercisedById, new[] { exercisedOnId ?? Guid.Empty }, referenceId)[exercisedOnId ?? Guid.Empty];
        }

        IDictionary<Guid, Guid?> IExercisedCreditsCommand.ExerciseCredits(Guid creditId, HierarchyPath hierarchyPath, bool adjustAllocation, Guid exercisedById, IEnumerable<Guid> exercisedOnIds, Guid? referenceId)
        {
            return ExerciseCredits(creditId, hierarchyPath, adjustAllocation, exercisedById, exercisedOnIds, referenceId);
        }

        private IDictionary<Guid, Guid?> ExerciseCredits(Guid creditId, IEnumerable<Guid> hierarchyPath, bool adjustAllocation, Guid exercisedById, IEnumerable<Guid> exercisedOnIds, Guid? referenceId)
        {
            var exercisedCredits = new Dictionary<Guid, ExercisedCredit>();
            var exercisedOwnerIds = new List<Guid>();

            var allocations = _repository.GetActiveAllocations(hierarchyPath, new[] { creditId });
            var originalRemainingQuantities = allocations.SelectMany(a => a.Value).ToDictionary(a => a.Id, a => a.RemainingQuantity);

            foreach (var exercisedOnId in exercisedOnIds)
            {
                // Iterate through the owners looking for someone who can exercise the credit.

                ExercisedCredit exercisedCredit = null;
                foreach (var ownerId in hierarchyPath)
                {
                    exercisedCredit = ExerciseCredit(allocations[ownerId], adjustAllocation, exercisedById, exercisedOnId, referenceId);
                    if (exercisedCredit != null)
                    {
                        if (!exercisedOwnerIds.Contains(ownerId))
                            exercisedOwnerIds.Add(ownerId);
                        break;
                    }
                }

                exercisedCredits[exercisedOnId] = exercisedCredit;
            }

            // Create all exercised credits.

            _repository.CreateExercisedCredits(from c in exercisedCredits where c.Value != null select c.Value);

            // Now update all allocations where needed.

            var changedAllocations = from a in allocations.SelectMany(a => a.Value)
                                     where a.RemainingQuantity != originalRemainingQuantities[a.Id]
                                     select a;
            if (changedAllocations.Any())
                _repository.UpdateAllocations(changedAllocations);

            // Fire events.

            if (exercisedOwnerIds.Count > 0)
            {
                var handlers = CreditExercised;
                if (handlers != null)
                {
                    foreach (var exercisedOwnerId in exercisedOwnerIds)
                        handlers(this, new CreditExercisedEventArgs(creditId, exercisedOwnerId, (from a in changedAllocations where a.OwnerId == exercisedOwnerId select a).Any()));
                }
            }

            return exercisedCredits.ToDictionary(c => c.Key, c => c.Value == null ? (Guid?)null : c.Value.Id);
        }

        [Publishes(PublishedEvents.CreditExercised)]
        public event EventHandler<CreditExercisedEventArgs> CreditExercised;

        private static ExercisedCredit ExerciseCredit(IEnumerable<Allocation> allocations, bool adjustAllocation, Guid exercisedById, Guid? exercisedOnId, Guid? referenceId)
        {
            Allocation earliest = null;

            foreach (var allocation in allocations)
            {
                // An unlimited quantity means there is nothing to exercise.

                if (allocation.IsUnlimited)
                    return ExerciseUnlimitedAllocation(allocation.Id, exercisedById, exercisedOnId, referenceId);

                // Only consider allocations that have credits.

                if (allocation.RemainingQuantity > 0 && (earliest == null || allocation.ExpiryDate < earliest.ExpiryDate))
                    earliest = allocation;
            }

            // Found the earliest expiring credit of this type. Decrement the quantity.

            if (earliest != null)
                return ExerciseLimitedAllocation(earliest, adjustAllocation, exercisedById, exercisedOnId, referenceId);

            return null;
        }

        private static ExercisedCredit ExerciseLimitedAllocation(Allocation allocation, bool adjustAllocation, Guid exercisedById, Guid? exercisedOnId, Guid? referenceId)
        {
            // Record the exercise.

            var exercisedCredit = CreateExercisedCredit(allocation.Id, adjustAllocation, exercisedById, exercisedOnId, referenceId);

            // Update the allocation itself.

            if (adjustAllocation)
                allocation.RemainingQuantity = allocation.RemainingQuantity.Value - 1;

            return exercisedCredit;
        }

        private static ExercisedCredit ExerciseUnlimitedAllocation(Guid allocationId, Guid exercisedById, Guid? exercisedOnId, Guid? referenceId)
        {
            return CreateExercisedCredit(allocationId, false, exercisedById, exercisedOnId, referenceId);
        }

        private static ExercisedCredit CreateExercisedCredit(Guid allocationId, bool adjustAllocation, Guid exercisedById, Guid? exercisedOnId, Guid? referenceId)
        {
            var exercisedCredit = new ExercisedCredit
            {
                AllocationId = allocationId,
                AdjustedAllocation = adjustAllocation,
                ExercisedById = exercisedById,
                ExercisedOnId = exercisedOnId == Guid.Empty ? null : exercisedOnId,
                ReferenceId = referenceId
            };

            exercisedCredit.Prepare();
            return exercisedCredit;
        }
    }
}
using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Credits.Commands
{
    public interface IExercisedCreditsCommand
    {
        Guid? ExerciseCredit(Guid creditId, HierarchyPath hierarchyPath, bool adjustAllocation, Guid exercisedById, Guid? exercisedOnId, Guid? referenceId);
        IDictionary<Guid, Guid?> ExerciseCredits(Guid creditId, HierarchyPath hierarchyPath, bool adjustAllocation, Guid exercisedById, IEnumerable<Guid> exercisedOnIds, Guid? referenceId);
    }
}
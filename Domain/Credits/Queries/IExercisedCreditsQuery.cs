using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Credits.Queries
{
    public interface IExercisedCreditsQuery
    {
        ExercisedCredit GetExercisedCredit(Guid id);

        IList<ExercisedCredit> GetExercisedCredits<T>(HierarchyPath hierarchyPath) where T : Credit;
        IList<ExercisedCredit> GetExercisedCredits<T>(HierarchyPath hierarchyPath, IEnumerable<Guid> exercisedOnIds) where T : Credit;

        bool HasExercisedCredit<T>(HierarchyPath hierarchyPath, Guid exercisedOnId) where T : Credit;
        IList<Guid> HasExercisedCredits<T1, T2>(HierarchyPath hierarchyPath) where T1 : Credit where T2 : Credit;
        IList<Guid> HasExercisedCredits<T1>(HierarchyPath hierarchyPath, IEnumerable<Guid> exercisedOnIds) where T1 : Credit;
        IList<Guid> HasExercisedCredits<T1, T2>(HierarchyPath hierarchyPath, IEnumerable<Guid> exercisedOnIds) where T1 : Credit where T2 : Credit;

        IList<ExercisedCredit> GetExercisedCredits(Guid allocationId);
        IList<ExercisedCredit> GetExercisedCreditsByExerciserId(Guid exercisedById, DateTimeRange timeRange);
        IList<ExercisedCredit> GetExercisedCreditsByOwnerId(Guid ownerId, DateTimeRange timeRange);
    }
}
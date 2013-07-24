using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Credits.Queries
{
    public class ExercisedCreditsQuery
        : IExercisedCreditsQuery
    {
        private readonly ICreditsRepository _repository;
        private readonly ICreditsQuery _creditsQuery;

        public ExercisedCreditsQuery(ICreditsRepository repository, ICreditsQuery creditsQuery)
        {
            _repository = repository;
            _creditsQuery = creditsQuery;
        }

        bool IExercisedCreditsQuery.HasExercisedCredit<T>(HierarchyPath hierarchyPath, Guid exercisedOnId)
        {
            return _repository.HasExercisedCredit(_creditsQuery.GetCredit<T>().Id, hierarchyPath, exercisedOnId);
        }

        ExercisedCredit IExercisedCreditsQuery.GetExercisedCredit(Guid id)
        {
            return _repository.GetExercisedCredit(id);
        }

        IList<ExercisedCredit> IExercisedCreditsQuery.GetExercisedCredits<T>(HierarchyPath hierarchyPath)
        {
            return _repository.GetExercisedCredits(_creditsQuery.GetCredit<T>().Id, hierarchyPath);
        }

        IList<ExercisedCredit> IExercisedCreditsQuery.GetExercisedCredits<T>(HierarchyPath hierarchyPath, IEnumerable<Guid> exercisedOnIds)
        {
            return _repository.GetExercisedCredits(_creditsQuery.GetCredit<T>().Id, hierarchyPath, exercisedOnIds);
        }

        IList<Guid> IExercisedCreditsQuery.HasExercisedCredits<T1, T2>(HierarchyPath hierarchyPath)
        {
            return _repository.HasExercisedCredits(new[] { _creditsQuery.GetCredit<T1>().Id, _creditsQuery.GetCredit<T2>().Id }, hierarchyPath);
        }

        IList<Guid> IExercisedCreditsQuery.HasExercisedCredits<T1>(HierarchyPath hierarchyPath, IEnumerable<Guid> exercisedOnIds)
        {
            return _repository.HasExercisedCredits(new[] { _creditsQuery.GetCredit<T1>().Id }, hierarchyPath);
        }

        IList<Guid> IExercisedCreditsQuery.HasExercisedCredits<T1, T2>(HierarchyPath hierarchyPath, IEnumerable<Guid> exercisedOnIds)
        {
            return _repository.HasExercisedCredits(new [] {_creditsQuery.GetCredit<T1>().Id, _creditsQuery.GetCredit<T2>().Id}, hierarchyPath, exercisedOnIds);
        }

        IList<ExercisedCredit> IExercisedCreditsQuery.GetExercisedCredits(Guid allocationId)
        {
            return _repository.GetExercisedCredits(allocationId);
        }

        IList<ExercisedCredit> IExercisedCreditsQuery.GetExercisedCreditsByExerciserId(Guid exercisedById, DateTimeRange timeRange)
        {
            return _repository.GetExercisedCreditsByExerciserId(exercisedById, timeRange);
        }

        IList<ExercisedCredit> IExercisedCreditsQuery.GetExercisedCreditsByOwnerId(Guid ownerId, DateTimeRange timeRange)
        {
            return _repository.GetExercisedCreditsByOwnerId(ownerId, timeRange);
        }
    }
}
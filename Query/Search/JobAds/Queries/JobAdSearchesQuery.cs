using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Search.JobAds.Queries
{
    public class JobAdSearchesQuery
        : IJobAdSearchesQuery
    {
        private readonly IJobAdsRepository _repository;

        public JobAdSearchesQuery(IJobAdsRepository repository)
        {
            _repository = repository;
        }

        JobAdSearch IJobAdSearchesQuery.GetJobAdSearch(Guid id)
        {
            return _repository.GetJobAdSearch(id);
        }

        IList<JobAdSearch> IJobAdSearchesQuery.GetJobAdSearches(Guid ownerId)
        {
            return _repository.GetJobAdSearches(ownerId);
        }

        JobAdSearchExecution IJobAdSearchesQuery.GetJobAdSearchExecution(Guid id)
        {
            return _repository.GetJobAdSearchExecution(id);
        }

        IList<JobAdSearchExecution> IJobAdSearchesQuery.GetJobAdSearchExecutions(Guid ownerId, DateTimeRange range)
        {
            return _repository.GetJobAdSearchExecutions(ownerId, range);
        }

        IList<JobAdSearchExecution> IJobAdSearchesQuery.GetJobAdSearchExecutions(Guid ownerId, int maxCount)
        {
            return _repository.GetJobAdSearchExecutions(ownerId, maxCount);
        }

        int IJobAdSearchesQuery.GetJobAdSearchExecutionCount(Guid ownerId, DateTimeRange dateTimeRange)
        {
            return _repository.GetJobAdSearchExecutionCount(ownerId, dateTimeRange);
        }
    }
}
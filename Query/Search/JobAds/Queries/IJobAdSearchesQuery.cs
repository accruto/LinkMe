using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Search.JobAds.Queries
{
    public interface IJobAdSearchesQuery
    {
        JobAdSearch GetJobAdSearch(Guid id);
        IList<JobAdSearch> GetJobAdSearches(Guid ownerId);

        JobAdSearchExecution GetJobAdSearchExecution(Guid id);
        IList<JobAdSearchExecution> GetJobAdSearchExecutions(Guid ownerId, DateTimeRange range);
        IList<JobAdSearchExecution> GetJobAdSearchExecutions(Guid ownerId, int maxCount);
        int GetJobAdSearchExecutionCount(Guid ownerId, DateTimeRange dateTimeRange);
    }
}
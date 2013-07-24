using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Search.JobAds
{
    public interface IJobAdsRepository
    {
        void CreateJobAdSearch(JobAdSearch search);
        void DeleteJobAdSearch(Guid id);
        void UpdateJobAdSearch(JobAdSearch search);
        void RemoveAlertFromSearch(Guid id);

        JobAdSearch GetJobAdSearch(Guid id);
        IList<JobAdSearch> GetJobAdSearches(Guid ownerId);

        void CreateJobAdSearchExecution(JobAdSearchExecution execution, int maxResults);
        JobAdSearchExecution GetJobAdSearchExecution(Guid id);
        IList<JobAdSearchExecution> GetJobAdSearchExecutions(Guid ownerId, DateTimeRange range);
        IList<JobAdSearchExecution> GetJobAdSearchExecutions(Guid ownerId, int maxCount);
        int GetJobAdSearchExecutionCount(Guid ownerId, DateTimeRange dateTimeRange);
    }
}
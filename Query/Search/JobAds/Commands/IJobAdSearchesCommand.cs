using System;

namespace LinkMe.Query.Search.JobAds.Commands
{
    public interface IJobAdSearchesCommand
    {
        void CreateJobAdSearch(Guid creatorId, JobAdSearch search);
        void UpdateJobAdSearch(Guid updaterId, JobAdSearch search);
        void DeleteJobAdSearch(Guid deleterId, JobAdSearch search);
        void DeleteJobAdSearch(Guid deleterId, Guid id);

        void CreateJobAdSearchExecution(JobAdSearchExecution execution);
    }
}
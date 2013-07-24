using System;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Apps.Agents.Communications.Alerts.Commands
{
    public interface IJobAdSearchAlertsCommand
    {
        void CreateJobAdSearch(Guid creatorId, JobAdSearch search);
        void UpdateJobAdSearch(Guid updaterId, JobAdSearch search);
        void DeleteJobAdSearch(Guid deleterId, Guid searchId);

        void CreateJobAdSearchAlert(Guid creatorId, Guid searchId, DateTime lastRunTime);
        void CreateJobAdSearchAlert(Guid creatorId, JobAdSearch search, DateTime lastRunTime);
        void DeleteJobAdSearchAlert(Guid deleterId, Guid searchId);
        void UpdateLastRunTime(Guid searchId, DateTime lastRunTime);
    }
}
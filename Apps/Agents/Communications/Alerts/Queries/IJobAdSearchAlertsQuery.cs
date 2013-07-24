using System;
using System.Collections.Generic;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Apps.Agents.Communications.Alerts.Queries
{
    public interface IJobAdSearchAlertsQuery
    {
        JobAdSearch GetJobAdSearch(Guid jobAdSearchId);
        JobAdSearchAlert GetJobAdSearchAlert(Guid jobAdSearchId);
        IList<JobAdSearchAlert> GetJobAdSearchAlerts(IEnumerable<Guid> jobAdSearchIds);
        IList<JobAdSearchAlert> GetJobAdSearchAlerts();
    }
}
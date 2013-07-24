using System;
using System.Collections.Generic;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Queries;

namespace LinkMe.Apps.Agents.Communications.Alerts.Queries
{
    public class JobAdSearchAlertsQuery
        : IJobAdSearchAlertsQuery
    {
        private readonly ISearchAlertsRepository _repository;
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery;

        public JobAdSearchAlertsQuery(ISearchAlertsRepository repository, IJobAdSearchesQuery jobAdSearchesQuery)
        {
            _repository = repository;
            _jobAdSearchesQuery = jobAdSearchesQuery;
        }

        JobAdSearch IJobAdSearchAlertsQuery.GetJobAdSearch(Guid jobAdSearchId)
        {
            return _jobAdSearchesQuery.GetJobAdSearch(jobAdSearchId);
        }

        JobAdSearchAlert IJobAdSearchAlertsQuery.GetJobAdSearchAlert(Guid jobAdSearchId)
        {
            return _repository.GetJobAdSearchAlert(jobAdSearchId);
        }

        IList<JobAdSearchAlert> IJobAdSearchAlertsQuery.GetJobAdSearchAlerts(IEnumerable<Guid> jobAdSearchIds)
        {
            return _repository.GetJobAdSearchAlerts(jobAdSearchIds);
        }

        IList<JobAdSearchAlert> IJobAdSearchAlertsQuery.GetJobAdSearchAlerts()
        {
            return _repository.GetJobAdSearchAlerts();
        }
    }
}
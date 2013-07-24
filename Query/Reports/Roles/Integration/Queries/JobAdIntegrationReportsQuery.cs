using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Integration.Queries
{
    public class JobAdIntegrationReportsQuery
        : IJobAdIntegrationReportsQuery
    {
        private readonly IIntegrationReportsRepository _repository;

        public JobAdIntegrationReportsQuery(IIntegrationReportsRepository repository)
        {
            _repository = repository;
        }

        IDictionary<Guid, JobAdIntegrationReport> IJobAdIntegrationReportsQuery.GetJobAdIntegrationReports(DateTimeRange timeRange)
        {
            return _repository.GetJobAdIntegrationReports(timeRange);
        }
    }
}

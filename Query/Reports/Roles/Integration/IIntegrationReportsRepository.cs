using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Integration
{
    public interface IIntegrationReportsRepository
    {
        void CreateJobAdIntegrationEvent(JobAdIntegrationEvent integration);

        IDictionary<Guid, JobAdIntegrationReport> GetJobAdIntegrationReports(DateTimeRange timeRange);
    }
}

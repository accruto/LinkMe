using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Integration.Queries
{
    public interface IJobAdIntegrationReportsQuery
    {
        IDictionary<Guid, JobAdIntegrationReport> GetJobAdIntegrationReports(DateTimeRange timeRange);
    }
}

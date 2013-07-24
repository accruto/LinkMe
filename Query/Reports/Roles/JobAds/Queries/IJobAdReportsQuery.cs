using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.JobAds.Queries
{
    public interface IJobAdReportsQuery
    {
        JobAdReport GetJobAdReport(IEnumerable<Guid> posterIds, DateRange dateRange);
        JobAdTotalsReport GetJobAdTotalsReport(IEnumerable<Guid> jobAdIds);

        int GetOpenJobAds();
        int GetInternalApplications(DateTimeRange timeRange);
        int GetExternalApplications(DateTimeRange timeRange);
        int GetExternalApplications(Guid integratorId, DateTimeRange timeRange);

        int GetCreatedJobAds(DateTimeRange dateRange);
    }
}
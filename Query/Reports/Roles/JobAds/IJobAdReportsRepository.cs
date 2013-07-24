using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.JobAds
{
    public interface IJobAdReportsRepository
    {
        JobAdReport GetJobAdReport(IEnumerable<Guid> posterIds, DateRange dateRange);
        JobAdTotalsReport GetJobAdTotalsReport(IEnumerable<Guid> jobAdIds);

        int GetCreatedJobAds(DateTimeRange dateRange);
        int GetOpenJobAds();

        int GetInternalApplications(DateTimeRange timeRange);
        int GetExternalApplications(DateTimeRange timeRange);
        int GetExternalApplications(Guid integratorUserId, DateTimeRange timeRange);
    }
}
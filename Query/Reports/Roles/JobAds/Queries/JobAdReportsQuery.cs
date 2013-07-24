using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.JobAds.Queries
{
    public class JobAdReportsQuery
        : IJobAdReportsQuery
    {
        private readonly IJobAdReportsRepository _repository;

        public JobAdReportsQuery(IJobAdReportsRepository repository)
        {
            _repository = repository;
        }

        JobAdReport IJobAdReportsQuery.GetJobAdReport(IEnumerable<Guid> posterIds, DateRange dateRange)
        {
            return _repository.GetJobAdReport(posterIds, dateRange);
        }

        JobAdTotalsReport IJobAdReportsQuery.GetJobAdTotalsReport(IEnumerable<Guid> jobAdIds)
        {
            return _repository.GetJobAdTotalsReport(jobAdIds);
        }

        int IJobAdReportsQuery.GetCreatedJobAds(DateTimeRange dateRange)
        {
            return _repository.GetCreatedJobAds(dateRange);
        }

        int IJobAdReportsQuery.GetOpenJobAds()
        {
            return _repository.GetOpenJobAds();
        }

        int IJobAdReportsQuery.GetInternalApplications(DateTimeRange timeRange)
        {
            return _repository.GetInternalApplications(timeRange);
        }

        int IJobAdReportsQuery.GetExternalApplications(DateTimeRange timeRange)
        {
            return _repository.GetExternalApplications(timeRange);
        }

        int IJobAdReportsQuery.GetExternalApplications(Guid integratorId, DateTimeRange timeRange)
        {
            return _repository.GetExternalApplications(integratorId, timeRange);
        }
    }
}
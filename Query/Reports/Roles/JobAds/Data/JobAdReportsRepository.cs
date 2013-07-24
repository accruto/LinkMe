using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Data;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Roles.JobAds.Data
{
    public class JobAdReportsRepository
        : ReportsRepository<JobAdsDataContext>, IJobAdReportsRepository
    {
        private static readonly Func<JobAdsDataContext, DateTimeRange, int> GetCreatedJobAds
            = CompiledQuery.Compile((JobAdsDataContext dc, DateTimeRange dateRange)
                => (from j in dc.JobAdEntities
                    where j.createdTime >= dateRange.Start.Value && j.createdTime < dateRange.End.Value
                    select j).Count());

        private static readonly Func<JobAdsDataContext, int> GetOpenJobAds
            = CompiledQuery.Compile((JobAdsDataContext dc)
                => (from j in dc.JobAdEntities
                    where j.status == (byte)JobAdStatus.Open
                    select j).Count());

        private static readonly Func<JobAdsDataContext, string, DateRange, IQueryable<Guid>> GetOpenedJobAds
            = CompiledQuery.Compile((JobAdsDataContext dc, string posterIds, DateRange dateRange)
                => (from j in dc.JobAdEntities
                    join p in dc.SplitGuids(SplitList<Guid>.Delimiter, posterIds) on j.jobPosterId equals p.value
                    join s in dc.JobAdStatusEntities on j.id equals s.jobAdId
                    where s.time >= dateRange.Start.Value && s.time < dateRange.End.Value.AddDays(1)
                    && s.newStatus == (byte)JobAdStatus.Open
                    && s.previousStatus == (byte)JobAdStatus.Draft
                    select j.id).Distinct());

        private static readonly Func<JobAdsDataContext, string, DateRange, IQueryable<Guid>> GetClosedJobAds
            = CompiledQuery.Compile((JobAdsDataContext dc, string posterIds, DateRange dateRange)
                => (from j in dc.JobAdEntities
                    join p in dc.SplitGuids(SplitList<Guid>.Delimiter, posterIds) on j.jobPosterId equals p.value
                    join s in dc.JobAdStatusEntities on j.id equals s.jobAdId
                    where s.time >= dateRange.Start.Value && s.time < dateRange.End.Value.AddDays(1)
                    && s.newStatus == (byte)JobAdStatus.Closed
                    && s.previousStatus != (byte)JobAdStatus.Closed
                    select j.id).Distinct());

        private static readonly Func<JobAdsDataContext, string, DateRange, int> GetViewedJobAdsByPosters
            = CompiledQuery.Compile((JobAdsDataContext dc, string posterIds, DateRange dateRange)
                => (from j in dc.JobAdEntities
                    join p in dc.SplitGuids(SplitList<Guid>.Delimiter, posterIds) on j.jobPosterId equals p.value
                    join v in dc.JobAdViewingEntities on j.id equals v.jobAdId
                    where v.time >= dateRange.Start.Value && v.time < dateRange.End.Value.AddDays(1)
                    select v).Count());

        private static readonly Func<JobAdsDataContext, string, int> GetViewedJobAds
            = CompiledQuery.Compile((JobAdsDataContext dc, string jobAdIds)
                => (from j in dc.JobAdEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, jobAdIds) on j.id equals i.value
                    join v in dc.JobAdViewingEntities on j.id equals v.jobAdId
                    select v).Count());

        private static readonly Func<JobAdsDataContext, string, DateRange, int> GetApplicationsByPosters
            = CompiledQuery.Compile((JobAdsDataContext dc, string posterIds, DateRange dateRange)
                => (from j in dc.JobAdEntities
                    join p in dc.SplitGuids(SplitList<Guid>.Delimiter, posterIds) on j.jobPosterId equals p.value
                    join a in dc.JobApplicationEntities on j.id equals a.jobAdId
                    where a.createdTime >= dateRange.Start.Value && a.createdTime < dateRange.End.Value.AddDays(1)
                    select a).Count());

        private static readonly Func<JobAdsDataContext, string, int> GetApplications
            = CompiledQuery.Compile((JobAdsDataContext dc, string jobAdIds)
                => (from a in dc.JobApplicationEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, jobAdIds) on a.jobAdId equals i.value
                    select a).Count());

        public JobAdReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        int IJobAdReportsRepository.GetCreatedJobAds(DateTimeRange dateRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetCreatedJobAds(dc, dateRange);
            }
        }

        int IJobAdReportsRepository.GetOpenJobAds()
        {
            using (var dc = CreateDataContext(true))
            {
                return GetOpenJobAds(dc);
            }
        }

        int IJobAdReportsRepository.GetInternalApplications(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from a in dc.JobApplicationEntities
                        where a.createdTime >= timeRange.Start && a.createdTime < timeRange.End
                        select a).Count();
            }
        }

        int IJobAdReportsRepository.GetExternalApplications(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                // Count distinct applicants-jobs.

                return (from a in dc.ExternalApplicationEntities
                        where a.createdTime >= timeRange.Start && a.createdTime < timeRange.End
                        select new {a.applicantId, a.positionId}).Distinct().Count();
            }
        }

        int IJobAdReportsRepository.GetExternalApplications(Guid integratorUserId, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                // Count distinct applicants-jobs.

                return (from a in dc.ExternalApplicationEntities
                        join j in dc.JobAdEntities on a.positionId equals j.id
                        where j.integratorUserId == integratorUserId
                              && a.createdTime >= timeRange.Start && a.createdTime < timeRange.End
                        select new {a.applicantId, a.positionId}).Distinct().Count();
            }
        }

        JobAdReport IJobAdReportsRepository.GetJobAdReport(IEnumerable<Guid> posterIds, DateRange dateRange)
        {
            using (var dc = CreateDataContext(true))
            {
                var posterIdList = new SplitList<Guid>(posterIds).ToString();
                return new JobAdReport
                           {
                               OpenedJobAds = GetOpenedJobAds(dc, posterIdList, dateRange).ToList(),
                               ClosedJobAds = GetClosedJobAds(dc, posterIdList, dateRange).ToList(),
                               Totals = new JobAdTotalsReport
                                            {
                                                Views = GetViewedJobAdsByPosters(dc, posterIdList, dateRange),
                                                Applications = GetApplicationsByPosters(dc, posterIdList, dateRange),
                                            },
                           };
            }
        }

        JobAdTotalsReport IJobAdReportsRepository.GetJobAdTotalsReport(IEnumerable<Guid> jobAdIds)
        {
            using (var dc = CreateDataContext(true))
            {
                var jobAdIdList = new SplitList<Guid>(jobAdIds).ToString();
                return new JobAdTotalsReport
                           {
                               Views = GetViewedJobAds(dc, jobAdIdList),
                               Applications = GetApplications(dc, jobAdIdList),
                           };
            }
        }
        /*
        int IJobAdReportsRepository.GetJobAdSearches(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext())
            {
                return (from s in dc.JobSearchEntities
                        where s.startTime >= dateRange.Start && s.startTime < dateRange.End
                              && (from c in dc.JobSearchCriteriaEntities
                                  where c.setId == s.criteriaSetId
                                        && c.name != "SortOrder"
                                  select c).Any()
                        select s).Count();
            }
        }

        int IJobAdReportsRepository.GetJobAdSearchAlerts()
        {
            using (var dc = CreateDataContext())
            {
                return (from a in dc.SavedJobSearchAlertEntities
                        join s in dc.SavedJobSearchEntities on a.id equals s.alertId
                        join u in GetEnabledUsers(dc) on s.ownerId equals u.id
                        select a).Count();
            }
        }

        private static IQueryable<RegisteredUserEntity> GetEnabledUsers(JobAdsDataContext dc)
        {
            return from u in dc.RegisteredUserEntities
                   where (u.flags & (int)UserFlags.Disabled) == 0
                   select u;
        }
        */

        protected override JobAdsDataContext CreateDataContext(IDbConnection connection)
        {
            return new JobAdsDataContext(connection);
        }
    }
}
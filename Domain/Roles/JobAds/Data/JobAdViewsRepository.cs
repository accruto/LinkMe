using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.JobAds.Data
{
    public class JobAdViewsRepository
        : Repository, IJobAdViewsRepository
    {
        private static readonly Func<JobAdsDataContext, Guid, int> GetViewedCount
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid jobAdId)
                => (from v in dc.JobAdViewingEntities
                    where v.jobAdId == jobAdId
                    select v).Count());

        private static readonly Func<JobAdsDataContext, Guid, int> GetDistinctViewedCount
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid jobAdId)
                => (from v in dc.JobAdViewingEntities
                    where v.jobAdId == jobAdId
                    group v by v.viewerId into g
                    select g).Count());

        private static readonly Func<JobAdsDataContext, Guid, Guid, bool> HasViewedJobAd
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid viewerId, Guid jobAdId)
                => (from v in dc.JobAdViewingEntities
                    where v.viewerId == viewerId
                    && v.jobAdId == jobAdId
                    select v).Any());

        private static readonly Func<JobAdsDataContext, Guid, string, IQueryable<Guid>> GetViewedJobAdIdsFromJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid viewerId, string ids)
                => (from v in dc.JobAdViewingEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on v.jobAdId equals i.value
                    where v.viewerId == viewerId
                    select v.jobAdId).Distinct());

        private static readonly Func<JobAdsDataContext, Guid, IQueryable<Guid>> GetViewedJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid viewerId)
                => (from v in dc.JobAdViewingEntities
                    where v.viewerId == viewerId
                    select v.jobAdId).Distinct());

        public JobAdViewsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IJobAdViewsRepository.CreateJobAdViewing(JobAdViewing viewing)
        {
            using (var dc = CreateContext())
            {
                dc.JobAdViewingEntities.InsertOnSubmit(viewing.Map());
                dc.SubmitChanges();
            }
        }

        int IJobAdViewsRepository.GetViewedCount(Guid jobAdId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetViewedCount(dc, jobAdId);
            }
        }

        int IJobAdViewsRepository.GetDistinctViewedCount(Guid jobAdId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDistinctViewedCount(dc, jobAdId);
            }
        }

        bool IJobAdViewsRepository.HasViewedJobAd(Guid viewerId, Guid jobAdId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return HasViewedJobAd(dc, viewerId, jobAdId);
            }
        }

        IList<Guid> IJobAdViewsRepository.GetViewedJobAdIds(Guid viewerId, IEnumerable<Guid> jobAdIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetViewedJobAdIdsFromJobAdIds(dc, viewerId, new SplitList<Guid>(jobAdIds).ToString()).ToList();
            }
        }

        IList<Guid> IJobAdViewsRepository.GetViewedJobAdIds(Guid viewerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetViewedJobAdIds(dc, viewerId).ToList();
            }
        }

        private JobAdsDataContext CreateContext()
        {
            return CreateContext(c => new JobAdsDataContext(c));
        }
    }
}

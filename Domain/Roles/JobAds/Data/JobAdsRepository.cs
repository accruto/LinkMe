using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts.Data;
using LinkMe.Domain.Data;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Roles.JobAds.Data
{
    public class JobAdsRepository
        : Repository, IJobAdsRepository
    {
        private readonly Tuple<ILocationQuery, IIndustriesQuery> _commands;
        private static readonly DataLoadOptions JobAdEntryLoadOptions;
        private static readonly DataLoadOptions JobAdLoadOptions;

        private struct JobAdCountsCriteria
        {
            public DateTime StartTime1;
            public DateTime EndTime1;
            public DateTime StartTime2;
            public DateTime EndTime2;
        }

        #region Compiled queries

        private static readonly Func<JobAdsDataContext, Guid, JobAdEntity> GetJobAdEntityQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from j in dc.JobAdEntities
                    where j.id == id
                    select j).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, JobAdEntity> GetJobAdEntryEntityQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from j in dc.JobAdEntities
                    where j.id == id
                    select j).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, JobAdEntry> GetJobAdEntryQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from j in dc.JobAdEntities
                    where j.id == id
                    select j.Map()).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, Tuple<ILocationQuery, IIndustriesQuery>, JobAd> GetJobAdQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id, Tuple<ILocationQuery, IIndustriesQuery> commands)
                => (from j in dc.JobAdEntities
                    where j.id == id
                    select j.Map(commands.Item1, commands.Item2)).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, Guid, string, IQueryable<Guid>> GetPosterJobAdIdsByExternalReferenceId
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid integratorUserId, Guid posterId, string externalReferenceId)
                => from j in dc.JobAdEntities
                   where j.integratorUserId == integratorUserId
                   && j.jobPosterId == posterId
                   && j.externalReferenceId == externalReferenceId
                   select j.id);

        private static readonly Func<JobAdsDataContext, string, IQueryable<Guid>> GetJobAdIdsByExternalReferenceId
            = CompiledQuery.Compile((JobAdsDataContext dc, string externalReferenceId)
                => from j in dc.JobAdEntities
                   where j.externalReferenceId == externalReferenceId
                   select j.id);

        private static readonly Func<JobAdsDataContext, string, string, IQueryable<Guid>> GetOpenJobAdIdsByExternalReferenceIdTitle
            = CompiledQuery.Compile((JobAdsDataContext dc, string externalReferenceId, string title)
                => from j in dc.JobAdEntities
                   where j.externalReferenceId == externalReferenceId
                   && j.title == title
                   && j.status == (byte)JobAdStatus.Open
                   select j.id);

        private static readonly Func<JobAdsDataContext, string, string, IQueryable<Guid>> GetOpenJobAdIdsByIntegratorReferenceIdTitle
            = CompiledQuery.Compile((JobAdsDataContext dc, string integratorReferenceId, string title)
                => from j in dc.JobAdEntities
                   where j.integratorReferenceId == integratorReferenceId
                   && j.title == title
                   && j.status == (byte)JobAdStatus.Open
                   select j.id);

        private static readonly Func<JobAdsDataContext, string, IQueryable<JobAdEntry>> GetJobAdEntriesQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, string ids)
                => from j in dc.JobAdEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on j.id equals i.value
                   select j.Map());

        private static readonly Func<JobAdsDataContext, string, Tuple<ILocationQuery, IIndustriesQuery>, IQueryable<JobAd>> GetJobAdsQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, string ids, Tuple<ILocationQuery, IIndustriesQuery> commands)
                => from j in dc.JobAdEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on j.id equals i.value
                   select j.Map(commands.Item1, commands.Item2));

        private static readonly Func<JobAdsDataContext, Guid, IQueryable<Guid>> GetJobAdIdsByPoster
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid posterId)
                => from j in dc.JobAdEntities
                   where j.jobPosterId == posterId
                   orderby j.expiryTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, Guid, JobAdStatus, IQueryable<Guid>> GetJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid posterId, JobAdStatus status)
                => from j in dc.JobAdEntities
                   where j.jobPosterId == posterId
                   && j.status == (byte) status
                   orderby j.expiryTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, Guid, Guid, IQueryable<Guid>> GetJobAdIdsByIntegratorAndPoster
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid integratorUserId, Guid posterId)
                => from j in dc.JobAdEntities
                   where j.integratorUserId == integratorUserId
                   && j.jobPosterId == posterId
                   orderby j.expiryTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, Guid, string, IQueryable<Guid>> GetJobAdIdsByIntegrator
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid integratorUserId, string integratorReferenceId)
                => from j in dc.JobAdEntities
                   where j.integratorUserId == integratorUserId
                   && j.integratorReferenceId == integratorReferenceId
                   orderby j.expiryTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, DateTime, IQueryable<Guid>> GetExpiredJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, DateTime expiryTime)
                => from j in dc.JobAdEntities
                   where j.status == (byte) JobAdStatus.Open
                   && j.expiryTime <= expiryTime
                   orderby j.expiryTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, DateTime, IQueryable<Guid>> GetJobAdIdsRequiringRefresh
            = CompiledQuery.Compile((JobAdsDataContext dc, DateTime lastRefreshTime)
                => from r in dc.JobAdRefreshEntities
                   where r.lastRefreshTime <= lastRefreshTime
                   select r.id);

        private static readonly Func<JobAdsDataContext, IQueryable<Guid>> GetAllJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc)
                => from j in dc.JobAdEntities
                   select j.id);

        private static readonly Func<JobAdsDataContext, IQueryable<Guid>> GetOpenJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open
                   orderby j.expiryTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, string, IQueryable<Guid>> GetIndustryOpenJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, string industryIds)
                => (from j in dc.JobAdEntities
                   join i in dc.JobAdIndustryEntities on j.id equals i.jobAdId
                   join d in dc.SplitGuids(SplitList<Guid>.Delimiter, industryIds) on i.industryId equals d.value
                   where j.status == (byte)JobAdStatus.Open
                   orderby j.expiryTime
                   select j.id).Distinct());

        private static readonly Func<JobAdsDataContext, string, DateTime, IQueryable<Guid>> GetModifiedIndustryOpenJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, string industryIds, DateTime modifiedSince)
                => (from j in dc.JobAdEntities
                   join i in dc.JobAdIndustryEntities on j.id equals i.jobAdId
                   join d in dc.SplitGuids(SplitList<Guid>.Delimiter, industryIds) on i.industryId equals d.value
                   where j.status == (byte)JobAdStatus.Open
                   && j.lastUpdatedTime >= modifiedSince
                   orderby j.expiryTime
                   select j.id).Distinct());

        private static readonly Func<JobAdsDataContext, DateTime, IQueryable<Guid>> GetModifiedOpenJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, DateTime modifiedSince)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open
                   && j.lastUpdatedTime >= modifiedSince
                   orderby j.expiryTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, IQueryable<Guid>> GetOpenJobAdIdsWithoutSalaries
            = CompiledQuery.Compile((JobAdsDataContext dc)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open
                   && j.maxParsedSalary == null
                   && j.minParsedSalary == null
                   select j.id);

        private static readonly Func<JobAdsDataContext, IQueryable<Guid>> GetOpenJobAdIdsWithoutSeniorityIndex
            = CompiledQuery.Compile((JobAdsDataContext dc)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open
                   && (j.seniorityIndex == null
                   || j.seniorityIndex == 0)
                   select j.id);

        private static readonly Func<JobAdsDataContext, IQueryable<Guid>> GetJobAdIdsWithoutSalaries
            = CompiledQuery.Compile((JobAdsDataContext dc)
                => from j in dc.JobAdEntities
                   where j.maxParsedSalary == null
                   && j.minParsedSalary == null
                   select j.id);

        private static readonly Func<JobAdsDataContext, Guid, IQueryable<Guid>> GetOpenJobAdsByPoster
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid posterId)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open
                   && j.jobPosterId == posterId
                   select j.id);

        private static readonly Func<JobAdsDataContext, DateTime, DateTime, IQueryable<Guid>> GetOpenJobAdIdsByCreatedTime
            = CompiledQuery.Compile((JobAdsDataContext dc, DateTime startCreatedTime, DateTime endCreatedTime)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open
                   && j.createdTime >= startCreatedTime && j.createdTime < endCreatedTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, DateTime, IQueryable<Guid>> GetOpenJobAdIdsByStartCreatedTime
            = CompiledQuery.Compile((JobAdsDataContext dc, DateTime startCreatedTime)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open
                   && j.createdTime >= startCreatedTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, DateTime, IQueryable<Guid>> GetOpenJobAdIdsByEndCreatedTime
            = CompiledQuery.Compile((JobAdsDataContext dc, DateTime endCreatedTime)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open
                   && j.createdTime < endCreatedTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, Guid, Guid, IQueryable<Guid>> GetOpenJobAdsByIntegratorAndPoster
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid integratorUserId, Guid posterId)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open
                   && j.integratorUserId == integratorUserId
                   && j.jobPosterId == posterId
                   select j.id);

        private static readonly Func<JobAdsDataContext, Guid, string, IQueryable<Guid>> GetOpenJobAdsByIntegratorAndReferenceId
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid integratorUserId, string integratorReferenceId)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open
                   && j.integratorUserId == integratorUserId
                   && j.integratorReferenceId == integratorReferenceId
                   orderby j.expiryTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, Guid?[], IQueryable<Guid>> GetOpenJobAdIdsExcluding
            = ((dc, integratorUserIds)
                => from j in dc.JobAdEntities
                   where j.status == (byte)JobAdStatus.Open 
                   && !integratorUserIds.Contains(j.integratorUserId)
                   orderby j.expiryTime
                   select j.id);

        private static readonly Func<JobAdsDataContext, Range, IQueryable<Guid>> GetRecentOpenJobAdIdsSkipTakeQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Range range)
                => (from j in dc.JobAdEntities
                    where j.status == (byte) JobAdStatus.Open
                    orderby j.createdTime descending
                    select j.id).Skip(range.Skip).Take(range.Take.Value));

        private static readonly Func<JobAdsDataContext, Range, IQueryable<Guid>> GetRecentOpenJobAdIdsTakeQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Range range)
                => (from j in dc.JobAdEntities
                    where j.status == (byte)JobAdStatus.Open
                    orderby j.createdTime descending
                    select j.id).Take(range.Take.Value));

        private static readonly Func<JobAdsDataContext, Range, IQueryable<Guid>> GetRecentOpenJobAdIdsSkipQuery
            = CompiledQuery.Compile((JobAdsDataContext dc, Range range)
                => (from j in dc.JobAdEntities
                    where j.status == (byte)JobAdStatus.Open
                    orderby j.createdTime descending
                    select j.id).Skip(range.Skip));

        private static readonly Func<JobAdsDataContext, IQueryable<Guid>> GetRecentOpenJobAdIdsQuery
            = CompiledQuery.Compile((JobAdsDataContext dc)
                => (from j in dc.JobAdEntities
                    where j.status == (byte)JobAdStatus.Open
                    orderby j.createdTime descending
                    select j.id));

        private static readonly Func<JobAdsDataContext, Guid, JobAdStatusEntity> GetJobAdStatusEntity
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from s in dc.JobAdStatusEntities
                    where s.id == id
                    select s).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, IQueryable<JobAdStatusChange>> GetStatusChanges
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid jobAdId)
                => from s in dc.JobAdStatusEntities
                   where s.jobAdId == jobAdId
                   orderby s.time
                   select s.Map());

        private static readonly Func<JobAdsDataContext, Guid, JobAdRefreshEntity> GetJobAdRefreshEntity
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from r in dc.JobAdRefreshEntities
                    where r.id == id
                    select r).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, DateTime?> GetLastRefreshTime
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid jobAdId)
                => (from j in dc.JobAdRefreshEntities
                    where j.id == jobAdId
                    select j.lastRefreshTime).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, Guid?> GetLastUsedLogoId
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid posterId)
                => (from j in dc.JobAdEntities
                    where j.jobPosterId == posterId
                    && j.brandingLogoImageId != null
                    orderby j.createdTime descending
                    select j.brandingLogoImageId).Take(1).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, JobPosterEntity> GetJobPosterEntity
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from p in dc.JobPosterEntities
                    where p.id == id
                    select p).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, JobPoster> GetJobPoster
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from p in dc.JobPosterEntities
                    where p.id == id
                    select p.Map()).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, JobAdExportEntity> GetJobAdExportEntity
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid id)
                => (from je in dc.JobAdExportEntities
                    where je.jobAdId == id
                    select je).SingleOrDefault());

        private static readonly Func<JobAdsDataContext, Guid, JobAdCountsCriteria, Tuple<int, int>> GetOpenJobAdCounts
            = CompiledQuery.Compile((JobAdsDataContext dc, Guid posterId, JobAdCountsCriteria criteria)
                => (from x in
                        (from j in dc.JobAdEntities
                         where j.jobPosterId == posterId
                         && j.status == (decimal) JobAdStatus.Open
                         select j)
                    group x by x.jobPosterId into y
                    select new Tuple<int, int>(
                        (from u in y where u.createdTime >= criteria.StartTime1 && u.createdTime < criteria.EndTime1 select u).Count(),
                        (from u in y where u.createdTime >= criteria.StartTime2 && u.createdTime < criteria.EndTime2 select u).Count())).SingleOrDefault());

        #endregion

        static JobAdsRepository()
        {
            JobAdLoadOptions = new DataLoadOptions();
            JobAdLoadOptions.LoadWith<JobAdEntity>(j => j.JobAdLocationEntities);
            JobAdLoadOptions.LoadWith<JobAdLocationEntity>(l => l.LocationReferenceEntity);
            JobAdLoadOptions.LoadWith<JobAdEntity>(j => j.JobAdIndustryEntities);
            JobAdLoadOptions.LoadWith<JobAdEntity>(j => j.ContactDetailsEntity);

            JobAdEntryLoadOptions = new DataLoadOptions();
            JobAdEntryLoadOptions.LoadWith<JobAdEntity>(j => j.ContactDetailsEntity);
        }

        public JobAdsRepository(IDataContextFactory dataContextFactory, ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
            : base(dataContextFactory)
        {
            _commands = new Tuple<ILocationQuery, IIndustriesQuery>(locationQuery, industriesQuery);
        }

        void IJobAdsRepository.CreateJobAd(JobAd jobAd)
        {
            using (var dc = CreateContext())
            {
                dc.JobAdEntities.InsertOnSubmit(jobAd.Map());
                dc.SubmitChanges();
            }
        }

        void IJobAdsRepository.UpdateJobAd(JobAd jobAd)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobAdEntity(dc, jobAd.Id);
                if (entity != null)
                {
                    // Delete the child objects if needed.

                    dc.CheckDeleteContactDetails(jobAd, entity);
                    CheckDeleteLocation(dc, jobAd, entity);
                    DeleteIndustries(dc, entity);

                    jobAd.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        void IJobAdsRepository.UpdateJobAd(JobAdEntry jobAd)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobAdEntryEntity(dc, jobAd.Id);
                if (entity != null)
                {
                    // Delete the child objects if needed.

                    dc.CheckDeleteContactDetails(jobAd, entity);

                    jobAd.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        void IJobAdsRepository.CreateApplicationRequirements(Guid jobAdId, string requirementsXml)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobAdEntryEntity(dc, jobAdId);
                if (entity != null)
                {
                    entity.jobg8ApplyForm = requirementsXml;
                    dc.SubmitChanges();
                }
            }
        }

        T IJobAdsRepository.GetJobAd<T>(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return typeof(T) == typeof(JobAd)
                    ? GetJobAd(dc, id) as T
                    : GetJobAdEntry(dc, id) as T;
            }
        }

        IList<T> IJobAdsRepository.GetJobAds<T>(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return typeof(T) == typeof(JobAd)
                    ? GetJobAds(dc, ids).Cast<T>().ToList()
                    : GetJobAdEntries(dc, ids).Cast<T>().ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(Guid integratorUserId, Guid posterId, string externalReferenceId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetPosterJobAdIdsByExternalReferenceId(dc, integratorUserId, posterId, externalReferenceId).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(string externalReferenceId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdIdsByExternalReferenceId(dc, externalReferenceId).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(string referenceId, string title)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOpenJobAdIdsByExternalReferenceIdTitle(dc, referenceId, title).Concat(
                    GetOpenJobAdIdsByIntegratorReferenceIdTitle(dc, referenceId, title)).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(Guid posterId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdIdsByPoster(dc, posterId).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(Guid posterId, JobAdStatus status)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdIds(dc, posterId, status).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(Guid integratorUserId, Guid posterId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdIdsByIntegratorAndPoster(dc, integratorUserId, posterId).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds(Guid integratorUserId, string integratorReferenceId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdIdsByIntegrator(dc, integratorUserId, integratorReferenceId).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetJobAdIds()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAllJobAdIds(dc).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOpenJobAdIds(dc).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(Guid posterId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOpenJobAdsByPoster(dc, posterId).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(DateTimeRange createdTimeRange)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (createdTimeRange.Start == null)
                {
                    return createdTimeRange.End == null
                        ? GetOpenJobAdIds(dc).ToList()
                        : GetOpenJobAdIdsByEndCreatedTime(dc, createdTimeRange.End.Value).ToList();
                }
                
                return createdTimeRange.End == null
                    ? GetOpenJobAdIdsByStartCreatedTime(dc, createdTimeRange.Start.Value).ToList()
                    : GetOpenJobAdIdsByCreatedTime(dc, createdTimeRange.Start.Value, createdTimeRange.End.Value).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(Guid integratorUserId, Guid posterId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOpenJobAdsByIntegratorAndPoster(dc, integratorUserId, posterId).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(Guid integratorUserId, string integratorReferenceId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOpenJobAdsByIntegratorAndReferenceId(dc, integratorUserId, integratorReferenceId).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(IEnumerable<Guid> excludedIntegratorUserIds)
        {
            if (excludedIntegratorUserIds == null)
                throw new ArgumentNullException("excludedIntegratorUserIds");

            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOpenJobAdIdsExcluding(dc, excludedIntegratorUserIds.Cast<Guid?>().ToArray()).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetOpenJobAdIds(IEnumerable<Guid> industryIds, DateTime? modifiedSince)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                if (industryIds == null || !industryIds.Any())
                {
                    return modifiedSince == null
                        ? GetOpenJobAdIds(dc).ToList()
                        : GetModifiedOpenJobAdIds(dc, modifiedSince.Value).ToList();
                }
                
                return modifiedSince == null
                    ? GetIndustryOpenJobAdIds(dc, new SplitList<Guid>(industryIds).ToString()).ToList()
                    : GetModifiedIndustryOpenJobAdIds(dc, new SplitList<Guid>(industryIds).ToString(), modifiedSince.Value).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetJobAdIdsWithoutSalaries(bool onlyOpenJobAds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return onlyOpenJobAds 
                    ? GetOpenJobAdIdsWithoutSalaries(dc).ToList()
                    : GetJobAdIdsWithoutSalaries(dc).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetJobAdIdsWithoutSeniorityIndex()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOpenJobAdIdsWithoutSeniorityIndex(dc).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetRecentOpenJobAdIds(Range range)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetRecentOpenJobAdIds(dc, range).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetExpiredJobAdIds()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetExpiredJobAdIds(dc, DateTime.Today).ToList();
            }
        }

        IList<Guid> IJobAdsRepository.GetJobAdIdsRequiringRefresh(DateTime lastRefreshTime)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobAdIdsRequiringRefresh(dc, lastRefreshTime).ToList();
            }
        }

        Tuple<int, int> IJobAdsRepository.GetOpenJobAdCounts(Guid posterId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var today = DateTime.Today;
                var criteria = new JobAdCountsCriteria
                {
                    StartTime1 = today,
                    EndTime1 = today.AddDays(1),
                    StartTime2 = today.AddDays(-1 * (int)today.DayOfWeek),
                    EndTime2 = today.AddDays(1)
                };
                return GetOpenJobAdCounts(dc, posterId, criteria)
                    ?? new Tuple<int, int>(0, 0);
            }
        }

        void IJobAdsRepository.TransferJobAd(Guid toPosterId, Guid jobAdId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobAdEntity(dc, jobAdId);
                entity.jobPosterId = toPosterId;
                dc.SubmitChanges();
            }
        }

        DateTime? IJobAdsRepository.GetLastRefreshTime(Guid jobAdId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetLastRefreshTime(dc, jobAdId);
            }
        }

        void IJobAdsRepository.ChangeStatus(Guid jobAdId, JobAdStatus newStatus, DateTime? newExpiryTime, DateTime time)
        {
            using (var dc = CreateContext())
            {
                // Update the status on the job ad itself.

                var entity = GetJobAdEntity(dc, jobAdId);
                var previousStatus = entity.status;
                entity.status = (byte)newStatus;
                entity.lastUpdatedTime = time;

                if (newExpiryTime != null)
                    entity.expiryTime = newExpiryTime.Value;

                // Record the change.

                var change = new JobAdStatusChange
                {
                    Id = Guid.NewGuid(),
                    Time = time,
                    PreviousStatus = (JobAdStatus) previousStatus,
                    NewStatus = newStatus
                };
                dc.JobAdStatusEntities.InsertOnSubmit(change.Map(jobAdId));

                dc.SubmitChanges();
            }
        }

        IList<JobAdStatusChange> IJobAdsRepository.GetStatusChanges(Guid jobAdId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetStatusChanges(dc, jobAdId).ToList();
            }
        }

        void IJobAdsRepository.UpdateStatusChange(JobAdStatusChange change)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobAdStatusEntity(dc, change.Id);
                if (entity != null)
                {
                    change.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        void IJobAdsRepository.CreateRefresh(Guid jobAdId, DateTime time)
        {
            UpdateRefresh(jobAdId, time);
        }

        void IJobAdsRepository.UpdateRefresh(Guid jobAdId, DateTime time)
        {
            UpdateRefresh(jobAdId, time);
        }

        void IJobAdsRepository.DeleteRefresh(Guid jobAdId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobAdRefreshEntity(dc, jobAdId);
                if (entity != null)
                {
                    dc.JobAdRefreshEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        Guid? IJobAdsRepository.GetLastUsedLogoId(Guid posterId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetLastUsedLogoId(dc, posterId);
            }
        }

        void IJobAdsRepository.CreateJobPoster(JobPoster poster)
        {
            using (var dc = CreateContext())
            {
                dc.JobPosterEntities.InsertOnSubmit(poster.Map());
                dc.SubmitChanges();
            }
        }

        void IJobAdsRepository.UpdateJobPoster(JobPoster poster)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobPosterEntity(dc, poster.Id);
                if (entity != null)
                {
                    poster.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        JobPoster IJobAdsRepository.GetJobPoster(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetJobPoster(dc, id);
            }
        }

        long? IJobAdsRepository.GetJobSearchId(Guid jobAdId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var entity = GetJobAdExportEntity(dc, jobAdId);

                if (entity == null)
                    return null;

                return entity.jobSearchVacancyId;
            }
        }

        void IJobAdsRepository.CreateJobSearchId(Guid jobAdId, long vacancyId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobAdExportEntity(dc, jobAdId);
                if (entity == null)
                {
                    entity = new JobAdExportEntity { jobAdId = jobAdId, jobSearchVacancyId = vacancyId };
                    dc.JobAdExportEntities.InsertOnSubmit(entity);
                }
                else
                {
                    entity.jobSearchVacancyId = vacancyId;
                }

                dc.SubmitChanges();
            }
        }

        void IJobAdsRepository.DeleteJobSearchId(Guid jobAdId)
        {
            using (var dc = CreateContext())
            {
                var export = new JobAdExportEntity { jobAdId = jobAdId, jobSearchVacancyId = long.MinValue };
                dc.JobAdExportEntities.Attach(export);
                export.jobSearchVacancyId = null;
                dc.SubmitChanges();
            }
        }

        private static JobAdEntity GetJobAdEntity(JobAdsDataContext dc, Guid id)
        {
            dc.LoadOptions = JobAdLoadOptions;
            return GetJobAdEntityQuery(dc, id);
        }

        private static JobAdEntity GetJobAdEntryEntity(JobAdsDataContext dc, Guid id)
        {
            dc.LoadOptions = JobAdEntryLoadOptions;
            return GetJobAdEntryEntityQuery(dc, id);
        }

        private static JobAdEntry GetJobAdEntry(JobAdsDataContext dc, Guid id)
        {
            dc.LoadOptions = JobAdEntryLoadOptions;
            return GetJobAdEntryQuery(dc, id);
        }

        private JobAd GetJobAd(JobAdsDataContext dc, Guid id)
        {
            dc.LoadOptions = JobAdLoadOptions;
            return GetJobAdQuery(dc, id, _commands);
        }

        private static IQueryable<JobAdEntry> GetJobAdEntries(JobAdsDataContext dc, IEnumerable<Guid> ids)
        {
            dc.LoadOptions = JobAdEntryLoadOptions;
            return GetJobAdEntriesQuery(dc, new SplitList<Guid>(ids).ToString());
        }

        private IQueryable<JobAd> GetJobAds(JobAdsDataContext dc, IEnumerable<Guid> ids)
        {
            dc.LoadOptions = JobAdLoadOptions;
            return GetJobAdsQuery(dc, new SplitList<Guid>(ids).ToString(), _commands);
        }

        private static IEnumerable<Guid> GetRecentOpenJobAdIds(JobAdsDataContext dc, Range range)
        {
            if (range.Skip == 0)
                return range.Take != null
                    ? GetRecentOpenJobAdIdsTakeQuery(dc, range)
                    : GetRecentOpenJobAdIdsQuery(dc);
            return range.Take != null
                ? GetRecentOpenJobAdIdsSkipTakeQuery(dc, range)
                : GetRecentOpenJobAdIdsSkipQuery(dc, range);
        }

        private static void CheckDeleteLocation(JobAdsDataContext dc, JobAd jobAd, IHaveDescriptionEntity entity)
        {
            var entityLocations = Locations(entity);
            if (entityLocations == 0)
                return;

            var locations = Locations(jobAd);

            // There were locations but they have now been removed.

            if (locations == 0)
                DeleteLocations(dc, entity);

            // There were locations but there is now a different number of them.
            // (shouldn't happen, only one location per job ad is allowed but just in case ...)

            if (locations > 0 && entityLocations != locations)
                DeleteLocations(dc, entity);
        }

        private static int Locations(JobAd jobAd)
        {
            return jobAd.Description.Location == null ? 0 : 1;
        }

        private static int Locations(IHaveDescriptionEntity entity)
        {
            return entity.JobAdLocationEntities == null ? 0 : entity.JobAdLocationEntities.Count;
        }

        private static void DeleteLocations(JobAdsDataContext dc, IHaveDescriptionEntity entity)
        {
            dc.LocationReferenceEntities.DeleteAllOnSubmit(from e in entity.JobAdLocationEntities select e.LocationReferenceEntity);
            dc.JobAdLocationEntities.DeleteAllOnSubmit(entity.JobAdLocationEntities);
            entity.JobAdLocationEntities.Clear();
        }

        private static void DeleteIndustries(JobAdsDataContext dc, IHaveDescriptionEntity entity)
        {
            // Cannot update in place so delete whatever is already there.

            if (entity.JobAdIndustryEntities != null && entity.JobAdIndustryEntities.Count > 0)
                dc.JobAdIndustryEntities.DeleteAllOnSubmit(entity.JobAdIndustryEntities);
        }

        private void UpdateRefresh(Guid jobAdId, DateTime time)
        {
            using (var dc = CreateContext())
            {
                var entity = GetJobAdRefreshEntity(dc, jobAdId);
                if (entity == null)
                {
                    entity = new JobAdRefreshEntity { id = jobAdId, lastRefreshTime = time };
                    dc.JobAdRefreshEntities.InsertOnSubmit(entity);
                }
                else
                {
                    entity.lastRefreshTime = time;
                }

                dc.SubmitChanges();
            }
        }

        private JobAdsDataContext CreateContext()
        {
            return CreateContext(c => new JobAdsDataContext(c));
        }
    }
}

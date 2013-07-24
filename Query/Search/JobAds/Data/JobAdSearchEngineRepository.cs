using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Query.JobAds;

namespace LinkMe.Query.Search.JobAds.Data
{
    public class JobAdSearchEngineRepository
        : Repository, IJobAdSearchEngineRepository
    {
        #region Queries

        private static readonly Func<JobAdsDataContext, IQueryable<Guid>> GetAllOpenJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc)
                => from j in dc.JobAdEntities
                   where j.status == (byte) JobAdStatus.Open
                   select j.id);

        private static readonly Func<JobAdsDataContext, IQueryable<Guid>> GetAllJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc)
                => from j in dc.JobAdEntities
                   select j.id);

        private static readonly Func<JobAdsDataContext, DateTime, IQueryable<Guid>> GetModifiedJobAdIds
            = CompiledQuery.Compile((JobAdsDataContext dc, DateTime modifiedSince)
                => from j in dc.JobAdIndexingEntities
                   where j.modifiedTime >= modifiedSince
                   select j.jobAdId);

        #endregion

        public JobAdSearchEngineRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        #region Implementation of ISearchRepository

        IList<Guid> IJobAdSearchEngineRepository.GetSearchModified(DateTime? modifiedSince)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                // For search only need open job ads.

                return modifiedSince.HasValue
                    ? GetModifiedJobAdIds(dc, modifiedSince.Value).ToList()
                    : GetAllOpenJobAdIds(dc).ToList();
            }
        }

        IList<Guid> IJobAdSearchEngineRepository.GetSortModified(DateTime? modifiedSince)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                // For search need all job ads.

                return modifiedSince.HasValue
                    ? GetModifiedJobAdIds(dc, modifiedSince.Value).ToList()
                    : GetAllJobAdIds(dc).ToList();
            }
        }

        void IJobAdSearchEngineRepository.SetModified(Guid id)
        {
            using (var dc = CreateContext())
            {
                dc.JobAdSetModified(id);
            }
        }

        #endregion

        private JobAdsDataContext CreateContext()
        {
            return CreateContext(c => new JobAdsDataContext(c));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Apps.Agents.Featured.Data
{
    public class FeaturedRepository
        : Repository, IFeaturedRepository
    {
        private static readonly Func<FeaturedDataContext, FeaturedStatistics> GetFeaturedStatistics
            = CompiledQuery.Compile((FeaturedDataContext dc)
                => (from e in dc.FeaturedStatisticsEntities
                    select e.Map()).SingleOrDefault());

        private static readonly Func<FeaturedDataContext, FeaturedStatisticsEntity> GetFeaturedStatisticsEntity
            = CompiledQuery.Compile((FeaturedDataContext dc)
                => (from e in dc.FeaturedStatisticsEntities
                    select e).SingleOrDefault());

        private static readonly Func<FeaturedDataContext, IQueryable<FeaturedEmployer>> GetFeaturedEmployers
            = CompiledQuery.Compile((FeaturedDataContext dc)
                => from e in dc.FeaturedEmployerEntities
                where e.enabled
                select e.Map());

        private static readonly Func<FeaturedDataContext, IQueryable<FeaturedItem>> GetFeaturedJobAds
            = CompiledQuery.Compile((FeaturedDataContext dc)
                => from e in dc.FeaturedJobAdEntities
                   select e.Map());

        private static readonly Func<FeaturedDataContext, IQueryable<FeaturedItem>> GetFeaturedCandidateSearches
            = CompiledQuery.Compile((FeaturedDataContext dc)
                => from e in dc.FeaturedCandidateSearchEntities
                   select e.Map());

        private static readonly Func<FeaturedDataContext, IQueryable<FeaturedJobAdEntity>> GetFeaturedJobAdEntities
            = CompiledQuery.Compile((FeaturedDataContext dc)
                => from e in dc.FeaturedJobAdEntities
                   select e);

        public FeaturedRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        FeaturedStatistics IFeaturedRepository.GetFeaturedStatistics()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFeaturedStatistics(dc);
            }
        }

        void IFeaturedRepository.UpdateFeaturedStatistics(FeaturedStatistics statistics)
        {
            using (var dc = CreateContext())
            {
                var entity = GetFeaturedStatisticsEntity(dc);
                if (entity != null)
                {
                    statistics.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        IList<FeaturedEmployer> IFeaturedRepository.GetFeaturedEmployers()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFeaturedEmployers(dc).ToList();
            }
        }

        IList<FeaturedItem> IFeaturedRepository.GetFeaturedJobAds()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFeaturedJobAds(dc).ToList();
            }
        }

        IList<FeaturedItem> IFeaturedRepository.GetFeaturedCandidateSearches()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFeaturedCandidateSearches(dc).ToList();
            }
        }

        void IFeaturedRepository.UpdateFeaturedJobAds(IEnumerable<FeaturedItem> jobAds)
        {
            using (var dc = CreateContext())
            {
                // Delete what is already there.

                var entities = GetFeaturedJobAdEntities(dc);
                dc.FeaturedJobAdEntities.DeleteAllOnSubmit(entities);
                dc.SubmitChanges();

                // Add in all new ones.

                dc.FeaturedJobAdEntities.InsertAllOnSubmit(from j in jobAds select j.Map());
                dc.SubmitChanges();
            }
        }

        private FeaturedDataContext CreateContext()
        {
            return CreateContext(c => new FeaturedDataContext(c));
        }
    }
}
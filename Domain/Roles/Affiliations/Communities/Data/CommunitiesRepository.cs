using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Affiliations.Communities.Data
{
    public class CommunitiesRepository
        : Repository, ICommunitiesRepository
    {
        private static readonly Func<CommunitiesDataContext, IQueryable<Community>> GetCommunities
            = CompiledQuery.Compile((CommunitiesDataContext dc)
                => from c in dc.CommunityEntities
                   orderby c.name
                   select c.Map());

        private static readonly Func<CommunitiesDataContext, Guid, CommunityEntity> GetCommunityEntity
            = CompiledQuery.Compile((CommunitiesDataContext dc, Guid id)
                => (from c in dc.CommunityEntities
                    where c.id == id
                    select c).SingleOrDefault());

        private static readonly Func<CommunitiesDataContext, Guid, Community> GetCommunity
            = CompiledQuery.Compile((CommunitiesDataContext dc, Guid id)
                => (from c in dc.CommunityEntities
                    where c.id == id
                    select c.Map()).SingleOrDefault());

        private static readonly Func<CommunitiesDataContext, string, Community> GetCommunityByName
            = CompiledQuery.Compile((CommunitiesDataContext dc, string name)
                => (from c in dc.CommunityEntities
                    where c.name == name
                    select c.Map()).SingleOrDefault());

        public CommunitiesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void ICommunitiesRepository.CreateCommunity(Community community)
        {
            using (var dc = CreateContext())
            {
                dc.CommunityEntities.InsertOnSubmit(community.Map());
                dc.SubmitChanges();
            }
        }

        void ICommunitiesRepository.UpdateCommunity(Community community)
        {
            using (var dc = CreateContext())
            {
                var entity = GetCommunityEntity(dc, community.Id);
                if (entity != null)
                {
                    community.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        IList<Community> ICommunitiesRepository.GetCommunities()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCommunities(dc).ToList();
            }
        }

        Community ICommunitiesRepository.GetCommunity(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCommunity(dc, id);
            }
        }

        Community ICommunitiesRepository.GetCommunity(string name)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCommunityByName(dc, name);
            }
        }

        private CommunitiesDataContext CreateContext()
        {
            return CreateContext(c => new CommunitiesDataContext(c));
        }
    }
}

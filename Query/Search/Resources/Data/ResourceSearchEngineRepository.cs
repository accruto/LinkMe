using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Query.Resources;

namespace LinkMe.Query.Search.Resources.Data
{
    public class ResourceSearchEngineRepository
        : Repository, IResourceSearchEngineRepository
    {
        private static readonly Func<ResourcesDataContext, IQueryable<Guid>> GetArticleIds
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from a in dc.ResourceArticleEntities
                   select a.id);

        private static readonly Func<ResourcesDataContext, IQueryable<Guid>> GetVideoIds
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from v in dc.ResourceVideoEntities
                   select v.id);

        private static readonly Func<ResourcesDataContext, IQueryable<Guid>> GetQnAIds
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from a in dc.ResourceAnsweredQuestionEntities
                   select a.id);

        private static readonly Func<ResourcesDataContext, IQueryable<Guid>> GetFaqIds
            = CompiledQuery.Compile((ResourcesDataContext dc)
                => from a in dc.FrequentlyAskedQuestionEntities
                   select a.id);

        private static readonly Func<ResourcesDataContext, DateTime, IQueryable<Guid>> GetModifiedArticleIds
            = CompiledQuery.Compile((ResourcesDataContext dc, DateTime modifiedSince)
                => from a in dc.ResourceArticleEntities
                   join i in dc.ResourceIndexingEntities on a.id equals i.resourceId
                   where i.modifiedTime >= modifiedSince
                   select a.id);

        private static readonly Func<ResourcesDataContext, DateTime, IQueryable<Guid>> GetModifiedVideoIds
            = CompiledQuery.Compile((ResourcesDataContext dc, DateTime modifiedSince)
                => from v in dc.ResourceVideoEntities
                   join i in dc.ResourceIndexingEntities on v.id equals i.resourceId
                   where i.modifiedTime >= modifiedSince
                   select v.id);

        private static readonly Func<ResourcesDataContext, DateTime, IQueryable<Guid>> GetModifiedQnAIds
            = CompiledQuery.Compile((ResourcesDataContext dc, DateTime modifiedSince)
                => from a in dc.ResourceAnsweredQuestionEntities
                   join i in dc.ResourceIndexingEntities on a.id equals i.resourceId
                   where i.modifiedTime >= modifiedSince
                   select a.id);

        private static readonly Func<ResourcesDataContext, DateTime, IQueryable<Guid>> GetModifiedFaqIds
            = CompiledQuery.Compile((ResourcesDataContext dc, DateTime modifiedSince)
                => from a in dc.FrequentlyAskedQuestionEntities
                   join i in dc.ResourceIndexingEntities on a.id equals i.resourceId
                   where i.modifiedTime >= modifiedSince
                   select a.id);

        public ResourceSearchEngineRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<Guid> IResourceSearchEngineRepository.GetModified(DateTime? modifiedSince)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return modifiedSince.HasValue
                    ? GetModifiedResourceIds(dc, modifiedSince.Value)
                    : GetResourceIds(dc).ToList();
            }
        }

        void IResourceSearchEngineRepository.SetModified(Guid id)
        {
            using (var dc = CreateContext())
            {
                dc.ResourceSetModified(id);
            }
        }

        private static IList<Guid> GetModifiedResourceIds(ResourcesDataContext dc, DateTime modifiedSince)
        {
            return GetModifiedArticleIds(dc, modifiedSince)
                .Concat(GetModifiedVideoIds(dc, modifiedSince))
                .Concat(GetModifiedQnAIds(dc, modifiedSince)
                .Concat(GetModifiedFaqIds(dc, modifiedSince))).ToList();
        }

        private static IEnumerable<Guid> GetResourceIds(ResourcesDataContext dc)
        {
            return GetArticleIds(dc)
                .Concat(GetVideoIds(dc))
                .Concat(GetQnAIds(dc)
                .Concat(GetFaqIds(dc))).ToList();
        }

        private ResourcesDataContext CreateContext()
        {
            return CreateContext(c => new ResourcesDataContext(c));
        }
    }
}
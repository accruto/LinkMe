using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Framework.Content.Data
{
    public class ContentRepository
        : IContentRepository
    {
        private class ContentItemQueryArgs
        {
            public string Type;
            public string Name;
            public Guid? VerticalId;
            public bool IncludeDisabled;
        }

        private readonly IContentCreator _contentCreator;
        private readonly IDbConnectionFactory _connectionFactory;

        private static readonly Func<ContentDataContext, ContentItemQueryArgs, IContentCreator, ContentItem> GetContentItemByName
             = CompiledQuery.Compile((ContentDataContext dc, ContentItemQueryArgs args, IContentCreator contentCreator)
                 => (from i in dc.ContentItemEntities
                     where i.name == args.Name
                     && i.type == args.Type
                     && (args.IncludeDisabled || i.enabled)
                     && i.parentId == null
                     && !i.deleted
                     && i.verticalId == null
                     select i.Map(contentCreator)).SingleOrDefault());

        private static readonly Func<ContentDataContext, ContentItemQueryArgs, IContentCreator, ContentItem> GetContentItemByNameAndVertical
            = CompiledQuery.Compile((ContentDataContext dc, ContentItemQueryArgs args, IContentCreator contentCreator)
                => (from i in dc.ContentItemEntities
                    where i.name == args.Name
                    && i.type == args.Type
                    && i.verticalId == args.VerticalId
                    && (args.IncludeDisabled || i.enabled)
                    && i.parentId == null
                    && !i.deleted
                    select i.Map(contentCreator)).SingleOrDefault());

        private static readonly Func<ContentDataContext, ContentItemQueryArgs, IContentCreator, ContentItem> GetContentItemByVertical
            = CompiledQuery.Compile((ContentDataContext dc, ContentItemQueryArgs args, IContentCreator contentCreator)
                => (from i in dc.ContentItemEntities
                    where i.type == args.Type
                    && i.verticalId == args.VerticalId
                    && (args.IncludeDisabled || i.enabled)
                    && i.parentId == null
                    && !i.deleted
                    select i.Map(contentCreator)).SingleOrDefault());

        private static readonly Func<ContentDataContext, Guid, IContentCreator, IQueryable<ContentItem>> GetVerticalContentItems
            = CompiledQuery.Compile((ContentDataContext dc, Guid verticalId, IContentCreator contentCreator)
                => from i in dc.ContentItemEntities
                   where i.verticalId == verticalId
                   && i.parentId == null
                   && !i.deleted
                   select i.Map(contentCreator));

        private static readonly Func<ContentDataContext, IContentCreator, IQueryable<ContentItem>> GetDefaultContentItems
            = CompiledQuery.Compile((ContentDataContext dc, IContentCreator contentCreator)
                => from i in dc.ContentItemEntities
                   where i.verticalId == null
                   && i.parentId == null
                   && !i.deleted
                   select i.Map(contentCreator));

        private static readonly Func<ContentDataContext, string, IContentCreator, IQueryable<ContentItem>> GetContentItemsByType
            = CompiledQuery.Compile((ContentDataContext dc, string type, IContentCreator contentCreator)
                => from i in dc.ContentItemEntities
                   where i.type == type
                   && i.parentId == null
                   && i.enabled
                   && !i.deleted
                   select i.Map(contentCreator));

        private static readonly Func<ContentDataContext, Guid, ContentItemEntity> GetContentItemEntity
            = CompiledQuery.Compile((ContentDataContext dc, Guid id)
                => (from i in dc.ContentItemEntities
                    where i.id == id
                    select i).SingleOrDefault());

        public ContentRepository(IContentCreator contentCreator, IDbConnectionFactory connectionFactory)
        {
            _contentCreator = contentCreator;
            _connectionFactory = connectionFactory;
        }

        void IContentRepository.DeleteContentItem(Guid id)
        {
            using (var dc = new ContentDataContext(_connectionFactory.CreateConnection()))
            {
                var entity = new ContentItemEntity { id = id, deleted = false };
                dc.ContentItemEntities.Attach(entity);
                entity.deleted = true;
                dc.SubmitChanges();
            }
        }

        void IContentRepository.EnableContentItem(Guid id)
        {
            using (var dc = new ContentDataContext(_connectionFactory.CreateConnection()))
            {
                var entity = new ContentItemEntity { id = id, enabled = false };
                dc.ContentItemEntities.Attach(entity);
                entity.enabled = true;
                dc.SubmitChanges();
            }
        }

        void IContentRepository.DisableContentItem(Guid id)
        {
            using (var dc = new ContentDataContext(_connectionFactory.CreateConnection()))
            {
                var entity = new ContentItemEntity { id = id, enabled = true };
                dc.ContentItemEntities.Attach(entity);
                entity.enabled = false;
                dc.SubmitChanges();
            }
        }

        ContentItem IContentRepository.GetContentItem(string type, string name, Guid? verticalId, bool includeDisabled)
        {
            var args = new ContentItemQueryArgs { Type = type, Name = name, VerticalId = verticalId, IncludeDisabled = includeDisabled };

            using (var dc = new ContentDataContext(_connectionFactory.CreateConnection()))
            {
                // Only certain combinations are supported for now (and they have to be exact).

                if (verticalId != null)
                {
                    if (name != null)
                        return GetContentItemByNameAndVertical(dc, args, _contentCreator);

                    // Just use the type.

                    return GetContentItemByVertical(dc, args, _contentCreator);
                }

                // Otherwise look for default content.

                if (name != null)
                    return GetContentItemByName(dc, args, _contentCreator);

                // Everything else.

                return null;
            }
        }

        IList<ContentItem> IContentRepository.GetContentItems(Guid? verticalId)
        {
            using (var dc = new ContentDataContext(_connectionFactory.CreateConnection()))
            {
                return verticalId == null
                    ? GetDefaultContentItems(dc, _contentCreator).ToList()
                    : GetVerticalContentItems(dc, verticalId.Value, _contentCreator).ToList();
            }
        }

        IList<ContentItem> IContentRepository.GetContentItems(string type)
        {
            using (var dc = new ContentDataContext(_connectionFactory.CreateConnection()))
            {
                return GetContentItemsByType(dc, type, _contentCreator).ToList();
            }
        }

        void IContentRepository.CreateContentItem(ContentItem item)
        {
            using (var dc = new ContentDataContext(_connectionFactory.CreateConnection()))
            {
                dc.ContentItemEntities.InsertOnSubmit(item.Map());
                dc.SubmitChanges();
            }
        }

        void IContentRepository.UpdateContentItem(ContentItem item)
        {
            using (var dc = new ContentDataContext(_connectionFactory.CreateConnection()))
            {
                var entity = GetContentItemEntity(dc, item.Id);
                if (entity != null)
                {
                    // Remove everything on the current entity because it will be replaced.

                    DeleteProperties(dc, entity);

                    item.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        private static void DeleteProperties(ContentDataContext dc, ContentItemEntity entity)
        {
            foreach (var childEntity in entity.ContentItemEntities)
                DeleteProperties(dc, childEntity);
            dc.ContentItemEntities.DeleteAllOnSubmit(entity.ContentItemEntities);
            dc.ContentDetailEntities.DeleteAllOnSubmit(entity.ContentDetailEntities);
        }
    }
}
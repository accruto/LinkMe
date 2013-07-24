using System;

namespace LinkMe.Framework.Content.Data
{
    internal static class Mappings
    {
        public static ContentItem Map(this ContentItemEntity entity, IContentCreator contentCreator)
        {
            // Create the item.

            var item = contentCreator.CreateContentItem(entity.type);
            item.Id = entity.id;

            // Set general properties.

            item.Name = entity.name;
            item.IsEnabled = entity.enabled;
            item.VerticalId = entity.verticalId;

            // Set content for child items and details.

            foreach (var childItem in entity.ContentItemEntities)
                item.SetChild(childItem.name, childItem.Map(contentCreator));
            foreach (var detail in entity.ContentDetailEntities)
                item.SetField(detail.name, detail.stringValue);

            return item;
        }

        public static ContentItemEntity Map(this ContentItem item)
        {
            var entity = new ContentItemEntity { id = item.Id };
            item.MapTo(entity);
            return entity;
        }

        public static void MapTo(this ContentItem item, ContentItemEntity entity)
        {
            // Set general properties.

            entity.name = item.Name;
            entity.type = item.GetType().Name;
            entity.enabled = item.IsEnabled;
            entity.deleted = false;
            entity.verticalId = item.VerticalId;

            // Set content.

            entity.ContentItemEntities.Clear();
            entity.ContentDetailEntities.Clear();

            foreach (var child in item.Children)
                entity.ContentItemEntities.Add(child.Map());
            foreach (var field in item.Fields)
                entity.ContentDetailEntities.Add(Map(field.Key, field.Value));
        }

        private static ContentDetailEntity Map(string name, object value)
        {
            return new ContentDetailEntity
            {
                id = Guid.NewGuid(),
                name = name,
                type = "String",
                stringValue = value == null ? null : value.ToString(),
            };
        }
    }
}

using System;
using System.Collections.Generic;

namespace LinkMe.Framework.Content
{
    public interface IContentEngine
    {
        void CreateContentItem(ContentItem item);
        void UpdateContentItem(ContentItem item);
        void DeleteContentItem(Guid id);
        void EnableContentItem(Guid id);
        void DisableContentItem(Guid id);

        T GetContentItem<T>(string name, Guid? verticalId) where T : ContentItem;
        T GetContentItem<T>(string name, Guid? verticalId, bool includeDeleted) where T : ContentItem;

        IList<ContentItem> GetContentItems(Guid? verticalId);
        IList<T> GetContentItems<T>() where T : ContentItem;
    }
}

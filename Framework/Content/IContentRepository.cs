using System;
using System.Collections.Generic;

namespace LinkMe.Framework.Content
{
    public interface IContentRepository
    {
        void CreateContentItem(ContentItem item);
        void UpdateContentItem(ContentItem item);
        void DeleteContentItem(Guid id);
        void EnableContentItem(Guid id);
        void DisableContentItem(Guid id);

        ContentItem GetContentItem(string type, string name, Guid? verticalId, bool includeDisabled);
        IList<ContentItem> GetContentItems(Guid? verticalId);
        IList<ContentItem> GetContentItems(string type);
    }
}

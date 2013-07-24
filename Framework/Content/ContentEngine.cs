using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Framework.Content
{
    public class ContentEngine
        : IContentEngine
    {
        private readonly IContentRepository _repository;

        public ContentEngine(IContentRepository repository)
        {
            _repository = repository;
        }

        void IContentEngine.CreateContentItem(ContentItem item)
        {
            Prepare(item, true);
            Validate(item);
            _repository.CreateContentItem(item);
        }

        void IContentEngine.UpdateContentItem(ContentItem item)
        {
            // The children may be newly created and attached to the parent so make sure they are set.

            foreach (var child in item.Children)
            {
                if (child != null)
                    Prepare(child, false);
            }

            Validate(item);
            _repository.UpdateContentItem(item);
        }

        void IContentEngine.DeleteContentItem(Guid id)
        {
            _repository.DeleteContentItem(id);
        }

        void IContentEngine.EnableContentItem(Guid id)
        {
            _repository.EnableContentItem(id);
        }

        void IContentEngine.DisableContentItem(Guid id)
        {
            _repository.DisableContentItem(id);
        }

        T IContentEngine.GetContentItem<T>(string name, Guid? verticalId)
        {
            return _repository.GetContentItem(typeof(T).Name, name, verticalId, false) as T;
        }

        T IContentEngine.GetContentItem<T>(string name, Guid? verticalId, bool includeDeleted)
        {
            return _repository.GetContentItem(typeof(T).Name, name, verticalId, includeDeleted) as T;
        }

        IList<ContentItem> IContentEngine.GetContentItems(Guid? verticalId)
        {
            return _repository.GetContentItems(verticalId);
        }

        IList<T> IContentEngine.GetContentItems<T>()
        {
            return (from i in _repository.GetContentItems(typeof(T).Name)
                    where (i as T != null)
                    select i as T).ToList();
        }

        private static void Prepare(ContentItem item, bool ensureEnabled)
        {
            item.Prepare();
            if (ensureEnabled)
                item.IsEnabled = true;

            foreach (var child in item.Children)
            {
                if (child != null)
                    Prepare(child, ensureEnabled);
            }
        }

        private static void Validate(ContentItem item)
        {
            item.Validate();
            foreach (var child in item.Children)
            {
                if (child != null)
                    Validate(child);
            }
        }
    }
}

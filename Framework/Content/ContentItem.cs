using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Framework.Content
{
    public class ContentItemChildren
        : IEnumerable<ContentItem>
    {
        private readonly IList<ContentItem> _children = new List<ContentItem>();

        internal T GetChild<T>(string name)
            where T : ContentItem
        {
            return (from child in _children where child.Name == name select child as T).FirstOrDefault();
        }

        internal void SetChild(ContentItem value)
        {
            // If the child already exists with the same name then replace it.

            if (!string.IsNullOrEmpty(value.Name))
            {
                for (var index = 0; index < _children.Count; ++index)
                {
                    if (_children[index].Name == value.Name)
                    {
                        _children[index] = value;
                        return;
                    }
                }
            }

            // Not found so add it.

            _children.Add(value);
        }

        internal IList<T> GetChildren<T>()
            where T : ContentItem
        {
            return (from child in _children where (child as T) != null select child as T).ToList();
        }

        internal void SetChildren<T>(IList<T> children)
            where T : ContentItem
        {
            // First need to remove all children of this type.

            var existingChildren = GetChildren<T>();
            foreach (var child in existingChildren)
                _children.Remove(child);

            foreach (var child in children)
                SetChild(child);
        }

        public ContentItem this[string name]
        {
            get { return GetChild<ContentItem>(name); }
        }

        IEnumerator<ContentItem> IEnumerable<ContentItem>.GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _children.GetEnumerator();
        }
    }

    public abstract class ContentItem
    {
        private readonly ContentItemChildren _children = new ContentItemChildren();
        private readonly IDictionary<string, object> _fields = new Dictionary<string, object>();

        [DefaultNewGuid]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public Guid? VerticalId { get; set; }

        protected T GetField<T>(string name)
        {
            object value;
            _fields.TryGetValue(name, out value);
            return (T)value;
        }

        protected internal void SetField<T>(string name, T value)
        {
            _fields[name] = value;
        }

        protected T GetChild<T>(string name)
            where T : ContentItem
        {
            return _children.GetChild<T>(name);
        }

        protected internal void SetChild(string name, ContentItem value)
        {
            if (value != null)
                value.Name = name;
            _children.SetChild(value);
        }

        protected IList<T> GetChildren<T>()
            where T : ContentItem
        {
            return _children.GetChildren<T>();
        }

        protected void SetChildren<T>(IList<T> children)
            where T : ContentItem
        {
            _children.SetChildren(children);
        }

        public ContentItemChildren Children
        {
            get { return _children; }
        }

        public IEnumerable<KeyValuePair<string, object>> Fields
        {
            get { return _fields; }
        }
    }
}

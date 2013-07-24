using System.Collections;
using System.Collections.Generic;
using System.IO;
using IEnumerable=System.Collections.IEnumerable;

namespace LinkMe.Framework.Content.Templates
{
    public class CopyItem
    {
        private readonly string _text;
        private readonly IList<ViewItem> _viewItems = new List<ViewItem>();

        public CopyItem(string text)
        {
            _text = text;
        }

        public string Text
        {
            get { return _text; }
        }

        public IList<ViewItem> ViewItems
        {
            get { return _viewItems; }
        }
    }

    public class ViewItem
    {
        private readonly string _mimeType;
        private readonly string _text;
        private ResourceItems _resourceItems;

        public ViewItem(string mimeType, string text)
        {
            _mimeType = mimeType;
            _text = text;
        }

        public string MimeType
        {
            get { return _mimeType; }
        }

        public string Text
        {
            get { return _text; }
        }

        public ResourceItems ResourceItems
        {
            get
            {
                if (_resourceItems == null)
                    _resourceItems = new ResourceItems();
                return _resourceItems;
            }
        }
    }

    public class ResourceItem
    {
        private readonly string _id;
        private readonly Stream _contentStream;
        private readonly string _mediaType;

        public ResourceItem(string id, Stream contentStream, string mediaType)
        {
            _id = id;
            _contentStream = contentStream;
            _mediaType = mediaType;
        }

        public string Id
        {
            get { return _id; }
        }

        public Stream ContentStream
        {
            get { return _contentStream; }
        }

        public string MediaType
        {
            get { return _mediaType; }
        }
    }

    public class ResourceItems
        : IEnumerable<ResourceItem>
    {
        private readonly IList<ResourceItem> _items = new List<ResourceItem>();

        public void Add(ResourceItem item)
        {
            _items.Add(item);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        IEnumerator<ResourceItem> IEnumerable<ResourceItem>.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
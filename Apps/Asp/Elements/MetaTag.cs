using System.Collections;
using System.Collections.Generic;

namespace LinkMe.Apps.Asp.Elements
{
    public class MetaTag
    {
        private readonly string _name;
        private readonly string _content;

        public MetaTag(string name, string content)
        {
            _name = name;
            _content = content;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Content
        {
            get { return _content; }
        }
    }

    public class MetaTags
        : IEnumerable<MetaTag>
    {
        private readonly IDictionary<string, MetaTag> _metaTags = new Dictionary<string, MetaTag>();

        public string this[string name]
        {
            get
            {
                MetaTag metaTag;
                _metaTags.TryGetValue(name, out metaTag);
                return metaTag == null ? null : metaTag.Content;
            }
            set
            {
                _metaTags[name] = new MetaTag(name, value);
            }
        }

        IEnumerator<MetaTag> IEnumerable<MetaTag>.GetEnumerator()
        {
            return _metaTags.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _metaTags.Values.GetEnumerator();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Web;
using HtmlAgilityPack;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlListItemTester
    {
        private readonly HtmlNode _node;

        public HtmlListItemTester(HtmlNode node)
        {
            _node = node;
        }

        public string Text
        {
            get { return HttpUtility.HtmlDecode(_node.InnerText); }
        }

        public string Value
        {
            get
            {
                var attribute = _node.Attributes["value"];
                return attribute == null ? null : attribute.Value;
            }
        }

        public bool IsSelected
        {
            get { return _node.Attributes["selected"] != null; }
        }

        public bool IsEnabled
        {
            get { return _node.Attributes["disabled"] == null; }
        }
    }

    public class HtmlListItemsList
        : IEnumerable<HtmlListItemTester>
    {
        private readonly IList<HtmlListItemTester> _items = new List<HtmlListItemTester>();

        IEnumerator<HtmlListItemTester> IEnumerable<HtmlListItemTester>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerator<HtmlListItemTester> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        internal void Add(HtmlListItemTester item)
        {
            _items.Add(item);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public HtmlListItemTester this[int index]
        {
            get { return _items[index]; }
        }
    }
}

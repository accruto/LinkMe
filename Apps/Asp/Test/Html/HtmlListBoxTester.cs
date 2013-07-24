using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlListBoxTester
        : HtmlTester
    {
        public HtmlListBoxTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        public HtmlListItemsList Items
        {
            get
            {
                var items = new HtmlListItemsList();
                foreach (var optionNode in GetNodes("option"))
                    items.Add(new HtmlListItemTester(optionNode));
                return items;
            }
        }

        public HtmlListItemsList SelectedItems
        {
            get
            {
                var items = new HtmlListItemsList();
                foreach (var optionNode in GetNodes("option"))
                {
                    var item = new HtmlListItemTester(optionNode);
                    if (item.IsSelected)
                        items.Add(item);
                }
                return items;
            }
        }

        public IList<string> SelectedValues
        {
            get
            {
                return (from i in SelectedItems select i.Value).ToList();
            }
            set
            {
                var name = GetAttributeValue("name");

                // Remove what is already there and then replace.

                RemoveValue(name);
                foreach (var itemValue in value)
                    AddValue(name, itemValue);
            }
        }

        public IList<string> SelectedTexts
        {
            get
            {
                return (from i in SelectedItems select i.Text).ToList();
            }
            set
            {
                var name = GetAttributeValue("name");

                // Remove what is already there and then replace.

                RemoveValue(name);
                foreach (var item in Items)
                {
                    if (value.Contains(item.Text))
                        AddValue(name, item.Value);
                }
            }
        }

        protected override void AssertNode(HtmlNode node)
        {
            Assert.AreEqual("select", node.Name);
            var sizeAttribute = node.Attributes["size"];
            var multipleAttribute = node.Attributes["multiple"];

            // Is this right?

            Assert.IsTrue(sizeAttribute != null || multipleAttribute != null);
            if (sizeAttribute != null)
                Assert.IsTrue(int.Parse(node.Attributes["size"].Value) > 1);
        }
    }
}

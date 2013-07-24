using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlDropDownListTester
        : HtmlTester
    {
        public HtmlDropDownListTester(HttpClient httpClient, string id)
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

        public HtmlListItemTester SelectedItem
        {
            get
            {
                foreach (var item in Items)
                {
                    if (item.IsSelected)
                        return item;
                }

                return null;
            }
        }

        public string SelectedValue
        {
            get
            {
                var item = SelectedItem;
                return item == null ? null : item.Value;
            }
            set
            {
                var name = GetAttributeValue("name");
                RemoveValue(name);

                foreach (var item in Items)
                {
                    if (Equals(item.Value, value))
                    {
                        SetValue(name, value);
                        break;
                    }
                }
            }
        }

        public int? SelectedIndex
        {
            get
            {
                var items = Items;
                for (var index = 0; index < items.Count; ++index)
                {
                    if (items[index].IsSelected)
                        return index;
                }

                return null;
            }
            set
            {
                var name = GetAttributeValue("name");
                RemoveValue(name);

                if (value != null)
                    SetValue(name, Items[value.Value].Value);
            }
        }

        protected override void AssertNode(HtmlNode node)
        {
            Assert.AreEqual("select", node.Name);
            Assert.IsNull(node.Attributes["multiple"]);
        }
    }
}

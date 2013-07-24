using System.Web;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlLinkButtonTester
        : HtmlTester
    {
        public HtmlLinkButtonTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        public void Click()
        {
            PostBack(HttpUtility.HtmlDecode(GetAttributeValue("href")));
        }

        protected override void AssertNode(HtmlNode node)
        {
            Assert.AreEqual("a", node.Name);
            Assert.IsNotNull(node.Attributes["href"]);
            Assert.IsFalse(string.IsNullOrEmpty(node.Attributes["href"].Value));
        }
    }
}

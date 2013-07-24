using System.Web;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlTextAreaTester
        : HtmlTester
    {
        public HtmlTextAreaTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        public string Text
        {
            get { return HttpUtility.HtmlDecode(GetInnerText()); }
            set { SetValue(GetAttributeValue("name"), value); }
        }

        protected override void AssertNode(HtmlNode node)
        {
            Assert.AreEqual("textarea", node.Name);
        }
    }
}
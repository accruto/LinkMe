using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class HtmlLinkTester
        : HtmlTester
    {
        private const RegexOptions Options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
        private static readonly Regex PopupPattern = new Regex("window.open\\('(?<link>.*?)'", Options);

        public HtmlLinkTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        public void Click()
        {
            var popupLink = PopupLink;
            if (popupLink != null)
            {
                Get(popupLink);
            }
            else
            {
                var href = HRef;
                if (IsPostBack(href))
                    PostBack(href);
                else
                    Get(href);
            }
        }

        public string HRef
        {
            get { return HttpUtility.HtmlDecode(GetAttributeValue("href")); }
        }

        private string PopupLink
        {
            get
            {
                var onClick = GetOptionalAttributeValue("onclick");
                if (onClick == null)
                    return null;
                var match = PopupPattern.Match(onClick);
                return match.Captures.Count == 1 ? match.Groups["link"].Value : null;
            }
        }

        protected override void AssertNode(HtmlNode node)
        {
            Assert.AreEqual("a", node.Name);
            Assert.IsNotNull(node.Attributes["href"]);
            Assert.IsFalse(string.IsNullOrEmpty(node.Attributes["href"].Value));
        }
    }
}
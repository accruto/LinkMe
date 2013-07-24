using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Html
{
    public abstract class HtmlInputTester
        : HtmlTester
    {
        protected HtmlInputTester(HttpClient httpClient, string id)
            : base(httpClient, id)
        {
        }

        protected abstract string InputType { get; }

        protected override void AssertNode(HtmlNode node)
        {
            Assert.AreEqual("input", node.Name);
            Assert.AreEqual(InputType, node.Attributes["type"].Value);
        }
    }
}
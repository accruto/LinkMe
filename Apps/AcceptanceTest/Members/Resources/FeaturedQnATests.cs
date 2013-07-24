using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Resources
{
    [TestClass]
    public class FeaturedQnATests
        : ResourcesTests
    {
        [TestMethod]
        public void TestNew()
        {
            var featuredQnA = _resourcesQuery.GetFeaturedQnAs().Single();
            var qna = _resourcesQuery.GetQnA(featuredQnA.ResourceId);

            Get(_resourcesUrl);
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='featuredquestion']/div[@class='bg']");
            Assert.AreEqual(1, nodes.Count);
            var node = nodes[0];

            var a = node.SelectSingleNode("a");
            var href = a.Attributes["href"].Value;
            var id = new Guid(href.Substring(href.LastIndexOf('/') + 1));

            Assert.AreEqual(qna.Id, id);
            Assert.AreEqual(qna.Title, node.SelectSingleNode("./div[@class='title']").InnerText);
            Assert.AreEqual(GetInnerText(qna.Text), GetText(node.SelectSingleNode("./div[@class='content']").InnerText));
        }
    }
}

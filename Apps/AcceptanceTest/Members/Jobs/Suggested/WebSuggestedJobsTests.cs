using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Suggested
{
    [TestClass]
    public class WebSuggestedJobsTests
        : SuggestedJobsTests
    {
        protected override void AssertNoJobs()
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='results']/div[@class='emptylist']/span");
            Assert.IsNotNull(node);
            Assert.AreEqual("Unfortunately, there aren't any suggested jobs for you at the moment.", node.InnerText);
        }

        protected override IList<HtmlNode> GetSuggestedJobAdNodes()
        {
            var resultsNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='results']");
            return (from c in resultsNode.ChildNodes
                    let a = c.Attributes["class"]
                    where c.Name == "div"
                    && a != null
                    && a.Value.StartsWith("jobad-list-view")
                    && !a.Value.Contains("empty")
                    select c).ToList();
        }

        protected override void AssertIsNew(bool expectedIsNew, HtmlNode node)
        {
            var newNode = node.SelectSingleNode(".//div[@class='isnew']");
            if (expectedIsNew)
                Assert.IsNotNull(newNode);
            else
                Assert.IsNull(newNode);
        }

        protected override IList<HtmlNode> GetJobSuggestedJobAdNodes()
        {
            return Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='suggestedjobs']/div[@class='suggestedjob']");
        }
    }
}

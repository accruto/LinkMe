using System.Collections.Generic;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Suggested
{
    [TestClass]
    public class MobileSuggestedJobsTests
        : SuggestedJobsTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Browser.UseMobileUserAgent = true;
        }

        protected override void AssertNoJobs()
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='mobilefolder']/div[@class='noresults']/div[@class='desc']");
            Assert.IsNotNull(node);
            Assert.AreEqual("Unfortunately, there aren't any suggested jobs for you at the moment.Update your profile on a computer to help us to find you suitable jobs.", node.InnerText);
        }

        protected override IList<HtmlNode> GetSuggestedJobAdNodes()
        {
            return Browser.CurrentHtml.DocumentNode.SelectNodes("//div[starts-with(@class, 'jobad-list-view')]");
        }

        protected override void AssertIsNew(bool expectedIsNew, HtmlNode node)
        {
            var newNode = node.SelectSingleNode(".//div[starts-with(@class, 'icon new ')]");
            Assert.IsNotNull(newNode);
            Assert.AreEqual(expectedIsNew ? "icon new active" : "icon new ", newNode.Attributes["class"].Value);
        }

        protected override IList<HtmlNode> GetJobSuggestedJobAdNodes()
        {
            return Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='similarjobs']/div[starts-with(@class, 'row ')]");
        }
    }
}

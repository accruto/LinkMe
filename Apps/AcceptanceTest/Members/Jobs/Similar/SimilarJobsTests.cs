using System;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Similar
{
    [TestClass]
    public class SimilarJobsTests
        : JobsTests
    {
        [TestMethod]
        public void TestNoSimilarJobs()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Get the page itself.

            var url = GetSimilarJobsUrl(jobAd.Id);
            Get(url);
            AssertUrl(url);

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='results']/div[@class='emptylist']/span");
            Assert.IsNotNull(node);
            Assert.AreEqual("Unfortunately, there aren't any similar jobs at the moment.", node.InnerText);

            Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='row empty']"));

            // Get the partial page.

            url = GetPartialSimilarJobsUrl(jobAd.Id);
            Get(url);
            AssertUrl(url);

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='row']");
            Assert.IsNull(nodes);
        }

        private static ReadOnlyUrl GetSimilarJobsUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/similar/" + jobAdId);
        }

        private static ReadOnlyUrl GetPartialSimilarJobsUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/similar/" + jobAdId + "/partial");
        }
    }
}

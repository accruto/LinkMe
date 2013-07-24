using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class ContentTests
        : DisplayTests
    {
        private const string Content = "Here is some content";
        private const string HtmlContent = "Here is <b>some</b> content";

        [TestMethod]
        public void TestContent()
        {
            TestContent(Content, Content);
        }

        [TestMethod]
        public void TestHtmlContent()
        {
            TestContent(HtmlContent, Content);
        }

        private void TestContent(string content, string expectedContent)
        {
            var jobAd = CreateJobAd(content);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            node = node.SelectSingleNode(".//div[@class='description']");
            Assert.IsNotNull(node);
            Assert.AreEqual(expectedContent, node.InnerText);

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.AreEqual(expectedContent, model.JobAds[0].Content);
        }

        private JobAd CreateJobAd(string content)
        {
            var employer = CreateEmployer(0);
            var jobAd = employer.CreateTestJobAd(Keywords);
            jobAd.Description.Content = content;
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}

using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class TitleTests
        : DisplayTests
    {
        private const string Title = "I.T. Architect";
        private const string TitleKeywords = "Architect";

        [TestMethod]
        public void TestTitle()
        {
            var jobAd = CreateJobAd(Title);
            Search(TitleKeywords);
            var node = GetResult(jobAd.Id);

            node = node.SelectSingleNode(".//a[@class='title']");
            Assert.IsNotNull(node);
            Assert.AreEqual(Title, node.Attributes["title"].Value);
            Assert.AreEqual(Title, node.InnerText);

            var model = ApiSearch(TitleKeywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.AreEqual(Title, model.JobAds[0].Title);
        }

        private JobAd CreateJobAd(string title)
        {
            var employer = CreateEmployer(0);
            var jobAd = employer.CreateTestJobAd(title);
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}

using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Mobile
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

            node = node.SelectSingleNode(".//div[@class='title']");
            Assert.IsNotNull(node);
            Assert.AreEqual(Title, node.InnerText);
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

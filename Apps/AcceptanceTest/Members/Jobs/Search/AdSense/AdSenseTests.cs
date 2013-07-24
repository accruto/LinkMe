using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.AdSense
{
    [TestClass]
    public class AdSenseTests
        : SearchTests
    {
        private const string Keywords = "Analyst";

        [TestMethod]
        public void TestSearch()
        {
            var employer = CreateEmployer(0);
            PostJobAd(Keywords, employer);

            Search(Keywords);
            AssertAdSense(Keywords);
        }

        [TestMethod]
        public void TestSearchNone()
        {
            Search(Keywords);
            AssertAdSense(Keywords);
        }

        [TestMethod]
        public void TestSearchMany()
        {
            var employer = CreateEmployer(0);
            PostJobAd(Keywords, employer);
            PostJobAd(Keywords, employer);
            PostJobAd(Keywords, employer);
            PostJobAd(Keywords, employer);

            Search(Keywords);
            AssertAdSense(Keywords);
        }

        private void PostJobAd(string jobTitle, IEmployer employer)
        {
            var jobAd = employer.CreateTestJobAd(jobTitle, "The content for the job");
            _jobAdsCommand.PostJobAd(jobAd);
        }

        private void AssertAdSense(string keywords)
        {
            Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//script[@src='https://www.google.com/adsense/search/ads.js']"));
            AssertPageContains("\'query\': \'" + keywords.ToLower() + " jobs\'");
            AssertPageContains("var adblock1 = {");
            AssertPageContains("var adblock2 = {");
            AssertPageContains("adblock3 = {");
        }
    }
}

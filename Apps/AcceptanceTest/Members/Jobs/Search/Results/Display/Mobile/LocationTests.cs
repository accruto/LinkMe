using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Mobile
{
    [TestClass]
    public class LocationTests
        : DisplayTests
    {
        private const string Location = "Norlane VIC 3214";

        [TestMethod]
        public void TestLocation()
        {
            TestLocation(Location);
        }

        [TestMethod]
        public void TestNoLocation()
        {
            TestLocation(null);
        }

        private void TestLocation(string location)
        {
            var jobAd = CreateJobAd(location);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            node = node.SelectSingleNode(".//div[@class='location']");
            Assert.IsNotNull(node);
            Assert.AreEqual(location ?? "", node.InnerText);
        }

        private JobAd CreateJobAd(string location)
        {
            var employer = CreateEmployer(0);
            var jobAd = employer.CreateTestJobAd(Keywords);
            jobAd.Description.Location = string.IsNullOrEmpty(location)
                ? null
                : _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), location);
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}

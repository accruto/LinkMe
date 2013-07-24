using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class JobTypesTests
        : DisplayTests
    {
        [TestMethod]
        public void TestFullTime()
        {
            TestJobTypes(JobTypes.FullTime, "FullTime");
        }

        [TestMethod]
        public void TestFullTimeContract()
        {
            TestJobTypes(JobTypes.FullTime | JobTypes.Contract, "FullTime, Contract");
        }

        [TestMethod]
        public void TestAll()
        {
            TestJobTypes(JobTypes.All, "All");
        }

        [TestMethod]
        public void TestNone()
        {
            TestJobTypes(JobTypes.None, "None");
        }

        private void TestJobTypes(JobTypes jobTypes, string expectedJobTypes)
        {
            var jobAd = CreateJobAd(jobTypes);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            node = node.SelectSingleNode(".//div[@class='icon types']");
            Assert.IsNotNull(node);
            Assert.AreEqual(expectedJobTypes, node.Attributes["jobtypes"].Value);

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.AreEqual(expectedJobTypes, model.JobAds[0].JobTypes);
        }

        private JobAd CreateJobAd(JobTypes jobTypes)
        {
            var employer = CreateEmployer(0);
            var jobAd = employer.CreateTestJobAd(Keywords);
            jobAd.Description.JobTypes = jobTypes;
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}

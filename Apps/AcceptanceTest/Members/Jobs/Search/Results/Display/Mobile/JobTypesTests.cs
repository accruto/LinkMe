using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Mobile
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

            node = node.SelectSingleNode(".//div[@class='icon jobtype']");
            Assert.IsNotNull(node);
            Assert.AreEqual(expectedJobTypes, node.Attributes["jobtypes"].Value);
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

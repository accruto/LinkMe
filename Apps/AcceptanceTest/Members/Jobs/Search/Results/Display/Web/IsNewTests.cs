using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class IsNewTests
        : DisplayTests
    {
        [TestMethod]
        public void TestIsNew()
        {
            var jobAd = CreateJobAd(DateTime.Now.AddHours(-1));
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.IsNotNull(node.SelectSingleNode(".//div[@class='icon new active']"));
            Assert.IsNull(node.SelectSingleNode(".//div[@class='icon new ']"));

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsTrue(model.JobAds[0].IsNew);
        }

        [TestMethod]
        public void TestIsNotNew()
        {
            var jobAd = CreateJobAd(DateTime.Now.AddDays(-10));
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.IsNull(node.SelectSingleNode(".//div[@class='icon new active']"));
            Assert.IsNotNull(node.SelectSingleNode(".//div[@class='icon new ']"));

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsFalse(model.JobAds[0].IsNew);
        }

        private JobAd CreateJobAd(DateTime createdTime)
        {
            var employer = CreateEmployer(0);
            var jobAd = employer.CreateTestJobAd(Keywords);
            jobAd.CreatedTime = createdTime;
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}

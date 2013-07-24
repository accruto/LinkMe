using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Mobile
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
        }

        [TestMethod]
        public void TestIsNotNew()
        {
            var jobAd = CreateJobAd(DateTime.Now.AddDays(-10));
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.IsNull(node.SelectSingleNode(".//div[@class='icon new active']"));
            Assert.IsNotNull(node.SelectSingleNode(".//div[@class='icon new ']"));
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

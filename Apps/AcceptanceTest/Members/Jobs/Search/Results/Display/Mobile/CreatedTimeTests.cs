using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Mobile
{
    [TestClass]
    public class CreatedTimeTests
        : DisplayTests
    {
        [TestMethod]
        public void TestHours()
        {
            TestCreatedTime(DateTime.Now.AddHours(-1).AddMinutes(-10), "Posted 1 hour ago");
        }

        [TestMethod]
        public void TestDays()
        {
            TestCreatedTime(DateTime.Now.AddDays(-10).AddHours(-1), "Posted 10 days ago");
        }

        private void TestCreatedTime(DateTime createdTime, string expectedCreatedTime)
        {
            var jobAd = CreateJobAd(createdTime);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            node = node.SelectSingleNode(".//div[@class='date']");
            Assert.AreEqual(expectedCreatedTime, node.InnerText);
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

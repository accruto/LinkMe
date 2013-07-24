using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Create
{
    [TestClass]
    public class FeatureBoostCreateJobAdTests
        : CreateJobAdTests
    {
        [TestMethod]
        public void TestNone()
        {
            TestBoost(JobAdFeatureBoost.None);
        }

        [TestMethod]
        public void TestLow()
        {
            TestBoost(JobAdFeatureBoost.Low);
        }

        [TestMethod]
        public void TestHigh()
        {
            TestBoost(JobAdFeatureBoost.High);
        }

        private void TestBoost(JobAdFeatureBoost boost)
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(poster.Id, 0);
            jobAd.FeatureBoost = boost;
            _jobAdsCommand.CreateJobAd(jobAd);
            AssertFeaturedLevel(jobAd.Id, boost);
        }

        private void AssertFeaturedLevel(Guid jobAdId, JobAdFeatureBoost expectedBoost)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            Assert.AreEqual(expectedBoost, jobAd.FeatureBoost);
        }
    }
}
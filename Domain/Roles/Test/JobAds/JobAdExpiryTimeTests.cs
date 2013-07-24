using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.JobAds
{
    [TestClass]
    public class JobAdExpiryTimeTests
        : JobAdsTests
    {
        private const int ExpiryDurationDays = 14;
        private const int ExtendedExpiryDurationDays = 30;

        [TestMethod]
        public void TestInitialDefaultExpiryTime()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, null);

            // When created the expiry time is null if not explicitly set.

            Assert.AreEqual(null, jobAd.ExpiryTime);
            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(null, jobAd.ExpiryTime);
        }

        [TestMethod]
        public void TestInitialExpiryTime()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, j => j.ExpiryTime = DateTime.Now.AddDays(10));

            // When created the expiry time is null if not explicitly set.

            var expiryTime = DateTime.Now.Date.AddDays(10).AddDays(1).AddSeconds(-1);
            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
        }

        [TestMethod]
        public void TestDefaultOpenExpiryTime()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, null);

            _jobAdsCommand.OpenJobAd(jobAd);

            var expiryTime = DateTime.Now.Date.AddDays(ExpiryDurationDays).AddDays(1).AddSeconds(-1);
            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
        }

        [TestMethod]
        public void TestDefaultOpenExtendedExpiryTime()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, j => j.Features = JobAdFeatures.ExtendedExpiry);

            _jobAdsCommand.OpenJobAd(jobAd);

            var expiryTime = DateTime.Now.Date.AddDays(ExtendedExpiryDurationDays).AddDays(1).AddSeconds(-1);
            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
        }

        [TestMethod]
        public void TestInitialOpenExpiryTime()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, j => j.ExpiryTime = DateTime.Now.AddDays(10));

            _jobAdsCommand.OpenJobAd(jobAd);

            var expiryTime = DateTime.Now.Date.AddDays(10).AddDays(1).AddSeconds(-1);
            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
        }

        [TestMethod]
        public void TestChangedOpenExpiryTime()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, null);

            // Set the expiry time to shorter then the default.

            var expiryTime = DateTime.Now.Date.AddDays(ExpiryDurationDays).AddDays(1).AddSeconds(-1).AddDays(-10);
            jobAd.ExpiryTime = expiryTime;
            _jobAdsCommand.UpdateJobAd(jobAd);

            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);

            // Now open it.

            _jobAdsCommand.OpenJobAd(jobAd);

            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(expiryTime, jobAd.ExpiryTime);
        }
    }
}

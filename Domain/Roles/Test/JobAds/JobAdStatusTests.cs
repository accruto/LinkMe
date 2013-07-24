using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.JobAds
{
    [TestClass]
    public class JobAdStatusTests
        : JobAdsTests
    {
        private readonly IJobAdsRepository _repository = Resolve<IJobAdsRepository>();

        private const int ExpiryDurationDays = 14;
        private const int ExtendedExpiryDurationDays = 30;

        [TestMethod]
        public void TestDraftJobAd()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, null);

            AssertStatus(JobAdStatus.Draft, null, jobAd);
        }

        [TestMethod]
        public void TestOpenDraftJobAd()
        {
            var jobAd = CreateJobAd(false);
            Assert.IsTrue(_jobAdsCommand.CanBeOpened(jobAd));
            _jobAdsCommand.OpenJobAd(jobAd);
            AssertStatus(JobAdStatus.Open, GetExpectedExpiryTime(false), jobAd);
        }

        [TestMethod]
        public void TestOpenDraftExtendedJobAd()
        {
            var jobAd = CreateJobAd(true);
            Assert.IsTrue(_jobAdsCommand.CanBeOpened(jobAd));
            _jobAdsCommand.OpenJobAd(jobAd);
            AssertStatus(JobAdStatus.Open, GetExpectedExpiryTime(true), jobAd);
        }

        [TestMethod]
        public void TestDeleteDraftJobAd()
        {
            var jobAd = CreateJobAd(false);
            _jobAdsCommand.DeleteJobAd(jobAd);
            AssertStatus(JobAdStatus.Deleted, null, jobAd);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestCloseDraftJobAd()
        {
            var jobAd = CreateJobAd(false);
            _jobAdsCommand.CloseJobAd(jobAd);
        }

        [TestMethod]
        public void TestOpenOpenJobAd()
        {
            var jobAd = CreateJobAd(false);
            _jobAdsCommand.OpenJobAd(jobAd);
            Assert.IsTrue(_jobAdsCommand.CanBeOpened(jobAd));
            _jobAdsCommand.OpenJobAd(jobAd);
            AssertStatus(JobAdStatus.Open, GetExpectedExpiryTime(false), jobAd);
        }

        [TestMethod]
        public void TestOpenOpenExtendedJobAd()
        {
            var jobAd = CreateJobAd(true);
            _jobAdsCommand.OpenJobAd(jobAd);
            Assert.IsTrue(_jobAdsCommand.CanBeOpened(jobAd));
            _jobAdsCommand.OpenJobAd(jobAd);
            AssertStatus(JobAdStatus.Open, GetExpectedExpiryTime(true), jobAd);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestDeleteOpenJobAd()
        {
            var jobAd = CreateJobAd(false);
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.DeleteJobAd(jobAd);
        }

        [TestMethod]
        public void TestCloseOpenJobAd()
        {
            var jobAd = CreateJobAd(false);
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            AssertStatus(JobAdStatus.Closed, GetExpectedExpiryTime(false), jobAd);
        }

        [TestMethod]
        public void TestCloseOpenExtendedJobAd()
        {
            var jobAd = CreateJobAd(true);
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            AssertStatus(JobAdStatus.Closed, GetExpectedExpiryTime(true), jobAd);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestOpenDeletedJobAd()
        {
            var jobAd = CreateJobAd(false);
            _jobAdsCommand.DeleteJobAd(jobAd);
            Assert.IsFalse(_jobAdsCommand.CanBeOpened(jobAd));
            _jobAdsCommand.OpenJobAd(jobAd);
        }

        [TestMethod]
        public void TestDeleteDeletedJobAd()
        {
            var jobAd = CreateJobAd(false);
            _jobAdsCommand.DeleteJobAd(jobAd);
            _jobAdsCommand.DeleteJobAd(jobAd);
            AssertStatus(JobAdStatus.Deleted, null, jobAd);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestCloseDeletedJobAd()
        {
            var jobAd = CreateJobAd(false);
            _jobAdsCommand.DeleteJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestOpenClosedExpiredJobAd()
        {
            var jobAd = CreateJobAd(DateTime.Now.Date.AddDays(-40), DateTime.Now.Date.AddDays(-10));
            _jobAdsCommand.CloseJobAd(jobAd);
            Assert.IsFalse(_jobAdsCommand.CanBeOpened(jobAd));
            _jobAdsCommand.OpenJobAd(jobAd);
        }

        [TestMethod]
        public void TestOpenClosedNonExpiredJobAd()
        {
            var jobAd = CreateJobAd(DateTime.Now.Date.AddDays(-20), DateTime.Now.Date.AddDays(10));
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            Assert.IsTrue(_jobAdsCommand.CanBeOpened(jobAd));
            _jobAdsCommand.OpenJobAd(jobAd);
            AssertStatus(JobAdStatus.Open, DateTime.Now.Date.AddDays(10), jobAd);
        }

        [TestMethod]
        public void TestDeleteClosedJobAd()
        {
            var jobAd = CreateJobAd(false);
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            _jobAdsCommand.DeleteJobAd(jobAd);
            AssertStatus(JobAdStatus.Deleted, GetExpectedExpiryTime(false), jobAd);
        }

        [TestMethod]
        public void TestDeleteClosedExtendedJobAd()
        {
            var jobAd = CreateJobAd(true);
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            _jobAdsCommand.DeleteJobAd(jobAd);
            AssertStatus(JobAdStatus.Deleted, GetExpectedExpiryTime(true), jobAd);
        }

        [TestMethod]
        public void TestCloseClosedJobAd()
        {
            var jobAd = CreateJobAd(false);
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            AssertStatus(JobAdStatus.Closed, GetExpectedExpiryTime(false), jobAd);
        }

        [TestMethod]
        public void TestCloseClosedExtendedJobAd()
        {
            var jobAd = CreateJobAd(true);
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            AssertStatus(JobAdStatus.Closed, GetExpectedExpiryTime(true), jobAd);
        }

        private JobAd CreateJobAd(bool isExtended)
        {
            var poster = CreateJobPoster();
            return CreateJobAd(
                0,
                poster,
                j =>
                {
                    j.CreatedTime = DateTime.Now.AddDays(-40);
                    if (isExtended)
                        j.Features = j.Features.SetFlag(JobAdFeatures.ExtendedExpiry);
                });
        }

        private JobAd CreateJobAd(DateTime createdTime, DateTime expiryTime)
        {
            var jobAd = CreateJobAd(false);
            jobAd.CreatedTime = createdTime;
            jobAd.ExpiryTime = expiryTime;
            _repository.UpdateJobAd(jobAd);
            return jobAd;
        }

        private static DateTime GetExpectedExpiryTime(bool isExtended)
        {
            return DateTime.Now.Date.AddDays(isExtended ? ExtendedExpiryDurationDays : ExpiryDurationDays).AddDays(1).AddSeconds(-1);
        }

        private void AssertStatus(JobAdStatus expectedStatus, DateTime? expectedExpiryTime, JobAd jobAd)
        {
            Assert.AreEqual(expectedStatus, jobAd.Status);
            Assert.AreEqual(expectedExpiryTime, jobAd.ExpiryTime);
            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(expectedStatus, jobAd.Status);
            Assert.AreEqual(expectedExpiryTime, jobAd.ExpiryTime);
        }
    }
}

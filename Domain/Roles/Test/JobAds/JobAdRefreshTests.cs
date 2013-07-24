using System;
using System.Threading;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.JobAds
{
    [TestClass]
    public class JobAdRefreshTests
        : JobAdsTests
    {
        [TestMethod]
        public void TestOpenNoRefresh()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, null);

            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1)).Count);
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd.Id));

            _jobAdsCommand.OpenJobAd(jobAd);

            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1)).Count);
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd.Id));
        }

        [TestMethod]
        public void TestOpenRefresh()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, j => j.Features = JobAdFeatures.Refresh);

            // Opening the job ad with this feature.

            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1)).Count);
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd.Id));

            _jobAdsCommand.OpenJobAd(jobAd);
            
            Assert.IsTrue(new[] { jobAd.Id }.CollectionEqual(_jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1))));
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNotNull(_jobAdsQuery.GetLastRefreshTime(jobAd.Id));
        }

        [TestMethod]
        public void TestCloseRefresh()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, j => j.Features = JobAdFeatures.Refresh);

            // Opening the job ad with this feature.

            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1)).Count);
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd.Id));

            _jobAdsCommand.OpenJobAd(jobAd);

            Assert.IsTrue(new[] { jobAd.Id }.CollectionEqual(_jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1))));
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNotNull(_jobAdsQuery.GetLastRefreshTime(jobAd.Id));

            _jobAdsCommand.CloseJobAd(jobAd);

            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1)).Count);
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd.Id));
        }

        [TestMethod]
        public void TestMultipleJobAds()
        {
            var poster = CreateJobPoster();

            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1)).Count);
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);

            // First job ad with feature.

            var jobAd1 = CreateJobAd(1, poster, j => j.Features = JobAdFeatures.Refresh);
            _jobAdsCommand.OpenJobAd(jobAd1);

            Assert.IsTrue(new[] { jobAd1.Id }.CollectionEqual(_jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1))));
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNotNull(_jobAdsQuery.GetLastRefreshTime(jobAd1.Id));

            // Second job ad no feature.

            var jobAd2 = CreateJobAd(2, poster, null);
            _jobAdsCommand.OpenJobAd(jobAd2);

            Assert.IsTrue(new[] { jobAd1.Id }.CollectionEqual(_jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1))));
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNotNull(_jobAdsQuery.GetLastRefreshTime(jobAd1.Id));
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd2.Id));

            // Third job ad with features.

            var jobAd3 = CreateJobAd(3, poster, j => j.Features = JobAdFeatures.Refresh);
            _jobAdsCommand.OpenJobAd(jobAd3);

            Assert.IsTrue(new[] { jobAd1.Id, jobAd3.Id }.CollectionEqual(_jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1))));
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNotNull(_jobAdsQuery.GetLastRefreshTime(jobAd1.Id));
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd2.Id));
            Assert.IsNotNull(_jobAdsQuery.GetLastRefreshTime(jobAd3.Id));

            // Close the first.

            _jobAdsCommand.CloseJobAd(jobAd1);

            Assert.IsTrue(new[] { jobAd3.Id }.CollectionEqual(_jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1))));
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd1.Id));
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd2.Id));
            Assert.IsNotNull(_jobAdsQuery.GetLastRefreshTime(jobAd3.Id));

            // Close the second.

            _jobAdsCommand.CloseJobAd(jobAd2);

            Assert.IsTrue(new[] { jobAd3.Id }.CollectionEqual(_jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1))));
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd1.Id));
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd2.Id));
            Assert.IsNotNull(_jobAdsQuery.GetLastRefreshTime(jobAd3.Id));

            // Close the third.

            _jobAdsCommand.CloseJobAd(jobAd3);

            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(1)).Count);
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now.AddDays(-1)).Count);
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd1.Id));
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd2.Id));
            Assert.IsNull(_jobAdsQuery.GetLastRefreshTime(jobAd3.Id));
        }

        [TestMethod]
        public void Refresh()
        {
            var poster = CreateJobPoster();

            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime.Now).Count);

            // Open job ad.

            Thread.Sleep(20);
            var jobAd = CreateJobAd(0, poster, j => j.Features = JobAdFeatures.Refresh);
            _jobAdsCommand.OpenJobAd(jobAd);

            Thread.Sleep(20);
            var now = DateTime.Now;
            Assert.IsTrue(new[] { jobAd.Id }.CollectionEqual(_jobAdsQuery.GetJobAdIdsRequiringRefresh(now)));
            var lastRefreshTime = _jobAdsQuery.GetLastRefreshTime(jobAd.Id);
            Assert.IsNotNull(lastRefreshTime);

            // Refresh.

            Thread.Sleep(20);
            _jobAdsCommand.RefreshJobAd(jobAd);
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(now).Count);
            Assert.AreNotEqual(lastRefreshTime, _jobAdsQuery.GetLastRefreshTime(jobAd.Id));
            lastRefreshTime = _jobAdsQuery.GetLastRefreshTime(jobAd.Id);
            Assert.IsNotNull(lastRefreshTime);

            // Wait some time.

            Thread.Sleep(20);
            now = DateTime.Now;
            Assert.IsTrue(new[] { jobAd.Id }.CollectionEqual(_jobAdsQuery.GetJobAdIdsRequiringRefresh(now)));
            Assert.AreEqual(lastRefreshTime, _jobAdsQuery.GetLastRefreshTime(jobAd.Id));
            lastRefreshTime = _jobAdsQuery.GetLastRefreshTime(jobAd.Id);
            Assert.IsNotNull(lastRefreshTime);

            // Refresh again.

            Thread.Sleep(20);
            _jobAdsCommand.RefreshJobAd(jobAd);
            Assert.AreEqual(0, _jobAdsQuery.GetJobAdIdsRequiringRefresh(now).Count);
            Assert.AreNotEqual(lastRefreshTime, _jobAdsQuery.GetLastRefreshTime(jobAd.Id));
            lastRefreshTime = _jobAdsQuery.GetLastRefreshTime(jobAd.Id);
            Assert.IsNotNull(lastRefreshTime);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestRefreshNoFeatureJobAd()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, null);

            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.RefreshJobAd(jobAd);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestRefreshDraft()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, j => j.Features = JobAdFeatures.Refresh);
            _jobAdsCommand.RefreshJobAd(jobAd);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestRefreshClosed()
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(0, poster, j => j.Features = JobAdFeatures.Refresh);

            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            _jobAdsCommand.RefreshJobAd(jobAd);
        }
    }
}

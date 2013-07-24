using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.PreviewJobAds
{
    [TestClass]
    public class PreviewClosedExpiredJobAdTests
        : PreviewJobAdTests
    {
        [TestMethod]
        public void TestClosedZeroCreditsEdit()
        {
            var employer = CreateEmployer(1);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-35), DateTime.Now.AddDays(-5).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsTrue(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _editButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertNoMessages();
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestClosedZeroCreditsRepost()
        {
            var employer = CreateEmployer(1);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-35), DateTime.Now.AddDays(-5).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsTrue(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _repostButton.Click();

            // Look for the new job ad.

            var jobAdIds = _jobAdsQuery.GetOpenJobAdIds(employer.Id);
            Assert.AreEqual(1, jobAdIds.Count);
            var newJobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdIds[0]);
            AssertUrlWithoutQuery(GetSuggestedCandidatesUrl(newJobAd.Id));
            AssertConfirmationMessage("<p>\'" + HtmlUtil.HtmlToText(newJobAd.Title) + "\' was successfully published.</p><p>It will expire on <b>" + DateTime.Now.Date.AddDays(14).ToShortDateString() + "</b>.</p>");

            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));
            AssertJobAd(newJobAd.Id, JobAdStatus.Open, DateTime.Now.Date.AddDays(14).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestClosedSomeCreditsEdit()
        {
            var employer = CreateEmployer(10);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-35), DateTime.Now.AddDays(-5).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.Date.AddDays(14));

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsTrue(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _editButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertNoMessages();
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestClosedSomeCreditsRepost()
        {
            var employer = CreateEmployer(10);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-35), DateTime.Now.AddDays(-5).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.Date.AddDays(14));

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsTrue(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _repostButton.Click();

            // Look for the new job.

            var jobAdIds = _jobAdsQuery.GetOpenJobAdIds(employer.Id);
            Assert.AreEqual(1, jobAdIds.Count);
            var newJobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdIds[0]);
            AssertUrlWithoutQuery(GetSuggestedCandidatesUrl(newJobAd.Id));
            AssertConfirmationMessage("<p>\'" + HtmlUtil.HtmlToText(newJobAd.Title) + "\' was successfully published.</p><p>It will expire on <b>" + DateTime.Now.Date.AddDays(14).ToShortDateString() + "</b>.</p>");

            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));
            AssertJobAd(newJobAd.Id, JobAdStatus.Open, DateTime.Now.Date.AddDays(14).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestClosedUnlimitedCreditsEdit()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-35), DateTime.Now.AddDays(-5).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.Date.AddDays(14));

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsTrue(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _editButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertNoMessages();
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestClosedUnlimitedCreditsRepost()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-35), DateTime.Now.AddDays(-5).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.Date.AddDays(14));

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsTrue(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _repostButton.Click();

            // Look for the new job ad.

            var jobAdIds = _jobAdsQuery.GetOpenJobAdIds(employer.Id);
            Assert.AreEqual(1, jobAdIds.Count);
            var newJobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdIds[0]);
            AssertUrlWithoutQuery(GetSuggestedCandidatesUrl(newJobAd.Id));
            AssertConfirmationMessage("<p>\'" + HtmlUtil.HtmlToText(newJobAd.Title) + "\' was successfully published.</p><p>It will expire on <b>" + DateTime.Now.Date.AddDays(14).ToShortDateString() + "</b>.</p>");

            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(-5).AddDays(1).AddSeconds(-1));
            AssertJobAd(newJobAd.Id, JobAdStatus.Open, DateTime.Now.Date.AddDays(14).AddDays(1).AddSeconds(-1));
        }
    }
}

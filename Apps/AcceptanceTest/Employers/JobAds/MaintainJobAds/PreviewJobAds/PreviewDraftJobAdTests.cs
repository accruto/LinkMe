using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.PreviewJobAds
{
    [TestClass]
    public class PreviewDraftJobAdTests
        : PreviewJobAdTests
    {
        [TestMethod]
        public void TestDraftZeroCreditsPublish()
        {
            var employer = CreateEmployer(0);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft, DateTime.Now.AddDays(-5), null);
            AssertJobAd(jobAd.Id, JobAdStatus.Draft, null);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();

            Assert.IsTrue(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsTrue(_baseFeaturePack.IsVisible);
            Assert.IsTrue(_featurePack1.IsVisible);
            Assert.IsTrue(_featurePack2.IsVisible);

            _publishButton.Click();
            AssertUrlWithoutQuery(GetSuggestedCandidatesUrl(jobAd.Id));
            AssertConfirmationMessage("<p>\'" + HtmlUtil.HtmlToText(jobAd.Title) + "\' was successfully published.</p><p>It will expire on <b>" + DateTime.Now.Date.AddDays(14).ToShortDateString() + "</b>.</p>");
            AssertJobAd(jobAd.Id, JobAdStatus.Open, DateTime.Now.Date.AddDays(14).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestDraftZeroCreditsEdit()
        {
            var employer = CreateEmployer(0);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft, DateTime.Now.AddDays(-5), null);
            AssertJobAd(jobAd.Id, JobAdStatus.Draft, null);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();

            Assert.IsTrue(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsTrue(_baseFeaturePack.IsVisible);
            Assert.IsTrue(_featurePack1.IsVisible);
            Assert.IsTrue(_featurePack2.IsVisible);

            _editButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertNoMessages();
            AssertJobAd(jobAd.Id, JobAdStatus.Draft, null);
        }

        [TestMethod]
        public void TestDraftSomeCreditsPublish()
        {
            var employer = CreateEmployer(10);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft, DateTime.Now.AddDays(-5), null);
            AssertJobAd(jobAd.Id, JobAdStatus.Draft, null);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.Date.AddDays(14));

            Assert.IsTrue(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _publishButton.Click();
            AssertUrlWithoutQuery(GetSuggestedCandidatesUrl(jobAd.Id));
            AssertConfirmationMessage("<p>\'" + HtmlUtil.HtmlToText(jobAd.Title) + "\' was successfully published.</p><p>It will expire on <b>" + DateTime.Now.Date.AddDays(14).ToShortDateString() + "</b>.</p>");
            AssertJobAd(jobAd.Id, JobAdStatus.Open, DateTime.Now.Date.AddDays(14).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestDraftSomeCreditsEdit()
        {
            var employer = CreateEmployer(10);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft, DateTime.Now.AddDays(-5), null);
            AssertJobAd(jobAd.Id, JobAdStatus.Draft, null);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.Date.AddDays(14));

            Assert.IsTrue(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _editButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertNoMessages();
            AssertJobAd(jobAd.Id, JobAdStatus.Draft, null);
        }

        [TestMethod]
        public void TestDraftUnlimitedCreditsPublish()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft, DateTime.Now.AddDays(-5), null);
            AssertJobAd(jobAd.Id, JobAdStatus.Draft, null);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.Date.AddDays(14));

            Assert.IsTrue(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _publishButton.Click();
            AssertUrlWithoutQuery(GetSuggestedCandidatesUrl(jobAd.Id));
            AssertConfirmationMessage("<p>\'" + HtmlUtil.HtmlToText(jobAd.Title) + "\' was successfully published.</p><p>It will expire on <b>" + DateTime.Now.Date.AddDays(14).ToShortDateString() + "</b>.</p>");
            AssertJobAd(jobAd.Id, JobAdStatus.Open, DateTime.Now.Date.AddDays(14).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestDraftUnlimitedCreditsEdit()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft, DateTime.Now.AddDays(-5), null);
            AssertJobAd(jobAd.Id, JobAdStatus.Draft, null);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.Date.AddDays(14));

            Assert.IsTrue(_publishButton.IsVisible);
            Assert.IsFalse(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _editButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertNoMessages();
            AssertJobAd(jobAd.Id, JobAdStatus.Draft, null);
        }
    }
}

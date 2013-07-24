using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Web.UI.Registered.Employers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.PreviewJobAds
{
    [TestClass]
    public class PreviewClosedJobAdTests
        : PreviewJobAdTests
    {
        [TestMethod]
        public void TestClosedZeroCreditsEdit()
        {
            var employer = CreateEmployer(1);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsTrue(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _editButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertNoMessages();
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestClosedZeroCreditsReopen()
        {
            var employer = CreateEmployer(1);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsTrue(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _reopenButton.Click();
            AssertPage<EmployerJobAds>();
            AssertConfirmationMessage("\'" + HtmlUtil.HtmlToText(jobAd.Title) + "\' has been reopened.");
            AssertJobAd(jobAd.Id, JobAdStatus.Open, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestClosedSomeCreditsEdit()
        {
            var employer = CreateEmployer(10);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsTrue(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _editButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertNoMessages();
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestClosedSomeCreditsReopen()
        {
            var employer = CreateEmployer(10);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsTrue(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _reopenButton.Click();
            AssertPage<EmployerJobAds>();
            AssertConfirmationMessage("\'" + HtmlUtil.HtmlToText(jobAd.Title) + "\' has been reopened.");
            AssertJobAd(jobAd.Id, JobAdStatus.Open, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestClosedUnlimitedCreditsEdit()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsTrue(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _editButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertNoMessages();
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));
        }

        [TestMethod]
        public void TestClosedUnlimitedCreditsReopen()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));
            AssertJobAd(jobAd.Id, JobAdStatus.Closed, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));
            _previewButton.Click();
            AssertExpiryTime(DateTime.Now.AddDays(12).Date.AddDays(1).AddSeconds(-1));

            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsTrue(_reopenButton.IsVisible);
            Assert.IsFalse(_repostButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            Assert.IsFalse(_baseFeaturePack.IsVisible);
            Assert.IsFalse(_featurePack1.IsVisible);
            Assert.IsFalse(_featurePack2.IsVisible);

            _reopenButton.Click();
            AssertPage<EmployerJobAds>();
            AssertConfirmationMessage("\'" + HtmlUtil.HtmlToText(jobAd.Title) + "\' has been reopened.");
            AssertJobAd(jobAd.Id, JobAdStatus.Open, DateTime.Now.Date.AddDays(12).AddDays(1).AddSeconds(-1));
        }
    }
}

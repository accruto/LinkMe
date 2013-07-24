using System;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Web.UI.Registered.Employers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds
{
	[TestClass]
    public class JobAdListTests
        : MaintainJobAdTests
	{
        private HtmlLinkButtonTester _lbExecuteAd;

        [TestInitialize]
        public void TestInitialize()
		{
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _lbExecuteAd = new HtmlLinkButtonTester(Browser, "ctl00_ctl00_Body_FormContent_employerJobAdsControl_AdsRepeater_ctl00_lbExecuteAd");
        }

        [TestMethod]
        public void TestOpenJobAd()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Open);
            LogIn(employer);

            // Go to job ads page and look for ad.

            ViewJobAd(jobAd);
        }

        [TestMethod]
        public void TestClosedJobAd()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Closed);
            LogIn(employer);

            // Go to job ads page and look for ad.

            ViewJobAd(jobAd);
        }

        [TestMethod]
        public void TestDraftJobAd()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft);
            LogIn(employer);

            // Go to job ads page and look for ad.

            ViewJobAd(jobAd);
        }

        [TestMethod]
		public void TestOpenClosedJobAd()
		{
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Open);
            LogIn(employer);

            // View the ad.

            GetPage<EmployerJobAds>();
            AssertLink("//a[@class='edit-action']", GetJobAdUrl(jobAd.Id), "Edit");

            // Close the job ad.

            _jobAdsCommand.CloseJobAd(jobAd);

            // Try to view again.

            GetPage<EmployerJobAds>();
            Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='edit-action']"));

            // Reopen it.

            GetPage<EmployerJobAds>(EmployerJobAds.QryStrSwitchedMode, JobAdStatus.Closed.ToString());
            AssertPageContains(jobAd.Title);
            _lbExecuteAd.Click();

            // Check.

            AssertPage<EmployerJobAds>();
            AssertPageContains(jobAd.GetTitleAndReferenceDisplayText() + " has been reopened");
            Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='edit-action']"));
        }

	    [TestMethod]
        public void TestOpenClosedExpiredJobAd()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Open);
            LogIn(employer);

            // View the ad.

            GetPage<EmployerJobAds>();
            AssertLink("//a[@class='edit-action']", GetJobAdUrl(jobAd.Id), "Edit");

            // Expire and close the job ad.

            jobAd.CreatedTime = DateTime.Now.AddDays(-26);
            jobAd.ExpiryTime = DateTime.Now.AddDays(-6);
            _jobAdsCommand.UpdateJobAd(jobAd);
	        _jobAdsCommand.CloseJobAd(jobAd);

            // Try to view again.

            GetPage<EmployerJobAds>();
            Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='edit-action']"));

            // Reopen it.

            GetPage<EmployerJobAds>(EmployerJobAds.QryStrSwitchedMode, JobAdStatus.Closed.ToString());
            AssertPageContains(jobAd.Title);
            _lbExecuteAd.Click();

            // Should be directed to the preview page.

            AssertUrlWithoutQuery(GetPreviewUrl(jobAd.Id));
            AssertInformationMessage("Please review the ad before publishing it.");

            // Affirm the repost.

	        _repostButton.Click();
            AssertConfirmationMessage("<p>\'" + HtmlUtil.HtmlToText(jobAd.Title) + "\' was successfully published.</p><p>It will expire on <b>" + DateTime.Now.Date.AddDays(14).ToShortDateString() + "</b>.</p>");
        }

        [TestMethod]
        public void TestDeleteDraftJobAd()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft);
            Assert.AreEqual(JobAdStatus.Draft, jobAd.Status);

            LogIn(employer);

            // Delete the job ad.

            GetPage<EmployerJobAds>(EmployerJobAds.QryStrSwitchedMode, JobAdStatus.Draft.ToString());
            AssertPageContains(jobAd.Title);
            _lbExecuteAd.Click();

            // Check.

            AssertPageContains(string.Format("Your draft ad &#39;{0}&#39; has been deleted.", jobAd.Title));
            GetPage<EmployerJobAds>(EmployerJobAds.QryStrSwitchedMode, JobAdStatus.Draft.ToString());
            AssertPageDoesNotContain(jobAd.Title);

            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(JobAdStatus.Deleted, jobAd.Status);
        }

        [TestMethod]
        public void TestCloseOpenJobAd()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Open);
            Assert.AreEqual(JobAdStatus.Open, jobAd.Status);

            LogIn(employer);

            // Close the job ad.

            GetPage<EmployerJobAds>(EmployerJobAds.QryStrSwitchedMode, JobAdStatus.Open.ToString());
            AssertPageContains(jobAd.Title);
            _lbExecuteAd.Click();

            // Check.

            AssertPageContains(jobAd.Title + " has been closed.");
            GetPage<EmployerJobAds>(EmployerJobAds.QryStrSwitchedMode, JobAdStatus.Open.ToString());
            AssertPageDoesNotContain(jobAd.Title);

            GetPage<EmployerJobAds>(EmployerJobAds.QryStrSwitchedMode, JobAdStatus.Closed.ToString());
            AssertPageContains(jobAd.Title);

            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(JobAdStatus.Closed, jobAd.Status);
        }

	    private void ViewJobAd(JobAdEntry jobAd)
        {
            // Open ads.

            GetPage<EmployerJobAds>();
            if (jobAd.Status == JobAdStatus.Open)
            {
                AssertPageContains(jobAd.Title);
                AssertPageContains(jobAd.ContactDetails.EmailAddress);
            }
            else
            {
                AssertPageDoesNotContain(jobAd.Title);
                AssertPageDoesNotContain(jobAd.ContactDetails.EmailAddress);
            }

            // Draft ads.

            GetPage<EmployerJobAds>(EmployerJobAds.QryStrSwitchedMode, JobAdStatus.Draft.ToString());
            if (jobAd.Status == JobAdStatus.Draft)
            {
                AssertPageContains(jobAd.Title);
                AssertPageContains(jobAd.ContactDetails.EmailAddress);
            }
            else
            {
                AssertPageDoesNotContain(jobAd.Title);
                AssertPageDoesNotContain(jobAd.ContactDetails.EmailAddress);
            }

            // Closed ads.

            GetPage<EmployerJobAds>(EmployerJobAds.QryStrSwitchedMode, JobAdStatus.Closed.ToString());
            if (jobAd.Status == JobAdStatus.Closed)
            {
                AssertPageContains(jobAd.Title);
                AssertPageContains(jobAd.ContactDetails.EmailAddress);
            }
            else
            {
                AssertPageDoesNotContain(jobAd.Title);
                AssertPageDoesNotContain(jobAd.ContactDetails.EmailAddress);
            }
        }
	}
}

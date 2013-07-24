using System;
using LinkMe.Apps.Agents.Test.Users;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.NewJobAdFlow
{
    [TestClass]
    public abstract class EmployerNewJobAdFlowTests
        : NewJobAdFlowTests
    {
        private const string EmailAddress = "monty@test.linkme.net.au";

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlButtonTester _loginButton;

        [TestInitialize]
        public void NewJobAdFlowTestInitialize()
        {
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _loginButton = new HtmlButtonTester(Browser, "login");
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestLoggedIn()
        {
            var credits = GetEmployerCredits();
            var employer = CreateEmployer(credits, CreateOrganisation());
            LogIn(employer);

            // Job ad.

            Get(GetJobAdUrl(null));
            
            AssertJobAdPage(true);

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { _accounting.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = DefaultLocation;

            // Preview.

            _previewButton.Click();

            var jobAdId = AssertJobAd(employer.Id, JobAdStatus.Draft, JobAdFeatures.None).Id;
            AssertPreviewPage(jobAdId, true, credits);

            // Publish.

            _publishButton.Click();

            var jobAd = AssertJobAd(employer.Id, JobAdStatus.Open, JobAdFeatures.None);
            AssertPublishPage(jobAd, JobAdFeatures.None);

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNotLoggedIn()
        {
            var credits = GetEmployerCredits();
            var employer = CreateEmployer(credits, CreateOrganisation());
            Get(EmployerHomeUrl);
            var anonymousId = GetAnonymousId();
            Assert.IsNotNull(anonymousId);

            // Job ad.

            Get(GetJobAdUrl(null));

            AssertJobAdPage(false);

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { _accounting.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _emailAddressTextBox.Text = EmailAddress;
            _locationTextBox.Text = DefaultLocation;

            // Preview.

            _previewButton.Click();

            var jobAdId = AssertJobAd(anonymousId, JobAdStatus.Draft, JobAdFeatures.None).Id;
            AssertPreviewPage(jobAdId, false, 0);

            // Publish.

            _publishButton.Click();

            AssertJobAd(anonymousId, JobAdStatus.Draft, JobAdFeatures.None);
            AssertAccountPage(jobAdId);

            // Login.

            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = employer.GetPassword();
            _loginButton.Click();

            // Check.

            var jobAd = AssertJobAd(employer.Id, JobAdStatus.Open, JobAdFeatures.None);
            AssertNoJobAd(anonymousId);
            AssertPublishPage(jobAd, JobAdFeatures.None);

            _emailServer.AssertNoEmailSent();
        }

        protected abstract int? GetEmployerCredits();
        protected abstract bool ShouldFeaturePacksShow { get; }

        protected virtual Organisation CreateOrganisation()
        {
            return _organisationsCommand.CreateTestOrganisation(0);
        }

        protected void AssertJobAdPage(bool isLoggedIn)
        {
            Assert.IsTrue(_previewButton.IsVisible);
            Assert.AreEqual(isLoggedIn,  _saveButton.IsVisible);
        }

        protected void AssertPreviewPage(Guid jobAdId, bool isLoggedIn, int? credits)
        {
            AssertUrl(GetPreviewUrl(jobAdId));
            Assert.IsTrue(_publishButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);

            var shouldFeaturePacksShow = ShouldFeaturePacksShow || !isLoggedIn;
            Assert.AreEqual(shouldFeaturePacksShow, _baseFeaturePack.IsVisible);
            Assert.AreEqual(shouldFeaturePacksShow, _featurePack1.IsVisible);
            Assert.AreEqual(shouldFeaturePacksShow, _featurePack2.IsVisible);
            if (shouldFeaturePacksShow)
            {
                Assert.IsTrue(_baseFeaturePack.IsChecked);
                Assert.IsFalse(_featurePack1.IsChecked);
                Assert.IsFalse(_featurePack2.IsChecked);

                var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='three-columns-shadowed-section shadowed-section section']/div[@class='section-body']/div[@class='section-title']/h2");
                Assert.IsNotNull(node);
                Assert.AreEqual("Free", node.InnerText);
            }
            else
            {
                var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='three-columns-shadowed-section shadowed-section section']/div[@class='section-body']/div[@class='section-title']/h2");
                Assert.IsNull(node);
            }
        }

        private void AssertAccountPage(Guid jobAdId)
        {
            AssertUrl(GetAccountUrl(jobAdId));
        }

        protected void AssertPaymentPage(Guid jobAdId, JobAdFeaturePack featurePack)
        {
            AssertUrl(GetPaymentUrl(jobAdId, featurePack));
        }

        protected void AssertPublishPage(JobAdEntry jobAd, JobAdFeatures features)
        {
            AssertUrlWithoutQuery(GetSuggestedCandidatesUrl(jobAd.Id));
            AssertConfirmationMessage(
                "<p>\'"
                + HtmlUtil.HtmlToText(jobAd.Title)
                + "\' was successfully published.</p><p>It will expire on <b>"
                + DateTime.Now.Date.AddDays(features.IsFlagSet(JobAdFeatures.ExtendedExpiry) ? 30 : 14).ToShortDateString()
                + "</b>.</p>");
        }

        protected void AssertReceiptPage(JobAdEntry jobAd, Order order)
        {
            AssertUrl(GetReceiptUrl(jobAd.Id, order.Id));
            AssertPageContains("'" + HtmlUtil.HtmlToText(jobAd.Title) + "' was successfully published");
        }
    }
}

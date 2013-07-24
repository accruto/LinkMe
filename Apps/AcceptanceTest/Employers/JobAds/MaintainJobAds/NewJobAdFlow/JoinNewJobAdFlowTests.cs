using System;
using LinkMe.Apps.Agents.Users.Employers.Queries;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.NewJobAdFlow
{
    [TestClass]
    public class JoinNewJobAdFlowTests
        : NewJobAdFlowTests
    {
        private readonly IEmployerAccountsQuery _employerAccountsQuery = Resolve<IEmployerAccountsQuery>();

        private const string LoginId = "tester";
        private const string Password = "password";
        private const string FirstName = "Marge";
        private const string LastName = "Simpson";
        private const string EmailAddress = "tester@test.linkme.net.au";
        private const string PhoneNumber = "0399998888";
        private const string OrganisationName = "Acme";
        private const string Location = "Camberwell VIC 3124";

        private HtmlTextBoxTester _joinLoginIdTextBox;
        private HtmlPasswordTester _joinPasswordTextBox;
        private HtmlPasswordTester _joinConfirmPasswordTextBox;
        private HtmlTextBoxTester _joinFirstNameTextBox;
        private HtmlTextBoxTester _joinLastNameTextBox;
        private HtmlTextBoxTester _joinEmailAddressTextBox;
        private HtmlTextBoxTester _joinPhoneNumberTextBox;
        private HtmlTextBoxTester _joinOrganisationNameTextBox;
        private HtmlTextBoxTester _joinLocationTextBox;
        private HtmlCheckBoxTester _joinAcceptTermsCheckBox;
        private HtmlButtonTester _joinButton;

        [TestInitialize]
        public void JoinNewJobAdFlowTestInitialize()
        {
            _joinLoginIdTextBox = new HtmlTextBoxTester(Browser, "JoinLoginId");
            _joinPasswordTextBox = new HtmlPasswordTester(Browser, "JoinPassword");
            _joinConfirmPasswordTextBox = new HtmlPasswordTester(Browser, "JoinConfirmPassword");
            _joinFirstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _joinLastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _joinEmailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _joinPhoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _joinOrganisationNameTextBox = new HtmlTextBoxTester(Browser, "OrganisationName");
            _joinLocationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _joinAcceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
            _joinButton = new HtmlButtonTester(Browser, "join");
        }

        [TestMethod]
        public void TestJoin()
        {
            Get(EmployerHomeUrl);
            var anonymousId = GetAnonymousId();
            Assert.IsNotNull(anonymousId);

            // Job ad.

            Get(GetJobAdUrl(null));
            Assert.IsTrue(_previewButton.IsVisible);
            Assert.IsFalse(_saveButton.IsVisible);

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { _accounting.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _emailAddressTextBox.Text = EmailAddress;
            _locationTextBox.Text = DefaultLocation;

            // Preview.

            _previewButton.Click();

            var jobAdId = AssertJobAd(anonymousId, JobAdStatus.Draft, JobAdFeatures.None).Id;
            AssertUrl(GetPreviewUrl(jobAdId));
            Assert.IsTrue(_publishButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);

            // Publish.

            _publishButton.Click();

            AssertUrl(GetAccountUrl(jobAdId));
            AssertJobAd(anonymousId, JobAdStatus.Draft, JobAdFeatures.None);

            // Join.

            _joinLoginIdTextBox.Text = LoginId;
            _joinPasswordTextBox.Text = Password;
            _joinConfirmPasswordTextBox.Text = Password;
            _joinFirstNameTextBox.Text = FirstName;
            _joinLastNameTextBox.Text = LastName;
            _joinEmailAddressTextBox.Text = EmailAddress;
            _joinPhoneNumberTextBox.Text = PhoneNumber;
            _joinOrganisationNameTextBox.Text = OrganisationName;
            _joinLocationTextBox.Text = Location;
            _joinAcceptTermsCheckBox.IsChecked = true;

            _joinButton.Click();

            // Check.

            var employer = _employerAccountsQuery.GetEmployer(LoginId);
            var jobAd = AssertJobAd(employer.Id, JobAdStatus.Open, JobAdFeatures.None);
            AssertUrlWithoutQuery(GetSuggestedCandidatesUrl(jobAd.Id));
            AssertConfirmationMessage("<p>\'" + HtmlUtil.HtmlToText(jobAd.Title) + "\' was successfully published.</p><p>It will expire on <b>" + DateTime.Now.Date.AddDays(14).ToShortDateString() + "</b>.</p>");
            AssertNoJobAd(anonymousId);
        }
    }
}

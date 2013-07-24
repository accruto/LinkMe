using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.External
{
    [TestClass]
    public class ExternalConvertTests
        : ExternalCredentialsTests
    {
        private readonly IResumesCommand _resumesCommand = Resolve<IResumesCommand>();

        private const string ExternalId = "abcdefgh";
        private const string EmailAddress = "member@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string JobTitle = "Archeologist";
        private const string JobCompany = "Acme";
        private const string NewEmailAddress = "hsimpson@test.linkme.net.au";
        private const string NewPassword = "abc123";

        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _jobTitleTextBox;
        private HtmlTextBoxTester _jobCompanyTextBox;
        private HtmlTextBoxTester _newEmailAddressTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlPasswordTester _confirmPasswordTextBox;
        private HtmlButtonTester _saveButton;

        private ReadOnlyUrl _activationUrl;
        private ReadOnlyUrl _profileUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _jobTitleTextBox = new HtmlTextBoxTester(Browser, "JobTitle");
            _jobCompanyTextBox = new HtmlTextBoxTester(Browser, "JobCompany");
            _newEmailAddressTextBox = new HtmlTextBoxTester(Browser, "NewEmailAddress");
            _saveButton = new HtmlButtonTester(Browser, "save");

            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _confirmPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmPassword");

            _activationUrl = new ReadOnlyApplicationUrl("~/accounts/activation");
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");

            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestUnknownVertical()
        {
            Get(GetConvertUrl("aaa"));
            AssertUrl(HomeUrl);
        }

        [TestMethod]
        public void TestNonExternalVertical()
        {
            var vertical = CreateVertical(TestCommunity.Scouts);
            Get(GetConvertUrl(vertical.Url));
            AssertUrl(HomeUrl);
        }

        [TestMethod]
        public void TestExternalVertical()
        {
            var vertical = CreateVertical(TestCommunity.LiveInAustralia);
            var reclaimUrl = GetConvertUrl(vertical.Url);
            Get(reclaimUrl);

            AssertUrl(reclaimUrl);
            Assert.AreEqual("", _emailAddressTextBox.Text);
            Assert.AreEqual("", _firstNameTextBox.Text);
            Assert.AreEqual("", _lastNameTextBox.Text);
            Assert.AreEqual("", _jobTitleTextBox.Text);
            Assert.AreEqual("", _jobCompanyTextBox.Text);
            Assert.AreEqual("", _newEmailAddressTextBox.Text);
        }

        [TestMethod]
        public void TestConvert()
        {
            var vertical = CreateVertical();
            CreateMember(EmailAddress, FirstName, LastName, JobTitle, JobCompany, vertical.Id, true, ExternalId);

            // Check initial member.

            var member = _membersQuery.GetMember(EmailAddress);
            AssertMember(EmailAddress, FirstName, LastName, vertical.Id, true, true, member);
            Assert.IsNull(_membersQuery.GetMember(NewEmailAddress));
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));

            var reclaimUrl = GetConvertUrl(vertical.Url);
            Get(reclaimUrl);
            AssertUrl(reclaimUrl);
            _emailAddressTextBox.Text = EmailAddress;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _jobTitleTextBox.Text = JobTitle;
            _jobCompanyTextBox.Text = JobCompany;
            _newEmailAddressTextBox.Text = NewEmailAddress;
            _passwordTextBox.Text = NewPassword;
            _confirmPasswordTextBox.Text = NewPassword;
            _saveButton.Click();
            AssertUrl(GetConvertedUrl(vertical.Url));
            AssertPageContains("account has been converted into a full LinkMe account");

            // Check member.

            Assert.IsNull(_membersQuery.GetMember(EmailAddress));
            member = _membersQuery.GetMember(NewEmailAddress);
            AssertMember(NewEmailAddress, FirstName, LastName, null, true, false, member);
            AssertCredentials(NewEmailAddress, false, true, _loginCredentialsQuery.GetCredentials(member.Id));
            Assert.IsNull(_externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));

            // Activation email should have been sent.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            var url = GetLink(email.GetHtmlView().Body);

            Get(url);
            AssertUrlWithoutQuery(_activationUrl);
            SubmitLogIn(member.EmailAddresses[0].Address, NewPassword);

            AssertUrl(_profileUrl);
        }

        [TestMethod]
        public void TestRequired()
        {
            var vertical = CreateVertical();
            var reclaimUrl = GetConvertUrl(vertical.Url);
            Get(reclaimUrl);

            _emailAddressTextBox.Text = EmailAddress;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _jobTitleTextBox.Text = JobTitle;
            _jobCompanyTextBox.Text = JobCompany;
            _newEmailAddressTextBox.Text = NewEmailAddress;
            _passwordTextBox.Text = NewPassword;
            _confirmPasswordTextBox.Text = NewPassword;

            // Email address.

            _emailAddressTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(reclaimUrl);
            AssertErrorMessage("The email address is required.");
            _emailAddressTextBox.Text = EmailAddress;

            // First name.

            _firstNameTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(reclaimUrl);
            AssertErrorMessage("The first name is required.");
            _firstNameTextBox.Text = FirstName;

            // Last name.

            _lastNameTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(reclaimUrl);
            AssertErrorMessage("The last name is required.");
            _lastNameTextBox.Text = LastName;

            // Job title.

            _jobTitleTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(reclaimUrl);
            AssertErrorMessage("The job title is required.");
            _jobTitleTextBox.Text = JobTitle;

            // Job company.

            _jobCompanyTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(reclaimUrl);
            AssertErrorMessage("The job company is required.");
            _jobCompanyTextBox.Text = JobCompany;

            // New email address.

            _newEmailAddressTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(reclaimUrl);
            AssertErrorMessage("The new email address is required.");
            _newEmailAddressTextBox.Text = NewEmailAddress;

            // Password.

            _passwordTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(reclaimUrl);
            AssertErrorMessages("The password is required.", "The confirm password and password must match.");
            _passwordTextBox.Text = NewPassword;

            // Confirm password.

            _confirmPasswordTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(reclaimUrl);
            AssertErrorMessages("The confirm password is required.", "The confirm password and password must match.");
            _confirmPasswordTextBox.Text = NewPassword;
        }

        [TestMethod]
        public void TestUnknownEmailAddress()
        {
            TestNotFound(() => _emailAddressTextBox.Text = "Unknown");
        }

        [TestMethod]
        public void TestNonMatchingFirstName()
        {
            TestNotFound(() => _firstNameTextBox.Text = "Other");
        }

        [TestMethod]
        public void TestNonMatchingLastName()
        {
            TestNotFound(() => _lastNameTextBox.Text = "Other");
        }

        [TestMethod]
        public void TestNonMatchingJobTitle()
        {
            TestNotFound(() => _jobTitleTextBox.Text = "Other");
        }

        [TestMethod]
        public void TestNonMatchingJobCompany()
        {
            TestNotFound(() => _jobCompanyTextBox.Text = "Other");
        }

        private void TestNotFound(Action action)
        {
            var vertical = CreateVertical();
            CreateMember(EmailAddress, FirstName, LastName, JobTitle, JobCompany, vertical.Id, true, ExternalId);

            var reclaimUrl = GetConvertUrl(vertical.Url);
            Get(reclaimUrl);

            _emailAddressTextBox.Text = EmailAddress;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _jobTitleTextBox.Text = JobTitle;
            _jobCompanyTextBox.Text = JobCompany;
            _newEmailAddressTextBox.Text = NewEmailAddress;
            _passwordTextBox.Text = NewPassword;
            _confirmPasswordTextBox.Text = NewPassword;

            action();

            _saveButton.Click();

            AssertUrl(reclaimUrl);
            AssertErrorMessage("The account cannot be found. Please check the spelling for all fields carefully.");
        }

        private static void AssertCredentials(string loginId, bool mustChangePassword, bool hasPasswordHash, LoginCredentials credentials)
        {
            Assert.AreEqual(loginId, credentials.LoginId);
            Assert.AreEqual(mustChangePassword, credentials.MustChangePassword);
            Assert.AreEqual(hasPasswordHash, !string.IsNullOrEmpty(credentials.PasswordHash));
        }

        private static void AssertMember(string emailAddress, string firstName, string lastName, Guid? affiliateId, bool isEnabled, bool isActivated, IMember member)
        {
            Assert.IsNotNull(member);
            Assert.AreEqual(emailAddress, member.EmailAddresses[0].Address);
            Assert.AreEqual(firstName, member.FirstName);
            Assert.AreEqual(lastName, member.LastName);
            Assert.AreEqual(isEnabled, member.IsEnabled);
            Assert.AreEqual(isActivated, member.IsActivated);
            Assert.AreEqual(affiliateId, member.AffiliateId);
        }

        private static ReadOnlyUrl GetConvertUrl(string verticalUrl)
        {
            return new ReadOnlyApplicationUrl("~/" + verticalUrl + "/accounts/convert");
        }

        private static ReadOnlyUrl GetConvertedUrl(string verticalUrl)
        {
            return new ReadOnlyApplicationUrl("~/" + verticalUrl + "/accounts/converted");
        }

        private Vertical CreateVertical(TestCommunity testCommunity)
        {
            var community = testCommunity.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            return _verticalsCommand.GetVertical(community);
        }

        private void CreateMember(string emailAddress, string firstName, string lastName, string jobTitle, string jobCompany, Guid verticalId, bool isAffiliated, string externalId)
        {
            var member = new Member
            {
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress } },
                FirstName = firstName,
                LastName = lastName,
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Sydney 2000 NSW") },
                IsActivated = true,
            };

            var externalCredentals = new ExternalCredentials { ProviderId = verticalId, ExternalId = externalId };
            _memberAccountsCommand.CreateMember(member, externalCredentals, isAffiliated ? verticalId : (Guid?)null);

            // Add a resume.

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);
            resume.Jobs = new List<Job> { new Job { Title = jobTitle, Company = jobCompany } };
            _resumesCommand.UpdateResume(resume);
        }

        private static ReadOnlyUrl GetLink(string body)
        {
            var url = XElement.Load(new StringReader(body))
                .Descendants("a").Attributes("href")
                .Select(a => a.Value).First();
            return new ReadOnlyApplicationUrl(url);
        }
    }
}

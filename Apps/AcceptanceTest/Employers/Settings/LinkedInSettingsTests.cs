using System;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration.LinkedIn;
using LinkMe.Domain.Roles.Integration.LinkedIn.Commands;
using LinkMe.Domain.Roles.Integration.LinkedIn.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Accounts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Settings
{
	[TestClass]
	public class LinkedInSettingsTests
        : WebTestClass
	{
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ILinkedInCommand _linkedInCommand = Resolve<ILinkedInCommand>();
        private readonly ILinkedInQuery _linkedInQuery = Resolve<ILinkedInQuery>();

	    private const string LinkedInId = "abcdef";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string EmailAddress = "homer@test.linkme.net.au";
        private const string PhoneNumber = "76767676";
        private const string LoginId = "homerj";
        private const string Password = "password";

	    private ReadOnlyUrl _settingsUrl;
        private ReadOnlyUrl _apiLoginUrl;

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlPasswordTester _confirmPasswordTextBox;
        private HtmlCheckBoxTester _useLinkedInProfileCheckBox;
		private HtmlButtonTester _saveButton;

        private class LinkedInApiProfile
            : JsonRequestModel
        {
            public string Id { get; set; }
        }

        [TestInitialize]
        public void TestInitialize()
		{
			Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _useLinkedInProfileCheckBox = new HtmlCheckBoxTester(Browser, "UseLinkedInProfile");
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _confirmPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmPassword");
            _saveButton = new HtmlButtonTester(Browser, "save");

            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/employers/settings");
            _apiLoginUrl = new ReadOnlyApplicationUrl(true, "~/accounts/linkedin/api/login");
        }

        [TestMethod]
        public void TestLoginIdLinkedInProfile()
        {
            var employer = CreateEmployer(0);
            _linkedInCommand.UpdateProfile(new LinkedInProfile { Id = LinkedInId, UserId = employer.Id });

            LogIn(employer);
            Get(_settingsUrl);
            AssertLinkedInProfile(employer.GetLoginId(), false, true);

            // Try to update.

            _useLinkedInProfileCheckBox.IsChecked = false;
            _saveButton.Click();
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);

            // Assert profile.

            Assert.IsNull(_linkedInQuery.GetProfile(LinkedInId));
            Assert.IsNull(_linkedInQuery.GetProfile(employer.Id));

            Get(_settingsUrl);
            AssertNoLinkedInProfile(employer.GetLoginId());
        }

        [TestMethod]
        public void TestLoginIdLinkedInProfileValidation()
        {
            var employer = CreateEmployer(0);
            _linkedInCommand.UpdateProfile(new LinkedInProfile { Id = LinkedInId, UserId = employer.Id });

            LogIn(employer);
            Get(_settingsUrl);
            AssertLinkedInProfile(employer.GetLoginId(), false, true);

            // Try to remove the login id.

            _useLinkedInProfileCheckBox.IsChecked = false;
            _loginIdTextBox.Text = "";
            _saveButton.Click();

            AssertUrl(_settingsUrl);
            AssertNoLinkedInProfile("");
            AssertErrorMessage("The username is required.");

            // Assert profile not changed.

            var profile = _linkedInQuery.GetProfile(LinkedInId);
            Assert.IsNotNull(profile);
            Assert.AreEqual(employer.Id, profile.UserId);
        }

        [TestMethod]
        public void TestNoLinkedInProfile()
        {
            var employer = CreateEmployer(0);

            LogIn(employer);
            Get(_settingsUrl);
            AssertNoLinkedInProfile(employer.GetLoginId());

            // Log into LinkedIn.

            var url = LinkedInApiLogIn(AuthenticationStatus.Authenticated, new LinkedInApiProfile { Id = LinkedInId });
	        Get(url);
            AssertUrl(_settingsUrl);
            AssertLinkedInProfile(employer.GetLoginId(), false, true);

            // Check profile created.

            var profile = _linkedInQuery.GetProfile(LinkedInId);
            Assert.IsNotNull(profile);
            Assert.AreEqual(employer.Id, profile.UserId);
        }

        [TestMethod]
        public void TestNoLinkedInProfileLoginIdValidation()
        {
            var employer = CreateEmployer(0);

            LogIn(employer);
            Get(_settingsUrl);
            AssertNoLinkedInProfile(employer.GetLoginId());

            // Try to remove the login id.

            _loginIdTextBox.Text = "";
            _saveButton.Click();

            AssertUrl(_settingsUrl);
            AssertNoLinkedInProfile("");
            AssertErrorMessage("The username is required.");
        }

        [TestMethod]
        public void TestNoLoginIdLinkedInProfile()
        {
            var employer = CreateEmployer(LinkedInId);

            // Log in using LinkedIn.

            Get(EmployerLogInUrl);
            var url = LinkedInApiLogIn(AuthenticationStatus.Authenticated, new LinkedInApiProfile { Id = LinkedInId });
            Get(url);
            Get(_settingsUrl);
            AssertUrl(_settingsUrl);

            AssertLinkedInProfile("", true, false);

            // Try to update.

            _saveButton.Click();
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);

            // Assert profile still in place.

            var profile = _linkedInQuery.GetProfile(LinkedInId);
            Assert.AreEqual(LinkedInId, profile.Id);
            Assert.AreEqual(employer.Id, profile.UserId);
            profile = _linkedInQuery.GetProfile(employer.Id);
            Assert.AreEqual(LinkedInId, profile.Id);
            Assert.AreEqual(employer.Id, profile.UserId);

            Get(_settingsUrl);
            AssertLinkedInProfile("", true, false);

            // Add some credentials.

            _loginIdTextBox.Text = LoginId;
            _passwordTextBox.Text = Password;
            _confirmPasswordTextBox.Text = Password;
            _saveButton.Click();
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);

            Get(_settingsUrl);
            AssertLinkedInProfile(LoginId, false, true);

            // Check can log in with those credentials.

            LogOut();
            AssertUrl(EmployerHomeUrl);
            SubmitLogIn(LoginId, Password);
            AssertUrl(LoggedInEmployerHomeUrl);

            Get(_settingsUrl);
            AssertLinkedInProfile(LoginId, false, true);
        }

        [TestMethod]
        public void TestNoLoginIdLinkedInProfileValidation()
        {
            CreateEmployer(LinkedInId);

            // Log in using LinkedIn.

            Get(EmployerLogInUrl);
            var url = LinkedInApiLogIn(AuthenticationStatus.Authenticated, new LinkedInApiProfile { Id = LinkedInId });
            Get(url);
            Get(_settingsUrl);
            AssertUrl(_settingsUrl);

            AssertLinkedInProfile("", true, false);

            // No passwords

            _loginIdTextBox.Text = LoginId;
            _passwordTextBox.Text = "";
            _confirmPasswordTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(_settingsUrl);
            AssertErrorMessages("The password is required.", "The confirm password is required.");

            // Not matching passwords.

            _loginIdTextBox.Text = LoginId;
            _passwordTextBox.Text = Password;
            _confirmPasswordTextBox.Text = Password + "a";
            _saveButton.Click();
            AssertUrl(_settingsUrl);
            AssertErrorMessages("The confirm password and password must match.");

            // Duplicate login id.

            var employer1 = CreateEmployer(1);

            _loginIdTextBox.Text = employer1.GetLoginId();
            _passwordTextBox.Text = Password;
            _confirmPasswordTextBox.Text = Password;
            _saveButton.Click();
            AssertUrl(_settingsUrl);
            AssertErrorMessages("The username is already being used.");
        }

        private Employer CreateEmployer(int index)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(0));
            employer.JobTitle = "Archeologist";
            _employerAccountsCommand.UpdateEmployer(employer);
            return employer;
        }

        private Employer CreateEmployer(string linkedInId)
        {
            var employer = new Employer
            {
                EmailAddress = new EmailAddress {Address = EmailAddress},
                IsActivated = true,
                IsEnabled = true,
                PhoneNumber = new PhoneNumber {Number = PhoneNumber, Type = PhoneNumberType.Mobile},
                FirstName = FirstName,
                LastName = LastName,
                Organisation = _organisationsCommand.CreateTestOrganisation(0),
                JobTitle = "Archeologist",
                SubRole = EmployerSubRole.Employer,
            };

            var profile = new LinkedInProfile { Id = linkedInId };

            _employerAccountsCommand.CreateEmployer(employer, profile);
            return employer;
        }

        private ReadOnlyUrl LinkedInApiLogIn(AuthenticationStatus expectedStatus, JsonRequestModel profile)
        {
            // Ensure element is on the screen.

            Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div/script[@type='in/Login']"));

            // Extract the urls to redirect to.

            const string find = "window.location = data.Status == \"Authenticated\" ? \"";

            var pos = Browser.CurrentPageText.IndexOf(find);
            Assert.AreNotEqual(-1, pos);
            pos = pos + find.Length;

            var endPos = Browser.CurrentPageText.IndexOf("\"", pos, StringComparison.CurrentCultureIgnoreCase);
            Assert.AreNotEqual(-1, endPos);

            var authenticatedUrl = Browser.CurrentPageText.Substring(pos, endPos - pos);

            pos = endPos + 1;
            endPos = Browser.CurrentPageText.IndexOf("\"", pos, StringComparison.CurrentCultureIgnoreCase);
            Assert.AreNotEqual(-1, endPos);

            pos = endPos + 1;
            endPos = Browser.CurrentPageText.IndexOf("\"", pos, StringComparison.CurrentCultureIgnoreCase);
            Assert.AreNotEqual(-1, endPos);

            var accountUrl = Browser.CurrentPageText.Substring(pos, endPos - pos);

            // Login.

            var response = Deserialize<LinkedInAuthenticationModel>(Post(_apiLoginUrl, JsonContentType, Serialize(profile)));
            AssertJsonSuccess(response);
            var status = (AuthenticationStatus)Enum.Parse(typeof(AuthenticationStatus), response.Status);

            Assert.AreEqual(expectedStatus, status);
            return expectedStatus == AuthenticationStatus.Authenticated
                ? new ReadOnlyApplicationUrl(authenticatedUrl)
                : new ReadOnlyApplicationUrl(accountUrl);
        }

        private void AssertLinkedInProfile(string loginId, bool passwordVisible, bool useLinkedInProfileVisible)
        {
            Assert.IsTrue(_loginIdTextBox.IsVisible);
            Assert.AreEqual(loginId, _loginIdTextBox.Text);

            Assert.AreEqual(passwordVisible, _passwordTextBox.IsVisible);
            Assert.AreEqual(passwordVisible, _confirmPasswordTextBox.IsVisible);

            if (useLinkedInProfileVisible)
            {
                Assert.IsTrue(_useLinkedInProfileCheckBox.IsVisible);
                Assert.IsTrue(_useLinkedInProfileCheckBox.IsChecked);
            }
            else
            {
                Assert.IsFalse(_useLinkedInProfileCheckBox.IsVisible);
            }

            Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//script[@type='in/Login']"));
        }

        private void AssertNoLinkedInProfile(string loginId)
        {
            Assert.IsTrue(_loginIdTextBox.IsVisible);
            Assert.AreEqual(loginId, _loginIdTextBox.Text);

            Assert.IsFalse(_passwordTextBox.IsVisible);
            Assert.IsFalse(_confirmPasswordTextBox.IsVisible);

            Assert.IsFalse(_useLinkedInProfileCheckBox.IsVisible);
            Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//script[@type='in/Login']"));
        }
	}
}

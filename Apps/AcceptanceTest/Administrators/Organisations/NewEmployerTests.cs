using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Organisations
{
    [TestClass]
    public class NewEmployerTests
        : OrganisationsTests
    {
        private const string LoginIdFormat = "employer{0}";
        private const string Password = "abcdef";
        private const string NewPassword = "ghijkl";
        private const string EmailAddressFormat = "employer{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Homer{0}";
        private const string LastNameFormat = "Simpson{0}";
        private const string PhoneNumberFormat = "9999999{0}";
        private const string JobTitleFormat = "Schmoh{0}";

        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly IRecruitersQuery _recruitersQuery = Resolve<IRecruitersQuery>();

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlTextBoxTester _jobTitleTextBox;
        private HtmlDropDownListTester _industryIdDropDownList;

        private HtmlButtonTester _createButton;
        private HtmlButtonTester _cancelButton;

        private HtmlPasswordTester _passwordTextBox;
        private HtmlPasswordTester _newPasswordTextBox;
        private HtmlPasswordTester _confirmNewPasswordTextBox;
        private HtmlButtonTester _saveButton;

        private ReadOnlyUrl _mustChangePasswordUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _jobTitleTextBox = new HtmlTextBoxTester(Browser, "JobTitle");
            _industryIdDropDownList = new HtmlDropDownListTester(Browser, "IndustryId");

            _createButton = new HtmlButtonTester(Browser, "create");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");

            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _newPasswordTextBox = new HtmlPasswordTester(Browser, "NewPassword");
            _confirmNewPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmNewPassword");
            _saveButton = new HtmlButtonTester(Browser, "save");

            _mustChangePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/mustchangepassword");

            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestUnverifiedOrganisation()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestOrganisation(1);

            // Get the page.

            LogIn(administrator);
            var url = GetNewEmployerUrl(organisation);
            Get(url);

            // Should be an error message.

            AssertErrorMessage("Cannot create logins for unverified organisations.");
            Assert.IsFalse(_createButton.IsVisible);
            Assert.IsTrue(_cancelButton.IsVisible);
        }

        [TestMethod]
        public void TestCancel()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            // Get the page.

            LogIn(administrator);
            var url = GetNewEmployerUrl(organisation);
            Get(url);

            // Should be an error message.

            Assert.IsTrue(_createButton.IsVisible);
            Assert.IsTrue(_cancelButton.IsVisible);
            _cancelButton.Click();

            AssertUrl(GetEmployersUrl(organisation));
        }

        [TestMethod]
        public void TestErrors()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            // Get the page.

            LogIn(administrator);
            var url = GetNewEmployerUrl(organisation);
            Get(url);

            Assert.AreEqual(string.Empty, _loginIdTextBox.Text);
            Assert.AreEqual(string.Empty, _passwordTextBox.Text);
            Assert.AreEqual(string.Empty, _emailAddressTextBox.Text);
            Assert.AreEqual(string.Empty, _firstNameTextBox.Text);
            Assert.AreEqual(string.Empty, _lastNameTextBox.Text);
            Assert.AreEqual(string.Empty, _phoneNumberTextBox.Text);
            Assert.AreEqual(string.Empty, _jobTitleTextBox.Text);
            Assert.AreEqual(string.Empty, _industryIdDropDownList.SelectedItem.Text);

            // Nothing filled in.

            _createButton.Click();

            AssertErrorMessages(
                "The username is required.",
                "The password is required.",
                "The email address is required.",
                "The first name is required.",
                "The last name is required.",
                "The phone number is required.");

            // Bad values.

            _loginIdTextBox.Text = new string('a', 350);
            _passwordTextBox.Text = "a";
            _emailAddressTextBox.Text = "sdffsdf";
            _firstNameTextBox.Text = "%$^&^*()";
            _lastNameTextBox.Text = "%$&$*&(&";
            _phoneNumberTextBox.Text = "%$&*()&*()";
            _createButton.Click();

            AssertErrorMessages(
                "The username must be no more than 320 characters in length.",
                "The password must be between 6 and 50 characters in length.",
                "The email address must be valid and have less than 320 characters.",
                "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestNewEmployers()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            // Get the page.

            LogIn(administrator);
            var url = GetNewEmployerUrl(organisation);
            Get(url);

            var employer1 = CreateLogin(1);
            AssertLogins(organisation, employer1);
            AssertEmail(employer1, Password);

            Get(url);
            var employer2 = CreateLogin(2);
            AssertLogins(organisation, employer1, employer2);
            AssertEmail(employer2, Password);

            Get(url);
            var employer3 = CreateLogin(3);
            AssertLogins(organisation, employer1, employer2, employer3);
            AssertEmail(employer3, Password);
        }

        [TestMethod]
        public void TestNewEmployerChangePassword()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            // Get the page.

            LogIn(administrator);
            var url = GetNewEmployerUrl(organisation);
            Get(url);

            var employer = CreateLogin(1);
            AssertLogins(organisation, employer);
            AssertEmail(employer, Password);

            // Login as that employer.

            LogOut();
            LogIn(employer, Password);

            // Should need to change their password.

            AssertUrlWithoutQuery(_mustChangePasswordUrl);
            _passwordTextBox.Text = Password;
            _newPasswordTextBox.Text = NewPassword;
            _confirmNewPasswordTextBox.Text = NewPassword;
            _saveButton.Click();
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);

            // Check the user login.

            LogOut();
            LogIn(employer, Password);
            AssertNotLoggedIn();

            LogIn(employer, NewPassword);
            AssertUrl(LoggedInEmployerHomeUrl);
        }

        [TestMethod]
        public void TestExistingLoginId()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation1 = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var organisation2 = _organisationsCommand.CreateTestVerifiedOrganisation(2, null, administrator.Id);

            // Get the page.

            LogIn(administrator);
            var url = GetNewEmployerUrl(organisation1);
            Get(url);

            var employer1 = CreateLogin(1);
            AssertLogins(organisation1, employer1);

            // Create another with same login id.

            url = GetNewEmployerUrl(organisation2);
            Get(url);

            const int index = 2;
            _loginIdTextBox.Text = employer1.GetLoginId();
            _passwordTextBox.Text = Password;
            _emailAddressTextBox.Text = string.Format(EmailAddressFormat, index);
            _firstNameTextBox.Text = string.Format(FirstNameFormat, index);
            _lastNameTextBox.Text = string.Format(LastNameFormat, index);
            _phoneNumberTextBox.Text = string.Format(PhoneNumberFormat, index);
            _jobTitleTextBox.Text = string.Format(JobTitleFormat, index);
            _industryIdDropDownList.SelectedIndex = index + 1;
            _createButton.Click();

            AssertErrorMessage("The username is already being used.");

            // Need to ensure that the user has not been created.

            AssertLogins(organisation1, employer1);
            AssertLogins(organisation2);
        }

        private void AssertEmail(Employer employer, string password)
        {
            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertHtmlViewContains("Your login ID: " + employer.GetLoginId());
            email.AssertHtmlViewContains("Your password: " + password);
        }

        private void AssertLogins(IOrganisation organisation, params Employer[] expectedEmployers)
        {
            AssertSavedLogins(organisation, expectedEmployers);
            AssertPageLogins(organisation, expectedEmployers);
        }

        private void AssertSavedLogins(IOrganisation organisation, ICollection<Employer> expectedEmployers)
        {
            var recruiters = _recruitersQuery.GetRecruiters(organisation.Id);
            Assert.AreEqual(expectedEmployers.Count, recruiters.Count);

            foreach (var expectedEmployer in expectedEmployers)
            {
                var id = _loginCredentialsQuery.GetUserId(expectedEmployer.GetLoginId());
                var employer = _employersQuery.GetEmployer(id.Value);
                Assert.IsNotNull(employer);

                // Check one of the recrutiers for the organisation.

                Assert.AreEqual(true, (from r in recruiters where r == employer.Id select r).Any());

                Assert.AreEqual(expectedEmployer.EmailAddress, employer.EmailAddress);
                Assert.AreEqual(expectedEmployer.FirstName, employer.FirstName);
                Assert.AreEqual(expectedEmployer.LastName, employer.LastName);
                Assert.AreEqual(expectedEmployer.PhoneNumber, employer.PhoneNumber);
                Assert.AreEqual(expectedEmployer.JobTitle, employer.JobTitle);
                Assert.AreEqual(expectedEmployer.SubRole, employer.SubRole);

                Assert.AreEqual(1, employer.Industries.Count);
                Assert.AreEqual(expectedEmployer.Industries[0].Id, employer.Industries[0].Id);
            }
        }

        private void AssertPageLogins(IOrganisation organisation, IList<Employer> expectedEmployers)
        {
            // Go to the login page.

            Get(GetEmployersUrl(organisation));

            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@id='search-results']/tbody/tr");
            Assert.AreEqual(expectedEmployers.Count, trNodes == null ? 0 : trNodes.Count);

            if (trNodes != null)
            {
                for (var index = 0; index < trNodes.Count; ++index)
                {
                    var expectedEmployer = expectedEmployers[index];
                    var trNode = trNodes[index];
                    Assert.AreEqual(expectedEmployer.FullName, trNode.SelectSingleNode("td[position()=1]/a").InnerText);

                    var id = _loginCredentialsQuery.GetUserId(expectedEmployer.GetLoginId());
                    var employer = _employersQuery.GetEmployer(id.Value);
                    Assert.AreEqual(GetEmployerUrl(employer).PathAndQuery.ToLower(), trNode.SelectSingleNode("td[position()=1]/a").Attributes["href"].Value.ToLower());
                }
            }
        }

        private Employer CreateLogin(int index)
        {
            var firstName = string.Format(FirstNameFormat, index);
            var lastName = string.Format(LastNameFormat, index);

            _loginIdTextBox.Text = string.Format(LoginIdFormat, index);
            _passwordTextBox.Text = Password;
            _emailAddressTextBox.Text = string.Format(EmailAddressFormat, index);
            _firstNameTextBox.Text = firstName;
            _lastNameTextBox.Text = lastName;
            _phoneNumberTextBox.Text = string.Format(PhoneNumberFormat, index);
            _jobTitleTextBox.Text = string.Format(JobTitleFormat, index);
            _industryIdDropDownList.SelectedIndex = index + 1;
            _createButton.Click();

            AssertConfirmationMessage("The account for " + firstName + " " + lastName + " has been created.");

            return new Employer
            {
                EmailAddress = new EmailAddress { Address = string.Format(EmailAddressFormat, index), IsVerified = true },
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = new PhoneNumber { Number = string.Format(PhoneNumberFormat, index), Type = PhoneNumberType.Work },
                JobTitle = string.Format(JobTitleFormat, index),
                SubRole = EmployerSubRole.Recruiter,
                Industries = new List<Industry> { _industriesQuery.GetIndustryByAnyName(_industryIdDropDownList.Items[index + 1].Text) }
            };
        }
    }
}

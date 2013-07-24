using System;
using System.Net.Mime;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers
{
    [TestClass]
    public class EditEmployerTests
        : EmployersTests
    {
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private const string NewPassword = "newPassword";
        private const string NewNewPassword = "newNewPassword";
        private const string NewFirstName = "Monty";
        private const string NewLastName = "Burns";
        private const string NewEmailAddress = "mburns@test.linkme.net.au";
        private const string NewPhoneNumber = "7132926444";
        private const string NewLoginId = "newlogin";

        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlTextBoxTester _organisationNameTextBox;

        private HtmlTextBoxTester _isEnabledTextBox;
        private HtmlButtonTester _enableButton;
        private HtmlButtonTester _disableButton;

        private HtmlTextBoxTester _changePasswordTextBox;
        private HtmlCheckBoxTester _sendPasswordEmailCheckBox;
        private HtmlButtonTester _changeButton;

        private HtmlPasswordTester _passwordTextBox;
        private HtmlPasswordTester _newPasswordTextBox;
        private HtmlPasswordTester _confirmNewPasswordTextBox;
        private HtmlButtonTester _saveButton;

        private ReadOnlyUrl _mustChangePasswordUrl;
        private ReadOnlyUrl _contactUsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();

            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _organisationNameTextBox = new HtmlTextBoxTester(Browser, "OrganisationName");

            _isEnabledTextBox = new HtmlTextBoxTester(Browser, "IsEnabled");
            _enableButton = new HtmlButtonTester(Browser, "enable");
            _disableButton = new HtmlButtonTester(Browser, "disable");

            _changePasswordTextBox = new HtmlTextBoxTester(Browser, "Password");
            _sendPasswordEmailCheckBox = new HtmlCheckBoxTester(Browser, "SendPasswordEmail");
            _changeButton = new HtmlButtonTester(Browser, "change");

            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _newPasswordTextBox = new HtmlPasswordTester(Browser, "NewPassword");
            _confirmNewPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmNewPassword");
            _saveButton = new HtmlButtonTester(Browser, "save");

            _mustChangePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/mustchangepassword");
            _contactUsUrl = new ReadOnlyApplicationUrl("~/faqs/accessing-and-editing-your-profile/what-does-it-mean-that-my-account-is-disabled/7B7FAD42-E027-4586-843B-4D422F39E7EA");
        }

        [TestMethod]
        public void TestPageVisibility()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            // Assert that everything is as it should be.

            AssertVisibility(employer);
            AssertEmployer(employer, employer.GetLoginId());
        }

        [TestMethod]
        public void TestFirstName()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            // Check errors.

            _firstNameTextBox.Text = string.Empty;
            _saveButton.Click();
            AssertErrorMessage("The first name is required.");

            _firstNameTextBox.Text = new string('a', 200);
            _saveButton.Click();
            AssertErrorMessage("The first name must be between 2 and 30 characters in length and not have any invalid characters.");

            // Check update.

            _firstNameTextBox.Text = NewFirstName;
            _saveButton.Click();

            employer.FirstName = NewFirstName;
            AssertEmployer(employer, employer.GetLoginId());
            AssertEmployer(employer, employer.GetLoginId(), _employersQuery.GetEmployer(employer.Id), employer.GetLoginId());
        }

        [TestMethod]
        public void TestLastName()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            // Check errors.

            _lastNameTextBox.Text = string.Empty;
            _saveButton.Click();
            AssertErrorMessage("The last name is required.");

            _lastNameTextBox.Text = new string('a', 200);
            _saveButton.Click();
            AssertErrorMessage("The last name must be between 2 and 30 characters in length and not have any invalid characters.");

            // Check update.

            _lastNameTextBox.Text = NewLastName;
            _saveButton.Click();

            employer.LastName = NewLastName;
            AssertEmployer(employer, employer.GetLoginId());
            AssertEmployer(employer, employer.GetLoginId(), _employersQuery.GetEmployer(employer.Id), employer.GetLoginId());
        }

        [TestMethod]
        public void TestEmailAddress()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            // Check errors.

            _emailAddressTextBox.Text = string.Empty;
            _saveButton.Click();
            AssertErrorMessage("The email address is required.");

            // Check update.

            _emailAddressTextBox.Text = NewEmailAddress;
            _saveButton.Click();

            var loginId = employer.GetLoginId();
            employer.EmailAddress = new EmailAddress { Address = NewEmailAddress, IsVerified = true };
            AssertEmployer(employer, loginId);
            AssertEmployer(employer, loginId, _employersQuery.GetEmployer(employer.Id), loginId);
        }

        [TestMethod]
        public void TestPhoneNumber()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            // Check errors.

            _phoneNumberTextBox.Text = string.Empty;
            _saveButton.Click();
            AssertErrorMessage("The phone number is required.");

            _phoneNumberTextBox.Text = new string('a', 200);
            _saveButton.Click();
            AssertErrorMessage("The phone number must be between 8 and 20 characters in length and not have any invalid characters.");

            // Check update.

            _phoneNumberTextBox.Text = NewPhoneNumber;
            _saveButton.Click();

            employer.PhoneNumber = new PhoneNumber { Number = NewPhoneNumber, Type = PhoneNumberType.Work };
            AssertEmployer(employer, employer.GetLoginId());
            AssertEmployer(employer, employer.GetLoginId(), _employersQuery.GetEmployer(employer.Id), employer.GetLoginId());
        }

        [TestMethod]
        public void TestLoginId()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            // Check errors.

            _loginIdTextBox.Text = string.Empty;
            _saveButton.Click();
            AssertErrorMessage("The username is required.");

            _loginIdTextBox.Text = new string('a', 400);
            _saveButton.Click();
            AssertErrorMessage("The username must be no more than 320 characters in length.");

            // Check update.

            _loginIdTextBox.Text = NewLoginId;
            _saveButton.Click();

            AssertEmployer(employer, NewLoginId);
            AssertEmployer(employer, NewLoginId, _employersQuery.GetEmployer(employer.Id), _loginCredentialsQuery.GetLoginId(employer.Id));
        }

        [TestMethod]
        public void TestVerifiedOrganisationName()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation1 = _organisationsCommand.CreateTestVerifiedOrganisation(1);
            var organisation2 = _organisationsCommand.CreateTestVerifiedOrganisation(2);
            var organisation3 = _organisationsCommand.CreateTestVerifiedOrganisation(3, organisation2, Guid.NewGuid());

            var employer = _employerAccountsCommand.CreateTestEmployer(1, organisation1);

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            Assert.IsTrue(_organisationNameTextBox.IsVisible);

            // Check errors.
            
            _organisationNameTextBox.Text = string.Empty;
            _saveButton.Click();
            AssertErrorMessage("The organisation is required.");

            _organisationNameTextBox.Text = new string('a', 6);
            _saveButton.Click();
            AssertErrorMessage("The organisation is required.");

            // Check update.

            _organisationNameTextBox.Text = organisation2.FullName;
            _saveButton.Click();

            employer.Organisation = organisation2;
            AssertEmployer(employer, employer.GetLoginId());
            AssertEmployer(employer, employer.GetLoginId(), _employersQuery.GetEmployer(employer.Id), employer.GetLoginId());

            // Update again.

            _organisationNameTextBox.Text = organisation3.FullName;
            _saveButton.Click();

            employer.Organisation = organisation3;
            AssertEmployer(employer, employer.GetLoginId());
            AssertEmployer(employer, employer.GetLoginId(), _employersQuery.GetEmployer(employer.Id), employer.GetLoginId());
        }

        [TestMethod]
        public void TestUnverifiedOrganisationName()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            Assert.IsFalse(_organisationNameTextBox.IsVisible);
        }

        [TestMethod]
        public void TestDisable()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            // Disable the employer.

            _disableButton.Click();

            // Check the user details.

            employer.IsEnabled = false;
            AssertVisibility(employer);
            AssertEmployer(employer, employer.GetLoginId());

            // Check the user login.

            LogOut();
            LogIn(employer);
            AssertPath(_contactUsUrl);
        }

        [TestMethod]
        public void TestEnable()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            employer.IsEnabled = false;
            _employerAccountsCommand.UpdateEmployer(employer);

            LogIn(employer);
            AssertPath(_contactUsUrl);

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            // Enable the User.

            _enableButton.Click();

            // Check the user details.

            employer.IsEnabled = true;
            AssertVisibility(employer);
            AssertEmployer(employer, employer.GetLoginId());

            // Check the user login.

            LogOut();
            LogIn(employer);
            AssertUrl(LoggedInEmployerHomeUrl);
        }

        [TestMethod]
        public void TestChangePassword()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            // Change the password.

            _changePasswordTextBox.Text = NewPassword;
            _changeButton.Click();
            AssertConfirmationMessage("The password has been reset.");

            // Check the user details.

            AssertVisibility(employer);
            AssertEmployer(employer, employer.GetLoginId());
            _emailServer.AssertNoEmailSent();

            // Check the user login.

            LogOut();
            LogIn(employer);
            AssertNotLoggedIn();

            LogIn(employer, NewPassword);
            AssertUrlWithoutQuery(_mustChangePasswordUrl);

            // Change password.

            _passwordTextBox.Text = NewPassword;
            _newPasswordTextBox.Text = NewNewPassword;
            _confirmNewPasswordTextBox.Text = NewNewPassword;
            _saveButton.Click();
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);

            // Check the user login.

            LogOut();
            LogIn(employer);
            AssertNotLoggedIn();

            LogIn(employer, NewPassword);
            AssertNotLoggedIn();

            LogIn(employer, NewNewPassword);
            AssertUrl(LoggedInEmployerHomeUrl);
        }

        [TestMethod]
        public void TestChangePasswordAndSendEmail()
        {
            // Create the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            LogIn(administrator);
            Get(GetEmployerUrl(employer));

            _changePasswordTextBox.Text = NewPassword;
            _sendPasswordEmailCheckBox.IsChecked = true;
            _changeButton.Click();
            AssertConfirmationMessage("The password has been reset and an email has been sent.");

            AssertVisibility(employer);
            AssertEmployer(employer, employer.GetLoginId());

            var email = _emailServer.AssertEmailSent();
            email.AssertViewContains(MediaTypeNames.Text.Html, employer.GetLoginId());
            email.AssertViewContains(MediaTypeNames.Text.Html, NewPassword);

            // Check the user login.

            LogOut();
            LogIn(employer);
            AssertNotLoggedIn();

            LogIn(employer, NewPassword);
            AssertUrlWithoutQuery(_mustChangePasswordUrl);

            // Change password.

            _passwordTextBox.Text = NewPassword;
            _newPasswordTextBox.Text = NewNewPassword;
            _confirmNewPasswordTextBox.Text = NewNewPassword;
            _saveButton.Click();
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);

            // Check the user login.

            LogOut();
            LogIn(employer);
            AssertNotLoggedIn();

            LogIn(employer, NewPassword);
            AssertNotLoggedIn();

            LogIn(employer, NewNewPassword);
            AssertUrl(LoggedInEmployerHomeUrl);
        }

        private void AssertEmployer(IEmployer employer, string loginId)
        {
            Assert.AreEqual(employer.FirstName, _firstNameTextBox.Text);
            Assert.AreEqual(employer.LastName, _lastNameTextBox.Text);
            Assert.AreEqual(employer.EmailAddress.Address, _emailAddressTextBox.Text);
            Assert.AreEqual(employer.PhoneNumber.Number, _phoneNumberTextBox.Text);
            Assert.AreEqual(loginId, _loginIdTextBox.Text);
            Assert.AreEqual(_isEnabledTextBox.Text, employer.IsEnabled ? "Yes" : "No");

            if (employer.Organisation.IsVerified)
                Assert.AreEqual(employer.Organisation.FullName, _organisationNameTextBox.Text);
            else
                Assert.IsFalse(_organisationNameTextBox.IsVisible);
        }

        private static void AssertEmployer(IEmployer expectedEmployer, string expectedLoginId, IEmployer employer, string loginId)
        {
            Assert.AreEqual(expectedEmployer.FirstName, employer.FirstName);
            Assert.AreEqual(expectedEmployer.LastName, employer.LastName);
            Assert.AreEqual(expectedEmployer.EmailAddress, employer.EmailAddress);
            Assert.AreEqual(expectedEmployer.PhoneNumber, employer.PhoneNumber);
            Assert.AreEqual(expectedLoginId, loginId);
            Assert.AreEqual(expectedEmployer.IsEnabled, employer.IsEnabled);
            Assert.AreEqual(expectedEmployer.Organisation.Id, employer.Organisation.Id);
        }

        private void AssertVisibility(IUserAccount employer)
        {
            Assert.IsTrue(_firstNameTextBox.IsVisible);
            Assert.IsTrue(_lastNameTextBox.IsVisible);
            Assert.IsTrue(_emailAddressTextBox.IsVisible);
            Assert.IsTrue(_phoneNumberTextBox.IsVisible);

            Assert.IsTrue(_loginIdTextBox.IsVisible);
            Assert.IsTrue(_isEnabledTextBox.IsVisible);

            Assert.AreEqual(_enableButton.IsVisible, !employer.IsEnabled);
            Assert.AreEqual(_disableButton.IsVisible, employer.IsEnabled);

            Assert.IsTrue(_changePasswordTextBox.IsVisible);
            Assert.IsTrue(_changeButton.IsVisible);
            Assert.IsTrue(_sendPasswordEmailCheckBox.IsVisible);
        }
    }
}
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Administrators
{
    [TestClass]
    public class EditAdministratorTests
        : AdministratorsTests
    {
        private const string BadPassword = "newPassword";
        private const string GoodPassword = "newPassword3";

        private HtmlTextBoxTester _fullNameTextBox;

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlTextBoxTester _isEnabledTextBox;

        private HtmlButtonTester _enableButton;
        private HtmlButtonTester _disableButton;

        private HtmlPasswordTester _passwordTextBox;
        private HtmlButtonTester _changeButton;

        private ReadOnlyUrl _contactUsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _fullNameTextBox = new HtmlTextBoxTester(Browser, "FullName");

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _isEnabledTextBox = new HtmlTextBoxTester(Browser, "IsEnabled");
            _enableButton = new HtmlButtonTester(Browser, "enable");
            _disableButton = new HtmlButtonTester(Browser, "disable");

            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _changeButton = new HtmlButtonTester(Browser, "change");

            _contactUsUrl = new ReadOnlyApplicationUrl("~/faqs/accessing-and-editing-your-profile/what-does-it-mean-that-my-account-is-disabled/7B7FAD42-E027-4586-843B-4D422F39E7EA");
        }

        [TestMethod]
        public void TestPageVisibility()
        {
            // Create the administrator.

            var administrator1 = _administratorAccountsCommand.CreateTestAdministrator(1);
            var administrator2 = _administratorAccountsCommand.CreateTestAdministrator(2);

            LogIn(administrator1);
            Get(GetAdministratorUrl(administrator2));

            // Assert that everything is as it should be.

            AssertVisibility(administrator2);
            AssertAdministrator(administrator2);
        }

        [TestMethod]
        public void TestDisable()
        {
            // Create the administrator.

            var administrator1 = _administratorAccountsCommand.CreateTestAdministrator(1);
            var administrator2 = _administratorAccountsCommand.CreateTestAdministrator(2);

            LogIn(administrator1);
            Get(GetAdministratorUrl(administrator2));

            // Disable the administrator.

            _disableButton.Click();

            // Check the user details.

            administrator2.IsEnabled = false;
            AssertVisibility(administrator2);
            AssertAdministrator(administrator2);

            // Check the user login.

            LogOut();
            LogIn(administrator2);
            AssertPath(_contactUsUrl);
        }

        [TestMethod]
        public void TestEnable()
        {
            // Create the administrator.

            var administrator1 = _administratorAccountsCommand.CreateTestAdministrator(1);
            var administrator2 = _administratorAccountsCommand.CreateTestAdministrator(2);

            administrator2.IsEnabled = false;
            _administratorAccountsCommand.UpdateAdministrator(administrator2);

            LogIn(administrator2);
            AssertPath(_contactUsUrl);

            LogIn(administrator1);
            Get(GetAdministratorUrl(administrator2));

            // Enable the User.

            _enableButton.Click();

            // Check the user details.

            administrator2.IsEnabled = true;
            AssertVisibility(administrator2);
            AssertAdministrator(administrator2);

            // Check the user login.

            LogOut();
            LogIn(administrator2);
            AssertUrl(LoggedInAdministratorHomeUrl);
        }

        [TestMethod]
        public void TestChangeBadPassword()
        {
            // Create the administrator.

            var administrator1 = _administratorAccountsCommand.CreateTestAdministrator(1);
            var administrator2 = _administratorAccountsCommand.CreateTestAdministrator(2);

            LogIn(administrator1);
            Get(GetAdministratorUrl(administrator2));

            // Change the password.

            _passwordTextBox.Text = BadPassword;
            _changeButton.Click();
            AssertErrorMessage("The password must be between 8 and 50 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestChangePassword()
        {
            // Create the administrator.

            var administrator1 = _administratorAccountsCommand.CreateTestAdministrator(1);
            var administrator2 = _administratorAccountsCommand.CreateTestAdministrator(2);

            LogIn(administrator1);
            Get(GetAdministratorUrl(administrator2));

            // Change the password.

            _passwordTextBox.Text = GoodPassword;
            _changeButton.Click();
            AssertConfirmationMessage("The password has been reset.");

            // Check the user details.

            AssertVisibility(administrator2);
            AssertAdministrator(administrator2);

            // Check the user login.

            LogOut();
            LogIn(administrator2);
            AssertNotLoggedIn();

            LogIn(administrator2, GoodPassword);
            AssertUrl(LoggedInAdministratorHomeUrl);
        }

        private void AssertAdministrator(Administrator administrator)
        {
            Assert.AreEqual(administrator.FullName, _fullNameTextBox.Text);
            AssertPageContains(administrator.EmailAddress.Address);

            Assert.AreEqual(administrator.GetLoginId(), _loginIdTextBox.Text);
            Assert.AreEqual(_isEnabledTextBox.Text, administrator.IsEnabled ? "Yes" : "No");
        }

        private void AssertVisibility(IUserAccount administrator)
        {
            Assert.IsTrue(_fullNameTextBox.IsVisible);

            Assert.IsTrue(_loginIdTextBox.IsVisible);
            Assert.IsTrue(_isEnabledTextBox.IsVisible);

            Assert.AreEqual(!administrator.IsEnabled, _enableButton.IsVisible);
            Assert.AreEqual(administrator.IsEnabled, _disableButton.IsVisible);

            Assert.IsTrue(_passwordTextBox.IsVisible);
            Assert.IsTrue(_changeButton.IsVisible);
        }
    }
}
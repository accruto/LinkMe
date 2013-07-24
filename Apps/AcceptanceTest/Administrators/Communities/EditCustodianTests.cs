using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communities
{
    [TestClass]
    public class EditCustodianTests
        : CommunitiesTests
    {
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();

        private const string NewPassword = "newPassword";

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
            // Create the custodian.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            var community = CreateCommunity();
            var custodian = _custodianAccountsCommand.CreateTestCustodian(2, community.Id);

            LogIn(administrator);
            Get(GetCustodianUrl(custodian));

            // Assert that everything is as it should be.

            AssertVisibility(custodian);
            AssertCustodian(custodian);
        }

        [TestMethod]
        public void TestDisable()
        {
            // Create the custodian.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            var community = CreateCommunity();
            var custodian = _custodianAccountsCommand.CreateTestCustodian(2, community.Id);

            LogIn(administrator);
            Get(GetCustodianUrl(custodian));

            // Disable the custodian.

            _disableButton.Click();

            // Check the user details.

            custodian.IsEnabled = false;
            AssertVisibility(custodian);
            AssertCustodian(custodian);

            // Check the user login.

            LogOut();
            LogIn(custodian);
            AssertPath(_contactUsUrl);
        }

        [TestMethod]
        public void TestEnable()
        {
            // Create the custodian.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            var community = CreateCommunity();
            var custodian = _custodianAccountsCommand.CreateTestCustodian(2, community.Id);

            custodian.IsEnabled = false;
            _custodianAccountsCommand.UpdateCustodian(custodian);

            LogIn(custodian);
            AssertPath(_contactUsUrl);

            LogIn(administrator);
            Get(GetCustodianUrl(custodian));

            // Enable the User.

            _enableButton.Click();

            // Check the user details.

            custodian.IsEnabled = true;
            AssertVisibility(custodian);
            AssertCustodian(custodian);

            // Check the user login.

            LogOut();
            LogIn(custodian);
            AssertUrl(LoggedInCustodianHomeUrl);
        }

        [TestMethod]
        public void TestChangePassword()
        {
            // Create the custodian.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            var community = CreateCommunity();
            var custodian = _custodianAccountsCommand.CreateTestCustodian(2, community.Id);
            
            LogIn(administrator);
            Get(GetCustodianUrl(custodian));

            // Change the password.

            _passwordTextBox.Text = NewPassword;
            _changeButton.Click();
            AssertConfirmationMessage("The password has been reset.");

            // Check the user details.

            AssertVisibility(custodian);
            AssertCustodian(custodian);

            // Check the user login.

            LogOut();
            LogIn(custodian);
            AssertNotLoggedIn();

            LogIn(custodian, NewPassword);
            AssertUrl(LoggedInCustodianHomeUrl);
        }

        private void AssertCustodian(Custodian custodian)
        {
            Assert.AreEqual(custodian.FullName, _fullNameTextBox.Text);
            AssertPageContains(custodian.EmailAddress.Address);

            Assert.AreEqual(custodian.GetLoginId(), _loginIdTextBox.Text);
            Assert.AreEqual(_isEnabledTextBox.Text, custodian.IsEnabled ? "Yes" : "No");
        }

        private void AssertVisibility(IUserAccount custodian)
        {
            Assert.IsTrue(_fullNameTextBox.IsVisible);

            Assert.IsTrue(_loginIdTextBox.IsVisible);
            Assert.IsTrue(_isEnabledTextBox.IsVisible);

            Assert.AreEqual(_enableButton.IsVisible, !custodian.IsEnabled);
            Assert.AreEqual(_disableButton.IsVisible, custodian.IsEnabled);

            Assert.IsTrue(_passwordTextBox.IsVisible);
            Assert.IsTrue(_changeButton.IsVisible);
        }
    }
}
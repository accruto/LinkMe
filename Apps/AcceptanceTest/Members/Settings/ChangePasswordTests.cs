using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.WebTester.AspTester;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using NUnit.Framework;

namespace LinkMe.AcceptanceTest.Members.Settings
{
    [TestFixture]
    public class ChangePasswordTests
        : SettingsTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Container.Current.Resolve<IMemberAccountsCommand>();

        private const string Password = "password";
        private const string NewPassword = "aaaaaaaa";

        private TextBoxTester _currentPasswordTester;
        private TextBoxTester _passwordTester;
        private TextBoxTester _confirmPasswordTester;
        private ButtonTester _saveTester;
        private ButtonTester _cancelTester;

        protected override void SetUp()
        {
            base.SetUp();
            Container.Current.Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _currentPasswordTester = new TextBoxTester("CurrentPassword", CurrentWebForm);
            _passwordTester = new TextBoxTester("Password", CurrentWebForm);
            _confirmPasswordTester = new TextBoxTester("ConfirmPassword", CurrentWebForm);
            _saveTester = new ButtonTester("save", CurrentWebForm);
            _cancelTester = new ButtonTester("cancel", CurrentWebForm);
        }

        [Test]
        public void TestDefaults()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            GetPage(_changePasswordUrl);

            // Check.

            Assert.AreEqual(string.Empty, _currentPasswordTester.Text);
            Assert.AreEqual(string.Empty, _passwordTester.Text);
            Assert.AreEqual(string.Empty, _confirmPasswordTester.Text);
        }

        [Test]
        public void TestErrors()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            GetPage(_changePasswordUrl);

            // Required.

            _saveTester.Click();
            AssertErrorMessages(
                "The current password is required.",
                "The password is required.",
                "The confirm password is required.");

            // Invalid.

            _currentPasswordTester.Text = "aa";
            _passwordTester.Text = "aa";
            _confirmPasswordTester.Text = "aa";
            _saveTester.Click();
            AssertErrorMessages(
                "The current password must be between 6 and 50 characters in length.",
                "The password must be between 6 and 50 characters in length.",
                "The confirm password must be between 6 and 50 characters in length.");

            // Different.

            _currentPasswordTester.Text = "password";
            _passwordTester.Text = "aaaaaaaa";
            _confirmPasswordTester.Text = "bbbbbbbbb";
            _saveTester.Click();
            AssertErrorMessages(
                "The confirm password and password must match.");

            // Wrong current password.

            _currentPasswordTester.Text = "sdfsdfsdfsdfsd";
            _passwordTester.Text = "aaaaaaaa";
            _confirmPasswordTester.Text = "aaaaaaaa";
            _saveTester.Click();
            AssertErrorMessages(
                "Login failed. Please try again.");
        }

        [Test]
        public void TestCancel()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            GetPage(_changePasswordUrl);

            // Cancel.

            _cancelTester.Click();
            AssertUrl(_settingsUrl);
        }

        [Test]
        public void TestChangePassword()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            GetPage(_changePasswordUrl);

            _currentPasswordTester.Text = Password;
            _passwordTester.Text = NewPassword;
            _confirmPasswordTester.Text = NewPassword;
            _saveTester.Click();

            // Check.

            AssertNoErrorMessages();
            AssertConfirmationMessage("Your password has been changed.");

            // Try to login with the old password.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn(member);

            LogIn(member, NewPassword);
            AssertUrl(LoggedInMemberHomeUrl);
        }
    }
}
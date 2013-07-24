using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class MustChangePasswordTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();

        private const string Password = "password";
        private const string NewPassword = "theNewPasswod";
        private const string InvalidPassword = "short";

        private HtmlPasswordTester _newPasswordTextBox;
        private HtmlPasswordTester _confirmNewPasswordTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlButtonTester _saveButton;
        private HtmlButtonTester _cancelButton;

        private ReadOnlyUrl _mustChangePasswordUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _mustChangePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/mustchangepassword");

            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _newPasswordTextBox = new HtmlPasswordTester(Browser, "NewPassword");
            _confirmNewPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmNewPassword");
            _saveButton = new HtmlButtonTester(Browser, "save");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");
        }

        [TestMethod]
        public void TestDefaults()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_mustChangePasswordUrl);

            // Check.

            Assert.AreEqual(string.Empty, _passwordTextBox.Text);
            Assert.AreEqual(string.Empty, _newPasswordTextBox.Text);
            Assert.AreEqual(string.Empty, _confirmNewPasswordTextBox.Text);
        }

        [TestMethod]
        public void TestErrors()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_mustChangePasswordUrl);

            // Required.

            _saveButton.Click();
            AssertErrorMessages(
                "The password is required.",
                "The new password is required.",
                "The confirm new password is required.");

            // Invalid.

            _passwordTextBox.Text = "aa";
            _newPasswordTextBox.Text = "aa";
            _confirmNewPasswordTextBox.Text = "aa";
            _saveButton.Click();
            AssertErrorMessages(
                "The new password must be between 6 and 50 characters in length.");

            // Different.

            _passwordTextBox.Text = "password";
            _newPasswordTextBox.Text = "aaaaaaaa";
            _confirmNewPasswordTextBox.Text = "bbbbbbbbb";
            _saveButton.Click();
            AssertErrorMessages(
                "The confirm password and password must match.");

            // Wrong current password.

            _passwordTextBox.Text = "sdfsdfsdfsdfsd";
            _newPasswordTextBox.Text = "aaaaaaaa";
            _confirmNewPasswordTextBox.Text = "aaaaaaaa";
            _saveButton.Click();
            AssertErrorMessages(
                "Login failed. Please try again.");
        }

        [TestMethod]
        public void TestNoCancel()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_mustChangePasswordUrl);

            // Cancel should not be on this form.

            Assert.IsFalse(_cancelButton.IsVisible);
        }

        [TestMethod]
        public void TestChangePassword()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_mustChangePasswordUrl);

            _passwordTextBox.Text = Password;
            _newPasswordTextBox.Text = NewPassword;
            _confirmNewPasswordTextBox.Text = NewPassword;
            _saveButton.Click();

            // Check.

            AssertNoErrorMessages();
            AssertConfirmationMessage("Your password has been changed.");

            // Try to login with the old password.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            LogIn(member, NewPassword);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestConfirmPasswordRequired()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(_mustChangePasswordUrl);

            _passwordTextBox.Text = member.GetPassword();
            _newPasswordTextBox.Text = NewPassword;
            _confirmNewPasswordTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(_mustChangePasswordUrl);
            AssertErrorMessages("The confirm new password is required.",
                "The confirm password and password must match.");
        }

        [TestMethod]
        public void TestInvalidPassword()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(_mustChangePasswordUrl);

            _passwordTextBox.Text = member.GetPassword();
            _newPasswordTextBox.Text = InvalidPassword;
            _confirmNewPasswordTextBox.Text = InvalidPassword;
            _saveButton.Click();
            AssertUrl(_mustChangePasswordUrl);
            AssertErrorMessages("The new password must be between 6 and 50 characters in length.");
        }

        [TestMethod]
        public void TestSamePasswords()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(_mustChangePasswordUrl);

            _newPasswordTextBox.Text = member.GetPassword();
            _confirmNewPasswordTextBox.Text = member.GetPassword();
            _passwordTextBox.Text = member.GetPassword();
            _saveButton.Click();
            AssertUrl(_mustChangePasswordUrl);
            AssertErrorMessage("The new password needs to be different from its original value.");
        }
    }
}
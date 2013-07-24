using LinkMe.Apps.Agents.Test.Users;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public abstract class ChangePasswordTests
        : WebTestClass
    {
        protected const string Password = "password";
        protected const string NewPassword = "theNewPasswod";
        private const string InvalidPassword = "short";

        protected HtmlPasswordTester _newPasswordTextBox;
        protected HtmlPasswordTester _confirmNewPasswordTextBox;
        protected HtmlPasswordTester _passwordTextBox;
        protected HtmlButtonTester _saveButton;
        private HtmlButtonTester _cancelButton;

        protected ReadOnlyUrl _changePasswordUrl;

        [TestInitialize]
        public void ChangePasswordTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _changePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/changepassword");

            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _newPasswordTextBox = new HtmlPasswordTester(Browser, "NewPassword");
            _confirmNewPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmNewPassword");
            _saveButton = new HtmlButtonTester(Browser, "save");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");
        }

        protected abstract RegisteredUser CreateUser();
        protected abstract ReadOnlyUrl GetHomeUrl();
        protected abstract ReadOnlyUrl GetCancelUrl();

        [TestMethod]
        public void TestDefaults()
        {
            var user = CreateUser();
            LogIn(user);
            Get(_changePasswordUrl);

            // Check.

            Assert.AreEqual(string.Empty, _passwordTextBox.Text);
            Assert.AreEqual(string.Empty, _newPasswordTextBox.Text);
            Assert.AreEqual(string.Empty, _confirmNewPasswordTextBox.Text);

            Assert.IsTrue(_saveButton.IsVisible);
            Assert.IsTrue(_cancelButton.IsVisible);
        }

        [TestMethod]
        public void TestErrors()
        {
            var user = CreateUser();
            LogIn(user);
            Get(_changePasswordUrl);

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
        public void TestChangePassword()
        {
            var user = CreateUser();
            LogIn(user);
            Get(_changePasswordUrl);

            _passwordTextBox.Text = Password;
            _newPasswordTextBox.Text = NewPassword;
            _confirmNewPasswordTextBox.Text = NewPassword;
            _saveButton.Click();

            // Check.

            AssertUrlWithoutQuery(GetHomeUrl());
            AssertNoErrorMessages();
            AssertConfirmationMessage("Your password has been changed.");

            // Try to login with the old password.

            LogOut();
            LogIn(user);
            AssertNotLoggedIn();

            LogIn(user, NewPassword);
            AssertUrl(GetHomeUrl());
        }

        [TestMethod]
        public void TestConfirmPasswordRequired()
        {
            var user = CreateUser();
            LogIn(user);
            Get(_changePasswordUrl);

            _passwordTextBox.Text = user.GetPassword();
            _newPasswordTextBox.Text = NewPassword;
            _confirmNewPasswordTextBox.Text = "";
            _saveButton.Click();
            AssertUrl(_changePasswordUrl);
            AssertErrorMessages("The confirm new password is required.", "The confirm password and password must match.");
        }

        [TestMethod]
        public void TestInvalidPassword()
        {
            var user = CreateUser();
            LogIn(user);
            Get(_changePasswordUrl);

            _passwordTextBox.Text = user.GetPassword();
            _newPasswordTextBox.Text = InvalidPassword;
            _confirmNewPasswordTextBox.Text = InvalidPassword;
            _saveButton.Click();
            AssertUrl(_changePasswordUrl);
            AssertErrorMessage("The new password must be between 6 and 50 characters in length.");
        }

        [TestMethod]
        public void TestSamePasswords()
        {
            var user = CreateUser();
            LogIn(user);
            Get(_changePasswordUrl);

            _newPasswordTextBox.Text = user.GetPassword();
            _confirmNewPasswordTextBox.Text = user.GetPassword();
            _passwordTextBox.Text = user.GetPassword();
            _saveButton.Click();
            AssertUrl(_changePasswordUrl);
            AssertErrorMessage("The new password needs to be different from its original value.");
        }

        [TestMethod]
        public void TestCancel()
        {
            var user = CreateUser();
            LogIn(user);
            Get(_changePasswordUrl);

            _cancelButton.Click();
            AssertUrl(GetCancelUrl());
        }
    }
}
using System.Net.Mime;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Members
{
    [TestClass]
    public class EditMemberTests
        : MembersTests
    {
        private const string NewPassword = "newPassword";
        private const string NewNewPassword = "newNewPassword";

        private ReadOnlyUrl _notActivatedUrl;
        private ReadOnlyUrl _mustChangePasswordUrl;
        private ReadOnlyUrl _contactUsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();

            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
            _mustChangePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/mustchangepassword");
            _contactUsUrl = new ReadOnlyApplicationUrl("~/faqs/accessing-and-editing-your-profile/what-does-it-mean-that-my-account-is-disabled/7B7FAD42-E027-4586-843B-4D422F39E7EA");
        }

        [TestMethod]
        public void TestPageVisibility()
        {
            // Create the member.

            var member = CreateMember(true, true);

            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetMemberUrl(member));

            // Assert.

            AssertMember(member);
        }

        [TestMethod]
        public void TestDisable()
        {
            // Create the member.

            var member = CreateMember(true, true);

            // Log in as the member.

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
            LogOut();

            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetMemberUrl(member));

            // Disable.

            _disableButton.Click();

            // Check the member details.

            member.IsEnabled = false;
            AssertMember(member);

            // Check the member login.

            LogOut();
            LogIn(member);
            AssertPath(_contactUsUrl);
        }

        [TestMethod]
        public void TestEnable()
        {
            // Create the member.

            var member = CreateMember(false, true);

            // Log in as the member.

            LogIn(member);
            AssertPath(_contactUsUrl);
            LogOut();

            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetMemberUrl(member));

            // Enable the member.

            _enableButton.Click();

            // Check the member details.

            member.IsEnabled = true;
            AssertMember(member);

            // Check the user login.

            LogOut();
            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestDeactivate()
        {
            // Create the member.

            var member = CreateMember(true, true);

            // Log in as the member.

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
            LogOut();

            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetMemberUrl(member));

            // Deactivate the member.

            _deactivateButton.Click();

            // Check the member details.

            member.IsActivated = false;
            AssertMember(member);

            // Check the member login.

            LogOut();
            LogIn(member);
            AssertUrl(_notActivatedUrl);
        }

        [TestMethod]
        public void TestActivate()
        {
            // Create the member.

            var member = CreateMember(true, false);

            // Log in as the member.

            LogIn(member);
            AssertUrl(_notActivatedUrl);
            LogOut();

            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetMemberUrl(member));

            // Activate the User.

            _activateButton.Click();

            // Check the member details.

            member.IsActivated = true;
            AssertMember(member);

            // Check the member login.

            LogOut();
            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestChangePassword()
        {
            // Create the member.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var member = _memberAccountsCommand.CreateTestMember(1);

            LogIn(administrator);
            Get(GetMemberUrl(member));

            // Change the password.

            _changePasswordTextBox.Text = NewPassword;
            _changeButton.Click();
            AssertConfirmationMessage("The password has been reset.");

            // Check the user details.

            AssertMember(member);
            _emailServer.AssertNoEmailSent();

            // Check the user login.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            LogIn(member, NewPassword);
            AssertUrlWithoutQuery(_mustChangePasswordUrl);

            // Change password.

            _passwordTextBox.Text = NewPassword;
            _newPasswordTextBox.Text = NewNewPassword;
            _confirmNewPasswordTextBox.Text = NewNewPassword;
            _saveButton.Click();
            AssertUrlWithoutQuery(LoggedInMemberHomeUrl);

            // Check the user login.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            LogIn(member, NewPassword);
            AssertNotLoggedIn();

            LogIn(member, NewNewPassword);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestChangePasswordAndSendEmail()
        {
            // Create the member.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var member = _memberAccountsCommand.CreateTestMember(1);

            LogIn(administrator);
            Get(GetMemberUrl(member));

            _changePasswordTextBox.Text = NewPassword;
            _sendPasswordEmailCheckBox.IsChecked = true;
            _changeButton.Click();
            AssertConfirmationMessage("The password has been reset and an email has been sent.");

            AssertMember(member);

            // Check the user login.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            var email = _emailServer.AssertEmailSent();
            email.AssertViewContains(MediaTypeNames.Text.Html, member.GetLoginId());
            email.AssertViewContains(MediaTypeNames.Text.Html, NewPassword);

            // Check the user login.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            LogIn(member, NewPassword);
            AssertUrlWithoutQuery(_mustChangePasswordUrl);

            // Change password.

            _passwordTextBox.Text = NewPassword;
            _newPasswordTextBox.Text = NewNewPassword;
            _confirmNewPasswordTextBox.Text = NewNewPassword;
            _saveButton.Click();
            AssertUrlWithoutQuery(LoggedInMemberHomeUrl);

            // Check the user login.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            LogIn(member, NewPassword);
            AssertNotLoggedIn();

            LogIn(member, NewNewPassword);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        private void AssertMember(IMember member)
        {
            AssertMember(member, false);
        }

        private Member CreateMember(bool enabled, bool activated)
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            member.IsEnabled = enabled;
            member.IsActivated = activated;
            _memberAccountsCommand.UpdateMember(member);
            return member;
        }
    }
}
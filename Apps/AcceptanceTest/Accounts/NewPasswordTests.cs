using System.Net.Mime;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class NewPasswordTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private const string NewPassword = "abc123";
        private const string NewNewPassword = "newNewPassword";

        private ReadOnlyUrl _loginUrl;
        private ReadOnlyUrl _employerLoginUrl;
        private ReadOnlyUrl _mustChangePasswordUrl;
        private ReadOnlyUrl _newPasswordUrl;
        private ReadOnlyUrl _notActivatedUrl;

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlButtonTester _sendButton;
        private HtmlButtonTester _doneButton;
        private HtmlLinkTester _forgotPasswordLink;
        private HtmlButtonTester _cancelButton;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlPasswordTester _newPasswordTextBox;
        private HtmlPasswordTester _confirmNewPasswordTextBox;
        private HtmlButtonTester _saveButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _emailServer.ClearEmails();

            _loginUrl = new ReadOnlyApplicationUrl(true, "~/login");
            _employerLoginUrl = new ReadOnlyApplicationUrl(true, "~/employers/login");
            _mustChangePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/mustchangepassword");
            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
            _newPasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/newpassword");

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _sendButton = new HtmlButtonTester(Browser, "send");
            _doneButton = new HtmlButtonTester(Browser, "done");
            _forgotPasswordLink = new HtmlLinkTester(Browser, "ForgotPassword");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");

            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _newPasswordTextBox = new HtmlPasswordTester(Browser, "NewPassword");
            _confirmNewPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmNewPassword");
            _saveButton = new HtmlButtonTester(Browser, "save");
        }

        [TestMethod]
        public void TestValidationFail()
        {
            Get(_newPasswordUrl);

            Assert.IsTrue(_loginIdTextBox.IsVisible);

            _sendButton.Click();

            Assert.IsTrue(_loginIdTextBox.IsVisible);
            AssertErrorMessage("The username is required.");
        }

        [TestMethod]
        public void TestNoUser()
        {
            Get(_newPasswordUrl);
            _loginIdTextBox.Text = "linkme3@test.linkme.net.au";
            _sendButton.Click();
            AssertErrorMessage("The user cannot be found. Please try again.");
        }

        [TestMethod]
        public void TestMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            Get(_newPasswordUrl);

            _loginIdTextBox.Text = member.GetLoginId();
            _sendButton.Click();

            AssertUrl(_newPasswordUrl);
            Assert.IsTrue(_doneButton.IsVisible);
            _doneButton.Click();

            AssertUrl(HomeUrl);
            LogOut();

            var email = _emailServer.AssertEmailSent();
            email.AssertViewContains(MediaTypeNames.Text.Html, member.GetLoginId());
            email.AssertViewContains(MediaTypeNames.Text.Html, NewPassword);

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
        public void TestNotActivatedMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0, false);

            Get(_newPasswordUrl);

            _loginIdTextBox.Text = member.GetLoginId();
            _sendButton.Click();

            AssertUrl(_newPasswordUrl);
            Assert.IsTrue(_doneButton.IsVisible);
            _doneButton.Click();

            AssertUrl(HomeUrl);
            LogOut();

            LogIn(member, NewPassword);
            AssertUrlWithoutQuery(_mustChangePasswordUrl);

            // Change password.

            _passwordTextBox.Text = NewPassword;
            _newPasswordTextBox.Text = NewNewPassword;
            _confirmNewPasswordTextBox.Text = NewNewPassword;
            _saveButton.Click();
            AssertUrlWithoutQuery(_notActivatedUrl);

            // Check the user login.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            LogIn(member, NewPassword);
            AssertNotLoggedIn();

            LogIn(member, NewNewPassword);
            AssertUrl(_notActivatedUrl);
        }

        [TestMethod]
        public void TestEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            Get(_newPasswordUrl);

            _loginIdTextBox.Text = employer.GetLoginId();
            _sendButton.Click();

            AssertUrl(_newPasswordUrl);
            Assert.IsTrue(_doneButton.IsVisible);
            _doneButton.Click();

            AssertUrl(HomeUrl);
            LogOut();

            var email = _emailServer.AssertEmailSent();
            email.AssertViewContains(MediaTypeNames.Text.Html, employer.GetLoginId());
            email.AssertViewContains(MediaTypeNames.Text.Html, NewPassword);

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
        public void TestBackForMember()
        {
            // Go to the sign in and request a new password.

            Get(_loginUrl);
            _forgotPasswordLink.Click();
            AssertPath(_newPasswordUrl);

            // Go back.

            _cancelButton.Click();
            AssertUrl(HomeUrl);
        }

        [TestMethod]
        public void TestBackForEmployer()
        {
            // Go to the sign in and request a new password.

            Get(_employerLoginUrl);
            _forgotPasswordLink.Click();
            AssertPath(_newPasswordUrl);

            // Go back.

            _cancelButton.Click();
            AssertPath(EmployerHomeUrl);
        }

        [TestMethod]
        public void TestForgotPassword()
        {
            // Bug 3437: Clicking the forgot password link caused abend instead of redirection.

            Get(EmployerHomeUrl);
            AssertUrl(EmployerHomeUrl);

            // Click the link.

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='forgotpassword_link link']");
            Assert.IsNotNull(node);
            Get(new ReadOnlyApplicationUrl(node.Attributes["href"].Value));

            // Assert redirection.

            var newPasswordUrl = _newPasswordUrl.AsNonReadOnly();
            newPasswordUrl.QueryString["userType"] = UserType.Employer.ToString();
            AssertUrl(newPasswordUrl);
        }

        [TestMethod]
        public void TestLoggedInMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Login and try to access.

            LogIn(member);
            Get(_newPasswordUrl);

            // A logged in member can't ask for a new password, they should just go to there settings page and do it there.

            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestLoggedInEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Login and try to access.

            LogIn(employer);
            Get(_newPasswordUrl);

            // A logged in member can't ask for a new password, they should just go to there settings page and do it there.

            AssertUrl(LoggedInEmployerHomeUrl);
        }

        [TestMethod]
        public void TestErrorChangingPassword()
        {
            // Testing Case 11161.

            // 1. Go to 'forgot password' link

            var member = _memberAccountsCommand.CreateTestMember(0);
            Get(_newPasswordUrl);

            // 2. Have a password emailed to me

            _loginIdTextBox.Text = member.GetLoginId();
            _sendButton.Click();

            AssertUrl(_newPasswordUrl);
            Assert.IsTrue(_doneButton.IsVisible);
            _doneButton.Click();

            AssertUrl(HomeUrl);
            LogOut();

            // 3. Type in username and (new) password

            LogIn(member, NewPassword);

            // 4. Get redirected to 'change password' screen

            AssertUrlWithoutQuery(_mustChangePasswordUrl);

            // 5. Type in (correct) assigned password and then type in non-matching passwords (I used 147258 and 147852)

            _passwordTextBox.Text = NewPassword;
            _newPasswordTextBox.Text = NewNewPassword;
            _confirmNewPasswordTextBox.Text = NewNewPassword + NewNewPassword;
            _saveButton.Click();

            // 6. Get message saying passwords don't match

            AssertErrorMessage("The confirm password and password must match.");

            // 7. Attempt to change passwords with assigned password (from email) and matching passwords

            _passwordTextBox.Text = NewPassword;
            _newPasswordTextBox.Text = NewNewPassword;
            _confirmNewPasswordTextBox.Text = NewNewPassword;
            _saveButton.Click();

            // 8. Can't login - in fact now that it is fixed they can log in.

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
    }
}
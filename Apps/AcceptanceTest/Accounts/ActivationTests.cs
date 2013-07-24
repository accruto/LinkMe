using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Members.Friends;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class ActivationTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        private ReadOnlyUrl _activationUrl;
        private ReadOnlyUrl _notActivatedUrl;
        private ReadOnlyUrl _activationSentUrl;
        private ReadOnlyUrl _changeEmailUrl;
        private ReadOnlyUrl _profileUrl;

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlButtonTester _loginButton;
        private HtmlLinkTester _changeEmailAddressLink;
        private HtmlButtonTester _sendButton;
        private HtmlTextBoxTester _emailAddressTextBox;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _activationUrl = new ReadOnlyApplicationUrl("~/accounts/activation");
            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
            _activationSentUrl = new ReadOnlyApplicationUrl(true, "~/accounts/activationsent");
            _changeEmailUrl = new ReadOnlyApplicationUrl(true, "~/accounts/changeemail");
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _loginButton = new HtmlButtonTester(Browser, "login");
            _changeEmailAddressLink = new HtmlLinkTester(Browser, "ChangeEmailAddress");
            _sendButton = new HtmlButtonTester(Browser, "send");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
        }

        [TestMethod]
        public void TestInvalidCode()
        {
            // Get the page with an invalid code.
            
            Get(GetActivationUrl("invalidCode"));

            // Assert that the error message is displayed.

            Assert.IsTrue(_loginButton.IsVisible);
            AssertErrorMessage("The activation code with value 'invalidCode' cannot be found. Please log in to request another activation email.");
        }

        [TestMethod]
        public void TestNoCode()
        {
            // Assert that the error message is displayed.

            Get(_activationUrl);

            Assert.IsTrue(_loginButton.IsVisible);
            AssertErrorMessage("The activation code is required. Please log in to request another activation email.");
        }

        [TestMethod]
        public void TestChangedEmailAddress()
        {
            // Create a verification.

            var member = _memberAccountsCommand.CreateTestMember(0, false);
            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Change email.

            LogIn(member);
            AssertUrl(_notActivatedUrl);
            _changeEmailAddressLink.Click();

            // Assert that the user was redirected to the appropriate page.

            var changeEmailUrl = _changeEmailUrl.AsNonReadOnly();
            changeEmailUrl.QueryString["returnUrl"] = _notActivatedUrl.PathAndQuery;
            AssertUrl(changeEmailUrl);

            // Change the email address.

            const string newEmail = "linkme2@test.linkme.net.au";
            _emailAddressTextBox.Text = newEmail;
            _sendButton.Click();
            AssertUrl(_activationSentUrl);

            // Logout and try to log in again with the old email.

            LogOut();

            // Browse to the page.

            Get(GetActivationUrl(emailVerification.VerificationCode));

            // Assert that the error message is displayed.

            Assert.IsTrue(_loginButton.IsVisible);
            AssertErrorMessage("The activation code with value '" + emailVerification.VerificationCode + "' cannot be found. Please log in to request another activation email.");
        }

        [TestMethod]
        public void TestActivation()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0, false);

            // Create a verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Browse to the page.

            var activationUrl = GetActivationUrl(emailVerification.VerificationCode);
            Get(activationUrl);

            // Check that the email address is set on the page.

            AssertUrl(activationUrl);
            Assert.AreEqual(member.GetLoginId(), _loginIdTextBox.Text);
            Assert.IsTrue(_loginButton.IsVisible);

            // Check the user flags again.

            member = _membersQuery.GetMember(member.Id);
            Assert.IsNotNull(member);
            Assert.IsTrue(member.IsActivated);
        }

        [TestMethod]
        public void TestRedirect()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0, false);

            // Create a verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Browse to the page.

            var activationUrl = GetActivationUrl(emailVerification.VerificationCode).AsNonReadOnly();

            var returnUrl = new ReadOnlyApplicationUrl("~/search/jobs");
            activationUrl.QueryString["returnUrl"] = returnUrl.PathAndQuery;
            Get(activationUrl);

            // Check for the redirect.

            AssertUrl(returnUrl);

            // Check the user flags again.

            member = _membersQuery.GetMember(member.Id);
            Assert.IsNotNull(member);
            Assert.IsTrue(member.IsActivated);
        }

        [TestMethod]
        public void TestOldActivationLink()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0, false);

            // Create a verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Browse to the page.

            var activationUrl = GetActivationUrl(emailVerification.VerificationCode);

            // Use the old link.

            var oldUrl = new ReadOnlyApplicationUrl("~/activation.aspx", activationUrl.QueryString);
            Get(oldUrl);

            // Check that the email address is set on the page.

            AssertUrl(activationUrl);
            Assert.IsTrue(_loginButton.IsVisible);
            Assert.AreEqual(member.GetLoginId(), _loginIdTextBox.Text);

            // Check the user flags again.

            member = _membersQuery.GetMember(member.Id);
            Assert.IsNotNull(member);
            Assert.IsTrue(member.IsActivated);
        }

        [TestMethod]
        public void TestInvalidPassword()
        {
            var member = _memberAccountsCommand.CreateTestMember(0, false);
            var activationCode = Activate(member);

            // Verify with an invalid password.

            _passwordTextBox.Text = "invalidPassword";
            _loginButton.Click();

            // Assert that the appropriate error message is shown.

            AssertPath(GetActivationUrl(activationCode));
            AssertPageContains("Login failed. Please try again.");
        }

        [TestMethod]
        public void TestSuccessfulLogin()
        {
            var member = _memberAccountsCommand.CreateTestMember(0, false);
            Activate(member);

            // LogIn.

            _passwordTextBox.Text = member.GetPassword();
            _loginButton.Click();

            // Assert that the welcome page is displayed.

            AssertUrl(_profileUrl);
        }

        [TestMethod]
        public void TestSuccessfulNavigation()
        {
            var member = _memberAccountsCommand.CreateTestMember(0, false);
            Activate(member);

            // LogIn.

            _passwordTextBox.Text = member.GetPassword();
            _loginButton.Click();

            // Navigate to some pages to check that the account is activated.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();
        }

        [TestMethod]
        public void TestActivationAfterLogin()
        {
            var member = _memberAccountsCommand.CreateTestMember(0, false);
            var activationCode = CreateActivation(member);

            // LogIn.

            LogIn(member);
            AssertUrl(_notActivatedUrl);

            // Navigate to some pages to check that the account is not activated.

            Get(LoggedInMemberHomeUrl);
            AssertUrlWithoutQuery(_notActivatedUrl);
            GetPage<InviteFriends>();
            AssertUrlWithoutQuery(_notActivatedUrl);
            Get(_profileUrl);
            AssertUrlWithoutQuery(_notActivatedUrl);

            // Navigate to the activation page.

            Get(GetActivationUrl(activationCode));
            AssertUrl(_profileUrl);

            // Navigate to some pages to check that the account is activated.

            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();
            Get(_profileUrl);
            AssertUrl(_profileUrl);
        }

        [TestMethod]
        public void TestAlreadyActivated()
        {
            // Verify.

            var member = _memberAccountsCommand.CreateTestMember(0, false);
            var activationCode = Activate(member);

            // Logout and try again.

            LogOut();
            var activationUrl = GetActivationUrl(activationCode);
            Get(activationUrl);
            AssertUrl(activationUrl);

            // LogIn.

            _passwordTextBox.Text = member.GetPassword();
            _loginButton.Click();

            // Assert that the welcome page is displayed.

            AssertUrl(_profileUrl);
        }

        [TestMethod]
        public void TestMidSessionActivation()
        {
            // Create an activation.

            var member = _memberAccountsCommand.CreateTestMember(0, false);
            CreateActivation(member);

            // LogIn and try to go to a few pages.

            LogIn(member);
            AssertUrl(_notActivatedUrl);

            // Navigate to some pages to check that the account is not activated.

            Get(LoggedInMemberHomeUrl);
            AssertUrlWithoutQuery(_notActivatedUrl);
            GetPage<InviteFriends>();
            AssertUrlWithoutQuery(_notActivatedUrl);
            Get(_profileUrl);
            AssertUrlWithoutQuery(_notActivatedUrl);

            // Update the activation status but not through the browser.
            // This could be because a user activated their account through another browser instance.

            member.IsActivated = true;
            _memberAccountsCommand.UpdateMember(member);

            // Navigate again.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();
            Get(_profileUrl);
            AssertUrl(_profileUrl);
        }

        private ReadOnlyUrl GetActivationUrl(string activationCode)
        {
            var url = _activationUrl.AsNonReadOnly();
            url.QueryString["activationCode"] = activationCode;
            return url;
        }

        private string CreateActivation(ICommunicationRecipient member)
        {
            // Create a verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.EmailAddress };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Browse to the page.

            return emailVerification.VerificationCode;
        }

        private string Activate(Member member)
        {
            // Create an activation.

            var activationCode = CreateActivation(member);

            // Go to the activation page to activate.

            var activationUrl = GetActivationUrl(activationCode);
            Get(activationUrl);

            // Check that the email address is set on the page.

            AssertUrl(activationUrl);
            Assert.AreEqual(member.GetLoginId(), _loginIdTextBox.Text);

            return activationCode;
        }
    }
}
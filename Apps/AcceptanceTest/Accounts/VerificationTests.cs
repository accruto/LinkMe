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
    public class VerificationTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        private ReadOnlyUrl _verificationUrl;
        private ReadOnlyUrl _notVerifiedUrl;
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

            _verificationUrl = new ReadOnlyApplicationUrl("~/accounts/verification");
            _notVerifiedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notverified");
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
            
            Get(GetVerificationUrl("invalidCode"));

            // Assert that the error message is displayed.

            Assert.IsTrue(_loginButton.IsVisible);
            AssertErrorMessage("The verification code with value 'invalidCode' cannot be found. Please log in to request another verification email.");
        }

        [TestMethod]
        public void TestNoCode()
        {
            // Assert that the error message is displayed.

            Get(_verificationUrl);

            Assert.IsTrue(_loginButton.IsVisible);
            AssertErrorMessage("The verification code is required. Please log in to request another verification email.");
        }

        [TestMethod]
        public void TestChangedEmailAddress()
        {
            // Create a verification.

            var member = _memberAccountsCommand.CreateTestMember(0, true);
            _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[0].Address, null);
            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Change email.

            LogIn(member);
            AssertUrlWithoutQuery(_notVerifiedUrl);
            _changeEmailAddressLink.Click();

            // Assert that the user was redirected to the appropriate page.

            var changeEmailUrl = _changeEmailUrl.AsNonReadOnly();
            changeEmailUrl.QueryString["returnUrl"] = _notVerifiedUrl.PathAndQuery;
            AssertUrl(changeEmailUrl);

            // Change the email address.

            const string newEmail = "linkme2@test.linkme.net.au";
            _emailAddressTextBox.Text = newEmail;
            _sendButton.Click();

            // Changing an email results in reactivation.

            AssertUrl(_activationSentUrl);

            // Logout and try to log in again with the old email.

            LogOut();

            // Browse to the page.

            Get(GetVerificationUrl(emailVerification.VerificationCode));

            // Assert that the error message is displayed.

            Assert.IsTrue(_loginButton.IsVisible);
            AssertErrorMessage("The verification code with value '" + emailVerification.VerificationCode + "' cannot be found. Please log in to request another verification email.");
        }

        [TestMethod]
        public void TestVerification()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0, false);

            // Create a verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Browse to the page.

            var activationUrl = GetVerificationUrl(emailVerification.VerificationCode);
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
        public void TestInvalidPassword()
        {
            var member = _memberAccountsCommand.CreateTestMember(0, false);
            var verificationCode = Verify(member);

            // Verify with an invalid password.

            _passwordTextBox.Text = "invalidPassword";
            _loginButton.Click();

            // Assert that the appropriate error message is shown.

            AssertPath(GetVerificationUrl(verificationCode));
            AssertPageContains("Login failed. Please try again.");
        }

        [TestMethod]
        public void TestSuccessfulLogin()
        {
            var member = _memberAccountsCommand.CreateTestMember(0, false);
            Verify(member);

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
            Verify(member);

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
        public void TestVerificationAfterLogin()
        {
            var member = _memberAccountsCommand.CreateTestMember(0, true);
            _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[0].Address, null);
            var verificationCode = CreateVerification(member);

            // LogIn.

            LogIn(member);
            AssertUrlWithoutQuery(_notVerifiedUrl);

            // Navigate to some pages to check that they can be access although not activated.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();
            Get(_profileUrl);
            AssertUrl(_profileUrl);

            // Navigate to the activation page.

            Get(GetVerificationUrl(verificationCode));
            AssertUrl(_profileUrl);

            // Navigate to some pages to check that the account is activated.

            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();
            Get(_profileUrl);
            AssertUrl(_profileUrl);
        }

        [TestMethod]
        public void TestAlreadyVerified()
        {
            // Verify.

            var member = _memberAccountsCommand.CreateTestMember(0, true);
            _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[0].Address, null);
            var verificationCode = Verify(member);

            // Logout and try again.

            LogOut();
            var verificationUrl = GetVerificationUrl(verificationCode);
            Get(verificationUrl);
            AssertUrl(verificationUrl);

            // LogIn.

            _passwordTextBox.Text = member.GetPassword();
            _loginButton.Click();

            // Assert that the welcome page is displayed.

            AssertUrl(_profileUrl);
        }

        [TestMethod]
        public void TestMidSessionVerification()
        {
            // Create an activation.

            var member = _memberAccountsCommand.CreateTestMember(0, true);
            _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[0].Address, null);
            CreateVerification(member);

            // LogIn and try to go to a few pages.

            LogIn(member);
            AssertUrlWithoutQuery(_notVerifiedUrl);

            // Navigate to some pages to check that the account is not activated.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();
            Get(_profileUrl);
            AssertUrl(_profileUrl);

            // Update the verification status but not through the browser.
            // This could be because a user verified their account through another browser instance.

            _emailVerificationsCommand.VerifyEmailAddress(member.Id, member.EmailAddresses[0].Address);

            // Navigate again.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();
            Get(_profileUrl);
            AssertUrl(_profileUrl);
        }

        private ReadOnlyUrl GetVerificationUrl(string verificationCode)
        {
            var url = _verificationUrl.AsNonReadOnly();
            url.QueryString["verificationCode"] = verificationCode;
            return url;
        }

        private string CreateVerification(ICommunicationRecipient member)
        {
            // Create a verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.EmailAddress };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Browse to the page.

            return emailVerification.VerificationCode;
        }

        private string Verify(Member member)
        {
            // Create a verification.

            var verificationCode = CreateVerification(member);

            // Go to the activation page to activate.

            var verificationUrl = GetVerificationUrl(verificationCode);
            Get(verificationUrl);

            // Check that the email address is set on the page.

            AssertUrl(verificationUrl);
            Assert.AreEqual(member.GetLoginId(), _loginIdTextBox.Text);

            return verificationCode;
        }
    }
}
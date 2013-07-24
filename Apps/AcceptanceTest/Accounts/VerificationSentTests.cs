using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class VerificationSentTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        private HtmlButtonTester _resendVerificationEmailButton;
        private HtmlLinkTester _changeEmailAddressLink;
        private HtmlButtonTester _cancelButton;

        private ReadOnlyUrl _notVerifiedUrl;
        private ReadOnlyUrl _verificationSentUrl;
        private ReadOnlyUrl _changeEmailUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _resendVerificationEmailButton = new HtmlButtonTester(Browser, "ResendVerificationEmail");
            _changeEmailAddressLink = new HtmlLinkTester(Browser, "ChangeEmailAddress");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");

            _notVerifiedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notverified");
            _verificationSentUrl = new ReadOnlyApplicationUrl(true, "~/accounts/verificationsent");
            _changeEmailUrl = new ReadOnlyApplicationUrl(true, "~/accounts/changeemail");

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestPageVisibility()
        {
            // Assert that you have to be logged on to access the page.

            AssertSecureUrl(_verificationSentUrl, LogInUrl);

            // LogIn.

            var member = _memberAccountsCommand.CreateTestMember(0, true);
            _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[0].Address, null);
            LogIn(member);

            // Resend a verification.

            AssertUrlWithoutQuery(_notVerifiedUrl);
            _resendVerificationEmailButton.Click();

            // Assert that the verification email sent form is shown and that controls are visible.

            AssertUrlWithoutQuery(_verificationSentUrl);
            Assert.IsTrue(_changeEmailAddressLink.IsVisible);

            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestMultipleEmails()
        {
            // LogIn.

            var member = _memberAccountsCommand.CreateTestMember(0, true);
            _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[0].Address, null);
            LogIn(member);

            // Resend a verification.

            AssertUrlWithoutQuery(_notVerifiedUrl);
            _resendVerificationEmailButton.Click();

            AssertUrlWithoutQuery(_verificationSentUrl);
            _emailServer.AssertEmailSent();

            // Do it again.

            LogOut();
            LogIn(member);

            // Resend a verification.

            AssertUrlWithoutQuery(_notVerifiedUrl);
            _resendVerificationEmailButton.Click();

            AssertUrlWithoutQuery(_verificationSentUrl);
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestChangeEmailAddress()
        {
            // LogIn.

            var member = _memberAccountsCommand.CreateTestMember(0, true);
            _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[0].Address, null);
            LogIn(member);

            // Resend a verification.

            AssertUrlWithoutQuery(_notVerifiedUrl);
            _resendVerificationEmailButton.Click();

            // Change email.

            AssertUrlWithoutQuery(_verificationSentUrl);
            _changeEmailAddressLink.Click();

            // Assert that the user was redirected to the appropriate page.

            var changeEmailUrl = _changeEmailUrl.AsNonReadOnly();
            changeEmailUrl.QueryString["returnUrl"] = _verificationSentUrl.PathAndQuery;
            AssertUrl(changeEmailUrl);

            // Assert that selecting the cancel button returns the browser to this form.

            Assert.IsTrue(_cancelButton.IsVisible);
            _cancelButton.Click();
            AssertUrl(_verificationSentUrl);
        }
    }
}
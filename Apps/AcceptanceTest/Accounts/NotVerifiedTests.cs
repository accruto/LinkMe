using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class NotVerifiedTests
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

        private const string SecondaryEmailAddress = "homer@test.linkme.net.au";

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
        public void TestSecurity()
        {
            // Assert that you have to be logged on to access the page.

            AssertSecureUrl(_notVerifiedUrl, LogInUrl);
        }

        [TestMethod]
        public void TestPrimaryEmailVerified()
        {
            var member = CreateMember(true, false, false);
            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestSecondaryEmailNotVerified()
        {
            var member = CreateMember(true, true, false);
            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestSecondaryEmailVerified()
        {
            var member = CreateMember(false, true, true);
            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestPrimaryEmailNotVerified()
        {
            // LogIn.

            var member = CreateMember(false, false, false);
            LogIn(member);

            // Assert that the account not activated form is shown and that controls are visible.

            AssertUrlWithoutQuery(_notVerifiedUrl);
            Assert.IsTrue(_resendVerificationEmailButton.IsVisible);
            Assert.IsTrue(_changeEmailAddressLink.IsVisible);
            AssertPageContains(member.EmailAddresses[0].Address);
        }

        [TestMethod]
        public void TestBothEmailsNotVerified()
        {
            // LogIn.

            var member = CreateMember(false, true, false);
            LogIn(member);

            // Assert that the account not activated form is shown and that controls are visible.

            AssertUrlWithoutQuery(_notVerifiedUrl);
            Assert.IsTrue(_resendVerificationEmailButton.IsVisible);
            Assert.IsTrue(_changeEmailAddressLink.IsVisible);
            AssertPageContains(member.EmailAddresses[0].Address);
            AssertPageContains(member.EmailAddresses[1].Address);
        }

        [TestMethod]
        public void TestResendVerificationEmail()
        {
            // LogIn.

            var member = CreateMember(false, false, false);
            LogIn(member);

            // Resend a verification.

            AssertUrlWithoutQuery(_notVerifiedUrl);
            _resendVerificationEmailButton.Click();

            // Assert email.

            _emailServer.AssertEmailSent();

            // Assert that the user was redirected to the appropriate page.

            AssertUrlWithoutQuery(_verificationSentUrl);
        }

        [TestMethod]
        public void TestResendVerificationEmails()
        {
            // LogIn.

            var member = CreateMember(false, true, false);
            LogIn(member);

            // Resend a verification.

            AssertUrlWithoutQuery(_notVerifiedUrl);
            _resendVerificationEmailButton.Click();

            // Assert email.

            _emailServer.AssertEmailsSent(2);

            // Assert that the user was redirected to the appropriate page.

            AssertUrlWithoutQuery(_verificationSentUrl);
        }

        [TestMethod]
        public void TestChangeEmailAddress()
        {
            // LogIn.

            var member = CreateMember(false, false, false);
            LogIn(member);

            // Change email.

            AssertUrlWithoutQuery(_notVerifiedUrl);
            _changeEmailAddressLink.Click();

            // Assert that the user was redirected to the appropriate page.

            var changeEmailUrl = _changeEmailUrl.AsNonReadOnly();
            changeEmailUrl.QueryString["returnUrl"] = _notVerifiedUrl.PathAndQuery;
            AssertUrl(changeEmailUrl);

            // Assert that selecting the cancel button returns the browser to this form.

            Assert.IsTrue(_cancelButton.IsVisible);
            _cancelButton.Click();
            AssertUrl(_notVerifiedUrl);
        }

        private Member CreateMember(bool primaryEmailIsVerified, bool hasSecondaryEmail, bool secondaryEmailIsVerified)
        {
            var member = _memberAccountsCommand.CreateTestMember(0, true);

            if (!primaryEmailIsVerified)
                _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[0].Address, null);

            if (hasSecondaryEmail)
            {
                member.EmailAddresses.Add(new EmailAddress {Address = SecondaryEmailAddress});
                _memberAccountsCommand.UpdateMember(member);

                if (secondaryEmailIsVerified)
                    _emailVerificationsCommand.VerifyEmailAddress(member.Id, member.EmailAddresses[1].Address);
                else
                    _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[1].Address, null);
            }

            return member;
        }
    }
}
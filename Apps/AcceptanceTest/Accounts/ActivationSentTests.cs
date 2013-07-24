using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class ActivationSentTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();

        private HtmlButtonTester _resendActivationEmailButton;
        private HtmlLinkTester _changeEmailAddressLink;
        private HtmlButtonTester _cancelButton;

        private ReadOnlyUrl _notActivatedUrl;
        private ReadOnlyUrl _activationSentUrl;
        private ReadOnlyUrl _changeEmailUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _resendActivationEmailButton = new HtmlButtonTester(Browser, "ResendActivationEmail");
            _changeEmailAddressLink = new HtmlLinkTester(Browser, "ChangeEmailAddress");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");

            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
            _activationSentUrl = new ReadOnlyApplicationUrl(true, "~/accounts/activationsent");
            _changeEmailUrl = new ReadOnlyApplicationUrl(true, "~/accounts/changeemail");

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestPageVisibility()
        {
            // Assert that you have to be logged on to access the page.

            AssertSecureUrl(_activationSentUrl, LogInUrl);

            // LogIn.

            var member = _memberAccountsCommand.CreateTestMember(0, false);
            LogIn(member);

            // Resend an activation.

            AssertUrl(_notActivatedUrl);
            _resendActivationEmailButton.Click();

            // Assert that the verification email sent form is shown and that controls are visible.

            AssertUrl(_activationSentUrl);
            Assert.IsTrue(_changeEmailAddressLink.IsVisible);

            // Assert the email is sent.

            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestMultipleEmails()
        {
            // LogIn.

            var member = _memberAccountsCommand.CreateTestMember(0, false);
            LogIn(member);

            // Resend an activation.

            AssertUrl(_notActivatedUrl);
            _resendActivationEmailButton.Click();

            // Assert that the verification email is sent.

            AssertUrl(_activationSentUrl);
            _emailServer.AssertEmailSent();

            // Do it again.

            LogOut();
            LogIn(member);
            AssertUrl(_notActivatedUrl);
            _resendActivationEmailButton.Click();

            // Assert that the verification email is sent.

            AssertUrl(_activationSentUrl);
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestChangeEmailAddress()
        {
            // LogIn.

            var member = _memberAccountsCommand.CreateTestMember(0, false);
            LogIn(member);

            // Resend an activation.

            AssertUrl(_notActivatedUrl);
            _resendActivationEmailButton.Click();

            // Change email.

            AssertUrl(_activationSentUrl);
            _changeEmailAddressLink.Click();

            // Assert that the user was redirected to the appropriate page.

            var changeEmailUrl = _changeEmailUrl.AsNonReadOnly();
            changeEmailUrl.QueryString["returnUrl"] = _activationSentUrl.PathAndQuery;
            AssertUrl(changeEmailUrl);

            // Assert that selecting the cancel button returns the browser to this form.

            Assert.IsTrue(_cancelButton.IsVisible);
            _cancelButton.Click();
            AssertUrl(_activationSentUrl);
        }
    }
}
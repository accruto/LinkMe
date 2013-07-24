using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns.Reengagement
{
    [TestClass]
    public class SendTestEmailTests
        : ReengagementCampaignsTests
    {
        private const string SubjectFormat = "{0}, LinkMe misses you";
        private const string HtmlBody = "Reactivation600_01.png";
        private const string TextBody = "BE FOUND BY EMPLOYERS TODAY";

        private HtmlTextBoxTester _emailAddressesTextBox;
        private HtmlTextBoxTester _loginIdsTextBox;
        private HtmlButtonTester _sendButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _emailAddressesTextBox = new HtmlTextBoxTester(Browser, "emailAddresses");
            _loginIdsTextBox = new HtmlTextBoxTester(Browser, "loginIds");
            _sendButton = new HtmlButtonTester(Browser, "send");

            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void OneEmailMemberTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(administrator);
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Login as administrator.

            LogIn(administrator);

            // Send.

            Get(GetEditTemplateUrl(campaign.Id));
            _emailAddressesTextBox.Text = member.EmailAddresses[0].Address;
            _sendButton.Click();

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(member.EmailAddresses[0].Address, member.FullName));
            email.AssertSubject(GetSubject(member));
            Assert.IsTrue(email.GetHtmlView().Body.Contains(HtmlBody));
            Assert.IsTrue(email.GetPlainTextView().Body.Contains(TextBody));
        }

        [TestMethod]
        public void OneLoginMemberTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(administrator);

            var member = _memberAccountsCommand.CreateTestMember(0);
            var loginId = member.GetLoginId();

            // Login as administrator.

            LogIn(administrator);

            // Send.

            Get(GetEditTemplateUrl(campaign.Id));
            _loginIdsTextBox.Text = loginId;
            _sendButton.Click();

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(GetSubject(member));
            Assert.IsTrue(email.GetHtmlView().Body.Contains(HtmlBody));
            Assert.IsTrue(email.GetPlainTextView().Body.Contains(TextBody));
        }

        private static string GetSubject(IRegisteredUser member)
        {
            return string.Format(SubjectFormat, member.FirstName);
        }
    }
}
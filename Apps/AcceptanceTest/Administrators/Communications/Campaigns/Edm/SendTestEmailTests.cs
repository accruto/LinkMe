using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns.Edm
{
    [TestClass]
    public class SendTestEmailTests
        : EdmCampaignsTests
    {
        private const string Subject = "Edm";
        private const string HtmlBody = "EDM600_01.png";
        private const string TextBody = "We hope you've had a great start to 2012.";

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
            OneEmailTest();
        }

        [TestMethod]
        public void OneLoginMemberTest()
        {
            OneLoginTest();
        }

        private void OneEmailTest()
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
            email.AssertSubject(Subject);
            Assert.IsTrue(email.GetHtmlView().Body.Contains(HtmlBody));
            Assert.IsTrue(email.GetPlainTextView().Body.Contains(TextBody));
        }

        private void OneLoginTest()
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
            email.AssertSubject(Subject);
            Assert.IsTrue(email.GetHtmlView().Body.Contains(HtmlBody));
            Assert.IsTrue(email.GetPlainTextView().Body.Contains(TextBody));
        }
    }
}
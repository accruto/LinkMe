using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns.IosLaunch
{
    [TestClass]
    public class SendTestEmailTests
        : IosLaunchCampaignsTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private const string Subject = "Download 'Candidate Connect' for your iPhone and find talent at your fingertips";
        private const string HtmlBody = "iPhoneLaunch600_01.png";
        private const string TextBody = "*An internet connection is required";

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
        public void OneEmailEmployerTest()
        {
            OneEmailTest();
        }

        [TestMethod]
        public void OneLoginEmployerTest()
        {
            OneLoginTest();
        }

        private void OneEmailTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(administrator);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Login as administrator.

            LogIn(administrator);

            // Send.

            Get(GetEditTemplateUrl(campaign.Id));
            _emailAddressesTextBox.Text = employer.EmailAddress.Address;
            _sendButton.Click();

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(employer.EmailAddress.Address, employer.FullName));
            email.AssertSubject(Subject);
            Assert.IsTrue(email.GetHtmlView().Body.Contains(HtmlBody));
            Assert.IsTrue(email.GetPlainTextView().Body.Contains(TextBody));
        }

        private void OneLoginTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(administrator);

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var loginId = employer.GetLoginId();

            // Login as administrator.

            LogIn(administrator);

            // Send.

            Get(GetEditTemplateUrl(campaign.Id));
            _loginIdsTextBox.Text = loginId;
            _sendButton.Click();

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(Subject);
            Assert.IsTrue(email.GetHtmlView().Body.Contains(HtmlBody));
            Assert.IsTrue(email.GetPlainTextView().Body.Contains(TextBody));
        }
    }
}
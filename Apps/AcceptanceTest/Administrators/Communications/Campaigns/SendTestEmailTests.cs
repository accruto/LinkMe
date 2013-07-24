using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns
{
    [TestClass]
    public class SendTestEmailTests
        : CampaignsTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private const string Subject = "My updated subject";
        private const string Body = "My updated body";
        private const string InvalidEmail = "xyz";
        private const string InvalidLoginId = "abc";

        private HtmlTextBoxTester _subjectTextBox;
        private HtmlTextAreaTester _bodyTextArea;
        private HtmlTextBoxTester _emailAddressesTextBox;
        private HtmlTextBoxTester _loginIdsTextBox;
        private HtmlButtonTester _sendButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _subjectTextBox = new HtmlTextBoxTester(Browser, "Subject");
            _bodyTextArea = new HtmlTextAreaTester(Browser, "Body");

            _emailAddressesTextBox = new HtmlTextBoxTester(Browser, "emailAddresses");
            _loginIdsTextBox = new HtmlTextBoxTester(Browser, "loginIds");
            _sendButton = new HtmlButtonTester(Browser, "send");

            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void EmptyListsTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, administrator);

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Send with no emails or login ids set.

            _sendButton.Click();

            // Check.

            AssertPageContains("The email address list or login id list is required.");
        }

        [TestMethod]
        public void InvalidEmailTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, administrator);

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Send with invalid email set.

            _emailAddressesTextBox.Text = InvalidEmail;
            _sendButton.Click();

            // Check.

            AssertPageContains("The email addresses must be valid and have less than 320 characters.");
        }

        [TestMethod]
        public void InvalidEmployerTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, CampaignCategory.Employer, administrator);

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Send with an unknown login id.

            _loginIdsTextBox.Text = InvalidLoginId;
            _sendButton.Click();

            // Check.

            AssertPageContains("The login ids cannot be found.");
        }

        [TestMethod]
        public void InvalidMemberTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, CampaignCategory.Member, administrator);

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Send with an unknown login id.

            _loginIdsTextBox.Text = InvalidLoginId;
            _sendButton.Click();

            // Check.

            AssertPageContains("The login ids cannot be found.");
        }

        [TestMethod]
        public void OneEmailEmployerTest()
        {
            OneEmailTest(CampaignCategory.Employer);
        }

        [TestMethod]
        public void OneEmailMemberTest()
        {
            OneEmailTest(CampaignCategory.Member);
        }

        [TestMethod]
        public void OneLoginEmployerTest()
        {
            OneLoginTest(CampaignCategory.Employer);
        }

        [TestMethod]
        public void OneLoginMemberTest()
        {
            OneLoginTest(CampaignCategory.Member);
        }

        [TestMethod]
        public void MultipleEmailsEmployerTest()
        {
            MultipleEmailsTest(CampaignCategory.Employer);
        }

        [TestMethod]
        public void MultipleEmailsMemberTest()
        {
            MultipleEmailsTest(CampaignCategory.Member);
        }

        [TestMethod]
        public void MultipleLoginsEmployerTest()
        {
            MultipleLoginsTest(CampaignCategory.Employer);
        }

        [TestMethod]
        public void MultipleLoginsMemberTest()
        {
            MultipleLoginsTest(CampaignCategory.Member);
        }

        [TestMethod]
        public void MultipleEmailsLoginsEmployerTest()
        {
            MultipleEmailsLoginsTest(CampaignCategory.Employer);
        }

        [TestMethod]
        public void MultipleEmailsLoginsMemberTest()
        {
            MultipleEmailsLoginsTest(CampaignCategory.Member);
        }

        [TestMethod]
        public void SubstitutionsEmployerTest()
        {
            SubstitutionsTest(CampaignCategory.Employer);
        }

        [TestMethod]
        public void SubstitutionsMemberTest()
        {
            SubstitutionsTest(CampaignCategory.Member);
        }

        private void OneEmailTest(CampaignCategory category)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, category, administrator);

            string emailAddress;
            string fullName;
            if (category == CampaignCategory.Member)
            {
                var member = _memberAccountsCommand.CreateTestMember(0);
                emailAddress = member.EmailAddresses[0].Address;
                fullName = member.FullName;
            }
            else
            {
                var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
                emailAddress = employer.EmailAddress.Address;
                fullName = employer.FullName;
            }

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Send.

            _emailAddressesTextBox.Text = emailAddress;
            _sendButton.Click();

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(emailAddress, fullName));
            email.AssertSubject(Subject);
            Assert.IsTrue(email.GetHtmlView().Body.Contains(Body));
        }

        private void OneLoginTest(CampaignCategory category)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, category, administrator);

            ICommunicationUser user;
            string loginId;

            if (category == CampaignCategory.Employer)
            {
                var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
                user = employer;
                loginId = employer.GetLoginId();
            }
            else
            {
                var member = _memberAccountsCommand.CreateTestMember(0);
                user = member;
                loginId = member.GetLoginId();
            }

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Send.

            _loginIdsTextBox.Text = loginId;
            _sendButton.Click();

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, user);
            email.AssertSubject(Subject);
            Assert.IsTrue(email.GetHtmlView().Body.Contains(Body));
        }

        private void MultipleEmailsTest(CampaignCategory category)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, category, administrator);

            var emailAddresses = new string[2];
            var fullNames = new string[2];
            if (category == CampaignCategory.Member)
            {
                for (var index = 0; index < 2; ++index)
                {
                    var member = _memberAccountsCommand.CreateTestMember(index);
                    emailAddresses[index] = member.EmailAddresses[0].Address;
                    fullNames[index] = member.FullName;
                }
            }
            else
            {
                for (var index = 0; index < 2; ++index)
                {
                    var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
                    emailAddresses[index] = employer.EmailAddress.Address;
                    fullNames[index] = employer.FullName;
                }
            }

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Send.

            _emailAddressesTextBox.Text = string.Join(",", emailAddresses);
            _sendButton.Click();

            // Check.

            var emails = _emailServer.AssertEmailsSent(emailAddresses.Length);
            for (var index = 0; index < emailAddresses.Length; ++index)
            {
                var email = emails[index];
                email.AssertAddresses(Return, Return, new EmailRecipient(emailAddresses[index], fullNames[index]));
                email.AssertSubject(Subject);
                Assert.IsTrue(email.GetHtmlView().Body.Contains(Body));
            }
        }

        private void MultipleLoginsTest(CampaignCategory category)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, category, administrator);

            const int count = 2;
            var users = new ICommunicationUser[count];
            var loginIds = new string[count];

            if (category == CampaignCategory.Employer)
            {
                for (var index = 0; index < count; ++index)
                {
                    var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
                    users[index] = employer;
                    loginIds[index] = employer.GetLoginId();
                }
            }
            else
            {
                for (var index = 0; index < count; ++index)
                {
                    var member = _memberAccountsCommand.CreateTestMember(index);
                    users[index] = member;
                    loginIds[index] = member.GetLoginId();
                }
            }

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Send.

            _loginIdsTextBox.Text = string.Join(",", loginIds);
            _sendButton.Click();

            // Check.

            var emails = _emailServer.AssertEmailsSent(users.Length);
            for (var index = 0; index < users.Length; ++index)
            {
                var email = emails[index];
                email.AssertAddresses(Return, Return, users[index]);
                email.AssertSubject(Subject);
                Assert.IsTrue(email.GetHtmlView().Body.Contains(Body));
            }
        }

        private void MultipleEmailsLoginsTest(CampaignCategory category)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, category, administrator);

            const int count = 2;
            var loginIds = new string[count];
            var emailAddresses = new string[count];
            var fullNames = new string[count];

            if (category == CampaignCategory.Employer)
            {
                for (var index = 0; index < count; ++index)
                {
                    var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
                    loginIds[index] = employer.GetLoginId();
                    emailAddresses[index] = employer.EmailAddress.Address;
                    fullNames[index] = employer.FullName;
                }
            }
            else
            {
                for (var index = 0; index < count; ++index)
                {
                    var member = _memberAccountsCommand.CreateTestMember(index);
                    loginIds[index] = member.GetLoginId();
                    emailAddresses[index] = member.EmailAddresses[0].Address;
                    fullNames[index] = member.FullName;
                }
            }

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Send.

            _loginIdsTextBox.Text = string.Join(",", loginIds);
            _sendButton.Click();

            // Check.

            var emails = _emailServer.AssertEmailsSent(loginIds.Length);
            for (var index = 0; index < loginIds.Length; ++index)
            {
                var email = emails[index];
                email.AssertAddresses(Return, Return, new EmailRecipient(emailAddresses[index], fullNames[index]));
                email.AssertSubject(Subject);
                Assert.IsTrue(email.GetHtmlView().Body.Contains(Body));
            }
        }

        private void SubstitutionsTest(CampaignCategory category)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, category, administrator);

            ICommunicationUser user;
            string loginId;

            if (category == CampaignCategory.Employer)
            {
                var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
                user = employer;
                loginId = employer.GetLoginId();
            }
            else
            {
                var member = _memberAccountsCommand.CreateTestMember(0);
                user = member;
                loginId = member.GetLoginId();
            }

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));

            const string subject = "Something for <%= To.FirstName %>";
            const string body = "<%= To.FullName %>, here is something for you.";

            _subjectTextBox.Text = subject;
            _bodyTextArea.Text = body;
            _saveButton.Click();

            // Send.

            _loginIdsTextBox.Text = loginId;
            _sendButton.Click();

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, user);
            email.AssertSubject(subject.Replace("<%= To.FirstName %>", user.FirstName));
            Assert.IsTrue(email.GetHtmlView().Body.Contains(body.Replace("<%= To.FullName %>", user.FullName)));
        }
    }
}

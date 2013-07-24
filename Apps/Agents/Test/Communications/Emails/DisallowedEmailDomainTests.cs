using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails
{
    [TestClass]
    public class DisallowedEmailDomainTests
        : EmailTests
    {
        private const string NewPassword = "newpassword";
        private const string FirstName = "Barney";
        private const string LastName = "Gumble";

        public override TemplateEmail GeneratePreview(Community community)
        {
            return null;
        }

        [TestMethod]
        public void TestAllowedEmailDomain()
        {
            const string emailAddress = "linkme1@test.linkme.net.au";
            _emailsCommand.TrySend(new PasswordReminderEmail(CreateMember(emailAddress), emailAddress, NewPassword));
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestDisallowedEmailDomains()
        {
            // Missing piece.

            var emailAddress = "linkme1@net.au";
            _emailsCommand.TrySend(new PasswordReminderEmail(CreateMember(emailAddress), emailAddress, NewPassword));
            _emailServer.AssertNoEmailSent();

            // Other domains.

            emailAddress = "linkme1@linkme.com.au";
            _emailsCommand.TrySend(new PasswordReminderEmail(CreateMember(emailAddress), emailAddress, NewPassword));
            _emailServer.AssertNoEmailSent();

            emailAddress = "linkme1@gmail.com";
            _emailsCommand.TrySend(new PasswordReminderEmail(CreateMember(emailAddress), emailAddress, NewPassword));
            _emailServer.AssertNoEmailSent();

            emailAddress = "linkme1@hotmail.com";
            _emailsCommand.TrySend(new PasswordReminderEmail(CreateMember(emailAddress), emailAddress, NewPassword));
            _emailServer.AssertNoEmailSent();

            emailAddress = "linkme1@yahoo.com";
            _emailsCommand.TrySend(new PasswordReminderEmail(CreateMember(emailAddress), emailAddress, NewPassword));
            _emailServer.AssertNoEmailSent();
        }

        private Member CreateMember(string emailAddress)
        {
            return _memberAccountsCommand.CreateTestMember(emailAddress, FirstName, LastName);
        }
    }
}
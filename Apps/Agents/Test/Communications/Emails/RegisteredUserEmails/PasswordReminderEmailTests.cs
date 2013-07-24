using System;
using System.Net.Mime;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.RegisteredUserEmails
{
    [TestClass]
    public class PasswordReminderEmailTests
        : EmailTests
    {
        private const string Email = "test@test.linkme.net.au";
        private const string LoginId = "test";
        private const string NewPassword = "newpassword";
        private const string FirstName = "Barney";
        private const string LastName = "Gumble";
        private const string Subject = "New Password";

        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        public override TemplateEmail GeneratePreview(Community community)
        {
            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(Email, FirstName, LastName, community != null ? community.Id : (Guid?)null);

            // Send.

            return new PasswordReminderEmail(member, Email, NewPassword);
        }

        [TestMethod]
        public void TestMemberEmailContents()
        {
            // Create a member.

            Member member = _memberAccountsCommand.CreateTestMember(Email, FirstName, LastName);

            // Send.

            _emailsCommand.TrySend(new PasswordReminderEmail(member, Email, NewPassword));

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(Subject);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmployerEmailContents()
        {
            // Create a member.

            Employer employer = _employerAccountsCommand.CreateTestEmployer(LoginId, FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));

            // Send.

            _emailsCommand.TrySend(new PasswordReminderEmail(employer, LoginId, NewPassword));

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(Subject);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailAlternateViewsContents()
        {
            // Create a member.

            Member member = _memberAccountsCommand.CreateTestMember(Email, FirstName, LastName);

            // Send.

            var communication = new PasswordReminderEmail(member, Email, NewPassword);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            Assert.AreEqual("", email.Body);
            email.AssertViewCount(2);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(communication, member, NewPassword)));
            email.AssertView(MediaTypeNames.Text.Plain, GetPlainBody(communication, GetPlainContent(communication, member, NewPassword)));
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient member, string password)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Dear " + member.FirstName + ",</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  As requested, your password has been reset.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Your login ID is <strong>" + member.EmailAddress + "</strong>");
            sb.AppendLine("  <br/>");
            sb.AppendLine("  and your new temporary password is <strong>" + password + "</strong>");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/login") + "\">Log in</a>");
            sb.AppendLine("  to access your account and update your password.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }

        private string GetPlainContent(TemplateEmail templateEmail, ICommunicationRecipient member, string password)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Dear " + member.FirstName + ",");
            sb.AppendLine();
            sb.AppendLine("As requested, your password has been reset.");
            sb.AppendLine();
            sb.AppendLine("Your login ID is '" + member.EmailAddress + "'");
            sb.AppendLine("and your new temporary password is '" + password + "'");
            sb.AppendLine();
            sb.AppendLine("<a href=\"" + GetTinyUrl(templateEmail, "text/plain", true, "~/login") + "\">Log in</a>");
            sb.AppendLine("to access your account and update your password.");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
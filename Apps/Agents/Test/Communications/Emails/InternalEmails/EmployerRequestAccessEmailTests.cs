using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.InternalEmails
{
    [TestClass]
    public class EmployerRequestAccessEmailTests
        : EmailTests
    {
        private const string Subject = "Employer access request";
        private const string ContactName = "Apu";
        private const string ContactPhone = "55555555";
        private const string ContactEmail = "apu@kwikemart.com";
        private const string CompanyName = "Kwik-E-Mart";
        private const string Message = "Let me in";

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Send.

            return new EmployerRequestAccessEmail(ContactName, ContactPhone, ContactEmail, CompanyName, Message);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Send.

            var communication = new EmployerRequestAccessEmail(ContactName, ContactPhone, ContactEmail, CompanyName, Message);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, ClientServicesInbox);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(Message)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmptyMessage()
        {
            // Send.

            var communication = new EmployerRequestAccessEmail(ContactName, ContactPhone, ContactEmail, CompanyName, null);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, ClientServicesInbox);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(null)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetContent(string message)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>");
            sb.AppendLine("  An employer or recruiter is requesting access to the LinkMe database.");
            sb.AppendLine("  Their details are below:");
            sb.AppendLine("</p>");
            sb.AppendLine("<dl>");
            sb.AppendLine("  <dt>Contact name:</dt>");
            sb.AppendLine("  <dd>" + ContactName + "</dd>");
            sb.AppendLine("  <dt>Contact phone:</dt>");
            sb.AppendLine("  <dd>" + ContactPhone + "</dd>");
            sb.AppendLine("  <dt>Email address:</dt>");
            sb.AppendLine("  <dd>" + ContactEmail + "</dd>");
            sb.AppendLine("  <dt>Company name:</dt>");
            sb.AppendLine("  <dd>" + CompanyName + "</dd>");
            sb.AppendLine("</dl>");
            sb.AppendLine();

            if (message != null)
            {
                sb.AppendLine("<p>The following message was entered by the user:</p>");
                sb.AppendLine("<p>" + message + "</p>");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
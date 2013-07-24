using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class ExpiringJobAdCreditsEmailTests
        : EmailTests
    {
        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Create an employer.

            var employer = CreateEmployer();

            // Send.

            return new ExpiringJobAdCreditsEmail(employer, null, 20);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Send.

            var communication = new ExpiringJobAdCreditsEmail(employer, null, 20);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, employer, GetContent(communication, employer, 20)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject()
        {
            return "You have unused LinkMe job ad credits";
        }

        private string GetContent(TemplateEmail templateEmail, IRegisteredUser employer, int quantity)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Hi " + employer.FirstName + "</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Thank you for using LinkMe for your job ad posting");
            sb.AppendLine("  – our records show that you have " + quantity + " job ad credits");
            sb.AppendLine("  left in your account that are due to expire in one month.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  To use the remaining job ad credits, please");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/employers/login") + "\">log in</a>");
            sb.AppendLine("  to LinkMe to post your advertisements");
            sb.AppendLine("  for your current employment opportunities.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  To discuss further purchase options,");
            sb.AppendLine("  please contact me on 1800 LINK ME at your earliest convenience.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  We look forward to seeing you on our site again soon.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}
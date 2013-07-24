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
    public class ExpiringUnlimitedContactCreditsEmailTests
        : EmailTests
    {
        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Create an employer.

            var employer = CreateEmployer();

            // Send.

            return new ExpiringUnlimitedContactCreditsEmail(employer, CreateAdministrator());
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer.

            var employer = CreateEmployer();
            var administrator = CreateAdministrator();

            // Send.

            var communication = new ExpiringUnlimitedContactCreditsEmail(employer, administrator);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer, null, administrator);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, employer, GetContent(employer, administrator)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject()
        {
            return "Your unlimited LinkMe account will expire in one month";
        }

        private static string GetContent(IRegisteredUser employer, IAdministrator administrator)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Hi " + employer.FirstName + "</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Thank you for using LinkMe for your candidate sourcing");
            sb.AppendLine("  - our records show that your unlimited access to LinkMe is due to expire in 1 month.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  To continue uninterrupted access to the LinkMe database for your candidate");
            sb.AppendLine("  sourcing and management,");
            sb.AppendLine("  please contact me on 1800 LINK ME or alternatively email me at");
            sb.AppendLine("  <a href=\"mailto:" + administrator.EmailAddress.Address + "\">" + administrator.EmailAddress.Address + "</a>");
            sb.AppendLine("  to discuss continuing your subscription,");
            sb.AppendLine("  if you have not done so already.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  I look forward to speaking with you about continuing your LinkMe access.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}
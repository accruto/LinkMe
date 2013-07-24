using System.Net.Mime;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class LowContactCreditsEmailTests
        : EmailTests
    {
        public override TemplateEmail GeneratePreview(Community community)
        {
            return new LowContactCreditsEmail(CreateEmployer(), null, 5);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            const int quantity = 5;

            // Create an employer.

            var employer = CreateEmployer();

            // Send the email.

            var templateEmail = new LowContactCreditsEmail(employer, null, quantity);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject());
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(templateEmail, employer, GetContent(templateEmail, employer, quantity)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject()
        {
            return "A reminder from LinkMe: Your remaining 'candidate contact credits' are running low";
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient employer, int quantity)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Hi " + employer.FirstName + "</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Thank you for using LinkMe for your candidate sourcing");
            sb.AppendLine("  - our records show that you have " + quantity + " credits left in your account.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  This is just a reminder to plan your next purchase of candidate contact credits");
            sb.AppendLine("  for continued access to LinkMe's resume database candidates.");
            sb.AppendLine("  To purchase more credits,");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/employers/products/neworder") + "\">click here</a>");
            sb.AppendLine("  to login and use our secure credit card transaction process.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Did you know that LinkMe offers subscriptions for unlimited access");
            sb.AppendLine("  to the resume database for larger volume users?");
            sb.AppendLine("  Call us on 1800 546 563 to find out how an annual subscription to LinkMe may");
            sb.AppendLine("  be more cost effective for your organisation.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  We look forward to seeing you on our site again soon.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}

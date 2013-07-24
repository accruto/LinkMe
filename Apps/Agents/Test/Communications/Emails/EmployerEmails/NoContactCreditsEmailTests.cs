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
    public class NoContactCreditsEmailTests
        : EmailTests
    {
        public override TemplateEmail GeneratePreview(Community community)
        {
            return new NoContactCreditsEmail(CreateEmployer(), null);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Send the email.

            var templateEmail = new NoContactCreditsEmail(employer, null);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject());
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(templateEmail, employer, GetContent(templateEmail, employer)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject()
        {
            return "A notice from LinkMe: You have no remaining 'candidate contact credits'";
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient employer)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Hi " + employer.FirstName + "</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Thank you for using LinkMe for your candidate sourcing");
            sb.AppendLine("  - our records show that you have no remaining contact credits in your account.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  To purchase more candidate contact credits for continued access to LinkMe's");
            sb.AppendLine("  resume database candidates,");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/employers/products/neworder") + "\">click here</a>.");
            sb.AppendLine("  All data associated with your account,");
            sb.AppendLine("  such as folders and saved searches, will remain available to you.");
            sb.AppendLine("  Once logged in, you can use our secure credit card transaction process");
            sb.AppendLine("  to purchase more credits.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Did you know that LinkMe offers subscriptions for unlimited access");
            sb.AppendLine("  to the resume database for larger volume users?");
            sb.AppendLine("  Call us on 1800 546 563 to find out how an annual subscription");
            sb.AppendLine("  to LinkMe may be more cost effective for your organisation.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  We look forward to seeing you on our site again soon.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}

using System.Net.Mime;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class NewEmployerWelcomeEmailTests
        : EmailTests
    {
        private const int Candidates = 23;

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Create an employer.

            var employer = CreateEmployer();

            // Send the email.

            return new NewEmployerWelcomeEmail(employer, employer.GetLoginId(), employer.GetPassword(), Candidates);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Send the email.

            var templateEmail = new NewEmployerWelcomeEmail(employer, employer.GetLoginId(), employer.GetPassword(), Candidates);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject(employer));
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(templateEmail, employer, GetContent(templateEmail, employer, employer.GetLoginId(), employer.GetPassword())));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject(ICommunicationRecipient employer)
        {
            return "LinkMe login details for " + employer.FullName;
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient employer, string loginId, string password)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Dear " + employer.FirstName + ",</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Welcome to LinkMe, Australia's leading online career network,");
            sb.AppendLine("  with " + Candidates + " candidates and growing!");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Your login ID: " + loginId + "<br />");
            sb.AppendLine();
            sb.AppendLine("  Your password: " + password + "</p>");
            sb.AppendLine();

            var tinyUrl = GetTinyUrl(templateEmail, true, "~/employers/login", "returnUrl", new ReadOnlyApplicationUrl("~/accounts/changepassword").PathAndQuery);

            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + tinyUrl + "\">");
            sb.AppendLine("    Log in</a>");
            sb.AppendLine("  now to change your password.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <h3>Key LinkMe functions:</h3>");
            sb.AppendLine("  <ul>");
            sb.AppendLine("    <li>");
            sb.AppendLine("      Email alerts");
            sb.AppendLine("      - keep up to date with the freshest candidates");
            sb.AppendLine("      <p />");
            sb.AppendLine("    </li>");
            sb.AppendLine("    <li>");
            sb.AppendLine("      Advanced search");
            sb.AppendLine("      - refine your search for more relevant candidates");
            sb.AppendLine("      <p />");
            sb.AppendLine("    </li>");
            sb.AppendLine("    <li>");
            sb.AppendLine("      Keyword search");
            sb.AppendLine("      - find candidates with transferable skills for your hard to fill roles");
            sb.AppendLine("      <p />");
            sb.AppendLine("    </li>");
            sb.AppendLine("    <li>");
            sb.AppendLine("      Find passive candidates");
            sb.AppendLine("      - candidates you might not otherwise have access to");
            sb.AppendLine("    </li>");
            sb.AppendLine("  </ul>");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  If you would like more information, visit our");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, false, "~/employers/resources") + "\">resources</a>");
            sb.AppendLine("  section.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}
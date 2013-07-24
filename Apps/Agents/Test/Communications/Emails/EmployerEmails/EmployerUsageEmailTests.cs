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
    public class EmployerUsageEmailTests
        : EmailTests
    {
        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            const int newCandidates = 12500;

            // Create an employer.

            var employer = CreateEmployer();

            // Send.

            return new EmployerUsageEmail(employer, newCandidates);
        }

        [TestMethod]
        public void TestContents()
        {
            const int newCandidates = 12500;

            // Create an employer.

            var employer = CreateEmployer();

            // Send.

            var templateEmail = new EmployerUsageEmail(employer, newCandidates);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject(newCandidates));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, employer, GetContent(templateEmail, employer, newCandidates)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject(int newCandidates)
        {
            return newCandidates + " fresh candidates";
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient employer, int newCandidates)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Hi " + employer.FirstName + "</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  " + newCandidates + " candidates have uploaded");
            sb.AppendLine("  or updated their resume since you last visited our web site.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Make your life easier by having the freshest and hard-to-find");
            sb.AppendLine("  candidates delivered direct to your inbox -");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/search/candidates") + "\">set");
            sb.AppendLine("    up candidate alerts now</a>.");
            sb.AppendLine("  View our handy");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, false, "~/employers/resources/EmployerWelcomePack.pdf") + "\">tips</a>");
            sb.AppendLine("  to get more out of your subscription.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Take the hassle out of candidate sourcing.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}
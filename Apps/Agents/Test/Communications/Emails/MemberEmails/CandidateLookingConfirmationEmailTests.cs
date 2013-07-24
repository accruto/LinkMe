using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberEmails
{
    [TestClass]
    public class CandidateLookingConfirmationEmailTests
        : EmailTests
    {
        private const string Subject = "Your LinkMe status: Please confirm that you are still Actively Looking for Work";

        public override TemplateEmail GeneratePreview(Community community)
        {
            return new CandidateLookingConfirmationEmail(CreateMember(community));
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create a member.

            var member = CreateMember();

            // Send the email.

            var templateEmail = new CandidateLookingConfirmationEmail(member);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, member, GetContent(templateEmail, member)));
            AssertCompatibleAddresses(email);
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient member)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>");
            sb.AppendLine("  Dear " + member.FirstName + ",");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  You are currently listed on LinkMe as 'Actively Looking for Work'.");
            sb.AppendLine("  To provide employers with your most accurate work status you should either:");
            sb.AppendLine("</p>");
            sb.AppendLine("<ol>");
            sb.AppendLine("  <li>");
            sb.AppendLine("    Confirm your");
            sb.AppendLine("    <a href=\"" + GetTinyUrl(templateEmail, true, "~/members/profile/status/update", "status", "ActivelyLooking") + "\">Actively Looking for Work</a>");
            sb.AppendLine("    status, or");
            sb.AppendLine("    <p />");
            sb.AppendLine("  </li>");
            sb.AppendLine("  <li>");
            sb.AppendLine("    <a href=\"" + GetTinyUrl(templateEmail, true, "~/members/profile/status/update") + "\">Change your work status</a>");
            sb.AppendLine("    to reflect your current situation, or");
            sb.AppendLine("    <p />");
            sb.AppendLine("  </li>");
            sb.AppendLine("  <li>");
            sb.AppendLine("    Do nothing and your work status will be changed automatically to");
            sb.AppendLine("    'Not Looking but Happy to Talk'.");
            sb.AppendLine("  </li>");
            sb.AppendLine("</ol>");
            sb.AppendLine("<p>");
            sb.AppendLine("  By ensuring your work status is up to date,");
            sb.AppendLine("  you increase your chances of being found and contacted by employers.");
            sb.AppendLine("  Employers and recruiters can search for Immediately Available or active jobseekers,");
            sb.AppendLine("  and can make their decisions to contact based on this information.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Please note: if you confirm your Actively Looking for Work status,");
            sb.AppendLine("  this will need to be confirmed on a monthly basis.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}

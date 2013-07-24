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
    public class CandidatePassiveNotificationEmailTests
        : EmailTests
    {
        private const string Subject = "Your LinkMe work status has been automatically updated";

        public override TemplateEmail GeneratePreview(Community community)
        {
            return new CandidatePassiveNotificationEmail(CreateMember(community));
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create a member.

            var member = CreateMember();

            // Send the email.

            var templateEmail = new CandidatePassiveNotificationEmail(member);
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
            sb.AppendLine("  Your work status on LinkMe has been changed from 'Actively Looking for Work'");
            sb.AppendLine("  to 'Not Looking but Happy to Talk'.");
            sb.AppendLine("  This means that an employer who views your resume may think that you are no longer looking for work,");
            sb.AppendLine("  and so may not contact you for opportunities.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  If this is not accurate, you can");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/members/profile/status/update") + "\">change your work status</a>");
            sb.AppendLine("  to:");
            sb.AppendLine("</p>");
            sb.AppendLine("<ol>");
            sb.AppendLine("  <li>'Actively Looking for Work', or<p /></li>");
            sb.AppendLine("  <li>'Immediately Available', or<p /></li>");
            sb.AppendLine("  <li>'Not Looking for Work'</li>");
            sb.AppendLine("</ol>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Please note that if you indicate that you are Immediately Available");
            sb.AppendLine("  or Actively Looking for Work,");
            sb.AppendLine("  your status will have to be confirmed regularly.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}

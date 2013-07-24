using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberEmails
{
    [TestClass]
    public class ResumeAttachmentEmailTests
        : EmailTests
    {
        private const string Subject = "Your Resume.";

        public override TemplateEmail GeneratePreview(Community community)
        {
            // Create a member.

            var member = CreateMember(community);

            // Create a resume.

            var resume = new ResumeFile { Contents = "This is my resume", FileName = "Test" };

            // Send the email.

            return new ResumeAttachmentEmail(member, resume);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create a member.

            var member = CreateMember();

            // Create a resume.

            var resume = new ResumeFile { Contents = "This is my resume", FileName = "Test.doc" };

            // Send the email.

            var templateEmail = new ResumeAttachmentEmail(member, resume);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, member, GetContent(templateEmail, member)));
            email.AssertAttachment(resume.FileName, "application/msword");
            AssertCompatibleAddresses(email);
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient member)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Dear " + member.FirstName + ",</p>");
            sb.AppendLine("<p>As requested, attached is a copy of your resume.</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  With a fresh resume you're twice as likely to be found by employers");
            sb.AppendLine("  for your dream job.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/members/profile") + "\">Update your resume now</a>");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}
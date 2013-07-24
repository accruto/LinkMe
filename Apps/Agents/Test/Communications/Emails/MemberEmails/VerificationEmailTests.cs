using System.Net.Mime;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberEmails
{
    [TestClass]
    public class VerificationEmailTests
        : EmailTests
    {
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        public override TemplateEmail GeneratePreview(Community community)
        {
            var member = CreateMember(community);

            // Create an email verification.

            var emailVerification = new EmailVerification {UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address};
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Send the email.

            return new VerificationEmail(member, emailVerification);
        }

        [TestMethod]
        public void TestMember()
        {
            // Create a member.

            var member = CreateMember();

            // Create an email verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Send the email.

            var templateEmail = new VerificationEmail(member, emailVerification);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(GetSubject());
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(templateEmail, member, GetContent(templateEmail, member, emailVerification)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject()
        {
            return "Please verify your email address for your career network";
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient member, EmailVerification emailVerification)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Dear " + member.FirstName + ",</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  In order to protect your privacy");
            sb.AppendLine("  we need you to verify that the email address you have supplied is correct.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  More importantly, employers may not be able to send possible job opportunities");
            sb.AppendLine("  to you if your email address is not verified.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, 0, false, "~/accounts/verification", "verificationCode", emailVerification.VerificationCode) + "\">");
            sb.AppendLine("    Please verify your email address now");
            sb.AppendLine("  </a>");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  If this link doesn't work, please copy and paste the following link");
            sb.AppendLine("  into your browser:");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, 1, false, "~/accounts/verification", "verificationCode", emailVerification.VerificationCode) + "\">");
            sb.AppendLine("    " + GetTinyUrl(templateEmail, 2, false, "~/accounts/verification", "verificationCode", emailVerification.VerificationCode));
            sb.AppendLine("  </a>");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}
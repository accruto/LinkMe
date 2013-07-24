using System.Net.Mime;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberEmails
{
    [TestClass]
    public class ReactivationEmailTests
        : EmailTests
    {
        private const string Subject = "Please verify your email address for your career network";
        private bool _isMonashCommunity;

        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        public override TemplateEmail GeneratePreview(Community community)
        {
            // Create a member.

            var member = CreateMember(community);

            // Create an email verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Send the email.

            return new ReactivationEmail(member, emailVerification);
        }

        [TestMethod]
        public void TestMember()
        {
            // Create a member.

            var member = CreateMember();
            
            // Create an email emailVerification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Send the email.

            var templateEmail = new ReactivationEmail(member, emailVerification);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(templateEmail, member, GetContent(templateEmail, member, emailVerification)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestMemberMonashGsbCommunity()
        {
            // Create a member.

            var community = TestCommunity.MonashGsb.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var member = CreateMember(community);
            _isMonashCommunity = true;

            // Create an email verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Send the email.

            var templateEmail = new ReactivationEmail(member, emailVerification);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(templateEmail, member, GetContent(templateEmail, member, emailVerification)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient member, EmailVerification emailVerification)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Dear " + member.FirstName + ",</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  In order to protect your privacy");
            sb.AppendLine("  we need you to verify that the email address you have supplied is correct.");
            sb.AppendLine("  You will not be able to access the full functionality of the web site");
            sb.AppendLine("  until this is done.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  More importantly, employers may not be able to send possible job opportunities");
            sb.AppendLine("  to you if your email address is not verified.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, 0, false, "~/accounts/activation", "activationCode", emailVerification.VerificationCode) + "\">");
            sb.AppendLine("    Please verify your email address now");
            sb.AppendLine("  </a>");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  If this link doesn't work, please copy and paste the following link");
            sb.AppendLine("  into your browser:");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, 1, false, "~/accounts/activation", "activationCode", emailVerification.VerificationCode) + "\">");
            sb.AppendLine("    " + GetTinyUrl(templateEmail, 2, false, "~/accounts/activation", "activationCode", emailVerification.VerificationCode));
            sb.AppendLine("  </a>");
            sb.AppendLine("</p>");
            return sb.ToString();
        }

        protected override void GetMemberSignature(TemplateEmail email, StringBuilder sb)
        {
            if (_isMonashCommunity)
            {
                sb.AppendLine("        <br />Thanks,");
                sb.AppendLine("        <br />Your Career Networking team");
                sb.AppendLine("        <br />");
                sb.AppendLine("        <br />For support please");
                sb.AppendLine("        <a href=\"" + GetTinyUrl(email, false, "~/contactus") + "\">contact us</a>.");
                sb.AppendLine("        <p>This service brought to you by</p>");
                sb.AppendLine("        <p>");
                sb.AppendLine("          <img src=\"" + InsecureRootPath + "email/logo-for-emails.gif\" alt=\"LinkMe\" />");
                sb.AppendLine("        </p>");
            }
            else
            {
                base.GetMemberSignature(email, sb);
            }
        }
    }
}
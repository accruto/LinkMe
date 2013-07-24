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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberEmails
{
    [TestClass]
    public class ActivationEmailTests
        : EmailTests
    {
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        private enum ActiveCommunity
        {
            Monash = 1,
            Finsia = 2
        };

        private ActiveCommunity _activeCommunity;

        public override TemplateEmail GeneratePreview(Community community)
        {
            var member = CreateMember(community);

            // Create an email verification.

            var emailVerification = new EmailVerification {UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address};
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Send the email.

            return new ActivationEmail(member, emailVerification);
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

            var templateEmail = new ActivationEmail(member, emailVerification);
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

        [TestMethod]
        public void TestMemberMonashGsbCommunity()
        {
            // Create a member.

            var community = TestCommunity.MonashGsb.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var member = CreateMember(community);
            _activeCommunity = ActiveCommunity.Monash;

            // Create an email verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Send the email.

            var templateEmail = new ActivationEmail(member, emailVerification);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, member, GetContent(templateEmail, member, emailVerification)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestMemberFinsiaCommunity()
        {
            // Create a member.

            var community = TestCommunity.Finsia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var member = CreateMember(community);
            _activeCommunity = ActiveCommunity.Finsia;

            // Create an email verification.

            var emailVerification = new EmailVerification { UserId = member.Id, EmailAddress = member.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            // Send the email.

            var templateEmail = new ActivationEmail(member, emailVerification);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, member, GetContent(templateEmail, member, emailVerification)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private string GetSubject()
        {
            switch (_activeCommunity)
            {
                case ActiveCommunity.Monash:
                    return "Activate your Monash University Business and Economics networking portal account";

                case ActiveCommunity.Finsia:
                    return "Activate your Finsia Career Networking account";

                default:
                    return "Welcome to LinkMe: please confirm your details";
            }
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient member, EmailVerification emailVerification)
        {
            var sb = new StringBuilder();

            switch (_activeCommunity)
            {
                case ActiveCommunity.Monash:
                    sb.AppendLine("<p>");
                    sb.AppendLine("  <img src=\"" + InsecureRootPath + "themes/communities/monash/gsb/img/gsb-logo-1line-small.jpg\" alt=\"Monash University Graduate School of Business\" />");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>Dear " + member.FirstName + ",</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  Welcome to the Monash University Business and Economics networking portal.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>You recently joined with this email address.</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, false, "~/accounts/activation", "activationCode", emailVerification.VerificationCode) + "\">");
                    sb.AppendLine("    Please activate your account");
                    sb.AppendLine("  </a>");
                    sb.AppendLine("</p>");
                    break;

                case ActiveCommunity.Finsia:
                    sb.AppendLine("<p>Dear " + member.FirstName + ",</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  Welcome to the Finsia Career Network,");
                    sb.AppendLine("  your online financial services community.");
                    sb.AppendLine("  We hope you enjoy the many benefits it provides,");
                    sb.AppendLine("  including our industry networking forums,");
                    sb.AppendLine("  career management tools and resources,");
                    sb.AppendLine("  and access to the hidden jobs market.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  To activate your account, please ");
                    sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, 0, false, "~/accounts/activation", "activationCode", emailVerification.VerificationCode) + "\">");
                    sb.AppendLine("    verify your email address is correct");
                    sb.AppendLine("  </a>");
                    sb.AppendLine("   by clicking on the link.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  We've included below some housekeeping tips to help");
                    sb.AppendLine("  you make the most of your profile.");
                    sb.AppendLine("  If you run into any trouble or have any questions,");
                    sb.AppendLine("  please send an email to ");
                    sb.AppendLine("  <a href=\"mailto:careernetwork@finsia.com\">careernetwork@finsia.com</a>");
                    sb.AppendLine("  and we'll get back to you as soon as we can.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("      </div>");
                    sb.AppendLine("      <div class=\"signature\" style=\"padding-left: 9px; font-family: Arial, Helvetica, sans-serif; color: gray; font-size: 9pt; padding-top:1em;\">");
                    sb.AppendLine("        <p>Cheers,</p>");
                    sb.AppendLine("        <p>");
                    sb.AppendLine("          The Finsia Career Network team.<br />");
                    sb.AppendLine("          <a href=\"mailto:careernetwork@finsia.com\">careernetwork@finsia.com </a>");
                    sb.AppendLine("        </p>");
                    sb.AppendLine("      </div>");
                    sb.AppendLine("      <div class=\"body\">");
                    sb.AppendLine("<p>***************************************************</p>");
                    sb.AppendLine("<p><strong>Your profile</strong></p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  Once you verify your email,");
                    sb.AppendLine("  you will be asked to update your career profile.");
                    sb.AppendLine("  This includes your employment history,");
                    sb.AppendLine("  desired job and salary,");
                    sb.AppendLine("  career objectives and your education and qualifications.<br />");
                    sb.AppendLine("  You can add as much or as little detail in these fields as you choose.");
                    sb.AppendLine("  Just remember, the more information you provide,");
                    sb.AppendLine("  and the more often you update it,");
                    sb.AppendLine("  the more chance you have for employers finding you.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p><strong>Your security</strong></p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  You control whether your resume is accessible");
                    sb.AppendLine("  to potential employers by selecting or changing");
                    sb.AppendLine("  your visibility at any time.");
                    sb.AppendLine("  Hide personal information including your name");
                    sb.AppendLine("  and current and previous employers,");
                    sb.AppendLine("  or make these public to your network - it's your choice.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  Select \"Actively looking\"; \"Not looking, but happy to talk\";");
                    sb.AppendLine("  or \"Not looking\" to ensure your profile receives the attention");
                    sb.AppendLine("  you desire from potential employers.");
                    sb.AppendLine("  The Finsia Career Network will never reveal your personal");
                    sb.AppendLine("  address or email address to employers unless you actually");
                    sb.AppendLine("  apply for a job they have advertised.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  You're also free to join and un-join networking groups,");
                    sb.AppendLine("  job alerts and updates quickly and easily via your profile.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p><strong>Support</strong></p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  If you need help with your profile,");
                    sb.AppendLine("  would like to report any inappropriate activity");
                    sb.AppendLine("  or have any questions about Finsia's Career Network, please");
                    sb.AppendLine("  email <a href=\"mailto:careernetwork@finsia.com\">careernetwork@finsia.com</a>.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>***************************************************</p>");
                    break;

                default:
                    sb.AppendLine("<p>Dear " + member.FirstName + ",</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  You recently joined LinkMe by either uploading your resume,");
                    sb.AppendLine("  applying for a job, joining a group or joining a network.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  Please");
                    sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, 0, false, "~/accounts/activation", "activationCode", emailVerification.VerificationCode) + "\">");
                    sb.AppendLine("    click here");
                    sb.AppendLine("  </a>");
                    sb.AppendLine("  to confirm your details.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  If this link doesn't work, please copy and paste the following link");
                    sb.AppendLine("  into your browser:");
                    sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, 1, false, "~/accounts/activation", "activationCode", emailVerification.VerificationCode) + "\">");
                    sb.AppendLine("    " + GetTinyUrl(templateEmail, 2, false, "~/accounts/activation", "activationCode", emailVerification.VerificationCode));
                    sb.AppendLine("  </a>");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  LinkMe is an online career network and resume database focused on you,");
                    sb.AppendLine("  for the life of your career.");
                    sb.AppendLine("  By having your resume on our site and your details up to date,");
                    sb.AppendLine("  you have the potential to be contacted by the many recruiters");
                    sb.AppendLine("  and employers searching for staff.");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  LinkMe is a quick and easy way to:");
                    sb.AppendLine("  <ul>");
                    sb.AppendLine("    <li>");
                    sb.AppendLine("      Be contacted by potential employers for job opportunities you may");
                    sb.AppendLine("      have otherwise missed<p />");
                    sb.AppendLine("    </li>");
                    sb.AppendLine("    <li>");
                    sb.AppendLine("      Have jobs sent to you that match your profile<p />");
                    sb.AppendLine("    </li>");
                    sb.AppendLine("    <li>");
                    sb.AppendLine("      Keep your resume updated and ready to use for the duration of");
                    sb.AppendLine("      your career<p />");
                    sb.AppendLine("    </li>");
                    sb.AppendLine("    <li>");
                    sb.AppendLine("      Make your resume either visible or invisible to employers<p />");
                    sb.AppendLine("    </li>");
                    sb.AppendLine("    <li>");
                    sb.AppendLine("      Search and apply for jobs<p />");
                    sb.AppendLine("    </li>");
                    sb.AppendLine("    <li>");
                    sb.AppendLine("      Network with friends and colleagues who can help you progress in");
                    sb.AppendLine("      your career<p />");
                    sb.AppendLine("    </li>");
                    sb.AppendLine("    <li>");
                    sb.AppendLine("      Access free career resources such as videos, sample resumes,");
                    sb.AppendLine("      personality tests and suggested connections<p />");
                    sb.AppendLine("    </li>");
                    sb.AppendLine("    <li>");
                    sb.AppendLine("      Keep your details private");
                    sb.AppendLine("    </li>");
                    sb.AppendLine("  </ul>");
                    sb.AppendLine("</p>");
                    sb.AppendLine("<p>");
                    sb.AppendLine("  We look forward to helping you throughout the life of your career.");
                    sb.AppendLine("</p>");
                    break;
            }

            return sb.ToString();
        }

        protected override void GetMemberSignature(TemplateEmail email, StringBuilder sb)
        {
            switch (_activeCommunity)
            {
                case ActiveCommunity.Monash:
                    sb.AppendLine("        <br />Thanks,");
                    sb.AppendLine("        <br />Your Career Networking team");
                    sb.AppendLine("        <br />");
                    sb.AppendLine("        <br />For support please");
                    sb.AppendLine("        <a href=\"" + GetTinyUrl(email, false, "~/contactus") + "\">contact us</a>.");
                    sb.AppendLine("        <p>This service brought to you by</p>");
                    sb.AppendLine("        <p>");
                    sb.AppendLine("          <img src=\"" + InsecureRootPath + "email/logo-for-emails.gif\" alt=\"LinkMe\" />");
                    sb.AppendLine("        </p>");
                    break;

                case ActiveCommunity.Finsia:
                    sb.AppendLine("        <p>This service is provided by");
                    sb.AppendLine("          <img src=\"" + InsecureRootPath + "email/logo-for-emails.gif\" alt=\"LinkMe\" />");
                    sb.AppendLine("        </p>");
                    sb.AppendLine("        <p>");
                    sb.AppendLine("          Copyright 2008 Finsia - Financial Services Institute of Australasia.");
                    sb.AppendLine("          All rights reserved");
                    sb.AppendLine("        </p>");
                    break;

                default:
                    base.GetMemberSignature(email, sb);
                    break;
            }
        }
    }
}
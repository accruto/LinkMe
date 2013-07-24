using System;
using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberToMemberNotifications
{
    [TestClass]
    public class RepresentativeInvitationEmailTests
        : EmailTests
    {
        private const string MessageText = "Do it now";
        private const string RequiresEncodingMessageText = @"Do & it
now";

        private readonly IRepresentativeInvitationsCommand _representativeInvitationsCommand = Resolve<IRepresentativeInvitationsCommand>();
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        public override TemplateEmail GeneratePreview(Community community)
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1, community != null ? community.Id : (Guid?)null);
            var invitation = new RepresentativeInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText };
            _representativeInvitationsCommand.CreateInvitation(invitation);

            return new RepresentativeInvitationEmail(invitee, inviter, invitation, null);
        }

        [TestMethod]
        public void TestNotificationsOff()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new RepresentativeInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText };
            _representativeInvitationsCommand.CreateInvitation(invitation);

            // No settings so notification should get through.

            _emailsCommand.TrySend(new RepresentativeInvitationEmail(invitee, inviter, invitation, null));
            _emailServer.AssertEmailSent();

            // Turn off.

            var category = _settingsQuery.GetCategory("MemberToMemberNotification");
            _settingsCommand.SetFrequency(invitee.Id, category.Id, Frequency.Never);
            _emailsCommand.TrySend(new RepresentativeInvitationEmail(invitee, inviter, invitation, null));
            _emailServer.AssertNoEmailSent();

            // Turn on.

            _settingsCommand.SetFrequency(invitee.Id, category.Id, Frequency.Immediately);
            _emailsCommand.TrySend(new RepresentativeInvitationEmail(invitee, inviter, invitation, null));
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new RepresentativeInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText };
            _representativeInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new RepresentativeInvitationEmail(invitee, inviter, invitation, null);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailDeactivatedMember()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);
            invitee.IsActivated = false;
            _memberAccountsCommand.UpdateMember(invitee);

            var invitation = new RepresentativeInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = MessageText };
            _representativeInvitationsCommand.CreateInvitation(invitation);

            var emailVerification = new EmailVerification { UserId = invitee.Id, EmailAddress = invitee.GetBestEmailAddress().Address };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            var templateEmail = new RepresentativeInvitationEmail(invitee, inviter, invitation, emailVerification);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, emailVerification);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsNullMessageText()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new RepresentativeInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = null };
            _representativeInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new RepresentativeInvitationEmail(invitee, inviter, invitation, null);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsEmptyMessageText()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new RepresentativeInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = string.Empty };
            _representativeInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new RepresentativeInvitationEmail(invitee, inviter, invitation, null);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailContentsRequiresEncodingMessageText()
        {
            // Create the invitation.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            inviter.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.None;
            _memberAccountsCommand.UpdateMember(inviter);

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            var invitation = new RepresentativeInvitation { InviterId = inviter.Id, InviteeId = invitee.Id, Text = RequiresEncodingMessageText };
            _representativeInvitationsCommand.CreateInvitation(invitation);

            var templateEmail = new RepresentativeInvitationEmail(invitee, inviter, invitation, null);
            _emailsCommand.TrySend(templateEmail);

            // Check. The email should have been sent as part of the invitation creation.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            AssertHtmlView(email, templateEmail, inviter, invitee, invitation, null);
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject()
        {
            return "Nominated representative invitation";
        }

        private void AssertHtmlView(MockEmail email, TemplateEmail templateEmail, ICommunicationRecipient inviter, RegisteredUser invitee, Request invitation, EmailVerification emailVerification)
        {
            email.AssertHtmlView(GetBody(templateEmail, invitee, GetContent(templateEmail, inviter, invitee, invitation, emailVerification)));
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient inviter, ICommunicationRecipient invitee, Request invitation, EmailVerification emailVerification)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Hi " + invitee.FirstName + "</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  " + inviter.FullName + " has sent you a request to represent them");
            sb.AppendLine("  and respond to enquiries from employers on their behalf.");
            sb.AppendLine("</p>");

            sb.AppendLine("<p>");
            sb.AppendLine("  By accepting this request you will receive any communication");
            sb.AppendLine("  sent by employers relating to employment opportunities for");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/members/friends/ViewFriend.aspx", "friendId", inviter.Id.ToString()) + "\">");
            sb.AppendLine("    " + inviter.FullName);
            sb.AppendLine("  </a>.");
            sb.AppendLine("</p>");
            sb.AppendLine();

            // Show individual text if set.

            if (!string.IsNullOrEmpty(invitation.Text))
            {
                sb.AppendLine("<p>");
                sb.AppendLine("  " + inviter.FirstName.MakeNamePossessive() + " personal message:");
                sb.AppendLine("  <br />");
                sb.AppendLine("  " + HtmlUtil.LineBreaksToHtml(HttpUtility.HtmlEncode(invitation.Text)));
                sb.AppendLine("</p>");
                sb.AppendLine();
            }

            // Link depends on whether the invitee is activated.

            sb.AppendLine("<p>");
            sb.AppendLine();
            if (emailVerification == null)
            {
                sb.AppendLine("  <a class=\"major-action\" href=\"" + GetTinyUrl(templateEmail, true, "~/members/Friends/Invitations.aspx") + "\">Respond to " + inviter.FirstName.MakeNamePossessive() + " invitation</a>");
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine("  <a class=\"major-action\" href=\"" + GetTinyUrl(templateEmail, false, "~/accounts/activation", "activationCode", emailVerification.VerificationCode, "isInvite", "true") + "\">Reactivate");
                sb.AppendLine("  your account and respond to " + inviter.FirstName.MakeNamePossessive() + " invitation</a>");
                sb.AppendLine();
            }
            sb.AppendLine("</p>");

            return sb.ToString();
        }
    }
}
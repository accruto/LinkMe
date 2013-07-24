using System;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberToMemberNotifications
{
    [TestClass]
    public class FriendInvitationConfirmationEmailTests
        : EmailTests
    {
        public override TemplateEmail GeneratePreview(Community community)
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1, community != null ? community.Id : (Guid?)null);

            // Send the email.

            return new FriendInvitationConfirmationEmail(inviter, invitee);
        }

        [TestMethod]
        public void TestNotificationsOff()
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);

            // No settings so notification should get through.

            _emailsCommand.TrySend(new FriendInvitationConfirmationEmail(inviter, invitee));
            _emailServer.AssertEmailSent();

            // Turn off.

            var category = _settingsQuery.GetCategory("MemberToMemberNotification");
            _settingsCommand.SetFrequency(inviter.Id, category.Id, Frequency.Never);
            _emailsCommand.TrySend(new FriendInvitationConfirmationEmail(inviter, invitee));
            _emailServer.AssertNoEmailSent();

            // Turn on.

            _settingsCommand.SetFrequency(inviter.Id, category.Id, Frequency.Immediately);
            _emailsCommand.TrySend(new FriendInvitationConfirmationEmail(inviter, invitee));
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestEmailContents()
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);

            // Send the email.

            var templateEmail = new FriendInvitationConfirmationEmail(inviter, invitee);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, inviter);
            email.AssertSubject(GetSubject(invitee));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, inviter, GetContent(templateEmail, inviter, invitee)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject(ICommunicationRecipient invitee)
        {
            return invitee.FullName + " has joined your career network";
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient inviter, ICommunicationRecipient invitee)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Dear " + inviter.FirstName + ",</p>");
            sb.AppendLine("<p>" + invitee.FirstName + " " + invitee.LastName + " accepted your invitation to join your network.</p>");
            sb.AppendLine("<p>You can now see and contact people in " + invitee.FirstName + "'s own network.</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/login") + "\">Log in</a>");
            sb.AppendLine("  now to see how your network has grown!");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}
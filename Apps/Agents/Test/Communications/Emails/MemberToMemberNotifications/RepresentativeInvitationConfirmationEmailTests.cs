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
    public class RepresentativeInvitationConfirmationEmailTests
        : EmailTests
    {
        public override TemplateEmail GeneratePreview(Community community)
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1, community != null ? community.Id : (Guid?)null);

            // Send the email.

            return new RepresentativeInvitationConfirmationEmail(inviter, invitee);
        }

        [TestMethod]
        public void TestNotificationsOff()
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);

            // No settings so notification should get through.

            _emailsCommand.TrySend(new RepresentativeInvitationConfirmationEmail(inviter, invitee));
            _emailServer.AssertEmailSent();

            // Turn off.

            var category = _settingsQuery.GetCategory("MemberToMemberNotification");
            _settingsCommand.SetFrequency(inviter.Id, category.Id, Frequency.Never);
            _emailsCommand.TrySend(new RepresentativeInvitationConfirmationEmail(inviter, invitee));
            _emailServer.AssertNoEmailSent();

            // Turn on.

            _settingsCommand.SetFrequency(inviter.Id, category.Id, Frequency.Immediately);
            _emailsCommand.TrySend(new RepresentativeInvitationConfirmationEmail(inviter, invitee));
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestEmailContents()
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);

            // Send the email.

            var templateEmail = new RepresentativeInvitationConfirmationEmail(inviter, invitee);
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
            return invitee.FullName + " has become your nominated representative";
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient inviter, ICommunicationRecipient invitee)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Dear " + inviter.FirstName + ",</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  " + invitee.FullName + " has accepted your request for them to represent you");
            sb.AppendLine("  in fielding employer enquiries.");
            sb.AppendLine("  They will receive any communication from employers on your behalf.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  You can change your representative any time");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/members/friends/ViewRepresentative.aspx") + "\">here</a>.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}
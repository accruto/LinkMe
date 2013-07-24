using System;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberToMemberNotifications
{
    [TestClass]
    public class MemberMessageSentEmailTests
        : EmailTests
    {
        private const string SenderEmailAddress = "linkme1@test.linkme.net.au";
        private const string SenderFirstName = "Barney";
        private const string SenderLastName = "Gumble";
        private const string RecipientEmailAddress = "linkme2@test.linkme.net.au";
        private const string RecipientFirstName = "Homer";
        private const string RecipientLastName = "Simpson";
        private const string Subject = SenderFirstName + " sent you a message";
        private const string MessageSubject = "Message subject";
        private const string MessageText = "Message text";

        private enum ActiveCommunity
        {
            None,
            LiveInAustralia,
            Finsia
        };

        private ActiveCommunity _activeCommunity;

        public override TemplateEmail GeneratePreview(Community community)
        {
            _activeCommunity = ActiveCommunity.None;

            // Create a member.

            var sender = _memberAccountsCommand.CreateTestMember(SenderEmailAddress, SenderFirstName, SenderLastName);
            var recipient = _memberAccountsCommand.CreateTestMember(RecipientEmailAddress, RecipientFirstName, RecipientLastName, community != null ? community.Id : (Guid?)null);
            var threadId = Guid.NewGuid();
            var messageId = Guid.NewGuid();

            // Send the email.

            return new MemberMessageSentEmail(recipient, sender, threadId, messageId, MessageSubject, MessageText);
        }

        [TestMethod]
        public void TestNotificationsOff()
        {
            _activeCommunity = ActiveCommunity.None;

            // Create a member.

            var sender = _memberAccountsCommand.CreateTestMember(SenderEmailAddress, SenderFirstName, SenderLastName);
            var recipient = _memberAccountsCommand.CreateTestMember(RecipientEmailAddress, RecipientFirstName, RecipientLastName);
            var threadId = Guid.NewGuid();
            var messageId = Guid.NewGuid();

            // No settings so notification should get through.

            _emailsCommand.TrySend(new MemberMessageSentEmail(recipient, sender, threadId, messageId, MessageSubject, MessageText));
            _emailServer.AssertEmailSent();

            // Turn off.

            var category = _settingsQuery.GetCategory("MemberToMemberNotification");
            _settingsCommand.SetFrequency(recipient.Id, category.Id, Frequency.Never);
            _emailsCommand.TrySend(new MemberMessageSentEmail(recipient, sender, threadId, messageId, MessageSubject, MessageText));
            _emailServer.AssertNoEmailSent();

            // Turn on.

            _settingsCommand.SetFrequency(recipient.Id, category.Id, Frequency.Immediately);
            _emailsCommand.TrySend(new MemberMessageSentEmail(recipient, sender, threadId, messageId, MessageSubject, MessageText));
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestEmailContents()
        {
            _activeCommunity = ActiveCommunity.None;

            // Create a member.

            Member sender = _memberAccountsCommand.CreateTestMember(SenderEmailAddress, SenderFirstName, SenderLastName);
            Member recipient = _memberAccountsCommand.CreateTestMember(RecipientEmailAddress, RecipientFirstName, RecipientLastName);
            Guid threadId = Guid.NewGuid();
            var messageId = Guid.NewGuid();

            // Send the email.

            var templateEmail = new MemberMessageSentEmail(recipient, sender, threadId, messageId, MessageSubject, MessageText);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(sender, Return, recipient);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, recipient, GetContent(templateEmail, sender, threadId, messageId)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestCommunityToCommunity()
        {
            _activeCommunity = ActiveCommunity.Finsia;

            // Create a member.

            var communitySender = TestCommunity.LiveInAustralia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var communityRecipient = TestCommunity.Finsia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            var sender = _memberAccountsCommand.CreateTestMember(SenderEmailAddress, SenderFirstName, SenderLastName, communitySender.Id);
            var recipient = _memberAccountsCommand.CreateTestMember(RecipientEmailAddress, RecipientFirstName, RecipientLastName, communityRecipient.Id);
            var threadId = Guid.NewGuid();
            var messageId = Guid.NewGuid();

            // Send the email.

            var templateEmail = new MemberMessageSentEmail(recipient, sender, threadId, messageId, MessageSubject, MessageText);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(sender, Return, recipient);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, recipient, GetContent(templateEmail, sender, threadId, messageId)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestFromCommunity()
        {
            _activeCommunity = ActiveCommunity.None;

            // Create a member.

            var communitySender = TestCommunity.Finsia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var sender = _memberAccountsCommand.CreateTestMember(SenderEmailAddress, SenderFirstName, SenderLastName, communitySender.Id);
            var recipient = _memberAccountsCommand.CreateTestMember(RecipientEmailAddress, RecipientFirstName, RecipientLastName);
            var threadId = Guid.NewGuid();
            var messageId = Guid.NewGuid();

            // Send the email.

            var templateEmail = new MemberMessageSentEmail(recipient, sender, threadId, messageId, MessageSubject, MessageText);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(sender, Return, recipient);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, recipient, GetContent(templateEmail, sender, threadId, messageId)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestToCommunity()
        {
            _activeCommunity = ActiveCommunity.Finsia;

            // Create a member.

            var communityRecipient = TestCommunity.Finsia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var sender = _memberAccountsCommand.CreateTestMember(SenderEmailAddress, SenderFirstName, SenderLastName);
            var recipient = _memberAccountsCommand.CreateTestMember(RecipientEmailAddress, RecipientFirstName, RecipientLastName, communityRecipient.Id);
            var threadId = Guid.NewGuid();
            var messageId = Guid.NewGuid();

            // Send the email.

            var templateEmail = new MemberMessageSentEmail(recipient, sender, threadId, messageId, MessageSubject, MessageText);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(sender, Return, recipient);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, recipient, GetContent(templateEmail, sender, threadId, messageId)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient sender, Guid threadId, Guid messageId)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>" + sender.FirstName + " sent you a message.</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Subject: " + MessageSubject);
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  " + MessageText);
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <br />");
            sb.AppendLine("  <br />");
            sb.AppendLine("  Follow this link to");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/ui/registered/messaging/ViewThread.aspx", "threadId", threadId.ToString("n")) + "\">reply</a>.");
            sb.AppendLine("  <br />");
            sb.AppendLine("  Report as");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/members/messages/reportspam", "messageId", messageId.ToString("n")) + "\">spam</a>.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }

        protected override void GetMemberSignature(TemplateEmail email, StringBuilder sb)
        {
            switch (_activeCommunity)
            {
                case ActiveCommunity.LiveInAustralia:
                    sb.AppendLine("        <p>Cheers,</p>");
                    sb.AppendLine("        <p>Peter,<br />Live In Australia</p>");
                    sb.AppendLine("        <p>Provided by <img src=\"" + InsecureRootPath + "email/logo-for-emails.gif\" alt=\"LinkMe\" /></p>");
                    break;

                case ActiveCommunity.Finsia:
                    sb.AppendLine("        <p>Cheers,</p>");
                    sb.AppendLine("        <p>");
                    sb.AppendLine("          The Finsia Career Network team.<br />");
                    sb.AppendLine("          <a href=\"mailto:careernetwork@finsia.com\">careernetwork@finsia.com </a>");
                    sb.AppendLine("        </p>");
                    sb.AppendLine("      </div>");
                    sb.AppendLine("      <div class=\"body\">      </div>");
                    sb.AppendLine("      <div class=\"signature\" style=\"padding-left: 9px; font-family: Arial, Helvetica, sans-serif; color: gray; font-size: 9pt; padding-top:1em;\">");
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
using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.InternalEmails
{
    [TestClass]
    public class GroupAbandonedEmailTests
        : EmailTests
    {
        private const string GroupName = "Dummy";
        private const string RequiresEncodingGroupName = "Dumb & Dumber";

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Send.

            return new GroupAbandonedEmail(GroupName);
        }

        [TestMethod]
        public void TestMailContents()
        {
            // Send.

            var communication = new GroupAbandonedEmail(GroupName);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, MemberServicesInbox);
            email.AssertSubject("The " + GroupName + " group has been abandoned and has no administrator");
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(GroupName)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestMailRequiresEncodingContents()
        {
            // Send.

            var communication = new GroupAbandonedEmail(RequiresEncodingGroupName);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, MemberServicesInbox);
            email.AssertSubject("The " + RequiresEncodingGroupName + " group has been abandoned and has no administrator");
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(RequiresEncodingGroupName)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetContent(string groupName)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>");
            sb.AppendLine("  The last remaining administrator of the");
            sb.AppendLine("  " + HtmlUtil.LineBreaksToHtml(HttpUtility.HtmlEncode(groupName)) + " group has left the group.");
            sb.AppendLine("  Please add a message to the group's home page");
            sb.AppendLine("  inviting existing members to nominate themselves as the");
            sb.AppendLine("  group's new administrator by contacting us.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }
    }
}
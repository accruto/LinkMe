using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.InternalEmails
{
    [TestClass]
    public class ContactUsEmailTests
        : EmailTests
    {
        private const string From = "from@test.linkme.net.au";
        private const string Content = "This is user entered content.";
        private const string Name = "Mutley";
        private const string PhoneNumber = "0410635666";
        private const string EnquiryType = "Report a site issue";
        private const string RequiresEncodingContent = @"
Wow! My & mayfly existence lasted
for about 1ms.";

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Send.

            return new ContactUsEmail(From, Name, UserType.Member, PhoneNumber, null, Content);
        }

        [TestMethod]
        public void TestMailContents()
        {
            // Send.

            var communication = new ContactUsEmail(From, Name, UserType.Member, PhoneNumber, null, Content);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(From, Name), Return, MemberServicesInbox);
            email.AssertSubject(GetSubject(From, UserType.Member));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(Name, PhoneNumber, EnquiryType, From, Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestMailContentsNoPhoneNumber()
        {
            // Send.

            var communication = new ContactUsEmail(From, Name, UserType.Member, null, null, Content);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(From, Name), Return, MemberServicesInbox);
            email.AssertSubject(GetSubject(From, UserType.Member));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(Name, null, EnquiryType, From, Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEncodingContents()
        {
            // Send.

            var communication = new ContactUsEmail(From, Name, UserType.Employer, PhoneNumber, null, RequiresEncodingContent);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(From, Name), Return, ClientServicesInbox);
            email.AssertSubject(GetSubject(From, UserType.Employer));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(Name, PhoneNumber, EnquiryType, From, RequiresEncodingContent)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject(string emailAddress, UserType userType)
        {
            return string.Format("Feedback from {0} {1}", userType.ToString().ToLower(), emailAddress);
        }

        private static string GetContent(string name, string phoneNumber, string enquiryType, string emailAddress, string content)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>");
            sb.AppendLine("  <table cellspacing=\"0\" cellpadding=\"2\">");
            sb.AppendLine("    <tr>");
            sb.AppendLine("      <td>User's name:</td>");
            sb.AppendLine("      <td>" + name + "</td>");
            sb.AppendLine("    </tr>");
            sb.AppendLine("    <tr>");
            sb.AppendLine("      <td>User's email address:</td>");
            sb.AppendLine("      <td>" + emailAddress + "</td>");
            sb.AppendLine("    </tr>");
            sb.AppendLine("    <tr>");
            sb.AppendLine("      <td>User's phone number:</td>");
            sb.AppendLine("      <td>" + phoneNumber + "</td>");
            sb.AppendLine("    </tr>");
            sb.AppendLine("    <tr>");
            sb.AppendLine("      <td>Type of enquiry:</td>");
            sb.AppendLine("      <td>" + enquiryType + "</td>");
            sb.AppendLine("    </tr>");
            sb.AppendLine("  </table>");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>" + HtmlUtil.LineBreaksToHtml(HttpUtility.HtmlEncode(content)) + "</p>");
            return sb.ToString();
        }
    }
}
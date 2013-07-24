using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.PartnerEmails;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.InternalEmails
{
    [TestClass]
    public class NewResourceQuestionEmailTests
        : EmailTests
    {
        private const string Subject = "New Question from a LinkMe User";
        private const string Content = "Why do I need to have an update to date resume?";
        private const string RequiresEncodingContent = "This is a question & I should get an Answer!";
        private const string CategoryName = "Some Category";
        private const string EmailAddress = "test@test.linkme.net.au";
        private const string Name = "Some LinkMe User";

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Send.

            return new NewResourceQuestionEmail(Content, EmailAddress, Name, CategoryName);
        }

        [TestMethod]
        public void TestMailContents()
        {
            // Send.

            var communication = new NewResourceQuestionEmail(Content, EmailAddress, Name, CategoryName);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, RedStarResume);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestMailRequiresEncodingContents()
        {
            // Send.

            var communication = new NewResourceQuestionEmail(RequiresEncodingContent, EmailAddress, Name, CategoryName);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, RedStarResume);
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(RequiresEncodingContent)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetContent(string question)
        {
            var sb = new StringBuilder();
            sb.Append("Asker's email: ").AppendLine(EmailAddress);
            sb.AppendLine("<br />");
            sb.Append("Asker's name: ").AppendLine(Name);
            sb.AppendLine("<br />");
            sb.Append("Category: ").AppendLine(CategoryName);
            sb.AppendLine("<br />");
            sb.AppendLine(HtmlUtil.LineBreaksToHtml(HttpUtility.HtmlEncode(question)));
            return sb.ToString();
        }
    }
}
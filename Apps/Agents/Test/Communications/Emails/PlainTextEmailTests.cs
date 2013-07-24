using System.Net.Mime;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails
{
    [TestClass]
    public class PlainTextEmailTests
        : EmailTests
    {
        private const string Email = "linkme1@test.linkme.net.au";
        private const string NewPassword = "newpassword";
        private const string FirstName = "Barney";
        private const string LastName = "Gumble";

        public override TemplateEmail GeneratePreview(Community community)
        {
            return null;
        }

        [TestMethod]
        public void TestSendPlainTextOnlyOff()
        {
            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(Email, FirstName, LastName);

            // Send.

            var communication = new PasswordReminderEmail(member, Email, NewPassword);
            _emailsCommand.TrySend(communication);

            // Should get both html and plain text versions.

            var email = _emailServer.AssertEmailSent();
            Assert.AreEqual("", email.Body);
            email.AssertViewCount(2);

            // Plain view should be first so it is shown properly in gmail / iPhone etc.

            Assert.AreEqual(MediaTypeNames.Text.Plain, email.AlternateViews[0].MediaType);
            Assert.AreEqual(MediaTypeNames.Text.Html, email.AlternateViews[1].MediaType);

            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(communication, member, NewPassword)));
            email.AssertView(MediaTypeNames.Text.Plain, GetPlainBody(communication, GetPlainContent(communication, member, NewPassword)));
            Assert.AreEqual(email.GetHtmlView().Body, GetBody(communication, member, GetContent(communication, member, NewPassword)));
            Assert.AreEqual(email.GetPlainTextView().Body, GetPlainBody(communication, GetPlainContent(communication, member, NewPassword)));
        }

        private string GetContent(TemplateEmail templateEmail, ICommunicationRecipient member, string password)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Dear " + member.FirstName + ",</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  As requested, your password has been reset.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Your login ID is <strong>" + member.EmailAddress + "</strong>");
            sb.AppendLine("  <br/>");
            sb.AppendLine("  and your new temporary password is <strong>" + password + "</strong>");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/login") + "\">Log in</a>");
            sb.AppendLine("  to access your account and update your password.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }

        private string GetPlainContent(TemplateEmail templateEmail, ICommunicationRecipient member, string password)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Dear " + member.FirstName + ",");
            sb.AppendLine();
            sb.AppendLine("As requested, your password has been reset.");
            sb.AppendLine();
            sb.AppendLine("Your login ID is '" + member.EmailAddress + "'");
            sb.AppendLine("and your new temporary password is '" + password + "'");
            sb.AppendLine();
            sb.AppendLine("<a href=\"" + GetTinyUrl(templateEmail, "text/plain", true, "~/login") + "\">Log in</a>");
            sb.AppendLine("to access your account and update your password.");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
using System;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Test.Verticals;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class EmployerContactCandidateConfirmationEmailTests
        : EmailTests
    {
        private const string NewEmployerEmail = "waylon@test.linkme.net.au";
        private const string Subject = "Test subject";
        private const string Content = "Test content (not copied to employer)";
        private const string RequiresEncodingContent = @"Test <div /> content
(not copied to employer)";

        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            ActivityContext.Current.Community.Reset();
        }

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember();

            // Send.

            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            return new EmployerContactCandidateConfirmationEmail(null, employer, member.Id, view.GetDisplayText(true), Subject, Content);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember();

            // Send.

            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            var templateEmail = new EmployerContactCandidateConfirmationEmail(null, employer, member.Id, view.GetDisplayText(true), Subject, Content);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, employer, GetContent(templateEmail, member, employer, Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestOverrideEmployerEmail()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember();

            // Send.

            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            var templateEmail = new EmployerContactCandidateConfirmationEmail(NewEmployerEmail, employer, member.Id, view.GetDisplayText(true), Subject, Content);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(NewEmployerEmail, employer.FullName));
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, employer, GetContent(templateEmail, member, employer, Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestAutopeople()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember();

            // Send in the context of Autopeople.

            var vertical = TestVertical.Autopeople.CreateTestVertical(_verticalsCommand, _contentEngine);

            try
            {
                ActivityContext.Current.Set(vertical);

                var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
                var templateEmail = new EmployerContactCandidateConfirmationEmail(null, employer, member.Id, view.GetDisplayText(true), Subject, Content);
                _emailsCommand.TrySend(templateEmail);

                // Check.

                var email = _emailServer.AssertEmailSent();
                email.AssertAddresses(Return, Return, employer);
                email.AssertSubject(GetSubject());
                email.AssertHtmlViewChecks();
                email.AssertHtmlView(GetBody(templateEmail, member, employer, Content));
                email.AssertNoAttachments();
                AssertCompatibleAddresses(email);
            }
            finally
            {
                ActivityContext.Current.Reset();
            }
        }

        [TestMethod]
        public void TestEncodingContents()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember();

            // Send.

            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            var templateEmail = new EmployerContactCandidateConfirmationEmail(null, employer, member.Id, view.GetDisplayText(true), Subject, RequiresEncodingContent);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, employer, GetContent(templateEmail, member, employer, RequiresEncodingContent)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject()
        {
            return "Copy: " + Subject;
        }

        private string GetContent(TemplateEmail templateEmail, Member member, IEmployer employer, string content)
        {
            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);

            var sb = new StringBuilder();
            sb.AppendLine("<p>");
            sb.AppendLine("  Please find a copy of the email you sent to");
            sb.AppendLine("  " + view.GetDisplayText(true));
            sb.AppendLine("  on the " + DateTime.Now.ToShortDateString() + ".");
            sb.AppendLine("  If you would like to see their resume please");
            sb.AppendLine("  <br />");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/employers/candidates", "candidateId", member.Id.ToString()) + "\">click here</a>");
            sb.AppendLine("  <br /><br />");
            sb.AppendLine("  -------- Original Message --------");
            sb.AppendLine("  <br />" + content + "<br />");
            sb.AppendLine("</p>");
            return sb.ToString();
        }

        private string GetBody(TemplateEmail templateEmail, Member member, IEmployer employer, string content)
        {
            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);

            // Autopeople specific.

            var sb = new StringBuilder();
            GetBodyStart(sb);
            sb.AppendLine("Please find a copy of the email you sent to");
            sb.AppendLine(view.GetDisplayText(true));
            sb.AppendLine("on the " + DateTime.Now.ToShortDateString() + ".");
            sb.AppendLine("<br /><br />-------- Original Message --------");
            sb.AppendLine("<br />" + content);
            sb.AppendLine("<br />");
            sb.AppendLine("      </div>");
            sb.AppendLine("      <div class=\"signature\" style=\"padding-left: 9px; font-family: Arial, Helvetica, sans-serif; color: gray; font-size: 9pt; padding-top:1em;\">");
            sb.AppendLine("        --------------------<br />");
            sb.AppendLine("        Kind regards, <br />");
            sb.AppendLine("        Autopeople resume search (powered by LinkMe) <br />");
            sb.AppendLine("        _____________________________________________<br />");
            sb.AppendLine("        Autopeople <a href=\"http://www.autopeople.com.au\">www.autopeople.com.au</a>");
            sb.AppendLine("        <br />");
            sb.AppendLine("        Tel:+61 3 9614 0066  |  Fax: +61 3 9533 7224<br />");
            sb.AppendLine("        2nd Floor, 470 Collins Street, Melbourne Vic 3000<br />");
            sb.AppendLine("        <br />");
            sb.AppendLine("        <i>real <b>jobs</b> for real <b>auto</b> people</i><br />");
            sb.AppendLine("        <br />");
            sb.AppendLine("        <br />");
            sb.AppendLine("        To update your details, log in to your");
            sb.AppendLine("        <a href=\"http://www.autopeople.com.au\">Autopeople account</a>");
            sb.AppendLine("        <br />");
            GetBodyEnd(templateEmail, sb);
            return sb.ToString();
        }
    }
}
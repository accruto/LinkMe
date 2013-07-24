using System.Net.Mime;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerToMemberNotifications;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerToMemberNotifications
{
    [TestClass]
    public class ContactCandidateEmailTests
        : EmailTests
    {
        private const string NewEmployerEmail = "waylon@test.linkme.net.au";
        private const string Subject = "Test subject";
        private const string SimpleContent = "Test content (not copied to employer)";
        private const string HtmlContent = @"<p>Dear Robert<br /><br /></p>
<p><span>My client is looking for a Strong Database Developer to work for a large consulting organisation. </span></p>
<p><span>Please find the role attributes - </span></p>

<p><span>Would this be of interest to you or your colleagues? </span><br /><br />Regards,<br /><br />Monty Burns</p>
<p><span><strong>M:</strong></span><span> 0400111111 &nbsp;|&nbsp; </span><strong><span>T:</span></strong><span> 02 9999 9999&nbsp; |&nbsp; </span><strong><span>F:</span></strong><span> 02 9999 9999 <br /></span><strong><span>E:</span></strong><span> <a href=""mailto:monty@test.linkme.net.au""><span>monty.burns@test.linkme.net.au</span></a>&nbsp; |&nbsp; </span><strong><span>W:</span></strong><span> <a href=""http://www.linkme.com.au/""><span>www.linkme.com.au <http://www.linkme.com.au/> </span></a> <br /></span><strong><span>A:</span></strong><span> Level 1, 20 Smithers Street, Sydney NSW 2000</span>&nbsp;</p>
<p>&nbsp;</p>
";
        private const string PlainHtmlContent = @"

Dear Robert



My client is looking for a Strong Database Developer to work for a large consulting organisation.

Please find the role attributes -

Would this be of interest to you or your colleagues?

Regards,

Monty Burns

M:0400111111 | T:02 9999 9999 | F:02 9999 9999
E:monty.burns@test.linkme.net.au | W:www.linkme.com.au
A:Level 1, 20 Smithers Street, Sydney NSW 2000 

 ";

        private enum ActiveCommunity
        {
            None,
            AutoPeople,
            Finsia
        };

        private ActiveCommunity _activeCommunity;

        [TestInitialize]
        public void TestInitialize()
        {
            ActivityContext.Current.Community.Reset();
            _activeCommunity = ActiveCommunity.None;
        }

        public override TemplateEmail GeneratePreview(Community community)
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember(community);
            
            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            return new ContactCandidateEmail(view, null, employer, Subject, SimpleContent);
        }

        [TestMethod]
        public void TestNotificationsOff()
        {
            var employer = CreateEmployer();
            var member = CreateMember();

            // No settings so notification should get through.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            _emailsCommand.TrySend(new ContactCandidateEmail(view, null, employer, Subject, SimpleContent));
            _emailServer.AssertEmailSent();

            // Turn off.

            var category = _settingsQuery.GetCategory("EmployerToMemberNotification");
            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Never);
            _emailsCommand.TrySend(new ContactCandidateEmail(view, null, employer, Subject, SimpleContent));
            _emailServer.AssertNoEmailSent();

            // Turn on.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Immediately);
            _emailsCommand.TrySend(new ContactCandidateEmail(view, null, employer, Subject, SimpleContent));
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer.

            Employer employer = CreateEmployer();

            // Create a member.

            Member member = CreateMember();

            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new ContactCandidateEmail(view, null, employer, Subject, SimpleContent);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(employer, Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, employer, SimpleContent)));
            email.AssertView(MediaTypeNames.Text.Plain, GetPlainBody(communication, GetPlainContent(member, employer, SimpleContent)));
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

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new ContactCandidateEmail(view, NewEmployerEmail, employer, Subject, SimpleContent);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(NewEmployerEmail, employer.FullName), Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, employer, SimpleContent)));
            email.AssertView(MediaTypeNames.Text.Plain, GetPlainBody(communication, GetPlainContent(member, employer, SimpleContent)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestAutoPeople()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember();

            // Send in the context of Autopeople. Should still be a standard email because the member is not a member of Autopeople.

            Community community = TestCommunity.Autopeople.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            ActivityContext.Current.Community.Set(community);

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new ContactCandidateEmail(view, NewEmployerEmail, employer, Subject, SimpleContent);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(NewEmployerEmail, employer.FullName), Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, employer, SimpleContent)));
            email.AssertView(MediaTypeNames.Text.Plain, GetPlainBody(communication, GetPlainContent(member, employer, SimpleContent)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestFinsia()
        {
            _activeCommunity = ActiveCommunity.Finsia;

            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var community = TestCommunity.Finsia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var member = CreateMember(community);

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new ContactCandidateEmail(view, NewEmployerEmail, employer, Subject, SimpleContent);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(NewEmployerEmail, employer.FullName), Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, employer, SimpleContent)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestHtmlContent()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember();

            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new ContactCandidateEmail(view, null, employer, Subject, HtmlContent);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(employer, Return, member);
            email.AssertSubject(Subject);
            //email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, employer, HtmlContent)));
            email.AssertView(MediaTypeNames.Text.Plain, GetPlainBody(communication, GetPlainContent(member, employer, PlainHtmlContent)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestNoAccess()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember();

            // Create a view with no access. Should still be sent with no problems (it is not up to the email to decide whether it can be sent or not).

            var view = new ProfessionalView(member, 0, ProfessionalContactDegree.NotContacted, true, false);
            var communication = new ContactCandidateEmail(view, null, employer, Subject, SimpleContent);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(employer, Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, employer, SimpleContent)));
            email.AssertView(MediaTypeNames.Text.Plain, GetPlainBody(communication, GetPlainContent(member, employer, SimpleContent)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetContent(ICommunicationRecipient member, IEmployer employer, string content)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Dear " + member.FirstName + ",</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  " + employer.FullName + " of " + employer.Organisation.FullName);
            sb.AppendLine("  has found you in our network and has a job opportunity");
            sb.AppendLine("  you may be interested in.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  " + employer.FirstName.MakeNamePossessive() + " personal message:");
            sb.AppendLine("  <br />");
            sb.AppendLine("  " + content);
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  If you are interested please reply to this email.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }

        private static string GetPlainContent(ICommunicationRecipient member, IEmployer employer, string content)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Dear " + member.FirstName + ",");
            sb.AppendLine();
            sb.AppendLine(employer.FullName + " of " + employer.Organisation.FullName);
            sb.AppendLine("has found you in our network and has a job opportunity");
            sb.AppendLine("you may be interested in.");
            sb.AppendLine();
            sb.AppendLine(employer.FirstName.MakeNamePossessive() + " personal message:");
            sb.AppendLine();
            sb.AppendLine(content);
            sb.AppendLine();
            sb.AppendLine("If you are interested please reply to this email.");
            sb.AppendLine();
            return sb.ToString();
        }

        protected override void GetMemberSignature(TemplateEmail email, StringBuilder sb)
        {
            switch (_activeCommunity)
            {
                case ActiveCommunity.AutoPeople:
                    sb.AppendLine("        <br />Thanks,");
                    sb.AppendLine("        <br />Your LinkMe team in partnership with Autopeople");
                    sb.AppendLine("        <br />");
                    sb.AppendLine("        <br />For support please email");
                    sb.AppendLine("        <a href=\"mailto:customerservice@autopeople.com.au\">customerservice@autopeople.com.au</a>");
                    sb.AppendLine("        <p><img src=\"" + InsecureRootPath + "email/logo-for-emails.gif\" alt=\"\" /></p>");
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
using System;
using System.Net.Mime;
using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerToMemberNotifications;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerToMemberNotifications
{
    [TestClass]
    public class RepresentativeContactCandidateEmailTests
        : EmailTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private const string NewEmployerEmail = "waylon@test.linkme.net.au";
        private const string Subject = "Test subject";
        private const string Content = "Test content (not copied to employer)";
        private const string RequiresEncodingContent = @"Test & content
(not copied to employer)";

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

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(0, community != null ? community.Id : (Guid?)null);
            var representee = _memberAccountsCommand.CreateTestMember(1);

            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            return new RepresentativeContactCandidateEmail(view, null, employer, representee, Subject, Content);
        }

        [TestMethod]
        public void TestNotificationsOff()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var member = _memberAccountsCommand.CreateTestMember(0);
            var representee = _memberAccountsCommand.CreateTestMember(1);

            // No settings so notification should get through.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            _emailsCommand.TrySend(new RepresentativeContactCandidateEmail(view, null, employer, representee, Subject, Content));
            _emailServer.AssertEmailSent();

            // Turn off.

            var category = _settingsQuery.GetCategory("EmployerToMemberNotification");
            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Never);
            _emailsCommand.TrySend(new RepresentativeContactCandidateEmail(view, null, employer, representee, Subject, Content));
            _emailServer.AssertNoEmailSent();

            // Turn on.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Immediately);
            _emailsCommand.TrySend(new RepresentativeContactCandidateEmail(view, null, employer, representee, Subject, Content));
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var representee = _memberAccountsCommand.CreateTestMember(1);

            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new RepresentativeContactCandidateEmail(view, null, employer, representee, Subject, Content);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(employer, Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, representee, employer, Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestOverrideEmployerEmail()
        {
            // Create an employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var representee = _memberAccountsCommand.CreateTestMember(1);

            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new RepresentativeContactCandidateEmail(view, NewEmployerEmail, employer, representee, Subject, Content);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(NewEmployerEmail, employer.FullName), Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, representee, employer, Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestAutoPeople()
        {
            // Create an employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var representee = _memberAccountsCommand.CreateTestMember(1);

            // Send in the context of Autopeople. Should still be a standard email because the member is not a member of Autopeople.

            Community community = TestCommunity.Autopeople.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            ActivityContext.Current.Community.Set(community);

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new RepresentativeContactCandidateEmail(view, NewEmployerEmail, employer, representee, Subject, Content);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(NewEmployerEmail, employer.FullName), Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, representee, employer, Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestFinsia()
        {
            _activeCommunity = ActiveCommunity.Finsia;

            // Create an employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Create a member.

            var community = TestCommunity.Finsia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var member = _memberAccountsCommand.CreateTestMember(0, community.Id);
            var representee = _memberAccountsCommand.CreateTestMember(1);

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new RepresentativeContactCandidateEmail(view, NewEmployerEmail, employer, representee, Subject, Content);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(NewEmployerEmail, employer.FullName), Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, representee, employer, Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEncodingContent()
        {
            // Create an employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var representee = _memberAccountsCommand.CreateTestMember(1);

            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new RepresentativeContactCandidateEmail(view, null, employer, representee, Subject, RequiresEncodingContent);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(employer, Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, representee, employer, RequiresEncodingContent)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestNoAccess()
        {
            // Create an employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var representee = _memberAccountsCommand.CreateTestMember(1);

            // Send.

            var view = new ProfessionalView(member, 0, ProfessionalContactDegree.NotContacted, false, false);
            var representeeView = new ProfessionalView(representee, 0, ProfessionalContactDegree.NotContacted, false, false);
            var communication = new RepresentativeContactCandidateEmail(view, null, employer, representeeView, Subject, Content);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(employer, Return, member);
            email.AssertSubject(Subject);
            email.AssertViewChecks(MediaTypeNames.Text.Html);
            email.AssertView(MediaTypeNames.Text.Html, GetBody(communication, member, GetContent(member, representee, employer, Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetContent(ICommunicationRecipient member, ICommunicationRecipient representee, IEmployer employer, string content)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Dear " + member.FirstName + ",</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  " + employer.FullName + " of " + employer.Organisation.FullName);
            sb.AppendLine("  has found " + representee.FullName);
            sb.AppendLine("  in our network and has a job opportunity");
            sb.AppendLine("  they may be interested in.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  " + employer.FirstName.MakeNamePossessive() + " personal message:");
            sb.AppendLine("  <br />");
            sb.AppendLine("  " + HtmlUtil.LineBreaksToHtml(HttpUtility.HtmlEncode(content)));
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  If you are interested please reply to this email.");
            sb.AppendLine("</p>");
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
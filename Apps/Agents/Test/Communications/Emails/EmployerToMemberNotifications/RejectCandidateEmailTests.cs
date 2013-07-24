using System;
using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerToMemberNotifications;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerToMemberNotifications
{
    [TestClass]
    public class RejectCandidateEmailTests
        : EmailTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private const string FromEmailAddress = "from@test.linkme.net.au";
        private const string FromLoginId = "from";
        private const string To = "to@test.linkme.net.au";
        private const string Subject = "Rejection";
        private const string Content = "This is user entered content.";
        private const string RequiresEncodingContent = @"Don't & want
it";

        public override TemplateEmail GeneratePreview(Community community)
        {
            var member = _memberAccountsCommand.CreateTestMember(To, community != null ? community.Id : (Guid?)null);
            var employer = _employerAccountsCommand.CreateTestEmployer(FromLoginId, _organisationsCommand.CreateTestOrganisation(0));

            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            return new RejectCandidateEmail(view, FromEmailAddress, employer, Subject, Content);
        }

        [TestMethod]
        public void TestNotificationsOff()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(FromLoginId, _organisationsCommand.CreateTestOrganisation(0));
            var member = _memberAccountsCommand.CreateTestMember(To);

            // No settings so notification should get through.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            _emailsCommand.TrySend(new RejectCandidateEmail(view, FromEmailAddress, employer, Subject, Content));
            _emailServer.AssertEmailSent();

            // Turn off.

            var category = _settingsQuery.GetCategory("EmployerToMemberNotification");
            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Never);
            _emailsCommand.TrySend(new RejectCandidateEmail(view, FromEmailAddress, employer, Subject, Content));
            _emailServer.AssertNoEmailSent();

            // Turn on.

            _settingsCommand.SetFrequency(member.Id, category.Id, Frequency.Immediately);
            _emailsCommand.TrySend(new RejectCandidateEmail(view, FromEmailAddress, employer, Subject, Content));
            _emailServer.AssertEmailSent();
        }

        [TestMethod]
        public void TestMailContents()
        {
            Member member = _memberAccountsCommand.CreateTestMember(To);
            Employer employer = _employerAccountsCommand.CreateTestEmployer(FromLoginId, _organisationsCommand.CreateTestOrganisation(0));

            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new RejectCandidateEmail(view, FromEmailAddress, employer, Subject, Content);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(FromEmailAddress, employer.FullName), Return, new EmailRecipient(To, member.FullName));
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestMailRequiresEncodingContents()
        {
            Member member = _memberAccountsCommand.CreateTestMember(To);
            Employer employer = _employerAccountsCommand.CreateTestEmployer(FromLoginId, _organisationsCommand.CreateTestOrganisation(0));

            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new RejectCandidateEmail(view, FromEmailAddress, employer, Subject, RequiresEncodingContent);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(FromEmailAddress, employer.FullName), Return, new EmailRecipient(To, member.FullName));
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(RequiresEncodingContent)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestMailContentsNoFrom()
        {
            Member member = _memberAccountsCommand.CreateTestMember(To);
            Employer employer = _employerAccountsCommand.CreateTestEmployer(FromLoginId, _organisationsCommand.CreateTestOrganisation(0));

            // Send.

            var view = new ProfessionalView(member, null, ProfessionalContactDegree.Contacted, true, false);
            var communication = new RejectCandidateEmail(view, null, employer, Subject, Content);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(FromEmailAddress, employer.FullName), Return, new EmailRecipient(To, member.FullName));
            email.AssertSubject(Subject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(Content)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetContent(string content)
        {
            var sb = new StringBuilder();
            sb.AppendLine(HtmlUtil.LineBreaksToHtml(HttpUtility.HtmlEncode(content)));
            return sb.ToString();
        }
    }
}
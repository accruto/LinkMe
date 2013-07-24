using System;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Reports.Commands;
using LinkMe.Apps.Agents.Reports.Employers;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Communications;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.CommandLine.Monthly
{
    [TestClass]
    public class EmailCustomerReportsTaskTest
        : MonthlyTest
    {
        private const string ClientFirstName = "Harvey";
        private const string ClientLastName = "Thomson";
        private const string ClientEmail = "clientemail@test.linkme.net.au";
        private const string SecondaryClientEmail = "clientemail2@test.linkme.net.au";

        private static readonly string ReportPeriodText = "the month of " + DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");

        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IReportsCommand _reportsCommand = Resolve<IReportsCommand>();
        private readonly IMemberSearchesCommand _memberSearchesCommand = Resolve<IMemberSearchesCommand>();
        private Administrator _admin;
        private VerifiedOrganisation _orgUnit;
        private Employer _employer;

        [TestInitialize]
        public void TestInitialize()
        {
            _admin = _administratorAccountsCommand.CreateTestAdministrator(1);
            _orgUnit = _organisationsCommand.CreateTestVerifiedOrganisation("Headhunters Inc.", null, _admin.Id, _admin.Id);

            _employer = _employerAccountsCommand.CreateTestEmployer("employer", _organisationsCommand.CreateTestOrganisation(0));
            _employer.Organisation = _orgUnit;
            _employer.CreatedTime = DateTime.Now.AddYears(-1); // Must exist before the reporting period
            _employerAccountsCommand.UpdateEmployer(_employer);
        }

        [TestMethod]
        public void NoReportDefined()
        {
            Execute(true);
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void ReportNoActivity()
        {
            // Define the report

            var report = new ResumeSearchActivityReport
            {
                ClientId = _orgUnit.Id,
                SendToAccountManager = false,
                SendToClient = true
            };

            _reportsCommand.CreateReport(report);

            // To client only and there's no activity, so no report.

            Execute(true);
            _emailServer.AssertNoEmailSent();

            // To client and AM, but no activity, so sent to AM only.

            report.SendToAccountManager = true;
            _reportsCommand.UpdateReport(report);

            Execute(true);

            var emails = _emailServer.AssertEmailsSent(1);
            emails[0].AssertSubject("No resume search activity for " + _orgUnit.FullName + " for " + ReportPeriodText);
            emails[0].AssertAddresses(Return, Return, _admin);
            emails[0].AssertAttachments(1);

            // Try again - no duplicate report should be sent.

            Execute(true);
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void ReportActivityToAccountManager()
        {
            LogSearch();

            // Define the report

            var report = new ResumeSearchActivityReport
            {
                ClientId = _orgUnit.Id,
                SendToAccountManager = true,
                SendToClient = false,
                IncludeChildOrganisations = true
            };

            // Doesn't make any difference in this test, just exercising another code path

            _reportsCommand.CreateReport(report);

            // To AM only.

            Execute(true);

            var emails = _emailServer.AssertEmailsSent(1);
            emails[0].AssertSubject(GetSubject());
            emails[0].AssertAddresses(Return, Return, _admin);
            emails[0].AssertAttachments(1);
        }

        [TestMethod]
        public void ReportActivityToClient()
        {
            LogSearch();

            // Define the report

            var report = new ResumeSearchActivityReport
            {
                ClientId = _orgUnit.Id,
                SendToAccountManager = false,
                SendToClient = true,
                IncludeDisabledUsers = true,
            };

            _reportsCommand.CreateReport(report);

            // To client only - but no primary contact email is set.

            Execute(true);
            _emailServer.AssertNoEmailSent();

            // Set the primary contact

            _orgUnit.ContactDetails = new ContactDetails {FirstName = ClientFirstName, LastName = ClientLastName, EmailAddress = ClientEmail};
            _organisationsCommand.UpdateOrganisation(_orgUnit);

            Execute(true);
            var emails = _emailServer.AssertEmailsSent(1);

            emails[0].AssertSubject(GetSubject());
            emails[0].AssertAddresses(_admin, Return, new EmailRecipient(ClientEmail, ClientFirstName.CombineLastName(ClientLastName), ClientFirstName, ClientLastName));
            emails[0].AssertAttachments(1);
        }

        [TestMethod]
        public void ReportActivityToClients()
        {
            LogSearch();

            // Define the report

            var report = new ResumeSearchActivityReport
            {
                ClientId = _orgUnit.Id,
                SendToAccountManager = false,
                SendToClient = true,
                IncludeDisabledUsers = true,
            };

            _reportsCommand.CreateReport(report);

            // To client only - but no primary contact emails set.

            Execute(true);
            _emailServer.AssertNoEmailSent();

            // Set the primary contact

            _orgUnit.ContactDetails = new ContactDetails
            {
                FirstName = ClientFirstName,
                LastName = ClientLastName,
                EmailAddress = ClientEmail,
                SecondaryEmailAddresses = SecondaryClientEmail
            };
            _organisationsCommand.UpdateOrganisation(_orgUnit);

            Execute(true);
            var emails = _emailServer.AssertEmailsSent(2);

            emails[0].AssertSubject(GetSubject());
            emails[0].AssertAddresses(_admin, Return, new EmailRecipient(ClientEmail, ClientFirstName.CombineLastName(ClientLastName), ClientFirstName, ClientLastName));
            emails[0].AssertAttachments(1);
            emails[1].AssertSubject(GetSubject());
            emails[1].AssertAddresses(_admin, Return, new EmailRecipient(SecondaryClientEmail));
            emails[1].AssertAttachments(1);
        }

        [TestMethod]
        public void ReportActivityToClientAndAccountManager()
        {
            LogSearch();

            // Define the report

            var report = new ResumeSearchActivityReport
            {
                ClientId = _orgUnit.Id,
                SendToAccountManager = false,
                SendToClient = true,
                IncludeDisabledUsers = true,
            };

            _reportsCommand.CreateReport(report);

            // To client only - but no primary contact email is set.

            Execute(true);
            _emailServer.AssertNoEmailSent();

            // Set the primary contact and account manager.

            _orgUnit.ContactDetails = new ContactDetails { FirstName = ClientFirstName, LastName = ClientLastName, EmailAddress = ClientEmail };
            _organisationsCommand.UpdateOrganisation(_orgUnit);

            report.SendToAccountManager = true;
            _reportsCommand.UpdateReport(report);

            Execute(true);

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertSubject(GetSubject());
            emails[0].AssertAddresses(Return, Return, _admin);
            emails[0].AssertAttachments(1);
            emails[1].AssertSubject(GetSubject());
            emails[1].AssertAddresses(_admin, Return, new EmailRecipient(ClientEmail, ClientFirstName.CombineLastName(ClientLastName), ClientFirstName, ClientLastName));
            emails[1].AssertAttachments(1);
        }

        [TestMethod]
        public void ReportActivityToClientsAndAccountManager()
        {
            LogSearch();

            // Define the report

            var report = new ResumeSearchActivityReport
            {
                ClientId = _orgUnit.Id,
                SendToAccountManager = false,
                SendToClient = true,
                IncludeDisabledUsers = true,
            };

            _reportsCommand.CreateReport(report);

            // To client only - but no primary contact email is set.

            Execute(true);
            _emailServer.AssertNoEmailSent();

            // Set the primary contact and account manager.

            _orgUnit.ContactDetails = new ContactDetails
            {
                FirstName = ClientFirstName,
                LastName = ClientLastName,
                EmailAddress = ClientEmail,
                SecondaryEmailAddresses = SecondaryClientEmail
            };
            _organisationsCommand.UpdateOrganisation(_orgUnit);

            report.SendToAccountManager = true;
            _reportsCommand.UpdateReport(report);

            Execute(true);

            var emails = _emailServer.AssertEmailsSent(3);
            emails[0].AssertSubject(GetSubject());
            emails[0].AssertAddresses(Return, Return, _admin);
            emails[0].AssertAttachments(1);
            emails[1].AssertSubject(GetSubject());
            emails[1].AssertAddresses(_admin, Return, new EmailRecipient(ClientEmail, ClientFirstName.CombineLastName(ClientLastName), ClientFirstName, ClientLastName));
            emails[1].AssertAttachments(1);
            emails[2].AssertSubject(GetSubject());
            emails[2].AssertAddresses(_admin, Return, new EmailRecipient(SecondaryClientEmail));
            emails[2].AssertAttachments(1);
        }

        private void LogSearch()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("test");
            var execution = new MemberSearchExecution
            {
                SearcherId = _employer.Id,
                Criteria = criteria,
                StartTime = DateTime.Now.AddMonths(-1),
                Results = new MemberSearchResults {MemberIds = new Guid[0]}
            };
            _memberSearchesCommand.CreateMemberSearchExecution(execution);
        }

        private string GetSubject()
        {
            return "LinkMe usage report for " + _orgUnit.FullName + " for " + ReportPeriodText;
        }
    }
}

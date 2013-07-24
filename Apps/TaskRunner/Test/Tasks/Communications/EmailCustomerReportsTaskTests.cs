using System;
using System.Linq;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Reports.Commands;
using LinkMe.Apps.Agents.Reports.Employers;
using LinkMe.Apps.Agents.Reports.Employers.Commands;
using LinkMe.Apps.Agents.Reports.Employers.Queries;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Members;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications
{
    [TestClass]
    public class EmailCustomerReportsTaskTests
        : TaskTests
    {
        private const string ClientFirstName = "Harvey";
        private const string ClientLastName = "Thomson";
        private const string ClientEmail = "clientemail@test.linkme.net.au";
        private const string SecondaryClientEmail = "clientemail2@test.linkme.net.au";

        private static readonly string ReportPeriodText = "the month of " + DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");

        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IReportsCommand _reportsCommand = Resolve<IReportsCommand>();
        private readonly IMemberSearchesCommand _memberSearchesCommand = Resolve<IMemberSearchesCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsRepository _jobAdsRepository = Resolve<IJobAdsRepository>();

        private class ReportStatus
        {
            public EmployerReport Report { get; private set; }
            public bool IsActivity { get; private set; }

            public ReportStatus(EmployerReport report, bool isActivity)
            {
                Report = report;
                IsActivity = isActivity;
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoReportDefined()
        {
            Execute();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestReportNoResumeSearchActivityToAccountManager()
        {
            TestReportActivity<ResumeSearchActivityReport>(NoActivity, true, false, false);
        }

        [TestMethod]
        public void TestReportNoJobBoardActivityToAccountManager()
        {
            TestReportActivity<JobBoardActivityReport>(NoActivity, true, false, false);
        }

        [TestMethod]
        public void TestReportNoResumeSearchActivityToClient()
        {
            TestReportActivity<ResumeSearchActivityReport>(NoActivity, false, true, false);
        }

        [TestMethod]
        public void TestReportNoJobBoardActivityToClient()
        {
            TestReportActivity<JobBoardActivityReport>(NoActivity, false, true, false);
        }

        [TestMethod]
        public void TestReportNoResumeSearchActivityToClientAndAccountManager()
        {
            TestReportActivity<ResumeSearchActivityReport>(NoActivity, true, true, false);
        }

        [TestMethod]
        public void TestReportNoJobBoardActivityToClientAndAccountManager()
        {
            TestReportActivity<JobBoardActivityReport>(NoActivity, true, true, false);
        }

        [TestMethod]
        public void TestReportResumeSearchActivityToAccountManager()
        {
            TestReportActivity<ResumeSearchActivityReport>(CreateResumeSearchActivity, true, false, false);
        }

        [TestMethod]
        public void TestReportJobBoardActivityToAccountManager()
        {
            TestReportActivity<JobBoardActivityReport>(CreateJobBoardActivity, true, false, false);
        }

        [TestMethod]
        public void TestReportResumeSearchActivityToClient()
        {
            TestReportActivity<ResumeSearchActivityReport>(CreateResumeSearchActivity, false, true, false);
        }

        [TestMethod]
        public void TestReportJobBoardActivityToClient()
        {
            TestReportActivity<JobBoardActivityReport>(CreateJobBoardActivity, false, true, false);
        }

        [TestMethod]
        public void TestReportResumeSearchActivityToClients()
        {
            TestReportActivity<ResumeSearchActivityReport>(CreateResumeSearchActivity, false, true, true);
        }

        [TestMethod]
        public void TestReportJobBoardActivityToClients()
        {
            TestReportActivity<JobBoardActivityReport>(CreateJobBoardActivity, false, true, true);
        }

        [TestMethod]
        public void TestReportResumeSearchActivityToClientAndAccountManager()
        {
            TestReportActivity<ResumeSearchActivityReport>(CreateResumeSearchActivity, true, true, false);
        }

        [TestMethod]
        public void TestReportJobBoardActivityToClientAndAccountManager()
        {
            TestReportActivity<JobBoardActivityReport>(CreateJobBoardActivity, true, true, false);
        }

        [TestMethod]
        public void TestReportResumeSearchActivityToClientsAndAccountManager()
        {
            TestReportActivity<ResumeSearchActivityReport>(CreateResumeSearchActivity, true, true, true);
        }

        [TestMethod]
        public void TestReportJobBoardActivityToClientsAndAccountManager()
        {
            TestReportActivity<JobBoardActivityReport>(CreateJobBoardActivity, true, true, true);
        }

        [TestMethod]
        public void TestMultipleReportsToClient()
        {
            TestMultipleReports(CreateResumeSearchActivity, CreateJobBoardActivity, false);
        }

        [TestMethod]
        public void TestMultipleReportsNoResumeSearchActivityToClient()
        {
            TestMultipleReports(NoActivity, CreateJobBoardActivity, false);
        }

        [TestMethod]
        public void TestMultipleReportsNoJobBoardActivityToClient()
        {
            TestMultipleReports(CreateResumeSearchActivity, NoActivity, false);
        }

        [TestMethod]
        public void TestMultipleReportsNoActivityToClient()
        {
            TestMultipleReports(NoActivity, NoActivity, false);
        }

        [TestMethod]
        public void TestMultipleReportsToClientAndAccountManager()
        {
            TestMultipleReports(CreateResumeSearchActivity, CreateJobBoardActivity, true);
        }

        [TestMethod]
        public void TestMultipleReportsNoResumeSearchActivityToClientAndAccountManager()
        {
            TestMultipleReports(NoActivity, CreateJobBoardActivity, true);
        }

        [TestMethod]
        public void TestMultipleReportsNoJobBoardActivityToClientAndAccountManager()
        {
            TestMultipleReports(CreateResumeSearchActivity, NoActivity, true);
        }

        [TestMethod]
        public void TestMultipleReportsNoActivityToClientAndAccountManager()
        {
            TestMultipleReports(NoActivity, NoActivity, true);
        }

        private void TestReportActivity<TReport>(Func<IEmployer, bool> createActivity, bool sendToAccountManager, bool sendToClient, bool sendToSecondaryClients)
            where TReport : EmployerReport, new()
        {
            // Create everyone.

            var administrator = CreateAdministrator();
            var organisation = CreateOrganisation(administrator.Id);
            var employer = CreateEmployer(organisation);

            // Define the report.

            var report = new TReport
            {
                ClientId = organisation.Id,
                SendToAccountManager = sendToAccountManager,
                SendToClient = sendToClient,
            };
            _reportsCommand.CreateReport(report);

            // Create activity to report on.

            var isActivity = createActivity(employer);
            var status = new ReportStatus(report, isActivity);

            // No contact details set.

            if (sendToClient)
            {
                if (!sendToAccountManager)
                {
                    // Check that no email is sent to the client if no contact details set.

                    Execute();
                    _emailServer.AssertNoEmailSent();
                }

                // Set the contact details.

                organisation.ContactDetails = new ContactDetails { FirstName = ClientFirstName, LastName = ClientLastName, EmailAddress = ClientEmail };
                if (sendToSecondaryClients)
                    organisation.ContactDetails.SecondaryEmailAddresses = SecondaryClientEmail;
                _organisationsCommand.UpdateOrganisation(organisation);
            }

            // Run the report.

            Execute();

            MockEmail administratorEmail = null;
            MockEmail clientEmail = null;
            MockEmail secondaryClientEmail = null;

            var emails = _emailServer.GetEmails().ToList();
            if (sendToAccountManager)
            {
                Assert.IsTrue(emails.Count > 0);
                administratorEmail = emails[0];
                emails.RemoveAt(0);
            }

            if (sendToClient)
            {
                Assert.IsTrue(emails.Count > 0);
                clientEmail = emails[0];
                emails.RemoveAt(0);
            }

            if (sendToSecondaryClients)
            {
                Assert.IsTrue(emails.Count > 0);
                secondaryClientEmail = emails[0];
                emails.RemoveAt(0);
            }

            Assert.AreEqual(0, emails.Count);

            if (administratorEmail != null)
                AssertAdministratorEmail(administratorEmail, administrator, organisation, status);
            if (clientEmail != null)
                AssertClientEmail(clientEmail, administrator, organisation, status);
            if (secondaryClientEmail != null)
                AssertSecondaryClientEmail(secondaryClientEmail, administrator, organisation, status);
        }

        private void TestMultipleReports(Func<IEmployer, bool> createClientActvity, Func<IEmployer, bool> createJobBoardActivity, bool sendToAccountManager)
        {
            // Create everyone.

            var administrator = CreateAdministrator();
            var organisation = CreateOrganisation(administrator.Id);
            organisation.ContactDetails = new ContactDetails { FirstName = ClientFirstName, LastName = ClientLastName, EmailAddress = ClientEmail };
            _organisationsCommand.UpdateOrganisation(organisation);
            var employer = CreateEmployer(organisation);

            // Define the reports.

            var resumeSearchActivityReport = new ResumeSearchActivityReport
            {
                ClientId = organisation.Id,
                SendToAccountManager = sendToAccountManager,
                SendToClient = true,
            };
            _reportsCommand.CreateReport(resumeSearchActivityReport);

            var jobBoardActivityReport = new JobBoardActivityReport
            {
                ClientId = organisation.Id,
                SendToAccountManager = sendToAccountManager,
                SendToClient = true,
            };
            _reportsCommand.CreateReport(jobBoardActivityReport);

            // Create activity to report on.

            var isResumeSearchActivity = createClientActvity(employer);
            var isJobBoardActivity = createJobBoardActivity(employer);
            var clientStatus = new ReportStatus(resumeSearchActivityReport, isResumeSearchActivity);
            var jobBoardStatus = new ReportStatus(jobBoardActivityReport, isJobBoardActivity);

            // Run the reports.

            Execute();

            if (sendToAccountManager)
            {
                if ((isResumeSearchActivity && isJobBoardActivity) || (!isResumeSearchActivity && !isJobBoardActivity))
                {
                    var emails = _emailServer.AssertEmailsSent(2);
                    AssertAdministratorEmail(emails[0], administrator, organisation, clientStatus, jobBoardStatus);
                    AssertClientEmail(emails[1], administrator, organisation, clientStatus, jobBoardStatus);
                }
                else
                {
                    var emails = _emailServer.AssertEmailsSent(3);
                    AssertAdministratorEmail(emails[0], administrator, organisation, jobBoardStatus);
                    AssertClientEmail(emails[1], administrator, organisation, clientStatus, jobBoardStatus);
                    AssertAdministratorEmail(emails[2], administrator, organisation, clientStatus);
                }
            }
            else
            {
                var emails = _emailServer.AssertEmailsSent(1);
                AssertClientEmail(emails[0], administrator, organisation, clientStatus, jobBoardStatus);
            }
        }

        private static void AssertSecondaryClientEmail(MockEmail email, ICommunicationUser administrator, IOrganisation organisation, params ReportStatus[] statuses)
        {
            email.AssertAddresses(administrator, Return, new EmailRecipient(SecondaryClientEmail));
            AssertClientEmail(email, organisation, statuses);
        }

        private static void AssertClientEmail(MockEmail email, ICommunicationUser administrator, IOrganisation organisation, params ReportStatus[] statuses)
        {
            email.AssertAddresses(administrator, Return, new EmailRecipient(ClientEmail, ClientFirstName.CombineLastName(ClientLastName), ClientFirstName, ClientLastName));
            AssertClientEmail(email, organisation, statuses);
        }

        private static void AssertClientEmail(MockEmail email, IOrganisation organisation, params ReportStatus[] statuses)
        {
            // Subject.

            email.AssertSubject(GetSubject(organisation));

            // Attachments.

            email.AssertAttachments(GetAttachments(organisation, statuses));

            // Contents.

            if (statuses.Any(a => a.Report is ResumeSearchActivityReport && !a.IsActivity))
                email.AssertHtmlViewContains("your organisation has not performed any searches in the past month");
            else
                email.AssertHtmlViewContains("Please find attached the activity report for " + organisation.FullName);
        }

        private static void AssertAdministratorEmail(MockEmail email, ICommunicationUser administrator, IOrganisation organisation, params ReportStatus[] statuses)
        {
            email.AssertAddresses(Return, Return, administrator);

            // Subject.

            email.AssertSubject(GetAdministratorSubject(organisation, statuses));
            
            // Attachments.

            email.AssertAttachments(GetAttachments(organisation, statuses));

            // Contents.

            if (statuses.Any(s => s.IsActivity))
            {
                email.AssertHtmlViewContains("Please find attached the activity report for " + organisation.FullName);
            }
            else
            {
                if (statuses.Length == 1)
                {
                    if (statuses[0].Report is ResumeSearchActivityReport)
                        email.AssertHtmlViewContains("No resume search activity recorded for " + organisation.FullName);
                    else
                        email.AssertHtmlViewContains("No job board activity recorded for " + organisation.FullName);
                }
                else
                {
                    email.AssertHtmlViewContains("No job board activity and no resume search activity recorded for " + organisation.FullName);
                }
            }
        }

        private static void Execute()
        {
            new EmailCustomerReportsTask(
                Resolve<IEmailsCommand>(),
                Resolve<IEmployerReportsQuery>(),
                Resolve<IEmployerReportsCommand>(),
                Resolve<IExecuteEmployerReportsCommand>(),
                Resolve<IOrganisationsQuery>(),
                Resolve<IAdministratorsQuery>(),
                Resolve<IAccountReportsQuery>()).ExecuteTask();
        }

        private bool CreateResumeSearchActivity(IEmployer employer)
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("test");
            var execution = new MemberSearchExecution
            {
                SearcherId = employer.Id,
                Criteria = criteria,
                StartTime = DateTime.Now.AddMonths(-1),
                Results = new MemberSearchResults { MemberIds = new Guid[0] }
            };
            _memberSearchesCommand.CreateMemberSearchExecution(execution);
            return true;
        }

        private bool CreateJobBoardActivity(IEmployer employer)
        {
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Tweak the JobAdStatus directly.

            var statusChanges = _jobAdsRepository.GetStatusChanges(jobAd.Id);
            Assert.AreEqual(1, statusChanges.Count);
            statusChanges[0].Time = DateTime.Now.AddMonths(-2);
            _jobAdsRepository.UpdateStatusChange(statusChanges[0]);

            return true;
        }

        private static bool NoActivity(IEmployer employer)
        {
            return false;
        }

        private static string GetSubject(IOrganisation organisation)
        {
            return "LinkMe usage report for " + organisation.FullName + " for " + ReportPeriodText;
        }

        private static string GetAdministratorSubject(IOrganisation organisation, params ReportStatus[] statuses)
        {
            if (statuses.Any(s => s.IsActivity))
                return GetSubject(organisation);

            if (statuses.Length == 1)
                return statuses[0].Report is ResumeSearchActivityReport
                    ? "No resume search activity for " + organisation.FullName + " for " + ReportPeriodText
                    : "No job board activity for " + organisation.FullName + " for " + ReportPeriodText;

            return "No job board activity and no resume search activity for " + organisation.FullName + " for " + ReportPeriodText;
        }

        private static MockEmailAttachment[] GetAttachments(IOrganisation organisation, params ReportStatus[] statuses)
        {
            // Include if there is activity or if marked.

            return (from a in statuses
                    where a.IsActivity || a.Report.ReportFileEvenIfNoResults
                    select new MockEmailAttachment { Name = GetReportFileName(organisation, a.Report), MediaType = "application/pdf" }).ToArray();
        }

        private static string GetReportFileName(IOrganisation organisation, EmployerReport report)
        {
            if (report is ResumeSearchActivityReport)
                return "Resume search activity - " + organisation.FullName + ".pdf";
            return "Job board activity - " + organisation.FullName + ".pdf";
        }

        private Administrator CreateAdministrator()
        {
            return _administratorAccountsCommand.CreateTestAdministrator(0);
        }

        private VerifiedOrganisation CreateOrganisation(Guid administratorId)
        {
            return _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administratorId, administratorId);
        }

        private Employer CreateEmployer(IOrganisation organisation)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            employer.Organisation = organisation;
            employer.CreatedTime = DateTime.Now.AddYears(-1); // Must exist before the reporting period
            _employerAccountsCommand.UpdateEmployer(employer);
            return employer;
        }
    }
}

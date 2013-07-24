using System;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Reports.Employers;
using LinkMe.Apps.Agents.Reports.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Organisations
{
    [TestClass]
    public class RunReportsTests
        : OrganisationsTests
    {
        private const string PromoCode = "ABCD";
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IEmployerReportsCommand _employerReportsCommand = Resolve<IEmployerReportsCommand>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdViewsCommand _jobAdViewsCommand = Resolve<IJobAdViewsCommand>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        private readonly IReferralsCommand _referralsCommand = Resolve<IReferralsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        private HtmlTextBoxTester _startDateTextBox;
        private HtmlTextBoxTester _endDateTextBox;
        private HtmlButtonTester _downloadButton;
        private HtmlButtonTester _downloadPdfButton;
        private HtmlButtonTester _searchButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _startDateTextBox = new HtmlTextBoxTester(Browser, "StartDate");
            _endDateTextBox = new HtmlTextBoxTester(Browser, "EndDate");
            _downloadButton = new HtmlButtonTester(Browser, "download");
            _downloadPdfButton = new HtmlButtonTester(Browser, "downloadpdf");
            _searchButton = new HtmlButtonTester(Browser, "search");
        }

        [TestMethod]
        public void TestResumeSearchActivityXlsNoDates()
        {
            TestNoDates("ResumeSearchActivityReport", _downloadButton);
        }

        [TestMethod]
        public void TestResumeSearchActivityPdfNoDates()
        {
            TestNoDates("ResumeSearchActivityReport", _downloadPdfButton);
        }

        [TestMethod]
        public void TestJobBoardActivityXlsNoDates()
        {
            TestNoDates("JobBoardActivityReport", _downloadButton);
        }

        [TestMethod]
        public void TestJobBoardActivityPdfNoDates()
        {
            TestNoDates("JobBoardActivityReport", _downloadPdfButton);
        }

        [TestMethod]
        public void TestCandidateCareNoDates()
        {
            TestNoDates("CandidateCareReport", _searchButton);
        }

        [TestMethod]
        public void TestResumeSearchActivityXlsNoResults()
        {
            TestNoResults("ResumeSearchActivityReport", _downloadButton);
        }

        [TestMethod]
        public void TestResumeSearchActivityPdfNoResults()
        {
            TestNoResults("ResumeSearchActivityReport", _downloadPdfButton);
        }

        [TestMethod]
        public void TestJobBoardActivityXlsNoResults()
        {
            TestNoResults("JobBoardActivityReport", _downloadButton);
        }

        [TestMethod]
        public void TestJobBoardActivityPdfNoResults()
        {
            TestNoResults("JobBoardActivityReport", _downloadPdfButton);
        }

        [TestMethod]
        public void TestCandidateCareNoResults()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            var report = _employerReportsCommand.CreateReportTemplate<CandidateCareReport>(organisation.Id);
            report.PromoCode = PromoCode;
            _employerReportsCommand.CreateReport(report);

            LogIn(administrator);
            var url = GetReportUrl(organisation, "candidateCareReport");
            Get(url);

            _startDateTextBox.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
            _endDateTextBox.Text = DateTime.Now.AddDays(-20).ToString("dd/MM/yyyy");

            _searchButton.Click();
            AssertErrorMessage("No results were returned for the specified criteria.");
        }

        [TestMethod]
        public void TestCandidateCareResults()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            var report = _employerReportsCommand.CreateReportTemplate<CandidateCareReport>(organisation.Id);
            report.PromoCode = PromoCode;
            _employerReportsCommand.CreateReport(report);

            var member = _memberAccountsCommand.CreateTestMember(1);
            _referralsCommand.CreateAffiliationReferral(new AffiliationReferral { RefereeId = member.Id, PromotionCode = PromoCode });

            LogIn(administrator);
            var url = GetReportUrl(organisation, "candidateCareReport");
            Get(url);

            _startDateTextBox.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
            _endDateTextBox.Text = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy");

            _searchButton.Click();
            AssertErrorMessage(organisation.Name + " has referred 1 member during this reporting period.");
        }

        [TestMethod]
        public void TestResumeSearchActivityXlsNoSavedReportResults()
        {
            TestResults<ResumeSearchActivityReport>(false, CreateVerifiedOrganisation, TestXlsResults);
        }

        [TestMethod]
        public void TestResumeSearchActivityPdfNoSavedReportResults()
        {
            TestResults<ResumeSearchActivityReport>(false, CreateVerifiedOrganisation, TestPdfResults);
        }

        [TestMethod]
        public void TestResumeSearchActivityXlsSavedReportResults()
        {
            TestResults<ResumeSearchActivityReport>(true, CreateVerifiedOrganisation, TestXlsResults);
        }

        [TestMethod]
        public void TestResumeSearchActivityPdfSavedReportResults()
        {
            TestResults<ResumeSearchActivityReport>(true, CreateVerifiedOrganisation, TestPdfResults);
        }

        [TestMethod]
        public void TestJobBoardActivityXlsNoSavedReportResults()
        {
            TestResults<JobBoardActivityReport>(false, CreateVerifiedOrganisation, TestXlsResults);
        }

        [TestMethod]
        public void TestJobBoardActivityPdfNoSavedReportResults()
        {
            TestResults<JobBoardActivityReport>(false, CreateVerifiedOrganisation, TestPdfResults);
        }

        [TestMethod]
        public void TestJobBoardActivityXlsSavedReportResults()
        {
            TestResults<JobBoardActivityReport>(true, CreateVerifiedOrganisation, TestXlsResults);
        }

        [TestMethod]
        public void TestJobBoardActivityPdfSavedReportResults()
        {
            TestResults<JobBoardActivityReport>(true, CreateVerifiedOrganisation, TestPdfResults);
        }

        [TestMethod]
        public void TestChildResumeSearchActivityXlsNoSavedReportResults()
        {
            TestResults<ResumeSearchActivityReport>(false, CreateChildVerifiedOrganisation, TestXlsResults);
        }

        [TestMethod]
        public void TestChildResumeSearchActivityPdfNoSavedReportResults()
        {
            TestResults<ResumeSearchActivityReport>(false, CreateChildVerifiedOrganisation, TestPdfResults);
        }

        [TestMethod]
        public void TestChildResumeSearchActivityXlsSavedReportResults()
        {
            TestResults<ResumeSearchActivityReport>(true, CreateChildVerifiedOrganisation, TestXlsResults);
        }

        [TestMethod]
        public void TestChildResumeSearchActivityPdfSavedReportResults()
        {
            TestResults<ResumeSearchActivityReport>(true, CreateChildVerifiedOrganisation, TestPdfResults);
        }

        [TestMethod]
        public void TestChildJobBoardActivityXlsNoSavedReportResults()
        {
            TestResults<JobBoardActivityReport>(false, CreateChildVerifiedOrganisation, TestXlsResults);
        }

        [TestMethod]
        public void TestChildJobBoardActivityPdfNoSavedReportResults()
        {
            TestResults<JobBoardActivityReport>(false, CreateChildVerifiedOrganisation, TestPdfResults);
        }

        [TestMethod]
        public void TestChildJobBoardActivityXlsSavedReportResults()
        {
            TestResults<JobBoardActivityReport>(true, CreateChildVerifiedOrganisation, TestXlsResults);
        }

        [TestMethod]
        public void TestChildJobBoardActivityPdfSavedReportResults()
        {
            TestResults<JobBoardActivityReport>(true, CreateChildVerifiedOrganisation, TestPdfResults);
        }

        [TestMethod]
        public void TestResumeSearchActivityXlsCredits()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = CreateVerifiedOrganisation(administrator.Id);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            var member = _memberAccountsCommand.CreateTestMember(1);

            // Assign some credits.

            _allocationsCommand.CreateAllocation(new Allocation
            {
                CreatedTime = DateTime.Now.AddDays(-60),
                CreditId = _creditsQuery.GetCredit<ContactCredit>().Id,
                InitialQuantity = 100,
                OwnerId = organisation.Id,
            });

            for (var index = 2; index < 20; ++index)
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, _memberAccountsCommand.CreateTestMember(index)), MemberAccessReason.PhoneNumberViewed);

            _employerMemberViewsCommand.ViewMember(_app, employer, member);
            var report = _employerReportsCommand.CreateReportTemplate<ResumeSearchActivityReport>(organisation.Id);

            TestResults(administrator, organisation, report, ".xls", "application/vnd.ms-excel", _downloadButton);
        }

        private IOrganisation CreateVerifiedOrganisation(Guid administratorId)
        {
            return _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administratorId);
        }

        private IOrganisation CreateChildVerifiedOrganisation(Guid administratorId)
        {
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administratorId);
            return _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administratorId);
        }

        private void TestResults<T>(bool createReport, Func<Guid, IOrganisation> createOrganisation, Action<Administrator, IOrganisation, EmployerReport> test)
            where T : EmployerReport
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = createOrganisation(administrator.Id);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            var member = _memberAccountsCommand.CreateTestMember(1);

            if (typeof(T) == typeof(ResumeSearchActivityReport))
            {
                _employerMemberViewsCommand.ViewMember(_app, employer, member);
            }
            else if (typeof(T) == typeof(JobBoardActivityReport))
            {
                var jobAd = _jobAdsCommand.PostTestJobAd(employer);
                _jobAdViewsCommand.ViewJobAd(Guid.NewGuid(), jobAd.Id);
                var application = new InternalApplication { ApplicantId = Guid.NewGuid() };
                _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
                _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
                _jobAdsCommand.CloseJobAd(jobAd);
            }

            var report = _employerReportsCommand.CreateReportTemplate<T>(organisation.Id);
            if (createReport)
                _employerReportsCommand.CreateReport(report);
            test(administrator, organisation, report);
        }

        private void TestXlsResults(Administrator administrator, IOrganisation organisation, EmployerReport report)
        {
            TestResults(administrator, organisation, report, ".xls", "application/vnd.ms-excel", _downloadButton);
        }

        private void TestPdfResults(Administrator administrator, IOrganisation organisation, EmployerReport report)
        {
            TestResults(administrator, organisation, report, ".pdf", "application/pdf", _downloadPdfButton);
        }

        private void TestResults(IUser administrator, IOrganisation organisation, EmployerReport report, string extension, string contentType, HtmlButtonTester button)
        {
            // Create a result.

            var startDate = DateTime.Now.AddDays(-30).Date;
            var endDate = DateTime.Now.AddDays(10).Date;

            LogIn(administrator);
            var url = GetReportUrl(organisation, report.GetType().Name);
            Get(url);

            _startDateTextBox.Text = startDate.ToString("dd/MM/yyyy");
            _endDateTextBox.Text = endDate.ToString("dd/MM/yyyy");
            button.Click();

            // Not sure how to check the contents but look for the appropriate headers etc to at least make sure a report was generated.

            Assert.AreEqual(contentType, Browser.ResponseHeaders["Content-Type"]);
            Assert.AreEqual("attachment; filename=\"" + organisation.FullName.Replace(Organisation.FullNameSeparator, '-') + " - " + report.Name + extension + "\"", Browser.ResponseHeaders["Content-Disposition"]);
        }

        private void TestNoDates(string report, HtmlButtonTester button)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            var url = GetReportUrl(organisation, report);
            Get(url);

            Assert.AreEqual(string.Empty, _startDateTextBox.Text);
            Assert.AreEqual(string.Empty, _endDateTextBox.Text);

            button.Click();
            AssertErrorMessages("The start date is required.", "The end date is required.");

            _startDateTextBox.Text = DateTime.Now.AddDays(-10).Date.ToString("dd/MM/yyyy");
            button.Click();
            AssertErrorMessages("The end date is required.");
            AssertPageDoesNotContain("The start date is required.");
        }

        private void TestNoResults(string report, HtmlButtonTester button)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            var url = GetReportUrl(organisation, report);
            Get(url);

            _startDateTextBox.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
            _endDateTextBox.Text = DateTime.Now.AddDays(-20).ToString("dd/MM/yyyy");

            button.Click();
            AssertErrorMessage("No results were returned for the specified criteria.");
        }
    }
}

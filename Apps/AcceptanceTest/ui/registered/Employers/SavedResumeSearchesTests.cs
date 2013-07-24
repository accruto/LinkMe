using System;
using System.Linq;
using System.Web;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Presentation.Domain.Users.Members.Search;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.Web.UI.Registered.Employers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.UI.Registered.Employers
{
    [TestClass]
    public class SavedResumeSearchesTests
        : WebFormDataTestCase
    {
        private readonly IDbConnectionFactory _connectionFactory = Resolve<IDbConnectionFactory>();
        private readonly IMemberSearchesCommand _memberSearchesCommand = Resolve<IMemberSearchesCommand>();
        private readonly IMemberSearchesQuery _memberSearchesQuery = Resolve<IMemberSearchesQuery>();
        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery = Resolve<IMemberSearchAlertsQuery>();

        private const string JobTitle = "superman";

        private HtmlTextBoxTester _keywordsTextBox;
        private HtmlButtonTester _searchButton;
        private HtmlLinkButtonTester _lbCreateAlert;
        private HtmlLinkButtonTester _lbRemove;
        //private LinkButtonTester _removeSavedSearchAlert;
        private HtmlLinkButtonTester _lbExecute;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();

            _keywordsTextBox = new HtmlTextBoxTester(Browser, "Keywords");
            _searchButton = new HtmlButtonTester(Browser, "search");
            _lbCreateAlert = new HtmlLinkButtonTester(Browser, "ctl00_ctl00_Body_FormContent_savedResumeSearchesControl_rptSavedResumeSearches_ctl01_lbCreateAlert");
            _lbRemove = new HtmlLinkButtonTester(Browser, "ctl00_ctl00_Body_FormContent_savedResumeSearchesControl_rptSavedResumeSearches_ctl01_lbRemove");
            //_removeSavedSearchAlert = new LinkButtonTester("SavedResumeSearchAlertsRepeater_ctl00_removeSavedSearchAlert", CurrentPageFormContent);
            _lbExecute = new HtmlLinkButtonTester(Browser, "ctl00_ctl00_Body_FormContent_savedResumeSearchesControl_rptSavedResumeSearches_ctl01_lbExecute");
        }

        [TestMethod]
        public void TestSave()
        {
            CreateMember();
            var employer = CreateEmployer();
            LogIn(employer);

            // Search.

            Get(new ReadOnlyApplicationUrl("~/search/candidates"));
            _keywordsTextBox.Text = JobTitle;
            _searchButton.Click();

            // Save.

            const string name = "MySearch";
            EmulateSaveSearchPopup(name, false);

            // Check.

            var search = AssertSearch(employer.Id, false);
            Assert.AreEqual(name, search.Name, "Unexpected search name.");
            AssertSearchPages(search, false);
        }

        [TestMethod]
        public void TestSaveRemove()
        {
            CreateMember();
            var employer = CreateEmployer();
            LogIn(employer);

            // Search.

            Get(new ReadOnlyApplicationUrl("~/search/candidates"));
            _keywordsTextBox.Text = JobTitle;
            _searchButton.Click();

            // Save.

            const string name = "MySearch";
            EmulateSaveSearchPopup(name, false);

            // Remove.

            var search = AssertSearch(employer.Id, false);
            GetPage<SavedResumeSearches>();
            _lbRemove.Click();

            AssertPage<SavedResumeSearches>();
            AssertPageContains("You currently have no saved searches.");
            AssertNoSearchPages(search);
        }

        [TestMethod]
        public void TestSaveSearch()
        {
            CreateMember();
            var employer = CreateEmployer();
            LogIn(employer);

            // Search.

            Get(new ReadOnlyApplicationUrl("~/search/candidates"));
            _keywordsTextBox.Text = JobTitle;
            _searchButton.Click();

            // Save.

            const string name = "MySearch";
            EmulateSaveSearchPopup(name, false);
            AssertSearch(employer.Id, false);

            // Search.

            GetPage<SavedResumeSearches>();
            _lbExecute.Click();

            var savedSearchId = _memberSearchesQuery.GetMemberSearches(employer.Id)[0].Id;

            AssertUrl(new ReadOnlyApplicationUrl(true, "~/search/candidates/saved/" + savedSearchId));
        }

        [TestMethod]
        public void TestExecuteSavedSearch()
        {
            // The previous test case doesn't result in the association between MemberSearch and MemberSearchExecution being created
            // because of the initial caching of results.  Explicitly create the MemberSearch here first and then search.

            CreateMember();
            var employer = CreateEmployer();
            var search = new MemberSearch { Name = "My Search", Criteria = new MemberSearchCriteria() };
            search.Criteria.SetKeywords(JobTitle);
            _memberSearchesCommand.CreateMemberSearch(employer, search);

            LogIn(employer);

            // Search.

            GetPage<SavedResumeSearches>();
            _lbExecute.Click();

            var savedSearchId = _memberSearchesQuery.GetMemberSearches(employer.Id)[0].Id;

            AssertUrl(new ReadOnlyApplicationUrl(true, "~/search/candidates/saved/" + savedSearchId));
            AssertSearchExecution(employer.Id, search);
        }

        [TestMethod]
        public void TestSaveSearchRemove()
        {
            CreateMember();
            var employer = CreateEmployer();
            LogIn(employer);

            // Search.

            Get(new ReadOnlyApplicationUrl("~/search/candidates"));
            _keywordsTextBox.Text = JobTitle;
            _searchButton.Click();

            // Save.

            const string name = "MySearch";
            EmulateSaveSearchPopup(name, false);

            // Search.

            GetPage<SavedResumeSearches>();
            _lbExecute.Click();

            // Remove.

            var search = AssertSearch(employer.Id, false);
            GetPage<SavedResumeSearches>();
            _lbRemove.Click();

            AssertPage<SavedResumeSearches>();
            AssertPageContains("You currently have no saved searches");
            AssertNoSearchPages(search);
        }

        [TestMethod]
        public void TestSaveSearchRemoveOld()
        {
            // In previous versions the criteria was shared between the various database entities.
            // Need to make sure that for these cases deleting the search does not result in
            // a foreign key violation for the execution.

            CreateMember();
            var employer = CreateEmployer();
            LogIn(employer);

            // Search.

            Get(new ReadOnlyApplicationUrl("~/search/candidates"));
            _keywordsTextBox.Text = JobTitle;
            _searchButton.Click();

            // Save.

            const string name = "MySearch";
            EmulateSaveSearchPopup(name, false);

            // Search.

            GetPage<SavedResumeSearches>();
            _lbExecute.Click();

            // Update the database so the criteria are shared.

            var search = AssertSearch(employer.Id, false);
            UpdateExecutionCriteria(employer.Id, search);

            // Remove.

            GetPage<SavedResumeSearches>();
            _lbRemove.Click();

            AssertPage<SavedResumeSearches>();
            AssertPageContains("You currently have no saved searches");
            AssertNoSearchPages(search);
        }

        [TestMethod]
        public void TestSaveAlert()
        {
            CreateMember();
            var employer = CreateEmployer();
            LogIn(employer);

            // Search.

            Get(new ReadOnlyApplicationUrl("~/search/candidates"));
            _keywordsTextBox.Text = JobTitle;
            _searchButton.Click();

            // Save.

            const string name = "MyAlert";
            EmulateSaveSearchPopup(name, true);

            // Check.

            var search = AssertSearch(employer.Id, true);
            Assert.AreEqual(name, search.Name, "Unexpected alert name.");
            AssertSearchPages(search, true);
        }

        [TestMethod]
        public void TestSaveSaveAlert()
        {
            CreateMember();
            var employer = CreateEmployer();
            LogIn(employer);

            // Search.

            Get(new ReadOnlyApplicationUrl("~/search/candidates"));
            _keywordsTextBox.Text = JobTitle;
            _searchButton.Click();

            AssertUrl(new ReadOnlyApplicationUrl(true, "~/search/candidates/results"));

            // Save.

            const string name = "MySearch";
            EmulateSaveSearchPopup(name, false);

            // Check.

            var search = AssertSearch(employer.Id, false);
            Assert.AreEqual(name, search.Name, "Unexpected search name.");
            AssertSearchPages(search, false);

            // Save as alert.

            GetPage<SavedResumeSearches>();
            _lbCreateAlert.Click();

            // Check.

            search = AssertSearch(employer.Id, true);
            AssertSearchPages(search, true);
        }

        private void CreateMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        private MemberSearch AssertSearch(Guid employerId, bool isAlert)
        {
            var searches = _memberSearchesQuery.GetMemberSearches(employerId);
            Assert.AreEqual(1, searches.Count);

            var alert = _memberSearchAlertsQuery.GetMemberSearchAlert(searches[0].Id, AlertType.Email);
            if (isAlert)
                Assert.IsNotNull(alert);
            else
                Assert.IsNull(alert);

            return searches[0];
        }

        private void AssertSearchExecution(Guid employerId, MemberSearch search)
        {
            var executions = _memberSearchesQuery.GetMemberSearchExecutions(employerId);

            // There should be one that corresponds to the search.

            Assert.AreEqual(true, (from e in executions where e.SearchId == search.Id select e).Any());
        }

        private void AssertSearchPages(MemberSearch search, bool isAlert)
        {
            GetPage<SavedResumeSearches>();
            AssertPageContains(search.GetDisplayHtml());

            GetPage<SavedResumeSearchAlerts>();
            if (isAlert)
                AssertPageContains(search.GetDisplayHtml());
            else
                AssertPageDoesNotContain(search.GetDisplayHtml());
        }

        private void AssertNoSearchPages(MemberSearch search)
        {
            GetPage<SavedResumeSearches>();
            AssertPageDoesNotContain(search.GetDisplayHtml());
            GetPage<SavedResumeSearchAlerts>();
            AssertPageDoesNotContain(search.GetDisplayHtml());
        }

        private void UpdateExecutionCriteria(Guid employerId, MemberSearch search)
        {
            var execution = _memberSearchesQuery.GetMemberSearchExecutions(employerId)[0];

            // Update the execution to point to the search's criteria.

            DatabaseHelper.ExecuteNonQuery(
                _connectionFactory,
                "UPDATE dbo.ResumeSearch SET criteriaSetId = @newSetId WHERE criteriaSetId = @oldSetId",
                "@newSetId",
                search.Criteria.Id,
                "@oldSetId",
                execution.Criteria.Id);

            // Delete the existing criteria for the execution.

            DatabaseHelper.ExecuteNonQuery(
                _connectionFactory,
                "DELETE dbo.ResumeSearchCriteria WHERE setId = @setId",
                "@setId",
                execution.Criteria.Id);
            
            DatabaseHelper.ExecuteNonQuery(
                _connectionFactory,
                "DELETE dbo.ResumeSearchCriteriaSet WHERE id = @setId",
                "@setId",
                execution.Criteria.Id);

            // Check.

            execution = _memberSearchesQuery.GetMemberSearchExecutions(employerId)[0];
            Assert.AreEqual(search.Criteria.Id, execution.Criteria.Id);
        }

        private void EmulateSaveSearchPopup(string searchName, bool isAlert)
        {
            var form = HttpUtility.ParseQueryString(string.Empty);
            form["name"] = searchName;
            form["isAlert"] = isAlert.ToString();

            var serviceUrl = new ReadOnlyApplicationUrl("~/employers/searches/api/save");
            Post(serviceUrl, form);
        }
    }
}

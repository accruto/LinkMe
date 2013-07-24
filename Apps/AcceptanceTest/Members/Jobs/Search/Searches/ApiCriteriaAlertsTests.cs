using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches
{
    public abstract class ApiCriteriaAlertsTests
        : ApiAlertsTests
    {
        private const string SearchName = "My search";

        protected abstract JobAdSearchCriteria GetCriteria();

        [TestMethod]
        public void TestCreateSearchFromCurrent()
        {
            var member = CreateMember();
            LogIn(member);

            // Do a search.

            var criteria = GetCriteria();
            Get(GetSearchUrl(criteria));

            // Save as alert.

            AssertJsonSuccess(CreateFromCurrent(SearchName, false));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.AreEqual(SearchName, searches[0].Name);
            Assert.AreEqual(criteria, searches[0].Criteria);

            var alert = _jobAdSearchAlertsQuery.GetJobAdSearchAlert(searches[0].Id);
            Assert.IsNull(alert);
        }

        [TestMethod]
        public void TestCreateAlertFromCurrent()
        {
            var member = CreateMember();
            LogIn(member);

            // Do a search.

            var criteria = GetCriteria();
            Get(GetSearchUrl(criteria));

            // Save as alert.

            AssertJsonSuccess(CreateFromCurrent(SearchName, true));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.AreEqual(SearchName, searches[0].Name);
            Assert.AreEqual(criteria, searches[0].Criteria);

            var alert = _jobAdSearchAlertsQuery.GetJobAdSearchAlert(searches[0].Id);
            Assert.IsNotNull(alert);
        }

        [TestMethod]
        public void TestCreateSearchNoNameFromCurrent()
        {
            var member = CreateMember();
            LogIn(member);

            // Do a search.

            var criteria = GetCriteria();
            Get(GetSearchUrl(criteria));

            // Save as alert.

            AssertJsonSuccess(CreateFromCurrent(null, false));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.IsNull(searches[0].Name);
            Assert.AreEqual(criteria, searches[0].Criteria);

            var alert = _jobAdSearchAlertsQuery.GetJobAdSearchAlert(searches[0].Id);
            Assert.IsNull(alert);
        }

        [TestMethod]
        public void TestCreateAlertNoNameFromCurrent()
        {
            var member = CreateMember();
            LogIn(member);

            // Do a search.

            var criteria = GetCriteria();
            Get(GetSearchUrl(criteria));

            // Save as alert.

            AssertJsonSuccess(CreateFromCurrent(null, true));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.IsNull(searches[0].Name);
            Assert.AreEqual(criteria, searches[0].Criteria);

            var alert = _jobAdSearchAlertsQuery.GetJobAdSearchAlert(searches[0].Id);
            Assert.IsNotNull(alert);
        }

        [TestMethod]
        public void TestCreateFromPrevious()
        {
            var member = CreateMember();
            LogIn(member);

            // Do a search.

            var criteria = GetCriteria();
            Get(GetSearchUrl(criteria));

            // Save as alert.

            var execution = _jobAdSearchesQuery.GetJobAdSearchExecutions(member.Id, 10)[0];
            AssertJsonSuccess(CreateFromPrevious(execution.Id));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.IsNull(searches[0].Name);
            Assert.AreEqual(criteria, searches[0].Criteria);

            var alert = _jobAdSearchAlertsQuery.GetJobAdSearchAlert(searches[0].Id);
            Assert.IsNotNull(alert);
        }

        [TestMethod]
        public void TestCreateFromSavedNoName()
        {
            var member = CreateMember();
            LogIn(member);

            // Save a search.

            var criteria = GetCriteria();
            var search = new JobAdSearch { Criteria = criteria };
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, search);

            // Save as alert.

            AssertJsonSuccess(CreateFromSaved(search.Id));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.IsNull(searches[0].Name);
            Assert.AreEqual(criteria, searches[0].Criteria);

            var alert = _jobAdSearchAlertsQuery.GetJobAdSearchAlert(searches[0].Id);
            Assert.IsNotNull(alert);
        }

        [TestMethod]
        public void TestCreateFromSavedName()
        {
            var member = CreateMember();
            LogIn(member);

            // Save a search.

            var criteria = GetCriteria();
            var search = new JobAdSearch { Criteria = criteria, Name = SearchName };
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, search);

            // Save as alert.

            AssertJsonSuccess(CreateFromSaved(search.Id));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.AreEqual(SearchName, searches[0].Name);
            Assert.AreEqual(criteria, searches[0].Criteria);

            var alert = _jobAdSearchAlertsQuery.GetJobAdSearchAlert(searches[0].Id);
            Assert.IsNotNull(alert);
        }
    }
}

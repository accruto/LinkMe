using System;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches.Mobile
{
    [TestClass]
    public class SavedSearchesTests
        : SearchesTests
    {
        private const string Keywords = "Archeologist";
        private const string SearchName = "My saved search";

        [TestMethod]
        public void TestNoSavedSearches()
        {
            var member = CreateMember(0);
            LogIn(member);

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);
            AssertEmptyText("You don't have any favourite searches.");
            AssertSavedSearches();
        }

        [TestMethod]
        public void TestSavedSearch()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new JobAdSearch { Criteria = criteria, Name = SearchName };
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, search);

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);
            AssertNoEmptyText();
            AssertSavedSearches(Tuple.Create(search, false));
        }

        [TestMethod]
        public void TestSavedSearchNoName()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new JobAdSearch { Criteria = criteria };
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, search);

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);
            AssertNoEmptyText();
            AssertSavedSearches(Tuple.Create(search, false));
        }

        [TestMethod]
        public void TestSavedAlert()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new JobAdSearch { Criteria = criteria, Name = SearchName };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, search, DateTime.Now);

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);
            AssertNoEmptyText();
            AssertSavedSearches(Tuple.Create(search, true));
        }

        [TestMethod]
        public void TestSavedAlertNoName()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new JobAdSearch { Criteria = criteria };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, search, DateTime.Now);

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);
            AssertNoEmptyText();
            AssertSavedSearches(Tuple.Create(search, true));
        }
    }
}
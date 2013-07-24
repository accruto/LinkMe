using System;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches.Web
{
    [TestClass]
    public class SavedSearchesTests
        : SearchesTests
    {
        private readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand = Resolve<IJobAdSearchAlertsCommand>();

        private ReadOnlyUrl _savedSearchesUrl;
        private ReadOnlyUrl _partialSavedSearchesUrl;

        private const string Keywords = "Archeologist";
        private const string SearchName = "My saved search";

        [TestInitialize]
        public void TestInitialize()
        {
            _savedSearchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/saved");
            _partialSavedSearchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/partial/saved");
        }

        [TestMethod]
        public void TestNoSavedSearches()
        {
            var member = CreateMember(0);
            LogIn(member);

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);
            AssertEmptyText("You don't have any favourite searches.");
            AssertSearches();

            Get(_partialSavedSearchesUrl);
            AssertEmptyText("You don't have any favourite searches.");
            AssertSearches();
        }

        [TestMethod]
        public void TestSavedSearch()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, new JobAdSearch { Criteria = criteria, Name = SearchName });

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);
            AssertNoEmptyText();
            AssertSearches(SearchName);

            Get(_partialSavedSearchesUrl);
            AssertNoEmptyText();
            AssertSearches(SearchName);
        }

        [TestMethod]
        public void TestSavedSearchNoName()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, new JobAdSearch { Criteria = criteria });

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);
            AssertNoEmptyText();
            AssertSearches(Keywords);

            Get(_partialSavedSearchesUrl);
            AssertNoEmptyText();
            AssertSearches(Keywords);
        }

        [TestMethod]
        public void TestSavedAlert()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, new JobAdSearch { Criteria = criteria, Name = SearchName }, DateTime.Now);

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);
            AssertNoEmptyText();
            AssertSearches(SearchName);

            Get(_partialSavedSearchesUrl);
            AssertNoEmptyText();
            AssertSearches(SearchName);
        }

        [TestMethod]
        public void TestSavedAlertNoName()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, new JobAdSearch { Criteria = criteria }, DateTime.Now);

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);
            AssertNoEmptyText();
            AssertSearches(Keywords);

            Get(_partialSavedSearchesUrl);
            AssertNoEmptyText();
            AssertSearches(Keywords);
        }
    }
}
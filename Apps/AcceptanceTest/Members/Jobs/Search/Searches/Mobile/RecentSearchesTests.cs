using System;
using System.Linq;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches.Mobile
{
    [TestClass]
    public class RecentSearchesTests
        : SearchesTests
    {
        private ReadOnlyUrl _recentSearchesUrl;

        private const string Keywords = "Archeologist";

        [TestInitialize]
        public void TestInitialize()
        {
            _recentSearchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/recent");
        }

        [TestMethod]
        public void TestNoRecentSearches()
        {
            var member = CreateMember(0);
            LogIn(member);

            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);
            AssertEmptyText("You don't have any recent searches.");
            AssertRecentSearches();
        }

        [TestMethod]
        public void TestRecentSearch()
        {
            var member = CreateMember(0);
            Search(member.Id, Keywords, DateTime.Now);

            LogIn(member);

            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);
            AssertNoEmptyText();
            AssertRecentSearches(Keywords);
        }

        [TestMethod]
        public void TestConsolidation()
        {
            var member = CreateMember(0);

            const int count = 72;
            for (var index = 0; index < count; ++index)
                Search(member.Id, Keywords + "A", DateTime.Now.AddDays(-1 * index));
            for (var index = 0; index < count; ++index)
                Search(member.Id, Keywords + "B", DateTime.Now.AddDays(-1 * index - 1));

            LogIn(member);
            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);
            AssertNoEmptyText();

            AssertRecentSearches(Keywords + "A", Keywords + "B");
        }

        [TestMethod]
        public void TestDate()
        {
            var member = CreateMember(0);

            const int count = 12;
            for (var index = 1; index <= count; ++index)
                Search(member.Id, Keywords + index, DateTime.Now.AddMonths(-1 * index).AddDays(1));

            LogIn(member);
            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);
            AssertNoEmptyText();

            // Only the last 6 months of searches are considered.

            AssertRecentSearches((from i in Enumerable.Range(1, 6) select Keywords + i).ToArray());
        }

        private void Search(Guid memberId, string keywords, DateTime startTime)
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(keywords);
            _jobAdSearchesCommand.CreateJobAdSearchExecution(new JobAdSearchExecution
            {
                Criteria = criteria,
                SearcherId = memberId,
                StartTime = startTime,
                Results = new JobAdSearchResults(),
                Context = "test"
            });
        }
    }
}
using System;
using System.Net;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches
{
    [TestClass]
    public class ApiAlertsErrorTests
        : ApiAlertsTests
    {
        private const string SearchName = "My search";
        private const string Keywords = "Archeologist";

        [TestMethod]
        public void TestCreateNoCurrentSearch()
        {
            var member = CreateMember();
            LogIn(member);

            // Save as alert.

            AssertJsonError(CreateFromCurrent(HttpStatusCode.NotFound, SearchName, false), null, "400", "The current search cannot be found.");

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(0, searches.Count);
        }

        [TestMethod]
        public void TestCreateFromCurrentSameName()
        {
            var member = CreateMember();
            LogIn(member);

            // Create first.

            var criteria = GetCriteria(0);
            Get(GetSearchUrl(criteria));
            AssertJsonSuccess(CreateFromCurrent(SearchName, true));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);

            // Save again.

            criteria = GetCriteria(1);
            Get(GetSearchUrl(criteria));
            AssertJsonError(CreateFromCurrent(HttpStatusCode.Forbidden, SearchName, true), "Name", "300", "The name is already being used.");

            // Check.

            searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
        }

        [TestMethod]
        public void TestCreateNoPrevious()
        {
            var member = CreateMember();
            LogIn(member);

            AssertJsonError(CreateFromPrevious(HttpStatusCode.NotFound, Guid.NewGuid()), null, "400", "The search cannot be found.");

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(0, searches.Count);
        }

        [TestMethod]
        public void TestCreateNoSaved()
        {
            var member = CreateMember();
            LogIn(member);

            // Save as alert.

            AssertJsonError(CreateFromSaved(HttpStatusCode.NotFound, Guid.NewGuid()), null, "400", "The search cannot be found.");

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(0, searches.Count);
        }

        private static JobAdSearchCriteria GetCriteria(int index)
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords + index);
            return criteria;
        }
    }
}

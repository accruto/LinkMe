using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Searches
{
    [TestClass]
    public class SearchesTests
        : SearchTests
    {
        private readonly IMemberSearchesCommand _memberSearchesCommand = Resolve<IMemberSearchesCommand>();

        private const string SearchName = "My search";

        [TestMethod]
        public void TestLocation()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            criteria.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Melbourne");
            TestCriteria(criteria);
        }

        [TestMethod]
        public void TestDistance()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            criteria.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Melbourne");
            criteria.Distance = 200;
            TestCriteria(criteria);
        }

        private void TestCriteria(MemberSearchCriteria criteria)
        {
            var employer = CreateEmployer();

            var search = new MemberSearch { Criteria = criteria, OwnerId = employer.Id, Name = SearchName };
            _memberSearchesCommand.CreateMemberSearch(employer, search);

            LogIn(employer);
            Get(GetSavedSearchUrl(search.Id));

            AssertCriteria(criteria);
        }
    }
}

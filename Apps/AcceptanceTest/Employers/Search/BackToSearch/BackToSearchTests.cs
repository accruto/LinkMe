using System;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.BackToSearch
{
    public abstract class BackToSearchTests
        : SearchTests
    {
        private const string Keywords = "Business";

        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        [TestMethod]
        public void TestEducationKeywords()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { EducationKeywords = "Test" });
        }

        [TestMethod]
        public void TestIndustryIds()
        {
            var employer = LogIn();
            var id1 = _industriesQuery.GetIndustries()[1].Id;
            var id2 = _industriesQuery.GetIndustries()[3].Id;
            Test(employer, new MemberSearchCriteria { IndustryIds = new Guid[0] });
            Test(employer, new MemberSearchCriteria { IndustryIds = new[] { id1 } });
            Test(employer, new MemberSearchCriteria { IndustryIds = new[] { id1, id2 } });
        }

        [TestMethod]
        public void TestJobTitle()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { JobTitle = "Test" });
        }

        [TestMethod]
        public void TestDesiredJobTitle()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { DesiredJobTitle = "Test" });
        }

        [TestMethod]
        public void TestJobTitlesToSearch()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { JobTitlesToSearch = JobsToSearch.RecentJobs });
            Test(employer, new MemberSearchCriteria { JobTitlesToSearch = JobsToSearch.AllJobs });
        }

        [TestMethod]
        public void TestSearchByName()
        {
            // Must have unlimited credits to search by name.

            var employer = LogIn();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });

            Test(employer, new MemberSearchCriteria { Name = ""});
            Test(employer, new MemberSearchCriteria { Name = "John Berry" });
            Test(employer, new MemberSearchCriteria { IncludeSimilarNames = false });
            Test(employer, new MemberSearchCriteria { IncludeSimilarNames = true });
        }

        [TestMethod]
        public void TestCompanyKeywords()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { CompanyKeywords = "Test" });
            Test(employer, new MemberSearchCriteria { CompanyKeywords = "Test", CompaniesToSearch = JobsToSearch.AllJobs });
            Test(employer, new MemberSearchCriteria { CompanyKeywords = "Test", CompaniesToSearch = JobsToSearch.RecentJobs });
            Test(employer, new MemberSearchCriteria { CompanyKeywords = "Test", CompaniesToSearch = JobsToSearch.LastJob });
        }

        [TestMethod]
        public void TestLocation()
        {
            var employer = LogIn();
            var country = _locationQuery.GetCountry("Australia");
            Test(employer, new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, null) });
            Test(employer, new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, "Melbourne") });
            Test(employer, new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, "VIC") });
            Test(employer, new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, "Melbourne VIC 3000") });
        }

        [TestMethod]
        public void TestDistance()
        {
            var employer = LogIn();
            var country = _locationQuery.GetCountry("Australia");
            Test(employer, new MemberSearchCriteria { Distance = null });
            Test(employer, new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, "Melbourne"), Distance = 2 * MemberSearchCriteria.DefaultDistance });
        }

        [TestMethod]
        public void TestIncludeRelocating()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { IncludeRelocating = MemberSearchCriteria.DefaultIncludeRelocating });
            Test(employer, new MemberSearchCriteria { IncludeRelocating = !MemberSearchCriteria.DefaultIncludeRelocating });
        }

        [TestMethod]
        public void TestIncludeInternational()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { IncludeInternational = MemberSearchCriteria.DefaultIncludeInternational });
            Test(employer, new MemberSearchCriteria { IncludeRelocating = true, IncludeInternational = !MemberSearchCriteria.DefaultIncludeInternational });
        }

        [TestMethod]
        public void TestAnyKeywords()
        {
            var employer = LogIn();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test", null, "Test1 Test2", null);
            Test(employer, criteria);

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test", null, "Test1 Test2 Test3", null);
            Test(employer, criteria);

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test", null, "Test1 Test2 Test3 Test4", null);
            Test(employer, criteria);
        }

        [TestMethod]
        public void TestAllKeywords()
        {
            var employer = LogIn();
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test1 Test2", null, null, null);
            Test(employer, criteria);
        }

        [TestMethod]
        public void TestExactPhrase()
        {
            var employer = LogIn();
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, "Test1 Test2", null, null);
            Test(employer, criteria);
        }

        [TestMethod]
        public void TestWithoutKeywords()
        {
            var employer = LogIn();
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test1", null, null, "Test2");
            Test(employer, criteria);
        }

        [TestMethod]
        public void TestAllKeywordsCombination()
        {
            var employer = LogIn();
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test1", "Test2 Test3", "Test4 Test5", "Test6");
            Test(employer, criteria);
        }

        [TestMethod]
        public void TestKeywords()
        {
            var employer = LogIn();
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test");
            Test(employer, criteria);
        }

        [TestMethod]
        public void TestSalary()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { Salary = new Salary { Currency = Currency.AUD, Rate = SalaryRate.Year } });
            Test(employer, new MemberSearchCriteria { Salary = new Salary { LowerBound = 10000, Currency = Currency.AUD, Rate = SalaryRate.Year } });
            Test(employer, new MemberSearchCriteria { Salary = new Salary { UpperBound = 50000, Currency = Currency.AUD, Rate = SalaryRate.Year } });
            Test(employer, new MemberSearchCriteria { Salary = new Salary { LowerBound = 10000, UpperBound = 50000, Currency = Currency.AUD, Rate = SalaryRate.Year } });
        }

        [TestMethod]
        public void TestRecency()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { Recency = new TimeSpan(10, 0, 0, 0) });
        }

        [TestMethod]
        public void TestIncludeSynonyms()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { IncludeSynonyms = MemberSearchCriteria.DefaultIncludeSynonyms });
            Test(employer, new MemberSearchCriteria { IncludeSynonyms = !MemberSearchCriteria.DefaultIncludeSynonyms });
        }

        [TestMethod]
        public void TestSortOrder()
        {
            var employer = LogIn();
            Assert.AreEqual(MemberSortOrder.Relevance, MemberSearchCriteria.DefaultSortOrder);

            Test(employer, new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSearchCriteria.DefaultSortOrder } });
            Test(employer, new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.Availability } });
            Test(employer, new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSearchCriteria.DefaultSortOrder, ReverseSortOrder = false } });
            Test(employer, new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.Availability, ReverseSortOrder = true } });
        }

        [TestMethod]
        public void TestInFolder()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { InFolder = true });
            Test(employer, new MemberSearchCriteria { InFolder = false });
        }

        [TestMethod]
        public void TestIsFlagged()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { IsFlagged = true });
            Test(employer, new MemberSearchCriteria { IsFlagged = false });
        }

        [TestMethod]
        public void TestHasNotes()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { HasNotes = true });
            Test(employer, new MemberSearchCriteria { HasNotes = false });
        }

        [TestMethod]
        public void TestHasViewed()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { HasViewed = true });
            Test(employer, new MemberSearchCriteria { HasViewed = false });
        }

        [TestMethod]
        public void TestIsUnlocked()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { IsUnlocked = true });
            Test(employer, new MemberSearchCriteria { IsUnlocked = false });
        }

        [TestMethod]
        public void TestEthnicStatus()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { EthnicStatus = EthnicStatus.Aboriginal });
            Test(employer, new MemberSearchCriteria { EthnicStatus = EthnicStatus.Aboriginal | EthnicStatus.TorresIslander });
        }

        [TestMethod]
        public void TestVisaStatus()
        {
            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { VisaStatusFlags = VisaStatusFlags.Citizen });
            Test(employer, new MemberSearchCriteria { VisaStatusFlags = VisaStatusFlags.RestrictedWorkVisa | VisaStatusFlags.UnrestrictedWorkVisa });
        }

        [TestMethod]
        public void TestCommunityId()
        {
            // Create a couple of communities.

            var community1 = TestCommunity.Ahri.GetCommunityTestData().CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var community2 = TestCommunity.Rcsa.GetCommunityTestData().CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            var employer = LogIn();
            Test(employer, new MemberSearchCriteria { CommunityId = null });
            Test(employer, new MemberSearchCriteria { CommunityId = community1.Id });
            Test(employer, new MemberSearchCriteria { CommunityId = community2.Id });
        }

        protected abstract ReadOnlyUrl GetBackToSearchUrl(IEmployer employer);

        private void Test(IEmployer employer, MemberSearchCriteria criteria)
        {
            // Do a search.

            if (criteria.KeywordsExpression == null)
                criteria.SetKeywords(Keywords);
            Get(GetPartialSearchUrl(criteria));

            // Go to the page.

            Get(GetBackToSearchUrl(employer));

            // Find the link back to the search.

            var url = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='current-search-action back-action']").Attributes["href"].Value;
            Get(new ReadOnlyApplicationUrl(url));

            AssertPath(GetResultsUrl());
            AssertCriteria(criteria);
        }

        private IEmployer LogIn()
        {
            var employer = CreateEmployer();
            LogIn(employer);
            return employer;
        }
    }
}
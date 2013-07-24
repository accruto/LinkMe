using System;
using System.Linq;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    [TestClass]
    public class MemberSearchCriteriaQueryStringTests
        : QueryStringTests<MemberSearchCriteria>
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestMethod]
        public void TestNull()
        {
            Test(null, "");
        }

        [TestMethod]
        public void TestDefault()
        {
            Test(new MemberSearchCriteria(), "");
        }

        [TestMethod]
        public void TestEducationKeywords()
        {
            Test(new MemberSearchCriteria{ EducationKeywords = "Test" }, "EducationKeywords=Test");
        }

        [TestMethod]
        public void TestIndustryIds()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            Test(new MemberSearchCriteria{ IndustryIds = new Guid[0] }, "");
            Test(new MemberSearchCriteria{ IndustryIds = new[] { id1 } }, "IndustryIds=" + id1);
            Test(new MemberSearchCriteria{ IndustryIds = new[] { id1, id2 } }, "IndustryIds=" + id1 + "&IndustryIds=" + id2);
            Test(new MemberSearchCriteria{ IndustryIds = new[] { id1, id2, id3 } }, "IndustryIds=" + id1 + "&IndustryIds=" + id2 + "&IndustryIds=" + id3);

            // All industries should convert to none being included in the query string.

            Test(new MemberSearchCriteria{ IndustryIds = (from i in _industriesQuery.GetIndustries() select i.Id).ToArray() }, "");
        }

        [TestMethod]
        public void TestJobTitle()
        {
            Test(new MemberSearchCriteria{ JobTitle = "Test" }, "JobTitle=Test");
        }

        [TestMethod]
        public void TestDesiredJobTitle()
        {
            Test(new MemberSearchCriteria{ DesiredJobTitle = "Test" }, "DesiredJobTitle=Test");
        }

        [TestMethod]
        public void TestJobTitlesToSearch()
        {
            Assert.AreEqual(JobsToSearch.RecentJobs, MemberSearchCriteria.DefaultJobTitlesToSearch);
            Test(new MemberSearchCriteria{ JobTitlesToSearch = MemberSearchCriteria.DefaultJobTitlesToSearch }, "");
            Test(new MemberSearchCriteria{ JobTitlesToSearch = JobsToSearch.AllJobs }, "JobTitlesToSearch=AllJobs");
        }

        [TestMethod]
        public void TestCompanyKeywords()
        {
            Test(new MemberSearchCriteria{ CompanyKeywords = "Test" }, "CompanyKeywords=Test");
        }

        [TestMethod]
        public void TestCompaniesToSearch()
        {
            Assert.AreEqual(JobsToSearch.AllJobs, MemberSearchCriteria.DefaultCompaniesToSearch);
            Test(new MemberSearchCriteria{ CompaniesToSearch = MemberSearchCriteria.DefaultCompaniesToSearch }, "");
            Test(new MemberSearchCriteria{ CompaniesToSearch = JobsToSearch.RecentJobs }, "CompaniesToSearch=RecentJobs");
        }

        [TestMethod]
        public void TestLocation()
        {
            var country = _locationQuery.GetCountry("Australia");
            Test(new MemberSearchCriteria{ Location = _locationQuery.ResolveLocation(country, null) }, "CountryId=1");
            Test(new MemberSearchCriteria{ Location = _locationQuery.ResolveLocation(country, "Melbourne") }, "CountryId=1&Location=Melbourne");
            Test(new MemberSearchCriteria{ Location = _locationQuery.ResolveLocation(country, "VIC") }, "CountryId=1&Location=VIC");
            Test(new MemberSearchCriteria{ Location = _locationQuery.ResolveLocation(country, "Melbourne VIC 3000") }, "CountryId=1&Location=Melbourne+VIC+3000");
        }

        [TestMethod]
        public void TestDistance()
        {
            Test(new MemberSearchCriteria{ Distance = null }, "");
            Test(new MemberSearchCriteria{ Distance = MemberSearchCriteria.DefaultDistance }, "");
            Test(new MemberSearchCriteria{ Distance = 2 * MemberSearchCriteria.DefaultDistance }, "Distance=100");
        }

        [TestMethod]
        public void TestIncludeRelocating()
        {
            Test(new MemberSearchCriteria{ IncludeRelocating = MemberSearchCriteria.DefaultIncludeRelocating }, "");
            Test(new MemberSearchCriteria{ IncludeRelocating = !MemberSearchCriteria.DefaultIncludeRelocating }, "IncludeRelocating=true");
        }

        [TestMethod]
        public void TestIncludeInternational()
        {
            Test(new MemberSearchCriteria{ IncludeInternational = MemberSearchCriteria.DefaultIncludeInternational }, "");
            Test(new MemberSearchCriteria{ IncludeInternational = !MemberSearchCriteria.DefaultIncludeInternational }, "IncludeInternational=true");
        }

        [TestMethod]
        public void TestAnyKeywords()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, "Test1", null);
            Test(criteria, "AnyKeywords=Test1");

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2", null);
            Test(criteria, "AnyKeywords=Test1&AnyKeywords=Test2");

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2 Test3", null);
            Test(criteria, "AnyKeywords=Test1&AnyKeywords=Test2&AnyKeywords=Test3");

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2 Test3 Test4", null);
            Test(criteria, "AnyKeywords=Test1&AnyKeywords=Test2&AnyKeywords=Test3&AnyKeywords=Test4");
        }

        [TestMethod]
        public void TestAllKeywords()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test", null, null, null);
            Test(criteria, "Keywords=Test");
        }

        [TestMethod]
        public void TestExactPhrase()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, "Test", null, null);
            Test(criteria, "ExactPhrase=Test");
        }

        [TestMethod]
        public void TestWithoutKeywords()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, null, "Test");
            Test(criteria, "WithoutKeywords=Test");
        }

        [TestMethod]
        public void TestAllKeywordsCombination()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test1", "Test2", "Test3", "Test4");
            Test(criteria, "AnyKeywords=Test3&AllKeywords=Test1&ExactPhrase=Test2&WithoutKeywords=Test4");
        }

        [TestMethod]
        public void TestKeywords()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test");
            Test(criteria, "Keywords=Test");
        }

        [TestMethod]
        public void TestSalary()
        {
            Test(new MemberSearchCriteria{ Salary = new Salary { Currency = Currency.AUD, Rate = SalaryRate.Year } }, "");
            Test(new MemberSearchCriteria{ Salary = new Salary { LowerBound = 10000, Currency = Currency.AUD, Rate = SalaryRate.Year } }, "SalaryLowerBound=10000");
            Test(new MemberSearchCriteria{ Salary = new Salary { UpperBound = 50000, Currency = Currency.AUD, Rate = SalaryRate.Year } }, "SalaryUpperBound=50000");
            Test(new MemberSearchCriteria{ Salary = new Salary { LowerBound = 10000, UpperBound = 50000, Currency = Currency.AUD, Rate = SalaryRate.Year } }, "SalaryLowerBound=10000&SalaryUpperBound=50000");
        }

        [TestMethod]
        public void TestRecency()
        {
            Test(new MemberSearchCriteria{ Recency = new TimeSpan(10, 0, 0, 0) }, "Recency=10");
        }

        [TestMethod]
        public void TestIncludeSynonyms()
        {
            Test(new MemberSearchCriteria{ IncludeSynonyms = MemberSearchCriteria.DefaultIncludeSynonyms }, "");
            Test(new MemberSearchCriteria{ IncludeSynonyms = !MemberSearchCriteria.DefaultIncludeSynonyms }, "IncludeSynonyms=false");
        }

        [TestMethod]
        public void TestSortOrder()
        {
            Assert.AreEqual(MemberSortOrder.Relevance, MemberSearchCriteria.DefaultSortOrder);
            Test(new MemberSearchCriteria{ SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSearchCriteria.DefaultSortOrder } }, "");
            Test(new MemberSearchCriteria{ SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.Availability } }, "SortOrder=Availability&SortOrderDirection=SortOrderIsDescending");
            Test(new MemberSearchCriteria{ SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSearchCriteria.DefaultSortOrder, ReverseSortOrder = false } }, "");
            Test(new MemberSearchCriteria{ SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSearchCriteria.DefaultSortOrder, ReverseSortOrder = true } }, "SortOrder=Relevance&SortOrderDirection=SortOrderIsAscending");
        }

        [TestMethod]
        public void TestInFolder()
        {
            Test(new MemberSearchCriteria{ InFolder = true }, "InFolder=true");
            Test(new MemberSearchCriteria{ InFolder = false }, "InFolder=false");
        }

        [TestMethod]
        public void TestIsFlagged()
        {
            Test(new MemberSearchCriteria{ IsFlagged = true }, "IsFlagged=true");
            Test(new MemberSearchCriteria{ IsFlagged = false }, "IsFlagged=false");
        }

        [TestMethod]
        public void TestHasNotes()
        {
            Test(new MemberSearchCriteria{ HasNotes = true }, "HasNotes=true");
            Test(new MemberSearchCriteria{ HasNotes = false }, "HasNotes=false");
        }

        [TestMethod]
        public void TestHasViewed()
        {
            Test(new MemberSearchCriteria{ HasViewed = true }, "HasViewed=true");
            Test(new MemberSearchCriteria{ HasViewed = false }, "HasViewed=false");
        }

        [TestMethod]
        public void TestIsUnlocked()
        {
            Test(new MemberSearchCriteria{ IsUnlocked = true }, "IsUnlocked=true");
            Test(new MemberSearchCriteria{ IsUnlocked = false }, "IsUnlocked=false");
        }

        [TestMethod]
        public void TestCommunityId()
        {
            var id = Guid.NewGuid();
            Test(new MemberSearchCriteria{ CommunityId = id }, "CommunityId=" + id);
        }

        [TestMethod]
        public void TestVarious()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Accountant");
            Test(criteria, "Keywords=Accountant");

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Mining");
            criteria.JobTitle = "Engineer";
            Test(criteria, "Keywords=Mining&JobTitle=Engineer");

            criteria = new MemberSearchCriteria{ JobTitle = "Accountant AND CPA" };
            Test(criteria, "JobTitle=Accountant+AND+CPA");

            criteria = new MemberSearchCriteria{ JobTypes = JobTypes.Temp | JobTypes.Contract };
            Test(criteria, "Contract=true&Temp=true");

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Accountant");
            criteria.Salary = new Salary { LowerBound = 50000, UpperBound = 150000 };
            Test(criteria, "Keywords=Accountant&SalaryLowerBound=50000&SalaryUpperBound=150000");

            criteria = new MemberSearchCriteria{JobTitle = "Mining Engineer"};
            Test(criteria, "JobTitle=Mining+Engineer");

            criteria = new MemberSearchCriteria{ CandidateStatusFlags = CandidateStatusFlags.AvailableNow | CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.OpenToOffers };
            Test(criteria, "AvailableNow=true&ActivelyLooking=true&OpenToOffers=true");
        }

        private void Test(MemberSearchCriteria criteria, string expectedQueryString)
        {
            Test(criteria, expectedQueryString, () => new MemberSearchCriteriaConverter(_locationQuery, _industriesQuery), () => new MemberSearchCriteriaConverter(_locationQuery, _industriesQuery));
        }

        private static bool IsEmpty(ICanBeEmpty criteria)
        {
            return criteria == null || criteria.IsEmpty;
        }

        protected override void AssertAreEqual(MemberSearchCriteria expectedCriteria, MemberSearchCriteria criteria)
        {
            // All industries is actually equivalent to no industries set in criteria.

            if (expectedCriteria != null && expectedCriteria.IndustryIds != null && expectedCriteria.IndustryIds.CollectionEqual(_industriesQuery.GetIndustries().Select(i => i.Id)))
                expectedCriteria.IndustryIds = null;

            if (IsEmpty(expectedCriteria))
            {
                Assert.IsTrue(criteria.IsEmpty);
            }
            else
            {
                Assert.IsNotNull(expectedCriteria);
                Assert.IsFalse(criteria.IsEmpty);
                Assert.AreEqual(expectedCriteria.EducationKeywords, criteria.EducationKeywords);
                Assert.IsTrue(expectedCriteria.IndustryIds.NullableCollectionEqual(criteria.IndustryIds));
                Assert.AreEqual(expectedCriteria.JobTitle, criteria.JobTitle);
                Assert.AreEqual(expectedCriteria.DesiredJobTitle, criteria.DesiredJobTitle);
                Assert.AreEqual(expectedCriteria.JobTitlesToSearch, criteria.JobTitlesToSearch);
                Assert.AreEqual(expectedCriteria.CompanyKeywords, criteria.CompanyKeywords);
                Assert.AreEqual(expectedCriteria.CompaniesToSearch, criteria.CompaniesToSearch);
                Assert.AreEqual(expectedCriteria.Location, criteria.Location);
                Assert.AreEqual(expectedCriteria.Distance, criteria.Distance);
                Assert.AreEqual(expectedCriteria.IncludeRelocating, criteria.IncludeRelocating);
                Assert.AreEqual(expectedCriteria.IncludeInternational, criteria.IncludeInternational);
                Assert.AreEqual(expectedCriteria.InFolder, criteria.InFolder);
                Assert.AreEqual(expectedCriteria.IsFlagged, criteria.IsFlagged);
                Assert.AreEqual(expectedCriteria.HasNotes, criteria.HasNotes);
                Assert.AreEqual(expectedCriteria.HasViewed, criteria.HasViewed);
                Assert.AreEqual(expectedCriteria.IsUnlocked, criteria.IsUnlocked);
                Assert.AreEqual(expectedCriteria.CommunityId, criteria.CommunityId);
                Assert.AreEqual(expectedCriteria.AnyKeywords, criteria.AnyKeywords);
                Assert.AreEqual(expectedCriteria.AllKeywords, criteria.AllKeywords);
                Assert.AreEqual(expectedCriteria.ExactPhrase, criteria.ExactPhrase);
                Assert.AreEqual(expectedCriteria.WithoutKeywords, criteria.WithoutKeywords);
                Assert.AreEqual(expectedCriteria.GetKeywords(), criteria.GetKeywords());
                Assert.AreEqual(expectedCriteria.Salary, criteria.Salary);
                Assert.AreEqual(expectedCriteria.Recency, criteria.Recency);
                Assert.AreEqual(expectedCriteria.IncludeSynonyms, criteria.IncludeSynonyms);
                Assert.AreEqual(expectedCriteria.SortCriteria.SortOrder, criteria.SortCriteria.SortOrder);
                Assert.AreEqual(expectedCriteria.SortCriteria.ReverseSortOrder, criteria.SortCriteria.ReverseSortOrder);
            }
        }
    }
}
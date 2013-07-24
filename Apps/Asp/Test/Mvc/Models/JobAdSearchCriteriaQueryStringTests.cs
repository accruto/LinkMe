using System;
using System.Linq;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    [TestClass]
    public class JobAdSearchCriteriaQueryStringTests
        : QueryStringTests<JobAdSearchCriteria>
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
            Test(new JobAdSearchCriteria(), "");
        }

        [TestMethod]
        public void TestIndustryIds()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            Test(new JobAdSearchCriteria { IndustryIds = new Guid[0] }, "");
            Test(new JobAdSearchCriteria { IndustryIds = new[] { id1 } }, "IndustryIds=" + id1);
            Test(new JobAdSearchCriteria { IndustryIds = new[] { id1, id2 } }, "IndustryIds=" + id1 + "&IndustryIds=" + id2);
            Test(new JobAdSearchCriteria { IndustryIds = new[] { id1, id2, id3 } }, "IndustryIds=" + id1 + "&IndustryIds=" + id2 + "&IndustryIds=" + id3);

            // All industries should convert to none being included in the query string.

            Test(new JobAdSearchCriteria { IndustryIds = (from i in _industriesQuery.GetIndustries() select i.Id).ToArray() }, "");
        }

        [TestMethod]
        public void TestAdTitle()
        {
            Test(new JobAdSearchCriteria { AdTitle = "Test" }, "AdTitle=Test");
        }

        [TestMethod]
        public void TestLocation()
        {
            var country = _locationQuery.GetCountry("Australia");
            Test(new JobAdSearchCriteria { Location = _locationQuery.ResolveLocation(country, null) }, "CountryId=1");
            Test(new JobAdSearchCriteria { Location = _locationQuery.ResolveLocation(country, "Melbourne") }, "CountryId=1&Location=Melbourne");
            Test(new JobAdSearchCriteria { Location = _locationQuery.ResolveLocation(country, "VIC") }, "CountryId=1&Location=VIC");
            Test(new JobAdSearchCriteria { Location = _locationQuery.ResolveLocation(country, "Melbourne VIC 3000") }, "CountryId=1&Location=Melbourne+VIC+3000");
        }

        [TestMethod]
        public void TestDistance()
        {
            Test(new JobAdSearchCriteria { Distance = null }, "");
            Test(new JobAdSearchCriteria { Distance = JobAdSearchCriteria.DefaultDistance }, "");
            Test(new JobAdSearchCriteria { Distance = 2 * JobAdSearchCriteria.DefaultDistance }, "Distance=100");
        }

        [TestMethod]
        public void TestAnyKeywords()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, "Test1", null);
            Test(criteria, "AnyKeywords=Test1");

            criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2", null);
            Test(criteria, "AnyKeywords=Test1&AnyKeywords=Test2");

            criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2 Test3", null);
            Test(criteria, "AnyKeywords=Test1&AnyKeywords=Test2&AnyKeywords=Test3");

            criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2 Test3 Test4", null);
            Test(criteria, "AnyKeywords=Test1&AnyKeywords=Test2&AnyKeywords=Test3&AnyKeywords=Test4");
        }

        [TestMethod]
        public void TestAllKeywords()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("Test", null, null, null);
            Test(criteria, "Keywords=Test");
        }

        [TestMethod]
        public void TestExactPhrase()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, "Test", null, null);
            Test(criteria, "ExactPhrase=Test");
        }

        [TestMethod]
        public void TestWithoutKeywords()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, null, "Test");
            Test(criteria, "WithoutKeywords=Test");
        }

        [TestMethod]
        public void TestAllKeywordsCombination()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("Test1", "Test2", "Test3", "Test4");
            Test(criteria, "AnyKeywords=Test3&AllKeywords=Test1&ExactPhrase=Test2&WithoutKeywords=Test4");
        }

        [TestMethod]
        public void TestKeywords()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("Test");
            Test(criteria, "Keywords=Test");
        }

        [TestMethod]
        public void TestSalary()
        {
            Test(new JobAdSearchCriteria { Salary = new Salary { Currency = Currency.AUD, Rate = SalaryRate.Year } }, "");
            Test(new JobAdSearchCriteria { Salary = new Salary { LowerBound = 10000, Currency = Currency.AUD, Rate = SalaryRate.Year } }, "SalaryLowerBound=10000");
            Test(new JobAdSearchCriteria { Salary = new Salary { UpperBound = 50000, Currency = Currency.AUD, Rate = SalaryRate.Year } }, "SalaryUpperBound=50000");
            Test(new JobAdSearchCriteria { Salary = new Salary { LowerBound = 10000, UpperBound = 50000, Currency = Currency.AUD, Rate = SalaryRate.Year } }, "SalaryLowerBound=10000&SalaryUpperBound=50000");
        }

        [TestMethod]
        public void TestRecency()
        {
            Test(new JobAdSearchCriteria { Recency = new TimeSpan(10, 0, 0, 0) }, "Recency=10");
        }

        [TestMethod]
        public void TestIncludeSynonyms()
        {
            Test(new JobAdSearchCriteria { IncludeSynonyms = JobAdSearchCriteria.DefaultIncludeSynonyms }, "");
            Test(new JobAdSearchCriteria { IncludeSynonyms = !JobAdSearchCriteria.DefaultIncludeSynonyms }, "IncludeSynonyms=false");
        }

        [TestMethod]
        public void TestSortOrder()
        {
            Assert.AreEqual(JobAdSortOrder.Relevance, JobAdSearchCriteria.DefaultSortOrder);
            Test(new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSearchCriteria.DefaultSortOrder } }, "");
            Test(new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.Flagged } }, "SortOrder=Flagged&SortOrderDirection=SortOrderIsDescending");
            Test(new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSearchCriteria.DefaultSortOrder, ReverseSortOrder = false } }, "");
            Test(new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSearchCriteria.DefaultSortOrder, ReverseSortOrder = true } }, "SortOrder=Relevance&SortOrderDirection=SortOrderIsAscending");
        }

        [TestMethod]
        public void TestIsFlagged()
        {
            Test(new JobAdSearchCriteria { IsFlagged = true }, "IsFlagged=true");
            Test(new JobAdSearchCriteria { IsFlagged = false }, "IsFlagged=false");
        }

        [TestMethod]
        public void TestHasNotes()
        {
            Test(new JobAdSearchCriteria { HasNotes = true }, "HasNotes=true");
            Test(new JobAdSearchCriteria { HasNotes = false }, "HasNotes=false");
        }

        [TestMethod]
        public void TestHasViewed()
        {
            Test(new JobAdSearchCriteria { HasViewed = true }, "HasViewed=true");
            Test(new JobAdSearchCriteria { HasViewed = false }, "HasViewed=false");
        }

        [TestMethod]
        public void TestCommunityId()
        {
            var id = Guid.NewGuid();
            Test(new JobAdSearchCriteria { CommunityId = id }, "CommunityId=" + id);
        }

        [TestMethod]
        public void TestVarious()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("Accountant");
            Test(criteria, "Keywords=Accountant");

            criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("Mining");
            criteria.AdTitle = "Engineer";
            Test(criteria, "Keywords=Mining&AdTitle=Engineer");

            criteria = new JobAdSearchCriteria { AdTitle = "Accountant AND CPA" };
            Test(criteria, "AdTitle=Accountant+AND+CPA");

            criteria = new JobAdSearchCriteria { JobTypes = JobTypes.Temp | JobTypes.Contract };
            Test(criteria, "Contract=true&Temp=true");

            criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("Accountant");
            criteria.Salary = new Salary { LowerBound = 50000, UpperBound = 150000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            Test(criteria, "Keywords=Accountant&SalaryLowerBound=50000&SalaryUpperBound=150000");

            criteria = new JobAdSearchCriteria { AdTitle = "Mining Engineer" };
            Test(criteria, "AdTitle=Mining+Engineer");
        }

        private void Test(JobAdSearchCriteria criteria, string expectedQueryString)
        {
            Test(criteria, expectedQueryString, () => new JobAdSearchCriteriaConverter(_locationQuery, _industriesQuery), () => new JobAdSearchCriteriaConverter(_locationQuery, _industriesQuery));
        }

        private static bool IsEmpty(ICanBeEmpty criteria)
        {
            return criteria == null || criteria.IsEmpty;
        }

        protected override void AssertAreEqual(JobAdSearchCriteria expectedCriteria, JobAdSearchCriteria criteria)
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
                Assert.IsTrue(expectedCriteria.IndustryIds.NullableCollectionEqual(criteria.IndustryIds));
                Assert.AreEqual(expectedCriteria.AdTitle, criteria.AdTitle);
                Assert.AreEqual(expectedCriteria.Location, criteria.Location);
                Assert.AreEqual(expectedCriteria.Distance, criteria.Distance);
                Assert.AreEqual(expectedCriteria.IsFlagged, criteria.IsFlagged);
                Assert.AreEqual(expectedCriteria.HasNotes, criteria.HasNotes);
                Assert.AreEqual(expectedCriteria.HasViewed, criteria.HasViewed);
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
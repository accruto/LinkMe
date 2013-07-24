using System;
using System.Globalization;
using System.Linq;
using System.Text;
using LinkMe.Apps.Asp.Mvc.Models.JavaScriptConverters;
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
    public class MemberSearchCriteriaJavaScriptSerializerTests
        : JavaScriptSerializerTests<MemberSearchCriteria>
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestMethod]
        public void TestNull()
        {
            Test(null);
        }

        [TestMethod]
        public void TestDefault()
        {
            Test(new MemberSearchCriteria());
        }

        [TestMethod]
        public void TestEducationKeywords()
        {
            Test(new MemberSearchCriteria { EducationKeywords = "Test" });
        }

        [TestMethod]
        public void TestCandidateStatus()
        {
            Test(new MemberSearchCriteria { CandidateStatusFlags = CandidateStatusFlags.AvailableNow | CandidateStatusFlags.OpenToOffers });
        }

        [TestMethod]
        public void TestJobTypes()
        {
            Test(new MemberSearchCriteria { JobTypes = MemberSearchCriteria.DefaultJobTypes });
            Test(new MemberSearchCriteria { JobTypes = JobTypes.FullTime | JobTypes.PartTime });
        }

        [TestMethod]
        public void TestEthnicStatus()
        {
            Test(new MemberSearchCriteria { EthnicStatus = EthnicStatus.Aboriginal });
        }

        [TestMethod]
        public void TestIndustryIds()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            Test(new MemberSearchCriteria { IndustryIds = new Guid[0] });
            Test(new MemberSearchCriteria { IndustryIds = new[] { id1 } });
            Test(new MemberSearchCriteria { IndustryIds = new[] { id1, id2 } });
            Test(new MemberSearchCriteria { IndustryIds = new[] { id1, id2, id3 } });
        }

        [TestMethod]
        public void TestJobTitle()
        {
            Test(new MemberSearchCriteria { JobTitle = "Test" });
        }

        [TestMethod]
        public void TestDesiredJobTitle()
        {
            Test(new MemberSearchCriteria { DesiredJobTitle = "Test" });
        }

        [TestMethod]
        public void TestJobTitlesToSearch()
        {
            Assert.AreEqual(JobsToSearch.RecentJobs, MemberSearchCriteria.DefaultJobTitlesToSearch);
            Test(new MemberSearchCriteria { JobTitlesToSearch = MemberSearchCriteria.DefaultJobTitlesToSearch });
            Test(new MemberSearchCriteria { JobTitlesToSearch = JobsToSearch.AllJobs });
        }

        [TestMethod]
        public void TestCompanyKeywords()
        {
            Test(new MemberSearchCriteria { CompanyKeywords = "Test" });
        }

        [TestMethod]
        public void TestCompaniesToSearch()
        {
            Assert.AreEqual(JobsToSearch.AllJobs, MemberSearchCriteria.DefaultCompaniesToSearch);
            Test(new MemberSearchCriteria { CompaniesToSearch = MemberSearchCriteria.DefaultCompaniesToSearch });
            Test(new MemberSearchCriteria { CompaniesToSearch = JobsToSearch.RecentJobs });
        }

        [TestMethod]
        public void TestLocation()
        {
            var country = _locationQuery.GetCountry("Australia");
            Test(new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, null) });
            Test(new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, "Melbourne") });
            Test(new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, "VIC") });
            Test(new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, "Melbourne VIC 3000") });
        }

        [TestMethod]
        public void TestDistance()
        {
            Test(new MemberSearchCriteria { Distance = null });
            Test(new MemberSearchCriteria { Distance = MemberSearchCriteria.DefaultDistance });
            Test(new MemberSearchCriteria { Distance = 2 * MemberSearchCriteria.DefaultDistance });
        }

        [TestMethod]
        public void TestIncludeRelocating()
        {
            Test(new MemberSearchCriteria { IncludeRelocating = MemberSearchCriteria.DefaultIncludeRelocating });
            Test(new MemberSearchCriteria { IncludeRelocating = !MemberSearchCriteria.DefaultIncludeRelocating });
        }

        [TestMethod]
        public void TestIncludeInternational()
        {
            Test(new MemberSearchCriteria { IncludeInternational = MemberSearchCriteria.DefaultIncludeInternational });
            Test(new MemberSearchCriteria { IncludeInternational = !MemberSearchCriteria.DefaultIncludeInternational });
        }

        [TestMethod]
        public void TestAnyKeywords()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, "Test1", null);
            Test(criteria);

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2", null);
            Test(criteria);

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2 Test3", null);
            Test(criteria);

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2 Test3 Test4", null);
            Test(criteria);
        }

        [TestMethod]
        public void TestAllKeywords()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test", null, null, null);
            Test(criteria);
        }

        [TestMethod]
        public void TestExactPhrase()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, "Test", null, null);
            Test(criteria);
        }

        [TestMethod]
        public void TestWithoutKeywords()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, null, "Test");
            Test(criteria);
        }

        [TestMethod]
        public void TestAllKeywordsCombination()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test1", "Test2", "Test3", "Test4");
            Test(criteria);
        }

        [TestMethod]
        public void TestKeywords()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("Test");
            Test(criteria);
        }

        [TestMethod]
        public void TestSalary()
        {
            Test(new MemberSearchCriteria { Salary = new Salary { Currency = Currency.AUD, Rate = SalaryRate.Year } });
            Test(new MemberSearchCriteria { Salary = new Salary { LowerBound = 10000, Currency = Currency.AUD, Rate = SalaryRate.Year } });
            Test(new MemberSearchCriteria { Salary = new Salary { UpperBound = 50000, Currency = Currency.AUD, Rate = SalaryRate.Year } });
            Test(new MemberSearchCriteria { Salary = new Salary { LowerBound = 10000, UpperBound = 50000, Currency = Currency.AUD, Rate = SalaryRate.Year } });
        }

        [TestMethod]
        public void TestRecency()
        {
            Test(new MemberSearchCriteria { Recency = new TimeSpan(10, 0, 0, 0) });
        }

        [TestMethod]
        public void TestIncludeSynonyms()
        {
            Test(new MemberSearchCriteria { IncludeSynonyms = MemberSearchCriteria.DefaultIncludeSynonyms });
            Test(new MemberSearchCriteria { IncludeSynonyms = !MemberSearchCriteria.DefaultIncludeSynonyms });
        }

        [TestMethod]
        public void TestSortOrder()
        {
            Assert.AreEqual(MemberSortOrder.Relevance, MemberSearchCriteria.DefaultSortOrder);
            Test(new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSearchCriteria.DefaultSortOrder } });
            Test(new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.Availability } });
            Test(new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { ReverseSortOrder = false } });
            Test(new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { ReverseSortOrder = true } });
        }

        [TestMethod]
        public void TestInFolder()
        {
            Test(new MemberSearchCriteria { InFolder = true });
            Test(new MemberSearchCriteria { InFolder = false });
        }

        [TestMethod]
        public void TestIsFlagged()
        {
            Test(new MemberSearchCriteria { IsFlagged = true });
            Test(new MemberSearchCriteria { IsFlagged = false });
        }

        [TestMethod]
        public void TestHasNotes()
        {
            Test(new MemberSearchCriteria { HasNotes = true });
            Test(new MemberSearchCriteria { HasNotes = false });
        }

        [TestMethod]
        public void TestHasViewed()
        {
            Test(new MemberSearchCriteria { HasViewed = true });
            Test(new MemberSearchCriteria { HasViewed = false });
        }

        [TestMethod]
        public void TestIsUnlocked()
        {
            Test(new MemberSearchCriteria { IsUnlocked = true });
            Test(new MemberSearchCriteria { IsUnlocked = false });
        }

        [TestMethod]
        public void TestCommunityId()
        {
            var id = Guid.NewGuid();
            Test(new MemberSearchCriteria { CommunityId = id });
        }

        private void Test(MemberSearchCriteria criteria)
        {
            Test(criteria, () => new MemberSearchCriteriaJavaScriptConverter(_locationQuery, _industriesQuery), () => new MemberSearchCriteriaConverter(_locationQuery, _industriesQuery));
        }

        private static bool IsEmpty(ICanBeEmpty criteria)
        {
            return criteria == null || criteria.IsEmpty;
        }

        protected override void AssertAreEqual(MemberSearchCriteria expectedCriteria, MemberSearchCriteria criteria)
        {
            if (IsEmpty(expectedCriteria))
            {
                Assert.IsTrue(criteria == null || criteria.IsEmpty);
            }
            else
            {
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

        protected override string GetExpectedSerialization(MemberSearchCriteria criteria)
        {
            if (criteria == null)
                return "null";

            var sb = new StringBuilder();
            sb.Append("{");

            if (!string.IsNullOrEmpty(criteria.ExactPhrase) || !string.IsNullOrEmpty(criteria.AnyKeywords) || !string.IsNullOrEmpty(criteria.WithoutKeywords))
            {
                sb.Append("\"AnyKeywords\":").Append(criteria.AnyKeywords == null ? "null" : "[\"" + string.Join("\",\"", criteria.AnyKeywords.Split(new[] { ' ' })) + "\"]")
                    .Append(",\"AllKeywords\":").Append(criteria.AllKeywords == null ? "null" : "\"" + criteria.AllKeywords + "\"")
                    .Append(",\"ExactPhrase\":").Append(criteria.ExactPhrase == null ? "null" : "\"" + criteria.ExactPhrase + "\"")
                    .Append(",\"WithoutKeywords\":").Append(criteria.WithoutKeywords == null ? "null" : "\"" + criteria.WithoutKeywords + "\"");
            }
            else
            {
                var keywords = criteria.GetKeywords();
                sb.Append("\"Keywords\":").Append(keywords == null ? "null" : "\"" + keywords + "\"");
            }

            if (!string.IsNullOrEmpty(criteria.JobTitle))
                sb.Append(",\"JobTitle\":").Append("\"" + criteria.JobTitle + "\"");

            if (!string.IsNullOrEmpty(criteria.DesiredJobTitle))
                sb.Append(",\"DesiredJobTitle\":").Append("\"" + criteria.DesiredJobTitle + "\"");

            if (criteria.JobTitlesToSearch != MemberSearchCriteria.DefaultJobTitlesToSearch)
                sb.Append(",\"JobTitlesToSearch\":").Append("\"" + criteria.JobTitlesToSearch + "\"");

            if (!string.IsNullOrEmpty(criteria.CompanyKeywords))
                sb.Append(",\"CompanyKeywords\":").Append("\"" + criteria.CompanyKeywords + "\"");

            if (criteria.CompaniesToSearch != MemberSearchCriteria.DefaultCompaniesToSearch)
                sb.Append(",\"CompaniesToSearch\":").Append("\"" + criteria.CompaniesToSearch + "\"");

            if (criteria.Location != null)
            {
                sb.Append(",\"CountryId\":").Append(criteria.Location.Country.Id);
                var location = criteria.Location.ToString();
                if (!string.IsNullOrEmpty(location))
                    sb.Append(",\"Location\":").Append("\"" + location + "\"");
            }

            if (criteria.Distance != null && criteria.Distance != MemberSearchCriteria.DefaultDistance)
                sb.Append(",\"Distance\":").Append(criteria.Distance.Value);
            if (criteria.IncludeRelocating != MemberSearchCriteria.DefaultIncludeRelocating)
                sb.Append(",\"IncludeRelocating\":").Append(criteria.IncludeRelocating ? "true" : "false");
            if (criteria.IncludeInternational != MemberSearchCriteria.DefaultIncludeInternational)
                sb.Append(",\"IncludeInternational\":").Append(criteria.IncludeInternational ? "true" : "false");

            if (criteria.IncludeSynonyms != MemberSearchCriteria.DefaultIncludeSynonyms)
                sb.Append(",\"IncludeSynonyms\":").Append(criteria.IncludeSynonyms ? "true" : "false");

            if (!string.IsNullOrEmpty(criteria.EducationKeywords))
                sb.Append(",\"EducationKeywords\":").Append("\"" + criteria.EducationKeywords + "\"");

            if (criteria.CandidateStatusFlags != null)
            {
                if (criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.ActivelyLooking))
                    sb.Append(",\"ActivelyLooking\":true");
                if (criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.AvailableNow))
                    sb.Append(",\"AvailableNow\":true");
                if (criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.NotLooking))
                    sb.Append(",\"NotLooking\":true");
                if (criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.OpenToOffers))
                    sb.Append(",\"OpenToOffers\":true");
                if (criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.Unspecified))
                    sb.Append(",\"Unspecified\":true");
            }

            if (criteria.EthnicStatus != null)
            {
                if (criteria.EthnicStatus.Value.IsFlagSet(EthnicStatus.Aboriginal))
                    sb.Append(",\"Aboriginal\":true");
                if (criteria.EthnicStatus.Value.IsFlagSet(EthnicStatus.TorresIslander))
                    sb.Append(",\"TorresIslander\":true");
            }

            if (criteria.JobTypes != MemberSearchCriteria.DefaultJobTypes)
            {
                if (criteria.JobTypes.IsFlagSet(JobTypes.Contract))
                    sb.Append(",\"Contract\":true");
                if (criteria.JobTypes.IsFlagSet(JobTypes.FullTime))
                    sb.Append(",\"FullTime\":true");
                if (criteria.JobTypes.IsFlagSet(JobTypes.JobShare))
                    sb.Append(",\"JobShare\":true");
                if (criteria.JobTypes.IsFlagSet(JobTypes.PartTime))
                    sb.Append(",\"PartTime\":true");
                if (criteria.JobTypes.IsFlagSet(JobTypes.Temp))
                    sb.Append(",\"Temp\":true");
            }

            if (criteria.IndustryIds != null && criteria.IndustryIds.Count != 0)
            sb.Append(",\"IndustryIds\":").Append("[\"" + string.Join("\",\"", criteria.IndustryIds.Select(i => i.ToString()).ToArray()) + "\"]");

            if (criteria.Salary != null)
            {
                sb.Append(",\"SalaryLowerBound\":").Append(criteria.Salary.LowerBound == null ? "null" : criteria.Salary.LowerBound.Value.ToString(CultureInfo.InvariantCulture))
                    .Append(",\"SalaryUpperBound\":").Append(criteria.Salary.UpperBound == null ? "null" : criteria.Salary.UpperBound.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (criteria.Recency.HasValue)
                sb.Append(",\"Recency\":").Append(criteria.Recency.Value.Days.ToString(CultureInfo.InvariantCulture));

            if (criteria.InFolder.HasValue)
                sb.Append(",\"InFolder\":").Append(criteria.InFolder.Value ? "true" : "false");

            if (criteria.IsFlagged.HasValue)
                sb.Append(",\"IsFlagged\":").Append(criteria.IsFlagged.Value ? "true" : "false");

            if (criteria.HasNotes.HasValue)
                sb.Append(",\"HasNotes\":").Append(criteria.HasNotes.Value ? "true" : "false");

            if (criteria.HasViewed.HasValue)
                sb.Append(",\"HasViewed\":").Append(criteria.HasViewed.Value ? "true" : "false");

            if (criteria.IsUnlocked.HasValue)
                sb.Append(",\"IsUnlocked\":").Append(criteria.IsUnlocked.Value ? "true" : "false");

            if (criteria.CommunityId.HasValue)
                sb.Append(",\"CommunityId\":").Append("\"" + criteria.CommunityId.Value + "\"");

            if (criteria.SortCriteria.SortOrder != MemberSearchCriteria.DefaultSortOrder || criteria.SortCriteria.ReverseSortOrder)
            {
                sb.Append(",\"SortOrder\":").Append("\"" + criteria.SortCriteria.SortOrder + "\"");
                sb.Append(",\"SortOrderDirection\":").Append("\"" + (criteria.SortCriteria.ReverseSortOrder ? "SortOrderIsAscending" : "SortOrderIsDescending") + "\"");
            }

            sb.Append("}");
            return sb.ToString();
        }
    }
}
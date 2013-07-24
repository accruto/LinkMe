using LinkMe.Apps.Asp.Mvc.Models.Converters;
using LinkMe.Domain.Location.Queries;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    [TestClass]
    public class JobAdSearchCriteriaAdSenseQueryTests
        : AdSenseQueryTests<JobAdSearchCriteria>
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

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
        public void TestLocation()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, "Test", null);

            var country = _locationQuery.GetCountry("Australia");
            criteria.Location = _locationQuery.ResolveLocation(country, null);
            Test(criteria, "test jobs in australia");
            criteria.Location = _locationQuery.ResolveLocation(country, "Melbourne");
            Test(criteria, "test jobs in melbourne australia");
            criteria.Location = _locationQuery.ResolveLocation(country, "VIC");
            Test(criteria, "test jobs in vic australia");
            criteria.Location = _locationQuery.ResolveLocation(country, "Melbourne VIC 3000");
            Test(criteria, "test jobs in melbourne vic 3000 australia");
        }

        [TestMethod]
        public void TestAnyKeywords()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, "Test1", null);
            Test(criteria, "test1 jobs");

            criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2", null);
            Test(criteria, "test1 or test2 jobs");

            criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2 Test3", null);
            Test(criteria, "test1 or test2 or test3 jobs");

            criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, "Test1 Test2 Test3 Test4", null);
            Test(criteria, "test1 or test2 or test3 or test4 jobs");
        }

        [TestMethod]
        public void TestAllKeywords()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("Test", null, null, null);
            Test(criteria, "test jobs");
        }

        [TestMethod]
        public void TestExactPhrase()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, "Test", null, null);
            Test(criteria, "test jobs");

            criteria.SetKeywords(null, "Test1 Test2", null, null);
            Test(criteria, "\"test1 test2\" jobs");
        }

        [TestMethod]
        public void TestWithoutKeywords()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(null, null, null, "Test");
            Test(criteria, "-test jobs");

            criteria.SetKeywords(null, null, null, "Test1 Test2");
            Test(criteria, "-(test1 or test2) jobs");
        }

        [TestMethod]
        public void TestAllKeywordsCombination()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("Test1", "Test2", "Test3", "Test4");
            Test(criteria, "test1 test2 test3 -test4 jobs");
        }

        [TestMethod]
        public void TestKeywords()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("Test");
            Test(criteria, "test jobs");
        }

        private void Test(JobAdSearchCriteria criteria, string expectedQueryString)
        {
            Test(criteria, expectedQueryString, () => new JobAdSearchCriteriaAdSenseConverter());
        }
    }
}
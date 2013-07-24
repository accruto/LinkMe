using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Query.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Search
{
    [TestClass]
    public class SalaryTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void TestSalary()
        {
            // At the moment this test is just checking that no exception is raised in these situations.

            var jobQuery = new JobAdSearchQuery();
            AssertJobAds(Search(jobQuery));

            jobQuery.Salary = new Salary { LowerBound = 0, UpperBound = 0, Rate = SalaryRate.Year, Currency = Currency.AUD };
            AssertJobAds(Search(jobQuery));

            jobQuery.Salary = new Salary { LowerBound = 0, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD };
            AssertJobAds(Search(jobQuery));
            
            jobQuery.Salary = new Salary { LowerBound = null, UpperBound = 0, Rate = SalaryRate.Year, Currency = Currency.AUD };
            AssertJobAds(Search(jobQuery));
            
            jobQuery.Salary = new Salary { LowerBound = null, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD };
            AssertJobAds(Search(jobQuery));
            
            jobQuery.Salary = new Salary { LowerBound = 50000, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD };
            AssertJobAds(Search(jobQuery));
            
            jobQuery.Salary = new Salary { LowerBound = null, UpperBound = 50000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            AssertJobAds(Search(jobQuery));
            
            jobQuery.Salary = new Salary { LowerBound = 30000, UpperBound = 50000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            AssertJobAds(Search(jobQuery));
        }

        private static void AssertJobAds(JobAdSearchResults results, params JobAd[] jobAds)
        {
            Assert.AreEqual(jobAds.Length, results.JobAdIds.Count);
            Assert.IsTrue(results.JobAdIds.CollectionEqual(from j in jobAds select j.Id));
        }
    }
}

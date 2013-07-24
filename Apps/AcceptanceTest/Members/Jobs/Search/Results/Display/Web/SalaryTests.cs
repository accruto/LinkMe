using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class SalaryTests
        : DisplayTests
    {
        private const decimal LowerBound = 50000;
        private const decimal UpperBound = 100000;

        [TestMethod]
        public void TestSalary()
        {
            TestSalary(LowerBound, UpperBound, "$50,000 to $100,000 p.a.", "$50,000 to $100,000 p.a.");
        }

        [TestMethod]
        public void TestLowerBound()
        {
            TestSalary(LowerBound, null, "$50,000+ p.a.", "$50,000+ p.a.");
        }

        [TestMethod]
        public void TestUpperBound()
        {
            TestSalary(null, UpperBound, "$100,000 p.a.", "$100,000 p.a.");
        }

        [TestMethod]
        public void TestNone()
        {
            TestSalary(null, null, "Not specified", "Not specified");
        }

        private void TestSalary(decimal? lowerBound, decimal? upperBound, string expectedSalary, string expectedApiSalary)
        {
            var jobAd = CreateJobAd(lowerBound, upperBound);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            node = node.SelectSingleNode(".//div[@class='salary']");
            Assert.IsNotNull(node);
            Assert.AreEqual(expectedSalary, node.InnerText);

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.AreEqual(expectedApiSalary, model.JobAds[0].Salary);
        }

        private JobAd CreateJobAd(decimal? lowerBound, decimal? upperBound)
        {
            var employer = CreateEmployer(0);
            var jobAd = employer.CreateTestJobAd(Keywords);

            jobAd.Description.Salary = lowerBound == null && upperBound == null
                ? null
                : new Salary { LowerBound = lowerBound, UpperBound = upperBound, Currency = Currency.AUD, Rate = SalaryRate.Year };

            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}

using LinkMe.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.JobAds.SuggestedCandidates
{
    [TestClass]
    public class CriteriaTests
        : SuggestedCandidatesTests
    {
        private const string Country = "Australia";
        private const string Location = "Norlane VIC 3214";

        [TestMethod]
        public void TestTitle()
        {
            var employer = CreateEmployer(0);
            var jobAd = CreateJobAd(employer, 0);
            var criteria = _suggestedMembersQuery.GetCriteria(jobAd);

            Assert.IsTrue(!string.IsNullOrEmpty(criteria.JobTitle));
            Assert.IsTrue(string.IsNullOrEmpty(criteria.GetKeywords()));
            Assert.IsNull(criteria.Salary);
            Assert.IsNull(criteria.Location);

            LogIn(employer);
            Get(GetSuggestedCandidatesUrl(jobAd.Id));
            AssertCriteria(criteria);
        }

        [TestMethod]
        public void TestSalary()
        {
            var employer = CreateEmployer(0);
            var jobAd = CreateJobAd(employer, 0);
            jobAd.Description.Salary = new Salary { Currency = Currency.AUD, LowerBound = 50000, UpperBound = 75000, Rate = SalaryRate.Year };
            _employerJobAdsCommand.UpdateJobAd(employer, jobAd);

            var criteria = _suggestedMembersQuery.GetCriteria(jobAd);

            Assert.IsTrue(!string.IsNullOrEmpty(criteria.JobTitle));
            Assert.IsTrue(string.IsNullOrEmpty(criteria.GetKeywords()));
            Assert.IsNotNull(criteria.Salary);
            Assert.IsNull(criteria.Location);

            LogIn(employer);
            Get(GetSuggestedCandidatesUrl(jobAd.Id));
            AssertCriteria(criteria);
        }

        [TestMethod]
        public void TestLocation()
        {
            var employer = CreateEmployer(0);
            var jobAd = CreateJobAd(employer, 0);
            jobAd.Description.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
            _employerJobAdsCommand.UpdateJobAd(employer, jobAd);

            var criteria = _suggestedMembersQuery.GetCriteria(jobAd);

            Assert.IsTrue(!string.IsNullOrEmpty(criteria.JobTitle));
            Assert.IsTrue(string.IsNullOrEmpty(criteria.GetKeywords()));
            Assert.IsNull(criteria.Salary);
            Assert.IsNotNull(criteria.Location);

            LogIn(employer);
            Get(GetSuggestedCandidatesUrl(jobAd.Id));
            AssertCriteria(criteria);
        }
    }
}

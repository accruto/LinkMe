using LinkMe.Domain;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Mobile
{
    [TestClass]
    public class SalaryTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.Salary = null;
            TestDisplay(criteria);

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 20000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            TestDisplay(criteria);

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 20000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            TestDisplay(criteria);

            criteria.Salary = new Salary { LowerBound = 20000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            TestDisplay(criteria);
        }
    }
}
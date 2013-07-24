using LinkMe.Domain;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Mobile
{
    [TestClass]
    public class JobTypesTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            criteria.JobTypes = JobTypes.All;
            TestDisplay(criteria);

            criteria.JobTypes = JobTypes.None;
            TestDisplay(criteria);

            criteria.JobTypes = JobTypes.FullTime;
            TestDisplay(criteria);

            criteria.JobTypes = JobTypes.FullTime | JobTypes.PartTime | JobTypes.Temp;
            TestDisplay(criteria);
        }
    }
}
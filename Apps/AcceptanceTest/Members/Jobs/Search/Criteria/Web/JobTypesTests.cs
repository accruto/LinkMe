using LinkMe.Domain;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
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
            TestDisplay(false, criteria);

            criteria.JobTypes = JobTypes.None;
            TestDisplay(false, criteria);

            criteria.JobTypes = JobTypes.FullTime;
            TestDisplay(false, criteria);

            criteria.JobTypes = JobTypes.FullTime | JobTypes.PartTime | JobTypes.Temp;
            TestDisplay(false, criteria);
        }
    }
}
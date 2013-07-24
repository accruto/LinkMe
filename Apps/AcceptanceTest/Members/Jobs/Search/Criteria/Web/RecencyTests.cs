using System;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class RecencyTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.Recency = null;
            TestDisplay(false, criteria);

            criteria.Recency = new TimeSpan(1, 0, 0, 0);
            TestDisplay(false, criteria);

            criteria.Recency = new TimeSpan(7, 0, 0, 0);
            TestDisplay(false, criteria);

            criteria.Recency = new TimeSpan(JobAdSearchCriteria.DefaultRecency, 0, 0, 0);
            TestDisplay(false, criteria);
        }
    }
}
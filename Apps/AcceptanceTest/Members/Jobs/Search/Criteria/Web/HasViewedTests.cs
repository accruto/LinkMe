using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class HasViewedTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.HasViewed = null;
            TestDisplay(true, criteria);
            criteria.HasViewed = false;
            TestDisplay(true, criteria);
            criteria.HasViewed = true;
            TestDisplay(true, criteria);
        }
    }
}
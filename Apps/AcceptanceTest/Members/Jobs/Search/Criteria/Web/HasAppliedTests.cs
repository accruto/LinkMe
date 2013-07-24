using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class HasAppliedTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.HasApplied = null;
            TestDisplay(true, criteria);
            criteria.HasApplied = false;
            TestDisplay(true, criteria);
            criteria.HasApplied = true;
            TestDisplay(true, criteria);
        }
    }
}
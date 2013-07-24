using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class IsFlaggedTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.IsFlagged = null;
            TestDisplay(true, criteria);
            criteria.IsFlagged = false;
            TestDisplay(true, criteria);
            criteria.IsFlagged = true;
            TestDisplay(true, criteria);
        }
    }
}
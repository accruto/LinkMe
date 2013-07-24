using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class HasNotesTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var member = CreateMember(0);
            LogIn(member);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.HasNotes = null;
            TestDisplay(true, criteria);
            criteria.HasNotes = false;
            TestDisplay(true, criteria);
            criteria.HasNotes = true;
            TestDisplay(true, criteria);
        }
    }
}
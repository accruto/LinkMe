using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class IncludeSynonymsTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            criteria.IncludeSynonyms = true;
            TestDisplay(false, criteria);

            criteria.IncludeSynonyms = false;
            TestDisplay(false, criteria);
        }
    }
}
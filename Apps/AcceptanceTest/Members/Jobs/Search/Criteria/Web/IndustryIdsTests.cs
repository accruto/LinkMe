using System.Linq;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class IndustryIdsTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.IndustryIds = null;
            TestDisplay(false, criteria);

            var industries = _industriesQuery.GetIndustries();
            criteria.IndustryIds = new[] { industries[3].Id };
            TestDisplay(false, criteria);

            criteria.IndustryIds = new[] { industries[3].Id, industries[7].Id };
            TestDisplay(false, criteria);

            criteria.IndustryIds = (from i in industries select i.Id).ToList();
            TestDisplay(false, criteria);
        }
    }
}
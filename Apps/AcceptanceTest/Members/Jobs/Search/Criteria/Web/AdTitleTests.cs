using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class AdTitleTests
        : CriteriaTests
    {
        private const string JobTitle = "Archeologist";
        private const string QuotesJobTitle = "\"compliance officer\" OR \"compliance manager\"";

        protected override void TestDisplay()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            criteria.AdTitle = null;
            TestDisplay(false, criteria);

            criteria.AdTitle = JobTitle;
            TestDisplay(false, criteria);

            criteria.AdTitle = QuotesJobTitle;
            TestDisplay(false, criteria);
        }
    }
}
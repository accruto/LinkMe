using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    [TestClass]
    public class AdvertiserNameTests
        : CriteriaTests
    {
        private const string AdvertiserName = "Acme";
        private const string QuotesAdvertiserName = "\"Big Company\"";

        protected override void TestDisplay()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            criteria.AdvertiserName = null;
            TestDisplay(false, criteria);

            criteria.AdvertiserName = AdvertiserName;
            TestDisplay(false, criteria);

            criteria.AdvertiserName = QuotesAdvertiserName;
            TestDisplay(false, criteria);
        }
    }
}
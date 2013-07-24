using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches
{
    [TestClass]
    public class ApiKeywordsAlertsTests
        : ApiCriteriaAlertsTests
    {
        private const string Keywords = "Archeologist";

        protected override JobAdSearchCriteria GetCriteria()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            return criteria;
        }
    }
}

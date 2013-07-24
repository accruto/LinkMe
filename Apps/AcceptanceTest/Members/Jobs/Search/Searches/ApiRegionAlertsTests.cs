using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches
{
    [TestClass]
    public class ApiRegionAlertsTests
        : ApiCriteriaAlertsTests
    {
        private const string Keywords = "Archeologist";
        private const string Country = "Australia";
        private const string Region = "Melbourne";

        protected override JobAdSearchCriteria GetCriteria()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            criteria.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Region);
            criteria.Distance = 0;
            return criteria;
        }
    }
}

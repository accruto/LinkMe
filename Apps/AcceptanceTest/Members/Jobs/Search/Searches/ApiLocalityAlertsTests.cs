using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches
{
    [TestClass]
    public class ApiLocalityAlertsTests
        : ApiCriteriaAlertsTests
    {
        private const string Keywords = "Archeologist";
        private const string Country = "Australia";
        private const string Locality = "Melbourne VIC 3000";

        protected override JobAdSearchCriteria GetCriteria()
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            criteria.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Locality);
            criteria.Distance = 50;
            return criteria;
        }
    }
}

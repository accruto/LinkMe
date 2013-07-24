using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Criteria.Web
{
    [TestClass]
    public class CountrySubdivisionTests
        : LocationTests
    {
        [TestMethod]
        public void TestCountrySubdivision()
        {
            // Create jobs.

            PostJobAds();

            // Search within VIC.

            TestResults(
                _vic,
                null,
                _melbourneVic3000JobAd,
                _armadaleVic3143JobAd,
                _norlaneVic3214JobAd,
                _melbourneJobAd,
                _vicJobAd);

            // Search within NSW.

            TestResults(
                _nsw,
                null,
                _sydneyNsw2000JobAd,
                _sydneyJobAd,
                _nswJobAd);

            // Search within WA.

            TestResults(
                _wa,
                null);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Criteria.Web
{
    [TestClass]
    public class CountryTests
        : LocationTests
    {
        [TestMethod]
        public void TestCountry()
        {
            // Create jobs.

            PostJobAds();

            // Search within Australia.

            TestResults(
                _australia,
                null,
                _melbourneVic3000JobAd,
                _armadaleVic3143JobAd,
                _norlaneVic3214JobAd,
                _melbourneJobAd,
                _vicJobAd,
                _sydneyNsw2000JobAd,
                _sydneyJobAd,
                _nswJobAd,
                _australiaJobAd);

            // Search within New Zealand.

            TestResults(
                _newZealand,
                null,
                _newZealandJobAd,
                _aucklandJobAd,
                _northIslandJobAd);

            // No matter what you put in for New Zealand the same jobs will come up.

            TestResults(
                _northIsland,
                null,
                _newZealandJobAd,
                _aucklandJobAd,
                _northIslandJobAd);

            TestResults(
                _auckland,
                null,
                _newZealandJobAd,
                _aucklandJobAd,
                _northIslandJobAd);
        }
    }
}
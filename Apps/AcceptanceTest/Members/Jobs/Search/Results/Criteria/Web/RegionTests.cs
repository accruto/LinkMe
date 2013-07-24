using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Criteria.Web
{
    [TestClass]
    public class RegionTests
        : LocationTests
    {
        [TestMethod]
        public void TestRegion()
        {
            // Create jobs.

            PostJobAds();

            // Search within Melbourne.

            TestResults(
                _melbourne,
                null,
                _melbourneVic3000JobAd,
                _armadaleVic3143JobAd,
                _melbourneJobAd);

            // Search within Sydney.

            TestResults(
                _sydney,
                null,
                _sydneyNsw2000JobAd,
                _sydneyJobAd);

            // Search within Perth.

            TestResults(
                _perth,
                null);
        }
    }
}
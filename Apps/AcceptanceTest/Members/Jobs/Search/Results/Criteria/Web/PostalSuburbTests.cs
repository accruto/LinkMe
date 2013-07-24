using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Criteria.Web
{
    [TestClass]
    public class PostalSuburbTests
        : LocationTests
    {
        [TestMethod]
        public void TestPostalSuburb()
        {
            // Create jobs.

            PostJobAds();

            // Search within the default distance of Melbourne VIC 3000.

            TestResults(
                _melbourneVic3000,
                null,
                _melbourneVic3000JobAd,
                _armadaleVic3143JobAd,
                _melbourneJobAd);

            // Search within the default disatnce of Norlane VIC 3124.

            TestResults(
                _norlaneVic3214,
                null,
                _norlaneVic3214JobAd);

            // Search within the default distance of Sydney NSW 2000.

            TestResults(
                _sydneyNsw2000,
                null,
                _sydneyNsw2000JobAd,
                _sydneyJobAd);

            // Search within the default distance of Perth WA 6000.

            TestResults(
                _perthWa6000,
                null);
        }

        [TestMethod]
        public void TestPostalSuburbDistance()
        {
            // Create jobs.

            PostJobAds();

            // Search within 5 km of Melbourne VIC 3000: no Armadale jobs.

            TestResults(
                _melbourneVic3000,
                5,
                _melbourneVic3000JobAd);

            // Search within 10 km: Armadale back in.

            TestResults(
                _melbourneVic3000,
                10,
                _melbourneVic3000JobAd,
                _armadaleVic3143JobAd);

            // Search within 50 km: equivalent to the default.

            TestResults(
                _melbourneVic3000,
                50,
                _melbourneVic3000JobAd,
                _armadaleVic3143JobAd,
                _melbourneJobAd);

            // Search within 100 km: Norlane included.

            TestResults(
                _melbourneVic3000,
                100,
                _melbourneVic3000JobAd,
                _armadaleVic3143JobAd,
                _norlaneVic3214JobAd,
                _melbourneJobAd);

            // Search within 5 km of Norlane.

            TestResults(
                _norlaneVic3214,
                5,
                _norlaneVic3214JobAd);

            // Search within 10 km.

            TestResults(
                _norlaneVic3214,
                10,
                _norlaneVic3214JobAd);

            // Search within 50km: equivalent to the default.

            TestResults(
                _norlaneVic3214,
                50,
                _norlaneVic3214JobAd);

            // Search within 100 km.

            TestResults(
                _norlaneVic3214,
                100,
                _melbourneVic3000JobAd,
                _armadaleVic3143JobAd,
                _norlaneVic3214JobAd);
        }
    }
}
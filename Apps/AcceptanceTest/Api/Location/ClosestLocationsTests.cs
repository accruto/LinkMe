using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Api.Location
{
    [TestClass]
    public class ResolveLocationsTests
        : WebTestClass
    {
        private ReadOnlyUrl _resolveLocationUrl;
        private Country _newZealand;

        [TestInitialize]
        public void TestInitialize()
        {
            _resolveLocationUrl = new ReadOnlyApplicationUrl("~/api/location/resolve");

            _newZealand = _locationQuery.GetCountry("New Zealand");
        }

        [TestMethod]
        public void TestResolveSubdivision()
        {
            ResolveLocation(Australia, "VIC");
            ResolveLocation(Australia, "Victoria");
            ResolveLocation(Australia, "NSW");
            ResolveLocation(Australia, "New South Wales");
            ResolveLocation(Australia, "TAS");
            ResolveLocation(Australia, "Tasmania");
            ResolveLocation(Australia, "Tassie");
        }

        [TestMethod]
        public void TestResolveRegion()
        {
            // Not supported yet.

            ResolveLocation(Australia, "Melbourne");
            ResolveLocation(Australia, "Sydney");
            ResolveLocation(Australia, "Canberra");
        }

        [TestMethod]
        public void TestResolveLocality()
        {
            ResolveLocation(Australia, "3124");
        }

        [TestMethod]
        public void TestResolvePostalCode()
        {
            ResolveLocation(Australia, "0801");
        }

        [TestMethod]
        public void TestResolvePostalSuburb()
        {
            ResolveLocation(Australia, "Armatree NSW 2831");
            ResolveLocation(Australia, "ARMATREE NSW 2831");
            ResolveLocation(Australia, "2831 Armatree NSW");
            ResolveLocation(Australia, "Armatree");
        }

        [TestMethod]
        public void TestResolveCountry()
        {
            ResolveLocation(Australia, null);
            ResolveLocation(Australia, "");

            ResolveLocation(_newZealand, null);
            ResolveLocation(_newZealand, "abcd");
        }

        [TestMethod]
        public void TestCannotResolve()
        {
            TryResolveLocation(Australia, "xyz");
        }

        [TestMethod]
        public void TestNoCountry()
        {
            ResolveLocation(null, "VIC");
            ResolveLocation(null, "Melbourne");
            ResolveLocation(null, "3124");
            ResolveLocation(null, "0801");
            ResolveLocation(null, "Armatree NSW 2831");
            ResolveLocation(null, null);
            ResolveLocation(null, "");
            TryResolveLocation(null, "xyz");
        }

        private void ResolveLocation(Country country, string location)
        {
            var url = _resolveLocationUrl.AsNonReadOnly();
            if (country != null)
                url.QueryString.Add("countryId", country.Id.ToString());
            url.QueryString.Add("location", location);
            var response = Post(url);
            Assert.AreEqual("{\"Location\":\"" + location + "\",\"ResolvedLocation\":\"" + location + "\"}", response);
        }

        private void TryResolveLocation(Country country, string location)
        {
            var url = _resolveLocationUrl.AsNonReadOnly();
            if (country != null)
                url.QueryString.Add("countryId", country.Id.ToString());
            url.QueryString.Add("location", location);
            var response = Post(url);
            Assert.AreEqual("{\"Location\":\"" + location + "\",\"ResolvedLocation\":null}", response);
        }
    }
}
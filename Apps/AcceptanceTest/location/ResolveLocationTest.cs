using LinkMe.AcceptanceTest.ui;
using LinkMe.Domain.Location;
using LinkMe.Web.UI.Controls.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Location
{
    [TestClass]
    public class ResolveLocationTest : WebFormDataTestCase
    {
        private Country _newZealand;

        [TestInitialize]
        public void TestInitialize()
        {
            _newZealand = _locationQuery.GetCountry("New Zealand");
        }

        [TestMethod]
        public void ResolveSubdivisionTest()
        {
            ResolveNamedLocation(Australia, "VIC");
            ResolveNamedLocation(Australia, "Victoria");
            ResolveNamedLocation(Australia, "NSW");
            ResolveNamedLocation(Australia, "New South Wales");
            ResolveNamedLocation(Australia, "TAS");
            ResolveNamedLocation(Australia, "Tasmania");
            ResolveNamedLocation(Australia, "Tassie");
        }

        [TestMethod]
        public void ResolveRegionTest()
        {
            // Not supported yet.

            ResolveNamedLocation(Australia, "Melbourne");
            ResolveNamedLocation(Australia, "Sydney");
            ResolveNamedLocation(Australia, "Canberra");
        }

        [TestMethod]
        public void ResolveLocalityTest()
        {
            ResolveNamedLocation(Australia, "3124");
        }

        [TestMethod]
        public void ResolvePostalCodeTest()
        {
            ResolveNamedLocation(Australia, "0801");
        }

        [TestMethod]
        public void ResolvePostalSuburbTest()
        {
            ResolveNamedLocation(Australia, "Armatree NSW 2831");
            ResolveNamedLocation(Australia, "ARMATREE NSW 2831");
            ResolveNamedLocation(Australia, "2831 Armatree NSW");
            ResolveNamedLocation(Australia, "Armatree");
        }

        [TestMethod]
        public void ResolveCountryTest()
        {
            ResolveNamedLocation(Australia, null);
            ResolveNamedLocation(Australia, "");

            ResolveNamedLocation(_newZealand, null);
            ResolveNamedLocation(_newZealand, "abcd");
        }

        [TestMethod]
        public void CannotResolveTest()
        {
            TryResolve(Australia, "xyz");
        }

        private void ResolveNamedLocation(Country country, string location)
        {
            string response = PostAjaxProRequest(typeof(ResolveLocation), "ResolveNamedLocation", "countryId", country.Id.ToString(), "location", location);
            Assert.AreEqual("{\"value\":\"" + location + "\"}", response);
        }

        private void TryResolve(Country country, string location)
        {
            string response = PostAjaxProRequest(typeof(ResolveLocation), "ResolveNamedLocation", "countryId", country.Id.ToString(), "location", location);
            Assert.AreEqual("{\"value\":\"\"}", response);
        }
    }
}

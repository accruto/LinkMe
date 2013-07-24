using LinkMe.Domain.Data;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Location
{
    [TestClass]
    public class ToStringTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = new LocationQuery(new LocationRepository(Resolve<IDataContextFactory>()));
        private Country _australia;
        private Country _newZealand;

        [TestInitialize]
        public void ToStringTestsInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
            _newZealand = _locationQuery.GetCountry("New Zealand");
        }

        [TestMethod]
        public void PostalSuburbTest()
        {
            AssertToString(_australia, "Norlane 3214 VIC", "Norlane VIC 3214", "VIC", "Norlane 3214", "");
            AssertToString(_australia, "Camberwell 3124 VIC", "Camberwell VIC 3124", "VIC", "Camberwell 3124", "");
        }

        [TestMethod]
        public void PostalCodeTest()
        {
            AssertToString(_australia, "0801", "0801 NT", "NT", "0801", "");
        }

        [TestMethod]
        public void LocalityTest()
        {
            AssertToString(_australia, "3124", "3124 VIC", "VIC", "3124", "");

            // Locality which spans multiple subdivisions, which means that no subdivision will be shown.

            AssertToString(_australia, "2620", "2620", "", "2620", "");
        }

        [TestMethod]
        public void RegionTest()
        {
            // A regions display text is not subject to restriction.

            AssertToString(_australia, "Melbourne", "Melbourne", "Melbourne", "Melbourne", "Melbourne");
        }

        [TestMethod]
        public void CountrySubdivisionTest()
        {
            AssertToString(_australia, "VIC", "VIC", "VIC", "", "");
        }

        [TestMethod]
        public void CountryTest()
        {
            AssertToString(_australia, "", "", "", "", "");
            AssertToString(_newZealand, "", "", "", "", "");
        }

        [TestMethod]
        public void UnresolvedTest()
        {
            // With a subdivision.

            AssertToString(_australia, "xyz VIC", "xyz VIC", "VIC", "", "");

            // Without a subdivision.

            AssertToString(_australia, "xyz", "xyz", "", "", "");
        }

        private void AssertToString(Country country, string location, string displayText, string subdivisionDisplayText, string subSubdivisionDisplayText, string noAccessDisplayText)
        {
            // Resolve the location.

            var locationReference = _locationQuery.ResolveLocation(country, location);
            Assert.AreEqual(string.IsNullOrEmpty(location), locationReference.IsCountry);

            // Check the display text against the various combinations.

            Assert.AreEqual(displayText, locationReference.ToString());
            Assert.AreEqual(displayText, locationReference.ToString(true, true));
            Assert.AreEqual(subdivisionDisplayText, locationReference.ToString(true, false));
            Assert.AreEqual(subSubdivisionDisplayText, locationReference.ToString(false, true));
            Assert.AreEqual(noAccessDisplayText, locationReference.ToString(false, false));
        }
    }
}

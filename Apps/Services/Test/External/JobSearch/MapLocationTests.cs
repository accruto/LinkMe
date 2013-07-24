using LinkMe.Apps.Services.External.JobSearch;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.JobSearch
{
    [TestClass]
    public class MapLocationTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private Country _australia;

        [TestInitialize]
        public void TestInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
        }

        [TestMethod]
        public void TestMapState()
        {
            var location = _locationQuery.ResolveLocation(_australia, "VIC");
            string state;
            string postcode;
            string suburb;
            location.Map(out state, out postcode, out suburb);

            Assert.AreEqual("VIC", state);
            Assert.AreEqual("3000", postcode);
            Assert.AreEqual("MELBOURNE", suburb);
        }

        [TestMethod]
        public void TestMapRegion()
        {
            var location = _locationQuery.ResolveLocation(_australia, "Melbourne");
            string state;
            string postcode;
            string suburb;
            location.Map(out state, out postcode, out suburb);

            Assert.AreEqual("VIC", state);
            Assert.AreEqual("3000", postcode);
            Assert.AreEqual("MELBOURNE",suburb);
        }

        [TestMethod]
        public void TestMapPostalSuburb()
        {
            var location = _locationQuery.ResolveLocation(_australia, "Armadale VIC");
            string state;
            string postcode;
            string suburb;
            location.Map(out state, out postcode, out suburb);

            Assert.AreEqual("VIC", state);
            Assert.AreEqual("3143", postcode);
            Assert.AreEqual("ARMADALE", suburb);
        }

        [TestMethod]
        public void TestMapRegionAlias()
        {
            var location = _locationQuery.ResolveLocation(_australia, "Gold Coast (All Gold Coast)");
            string state;
            string postcode;
            string suburb;
            location.Map(out state, out postcode, out suburb);

            Assert.AreEqual("QLD", state);
            Assert.AreEqual("4217", postcode);
            Assert.AreEqual("GOLD COAST", suburb);
        }

        [TestMethod]
        public void TestMapUnstructuredLocation()
        {
            var location = _locationQuery.ResolveLocation(_australia, "xyz");
            string state;
            string postcode;
            string suburb;
            location.Map(out state, out postcode, out suburb);

            Assert.IsNull(state);
            Assert.IsNull(postcode);
            Assert.IsNull(suburb);
        }
    }
}
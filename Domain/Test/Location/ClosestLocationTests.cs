using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Location
{
    [TestClass]
    public class ClosestLocationTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private Country _australia;
        private Country _newZealand;

        [TestInitialize]
        public void TestInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
            _newZealand = _locationQuery.GetCountry("New Zealand");
        }

        [TestMethod]
        public void TestClosest()
        {
            TestClosest(_australia, -38.07888f, 144.3577f, "3214");
            TestClosest(_australia, -38.089918f, 144.365416f, "3214");
            TestClosest(_australia, -37.82452f, 145.0741f, "3126");
            TestClosest(_australia, -34.843395f, 138.623257f, "5085");
            TestClosest(_australia, -35.143494f, 139.275742f, "5254");
            TestClosest(_australia, -34.990629f, 139.378052f, "5253");
            TryTestClosest(_newZealand, -34.990629f, 139.378052f);
        }

        [TestMethod]
        public void TestNoPostBoxes()
        {
            // These coordinates correspond exactly to post code 1355 which is a postal box near Bondi.
            // That should be ignored though and the nearest delivery area 2022 should be returned.

            TestClosest(_australia, -33.89645f, 151.2427f, "2022");
        }

        private void TestClosest(Country country, float latitude, float longitude, string expectedName)
        {
            Assert.AreEqual(expectedName, _locationQuery.GetClosestLocality(country, new GeoCoordinates(latitude, longitude)).Name);
        }

        private void TryTestClosest(Country country, float latitude, float longitude)
        {
            Assert.IsNull(_locationQuery.GetClosestLocality(country, new GeoCoordinates(latitude, longitude)));
        }
    }
}

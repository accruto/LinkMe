using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Api.Location
{
    [TestClass]
    public class ClosestLocationsTests
        : WebTestClass
    {
        private ReadOnlyUrl _closestLocationUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _closestLocationUrl = new ReadOnlyApplicationUrl("~/api/location/closest");
        }

        [TestMethod]
        public void TestClosest()
        {
            ClosestLocation(Australia, -38.089918f, 144.365416f, "Corio VIC 3214");
            ClosestLocation(Australia, -37.837581f, 145.068626f, "Camberwell VIC 3124");
        }

        private void ClosestLocation(Country country, float latitude, float longitude, string location)
        {
            var url = _closestLocationUrl.AsNonReadOnly();
            if (country != null)
                url.QueryString.Add("countryId", country.Id.ToString());
            url.QueryString.Add("latitude", latitude.ToString());
            url.QueryString.Add("longitude", longitude.ToString());
            var response = Post(url);
            Assert.AreEqual("{\"Location\":\"" + location + "\",\"ResolvedLocation\":\"" + location + "\"}", response);
        }
    }
}
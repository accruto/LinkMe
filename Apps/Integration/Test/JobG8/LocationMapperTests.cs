using LinkMe.Apps.Integration.JobG8;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8
{
    [TestClass]
    public class LocationMapperTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        [TestMethod]
        public void MapByCountry()
        {
            var locationMapper = new LocationMapper(_locationQuery);

            var locationRef = locationMapper.Map("Australia", null, null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.IsTrue(locationRef.IsCountry);

            locationRef = locationMapper.Map("United Kingdom", null, null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.IsTrue(locationRef.IsCountry);

            locationRef = locationMapper.Map("United States", null, null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.IsTrue(locationRef.IsCountry);

            try
            {
                locationMapper.Map("Kazakhstan", null, null, null);
                Assert.Fail();
            }
            catch (ServiceEndUserException ex)
            {
                Assert.AreEqual("Invalid country.", ex.Message);
            }
        }

        [TestMethod]
        public void MapByCountryLocation()
        {
            var locationMapper = new LocationMapper(_locationQuery);

            // Sydney
            var locationRef = locationMapper.Map("Australia", "Sydney", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("NSW", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("Sydney", locationRef.NamedLocation.Name);

            // Gold Coast
            locationRef = locationMapper.Map("Australia", "Gold Coast", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("QLD", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("Gold Coast", locationRef.NamedLocation.Name);

            // NSW Other
            locationRef = locationMapper.Map("Australia", "NSW Other", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("NSW", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("New South Wales", locationRef.NamedLocation.Name);

            // VIC Other
            locationRef = locationMapper.Map("Australia", "VIC Other", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("VIC", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("Victoria", locationRef.NamedLocation.Name);

            // TAS Other
            locationRef = locationMapper.Map("Australia", "TAS Other", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("TAS", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("Tasmania", locationRef.NamedLocation.Name);

            // WA Other
            locationRef = locationMapper.Map("Australia", "WA Other", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("WA", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("Western Australia", locationRef.NamedLocation.Name);

            // SA Other
            locationRef = locationMapper.Map("Australia", "SA Other", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("SA", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("South Australia", locationRef.NamedLocation.Name);

            // Auckland
            locationRef = locationMapper.Map("New Zealand", "Auckland", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("New Zealand", locationRef.Country.Name);

            // Wellington
            locationRef = locationMapper.Map("New Zealand", "Wellington", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("New Zealand", locationRef.Country.Name);

            // North Island Other
            locationRef = locationMapper.Map("New Zealand", "North Island Other", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("New Zealand", locationRef.Country.Name);

            // UK Other
            locationRef = locationMapper.Map("United Kingdom", "UK Other", null, null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("United Kingdom", locationRef.Country.Name);

            // Perth
            locationRef = locationMapper.Map("Australia", "Western Australia", "Perth", null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("Australia", locationRef.Country.Name);
            Assert.AreEqual("Perth", locationRef.NamedLocation.Name);
        }

        [TestMethod]
        public void MapByCountryLocationArea()
        {
            var locationMapper = new LocationMapper(_locationQuery);

            // Ballarat
            LocationReference locationRef = locationMapper.Map("Australia", "VIC Other", "Ballarat", null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("VIC", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("Ballarat", locationRef.NamedLocation.Name);

            // Albury Wodonga
            locationRef = locationMapper.Map("Australia", "NSW Other", "Albury Wodonga", null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("NSW", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("Albury", locationRef.NamedLocation.Name);

            // Alice Springs
            locationRef = locationMapper.Map("Australia", "NT Other", "Alice Springs", null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("NT", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("Alice Springs", locationRef.NamedLocation.Name);

            // Auckland Inner
            locationRef = locationMapper.Map("New Zealand", "Auckland", "Auckland Inner", null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("New Zealand", locationRef.Country.Name);

            // Sydney Inner
            locationRef = locationMapper.Map("Australia", "Sydney", "Sydney Inner", null);
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("NSW", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("Sydney", locationRef.NamedLocation.Name);
        }

        [TestMethod]
        public void MapByPostcode()
        {
            var locationMapper = new LocationMapper(_locationQuery);

            LocationReference locationRef = locationMapper.Map("Australia", null, null, "3182");
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("VIC", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("3182", locationRef.NamedLocation.Name);

            locationRef = locationMapper.Map("Australia", "Melbourne", null, "3182");
            Assert.IsTrue(locationRef.IsFullyResolved);
            Assert.AreEqual("VIC", locationRef.CountrySubdivision.ShortName);
            Assert.AreEqual("3182", locationRef.NamedLocation.Name);
        }
    }
}

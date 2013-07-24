using LinkMe.Apps.Services.External.HrCareers;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.HrCareers
{
    [TestClass]
    public class LocationMapperTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private Country _australia;
        private LocationMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _mapper = new LocationMapper(_locationQuery);
            _australia = _locationQuery.GetCountry("Australia");
        }

        [TestMethod]
        public void LocalityTest()
        {
            var bentleigh = _locationQuery.ResolveLocation(_australia, "3204");
            var mapped = _mapper.Map(bentleigh);
            Assert.AreEqual("20-168-1", mapped.id);
        }

        [TestMethod]
        public void PostalSuburbTest()
        {
            var bentleigh = _locationQuery.ResolveLocation(_australia, "Bentleigh 3204");
            var mapped = _mapper.Map(bentleigh);
            Assert.AreEqual("20-168-1", mapped.id);
            Assert.AreEqual("Bentleigh VIC 3204", mapped.name);
        }

        [TestMethod]
        public void MetroTest()
        {
            var melbourne = _locationQuery.ResolveLocation(_australia, "Melbourne");
            var mapped = _mapper.Map(melbourne);
            Assert.AreEqual("20-168-1", mapped.id);
            Assert.AreEqual("Melbourne", mapped.name);
        }

        [TestMethod]
        public void RegionalTest()
        {
            var bendigo = _locationQuery.ResolveLocation(_australia, "3550");
            var mapped = _mapper.Map(bendigo);
            Assert.AreEqual("20-168-2", mapped.id);
        }
    }
}
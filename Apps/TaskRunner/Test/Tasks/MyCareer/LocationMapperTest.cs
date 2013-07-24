using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.TaskRunner.Tasks.MyCareer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.MyCareer
{
    [TestClass]
    public class LocationMapperTest
        : TaskTests
    {
        private LocationMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _mapper = new LocationMapper(Resolve<ILocationQuery>());
        }

        [TestMethod]
        public void CanMapFullLocation()
        {
            LocationReference locationReference;
            bool ok = _mapper.TryMap("Horsham,VIC 3400", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual("3400", locationReference.NamedLocation.Name);
        }

        [TestMethod]
        public void CanMapMetro()
        {
            LocationReference locationReference;
            bool ok = _mapper.TryMap("Sydney Metro,NSW     ", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "Sydney");

            ok = _mapper.TryMap("Melbourne Metro,VIC     ", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "Melbourne");

            ok = _mapper.TryMap("Brisbane Metro,QLD     ", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "Brisbane");

            ok = _mapper.TryMap("Adelaide Metro,SA     ", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "Adelaide");

            ok = _mapper.TryMap("Perth Metro,WA     ", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "Perth");
        }

        [TestMethod]
        public void CanMapCbd()
        {
            LocationReference locationReference;
            bool ok = _mapper.TryMap("Perth CBD,WA  6000", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual("6000", locationReference.NamedLocation.Name);
        }

        [TestMethod]
        public void CanMapGoldCoastAndHinterland()
        {
            LocationReference locationReference;
            bool ok = _mapper.TryMap("Gold Coast & Hinterland,QLD  ", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "Gold Coast");
        }

        [TestMethod]
        public void CanMapStKildaRd()
        {
            LocationReference locationReference;
            bool ok = _mapper.TryMap("Melbourne (St Kilda Road),VIC 3004", out locationReference);
            Assert.IsTrue(ok);
            Assert.AreEqual("3004", locationReference.Postcode);
        }

        [TestMethod]
        public void CanMapMelbourneSoutheasternSuburbs()
        {
            LocationReference locationReference;
            bool ok = _mapper.TryMap("Southeastern Suburbs, Melbourne,VIC     ", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "Melbourne");
        }

        [TestMethod]
        public void CanMapMelbourneInnerSuburbs()
        {
            LocationReference locationReference;
            bool ok = _mapper.TryMap("Melbourne Inner Suburbs,VIC     ", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "Melbourne");
        }

        [TestMethod]
        public void CanMapGoldCoastQld()
        {
            LocationReference locationReference;
            bool ok = _mapper.TryMap("Gold Coast,QLD", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "Gold Coast");
        }

        [TestMethod]
        public void CanMapAliceSpringsNt0870()
        {
            LocationReference locationReference;
            bool ok = _mapper.TryMap("Alice Springs,NT  0870", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "0870");
        }

        [TestMethod]
        public void CanMapCairnsAndRegionQld()
        {
            LocationReference locationReference;
            bool ok = _mapper.TryMap("Cairns & Region,QLD", out locationReference);
            Assert.IsTrue(ok);
            Assert.IsTrue(locationReference.IsFullyResolved);
            Assert.AreEqual(locationReference.NamedLocation.Name, "4870");
        }
    }
}
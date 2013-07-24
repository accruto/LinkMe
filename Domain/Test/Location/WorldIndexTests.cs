using System;
using System.Linq;
using System.Collections.Generic;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Location
{
    [TestClass]
    public class WorldIndexTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private WorldIndex _world;
        private Country _australia;

        [TestInitialize]
        public void WorldIndexTestsInitialize()
        {
            if (_world == null)
            {
                _world = new WorldIndex();
                _world.BuildUp(_locationQuery, true);
                _australia = _locationQuery.GetCountry("Australia");
            }
        }

        [TestMethod]
        public void EarthDistanceTest()
        {
            // Distance between poles
            double distance = WorldIndex.EarthDistance(90, 0, -90, 0);
            Assert.AreEqual(20021, (int)Math.Round(distance));

            // Distance between pole and equator
            distance = WorldIndex.EarthDistance(90, 0, 0, 0);
            Assert.AreEqual(10010, (int)Math.Round(distance));

            // Distance between LAX and BNA (as per http://en.wikipedia.org/wiki/Great-circle_distance)
            distance = WorldIndex.EarthDistance(36.12, -86.67, 33.94, -118.40);
            Assert.AreEqual(2887, (int)Math.Round(distance));

            // Distance between Bentleigh and StKilda
            distance = WorldIndex.EarthDistance(-33.65436, 151.279053, -33.62562, 151.146759);
            Assert.AreEqual(13, (int)Math.Round(distance));
        }

        [TestMethod]
        public void DistanceTest()
        {
            var bentleigh = _locationQuery.ResolveLocation(_australia, "3204").Locality;
            var stkilda = _locationQuery.ResolveLocation(_australia, "3182").Locality;

            Assert.AreEqual(8, _world.Distance(bentleigh, stkilda));
            Assert.AreEqual(8, _world.Distance(stkilda, bentleigh));
            Assert.AreEqual(0, _world.Distance(stkilda, stkilda));
        }

        [TestMethod]
        public void ContainmentTest()
        {
            var bentleighLoc = _locationQuery.ResolveLocation(_australia, "3204");
            var bentleigh = _world.GetPointSet(bentleighLoc);

            var melbourneMetroLoc = _locationQuery.ResolveLocation(_australia, "Melbourne");
            var melbourneMetro = _world.GetPointSet(melbourneMetroLoc);

            var bendigoLoc = _locationQuery.ResolveLocation(_australia, "3550");
            var bendigo = _world.GetPointSet(bendigoLoc);

            var victoriaLoc = _locationQuery.ResolveLocation(_australia, "VIC");
            var victoria = _world.GetPointSet(victoriaLoc);
            var countryVictoria = new HashSet<int>(victoria.Except(melbourneMetro));

            // Bentleigh is in Melbourne Metro.

            Assert.IsTrue(bentleigh.IsSubsetOf(melbourneMetro));

            // Bentleigh, Bendigo and Melbourne Metro are in Victoria.

            Assert.IsTrue(bentleigh.IsSubsetOf(victoria));
            Assert.IsTrue(bendigo.IsSubsetOf(victoria));
            Assert.IsTrue(melbourneMetro.IsSubsetOf(victoria));

            // Bendigo is not in Melbourne Metro.

            Assert.IsFalse(bendigo.IsSubsetOf(melbourneMetro));

            // Bendigo is in country Victoria.

            Assert.IsTrue(bendigo.IsSubsetOf(countryVictoria));

            // Bentleigh and Melbourne Metro are not in country Victoria.

            Assert.IsFalse(bentleigh.IsSubsetOf(countryVictoria));
            Assert.IsFalse(melbourneMetro.IsSubsetOf(countryVictoria));
        }

        [TestMethod]
        public void Within50KmFromMelbourne3000()
        {
            // The region Melbourne was initially defined to be 50km from 3000 so make
            // sure this still holds for now.

            var locationReference = _locationQuery.ResolveLocation(_australia, "Melbourne");
            Region melbourneMetro = locationReference.Region;

            locationReference = _locationQuery.ResolveLocation(_australia, "Melbourne VIC 3000");
            Locality melbourne3000 = locationReference.Locality;

            double distMax = 0;
            Locality mostRemote = null;

            foreach (Locality locality in _locationQuery.GetLocalities(melbourneMetro))
            {
                double dist = WorldIndex.EarthDistance(
                    melbourne3000.Centroid.Latitude, melbourne3000.Centroid.Longitude,
                    locality.Centroid.Latitude, locality.Centroid.Longitude);

                if (dist > distMax)
                {
                    distMax = dist;
                    mostRemote = locality;
                }
            }

            Assert.IsTrue(distMax <= 50, "The '" + mostRemote.Name + "' locality has a distance + " + distMax + "km which is more than 50km away from Melbourne VIC 3000.");
        }

        [TestMethod]
        public void Within50KmFromSydney2000()
        {
            var locationReference = _locationQuery.ResolveLocation(_australia, "Sydney");
            Region sydneyMetro = locationReference.Region;

            locationReference = _locationQuery.ResolveLocation(_australia, "Sydney NSW 2000");
            Locality sydney2000 = locationReference.Locality;

            double distMax = 0;
            Locality mostRemote = null;

            foreach (Locality locality in _locationQuery.GetLocalities(sydneyMetro))
            {
                double dist = WorldIndex.EarthDistance(
                    sydney2000.Centroid.Latitude, sydney2000.Centroid.Longitude,
                    locality.Centroid.Latitude, locality.Centroid.Longitude);

                if (dist > distMax)
                {
                    distMax = dist;
                    mostRemote = locality;
                }
            }

            Assert.IsTrue(distMax <= 50, "The '" + mostRemote.Name + "' locality has a distance + " + distMax + "km which is more than 50km away from Sydney NSW 2000.");
        }
    }
}
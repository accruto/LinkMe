using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Location
{
    [TestClass]
    public class ResolveLocationTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private Country _australia;

        [TestInitialize]
        public void ResolveLocationTestsInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
        }

        [TestMethod]
        public void TestResolveSubdivision()
        {
            // The location returned is a result of calling locationReference.ToString().
            // Resolving using that string should give you back exactly the same thing.

            var location = Resolve(_australia, "VIC", typeof(CountrySubdivision), "Victoria");
            Resolve(_australia, location, typeof(CountrySubdivision), "Victoria");

            location = Resolve(_australia, "Victoria", typeof(CountrySubdivision), "Victoria");
            Resolve(_australia, location, typeof(CountrySubdivision), "Victoria");

            location = Resolve(_australia, "NSW", typeof(CountrySubdivision), "New South Wales");
            Resolve(_australia, location, typeof(CountrySubdivision), "New South Wales");

            location = Resolve(_australia, "New South Wales", typeof(CountrySubdivision), "New South Wales");
            Resolve(_australia, location, typeof(CountrySubdivision), "New South Wales");

            location = Resolve(_australia, "TAS", typeof(CountrySubdivision), "Tasmania");
            Resolve(_australia, location, typeof(CountrySubdivision), "Tasmania");

            location = Resolve(_australia, "Tasmania", typeof(CountrySubdivision), "Tasmania");
            Resolve(_australia, location, typeof(CountrySubdivision), "Tasmania");

            location = Resolve(_australia, "Tassie", typeof(CountrySubdivision), "Tasmania");
            Resolve(_australia, location, typeof(CountrySubdivision), "Tasmania");

            location = Resolve(_australia, "SA AustraliA", typeof(CountrySubdivision), "South Australia");
            Resolve(_australia, location, typeof(CountrySubdivision), "South Australia");

            location = Resolve(_australia, "South Australia", typeof(CountrySubdivision), "South Australia");
            Resolve(_australia, location, typeof(CountrySubdivision), "South Australia");
        }

        [TestMethod]
        public void TestTrimResolveSubdivision()
        {
            // The location returned is a result of calling locationReference.ToString().
            // Resolving using that string should give you back exactly the same thing.

            Resolve(_australia, "VIC", typeof (CountrySubdivision), "Victoria");
            Resolve(_australia, "Victoria", typeof(CountrySubdivision), "Victoria");

            Resolve(_australia, "  VIC", typeof(CountrySubdivision), "Victoria");
            Resolve(_australia, "  Victoria", typeof(CountrySubdivision), "Victoria");

            Resolve(_australia, "VIC  ", typeof(CountrySubdivision), "Victoria");
            Resolve(_australia, "Victoria  ", typeof(CountrySubdivision), "Victoria");

            Resolve(_australia, " VIC ", typeof(CountrySubdivision), "Victoria");
            Resolve(_australia, " Victoria ", typeof(CountrySubdivision), "Victoria");
        }

        [TestMethod]
        public void TestResolveRegion()
        {
            var location = Resolve(_australia, "Melbourne", typeof(Region), "Melbourne");
            Resolve(_australia, location, typeof(Region), "Melbourne");

            location = Resolve(_australia, "Melbourne (All Melbourne)", typeof(Region), "Melbourne");
            Resolve(_australia, location, typeof(Region), "Melbourne");

            location = Resolve(_australia, "Sydney", typeof(Region), "Sydney");
            Resolve(_australia, location, typeof(Region), "Sydney");

            location = Resolve(_australia, "Sydney", typeof(Region), "Sydney");
            Resolve(_australia, location, typeof(Region), "Sydney");

            location = Resolve(_australia, "Canberra", typeof(Region), "Canberra");
            Resolve(_australia, location, typeof(Region), "Canberra");

            location = Resolve(_australia, "Canberra (All Canberra)", typeof(Region), "Canberra");
            Resolve(_australia, location, typeof(Region), "Canberra");

            location = Resolve(_australia, "Sydney AustraliA", typeof(Region), "Sydney");
            Resolve(_australia, location, typeof(Region), "Sydney");
        }

        [TestMethod]
        public void TestTrimResolveRegion()
        {
            Resolve(_australia, "Melbourne", typeof (Region), "Melbourne");
            Resolve(_australia, "  Melbourne", typeof(Region), "Melbourne");
            Resolve(_australia, "Melbourne  ", typeof(Region), "Melbourne");
            Resolve(_australia, " Melbourne ", typeof(Region), "Melbourne");
        }

        [TestMethod]
        public void TestResolveLocality()
        {
            var location = Resolve(_australia, "3124", typeof(Locality), "3124");
            Resolve(_australia, location, typeof(Locality), "3124");

            // Locality that spans 2 subdivisions.

            location = Resolve(_australia, "2620", typeof(Locality), "2620");
            Resolve(_australia, location, typeof(Locality), "2620");

            location = Resolve(_australia, "2620 NSW", typeof(Locality), "2620");
            Resolve(_australia, location, typeof(Locality), "2620");

            location = Resolve(_australia, "2620 ACT", typeof(Locality), "2620");
            Resolve(_australia, location, typeof(Locality), "2620");

            location = Resolve(_australia, "2009 australiA", typeof(Locality), "2009");
            Resolve(_australia, location, typeof(Locality), "2009");

            location = Resolve(_australia, "2009 NSW AustraliA", typeof(Locality), "2009");
            Resolve(_australia, location, typeof(Locality), "2009");

            location = Resolve(_australia, "2009 New South Wales Australia", typeof(Locality), "2009");
            Resolve(_australia, location, typeof(Locality), "2009");
        }

        [TestMethod]
        public void TestTrimResolveLocality()
        {
            Resolve(_australia, "3124", typeof (Locality), "3124");
            Resolve(_australia, "  3124", typeof(Locality), "3124");
            Resolve(_australia, "3124  ", typeof(Locality), "3124");
            Resolve(_australia, " 3124 ", typeof(Locality), "3124");
        }

        [TestMethod]
        public void TestResolvePostalCode()
        {
            var location = Resolve(_australia, "0801", typeof(PostalCode), "0801");
            Resolve(_australia, location, typeof(PostalCode), "0801");

            location = Resolve(_australia, "0801 austRalia", typeof(PostalCode), "0801");
            Resolve(_australia, location, typeof(PostalCode), "0801");

            // Country first

            location = Resolve(_australia, "Australia 0801", typeof(PostalCode), "0801");
            Resolve(_australia, location, typeof(PostalCode), "0801");

            /* TODO: Fix this - shouldn't PostalCode be preferred over Locality if both match the name?

            // PostalCode that has no Locality with the same name

            location = Resolve(_australia, "Australia 6770", typeof(PostalCode), "6770");
            Resolve(_australia, location, typeof(PostalCode), "6770");
            */
        }

        [TestMethod]
        public void TestTrimResolvePostalCode()
        {
            Resolve(_australia, "0801", typeof (PostalCode), "0801");
            Resolve(_australia, "  0801", typeof(PostalCode), "0801");
            Resolve(_australia, "0801  ", typeof(PostalCode), "0801");
            Resolve(_australia, " 0801 ", typeof(PostalCode), "0801");
        }

        [TestMethod]
        public void TestResolvePostalSuburb()
        {
            var location = Resolve(_australia, "Armadale VIC 3143", typeof(PostalSuburb), "Armadale");
            Resolve(_australia, location, typeof(PostalSuburb), "Armadale");

            location = Resolve(_australia, "Armadale 3143", typeof(PostalSuburb), "Armadale");
            Resolve(_australia, location, typeof(PostalSuburb), "Armadale");

            location = Resolve(_australia, "Pyrmont 2009 Australia", typeof(PostalSuburb), "Pyrmont");
            Resolve(_australia, location, typeof(PostalSuburb), "Pyrmont");

            location = Resolve(_australia, "Pyrmont 2009 Australia", typeof(PostalSuburb), "Pyrmont");
            Resolve(_australia, location, typeof(PostalSuburb), "Pyrmont");

            location = Resolve(_australia, "Pyrmont 2009 NSW Australia", typeof(PostalSuburb), "Pyrmont");
            Resolve(_australia, location, typeof(PostalSuburb), "Pyrmont");

            location = Resolve(_australia, "Pyrmont NSW Australia 2009", typeof(PostalSuburb), "Pyrmont");
            Resolve(_australia, location, typeof(PostalSuburb), "Pyrmont");

            location = Resolve(_australia, "Pyrmont Australia 2009", typeof(PostalSuburb), "Pyrmont");
            Resolve(_australia, location, typeof(PostalSuburb), "Pyrmont");

            location = TryResolve(_australia, "xyz");
            TryResolve(_australia, location);

            location = Resolve(_australia, "Perth Western Australia", typeof(PostalSuburb), "Perth");
            Resolve(_australia, location, typeof(PostalSuburb), "Perth");

            location = Resolve(_australia, "Perth, Western Australia", typeof(PostalSuburb), "Perth");
            Resolve(_australia, location, typeof(PostalSuburb), "Perth");
        }

        [TestMethod]
        public void TestTrimResolvePostalSuburb()
        {
            Resolve(_australia, "Armadale VIC 3143", typeof (PostalSuburb), "Armadale");
            Resolve(_australia, "  Armadale VIC 3143", typeof(PostalSuburb), "Armadale");
            Resolve(_australia, "Armadale VIC 3143  ", typeof(PostalSuburb), "Armadale");
            Resolve(_australia, " Armadale VIC 3143 ", typeof(PostalSuburb), "Armadale");
        }

        [TestMethod]
        public void TestResolveCountry()
        {
            var location = Resolve(_australia, null, typeof(CountrySubdivision), null);
            Resolve(_australia, location, typeof(CountrySubdivision), null);

            location = Resolve(_australia, "", typeof(CountrySubdivision), null);
            Resolve(_australia, location, typeof(CountrySubdivision), null);

            location = Resolve(_australia, "australiA", typeof(CountrySubdivision), null);
            Resolve(_australia, location, typeof(CountrySubdivision), null);
        }

        [TestMethod]
        public void TestTrimResolveCountry()
        {
            Resolve(_australia, "Australia", typeof(CountrySubdivision), null);
            Resolve(_australia, "  Australia", typeof(CountrySubdivision), null);
            Resolve(_australia, "Australia  ", typeof(CountrySubdivision), null);
            Resolve(_australia, " Australia ", typeof(CountrySubdivision), null);
        }

        private string Resolve(Country country, string location, System.Type expectedType, string expectedDisplayName)
        {
            var locationReference = _locationQuery.ResolveLocation(country, location);

            // The unstructured location is not set.

            Assert.IsNull(locationReference.UnstructuredLocation);

            // The named location is an instance of the expected type.

            Assert.IsNotNull(locationReference.NamedLocation);
            Assert.IsInstanceOfType(locationReference.NamedLocation, expectedType);
            Assert.AreEqual(expectedDisplayName, locationReference.NamedLocation.Name);

            // The postal code and its locality are determined by the postal suburb.

            if (expectedType == typeof(PostalSuburb))
            {
                var postalSuburb = (PostalSuburb)locationReference.NamedLocation;
                Assert.AreEqual(postalSuburb, locationReference.PostalSuburb);
                Assert.AreEqual(postalSuburb.PostalCode, locationReference.PostalCode);
                Assert.AreEqual(postalSuburb.PostalCode.Locality, locationReference.Locality);
                Assert.IsNull(locationReference.Region);
                Assert.AreEqual(postalSuburb.CountrySubdivision, locationReference.CountrySubdivision);
                Assert.AreEqual(postalSuburb.CountrySubdivision.Country, locationReference.Country);
            }
            else if (expectedType == typeof(PostalCode))
            {
                var postalCode = (PostalCode)locationReference.NamedLocation;
                Assert.IsNull(locationReference.PostalSuburb);
                Assert.AreEqual(postalCode, locationReference.PostalCode);
                Assert.AreEqual(postalCode.Locality, locationReference.Locality);
                Assert.IsNull(locationReference.Region);
                var subdivision = postalCode.Locality.CountrySubdivisions.Count == 1 ? postalCode.Locality.CountrySubdivisions[0] : _locationQuery.ResolveLocation(postalCode.Locality.CountrySubdivisions[0].Country, null).CountrySubdivision;
                Assert.AreEqual(subdivision, locationReference.CountrySubdivision);
                Assert.AreEqual(subdivision.Country, locationReference.Country);
            }
            else if (expectedType == typeof(Locality))
            {
                var locality = (Locality)locationReference.NamedLocation;
                Assert.IsNull(locationReference.PostalSuburb);
                Assert.IsNull(locationReference.PostalCode);
                Assert.AreEqual(locality, locationReference.Locality);
                Assert.IsNull(locationReference.Region);
                var subdivision = locality.CountrySubdivisions.Count == 1 ? locality.CountrySubdivisions[0] : _locationQuery.ResolveLocation(locality.CountrySubdivisions[0].Country, null).CountrySubdivision;
                Assert.AreEqual(subdivision, locationReference.CountrySubdivision);
                Assert.AreEqual(subdivision.Country, locationReference.Country);
            }
            else if (expectedType == typeof(Region))
            {
                var region = (Region)locationReference.NamedLocation;
                Assert.IsNull(locationReference.PostalSuburb);
                Assert.IsNull(locationReference.PostalCode);
                Assert.IsNull(locationReference.Locality);
                Assert.AreEqual(region, locationReference.Region);
            }
            else
            {
                Assert.AreEqual(expectedType, typeof(CountrySubdivision));
                var subdivision = (CountrySubdivision)locationReference.NamedLocation;
                Assert.IsNull(locationReference.PostalSuburb);
                Assert.IsNull(locationReference.PostalCode);
                Assert.IsNull(locationReference.Locality);
                Assert.IsNull(locationReference.Region);
                Assert.AreEqual(subdivision, locationReference.CountrySubdivision);
                Assert.AreEqual(subdivision.Country, locationReference.Country);
            }

            return locationReference.ToString();
        }

        private string TryResolve(Country country, string location)
        {
            var locationReference = _locationQuery.ResolveLocation(country, location);

            // The unstructured location is equal to the location.

            Assert.IsNotNull(locationReference.UnstructuredLocation);
            Assert.AreEqual(location, locationReference.UnstructuredLocation);

            // The named location is the country subdivision.

            Assert.AreEqual(locationReference.NamedLocation, _locationQuery.ResolveLocation(country, null).CountrySubdivision);
            Assert.AreEqual(locationReference.CountrySubdivision, _locationQuery.ResolveLocation(country, null).CountrySubdivision);

            Assert.IsNull(locationReference.PostalSuburb);
            Assert.IsNull(locationReference.PostalCode);
            Assert.IsNull(locationReference.Locality);
            Assert.IsNull(locationReference.Region);

            return locationReference.ToString();
        }
    }
}

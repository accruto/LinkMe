using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Location
{
    [TestClass]
    public class SerializationTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = new LocationQuery(new LocationRepository(Resolve<IDataContextFactory>()));
        private Country _australia;

        [TestInitialize]
        public void SerializationTestsInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
        }

        [TestMethod]
        public void CanSerializeCountrySubdivision()
        {
            var stream = new MemoryStream();
            var locationIn = _locationQuery.ResolveLocation(_australia, "VIC");
            Serialize(locationIn, stream);
            var locationOut = Deserialize(stream);
            Assert.AreEqual(locationIn, locationOut);
        }

        [TestMethod]
        public void CanSerializeRegion()
        {
            var stream = new MemoryStream();
            var locationIn = _locationQuery.ResolveLocation(_australia, "Melbourne");
            Serialize(locationIn, stream);
            var locationOut = Deserialize(stream);
            Assert.AreEqual(locationIn, locationOut);
        }

        [TestMethod]
        public void CanSerializeLocality()
        {
            var stream = new MemoryStream();
            var locationIn = _locationQuery.ResolveLocation(_australia, "3124");
            Serialize(locationIn, stream);
            var locationOut = Deserialize(stream);
            Assert.AreEqual(locationIn, locationOut);
        }

        [TestMethod]
        public void CanSerializePostalCode()
        {
            var stream = new MemoryStream();
            var locationIn = _locationQuery.ResolveLocation(_australia, "0801");
            Serialize(locationIn, stream);
            var locationOut = Deserialize(stream);
            Assert.AreEqual(locationIn, locationOut);
        }

        [TestMethod]
        public void CanSerializePostalSuburb()
        {
            var stream = new MemoryStream();
            var locationIn = _locationQuery.ResolveLocation(_australia, "Armadale VIC 3143");
            Serialize(locationIn, stream);
            var locationOut = Deserialize(stream);
            Assert.AreEqual(locationIn, locationOut);
        }

        [TestMethod]
        public void CanSerializeCountry()
        {
            var stream = new MemoryStream();
            var locationIn = _locationQuery.ResolveLocation(_australia, null);
            Serialize(locationIn, stream);
            var locationOut = Deserialize(stream);
            Assert.AreEqual(locationIn, locationOut);
        }

        private static void Serialize(LocationReference location, Stream stream)
        {
            var serializer = new DataContractSerializer(typeof(LocationReference));
            var writer = XmlDictionaryWriter.CreateBinaryWriter(stream);
            serializer.WriteObject(writer, location);
            writer.Flush();
        }

        private static LocationReference Deserialize(Stream stream)
        {
            var serializer = new DataContractSerializer(typeof(LocationReference));
            stream.Position = 0;
            var reader = XmlDictionaryReader.CreateBinaryReader(stream, new XmlDictionaryReaderQuotas());
            var location = (LocationReference)serializer.ReadObject(reader);
            return location;
        }
    }
}
    
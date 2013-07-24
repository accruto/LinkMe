using System;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Recruiters
{
    [TestClass]
    public class OrganisationAddressTests
        : OrganisationsTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private const string Line1 = "Apartment 111";
        private const string OtherLine1 = "Apartment 222";
        private const string Line2 = "300 Bourke St";
        private const string Location = "Melbourne VIC 3000";
        private const string OtherLocation = "Sydney NSW 2000";
        private const string Name = "Company";

        [TestMethod]
        public void TestCreate()
        {
            // Create.

            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), Location);
            var organisation = new Organisation { Name = Name, Address = new Address {Line1 = Line1, Line2 = Line2, Location = location} };
            _organisationsCommand.CreateOrganisation(organisation);

            Assert.AreNotEqual(Guid.Empty, organisation.Address.Id);
            Assert.AreNotEqual(Guid.Empty, organisation.Address.Location.Id);

            // Assert.

            organisation = _organisationsQuery.GetOrganisation(organisation.Id);
            Assert.AreEqual(Line1, organisation.Address.Line1);
            Assert.AreEqual(Line2, organisation.Address.Line2);
            Assert.AreEqual(location.Country.Id, organisation.Address.Location.Country.Id);
            Assert.AreEqual(location.CountrySubdivision.Id, organisation.Address.Location.CountrySubdivision.Id);
            Assert.AreEqual(location.NamedLocation.Id, organisation.Address.Location.NamedLocation.Id);
            Assert.AreEqual(location.Locality.Id, organisation.Address.Location.Locality.Id);
        }

        [TestMethod]
        public void TestUpdateAddress()
        {
            // Create.

            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), Location);
            var organisation = new Organisation { Name = Name, Address = new Address { Line1 = Line1, Line2 = Line2, Location = location } };
            _organisationsCommand.CreateOrganisation(organisation);

            // Update.

            organisation = _organisationsQuery.GetOrganisation(organisation.Id);
            organisation.Address.Line1 = OtherLine1;
            location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), OtherLocation);
            organisation.Address.Location = location;
            _organisationsCommand.UpdateOrganisation(organisation);

            // Assert.

            organisation = _organisationsQuery.GetOrganisation(organisation.Id);
            Assert.AreEqual(OtherLine1, organisation.Address.Line1);
            Assert.AreEqual(Line2, organisation.Address.Line2);
            Assert.AreEqual(location.Country.Id, organisation.Address.Location.Country.Id);
            Assert.AreEqual(location.CountrySubdivision.Id, organisation.Address.Location.CountrySubdivision.Id);
            Assert.AreEqual(location.NamedLocation.Id, organisation.Address.Location.NamedLocation.Id);
            Assert.AreEqual(location.Locality.Id, organisation.Address.Location.Locality.Id);
        }

        [TestMethod]
        public void TestUpdateLocation()
        {
            // Create.

            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), Location);
            var organisation = new Organisation { Name = Name, Address = new Address { Line1 = Line1, Line2 = Line2, Location = location } };
            _organisationsCommand.CreateOrganisation(organisation);

            // Update.

            organisation = _organisationsQuery.GetOrganisation(organisation.Id);
            location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), OtherLocation);
            organisation.Address.Location = location;
            _organisationsCommand.UpdateOrganisation(organisation);

            // Assert.

            organisation = _organisationsQuery.GetOrganisation(organisation.Id);
            Assert.AreEqual(Line1, organisation.Address.Line1);
            Assert.AreEqual(Line2, organisation.Address.Line2);
            Assert.AreEqual(location.Country.Id, organisation.Address.Location.Country.Id);
            Assert.AreEqual(location.CountrySubdivision.Id, organisation.Address.Location.CountrySubdivision.Id);
            Assert.AreEqual(location.NamedLocation.Id, organisation.Address.Location.NamedLocation.Id);
            Assert.AreEqual(location.Locality.Id, organisation.Address.Location.Locality.Id);
        }

        [TestMethod]
        public void TestDeleteAddress()
        {
            // Create.

            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), Location);
            var organisation = new Organisation { Name = Name, Address = new Address { Line1 = Line1, Line2 = Line2, Location = location } };
            _organisationsCommand.CreateOrganisation(organisation);

            // Update.

            organisation = _organisationsQuery.GetOrganisation(organisation.Id);
            organisation.Address = null;
            _organisationsCommand.UpdateOrganisation(organisation);

            // Assert.

            organisation = _organisationsQuery.GetOrganisation(organisation.Id);
            Assert.IsNull(organisation.Address);
        }

        [TestMethod]
        public void TestCreateAddress()
        {
            // Create.

            var organisation = new Organisation { Name = Name };
            _organisationsCommand.CreateOrganisation(organisation);

            // Assert.

            organisation = _organisationsQuery.GetOrganisation(organisation.Id);
            Assert.IsNull(organisation.Address);

            // Update.

            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), OtherLocation);
            organisation.Address = new Address { Line1 = Line1, Line2 = Line2, Location = location };
            _organisationsCommand.UpdateOrganisation(organisation);

            // Assert.

            organisation = _organisationsQuery.GetOrganisation(organisation.Id);
            Assert.AreEqual(Line1, organisation.Address.Line1);
            Assert.AreEqual(Line2, organisation.Address.Line2);
            Assert.AreEqual(location.Country.Id, organisation.Address.Location.Country.Id);
            Assert.AreEqual(location.CountrySubdivision.Id, organisation.Address.Location.CountrySubdivision.Id);
            Assert.AreEqual(location.NamedLocation.Id, organisation.Address.Location.NamedLocation.Id);
            Assert.AreEqual(location.Locality.Id, organisation.Address.Location.Locality.Id);
        }
    }
}
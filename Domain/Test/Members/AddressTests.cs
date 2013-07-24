using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Members
{
    [TestClass]
    public class AddressTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private Country _australia;
        private Country _notAustralia;

        [TestInitialize]
        public void AddressTestsInitialize()
        {
            _australia = _locationQuery.GetCountry("australia");
            _notAustralia = _locationQuery.GetCountry("new zealand");
        }

        [TestMethod]
        public void TestNullAddress()
        {
            var member = CreateMember();

            try
            {
                member.Validate();
            }
            catch (ValidationErrorsException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestValidAustralianAddress()
        {
            var member = CreateMember();
            member.Address = new Address { Location = _locationQuery.ResolveLocation(_australia, "Sydney 2000 NSW") };

            try
            {
                member.Validate();
            }
            catch (ValidationErrorsException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestInvalidAustralianAddress()
        {
            var member = CreateMember();

            member.Address = new Address
            {
                Location = _locationQuery.ResolveLocation(_australia, "City of Sails 2000 NSW")
            };

            try
            {
                member.Validate();
            }
            catch (ValidationErrorsException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count);
                Assert.IsInstanceOfType(ex.Errors[0], typeof(PostalSuburbValidationError));
            }
        }

        [TestMethod]
        public void TestNonAustralianAddres()
        {
            var member = CreateMember();

            member.Address = new Address
            {
                Location = _locationQuery.ResolveLocation(_notAustralia, "Auckland")
            };

            try
            {
                member.Validate();
            }
            catch (ValidationErrorsException)
            {
                Assert.Fail();
            }
        }

        private static Member CreateMember()
        {
            return new Member
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = "john@doefamily.com" } }
            };
        }
    }
}

using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Recruiters
{
    [TestClass]
    public class VerifyOrganisationTests
        : OrganisationsTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private const string Line1 = "Apartment 111";
        private const string Line2 = "300 Bourke St";
        private const string Location = "Melbourne VIC 3000";

        [TestMethod]
        public void TestVerifyOrganisation()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(1);
            var accountManagerId = Guid.NewGuid();
            var verifiedById = Guid.NewGuid();
            var verifiedOrganisation = _organisationsCommand.VerifyOrganisation(organisation, accountManagerId, verifiedById);

            AssertVerifiedOrganisation(organisation, accountManagerId, verifiedById, verifiedOrganisation);
            AssertVerifiedOrganisation(organisation, accountManagerId, verifiedById, _organisationsQuery.GetOrganisation(organisation.Id));
            AssertVerifiedOrganisation(organisation, accountManagerId, verifiedById, _organisationsQuery.GetVerifiedOrganisation(organisation.Name));
        }

        [TestMethod]
        public void TestVerifyOrganisationWithAddress()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(1);
            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), Location);
            organisation.Address = new Address {Line1 = Line1, Line2 = Line2, Location = location};
            _organisationsCommand.UpdateOrganisation(organisation);

            var accountManagerId = Guid.NewGuid();
            var verifiedById = Guid.NewGuid();
            var verifiedOrganisation = _organisationsCommand.VerifyOrganisation(organisation, accountManagerId, verifiedById);

            AssertVerifiedOrganisation(organisation, accountManagerId, verifiedById, verifiedOrganisation);
            AssertVerifiedOrganisation(organisation, accountManagerId, verifiedById, _organisationsQuery.GetOrganisation(organisation.Id));
            AssertVerifiedOrganisation(organisation, accountManagerId, verifiedById, _organisationsQuery.GetVerifiedOrganisation(organisation.Name));
        }

        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestVerifyOrganisationNameExists()
        {
            // Create existing organisation.

            _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());

            // Verify with same name.

            var organisation = _organisationsCommand.CreateTestOrganisation(1);
            var accountManagerId = Guid.NewGuid();
            var verifiedById = Guid.NewGuid();
            _organisationsCommand.VerifyOrganisation(organisation, accountManagerId, verifiedById);
        }

        private static void AssertVerifiedOrganisation(IOrganisation originalOrganisation, Guid accountManagerId, Guid verifiedById, Organisation organisation)
        {
            Assert.IsTrue(organisation.IsVerified);
            var verifiedOrganisation = (VerifiedOrganisation) organisation;
            Assert.AreEqual(originalOrganisation.Id, verifiedOrganisation.Id);
            Assert.AreEqual(originalOrganisation.Name, verifiedOrganisation.Name);
            Assert.AreEqual(originalOrganisation.FullName, verifiedOrganisation.FullName);
            Assert.IsNull(verifiedOrganisation.ParentId);
            Assert.IsNull(verifiedOrganisation.ParentFullName);
            Assert.AreEqual(originalOrganisation.AffiliateId, verifiedOrganisation.AffiliateId);
            Assert.AreEqual(originalOrganisation.Address, verifiedOrganisation.Address);
            Assert.AreEqual(accountManagerId, verifiedOrganisation.AccountManagerId);
            Assert.AreEqual(verifiedById, verifiedOrganisation.VerifiedById);
            Assert.IsNull(verifiedOrganisation.ContactDetails);
        }
    }
}

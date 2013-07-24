using System;
using LinkMe.Domain.Roles.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Recruiters.Affiliations
{
    [TestClass]
    public class SetOrganisationAffiliationsTests
        : AffiliationTests
    {
        private const string Name = "Company";

        [TestMethod]
        public void TestUnverifiedAddAffiliate()
        {
            TestAddAffiliate(new Organisation { Name = Name });
        }

        [TestMethod]
        public void TestVerifiedAddAffiliate()
        {
            TestAddAffiliate(new VerifiedOrganisation { Name = Name });
        }

        [TestMethod]
        public void TestUnverifiedRemoveAffiliate()
        {
            TestRemoveAffiliate(new Organisation { Name = Name });
        }

        [TestMethod]
        public void TestVerifiedRemoveAffiliate()
        {
            TestRemoveAffiliate(new VerifiedOrganisation { Name = Name });
        }

        [TestMethod]
        public void TestUnverifiedUpdateAffiliate()
        {
            TestUpdateAffiliate(new Organisation { Name = Name });
        }

        [TestMethod]
        public void TestVerifiedUpdateAffiliate()
        {
            TestUpdateAffiliate(new VerifiedOrganisation { Name = Name });
        }

        private void TestAddAffiliate(Organisation organisation)
        {
            _organisationsCommand.CreateOrganisation(organisation);
            AssertOrganisation(null, organisation);

            // Add it.

            organisation.AffiliateId = Guid.NewGuid();
            _organisationsCommand.UpdateOrganisation(organisation);
            AssertOrganisation(organisation.AffiliateId, organisation);
        }

        private void TestRemoveAffiliate(Organisation organisation)
        {
            // With affiliate.

            organisation.AffiliateId = Guid.NewGuid();
            _organisationsCommand.CreateOrganisation(organisation);
            AssertOrganisation(organisation.AffiliateId, organisation);

            // Remove it.

            organisation.AffiliateId = null;
            _organisationsCommand.UpdateOrganisation(organisation);
            AssertOrganisation(null, organisation);
        }

        private void TestUpdateAffiliate(Organisation organisation)
        {
            // With affiliate.

            organisation.AffiliateId = Guid.NewGuid();
            _organisationsCommand.CreateOrganisation(organisation);
            AssertOrganisation(organisation.AffiliateId, organisation);

            // Update it.

            organisation.AffiliateId = Guid.NewGuid();
            _organisationsCommand.UpdateOrganisation(organisation);
            AssertOrganisation(organisation.AffiliateId, organisation);
        }

        private void AssertOrganisation(Guid? affiliateId, Organisation organisation)
        {
            Assert.AreEqual(affiliateId, _organisationsQuery.GetOrganisation(organisation.Id).AffiliateId);
            Assert.AreEqual(affiliateId, _organisationsQuery.GetOrganisations(new[] {organisation.Id})[0].AffiliateId);

            if (organisation.IsVerified)
            {
                Assert.AreEqual(affiliateId, _organisationsQuery.GetOrganisation(organisation.ParentId, organisation.Name).AffiliateId);
                Assert.AreEqual(affiliateId, _organisationsQuery.GetVerifiedOrganisation(organisation.FullName).AffiliateId);
                Assert.AreEqual(affiliateId, _organisationsQuery.GetOrganisationHierarchy(organisation.Id).Organisation.AffiliateId);
            }
        }
    }
}
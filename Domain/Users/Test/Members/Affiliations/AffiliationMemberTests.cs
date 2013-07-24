using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Affiliations
{
    [TestClass]
    public class AffiliationMemberTests
        : AffiliationTests
    {
        [TestMethod]
        public void TestAddAffiliation()
        {
            var affiliateId = Guid.NewGuid();

            // No affiliate.

            var memberId = Guid.NewGuid();
            _memberAffiliationsCommand.SetAffiliation(memberId, null);
            Assert.AreEqual(null, _memberAffiliationsQuery.GetAffiliateId(memberId));

            // Add it.

            _memberAffiliationsCommand.SetAffiliation(memberId, affiliateId);
            Assert.AreEqual(affiliateId, _memberAffiliationsQuery.GetAffiliateId(memberId));
        }

        [TestMethod]
        public void TestRemoveAffiliation()
        {
            var affiliateId = Guid.NewGuid();

            // With affiliate.

            var memberId = Guid.NewGuid();
            _memberAffiliationsCommand.SetAffiliation(memberId, affiliateId);
            Assert.AreEqual(affiliateId, _memberAffiliationsQuery.GetAffiliateId(memberId));

            // Remove it.

            _memberAffiliationsCommand.SetAffiliation(memberId, null);
            Assert.AreEqual(null, _memberAffiliationsQuery.GetAffiliateId(memberId));
        }

        [TestMethod]
        public void TestUpdateAffiliation()
        {
            var affiliateId1 = Guid.NewGuid();
            var affiliateId2 = Guid.NewGuid();

            // With affiliate.

            var memberId = Guid.NewGuid();
            _memberAffiliationsCommand.SetAffiliation(memberId, affiliateId1);
            Assert.AreEqual(affiliateId1, _memberAffiliationsQuery.GetAffiliateId(memberId));

            // Update it.

            _memberAffiliationsCommand.SetAffiliation(memberId, affiliateId2);
            Assert.AreEqual(affiliateId2, _memberAffiliationsQuery.GetAffiliateId(memberId));
        }
    }
}
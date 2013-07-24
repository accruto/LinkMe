using System;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Users.Members.Affiliations.Affiliates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Affiliations
{
    [TestClass]
    public class AffiliationDataItemTests
        : AffiliationTests
    {
        private static readonly Guid AimeId = new Guid("7088F0A9-E627-4D72-AA06-2305846EA5D1");
        private static readonly Guid ItcraLinkId = new Guid("6F8E9378-D3C8-416D-A05F-319BA4A10EDA");

        [TestMethod]
        public void TestSetItems()
        {
            // Create the member.

            var affiliateId = ItcraLinkId;
            var memberId = Guid.NewGuid();
            _memberAffiliationsCommand.SetAffiliation(memberId, affiliateId);

            // Set some items.

            var items = new ItcraLinkAffiliationItems {Status = ItcraLinkMemberStatus.Certified, MemberId = "ABCD1234"};

            // Test.

            _memberAffiliationsCommand.SetItems(memberId, affiliateId, items);
            AssertItems(items, _memberAffiliationsQuery.GetItems(memberId, affiliateId));
        }

        [TestMethod]
        public void TestSetEmptyItems()
        {
            // Create the member.

            var affiliateId = ItcraLinkId;
            var memberId = Guid.NewGuid();
            _memberAffiliationsCommand.SetAffiliation(memberId, affiliateId);

            // Set some items.

            var items = new ItcraLinkAffiliationItems();

            // Test.

            _memberAffiliationsCommand.SetItems(memberId, affiliateId, items);
            AssertItems(null, _memberAffiliationsQuery.GetItems(memberId, affiliateId));
        }

        [TestMethod]
        public void TestSetItemsAgain()
        {
            // Create the member.

            var affiliateId = ItcraLinkId;
            var memberId = Guid.NewGuid();
            _memberAffiliationsCommand.SetAffiliation(memberId, affiliateId);

            // Set some items.

            var items = new ItcraLinkAffiliationItems { Status = ItcraLinkMemberStatus.Certified, MemberId = "ABCD1234" };

            // Test.

            _memberAffiliationsCommand.SetItems(memberId, affiliateId, items);
            AssertItems(items, _memberAffiliationsQuery.GetItems(memberId, affiliateId));

            // Set again.

            items = new ItcraLinkAffiliationItems { Status = ItcraLinkMemberStatus.ProfessionalMember, MemberId = "EFGH5678" };

            _memberAffiliationsCommand.SetItems(memberId, affiliateId, items);
            AssertItems(items, _memberAffiliationsQuery.GetItems(memberId, affiliateId));
        }

        [TestMethod]
        public void TestSetItemsMultipleMembers()
        {
            // Create the member.

            var affiliateId = ItcraLinkId;
            var memberId1 = Guid.NewGuid();
            _memberAffiliationsCommand.SetAffiliation(memberId1, affiliateId);
            var memberId2 = Guid.NewGuid();
            _memberAffiliationsCommand.SetAffiliation(memberId2, affiliateId);

            // Set some items.

            var items1 = new ItcraLinkAffiliationItems { Status = ItcraLinkMemberStatus.Certified, MemberId = "ABCD1234" };

            _memberAffiliationsCommand.SetItems(memberId1, affiliateId, items1);
            AssertItems(items1, _memberAffiliationsQuery.GetItems(memberId1, affiliateId));
            AssertItems(null, _memberAffiliationsQuery.GetItems(memberId2, affiliateId));

            var items2 = new ItcraLinkAffiliationItems { Status = ItcraLinkMemberStatus.ProfessionalMember, MemberId = "EFGH5678" };

            _memberAffiliationsCommand.SetItems(memberId2, affiliateId, items2);
            AssertItems(items1, _memberAffiliationsQuery.GetItems(memberId1, affiliateId));
            AssertItems(items2, _memberAffiliationsQuery.GetItems(memberId2, affiliateId));
        }

        [TestMethod]
        public void TestSetItemsMultipleAffiliates()
        {
            // Create the member.

            var affiliateId1 = ItcraLinkId;
            var affiliateId2 = AimeId;

            var memberId = Guid.NewGuid();
            _memberAffiliationsCommand.SetAffiliation(memberId, affiliateId1);

            // Set some items.

            var items1 = new ItcraLinkAffiliationItems { Status = ItcraLinkMemberStatus.Certified, MemberId = "ABCD1234" };

            _memberAffiliationsCommand.SetItems(memberId, affiliateId1, items1);
            AssertItems(items1, _memberAffiliationsQuery.GetItems(memberId, affiliateId1));
            AssertItems(null, _memberAffiliationsQuery.GetItems(memberId, affiliateId2));

            var items2 = new AimeAffiliationItems { Status = AimeMemberStatus.BecomeMentor };

            _memberAffiliationsCommand.SetItems(memberId, affiliateId2, items2);
            AssertItems(items1, _memberAffiliationsQuery.GetItems(memberId, affiliateId1));
            AssertItems(items2, _memberAffiliationsQuery.GetItems(memberId, affiliateId2));
        }

        private static void AssertItems(AffiliationItems expected, AffiliationItems actual)
        {
            if (expected == null)
            {
                Assert.IsNull(actual);
            }
            else
            {
                Assert.IsInstanceOfType(actual, expected.GetType());

                if (expected is FinsiaAffiliationItems)
                {
                    Assert.AreEqual(((FinsiaAffiliationItems)expected).MemberId, ((FinsiaAffiliationItems)actual).MemberId);
                }
                else if (expected is AimeAffiliationItems)
                {
                    Assert.AreEqual(((AimeAffiliationItems)expected).Status, ((AimeAffiliationItems)actual).Status);
                }
                else if (expected is ItcraLinkAffiliationItems)
                {
                    Assert.AreEqual(((ItcraLinkAffiliationItems)expected).MemberId, ((ItcraLinkAffiliationItems)actual).MemberId);
                    Assert.AreEqual(((ItcraLinkAffiliationItems)expected).Status, ((ItcraLinkAffiliationItems)actual).Status);
                }
            }
        }
    }
}
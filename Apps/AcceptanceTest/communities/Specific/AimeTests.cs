using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Users.Members.Affiliations.Affiliates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.Specific
{
    [TestClass]
    public class AimeTests
        : SpecificCommunityTests
    {
        private HtmlDropDownListTester _aimeMemberStatusDropDownList;

        [TestInitialize]
        public void TestInitialize()
        {
            _aimeMemberStatusDropDownList = new HtmlDropDownListTester(Browser, "AimeMemberStatus");
        }

        protected override TestCommunity GetTestCommunity()
        {
            return TestCommunity.Aime;
        }

        [TestMethod]
        public void TestAimeJoinWithoutStatus()
        {
            TestMemberJoin(JoinPageJoin, false, FillPersonalDetails, AssertPersonalDetails, null);
        }

        [TestMethod]
        public void TestAimeJoinWithStatus()
        {
            TestMemberJoin(JoinPageJoin, false, FillPersonalDetails, AssertPersonalDetails, new AimeAffiliationItems { Status = AimeMemberStatus.CurrentMentor });
        }

        private void FillPersonalDetails(CommunityTestData communityTestData, AffiliationItems personalDetailsItems)
        {
            AssertPageContains("AIME Status");
            Assert.IsTrue(_aimeMemberStatusDropDownList.IsVisible);

            var aimeAffiliationItems = personalDetailsItems as AimeAffiliationItems;
            if (aimeAffiliationItems != null)
            {
                var status = aimeAffiliationItems.Status;
                if (status != null)
                    _aimeMemberStatusDropDownList.SelectedValue = status.ToString();
            }
        }

        private void AssertPersonalDetails(CommunityTestData communityTestData, AffiliationItems personalDetailsItems)
        {
            // Check the saved data.

            var member = _membersQuery.GetMember(MemberEmailAddress);
            var items = _memberAffiliationsQuery.GetItems(member.Id, communityTestData.Id.Value);

            if (personalDetailsItems == null)
            {
                Assert.IsNull(items);
            }
            else
            {
                Assert.IsNotNull(items);
                Assert.IsInstanceOfType(personalDetailsItems, typeof(AimeAffiliationItems));
                Assert.IsInstanceOfType(items, typeof(AimeAffiliationItems));
                Assert.AreEqual(((AimeAffiliationItems)personalDetailsItems).Status, ((AimeAffiliationItems)items).Status);
            }
        }
    }
}

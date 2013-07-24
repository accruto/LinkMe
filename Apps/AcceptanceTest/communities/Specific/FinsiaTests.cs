using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Users.Members.Affiliations.Affiliates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.Specific
{
    [TestClass]
    public class FinsiaTests
        : SpecificCommunityTests
    {
        private HtmlTextBoxTester _memberIdTextBox;

        [TestInitialize]
        public void TestInitialize()
        {
            _memberIdTextBox = new HtmlTextBoxTester(Browser, "FinsiaMemberId");
        }

        protected override TestCommunity GetTestCommunity()
        {
            return TestCommunity.Finsia;
        }

        [TestMethod]
        public void TestFinsiaMemberJoin()
        {
            TestMemberJoin(JoinPageJoin, false, FillPersonalDetails, AssertPersonalDetails, null);
        }

        [TestMethod]
        public void TestFinsiaMemberJoinWithMemberId()
        {
            TestMemberJoin(JoinPageJoin, false, FillPersonalDetails, AssertPersonalDetails, new FinsiaAffiliationItems { MemberId = "1234567890" });
        }

        private void FillPersonalDetails(CommunityTestData data, AffiliationItems personalDetailsItems)
        {
            Assert.IsTrue(_memberIdTextBox.IsVisible);

            var items = personalDetailsItems as FinsiaAffiliationItems;
            if (items != null)
                _memberIdTextBox.Text = items.MemberId;
        }

        private void AssertPersonalDetails(CommunityTestData data, AffiliationItems personalDetailsItems)
        {
            // Check the saved data.

            var member = _membersQuery.GetMember(MemberEmailAddress);
            var items = _memberAffiliationsQuery.GetItems(member.Id, data.Id.Value) as FinsiaAffiliationItems;

            if (personalDetailsItems == null)
            {
                Assert.IsNull(items);
            }
            else
            {
                Assert.IsNotNull(items);
                Assert.IsInstanceOfType(personalDetailsItems, typeof(FinsiaAffiliationItems));
                Assert.AreEqual(((FinsiaAffiliationItems)personalDetailsItems).MemberId, items.MemberId);
            }
        }
    }
}

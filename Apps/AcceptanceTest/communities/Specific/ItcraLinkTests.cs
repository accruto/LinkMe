using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Users.Members.Affiliations.Affiliates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.Specific
{
    [TestClass]
    public class ItcraLinkTests
        : SpecificCommunityTests
    {
        private const string MemberId = "ABCD";
        private HtmlRadioButtonTester _certifiedRadioButton;
        private HtmlRadioButtonTester _professionalRadioButton;
        private HtmlTextBoxTester _memberIdTextBox;

        protected override TestCommunity GetTestCommunity()
        {
            return TestCommunity.ItcraLink;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _certifiedRadioButton = new HtmlRadioButtonTester(Browser, "ItcraLinkCertified");
            _professionalRadioButton = new HtmlRadioButtonTester(Browser, "ItcraLinkProfessionalMember");
            _memberIdTextBox = new HtmlTextBoxTester(Browser, "ItcraLinkMemberId");
        }

        [TestMethod]
        public void TestJoinWithoutStatus()
        {
            TestMemberJoin(JoinPageJoin, false, FillPersonalDetails, AssertPersonalDetails, null);
        }

        [TestMethod]
        public void TestJoinWithCertifiedStatus()
        {
            TestMemberJoin(JoinPageJoin, false, FillPersonalDetails, AssertPersonalDetails, new ItcraLinkAffiliationItems { Status = ItcraLinkMemberStatus.Certified });
        }

        [TestMethod]
        public void TestJoinWithProfessionalStatus()
        {
            TestMemberJoin(JoinPageJoin, false, FillPersonalDetails, AssertPersonalDetails, new ItcraLinkAffiliationItems { Status = ItcraLinkMemberStatus.ProfessionalMember });
        }

        [TestMethod]
        public void TestJoinWithMemberId()
        {
            TestMemberJoin(JoinPageJoin, false, FillPersonalDetails, AssertPersonalDetails, new ItcraLinkAffiliationItems { MemberId = MemberId });
        }

        private void FillPersonalDetails(CommunityTestData communityTestData, AffiliationItems personalDetailsItems)
        {
            AssertPageContains("ITCRA Status");
            Assert.IsTrue(_certifiedRadioButton.IsVisible);
            Assert.IsTrue(_professionalRadioButton.IsVisible);
            Assert.IsTrue(_memberIdTextBox.IsVisible);

            var items = personalDetailsItems as ItcraLinkAffiliationItems;
            if (items != null)
            {
                if (items.Status != null)
                {
                    if (items.Status.Value == ItcraLinkMemberStatus.Certified)
                        _certifiedRadioButton.IsChecked = true;
                    else
                        _professionalRadioButton.IsChecked = true;
                }

                _memberIdTextBox.Text = items.MemberId;
            }
        }

        private void AssertPersonalDetails(CommunityTestData communityTestData, AffiliationItems personalDetailsItems)
        {
            // Check the saved data.

            var member = _membersQuery.GetMember(MemberEmailAddress);
            var items = _memberAffiliationsQuery.GetItems(member.Id, communityTestData.Id.Value) as ItcraLinkAffiliationItems;

            if (personalDetailsItems == null)
            {
                Assert.IsNull(items);
            }
            else
            {
                Assert.IsNotNull(items);
                Assert.IsInstanceOfType(personalDetailsItems, typeof(ItcraLinkAffiliationItems));
                Assert.AreEqual(((ItcraLinkAffiliationItems) personalDetailsItems).Status, items.Status);
                Assert.AreEqual(((ItcraLinkAffiliationItems) personalDetailsItems).MemberId, items.MemberId);
            }
        }
    }
}

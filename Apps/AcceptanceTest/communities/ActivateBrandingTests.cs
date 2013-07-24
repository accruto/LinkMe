using System.Net;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Verticals;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.UI.Registered.Networkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities
{
    [TestClass]
    public class ActivateBrandingTests
        : CommunityTests
    {
        private const string EmailAddress = "member@test.linkme.net.au";

        private HtmlCheckBoxTester _communityCheckBox;
        private HtmlButtonTester _saveButton;

        protected ReadOnlyUrl _searchUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();

            _communityCheckBox = new HtmlCheckBoxTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_ucEmployerPrivacy_chkCommunity", false);
            _saveButton = new HtmlButtonTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_btnSave");

            _searchUrl = new ReadOnlyApplicationUrl("~/search/candidates");
        }

        [TestMethod]
        public void BrandVisiblityTest()
        {
            var data = TestCommunity.MonashGsb.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create member not associated with the community.

            var member = CreateMember();
            AssertCanSetBrand(member, data);

            // Associate them with the community.

            _memberAffiliationsCommand.SetAffiliation(member.Id, community.Id);
            member.AffiliateId = community.Id;
            AssertCanSetBrand(member, data);
        }

        [TestMethod]
        public void SetBrandFlagTest()
        {
            var data = TestCommunity.MonashGsb.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create member.

            var member = CreateMember();
            AssertSearch(data, false);

            // Associate them with the community.

            _memberAffiliationsCommand.SetAffiliation(member.Id, community.Id);
            member.AffiliateId = community.Id;
            AssertSearch(data, true);

            // Turn off the branding.

            SetBrand(ref member, false);
            AssertSearch(data, false);

            // Turn it back on again.

            SetBrand(ref member, true);
            AssertSearch(data, true);
        }

        private void SetBrand(ref Member member, bool setBrand)
        {
            // Login and go to the visibility page.

            LogIn(member);
            GetPage<VisibilitySettingsBasic>();

            // Check the current state.

            Assert.IsTrue(_communityCheckBox.IsVisible);
            if (setBrand)
                Assert.IsTrue(!_communityCheckBox.IsChecked);
            else
                Assert.IsTrue(_communityCheckBox.IsChecked);

            // Set and save.

            _communityCheckBox.IsChecked = setBrand;
            _saveButton.Click();

            // Log out and reset the member.

            LogOut();
            member = _membersQuery.GetMember(member.GetLoginId());

            if (setBrand)
                Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Communities));
            else
                Assert.IsTrue(!member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Communities));
        }

        private void AssertCanSetBrand(IRegisteredUser member, VerticalTestData data)
        {
            // Login and go to the visibility page.

            LogIn(member);
            GetPage<VisibilitySettingsBasic>();

            // The style atribute is set on the containing div so the control will always be "visible", ie contained within the page.

            Assert.AreEqual(_communityCheckBox.IsVisible, member.AffiliateId != null);

            if (member.AffiliateId != null)
            {
                // Should be able to see the community controls.

                AssertPageContains("You joined through " + data.Name);
                AssertPageContains(new ApplicationUrl(data.CandidateImageUrl).PathAndQuery);
            }
            else
            {
                AssertPageDoesNotContain(data.Name);
                AssertPageDoesNotContain(new ApplicationUrl(data.CandidateImageUrl).PathAndQuery);
            }

            // Log out.

            LogOut();
        }

        private Member CreateMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(EmailAddress);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _memberSearchService.UpdateMember(member.Id);
            return member;
        }

        private void AssertSearch(VerticalTestData data, bool expectBrand)
        {
            // Make sure non remnent of association with the community is present for the search.

            Browser.Cookies = new CookieContainer();

            // Search.

            var searchUrl = _searchUrl.AsNonReadOnly();
            searchUrl.QueryString["JobTitle"] = "business analyst";
            Get(searchUrl);

            // Assert that the member is returned.

            AssertPageContains("Results <span class=\"start\">1</span> - <span class=\"end\">1</span> of <span class=\"total\">1</span>");
            AssertBrand(data, expectBrand);
        }

        private void AssertBrand(VerticalTestData data, bool expectBrand)
        {
            // Look for the header if appropriate.

            var imageUrl = new ApplicationUrl(data.CandidateImageUrl);
            if (expectBrand)
                AssertPageContains(imageUrl.FileName);
            else
                AssertPageDoesNotContain(imageUrl.FileName);
        }
    }
}

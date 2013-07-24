using System;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities
{
    [TestClass]
    public class MemberLoginTests
        : CommunityTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCommunityMemberLoginCommunitySite()
        {
            // Create a community.

            CommunityTestData data = TestCommunity.Scouts.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create a member for that community.

            Member member = CreateMember(community, 0);

            // Go to the community home page.

            Get(GetCommunityUrl(community, ""));
            AssertUrl(HomeUrl);
            AssertHeader(data.HeaderSnippet);

            // Login.

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertHeader(data.HeaderSnippet);

            // Logout.

            LogOut();
            AssertUrl(HomeUrl);
            AssertHeader(data.HeaderSnippet);
        }

        [TestMethod]
        public void TestOtherCommunityMemberLoginCommunitySite()
        {
            // Create a community.

            CommunityTestData data = TestCommunity.Scouts.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create a member for another community.

            Community otherCommunity = TestCommunity.Rcsa.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            Member member = CreateMember(otherCommunity, 0);

            // Go to the community home page - branding should remain the entire way through.

            Get(GetCommunityUrl(community, ""));
            AssertUrl(HomeUrl);
            AssertHeader(data.HeaderSnippet);

            // Login.

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertHeader(data.HeaderSnippet);

            // Logout.

            LogOut();
            AssertUrl(HomeUrl);
            AssertHeader(data.HeaderSnippet);
        }

        [TestMethod]
        public void TestNonCommunityMemberLoginCommunitySite()
        {
            // Create a community.

            CommunityTestData data = TestCommunity.Scouts.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create a member with no community.

            Member member = CreateMember(null, 0);

            // Go to the community home page - branding should remain the entire way through.

            Get(GetCommunityUrl(community, ""));
            AssertUrl(HomeUrl);
            AssertHeader(data.HeaderSnippet);

            // Login.

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertHeader(data.HeaderSnippet);

            // Logout.

            LogOut();
            AssertUrl(HomeUrl);
            AssertHeader(data.HeaderSnippet);
        }

        [TestMethod]
        public void TestCommunityMemberLoginNonCommunitySite()
        {
            // Create a community.

            CommunityTestData firstData = TestCommunity.Scouts.GetCommunityTestData();
            var community = firstData.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create a member for the community.

            Member member = CreateMember(community, 0);

            // Go to the home page - branding should only appear when the user logs in.

            Get(HomeUrl);
            AssertUrl(HomeUrl);
            AssertNoHeader(firstData.HeaderSnippet);

            // Login.

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertHeader(firstData.HeaderSnippet);

            // Logout, branding should stay.

            LogOut();
            AssertUrl(HomeUrl);
            AssertHeader(firstData.HeaderSnippet);

            // Create another community and member.

            const TestCommunity secondData = TestCommunity.Rcsa;
            community = secondData.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            member = CreateMember(community, 1);

            // Go to the home page - branding should not change.

            Get(HomeUrl);
            AssertUrl(HomeUrl);
            AssertHeader(firstData.HeaderSnippet);

            // Login.

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertHeader(firstData.HeaderSnippet);

            // Logout, branding should go away.

            LogOut();
            AssertUrl(HomeUrl);
            AssertHeader(firstData.HeaderSnippet);
        }

        private Member CreateMember(Community community, int index)
        {
            return _memberAccountsCommand.CreateTestMember(index, community != null ? community.Id : (Guid?) null);
        }
    }
}

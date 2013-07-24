using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Communities
{
    [TestClass]
    public class CommunitySiteSearchTests
        : CommunityTests
    {
        private class Communities
        {
            private readonly IList<Community> _communities = new List<Community>();
            private readonly IList<Member[]> _members = new List<Member[]>();
            private Member[] _noCommunityMembers = new Member[0];
            private Member[] _allMembers = new Member[0];

            public void Add(Community community, Member[] members)
            {
                _communities.Add(community);
                _members.Add(members);
                _allMembers = _allMembers.Concat(members).ToArray();
            }

            public Member[] AllMembers
            {
                get { return _allMembers; }
            }

            public IList<Member[]> Members
            {
                get { return _members; }
            }

            public Member[] NoCommunityMembers
            {
                set
                {
                    _noCommunityMembers = value;
                    _allMembers = _allMembers.Concat(_noCommunityMembers).ToArray();
                }
            }

            public Community this[int index]
            {
                get { return _communities[index]; }
            }
        }

        [TestMethod]
        public void TestAnonymousEmployerDefaultSite()
        {
            // Create communities and members.

            var communities = CreateCommunities();

            // All communities should be available with no default.

            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(true, new[] { communities[0], communities[1] }, null);

            // Search, selecting each community.

            AssertSearch(null, communities.AllMembers);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[1]);
        }

        [TestMethod]
        public void TestAnonymousEmployerCommunitySite()
        {
            // Create communities and members.

            var communities = CreateCommunities();

            // No choice should be available, and all searches limited to the community.

            Get(GetCommunityEmployerUrl(communities[0]));
            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(false, null, null);

            // Search.

            AssertSearch(null, communities.Members[0]);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[0]);
        }

        [TestMethod]
        public void TestAnonymousEmployerSearchAllCommunitySite()
        {
            // Create communities and members.

            var communities = CreateCommunities();
            communities[0].HasMembers = false;
            communities[0].HasOrganisations = true;
            communities[0].OrganisationsCanSearchAllMembers = true;
            communities[0].OrganisationsAreBranded = true;
            _communitiesCommand.UpdateCommunity(communities[0]);

            // Check default.

            Get(GetCommunityEmployerUrl(communities[0]));
            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(true, new[] { communities[0], communities[1] }, null);

            // Search.

            AssertSearch(null, communities.AllMembers);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[1]);
        }

        [TestMethod]
        public void TestCommunityEmployerDefaultSite()
        {
            // Create communities and members.

            var communities = CreateCommunities();

            // Create employer. When they log in they will be flipped to a community context.

            var employer = CreateEmployer(communities[0]);
            LogIn(employer);

            // Check default.

            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(false, null, null);

            // Search.

            AssertSearch(null, communities.Members[0]);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[0]);
        }

        [TestMethod]
        public void TestCommunityEmployerCommunitySite()
        {
            // Create communities and members.

            var communities = CreateCommunities();

            // Create employer.

            var employer = CreateEmployer(communities[0]);
            LogIn(employer);

            // Check default.

            Get(GetCommunityEmployerUrl(communities[0]));
            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(false, null, null);

            // Search within community, should only get its members back.

            AssertSearch(null, communities.Members[0]);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[0]);
        }

        [TestMethod]
        public void TestCommunityEmployerSearchAllCommunitySite()
        {
            // Create communities and members.

            var communities = CreateCommunities();
            communities[0].HasMembers = false;
            communities[0].HasOrganisations = true;
            communities[0].OrganisationsCanSearchAllMembers = true;
            communities[0].OrganisationsAreBranded = true;
            _communitiesCommand.UpdateCommunity(communities[0]);

            // Create employer.

            var employer = CreateEmployer(communities[0]);
            LogIn(employer);

            // Check default.

            Get(GetCommunityEmployerUrl(communities[0]));
            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(true, new[] { communities[0], communities[1] }, null);

            // Search.

            AssertSearch(null, communities.AllMembers);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[1]);
        }

        [TestMethod]
        public void TestCommunityEmployerOtherCommunitySite()
        {
            // Create communities and members.

            var communities = CreateCommunities();

            // Create employer.

            var employer = CreateEmployer(communities[1]);
            LogIn(employer);

            // Check default.

            Get(GetCommunityEmployerUrl(communities[0]));
            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(false, null, null);

            // Search.

            AssertSearch(null, communities.Members[0]);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[0]);
        }

        [TestMethod]
        public void TestCommunityEmployerOtherSearchAllCommunitySite()
        {
            // Create communities and members.

            var communities = CreateCommunities();
            communities[0].HasMembers = false;
            communities[0].HasOrganisations = true;
            communities[0].OrganisationsCanSearchAllMembers = true;
            communities[0].OrganisationsAreBranded = true;
            _communitiesCommand.UpdateCommunity(communities[0]);

            // Create employer.

            var employer = CreateEmployer(communities[1]);
            LogIn(employer);

            // Check default.

            Get(GetCommunityEmployerUrl(communities[0]));
            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(true, new[] { communities[0], communities[1] }, null);

            // Search.

            AssertSearch(null, communities.AllMembers);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[1]);
        }

        [TestMethod]
        public void TestGeneralEmployerDefaultSite()
        {
            // Create communities and members.

            var communities = CreateCommunities();

            // Create employer.

            var employer = CreateEmployer(null);
            LogIn(employer);

            // Check default.

            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(true, new[] { communities[0], communities[1] }, null);

            // Search.

            AssertSearch(null, communities.AllMembers);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[1]);
        }

        [TestMethod]
        public void TestGeneralEmployerCommunitySite()
        {
            // Create communities and members.

            var communities = CreateCommunities();

            // Create employer.

            var employer = CreateEmployer(null);
            LogIn(employer);

            // Check default.

            Get(GetCommunityEmployerUrl(communities[0]));
            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(true, new[] { communities[0], communities[1] }, null);

            // Search.

            AssertSearch(null, communities.AllMembers);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[1]);
        }

        [TestMethod]
        public void TestGeneralEmployerSearchAllCommunitySite()
        {
            // Create communities and members.

            var communities = CreateCommunities();
            communities[0].HasMembers = false;
            communities[0].HasOrganisations = true;
            communities[0].OrganisationsCanSearchAllMembers = true;
            communities[0].OrganisationsAreBranded = true;
            _communitiesCommand.UpdateCommunity(communities[0]);

            // Create employer.

            var employer = CreateEmployer(null);
            LogIn(employer);

            // Check default.

            Get(GetCommunityEmployerUrl(communities[0]));
            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(true, new[] { communities[0], communities[1] }, null);

            // Search.

            AssertSearch(null, communities.AllMembers);
            AssertSearch(communities[0], communities.Members[0]);
            AssertSearch(communities[1], communities.Members[1]);
        }

        [TestMethod]
        public void TestGeneralEmployerDefaultSiteDisabledCommunity()
        {
            // Create communities and members.

            var communities = CreateCommunities();

            // Create employer.

            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(true, new[] { communities[0], communities[1] }, null);

            // Delete the community.

            var vertical = _verticalsCommand.GetVertical(communities[1]);
            vertical.IsDeleted = true;
            _verticalsCommand.UpdateVertical(vertical);

            // Check again.

            Get(GetSearchUrl(BusinessAnalyst));
            AssertCommunityId(true, new[] { communities[0] }, null);
        }

        private Communities CreateCommunities()
        {
            var index = 0;

            var communities = new Communities();

            var community = CreateCommunity(0);
            communities.Add(community, CreateMembers(ref index, community, 2));

            community = CreateCommunity(1);
            communities.Add(community, CreateMembers(ref index, community, 3));

            communities.NoCommunityMembers = CreateMembers(ref index, null, 4);

            return communities;
        }
    }
}
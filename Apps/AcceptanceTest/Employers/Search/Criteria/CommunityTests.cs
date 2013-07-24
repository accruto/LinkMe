using System;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class CommunityTests
        : CriteriaTests
    {
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.CommunityId = null;
            TestDisplay(criteria);

            var community = TestCommunity.Scouts.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            criteria.CommunityId = community.Id;
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestCommunities()
        {
            // Create members.

            var community1 = TestCommunity.Scouts.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var community2 = TestCommunity.Aime.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            var member0 = CreateMember(0, null);
            var member1 = CreateMember(1, community1);
            var member2 = CreateMember(2, community2);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do no community.

            criteria.CommunityId = null;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            // Community 1.

            criteria.CommunityId = community1.Id;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member1);

            // Community 2.

            criteria.CommunityId = community2.Id;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member2);
        }

        [TestMethod]
        public void TestHiddenCommunity()
        {
            // Create members.

            var community = TestCommunity.Scouts.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
                
            var member0 = CreateMember(0, community);
            var member1 = CreateMember(1, community);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Search.

            criteria.CommunityId = community.Id;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1);

            // Hide a member.

            member1.VisibilitySettings.Professional.EmploymentVisibility = member1.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Communities);
            _memberAccountsCommand.UpdateMember(member1);
            _memberSearchService.UpdateMember(member1.Id);

            // Search.

            criteria.CommunityId = community.Id;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0);
        }

        private Member CreateMember(int index, Community community)
        {
            var member = _memberAccountsCommand.CreateTestMember(index, community == null ? (Guid?) null : community.Id);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}
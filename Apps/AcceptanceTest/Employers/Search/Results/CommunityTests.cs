using System;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Verticals;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Results
{
    [TestClass]
    public class CommunityTests
        : SearchTests
    {
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        [TestMethod]
        public void TestCommunityMember()
        {
            // Use monash as they have a candidate image.

            var data = TestCommunity.MonashGsb.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create members.

            var member = CreateMember(community, true);

            // Search.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            Get(GetSearchUrl(criteria));
            AssertMembers(member);

            AssertBrand(member, data, data, true);
        }

        [TestMethod]
        public void TestHiddenCommunityMember()
        {
            var data = TestCommunity.MonashGsb.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create members.

            var member = CreateMember(community, false);

            // Search.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            Get(GetSearchUrl(criteria));
            AssertMembers(member);

            AssertBrand(member, data, data, false);
        }

        [TestMethod]
        public void TestNonCommunityMember()
        {
            var data = TestCommunity.MonashGsb.GetCommunityTestData();
            data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create members.

            var member = CreateMember(null, false);

            // Search.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            Get(GetSearchUrl(criteria));
            AssertMembers(member);

            AssertBrand(member, null, data, false);
        }

        [TestMethod]
        public void TestCommunityMemberLoggedIn()
        {
            var data = TestCommunity.MonashGsb.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create employer and login.

            var employer = CreateEmployer();
            LogIn(employer);

            // Create members.

            var member = CreateMember(community, true);

            // Search.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            Get(GetSearchUrl(criteria));
            AssertMembers(member);

            AssertBrand(member, data, data, true);
        }

        [TestMethod]
        public void TestHiddenCommunityMemberLoggedIn()
        {
            var data = TestCommunity.MonashGsb.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create employer and login.

            var employer = CreateEmployer();
            LogIn(employer);

            // Create members.

            var member = CreateMember(community, false);
            
            // Search.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            Get(GetSearchUrl(criteria));
            AssertMembers(member);

            AssertBrand(member, data, data, false);
        }

        [TestMethod]
        public void TestNonCommunityMemberLoggedIn()
        {
            var data = TestCommunity.MonashGsb.GetCommunityTestData();
            data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create employer and login.

            var employer = CreateEmployer();
            LogIn(employer);

            // Create members.

            var member = CreateMember(null, false);

            // Search.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            Get(GetSearchUrl(criteria));
            AssertMembers(member);

            AssertBrand(member, null, data, false);
        }

        private Member CreateMember(Community community, bool showCommunity)
        {
            var member = _memberAccountsCommand.CreateTestMember(0, community != null ? community.Id : (Guid?)null);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            if (community != null && showCommunity)
                member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Communities);
            else
                member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Communities);

            _memberAccountsCommand.UpdateMember(member);
            _memberSearchService.UpdateMember(member.Id);

            return member;
        }

        private void AssertBrand(IRegisteredUser member, VerticalTestData memberCommunityTestData, VerticalTestData communityData, bool showCommunity)
        {
            // Look for the brand if appropriate.

            if (member.AffiliateId != null && showCommunity)
            {
                var imageUrl = new ReadOnlyApplicationUrl(memberCommunityTestData.CandidateImageUrl);
                AssertPageContains(imageUrl.FileName);
            }
            else
            {
                var imageUrl = new ReadOnlyApplicationUrl(communityData.CandidateImageUrl);
                AssertPageDoesNotContain(imageUrl.FileName);
            }
        }
    }
}
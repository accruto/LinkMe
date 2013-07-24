using System;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Communities
{
    [TestClass]
    public class CommunitySearchSnippetTests
        : CommunityTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

        private enum CandidateType
        {
            General,
            Community
        }

        private enum EmployerType
        {
            Anonymous,
            General,
            Community,
        }

        private enum SiteType
        {
            General,
            Community
        }

        [TestMethod]
        public void TestGeneralCandidateAnonymousEmployerGeneralSite()
        {
            Test(CandidateType.General, EmployerType.Anonymous, SiteType.General, true);
        }

        [TestMethod]
        public void TestCommunityCandidateAnonymousEmployerGeneralSite()
        {
            Test(CandidateType.Community, EmployerType.Anonymous, SiteType.General, true);
        }

        [TestMethod]
        public void TestGeneralCandidateAnonymousEmployerCommunitySite()
        {
            Test(CandidateType.General, EmployerType.Anonymous, SiteType.Community, false);
        }

        [TestMethod]
        public void TestCommunityCandidateAnonymousEmployerCommunitySite()
        {
            Test(CandidateType.Community, EmployerType.Anonymous, SiteType.Community, true);
        }

        [TestMethod]
        public void TestGeneralCandidateCommunityEmployerGeneralSite()
        {
            Test(CandidateType.General, EmployerType.Community, SiteType.General, false);
        }

        [TestMethod]
        public void TestCommunityCandidateCommunityEmployerGeneralSite()
        {
            Test(CandidateType.Community, EmployerType.Community, SiteType.General, true);
        }
        
        [TestMethod]
        public void TestGeneralCandidateCommunityEmployerCommunitySite()
        {
            Test(CandidateType.General, EmployerType.Community, SiteType.Community, false);
        }

        [TestMethod]
        public void TestCommunityCandidateCommunityEmployerCommunitySite()
        {
            Test(CandidateType.Community, EmployerType.Community, SiteType.Community, true);
        }

        [TestMethod]
        public void TestGeneralCandidateGeneralEmployerGeneralSite()
        {
            Test(CandidateType.General, EmployerType.General, SiteType.General, true);
        }

        [TestMethod]
        public void TestCommunityCandidateGeneralEmployerGeneralSite()
        {
            Test(CandidateType.Community, EmployerType.General, SiteType.General, true);
        }

        [TestMethod]
        public void TestGeneralCandidateGeneralEmployerCommunitySite()
        {
            Test(CandidateType.General, EmployerType.General, SiteType.Community, true);
        }

        [TestMethod]
        public void TestCommunityCandidateGeneralEmployerCommunitySite()
        {
            Test(CandidateType.Community, EmployerType.General, SiteType.Community, true);
        }

        private void Test(CandidateType candidateType, EmployerType employerType, SiteType siteType, bool canView)
        {
            // Create community.

            var communities = CreateCommunities(0);
            var community = communities[0];

            // Create a member. Associate with the community if needed.

            Member member = null;
            if (canView)
                member = CreateMembers(candidateType == CandidateType.Community ? communities : new[] {(Community)null}, 1)[0][0];

            // Create employer. Associate with the community if needed, but give them no credits.

            Employer employer = null;
            if (employerType != EmployerType.Anonymous)
            {
                switch (employerType)
                {
                    case EmployerType.Community:
                        employer = CreateEmployer(community, false);
                        break;

                    case EmployerType.General:
                        employer = CreateEmployer(null, false);
                        break;

                    default:
                        throw new ApplicationException("Unexpected employer type: " + employerType);
                }

                LogIn(employer);
            }

            // The employer has no credits so no access should be given unless they are in the same community.

            Search(candidateType, siteType, community, member);

            if (employer != null)
            {
                // Give the employer some credits.

                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
                
                // Search again.

                Search(candidateType, siteType, community, member);
            }
        }

        private void Search(CandidateType candidateType, SiteType siteType, Community community, Member member)
        {
            Search(candidateType, siteType, community);

            // Assert that the member is shown or not shown as appropriate.

            if (member == null)
            {
                AssertNoResults();
            }
            else
            {
                AssertResultCounts(1, 1, 1);
                AssertMembers(member);
            }
        }

        private void Search(CandidateType candidateType, SiteType siteType, Community community)
        {
            // Navigate to search page, via the community page if needed.

            if (siteType == SiteType.Community)
                Get(GetCommunityEmployerUrl(community));

            var searchUrl = GetSearchUrl(BusinessAnalyst).AsNonReadOnly();

            // Set up the search. Some extra steps may need to be done to ensure that it works.

            if (siteType == SiteType.Community)
            {
                if (candidateType == CandidateType.General)
                {
  //                  if (employerType == EmployerType.General)
//                        SetCommunity(_ddlCommunity, null);
                }
            }
            else
            {
                // General site.

                if (candidateType == CandidateType.Community)
                    searchUrl.QueryString[MemberSearchCriteriaKeys.CommunityId] = community.Id.ToString();
            }

            // Fill in the form and search.

            Get(searchUrl);
        }
    }
}
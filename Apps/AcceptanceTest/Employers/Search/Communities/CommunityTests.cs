using System;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Communities
{
    [TestClass]
    public abstract class CommunityTests
        : SearchTests
    {
        protected const string Password = "password";

        protected readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsQuery _verticalsQuery = Resolve<IVerticalsQuery>();
        protected readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        protected readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        // Ensure that ids are consistent across multiple test cases because they are cached in the VerticalSiteMapProvider.

        private static readonly Guid[] CommunityIds = new[]
        {
            new Guid("{7EA341CB-71E1-4ed0-A998-736B3C2CC27D}"),
            new Guid("{96584585-608D-4af6-826E-EE301094ED8A}")
        };

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();
        }

        protected Community CreateCommunity(int index)
        {
            var id = CommunityIds[index];
            var name = string.Format("Community{0}", index);
            var url = string.Format("community{0}", index);

            _verticalsCommand.CreateVertical(new Vertical { Id = id, Name = name, Url = url });
            var community = new Community { Id = id, Name = name, HasMembers = true, HasOrganisations = true };
            _communitiesCommand.CreateCommunity(community);
            return community;
        }

        protected Community[] CreateCommunities(params int?[] indexes)
        {
            return (from i in indexes select (i == null ? null : CreateCommunity(i.Value))).ToArray();
        }

        protected Member[][] CreateMembers(Community[] communities, int count)
        {
            var index = 0;
            return (from c in communities select CreateMembers(ref index, c, count)).ToArray();
        }

        protected Member[] CreateMembers(ref int memberIndex, Community community, int count)
        {
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(ref memberIndex, community);
            return members;
        }

        private Member CreateMember(ref int index, Community community)
        {
            var joinTime = new DateTime(2007, 1, 1).AddDays(index);
            var member = _memberAccountsCommand.CreateTestMember(index, community != null ? community.Id : (Guid?)null);

            _locationQuery.ResolveLocation(member.Address.Location, Australia, "Melbourne 3000 VIC");

            member.CreatedTime = joinTime;
            _memberAccountsCommand.UpdateMember(member);

            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate, joinTime);

            _memberSearchService.UpdateMember(member.Id);

            index++;
            return member;
        }

        protected void AssertSearch(Community community, Member[] members)
        {
            // Search.

            var searchUrl = GetSearchUrl(BusinessAnalyst).AsNonReadOnly();
            if (community != null)
                searchUrl.QueryString[MemberSearchCriteriaKeys.CommunityId] = community.Id.ToString();
            Get(searchUrl);

            // Check results.

            AssertResultCounts(1, members.Length, members.Length);
            AssertMembers(false, members);
        }

        protected Url GetCommunityEmployerUrl(Community community)
        {
            var vertical = _verticalsQuery.GetVertical(community.Id);
            return new ApplicationUrl("~/" + vertical.Url + "/employers/Employer.aspx");
        }

        protected Employer CreateEmployer(Community community)
        {
            return CreateEmployer(community, true);
        }

        protected Employer CreateEmployer(Community community, bool allocateCredits)
        {
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);

            // Allocate credits.

            if (allocateCredits)
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });

            // Associate with the community.

            if (community != null)
            {
                organisation.AffiliateId = community.Id;
                _organisationsCommand.UpdateOrganisation(organisation);
            }
            return employer;
        }

        protected static void SetCommunity(HtmlDropDownListTester ddlCommunity, Community community)
        {
            Assert.IsTrue(ddlCommunity.IsVisible);
            Assert.AreEqual(true, ddlCommunity.IsEnabled);
            ddlCommunity.SelectedValue = community != null ? community.Id.ToString() : string.Empty;
        }

        protected static void ConfirmSelectedCommunity(HtmlDropDownListTester ddlCommunity, bool visible, Community community)
        {
            Assert.AreEqual(ddlCommunity, visible);
            if (visible)
                Assert.AreEqual(community != null ? community.Id.ToString() : string.Empty, ddlCommunity.SelectedItem.Value);
        }

        protected static void ConfirmCommunities(HtmlDropDownListTester ddlCommunity, params Community[] communities)
        {
            Assert.AreEqual(communities.Length + 1, ddlCommunity.Items.Count);
            foreach (var community in communities)
            {
                var found = false;
                foreach (var item in ddlCommunity.Items)
                {
                    if (item.Value == community.Id.ToString())
                    {
                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found);
            }
        }
    }
}
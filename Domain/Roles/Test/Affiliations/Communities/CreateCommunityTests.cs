using System;
using LinkMe.Domain.Roles.Affiliations.Communities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Affiliations.Communities
{
    [TestClass]
    public class CreateCommunityTests
        : CommunityTests
    {
        [TestMethod]
        public void TestCreateCommunity()
        {
            var community = new Community
            {
                Name = string.Format(CommunityNameFormat, 0),
                ShortName = string.Format(CommunityShortNameFormat, 0),
                HasMembers = true,
                HasOrganisations = true,
                OrganisationsCanSearchAllMembers = false,
                OrganisationsAreBranded = false,
                EmailDomain = "test.linkme.net.au",
            };

            _communitiesCommand.CreateCommunity(community);
            Assert.AreNotEqual(Guid.Empty, community.Id);
            AssertCommunity(community, _communitiesQuery.GetCommunity(community.Id));
        }

        private static void AssertCommunity(Community expectedCommunity, Community community)
        {
            Assert.AreEqual(expectedCommunity.Id, community.Id);
            Assert.AreEqual(expectedCommunity.Name, community.Name);
            Assert.AreEqual(expectedCommunity.ShortName, community.ShortName);
            Assert.AreEqual(expectedCommunity.HasMembers, community.HasMembers);
            Assert.AreEqual(expectedCommunity.HasOrganisations, community.HasOrganisations);
            Assert.AreEqual(expectedCommunity.OrganisationsCanSearchAllMembers, community.OrganisationsCanSearchAllMembers);
            Assert.AreEqual(expectedCommunity.OrganisationsAreBranded, community.OrganisationsAreBranded);
            Assert.AreEqual(expectedCommunity.EmailDomain, community.EmailDomain);
        }
    }
}

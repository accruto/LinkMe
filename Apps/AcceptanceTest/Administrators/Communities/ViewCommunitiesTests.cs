using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communities
{
    [TestClass]
    public class ViewCommunitiesTests
        : CommunitiesTests
    {
        [TestMethod]
        public void TestCommunities()
        {
            // Create some communities.

            var communities = new Community[3];
            communities[0] = TestCommunity.Ahri.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            communities[1] = TestCommunity.Scouts.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            communities[2] = TestCommunity.Talent2.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Login.

            var administrator = CreateAdministrator(1);
            LogIn(administrator);
            Get(GetCommunitiesUrl());

            AssertCommunities(communities);
        }

        [TestMethod]
        public void TestViewCommunity()
        {
            // Create a community.

            var community = CreateCommunity();
            var vertical = _verticalsCommand.GetVertical(community);

            // Login.

            var administrator = CreateAdministrator(1);
            LogIn(administrator);
            Get(GetCommunityUrl(community));

            AssertCommunity(community, vertical);
        }

        [TestMethod]
        public void TestViewDeletedCommunity()
        {
            // Create a community.

            var community = CreateCommunity();
            var vertical = _verticalsCommand.GetVertical(community);
            vertical.IsDeleted = true;
            _verticalsCommand.UpdateVertical(vertical);

            // Login.

            var administrator = CreateAdministrator(1);
            LogIn(administrator);
            Get(GetCommunityUrl(community));

            AssertCommunity(community, vertical);
        }

        private void AssertCommunity(Community community, Vertical vertical)
        {
            AssertPageContains(community.Name);
            if (!string.IsNullOrEmpty(vertical.Host))
                AssertPageContains(vertical.Host);
            if (!string.IsNullOrEmpty(vertical.SecondaryHost))
                AssertPageContains(vertical.SecondaryHost);
            if (!string.IsNullOrEmpty(vertical.TertiaryHost))
                AssertPageContains(vertical.TertiaryHost);
            if (!string.IsNullOrEmpty(vertical.Url))
                AssertPageContains(vertical.Url);

            // Enabled <=> !IsDeleted.

            AssertPageContains(vertical.IsDeleted ? "No" : "Yes");
        }

        private void AssertCommunities(IList<Community> communities)
        {
            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            Assert.AreEqual(communities.Count, trNodes.Count);

            for (var index = 0; index < communities.Count; ++index)
            {
                var community = communities[index];
                var trNode = trNodes[index];

                Assert.AreEqual(community.Name, trNode.SelectSingleNode("td/a").InnerText);
                Assert.AreEqual(GetCommunityUrl(community).PathAndQuery.ToLower(), trNode.SelectSingleNode("td/a").Attributes["href"].Value.ToLower());
            }
        }
    }
}

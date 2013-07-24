using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Test.Verticals;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.Specific
{
    [TestClass]
    public class AllTests
        : CommunityTests
    {
        private const string UserId = "testadmin";

        private const string RightSectionItemName = "Home page right section";
        private const string LeftSectionItemName = "Home page left section";
        private const string LoggedInLeftSectionItemName = "Logged in home page left section";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void CreateAllCommunitiesTest()
        {
            _administratorAccountsCommand.CreateTestAdministrator(1);

            CreateCommunity(TestCommunity.Autopeople);
            CreateCommunity(TestCommunity.ItWire);
            CreateCommunity(TestCommunity.MonashGsb);
            CreateCommunity(TestCommunity.Pga);
            CreateCommunity(TestCommunity.Rcsa);
            CreateCommunity(TestCommunity.Scouts);
            CreateCommunity(TestCommunity.Aimpe);
            CreateCommunity(TestCommunity.Aat);
            CreateCommunity(TestCommunity.Maanz);
            CreateCommunity(TestCommunity.LiveInAustralia);
            CreateCommunity(TestCommunity.TheNursingCentre);
            CreateCommunity(TestCommunity.BusinessSpectator);
            CreateCommunity(TestCommunity.Gta);
            CreateCommunity(TestCommunity.NextStep);
        }

        private void CreateCommunity(TestCommunity community)
        {
            CreateCommunity(community.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine));
        }

        private void CreateCommunity(Community community)
        {
            var vertical = _verticalsCommand.GetVertical(community);
            _contentEngine.AddSectionContent(vertical, false, RightSectionItemName, string.Empty, string.Empty);
            _contentEngine.AddSectionContent(vertical, false, LeftSectionItemName, string.Empty, string.Empty);
            _contentEngine.AddSectionContent(vertical, false, LoggedInLeftSectionItemName, string.Empty, string.Empty);
            CreateCommunityCustodian(community);
        }

        private void CreateCommunityCustodian(Community community)
        {
            var vertical = _verticalsQuery.GetVertical(community.Id);
            _custodianAccountsCommand.CreateTestCustodian(vertical.Url.Replace("/", "") + UserId, community.Id);
        }
    }
}

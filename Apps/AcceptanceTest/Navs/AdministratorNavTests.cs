using System.Collections.Generic;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navs
{
    [TestClass]
    public class AdministratorNavTests
        : NavTests
    {
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();

        private static ReadOnlyApplicationUrl _homeUrl;

        private static ReadOnlyApplicationUrl _searchMembersUrl;
        private const string SearchMembersText = "Search";
        private const string SearchMembersSecondaryText = "Search members";
        private static ReadOnlyApplicationUrl _searchEmployersUrl;
        private const string SearchEmployersText = "Search employers";
        private static ReadOnlyApplicationUrl _searchOrganisationsUrl;
        private const string SearchOrganisationsText = "Search organisations";
        private static ReadOnlyApplicationUrl _searchEnginesUrl;
        private const string SearchEnginesText = "Search engines";

        private static ReadOnlyApplicationUrl _campaignsUrl;
        private const string CampaignsText = "Campaigns";

        [TestInitialize]
        public void TestInitialize()
        {
            _homeUrl = new ReadOnlyApplicationUrl(true, "~/administrators/home");
            _searchMembersUrl = new ReadOnlyApplicationUrl(true, "~/administrators/members/search");
            _searchEmployersUrl = new ReadOnlyApplicationUrl(true, "~/administrators/employers/search");
            _searchOrganisationsUrl = new ReadOnlyApplicationUrl(true, "~/administrators/organisations/search");
            _searchEnginesUrl = new ReadOnlyApplicationUrl(true, "~/administrators/search/engines");
            _campaignsUrl = new ReadOnlyApplicationUrl(true, "~/administrators/communications/campaigns");
        }

        [TestMethod]
        public void TestNavs()
        {
            LogIn();
            var navs = GetNavs();
            SetNavs(_homeUrl, navs);
            TestNavs(_homeUrl);
            TestNavs(navs);
        }

        private void LogIn()
        {
            LogIn(_administratorAccountsCommand.CreateTestAdministrator(0));
        }

        private static IList<Nav> GetNavs()
        {
            return new List<Nav>
            {
                new Nav
                {
                    Url = _searchMembersUrl,
                    Text = SearchMembersText,
                    SubNavs = new List<Nav>
                    {
                        new Nav {Url = _searchMembersUrl, Text = SearchMembersSecondaryText},
                        new Nav {Url = _searchEmployersUrl, Text = SearchEmployersText},
                        new Nav {Url = _searchOrganisationsUrl, Text = SearchOrganisationsText},
                        new Nav {Url = _searchEnginesUrl, Text = SearchEnginesText},
                    },
                },
                new Nav {Url = _campaignsUrl, Text = CampaignsText},
            };
        }

        protected override void AssertHeaderNavs()
        {
            // Second row.

            AssertNoEmployerSwitchNav();
            AssertNoMemberSwitchNav();

            // Third row.

            AssertNoLoginNav();
            AssertNoAccountNav();
            AssertNoSettingsNav();
            AssertLogoutNav();
        }
    }
}
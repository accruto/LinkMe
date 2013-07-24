using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navs
{
    [TestClass]
    public class CustodianNavTests
        : NavTests
    {
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();

        private static ReadOnlyApplicationUrl _homeUrl;

        private static ReadOnlyApplicationUrl _changePasswordUrl;
        private const string PasswordText = "Password";

        [TestInitialize]
        public void TestInitialize()
        {
            _homeUrl = new ReadOnlyApplicationUrl(true, "~/custodians/home");
            _changePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/changepassword");
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

        private static IList<Nav> GetNavs()
        {
            return new List<Nav>
            {
                new Nav
                {
                    Url = _changePasswordUrl,
                    Text = PasswordText,
                },
            };
        }

        private void LogIn()
        {
            var community = TestCommunity.Scouts.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var administrator = _custodianAccountsCommand.CreateTestCustodian(0, community.Id);
            LogIn(administrator);
        }

        protected override void AssertHeaderNavs()
        {
            // Second row.

            AssertNoMemberSwitchNav();
            AssertNoEmployerSwitchNav();

            // Third row.

            AssertNoLoginNav();
            AssertNoAccountNav();
            AssertNoSettingsNav();
            AssertLogoutNav();
        }
    }
}
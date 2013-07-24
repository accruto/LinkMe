using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Home
{
    [TestClass]
    public class HomeTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();

        private ReadOnlyUrl _homeUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _homeUrl = new ReadOnlyApplicationUrl(true, "~/members/home");
        }

        [TestMethod]
        public void TestNotLoggedIn()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Browser.UseMobileUserAgent = false;
            Get(_homeUrl);
            AssertUrl(GetLoginUrl(LoggedInMemberHomeUrl));
            SubmitLogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestNotLoggedInOldHome()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Browser.UseMobileUserAgent = false;
            var url = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/NetworkerHome.aspx");
            Get(url);
            AssertUrl(GetLoginUrl(LoggedInMemberHomeUrl));
            SubmitLogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestHome()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Browser.UseMobileUserAgent = false;
            Get(_homeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestOldHome()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Browser.UseMobileUserAgent = false;
            var url = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/NetworkerHome.aspx");
            Get(url);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestMobileHome()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Browser.UseMobileUserAgent = true;
            Get(_homeUrl);
            AssertUrl(_homeUrl);
        }
    }
}

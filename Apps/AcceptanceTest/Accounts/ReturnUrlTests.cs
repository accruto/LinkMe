using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Web.Members.Friends;
using LinkMe.Apps.Asp;
using LinkMe.Apps.Asp.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class ReturnUrlsTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();

        [TestMethod]
        public void TestLoginNoReturnUrl()
        {
            Get(LogInUrl);
            AssertUrl(LogInUrl);

            var member = _memberAccountsCommand.CreateTestMember(0);
            SubmitLogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestLoginWithReturnUrl()
        {
            var loginUrl = LogInUrl.AsNonReadOnly();
            loginUrl.QueryString[Constants.ReturnUrlParameter] = NavigationManager.GetUrlForPage<ViewFriends>().ToString();
            Get(loginUrl);
            AssertUrl(loginUrl);

            var member = _memberAccountsCommand.CreateTestMember(0);
            SubmitLogIn(member);
            AssertPage<ViewFriends>();
        }
    }
}
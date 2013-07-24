using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class MemberChangePasswordTests
        : ChangePasswordTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private ReadOnlyUrl _settingsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/members/settings");
        }

        protected override RegisteredUser CreateUser()
        {
            return _memberAccountsCommand.CreateTestMember(1);
        }

        protected override ReadOnlyUrl GetHomeUrl()
        {
            return LoggedInMemberHomeUrl;
        }

        protected override ReadOnlyUrl GetCancelUrl()
        {
            return _settingsUrl;
        }
    }
}
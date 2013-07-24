using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class CustodianChangePasswordTests
        : ChangePasswordTests
    {
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();

        protected override RegisteredUser CreateUser()
        {
            var community = _communitiesCommand.CreateTestCommunity(0);
            return _custodianAccountsCommand.CreateTestCustodian(0, community.Id);
        }

        protected override ReadOnlyUrl GetHomeUrl()
        {
            return LoggedInCustodianHomeUrl;
        }

        protected override ReadOnlyUrl GetCancelUrl()
        {
            return LoggedInCustodianHomeUrl;
        }
    }
}
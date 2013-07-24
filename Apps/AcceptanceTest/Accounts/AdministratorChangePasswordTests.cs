using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class AdministratorChangePasswordTests
        : ChangePasswordTests
    {
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();

        protected override RegisteredUser CreateUser()
        {
            return _administratorAccountsCommand.CreateTestAdministrator(0);
        }

        protected override ReadOnlyUrl GetHomeUrl()
        {
            return LoggedInAdministratorHomeUrl;
        }

        protected override ReadOnlyUrl GetCancelUrl()
        {
            return LoggedInAdministratorHomeUrl;
        }
    }
}
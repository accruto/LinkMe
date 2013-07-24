using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Users.Administrators
{
    [TestClass]
    public class AdministratorAccountsTests
        : TestClass
    {
        private const string LoginId1 = "admin1";
        private const string LoginId2 = "admin2";

        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IAdministratorsQuery _administratorsQuery = Resolve<IAdministratorsQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly ILoginCredentialsCommand _loginCredentialsCommand = Resolve<ILoginCredentialsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreateAccount()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            AssertAreEqual(administrator, _administratorsQuery.GetAdministrator(administrator.Id));
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void TestCreateDuplicateAccounts()
        {
            _administratorAccountsCommand.CreateTestAdministrator(LoginId1);
            _administratorAccountsCommand.CreateTestAdministrator(LoginId1);
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void TestUpdateDuplicateAccounts()
        {
            _administratorAccountsCommand.CreateTestAdministrator(LoginId1);
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(LoginId2);
            var loginCredentials = _loginCredentialsQuery.GetCredentials(administrator.Id);
            loginCredentials.LoginId = LoginId1;
            _loginCredentialsCommand.UpdateCredentials(administrator.Id, loginCredentials, administrator.Id);
        }

        private static void AssertAreEqual(IAdministrator expectedAdministrator, IAdministrator administrator)
        {
            Assert.IsNotNull(expectedAdministrator);
            Assert.IsNotNull(administrator);
            Assert.AreEqual(expectedAdministrator.IsEnabled, administrator.IsEnabled);
            Assert.AreEqual(expectedAdministrator.EmailAddress, administrator.EmailAddress);
            Assert.AreEqual(expectedAdministrator.IsActivated, administrator.IsActivated);
            Assert.AreEqual(expectedAdministrator.FirstName, administrator.FirstName);
            Assert.AreEqual(expectedAdministrator.Id, administrator.Id);
            Assert.AreEqual(expectedAdministrator.LastName, administrator.LastName);
        }
    }
}

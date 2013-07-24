using System;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Custodians.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Users.Custodians
{
    [TestClass]
    public class CustodianAccountsTests
        : TestClass
    {
        private const string LoginId1 = "admin1";
        private const string LoginId2 = "admin2";

        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();
        private readonly ICustodiansQuery _custodiansQuery = Resolve<ICustodiansQuery>();
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
            var custodian = _custodianAccountsCommand.CreateTestCustodian(0, Guid.NewGuid());
            AssertAreEqual(custodian, _custodiansQuery.GetCustodian(custodian.Id));
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void TestCreateDuplicateAccounts()
        {
            _custodianAccountsCommand.CreateTestCustodian(LoginId1, Guid.NewGuid());
            _custodianAccountsCommand.CreateTestCustodian(LoginId1, Guid.NewGuid());
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void TestUpdateDuplicateAccounts()
        {
            _custodianAccountsCommand.CreateTestCustodian(LoginId1, Guid.NewGuid());
            var custodian = _custodianAccountsCommand.CreateTestCustodian(LoginId2, Guid.NewGuid());
            var loginCredentials = _loginCredentialsQuery.GetCredentials(custodian.Id);
            loginCredentials.LoginId = LoginId1;
            _loginCredentialsCommand.UpdateCredentials(custodian.Id, loginCredentials, custodian.Id);
        }

        private static void AssertAreEqual(Custodian expectedCustodian, Custodian custodian)
        {
            Assert.IsNotNull(expectedCustodian);
            Assert.IsNotNull(custodian);
            Assert.AreEqual(expectedCustodian.IsEnabled, custodian.IsEnabled);
            Assert.AreEqual(expectedCustodian.EmailAddress, custodian.EmailAddress);
            Assert.AreEqual(expectedCustodian.IsActivated, custodian.IsActivated);
            Assert.AreEqual(expectedCustodian.FirstName, custodian.FirstName);
            Assert.AreEqual(expectedCustodian.Id, custodian.Id);
            Assert.AreEqual(expectedCustodian.LastName, custodian.LastName);
        }
    }
}
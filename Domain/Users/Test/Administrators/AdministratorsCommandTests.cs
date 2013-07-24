using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Administrators.Commands;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Administrators
{
    [TestClass]
    public class AdministratorsCommandTests
        : TestClass
    {
        private readonly IAdministratorsCommand _administratorsCommand = Resolve<IAdministratorsCommand>();
        private readonly IAdministratorsQuery _administratorsQuery = Resolve<IAdministratorsQuery>();

        public void AdministratorsCommandTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUpdateFirstName()
        {
            var administrator = _administratorsCommand.CreateTestAdministrator(1);
            AssertAdministrator(administrator, _administratorsQuery.GetAdministrator(administrator.Id));

            administrator.FirstName = "Changed";
            _administratorsCommand.UpdateAdministrator(administrator);
            AssertAdministrator(administrator, _administratorsQuery.GetAdministrator(administrator.Id));
        }

        private static void AssertAdministrator(IAdministrator expectedAdministrator, IAdministrator administrator)
        {
            Assert.AreEqual(expectedAdministrator.Id, administrator.Id);
            Assert.AreEqual(expectedAdministrator.EmailAddress, administrator.EmailAddress);
            Assert.AreEqual(expectedAdministrator.FirstName, administrator.FirstName);
            Assert.AreEqual(expectedAdministrator.LastName, administrator.LastName);
            Assert.AreEqual(expectedAdministrator.IsActivated, administrator.IsActivated);
            Assert.AreEqual(expectedAdministrator.IsEnabled, administrator.IsEnabled);
            Assert.AreEqual(expectedAdministrator.IsActivated, administrator.IsActivated);
        }
    }
}
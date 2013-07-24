using System;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Query.Reports.Accounts.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Reports
{
    [TestClass]
    public class LogInTests
        : WebTestClass
    {
        private readonly IAccountReportsQuery _accountReportsQuery = Resolve<IAccountReportsQuery>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();

        [TestMethod]
        public void TestMember()
        {
            var member0 = _memberAccountsCommand.CreateTestMember(0);
            var member1 = _memberAccountsCommand.CreateTestMember(1);

            AssertLogIns(0, 0, 0, 0);

            LogIn(member0);
            LogOut();
            AssertLogIns(1, 0, 0, 0);

            LogIn(member0);
            LogOut();
            AssertLogIns(1, 0, 0, 0);

            LogIn(member1);
            LogOut();
            AssertLogIns(2, 0, 0, 0);
        }

        [TestMethod]
        public void TestEmployer()
        {
            var employer0 = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            AssertLogIns(0, 0, 0, 0);

            LogIn(employer0);
            LogOut();
            AssertLogIns(0, 1, 0, 0);

            LogIn(employer0);
            LogOut();
            AssertLogIns(0, 1, 0, 0);

            LogIn(employer1);
            LogOut();
            AssertLogIns(0, 2, 0, 0);
        }

        [TestMethod]
        public void TestAdministrator()
        {
            var administrator0 = _administratorAccountsCommand.CreateTestAdministrator(0);
            var administrator1 = _administratorAccountsCommand.CreateTestAdministrator(1);

            AssertLogIns(0, 0, 0, 0);

            LogIn(administrator0);
            LogOut();
            AssertLogIns(0, 0, 1, 0);

            LogIn(administrator0);
            LogOut();
            AssertLogIns(0, 0, 1, 0);

            LogIn(administrator1);
            LogOut();
            AssertLogIns(0, 0, 2, 0);
        }

        [TestMethod]
        public void TestCustodian()
        {
            var custodian0 = _custodianAccountsCommand.CreateTestCustodian(0, Guid.NewGuid());
            var custodian1 = _custodianAccountsCommand.CreateTestCustodian(1, Guid.NewGuid());

            AssertLogIns(0, 0, 0, 0);

            LogIn(custodian0);
            LogOut();
            AssertLogIns(0, 0, 0, 1);

            LogIn(custodian0);
            LogOut();
            AssertLogIns(0, 0, 0, 1);

            LogIn(custodian1);
            LogOut();
            AssertLogIns(0, 0, 0, 2);
        }

        [TestMethod]
        public void TestAll()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(2);
            var custodian = _custodianAccountsCommand.CreateTestCustodian(3, Guid.NewGuid());

            AssertLogIns(0, 0, 0, 0);

            LogIn(member);
            LogOut();
            AssertLogIns(1, 0, 0, 0);

            LogIn(custodian);
            LogOut();
            AssertLogIns(1, 0, 0, 1);

            LogIn(administrator);
            LogOut();
            AssertLogIns(1, 0, 1, 1);

            LogIn(employer);
            LogOut();
            AssertLogIns(1, 1, 1, 1);
        }

        private void AssertLogIns(int members, int employers, int administrators, int custodians)
        {
            var dateRange = new DateTimeRange(DateTime.Today, DateTime.Today.AddDays(1));
            Assert.AreEqual(members, _accountReportsQuery.GetLogIns(UserType.Member, dateRange));
            Assert.AreEqual(employers, _accountReportsQuery.GetLogIns(UserType.Employer, dateRange));
            Assert.AreEqual(administrators, _accountReportsQuery.GetLogIns(UserType.Administrator, dateRange));
            Assert.AreEqual(custodians, _accountReportsQuery.GetLogIns(UserType.Custodian, dateRange));
        }
    }
}

using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Accounts.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.ui.registered
{
    [TestClass]
    public class LoginStatsTest
        : WebTestClass
    {
        private List<RegisteredUser> _users;

        private const int Seed = 1010;
        private const int LoginMin = 3;
        private const int LoginMax = 5;

        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IAccountReportsQuery _accountReportsQuery = Resolve<IAccountReportsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            _users = new List<RegisteredUser>();
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _users.Add(_memberAccountsCommand.CreateTestMember("member1@test.linkme.net.au", "password", "John", "Lennon"));
            _users.Add(_memberAccountsCommand.CreateTestMember("member2@test.linkme.net.au", "password", "John", "Lennon"));
            _users.Add(_memberAccountsCommand.CreateTestMember("member3@test.linkme.net.au", "password", "John", "Lennon"));

            _users.Add(_employerAccountsCommand.CreateTestEmployer("employer1", _organisationsCommand.CreateTestOrganisation(0)));
            _users.Add(_employerAccountsCommand.CreateTestEmployer("employer2", _organisationsCommand.CreateTestOrganisation(0)));
            _users.Add(_employerAccountsCommand.CreateTestEmployer("employer3", _organisationsCommand.CreateTestOrganisation(0)));

            _users.Add(_administratorAccountsCommand.CreateTestAdministrator("admin1"));
        }

        [TestMethod]
        public void TestUniqueLoginsCount()
        {
            foreach (var user in _users)
            {
                //Login random times each, 
                //where count of logins is in between LoginMin and LoginMax

                int loginCnt = new Random(Seed).Next(LoginMin, LoginMax);
                for (int i = 0; i < loginCnt; i++)
                {
                    LogIn(user);
                    LogOut();
                }
            }
            
            Assert.AreEqual(_users.Count, 7);

            //Only Member logins
            var logins = _accountReportsQuery.GetLogIns(UserType.Member, new DateTimeRange(DateTime.Now.Date, GetEndOfToday()));
            Assert.AreEqual(3, logins);

            //Only Employer logins
            logins = _accountReportsQuery.GetLogIns(UserType.Employer, new DateTimeRange(DateTime.Now.Date, GetEndOfToday()));
            Assert.AreEqual(3, logins);

            //Only Admin logins
            logins = _accountReportsQuery.GetLogIns(UserType.Administrator, new DateTimeRange(DateTime.Now.Date, GetEndOfToday()));
            Assert.AreEqual(1, logins);

            //No logins yesterday
            logins = _accountReportsQuery.GetLogIns(UserType.Member, new DateTimeRange(DateTime.Now.Date.AddDays(-1), GetEndOfYesterday()));
            Assert.AreEqual(0, logins);
            logins = _accountReportsQuery.GetLogIns(UserType.Employer, new DateTimeRange(DateTime.Now.Date.AddDays(-1), GetEndOfYesterday()));
            Assert.AreEqual(0, logins);
            logins = _accountReportsQuery.GetLogIns(UserType.Administrator, new DateTimeRange(DateTime.Now.Date.AddDays(-1), GetEndOfYesterday()));
            Assert.AreEqual(0, logins);

            //No logins tommorow
            logins = _accountReportsQuery.GetLogIns(UserType.Member, new DateTimeRange(DateTime.Now.Date.AddDays(1), GetEndOfYesterday()));
            Assert.AreEqual(0, logins);
            logins = _accountReportsQuery.GetLogIns(UserType.Employer, new DateTimeRange(DateTime.Now.Date.AddDays(1), GetEndOfYesterday()));
            Assert.AreEqual(0, logins);
            logins = _accountReportsQuery.GetLogIns(UserType.Administrator, new DateTimeRange(DateTime.Now.Date.AddDays(1), GetEndOfYesterday()));
            Assert.AreEqual(0, logins);
        }

        private static DateTime GetEndOfYesterday()
        {
            return DateTime.Now.Date.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        private static DateTime GetEndOfToday()
        {
            return DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }
}

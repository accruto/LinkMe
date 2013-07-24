using System;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Accounts.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Query.Reports
{
    [TestClass]
    public class DailyReportsTests
        : TestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAccountReportsQuery _accountReportsQuery = Resolve<IAccountReportsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestGetMembers()
        {
            _memberAccountsCommand.CreateTestMember(1);
            _memberAccountsCommand.CreateTestMember(2);
            _memberAccountsCommand.CreateTestMember(3);
            Assert.AreEqual(3, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now));
        }

        [TestMethod]
        public void TestGetNewMembers()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            member1.CreatedTime = DateTime.Now.AddDays(-1);
            _memberAccountsCommand.UpdateMember(member1);

            _memberAccountsCommand.CreateTestMember(2);

            var member3 = _memberAccountsCommand.CreateTestMember(3);
            member3.CreatedTime = DateTime.Now.AddDays(-2);
            _memberAccountsCommand.UpdateMember(member3);

            Assert.AreEqual(1, _accountReportsQuery.GetNewUsers(UserType.Member, DayRange.Today));
            Assert.AreEqual(1, _accountReportsQuery.GetNewUsers(UserType.Member, DayRange.Yesterday));
        }

        [TestMethod]
        public void TestGetNewEmployers()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            employer1.CreatedTime = DateTime.Now.AddDays(-2);
            _employerAccountsCommand.UpdateEmployer(employer1);

            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(0));
            employer2.CreatedTime = DateTime.Now.AddDays(-1);
            _employerAccountsCommand.UpdateEmployer(employer2);

            var employer3 = _employerAccountsCommand.CreateTestEmployer(3, _organisationsCommand.CreateTestOrganisation(0));
            employer3.CreatedTime = DateTime.Now.AddDays(-3);
            _employerAccountsCommand.UpdateEmployer(employer3);

            var recruiter4 = _employerAccountsCommand.CreateTestRecruiter(4, _organisationsCommand.CreateTestOrganisation(0));
            recruiter4.CreatedTime = DateTime.Now.AddDays(-1);
            _employerAccountsCommand.UpdateEmployer(recruiter4);

            var recruiter5 = _employerAccountsCommand.CreateTestRecruiter(5, _organisationsCommand.CreateTestOrganisation(0));
            recruiter5.CreatedTime = DateTime.Now.AddDays(0);
            _employerAccountsCommand.UpdateEmployer(recruiter5);

            var recruiter6 = _employerAccountsCommand.CreateTestRecruiter(6, _organisationsCommand.CreateTestOrganisation(0));
            recruiter6.CreatedTime = DateTime.Now.AddDays(-4);
            _employerAccountsCommand.UpdateEmployer(recruiter6);
        }

        [TestMethod]
        public void TestGetDatedNewMembers()
        {
            _memberAccountsCommand.CreateTestMember(1);
            _memberAccountsCommand.CreateTestMember(2);

            var member3 = _memberAccountsCommand.CreateTestMember(3);
            member3.CreatedTime = DateTime.Now.AddDays(5);
            _memberAccountsCommand.UpdateMember(member3);

            var start = DateTime.Now.AddHours(-1);
            var end = DateTime.Now.AddDays(2);
            Assert.AreEqual(2, _accountReportsQuery.GetNewUsers(UserType.Member, new DateTimeRange(start, end)));
        }
    }
}
using System;
using System.Linq;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Users.Employers
{
    [TestClass]
    public class EmployerAccountsTests
        : TestClass
    {
        private const string FirstName = "Monty";
        private const string LastName = "Burns";
        private const string LoginId1 = "monty1";
        private const string LoginId2 = "monty2";
        private const string LoginId3 = "monty3";
        private const string EmailAddress1 = "employer1@test.linkme.net.au";
        private const string EmailAddress2 = "employer2@test.linkme.net.au";

        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly ILoginCredentialsCommand _loginCredentialsCommand = Resolve<ILoginCredentialsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestGetInvalidAccount()
        {
            Assert.IsNull(_employersQuery.GetEmployer(Guid.NewGuid()));
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void TestCreateDuplicateAccounts()
        {
            _employerAccountsCommand.CreateTestEmployer(LoginId1, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer(LoginId1, _organisationsCommand.CreateTestOrganisation(1));
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void TestUpdateDuplicateAccounts()
        {
            _employerAccountsCommand.CreateTestEmployer(LoginId1, _organisationsCommand.CreateTestOrganisation(0));
            var employer = _employerAccountsCommand.CreateTestEmployer(LoginId2, _organisationsCommand.CreateTestOrganisation(1));
            var loginCredentials = _loginCredentialsQuery.GetCredentials(employer.Id);
            loginCredentials.LoginId = LoginId1;
            _loginCredentialsCommand.UpdateCredentials(employer.Id, loginCredentials, employer.Id);
        }

        [TestMethod]
        public void TestUserType()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            Assert.IsTrue(employer.UserType == UserType.Employer);
        }

        [TestMethod]
        public void TestGetByEmailAddress()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer(LoginId1, FirstName, LastName, EmailAddress1, _organisationsCommand.CreateTestOrganisation(1));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(LoginId2, FirstName, LastName, EmailAddress2, _organisationsCommand.CreateTestOrganisation(2));
            var employer3 = _employerAccountsCommand.CreateTestEmployer(LoginId3, FirstName, LastName, EmailAddress1, _organisationsCommand.CreateTestOrganisation(3));

            var employers = _employersQuery.GetEmployers(EmailAddress1);
            Assert.AreEqual(2, employers.Count);
            Assert.IsTrue((from e in employers where e.Id == employer1.Id select e).Any());
            Assert.IsTrue((from e in employers where e.Id == employer3.Id select e).Any());

            employers = _employersQuery.GetEmployers(EmailAddress2);
            Assert.AreEqual(1, employers.Count);
            Assert.IsTrue((from e in employers where e.Id == employer2.Id select e).Any());
        }
    }
}

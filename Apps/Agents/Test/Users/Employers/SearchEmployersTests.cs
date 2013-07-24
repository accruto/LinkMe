using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.Employers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Users.Employers
{
    [TestClass]
    public class SearchEmployersTests
        : TestClass
    {
        private const string FirstName = "Waylon";
        private const string LastName = "Smithers";
        private const string EmailAddress = "wsmithers@test.linkme.net.au";
        private const string LoginId = "wsmithers";

        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IExecuteEmployerSearchCommand _executeEmployerSearchCommand = Resolve<IExecuteEmployerSearchCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoWildCards()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            var employer1 = _employerAccountsCommand.CreateTestEmployer(EmailAddress, FirstName, LastName, organisation);
            _employerAccountsCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer(3, _organisationsCommand.CreateTestOrganisation(0));

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   OrganisationName = organisation.Name,
                                   FirstName = FirstName,
                                   LastName = LastName,
                                   LoginId = LoginId,
                                   EmailAddress = EmailAddress,
                                   IsEnabled = true,
                                   IsDisabled = true
                               };

            var employers = _executeEmployerSearchCommand.Search(criteria);
            Assert.AreEqual(1, employers.Count);
            AssertEmployer(employer1, employers[0]);
        }

        [TestMethod]
        public void TestNoCriteria()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(0));
            var employer3 = _employerAccountsCommand.CreateTestEmployer(3, _organisationsCommand.CreateTestOrganisation(0));

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   IsEnabled = true,
                                   IsDisabled = true
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(3, employers.Count);
            AssertEmployer(employer1, employers[0]);
            AssertEmployer(employer2, employers[1]);
            AssertEmployer(employer3, employers[2]);
        }

        [TestMethod]
        public void TestEnabledOnly()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(0));
            var employer3 = _employerAccountsCommand.CreateTestEmployer(3, _organisationsCommand.CreateTestOrganisation(0));

            employer1.IsEnabled = false;
            _employerAccountsCommand.UpdateEmployer(employer1);

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   IsEnabled = true,
                                   IsDisabled = false
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(2, employers.Count);
            AssertEmployer(employer2, employers[0]);
            AssertEmployer(employer3, employers[1]);
        }

        [TestMethod]
        public void TestDisabledOnly()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer(3, _organisationsCommand.CreateTestOrganisation(0));

            employer1.IsEnabled = false;
            _employerAccountsCommand.UpdateEmployer(employer1);

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   IsEnabled = false,
                                   IsDisabled = true
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(1, employers.Count);
            AssertEmployer(employer1, employers[0]);
        }

        [TestMethod]
        public void TestEnabledAndDisabled()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(0));
            var employer3 = _employerAccountsCommand.CreateTestEmployer(3, _organisationsCommand.CreateTestOrganisation(0));

            employer1.IsEnabled = false;
            _employerAccountsCommand.UpdateEmployer(employer1);

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   IsEnabled = true,
                                   IsDisabled = true
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(3, employers.Count);
            AssertEmployer(employer1, employers[0]);
            AssertEmployer(employer2, employers[1]);
            AssertEmployer(employer3, employers[2]);
        }

        [TestMethod]
        public void TestNoResults()
        {
            _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer(3, _organisationsCommand.CreateTestOrganisation(0));

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   EmailAddress = "something.com",
                                   IsEnabled = true,
                                   IsDisabled = true
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(0, employers.Count);
        }

        [TestMethod]
        public void TestEmailAddress()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer("employer1", FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));
            employer1.EmailAddress = new EmailAddress { Address = "employer1@test.com" };
            _employerAccountsCommand.UpdateEmployer(employer1);

            _employerAccountsCommand.CreateTestEmployer(LoginId, FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));

            var employer3 = _employerAccountsCommand.CreateTestEmployer("employer3", FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));
            employer3.EmailAddress = new EmailAddress { Address = "employer3@test.com" };
            _employerAccountsCommand.UpdateEmployer(employer3);

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   EmailAddress = "test.com",
                                   IsEnabled = true,
                                   IsDisabled = true
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(2, employers.Count);
            AssertEmployer(employer1, employers[0]);
            AssertEmployer(employer3, employers[1]);
        }

        [TestMethod]
        public void TestFirstName()
        {
            _employerAccountsCommand.CreateTestEmployer("employer1", FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer("employer2", "Monty", LastName, _organisationsCommand.CreateTestOrganisation(0));
            var employer3 = _employerAccountsCommand.CreateTestEmployer("employer3", "Monty", "Burns", _organisationsCommand.CreateTestOrganisation(0));

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   FirstName = "Mon",
                                   IsEnabled = true,
                                   IsDisabled = true
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(2, employers.Count);
            AssertEmployer(employer2, employers[0]);
            AssertEmployer(employer3, employers[1]);
        }

        [TestMethod]
        public void TestLastName()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer("employer1", FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer("employer2", "Monty", LastName, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer("employer3", "Monty", "Burns", _organisationsCommand.CreateTestOrganisation(0));

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   LastName = "mith",
                                   IsEnabled = true,
                                   IsDisabled = true
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(2, employers.Count);
            AssertEmployer(employer1, employers[0]);
            AssertEmployer(employer2, employers[1]);
        }

        [TestMethod]
        public void TestLoginId()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer("employer1", FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer("employer2", "Monty", LastName, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer("mburns", "Monty", "Burns", _organisationsCommand.CreateTestOrganisation(0));

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   LoginId = "employer",
                                   IsEnabled = true,
                                   IsDisabled = true
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(2, employers.Count);
            AssertEmployer(employer1, employers[0]);
            AssertEmployer(employer2, employers[1]);
        }

        [TestMethod]
        public void TestCount()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer("employer1", FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer("employer2", "Monty", LastName, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer("mburns", "Monty", "Burns", _organisationsCommand.CreateTestOrganisation(0));

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   LoginId = "employer",
                                   IsEnabled = true,
                                   IsDisabled = true,
                                   Count = 1
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(1, employers.Count);
            AssertEmployer(employer1, employers[0]);
        }

        [TestMethod]
        public void TestOrganisationName()
        {
            _employerAccountsCommand.CreateTestEmployer("employer1", FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer("employer2", FirstName, LastName, _organisationsCommand.CreateTestOrganisation("Acme Pty Ltd"));
            var employer3 = _employerAccountsCommand.CreateTestEmployer("employer3", FirstName, LastName, _organisationsCommand.CreateTestOrganisation("Acme Pty Ltd"));

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   OrganisationName = "Acme",
                                   IsEnabled = true,
                                   IsDisabled = true
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(2, employers.Count);
            AssertEmployer(employer2, employers[0]);
            AssertEmployer(employer3, employers[1]);
        }

        [TestMethod]
        public void TestOrganisationCount()
        {
            _employerAccountsCommand.CreateTestEmployer("employer1", FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer("employer2", FirstName, LastName, _organisationsCommand.CreateTestOrganisation("Acme Pty Ltd"));
            _employerAccountsCommand.CreateTestEmployer("employer3", FirstName, LastName, _organisationsCommand.CreateTestOrganisation("Acme Pty Ltd"));

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   OrganisationName = "Acme",
                                   IsEnabled = true,
                                   IsDisabled = true,
                                   Count = 1
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(1, employers.Count);
            AssertEmployer(employer2, employers[0]);
        }

        [TestMethod]
        public void TestSortOrder()
        {
            _employerAccountsCommand.CreateTestEmployer("employer1", FirstName, LastName, _organisationsCommand.CreateTestOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer("employer2", FirstName, LastName, _organisationsCommand.CreateTestOrganisation("Acme 3 Pty Ltd"));
            var employer3 = _employerAccountsCommand.CreateTestEmployer("employer3", FirstName, LastName, _organisationsCommand.CreateTestOrganisation("Acme 2 Pty Ltd"));

            // Default.

            var criteria = new AdministrativeEmployerSearchCriteria
                               {
                                   OrganisationName = "Acme",
                                   IsEnabled = true,
                                   IsDisabled = true,
                               };
            var employers = _executeEmployerSearchCommand.Search(criteria);
            
            Assert.AreEqual(2, employers.Count);
            AssertEmployer(employer2, employers[0]);
            AssertEmployer(employer3, employers[1]);

            // LoginId

            criteria = new AdministrativeEmployerSearchCriteria
                           {
                               OrganisationName = "Acme",
                               IsEnabled = true,
                               IsDisabled = true,
                               SortOrder = EmployerSortOrder.LoginId
                           };
            employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(2, employers.Count);
            AssertEmployer(employer2, employers[0]);
            AssertEmployer(employer3, employers[1]);

            // OrganisationName LoginId

            criteria = new AdministrativeEmployerSearchCriteria
                           {
                               OrganisationName = "Acme",
                               IsEnabled = true,
                               IsDisabled = true,
                               SortOrder = EmployerSortOrder.OrganisationNameLoginId
                           };
            employers = _executeEmployerSearchCommand.Search(criteria);

            Assert.AreEqual(2, employers.Count);
            AssertEmployer(employer3, employers[0]);
            AssertEmployer(employer2, employers[1]);
        }

        private static void AssertEmployer(IEmployer expectedEmployer, IEmployer employer)
        {
            Assert.AreEqual(expectedEmployer.Id, employer.Id);
            Assert.AreEqual(expectedEmployer.EmailAddress, employer.EmailAddress);
            Assert.AreEqual(expectedEmployer.FirstName, employer.FirstName);
            Assert.AreEqual(expectedEmployer.LastName, employer.LastName);
            Assert.AreEqual(expectedEmployer.Organisation.FullName, employer.Organisation.FullName);
            Assert.AreEqual(expectedEmployer.IsEnabled, employer.IsEnabled);
            Assert.AreEqual(expectedEmployer.IsActivated, employer.IsActivated);
        }
    }
}
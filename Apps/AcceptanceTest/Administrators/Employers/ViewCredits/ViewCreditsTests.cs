using System;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.ViewCredits
{
    [TestClass]
    public abstract class ViewCreditsTests
        : EmployersTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestContactCredits()
        {
            TestCredits(_creditsQuery.GetCredit<ContactCredit>().Id);
        }

        [TestMethod]
        public void TestApplicantCredits()
        {
            TestCredits(_creditsQuery.GetCredit<ApplicantCredit>().Id);
        }

        [TestMethod]
        public void TestJobAdCredits()
        {
            TestCredits(_creditsQuery.GetCredit<JobAdCredit>().Id);
        }

        private void TestCredits(Guid creditId)
        {
            var employer = CreateEmployer(creditId);

            // Search.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            Get(GetCreditsUrl(employer));

            // Assert.

            AssertCredits();
            AssertExpiryDate();
        }

        private void AssertCredits()
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//table[@class='list']/tbody/tr[position()=1]/td[position()=3]");

            if (HasAllocation)
            {
                var credits = node.InnerText;
                if (ExpectedCredits == null)
                    Assert.AreEqual("unlimited", credits);
                else
                    Assert.AreEqual(ExpectedCredits.Value.ToString(), credits);
            }
            else
            {
                Assert.IsNull(node);
            }
        }

        private void AssertExpiryDate()
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//table[@class='list']/tbody/tr[position()=1]/td[position()=5]");

            if (HasAllocation)
            {
                var expiryDate = node.InnerText;
                if (ExpectedExpiryDate == null)
                    Assert.AreEqual("never", expiryDate);
                else
                    Assert.AreEqual(ExpectedExpiryDate.Value.ToShortDateString(), expiryDate);
            }
            else
            {
                Assert.IsNull(node);
            }
        }

        protected abstract Employer CreateEmployer(Guid creditId);
        protected abstract bool HasAllocation { get; }
        protected abstract int? ExpectedCredits { get; }
        protected abstract DateTime? ExpectedExpiryDate { get; }

        protected Employer CreateEmployer(bool verified)
        {
            return _employerAccountsCommand.CreateTestEmployer(1, verified ? _organisationsCommand.CreateTestVerifiedOrganisation(0) : _organisationsCommand.CreateTestOrganisation(0));
        }

        protected Employer CreateEmployer(bool verified, Guid creditId, DateTime? expiryDate, int? quantity)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(1, verified ? _organisationsCommand.CreateTestVerifiedOrganisation(0) : _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = creditId, ExpiryDate = expiryDate, InitialQuantity = quantity });
            return employer;
        }
    }
}
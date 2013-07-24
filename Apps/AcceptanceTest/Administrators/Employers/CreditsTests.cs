using System;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers
{
    [TestClass]
    public abstract class CreditsTests
        : EmployersTests
    {
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        protected readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        protected readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

        [TestInitialize]
        public void CreditsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected void AssertAllocation(Credit credit, DateTime? expiryDate, int? quantity)
        {
            AssertPageContains(credit.ShortDescription);
            AssertPageContains(expiryDate == null ? "never" : expiryDate.Value.ToShortDateString());
            AssertPageContains(quantity == null ? "unlimited" : quantity.ToString());
        }
    }
}

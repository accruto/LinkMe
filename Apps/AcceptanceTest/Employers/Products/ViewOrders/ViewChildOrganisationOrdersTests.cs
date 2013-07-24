using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.ViewOrders
{
    [TestClass]
    public class ViewChildOrganisationOrdersTests
        : ViewOrdersTests
    {
        protected override Employer CreateEmployer()
        {
            var administratorId = Guid.NewGuid();
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administratorId);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, administratorId);
            return _employerAccountsCommand.CreateTestEmployer(0, organisation);
        }

        protected override IOrganisation GetOrderOrganisation(Employer employer)
        {
            return employer.Organisation;
        }
    }
}
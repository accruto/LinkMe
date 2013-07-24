using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.ViewOrders
{
    [TestClass]
    public class ViewUnverifiedOrganisationOrdersTests
        : ViewOrdersTests
    {
        protected override Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected override IOrganisation GetOrderOrganisation(Employer employer)
        {
            return null;
        }
    }
}
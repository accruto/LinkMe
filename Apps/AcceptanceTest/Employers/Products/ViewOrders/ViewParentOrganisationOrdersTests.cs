using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.ViewOrders
{
    [TestClass]
    public class ViewParentOrganisationOrdersTests
        : ViewOrdersTests
    {
        private VerifiedOrganisation _parentOrganisation;

        protected override Employer CreateEmployer()
        {
            var administratorId = Guid.NewGuid();
            _parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administratorId);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, _parentOrganisation, administratorId);
            return _employerAccountsCommand.CreateTestEmployer(0, organisation);
        }

        protected override IOrganisation GetOrderOrganisation(Employer employer)
        {
            return _parentOrganisation;
        }

        [TestMethod]
        public void TestEmployerOrganisationsOrders()
        {
            var employer = CreateEmployer();

            var product1 = _productsQuery.GetProduct("Contacts5");
            var product2 = _productsQuery.GetProduct("Applicants20");
            var product3 = _productsQuery.GetProduct("Applicants200");

            var order1 = PurchaseOrder(employer.Id, employer, product1);
            var order2 = PurchaseOrder(employer.Organisation.Id, employer, product2);
            var order3 = PurchaseOrder(_parentOrganisation.Id, employer, product3);

            // Check credits.

            LogIn(employer);
            Get(_creditsUrl);

            var order1Url = GetOrderUrl(order1.Id);
            AssertEmployerCredits(order1, order1Url, product1.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product1).Duration.Value).Date);

            AssertOrganisationCredits(product2.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product2).Duration.Value).Date);
            AssertOrganisationCredits(product3.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product3).Duration.Value).Date);

            // Check order.

            var order2Url = GetOrderUrl(order2.Id);
            Get(HttpStatusCode.NotFound, order2Url);
            AssertPageContains("cannot be found");

            var order3Url = GetOrderUrl(order3.Id);
            Get(HttpStatusCode.NotFound, order3Url);
            AssertPageContains("cannot be found");

            Get(_searchUrl);
            var creditAdjustments = Combine(product1.CreditAdjustments, product2.CreditAdjustments);
            creditAdjustments = Combine(creditAdjustments, product3.CreditAdjustments);
            AssertCreditSummary(creditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product3).Duration.Value).Date);
        }
    }
}
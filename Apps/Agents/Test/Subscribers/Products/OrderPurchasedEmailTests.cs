using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Presentation.Domain.Products;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Subscribers.Products
{
    [TestClass]
    public class OrderPurchasedEmailTests
        : ProductEmailTests
    {
        [TestMethod]
        public void TestOrderReceiptEmail()
        {
            var product = GetProduct<ContactCredit>();

            // Purchase.

            var owner = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            var creditCard = CreateCreditCard();
            var order = _ordersCommand.PrepareOrder(new[] { product.Id }, null, null, creditCard.CardType);
            _ordersCommand.PurchaseOrder(owner.Id, order, new Purchaser { Id = owner.Id }, creditCard);

            // Assert.

            var email = _emailServer.AssertEmailSent();
            email.AssertSubject("Receipt for Order # " + order.ConfirmationCode);
        }

        private Product GetProduct<T>()
            where T : Credit
        {
            // Select the first product that has a single credit adjustment for the given type.

            var credit = _creditsQuery.GetCredit<T>();
            return (from p in _productsQuery.GetProducts()
            where p.CreditAdjustments.Count == 1
                && p.GetPrimaryCreditAdjustment().CreditId == credit.Id
            select p).First();
        }
    }
}
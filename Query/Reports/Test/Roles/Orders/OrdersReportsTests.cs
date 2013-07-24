using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Data;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Orders.Data;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Orders;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Roles.Orders;
using LinkMe.Query.Reports.Roles.Orders.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Roles.Orders
{
    [TestClass]
    public class OrdersReportsTests
        : TestClass
    {
        private readonly IOrderReportsQuery _orderReportsQuery = Resolve<IOrderReportsQuery>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly IProductsQuery _productsQuery = Resolve<IProductsQuery>();
        private readonly IPurchasesCommand _purchasesCommand;
        private readonly IOrdersCommand _ordersCommand;
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        public OrdersReportsTests()
        {
            _purchasesCommand = CreatePurchasesCommand();
            _ordersCommand = CreateOrdersCommand(_purchasesCommand);
        }

        [TestMethod]
        public void TestOrder()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            Assert.AreEqual(0, _orderReportsQuery.GetOrderReports(DayRange.Today).Count);

            var product = _productsQuery.GetProduct("Contacts5");
            var order = _ordersCommand.PrepareOrder(new[] {product.Id}, null, null, CreditCardType.MasterCard);
            _ordersCommand.PurchaseOrder(employer.Id, order, CreatePurchaser(employer.Id), CreateCreditCard());

            AssertOrders(new[] {order}, new[] {employer});
        }

        [TestMethod]
        public void Test2ProductsOrder()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            var product1 = _productsQuery.GetProduct("Contacts5");
            var product2 = _productsQuery.GetProduct("Applicants20");
            var order = _ordersCommand.PrepareOrder(new[] { product1.Id, product2.Id }, null, null, CreditCardType.MasterCard);
            _ordersCommand.PurchaseOrder(employer.Id, order, CreatePurchaser(employer.Id), CreateCreditCard());

            AssertOrders(new[] { order }, new[] { employer });
        }

        [TestMethod]
        public void Test2Orders()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            var product1 = _productsQuery.GetProduct("Contacts5");
            var product2 = _productsQuery.GetProduct("Applicants20");

            var order1 = _ordersCommand.PrepareOrder(new[] { product1.Id }, null, null, CreditCardType.MasterCard);
            _ordersCommand.PurchaseOrder(employer.Id, order1, CreatePurchaser(employer.Id), CreateCreditCard());

            var order2 = _ordersCommand.PrepareOrder(new[] { product2.Id }, null, null, CreditCardType.MasterCard);
            _ordersCommand.PurchaseOrder(employer.Id, order2, CreatePurchaser(employer.Id), CreateCreditCard());

            AssertOrders(new[] { order1, order2 }, new[] { employer, employer });
        }

        [TestMethod]
        public void Test2Orders2Employers()
        {
            var employer1 = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var employer2 = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            var product1 = _productsQuery.GetProduct("Contacts5");
            var product2 = _productsQuery.GetProduct("Applicants20");

            var order1 = _ordersCommand.PrepareOrder(new[] { product1.Id }, null, null, CreditCardType.MasterCard);
            _ordersCommand.PurchaseOrder(employer1.Id, order1, CreatePurchaser(employer1.Id), CreateCreditCard());

            var order2 = _ordersCommand.PrepareOrder(new[] { product2.Id }, null, null, CreditCardType.MasterCard);
            _ordersCommand.PurchaseOrder(employer2.Id, order2, CreatePurchaser(employer2.Id), CreateCreditCard());

            AssertOrders(new[] {order1, order2}, new[] {employer1, employer2});
        }

        private void AssertOrders(IList<Order> expectedOrders, IList<IEmployer> expectedEmployers)
        {
            var orders = _orderReportsQuery.GetOrderReports(DayRange.Today);
            Assert.AreEqual(expectedOrders.Count, orders.Count);

            for (var index = 0; index < orders.Count; ++index)
            {
                var expectedOrder = expectedOrders[index];
                AssertOrder((from o in orders where o.Id == expectedOrder.Id select o).Single(), expectedOrder, expectedEmployers[index]);
            }
        }

        private void AssertOrder(OrderReport report, Order expectedOrder, IEmployer expectedEmployer)
        {
            Assert.AreEqual(expectedEmployer.FullName, report.ClientName);
            Assert.AreEqual(expectedEmployer.Organisation.Name, report.OrganisationName);
            Assert.AreEqual(expectedOrder.AdjustedPrice, report.Price);
            Assert.AreEqual(expectedOrder.Items.Count, report.Products.Length);

            foreach (var item in expectedOrder.Items)
                Assert.IsTrue(report.Products.Contains(_productsQuery.GetProduct(item.ProductId).Name));
        }

        private static CreditCard CreateCreditCard()
        {
            return new CreditCard
            {
                CardNumber = "4444333322221111",
                Cvv = "123",
                ExpiryDate = new ExpiryDate(DateTime.Now.Date.AddYears(1)),
            };
        }

        private static IPurchasesCommand CreatePurchasesCommand()
        {
            return new MockPurchasesCommand(new MockAdjustmentPersister());
        }

        private IOrdersCommand CreateOrdersCommand(IPurchasesCommand purchasesCommand)
        {
            return new OrdersCommand(
                new OrdersRepository(Resolve<IDataContextFactory>(), new MockAdjustmentPersister()),
                new OrderPricesCommand(Resolve<ICreditCardsQuery>(), new MockAdjustmentPersister()),
                _productsQuery,
                Resolve<IAllocationsCommand>(),
                _allocationsQuery,
                purchasesCommand);
        }

        private static Purchaser CreatePurchaser(Guid id)
        {
            return new Purchaser
            {
                Id = id,
                IpAddress = "127.0.0.1",
                EmailAddress = "bill@test.linkme.net.au",
            };
        }
    }
}

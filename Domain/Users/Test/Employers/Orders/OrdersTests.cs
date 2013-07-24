using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Data;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Orders.Data;
using LinkMe.Domain.Roles.Test.Orders;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Orders.Commands;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Orders
{
    [TestClass]
    public class OrdersTests
        : TestClass
    {
        private const int BundledDiscount = 10;
        private IEmployerOrdersCommand _employerOrdersCommand;
        private readonly IEmployerOrdersQuery _employerOrdersQuery = Resolve<IEmployerOrdersQuery>();

        [TestInitialize]
        public void OrdersTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _employerOrdersCommand = new EmployerOrdersCommand(CreateOrdersCommand(), BundledDiscount);
        }

        [TestMethod]
        public void TestPrepareContactOrder()
        {
            var product = _employerOrdersQuery.GetContactProducts()[0];
            Assert.AreEqual(180, product.Price);

            var order = _employerOrdersCommand.PrepareOrder(new[] { product.Id }, null, null, CreditCardType.Visa);

            // Includes GST.

            AssertOrder(order, 180, 198);
        }

        [TestMethod]
        public void TestPrepareContactOrderWithDiscount()
        {
            var product = _employerOrdersQuery.GetContactProducts()[0];
            Assert.AreEqual(180, product.Price);

            var discount = new Discount {Percentage = (decimal) 0.3};
            var order = _employerOrdersCommand.PrepareOrder(new[] { product.Id }, null, discount, CreditCardType.Visa);

            // Includes GST.

            AssertOrder(order, 180, (decimal) 138.6);
        }

        [TestMethod]
        public void TestPrepareApplicantOrder()
        {
            var product = _employerOrdersQuery.GetApplicantProducts()[0];
            Assert.AreEqual(200, product.Price);

            var order = _employerOrdersCommand.PrepareOrder(new[] { product.Id }, null, null, CreditCardType.Visa);

            // Includes GST.

            AssertOrder(order, 200, 220);
        }

        [TestMethod]
        public void TestPrepareBundledOrder()
        {
            var product1 = _employerOrdersQuery.GetContactProducts()[0];
            Assert.AreEqual(180, product1.Price);
            var product2 = _employerOrdersQuery.GetApplicantProducts()[0];
            Assert.AreEqual(200, product2.Price);

            var order = _employerOrdersCommand.PrepareOrder(new[] { product1.Id, product2.Id }, null, null, CreditCardType.Visa);

            // Includes bundle discount and GST.

            AssertOrder(order, 380, (decimal) 376.2);
        }

        [TestMethod]
        public void TestPrepareBundledOrderWithDiscount()
        {
            var product1 = _employerOrdersQuery.GetContactProducts()[0];
            Assert.AreEqual(180, product1.Price);
            var product2 = _employerOrdersQuery.GetApplicantProducts()[0];
            Assert.AreEqual(200, product2.Price);

            var discount = new Discount { Percentage = (decimal)0.3 };
            var order = _employerOrdersCommand.PrepareOrder(new[] { product1.Id, product2.Id }, null, discount, CreditCardType.Visa);

            // Includes bundle discount and GST.

            AssertOrder(order, 380, (decimal)250.8);
        }

        private static void AssertOrder(Order order, decimal price, decimal adjustedPrice)
        {
            Assert.AreEqual(price, order.Price);
            Assert.AreEqual(adjustedPrice, order.AdjustedPrice);
        }

        private static IOrdersCommand CreateOrdersCommand()
        {
            return new OrdersCommand(
                new OrdersRepository(Resolve<IDataContextFactory>(), new MockAdjustmentPersister()),
                new OrderPricesCommand(Resolve<ICreditCardsQuery>(), new MockAdjustmentPersister()),
                Resolve<IProductsQuery>(),
                Resolve<IAllocationsCommand>(),
                Resolve<IAllocationsQuery>(),
                CreatePurchasesCommand());
        }

        private static IPurchasesCommand CreatePurchasesCommand()
        {
            return new MockPurchasesCommand(new MockAdjustmentPersister());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Data;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Commands;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Orders.Data;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Orders
{
    [TestClass]
    public abstract class OrdersTests
        : TestClass
    {
        protected const string ProductNameFormat = "Product{0}";
        private const decimal TaxRate = (decimal)0.1;

        protected ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        protected IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        protected IProductsQuery _productsQuery = Resolve<IProductsQuery>();
        protected IProductsCommand _productsCommand = Resolve<IProductsCommand>();
        private readonly IProductsRepository _productsRepository = Resolve<IProductsRepository>();
        protected IOrderPricesCommand _orderPricesCommand;
        protected IPurchasesCommand _purchasesCommand;
        protected IOrdersCommand _ordersCommand;
        protected IOrdersQuery _ordersQuery = Resolve<IOrdersQuery>();

        [TestInitialize]
        public void OrdersTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _orderPricesCommand = CreateOrderPricesCommand();
            _purchasesCommand = CreatePurchasesCommand();
            _ordersCommand = CreateOrdersCommand(_purchasesCommand);
        }

        protected void DeleteProduct(int index)
        {
            // If it already exists then delete it.

            var name = string.Format(ProductNameFormat, index);
            var product = _productsQuery.GetProduct(name);
            if (product != null)
              _productsRepository.DeleteProduct(product.Id);
        }

        protected Product CreateProduct(int index, UserType userType, decimal price, TimeSpan? duration, params ProductCreditAdjustment[] adjustments)
        {
            DeleteProduct(index);

            var product = new Product
            {
                Name = string.Format(ProductNameFormat, index),
                UserTypes = userType,
                Price = price,
                Currency = Currency.AUD,
                CreditAdjustments = adjustments == null || adjustments.Length == 0 ? null : adjustments.ToList()
            };
            _productsCommand.CreateProduct(product);
            return product;
        }

        private static IOrderPricesCommand CreateOrderPricesCommand()
        {
            return new OrderPricesCommand(Resolve<ICreditCardsQuery>(), new MockAdjustmentPersister());
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

        protected Product GetContactProduct()
        {
            // Select the first product that has a single credit adjustment for the given type.

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            return (from p in _productsQuery.GetProducts()
                    where p.CreditAdjustments.Count == 1
                          && p.CreditAdjustments[0].CreditId == credit.Id
                    select p).First();
        }

        protected Product GetApplicantProduct()
        {
            // Select the first product that has credit adjustments for both types.

            var credit1 = _creditsQuery.GetCredit<ApplicantCredit>();
            var credit2 = _creditsQuery.GetCredit<JobAdCredit>();
            return (from p in _productsQuery.GetProducts()
                    where p.CreditAdjustments.Count == 2
                          &&
                          (
                              (p.CreditAdjustments[0].CreditId == credit1.Id && p.CreditAdjustments[1].CreditId == credit2.Id)
                              ||
                              (p.CreditAdjustments[0].CreditId == credit2.Id && p.CreditAdjustments[1].CreditId == credit1.Id)
                          )
                    select p).First();
        }

        protected void AssertAllocations(Guid ownerId, Guid orderId, params ProductCreditAdjustment[] adjustments)
        {
            var allocations = _allocationsQuery.GetActiveAllocations(ownerId);
            Assert.AreEqual(adjustments.Length, allocations.Count);

            foreach (var adjustment in adjustments)
            {
                var creditId = adjustment.CreditId;
                var quantity = adjustment.Quantity;

                var allocation = (from a in allocations
                                  where a.CreditId == creditId && a.InitialQuantity == quantity
                                  select a).Single();
                Assert.AreEqual(adjustment.CreditId, allocation.CreditId);
                Assert.AreEqual(DateTime.Now.Add(adjustment.Duration.Value).Date, allocation.ExpiryDate);
                Assert.AreEqual(orderId, allocation.ReferenceId);
            }
        }

        protected void AssertOrders(Guid ownerId, Coupon coupon, Discount discount, params Product[] products)
        {
            var orders = _ordersQuery.GetOrders(ownerId);
            Assert.AreEqual(1, orders.Count);
            var order = orders[0];
            AssertOrder(order, coupon, discount, products);

            // Use the id and code.

            order = _ordersQuery.GetOrder(order.Id);
            AssertOrder(order, coupon, discount, products);
            order = _ordersQuery.GetOrder(order.ConfirmationCode);
            AssertOrder(order, coupon, discount, products);
        }

        private static void AssertOrder(Order order, Coupon coupon, Discount discount, params Product[] products)
        {
            Assert.AreEqual(GetPrice(products), order.Price);
            Assert.AreEqual(products[0].Currency, order.Currency);
            Assert.AreEqual(GetAdjustedPrice(products, coupon, discount), order.AdjustedPrice);

            Assert.AreEqual(products.Length, order.Items.Count);
            foreach (var product in products)
            {
                var productId = product.Id;
                var item = (from i in order.Items where i.ProductId == productId select i).Single();
                Assert.AreEqual(product.Price, item.Price);
                Assert.AreEqual(product.Currency, item.Currency);
            }

            if (order.Adjustments.Count == 1)
            {
                Assert.AreEqual(1, order.Adjustments.Count);

                var adjustment = order.Adjustments[0];
                Assert.IsInstanceOfType(adjustment, typeof(TaxAdjustment));
                var taxAdjustment = (TaxAdjustment) adjustment;
                Assert.AreEqual((decimal)0.1, taxAdjustment.TaxRate);
            }
            else if (order.Adjustments.Count == 2)
            {
                Assert.AreEqual(2, order.Adjustments.Count);

                var adjustment = order.Adjustments[0];
                Assert.IsInstanceOfType(adjustment, typeof(CouponAdjustment));
                var couponAdjustment = (CouponAdjustment) adjustment;
                Assert.AreEqual(coupon.Code, couponAdjustment.CouponCode);
                if (coupon.Discount is PercentageCouponDiscount)
                    Assert.AreEqual(((PercentageCouponDiscount)coupon.Discount).Percentage, ((PercentageAdjustmentAmount)couponAdjustment.Amount).PercentageChange);
                else
                    Assert.AreEqual(((FixedCouponDiscount)coupon.Discount).Amount, ((FixedAdjustmentAmount)couponAdjustment.Amount).FixedChange);
                var productId = coupon.ProductIds == null ? (Guid?)null : (from p in products where coupon.ProductIds.Contains(p.Id) select p.Id).SingleOrDefault();
                Assert.AreEqual(productId, couponAdjustment.ProductId);
                
                adjustment = order.Adjustments[1];
                Assert.IsInstanceOfType(adjustment, typeof(TaxAdjustment));
                var taxAdjustment = (TaxAdjustment)adjustment;
                Assert.AreEqual((decimal)0.1, taxAdjustment.TaxRate);
            }
            else
            {
                Assert.AreEqual(3, order.Adjustments.Count);

                var adjustment = order.Adjustments[0];
                Assert.IsInstanceOfType(adjustment, typeof(CouponAdjustment));
                var couponAdjustment = (CouponAdjustment)adjustment;
                Assert.AreEqual(coupon.Code, couponAdjustment.CouponCode);
                Assert.AreEqual(((PercentageCouponDiscount)coupon.Discount).Percentage, ((PercentageAdjustmentAmount)couponAdjustment.Amount).PercentageChange);
                var productId = coupon.ProductIds == null ? (Guid?)null : (from p in products where coupon.ProductIds.Contains(p.Id) select p.Id).SingleOrDefault();
                Assert.AreEqual(productId, couponAdjustment.ProductId);

                adjustment = order.Adjustments[1];
                Assert.IsInstanceOfType(adjustment, typeof(BundleAdjustment));
                var bundleAdjustment = (BundleAdjustment) adjustment;
                Assert.AreEqual(discount.Percentage, bundleAdjustment.Percentage);

                adjustment = order.Adjustments[2];
                Assert.IsInstanceOfType(adjustment, typeof(TaxAdjustment));
                var taxAdjustment = (TaxAdjustment)adjustment;
                Assert.AreEqual((decimal)0.1, taxAdjustment.TaxRate);
            }
        }

        private static decimal GetPrice(IEnumerable<Product> products)
        {
            // Add them all up.

            return products.Sum(product => product.Price);
        }

        private static decimal GetAdjustedPrice(IEnumerable<Product> products, Coupon coupon, Discount discount)
        {
            // Add them all up taking into account coupon discount.

            decimal price = 0;
            decimal adjustedPrice = 0;
            foreach (var product in products)
            {
                price += product.Price;

                if (coupon != null && coupon.ProductIds != null && coupon.ProductIds.Contains(product.Id))
                    adjustedPrice += product.Price - GetDiscount(product.Price, coupon.Discount);
                else
                    adjustedPrice += product.Price;
            }

            // Coupon may be applied to entire order.

            if (coupon != null && (coupon.ProductIds == null || coupon.ProductIds.Count == 0))
                adjustedPrice = price - GetDiscount(price, coupon.Discount);

            if (discount != null)
            {
                // Discount applies to the total price.

                adjustedPrice = adjustedPrice - GetDiscount(price, discount.Percentage);
            }

            // Tax.

            return Math.Round(adjustedPrice + adjustedPrice * TaxRate, 2);
        }

        private static decimal GetDiscount(decimal price, CouponDiscount discount)
        {
            return discount is PercentageCouponDiscount
                ? GetDiscount(price, ((PercentageCouponDiscount) discount).Percentage)
                : Math.Round(((FixedCouponDiscount) discount).Amount, 2);
        }

        private static decimal GetDiscount(decimal price, decimal percentage)
        {
            return Math.Round(price * percentage, 2);
        }

        protected void AssertReceipt(Guid orderId, CreditCardReceipt expectedPurchaseReceipt)
        {
            var receipts = _ordersQuery.GetReceipts(orderId);
            Assert.AreEqual(1, receipts.Count);
            var receipt = receipts[0];
            Assert.IsInstanceOfType(receipt, typeof(CreditCardReceipt));
            var purchaseReceipt = (CreditCardReceipt)receipt;

            AssertReceipt(expectedPurchaseReceipt, purchaseReceipt);
        }

        private static void AssertReceipt(CreditCardReceipt expectedReceipt, CreditCardReceipt receipt)
        {
            Assert.AreEqual(expectedReceipt.Id, receipt.Id);
            Assert.AreEqual(expectedReceipt.ExternalTransactionId, receipt.ExternalTransactionId);
            Assert.AreEqual(expectedReceipt.CreditCard.Pan, receipt.CreditCard.Pan);
            Assert.AreEqual(expectedReceipt.CreditCard.Type, receipt.CreditCard.Type);
        }

        protected static void AssertReceipt(Receipt expectedReceipt, Receipt receipt)
        {
            Assert.AreEqual(expectedReceipt.Id, receipt.Id);
            Assert.AreEqual(expectedReceipt.ExternalTransactionId, receipt.ExternalTransactionId);
        }

        protected static CreditCard CreateCreditCard(CreditCardType creditCardType)
        {
            return new CreditCard
            {
                CardNumber = "4444333322221111",
                Cvv = "123",
                ExpiryDate = new ExpiryDate(DateTime.Now.Date.AddYears(1)),
                CardType = creditCardType,
            };
        }
    }
}
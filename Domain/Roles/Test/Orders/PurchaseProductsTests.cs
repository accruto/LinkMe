using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Orders
{
    [TestClass]
    public class PurchaseProductsTests
        : OrdersTests
    {
        [TestMethod]
        public void TestPurchaseContactCredits()
        {
            var product = GetContactProduct();

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var order = _ordersCommand.PrepareOrder(new[] { product.Id }, null, null, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product.CreditAdjustments[0]);
            AssertOrders(ownerId, null, null, product);
            AssertReceipt(order.Id, receipt);
        }

        [TestMethod]
        public void TestPurchaseApplicantCredits()
        {
            var product = GetApplicantProduct();

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var order = _ordersCommand.PrepareOrder(new[] { product.Id }, null, null, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product.CreditAdjustments[0], product.CreditAdjustments[1]);
            AssertOrders(ownerId, null, null, product);
            AssertReceipt(order.Id, receipt);
        }

        [TestMethod]
        public void TestMultipleProducts()
        {
            var product1 = GetContactProduct();
            var product2 = GetApplicantProduct();

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var order = _ordersCommand.PrepareOrder(new[] { product1.Id, product2.Id }, null, null, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product1.CreditAdjustments[0], product2.CreditAdjustments[0], product2.CreditAdjustments[1]);
            AssertOrders(ownerId, null, null, product1, product2);
            AssertReceipt(order.Id, receipt);
        }

        [TestMethod]
        public void TestRefund()
        {
            const decimal price = 100;

            var credit = _creditsQuery.GetCredits()[0];
            var product = CreateProduct(1, price, credit, null, null);
            var ownerId = Guid.NewGuid();

            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var order = _ordersCommand.PrepareOrder(new[] { product.Id }, null, null, creditCard.CardType);
            var expectedPurchaseReceipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard);
            var orders = _ordersQuery.GetOrders(ownerId);
            Assert.AreEqual(1, orders.Count);
            order = orders[0];

            var receipts = _ordersQuery.GetReceipts(order.Id);
            Assert.AreEqual(1, receipts.Count);
            var receipt = receipts[0];
            Assert.IsInstanceOfType(receipt, typeof(CreditCardReceipt));
            var expectedCreditCardReceipt = (CreditCardReceipt) expectedPurchaseReceipt;

            // Refund.

            var expectedRefundReceipt = _ordersCommand.RefundOrder(order.Id);

            // Assert allocation.

            var allocations = _allocationsQuery.GetAllocationsByOwnerId(ownerId);
            Assert.AreEqual(1, allocations.Count);
            var allocation = allocations[0];
            Assert.AreEqual(credit.Id, allocation.CreditId);
            Assert.IsNull(allocation.ExpiryDate);
            Assert.AreEqual(null, allocation.InitialQuantity);
            Assert.AreEqual(null, allocation.RemainingQuantity);
            Assert.IsNotNull(allocation.DeallocatedTime);

            allocations = _allocationsQuery.GetActiveAllocations(ownerId);
            Assert.AreEqual(0, allocations.Count);

            // Assert receipts.

            receipts = _ordersQuery.GetReceipts(order.Id);
            Assert.AreEqual(2, receipts.Count);

            CreditCardReceipt creditCardReceipt;
            RefundReceipt refundReceipt;

            receipt = receipts[0];
            if (receipt is CreditCardReceipt)
            {
                creditCardReceipt = receipt as CreditCardReceipt;
                refundReceipt = receipts[1] as RefundReceipt;
            }
            else
            {
                creditCardReceipt = receipts[1] as CreditCardReceipt;
                refundReceipt = receipt as RefundReceipt;
            }

            AssertReceipt(expectedCreditCardReceipt, creditCardReceipt);
            AssertReceipt(expectedRefundReceipt, refundReceipt);
        }

        [TestMethod]
        public void TestPricesVisa()
        {
            TestPrices(CreditCardType.Visa, 0, 330, 363);
        }

        [TestMethod]
        public void TestPricesMasterCard()
        {
            TestPrices(CreditCardType.MasterCard, 0, 330, 363);
        }

        [TestMethod]
        public void TestPricesAmex()
        {
            TestPrices(CreditCardType.Amex, 0, 330, (decimal) 372.08);
        }

        [TestMethod]
        public void TestDiscount()
        {
            TestPrices(CreditCardType.Visa, (decimal)0.2, 330, (decimal) 290.4);
        }
        
        [TestMethod]
        public void TestDiscountAmex()
        {
            TestPrices(CreditCardType.Amex, (decimal)0.2, 330, (decimal)297.66);
        }

        private void TestPrices(CreditCardType creditCardType, decimal discount, decimal price, decimal adjustedPrice)
        {
            var product = GetContactProduct();

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(creditCardType);
            var order = _ordersCommand.PrepareOrder(new[] { product.Id }, null, discount == 0 ? null : new[] { new Discount { Percentage = discount } }, creditCard.CardType);
            _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard);

            // Assert.

            AssertPrices(order.Id, price, adjustedPrice);
        }

        private void AssertPrices(Guid orderId, decimal price, decimal adjustedPrice)
        {
            var order = _ordersQuery.GetOrder(orderId);
            Assert.AreEqual(price, order.Price);
            Assert.AreEqual(adjustedPrice, order.AdjustedPrice);
        }

        private Product CreateProduct(int index, decimal price, Credit credit, int? quantity, TimeSpan? duration)
        {
            DeleteProduct(index);

            var product = new Product
            {
                Name = string.Format(ProductNameFormat, index),
                Description = string.Format(ProductNameFormat, index),
                UserTypes = UserType.Employer,
                Price = price,
                Currency = Currency.AUD,
                CreditAdjustments = new List<ProductCreditAdjustment>{ new ProductCreditAdjustment {CreditId = credit.Id, Quantity = quantity, Duration = duration}}
            };
            _productsCommand.CreateProduct(product);
            return product;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Orders
{
    [TestClass]
    public class CouponOrderTests
        : OrdersTests
    {
        private readonly ICouponsCommand _couponsCommand = Resolve<ICouponsCommand>();

        private const string CouponCodeFormat = "ABC00{0}";
        private const decimal PercentDiscount = (decimal) 0.2;
        private const decimal AmountDiscount = 80;
        private const decimal BundleDiscount = (decimal)0.1;

        [TestMethod]
        public void TestNoCoupon()
        {
            var product1 = GetContactProduct();
            var product2 = GetApplicantProduct();

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var order = _ordersCommand.PrepareOrder(new[] { product1.Id, product2.Id }, null, null, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product1.CreditAdjustments.Concat(product2.CreditAdjustments).ToArray());
            AssertOrders(ownerId, null, null, product1, product2);
            AssertReceipt(order.Id, receipt);
        }

        [TestMethod]
        public void TestPercentageCoupon()
        {
            var product1 = GetContactProduct();

            var coupon1 = CreateCoupon(1, false, CreatePercentageDiscount(), null, null);
            var coupon2 = CreateCoupon(2, false, CreatePercentageDiscount(), null, null);
            _ordersCommand.ValidateCoupon(coupon1, null, null);
            _ordersCommand.ValidateCoupon(coupon2, null, null);

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var order = _ordersCommand.PrepareOrder(new[] { product1.Id }, coupon1, null, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product1.CreditAdjustments.ToArray());
            AssertOrders(ownerId, coupon1, null, product1);
            AssertReceipt(order.Id, receipt);
            _ordersCommand.ValidateCoupon(coupon1, null, null);
            _ordersCommand.ValidateCoupon(coupon2, null, null);
        }

        [TestMethod]
        public void TestFixedCoupon()
        {
            var product1 = GetContactProduct();

            var coupon1 = CreateCoupon(1, false, CreateFixedDiscount(), null, null);
            var coupon2 = CreateCoupon(2, false, CreateFixedDiscount(), null, null);
            _ordersCommand.ValidateCoupon(coupon1, null, null);
            _ordersCommand.ValidateCoupon(coupon2, null, null);

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var order = _ordersCommand.PrepareOrder(new[] { product1.Id }, coupon1, null, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product1.CreditAdjustments.ToArray());
            AssertOrders(ownerId, coupon1, null, product1);
            AssertReceipt(order.Id, receipt);
            _ordersCommand.ValidateCoupon(coupon1, null, null);
            _ordersCommand.ValidateCoupon(coupon2, null, null);
        }

        [TestMethod]
        public void TestCouponWithBundle()
        {
            var product1 = GetContactProduct();
            var product2 = GetApplicantProduct();

            var coupon1 = CreateCoupon(1, false, CreatePercentageDiscount(), null, null);
            var coupon2 = CreateCoupon(2, false, CreatePercentageDiscount(), null, null);
            _ordersCommand.ValidateCoupon(coupon1, null, null);
            _ordersCommand.ValidateCoupon(coupon2, null, null);

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var discount = new BundleDiscount { Percentage = BundleDiscount };
            var order = _ordersCommand.PrepareOrder(new[] { product1.Id, product2.Id }, coupon1, new[] { discount }, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product1.CreditAdjustments.Concat(product2.CreditAdjustments).ToArray());
            AssertOrders(ownerId, coupon1, discount, product1, product2);
            AssertReceipt(order.Id, receipt);
            _ordersCommand.ValidateCoupon(coupon1, null, null);
            _ordersCommand.ValidateCoupon(coupon2, null, null);
        }

        [TestMethod]
        public void TestCanBeUsedOnceCoupon()
        {
            var product1 = GetContactProduct();
            var product2 = GetApplicantProduct();

            var coupon1 = CreateCoupon(1, true, CreatePercentageDiscount(), null, null);
            var coupon2 = CreateCoupon(2, true, CreatePercentageDiscount(), null, null);
            _ordersCommand.ValidateCoupon(coupon1, null, null);
            _ordersCommand.ValidateCoupon(coupon2, null, null);

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var order = _ordersCommand.PrepareOrder(new[] { product1.Id, product2.Id }, coupon1, null, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product1.CreditAdjustments.Concat(product2.CreditAdjustments).ToArray());
            AssertOrders(ownerId, coupon1, null, product1, product2);
            AssertReceipt(order.Id, receipt);
            AssertException.Thrown<CouponExpiredException>(() => _ordersCommand.ValidateCoupon(coupon1, null, null));
            _ordersCommand.ValidateCoupon(coupon2, null, null);
        }

        [TestMethod]
        public void TestCouponProductId()
        {
            var product1 = GetContactProduct();

            var coupon1 = CreateCoupon(1, false, CreatePercentageDiscount(), null, new[] { product1.Id });
            AssertException.Thrown<CouponProductException>(() => _ordersCommand.ValidateCoupon(coupon1, null, null));
            _ordersCommand.ValidateCoupon(coupon1, null, new[] { product1.Id });

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var order = _ordersCommand.PrepareOrder(new[] { product1.Id }, coupon1, null, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product1.CreditAdjustments.ToArray());
            AssertOrders(ownerId, coupon1, null, product1);
            AssertReceipt(order.Id, receipt);
            AssertException.Thrown<CouponProductException>(() => _ordersCommand.ValidateCoupon(coupon1, null, null));
            _ordersCommand.ValidateCoupon(coupon1, null, new[] { product1.Id });
        }

        [TestMethod]
        public void TestCouponProductIdWithBundle()
        {
            var product1 = GetContactProduct();
            var product2 = GetApplicantProduct();

            var coupon1 = CreateCoupon(1, false, CreatePercentageDiscount(), null, new[] { product1.Id });
            AssertException.Thrown<CouponProductException>(() => _ordersCommand.ValidateCoupon(coupon1, null, null));
            _ordersCommand.ValidateCoupon(coupon1, null, new[] { product1.Id });

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var discount = new BundleDiscount { Percentage = BundleDiscount };
            var order = _ordersCommand.PrepareOrder(new[] { product1.Id, product2.Id }, coupon1, new[] { discount }, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product1.CreditAdjustments.Concat(product2.CreditAdjustments).ToArray());
            AssertOrders(ownerId, coupon1, discount, product1, product2);
            AssertReceipt(order.Id, receipt);
            AssertException.Thrown<CouponProductException>(() => _ordersCommand.ValidateCoupon(coupon1, null, null));
            _ordersCommand.ValidateCoupon(coupon1, null, new[] { product1.Id });
        }

        [TestMethod]
        public void TestCouponRedeemerId()
        {
            var product1 = GetContactProduct();
            var product2 = GetApplicantProduct();

            var redeemerId1 = Guid.NewGuid();
            var redeemerId2 = Guid.NewGuid();
            var redeemerId3 = Guid.NewGuid();

            var coupon1 = CreateCoupon(1, false, CreatePercentageDiscount(), new[] { redeemerId1, redeemerId2 }, null);
            AssertException.Thrown<CouponRedeemerException>(() => _ordersCommand.ValidateCoupon(coupon1, null, null));
            _ordersCommand.ValidateCoupon(coupon1, redeemerId1, null);
            _ordersCommand.ValidateCoupon(coupon1, redeemerId2, null);
            AssertException.Thrown<CouponRedeemerException>(() => _ordersCommand.ValidateCoupon(coupon1, redeemerId3, null));

            // Purchase.

            var ownerId = Guid.NewGuid();
            var creditCard = CreateCreditCard(CreditCardType.Visa);
            var order = _ordersCommand.PrepareOrder(new[] { product1.Id, product2.Id }, coupon1, null, creditCard.CardType);
            var receipt = _ordersCommand.PurchaseOrder(ownerId, order, new Purchaser { Id = ownerId }, creditCard) as CreditCardReceipt;

            // Assert.

            AssertAllocations(ownerId, order.Id, product1.CreditAdjustments.Concat(product2.CreditAdjustments).ToArray());
            AssertOrders(ownerId, coupon1, null, product1, product2);
            AssertReceipt(order.Id, receipt);
            AssertException.Thrown<CouponRedeemerException>(() => _ordersCommand.ValidateCoupon(coupon1, null, null));
            _ordersCommand.ValidateCoupon(coupon1, redeemerId1, null);
            _ordersCommand.ValidateCoupon(coupon1, redeemerId2, null);
            AssertException.Thrown<CouponRedeemerException>(() => _ordersCommand.ValidateCoupon(coupon1, redeemerId3, null));
        }

        private static PercentageCouponDiscount CreatePercentageDiscount()
        {
            return new PercentageCouponDiscount { Percentage = PercentDiscount };
        }

        private static FixedCouponDiscount CreateFixedDiscount()
        {
            return new FixedCouponDiscount { Amount = AmountDiscount };
        }

        private Coupon CreateCoupon(int index, bool canBeUsedOnce, CouponDiscount discount, IList<Guid> redeemerIds, IList<Guid> productIds)
        {
            var coupon = new Coupon
            {
                Code = string.Format(CouponCodeFormat, index),
                Discount = discount,
                IsEnabled = true,
                CanBeUsedOnce = canBeUsedOnce,
                RedeemerIds = redeemerIds,
                ProductIds = productIds,
            };
            _couponsCommand.CreateCoupon(coupon);
            return coupon;
        }
    }
}

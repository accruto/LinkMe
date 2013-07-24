using System;
using System.Collections.Generic;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Orders
{
    [TestClass]
    public class CouponsTests
        : TestClass
    {
        private readonly ICouponsCommand _couponsCommand = Resolve<ICouponsCommand>();
        private readonly ICouponsQuery _couponsQuery = Resolve<ICouponsQuery>();
        private readonly IProductsQuery _productsQuery = Resolve<IProductsQuery>();

        private const string CouponCodeFormat = "ABC00{0}";

        [TestInitialize]
        public void CouponsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreateCoupon()
        {
            var coupon = CreateCoupon(0);
            _couponsCommand.CreateCoupon(coupon);
            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Id));
            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Code));
        }

        [TestMethod]
        public void TestCreateCouponPercentageDiscount()
        {
            var coupon = CreateCoupon(0);
            coupon.CanBeUsedOnce = true;
            coupon.ExpiryDate = DateTime.Now.Date.AddYears(1);
            coupon.Discount = new PercentageCouponDiscount { Percentage = 0.15m };
            coupon.IsEnabled = false;

            _couponsCommand.CreateCoupon(coupon);

            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Id));
            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Code));
        }

        [TestMethod]
        public void TestCreateCouponAmountDiscount()
        {
            var coupon = CreateCoupon(0);
            coupon.CanBeUsedOnce = true;
            coupon.ExpiryDate = DateTime.Now.Date.AddYears(1);
            coupon.Discount = new FixedCouponDiscount { Amount = 80 };
            coupon.IsEnabled = false;

            _couponsCommand.CreateCoupon(coupon);

            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Id));
            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Code));
        }

        [TestMethod]
        public void TestCreateCouponProductIds()
        {
            var coupon = CreateCoupon(0);
            var products = _productsQuery.GetProducts();
            coupon.ProductIds = new List<Guid> { products[0].Id, products[2].Id };
            _couponsCommand.CreateCoupon(coupon);

            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Id));
            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Code));
        }

        [TestMethod]
        public void TestEnableCoupon()
        {
            var coupon = CreateCoupon(0);
            _couponsCommand.CreateCoupon(coupon);

            Assert.IsTrue(coupon.IsEnabled);
            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Id));
            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Code));

            _couponsCommand.DisableCoupon(coupon.Id);
            coupon.IsEnabled = false;

            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Id));
            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Code));

            _couponsCommand.EnableCoupon(coupon.Id);
            coupon.IsEnabled = true;

            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Id));
            AssertCoupon(coupon, _couponsQuery.GetCoupon(coupon.Code));
        }

        private static void AssertCoupon(Coupon expectedCoupon, Coupon coupon)
        {
            Assert.AreEqual(expectedCoupon.Id, coupon.Id);
            Assert.AreEqual(expectedCoupon.Code, coupon.Code);
            Assert.AreEqual(expectedCoupon.Discount, coupon.Discount);
            Assert.AreEqual(expectedCoupon.CanBeUsedOnce, coupon.CanBeUsedOnce);
            Assert.AreEqual(expectedCoupon.ExpiryDate, coupon.ExpiryDate);
            Assert.AreEqual(expectedCoupon.IsEnabled, coupon.IsEnabled);
            Assert.IsTrue(expectedCoupon.RedeemerIds.NullableCollectionEqual(coupon.RedeemerIds));
            Assert.IsTrue(expectedCoupon.ProductIds.NullableCollectionEqual(coupon.ProductIds));
        }

        private static Coupon CreateCoupon(int index)
        {
            return new Coupon
            {
                Code = string.Format(CouponCodeFormat, index),
                IsEnabled = true,
            };
        }
    }
}

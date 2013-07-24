using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.NewOrders
{
    [TestClass]
    public class NewOrderCouponTests
        : NewOrderTests
    {
        private readonly ICouponsCommand _couponsCommand = Resolve<ICouponsCommand>();

        private const string CouponCodeFormat = "ABC00{0}";
        private const decimal PercentageDiscount = (decimal)0.1;
        private const decimal AmountDiscount = 50;
        private const string ContactProductName = "Contacts40";
        private const string OtherContactProductName = "Contacts60";

        [TestMethod]
        public void TestPercentageCoupon()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            var coupon = CreateCoupon(0, true, true, null, null, null);
            TestCoupon(employer.Id, coupon, ContactProductName);
        }

        [TestMethod]
        public void TestFixedCoupon()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            var coupon = CreateCoupon(0, true, false, null, null, null);
            TestCoupon(employer.Id, coupon, ContactProductName);
        }

        [TestMethod]
        public void TestUnknownCoupon()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            const CreditCardType creditCardType = CreditCardType.MasterCard;
            var products = GetProducts(ContactProductName);
            var adjustments = GetAdjustments(products, null, creditCardType);

            // Choose.

            var instanceId = Choose(ContactProductName);
            AssertPaymentPage(instanceId, products, adjustments, false);

            // Pay.

            Pay(instanceId, Guid.NewGuid(), creditCardType, false, null);
            AssertUrl(GetPaymentUrl(instanceId));
            AssertErrorMessage("The coupon code cannot be found. Please confirm you've typed it in correctly.");
        }

        [TestMethod]
        public void TestDisabledCoupon()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            TestError(CreateCoupon(0, false, true, null, null, null), ContactProductName, "The coupon has already expired and is no longer valid.");
        }

        [TestMethod]
        public void TestExpiredCoupon()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            TestError(CreateCoupon(0, true, true, DateTime.Now.AddMonths(-1), null, null), ContactProductName, "The coupon has already expired and is no longer valid.");
        }

        [TestMethod]
        public void TestProduct()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            var products = GetProducts(ContactProductName);
            var coupon = CreateCoupon(0, true, true, null, null, (from p in products select p.Id).ToArray());
            TestCoupon(employer.Id, coupon, ContactProductName);
        }

        [TestMethod]
        public void TestWrongProduct()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            var products = GetProducts(OtherContactProductName);
            var coupon = CreateCoupon(0, true, true, null, null, (from p in products select p.Id).ToArray());
            TestError(coupon, ContactProductName, "The coupon does not apply to the type of products you've ordered.");
        }

        [TestMethod]
        public void TestRedeemer()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            var coupon = CreateCoupon(0, true, true, null, new[] { employer.Id }, null);
            TestCoupon(employer.Id, coupon, ContactProductName);
        }

        [TestMethod]
        public void TestWrongRedeemer()
        {
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);
            LogIn(employer2);
            var coupon = CreateCoupon(0, true, true, null, new[] { employer1.Id }, null);
            TestError(coupon, ContactProductName, "The coupon code is not valid for your account. Please login as a different user or user another coupon code.");
        }

        private void TestCoupon(Guid employerId, Coupon coupon, string contactProductName)
        {
            const CreditCardType creditCardType = CreditCardType.MasterCard;
            var products = GetProducts(contactProductName);
            var adjustments = GetAdjustments(products, null, creditCardType);

            // Choose.

            var instanceId = Choose(contactProductName);
            AssertPaymentPage(instanceId, products, adjustments, false);

            // Pay.

            Pay(instanceId, coupon.Id, creditCardType, false, null);

            // Now the coupon has been applied.

            adjustments = GetAdjustments(products, coupon, creditCardType);
            AssertReceiptPage(instanceId, products, adjustments);
            AssertOrdersPage(adjustments);
            AssertOrderPage(employerId, products, adjustments);
            AssertEmail(products, adjustments);

            AssertOrder(employerId, adjustments);
        }

        private void TestError(Coupon coupon, string contactProductName, string message)
        {
            const CreditCardType creditCardType = CreditCardType.MasterCard;
            var products = GetProducts(contactProductName);
            var adjustments = GetAdjustments(products, null, creditCardType);

            // Choose.

            var instanceId = Choose(contactProductName);
            AssertPaymentPage(instanceId, products, adjustments, false);

            // Pay.

            Pay(instanceId, coupon.Id, creditCardType, false, null);
            AssertUrl(GetPaymentUrl(instanceId));
            AssertErrorMessage(message);
        }

        private Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }

        private Coupon CreateCoupon(int index, bool isEnabled, bool percentageDiscount, DateTime? expiryDate, IList<Guid> redeemerIds, IList<Guid> productIds)
        {
            var coupon = new Coupon
                             {
                                 Code = string.Format(CouponCodeFormat, index),
                                 Discount = percentageDiscount ? (CouponDiscount)new PercentageCouponDiscount { Percentage = PercentageDiscount } : new FixedCouponDiscount { Amount = AmountDiscount },
                                 IsEnabled = isEnabled,
                                 ExpiryDate = expiryDate,
                                 CanBeUsedOnce = false,
                                 RedeemerIds = redeemerIds,
                                 ProductIds = productIds,
                             };
            _couponsCommand.CreateCoupon(coupon);
            return coupon;
        }

        private IList<Product> GetProducts(string contactProductName)
        {
            var products = new List<Product>();
            if (!string.IsNullOrEmpty(contactProductName))
                products.Add(_productsQuery.GetProduct(contactProductName));
            return products;
        }

        private static IList<OrderAdjustment> GetAdjustments(ICollection<Product> products, Coupon coupon, CreditCardType creditCardType)
        {
            var adjustments = new List<OrderAdjustment>();

            // Set up expected adjustments.

            var price = products.Sum(product => product.Price);
            var initialPrice = price;
            var adjustedPrice = price;

            if (coupon != null)
            {
                if (coupon.ProductIds != null)
                {
                    foreach (var product in products)
                    {
                        if (coupon.ProductIds.Contains(product.Id))
                        {
                            adjustedPrice = initialPrice - ((PercentageCouponDiscount)coupon.Discount).Percentage * product.Price;
                            adjustments.Add(new CouponAdjustment { InitialPrice = initialPrice, AdjustedPrice = adjustedPrice, Amount = new PercentageAdjustmentAmount { PercentageChange = ((PercentageCouponDiscount)coupon.Discount).Percentage }, CouponCode = coupon.Code, ProductId = product.Id });
                            break;
                        }
                    }
                }
                else
                {
                    adjustedPrice = initialPrice - coupon.Discount.GetDiscount(initialPrice);
                    adjustments.Add(
                        new CouponAdjustment
                            {
                                InitialPrice = initialPrice,
                                AdjustedPrice = adjustedPrice,
                                Amount = coupon.Discount is PercentageCouponDiscount
                                    ? (AdjustmentAmount) new PercentageAdjustmentAmount { PercentageChange = ((PercentageCouponDiscount)coupon.Discount).Percentage }
                                    : new FixedAdjustmentAmount { FixedChange = ((FixedCouponDiscount)coupon.Discount).Amount},
                                CouponCode = coupon.Code
                            });
                }
            }

            if (products.Count > 1)
            {
                initialPrice = adjustedPrice;
                adjustedPrice = initialPrice + (-1 * BundleDiscount * price);
                adjustments.Add(new BundleAdjustment { InitialPrice = initialPrice, AdjustedPrice = adjustedPrice, Percentage = BundleDiscount });
            }

            initialPrice = adjustedPrice;
            adjustedPrice = (1 + TaxRate) * adjustedPrice;
            adjustments.Add(new TaxAdjustment { InitialPrice = initialPrice, AdjustedPrice = adjustedPrice, TaxRate = TaxRate });

            if (creditCardType == CreditCardType.Amex)
            {
                initialPrice = adjustedPrice;
                adjustedPrice = (1 + AmexSurcharge) * initialPrice;
                adjustments.Add(new SurchargeAdjustment { InitialPrice = initialPrice, AdjustedPrice = adjustedPrice, Surcharge = AmexSurcharge, CreditCardType = creditCardType });
            }

            return adjustments;
        }

        private Guid Choose(string contactProductName)
        {
            // Choose.

            Get(GetChooseUrl());
            SelectContactProduct(string.IsNullOrEmpty(contactProductName) ? null : _productsQuery.GetProduct(contactProductName));
            _purchaseButton.Click();
            return GetInstanceId();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.NewOrders
{
    [TestClass]
    public class NewOrderCouponLinkTests
        : NewOrderTests
    {
        private readonly ICouponsCommand _couponsCommand = Resolve<ICouponsCommand>();

        private const string CouponCodeFormat = "ABC00{0}";
        private const decimal PercentageDiscount = 0.444444444m;
        private const decimal AmountDiscount = 50;
        private const string ContactProductName = "Contacts5";
        private const string OtherContactProductName = "Contacts60";

        [TestMethod]
        public void TestPercentageCoupon()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            var coupon = CreateCoupon(true, true, null, null);
            TestCoupon(employer.Id, coupon, ContactProductName);
        }

        [TestMethod]
        public void TestFixedCoupon()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            var coupon = CreateCoupon(true, false, null, null);
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

            const string couponCode = "UNKNOWN";
            var instanceId = Choose(ContactProductName, couponCode);
            AssertPaymentPage(instanceId, products, adjustments, false);

            // Pay.

            Pay(instanceId, Guid.NewGuid(), couponCode, creditCardType, false, null);
            AssertUrl(GetPaymentUrl(instanceId));
            AssertErrorMessage("The coupon code cannot be found. Please confirm you've typed it in correctly.");
        }

        [TestMethod]
        public void TestDisabledCoupon()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            TestError(CreateCoupon(false, true, null, null), ContactProductName, "The coupon has already expired and is no longer valid.");
        }

        [TestMethod]
        public void TestExpiredCoupon()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            TestError(CreateCoupon(true, true, DateTime.Now.AddMonths(-1), null), ContactProductName, "The coupon has already expired and is no longer valid.");
        }

        [TestMethod]
        public void TestProduct()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            var coupon = CreateCoupon(true, true, null, ContactProductName);
            TestCoupon(employer.Id, coupon, ContactProductName);
        }

        [TestMethod]
        public void TestWrongProduct()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            var coupon = CreateCoupon(true, true, null, ContactProductName);
            TestError(coupon, OtherContactProductName, "The coupon does not apply to the type of products you've ordered.");
        }

        private void TestCoupon(Guid employerId, Coupon coupon, string contactProductName)
        {
            const CreditCardType creditCardType = CreditCardType.MasterCard;
            var products = GetProducts(contactProductName);
            var adjustments = GetAdjustments(products, null, creditCardType);

            // Choose.

            var instanceId = Choose(contactProductName, coupon.Code);
            AssertPaymentPage(instanceId, products, adjustments, false);

            // Pay.

            Pay(instanceId, coupon.Id, coupon.Code, creditCardType, false, null);

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

            var instanceId = Choose(contactProductName, coupon.Code);
            AssertPaymentPage(instanceId, products, adjustments, false);

            // Pay.

            Pay(instanceId, coupon.Id, coupon.Code, creditCardType, false, null);
            AssertUrl(GetPaymentUrl(instanceId));
            AssertErrorMessage(message);
        }

        private Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }

        private Coupon CreateCoupon(bool isEnabled, bool percentageDiscount, DateTime? expiryDate, string productName)
        {
            var coupon = new Coupon
                             {
                                 Code = string.Format(CouponCodeFormat, 0),
                                 Discount = percentageDiscount ? (CouponDiscount)new PercentageCouponDiscount { Percentage = PercentageDiscount } : new FixedCouponDiscount { Amount = AmountDiscount },
                                 IsEnabled = isEnabled,
                                 ExpiryDate = expiryDate,
                                 CanBeUsedOnce = false,
                                 RedeemerIds = null,
                                 ProductIds = productName == null ? null : new[] { _productsQuery.GetProduct(productName).Id },
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
                            adjustedPrice = initialPrice - coupon.Discount.GetDiscount(product.Price);
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
                                    : new FixedAdjustmentAmount { FixedChange = ((FixedCouponDiscount)coupon.Discount).Amount },
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

        private Guid Choose(string contactProductName, string couponCode)
        {
            // Choose.

            Get(GetNewOrderUrl(couponCode));
            SelectContactProduct(string.IsNullOrEmpty(contactProductName) ? null : _productsQuery.GetProduct(contactProductName));
            _purchaseButton.Click();
            return GetInstanceId();
        }

        protected void Pay(Guid instanceId, Guid couponId, string couponCode, CreditCardType creditCardType, bool useDiscountVisible, bool? useDiscount)
        {
            // Payment.

            AssertUrl(GetPaymentUrl(instanceId));
            Assert.AreEqual(couponCode, _couponCodeTextBox.Text);

            // Simulate applying the coupon.

            _couponIdTextBox.Text = couponId.ToString();

            AssertUseDiscountVisibility(useDiscountVisible);
            if (useDiscount != null)
                _useDiscountCheckBox.IsChecked = useDiscount.Value;

            _cardNumberTextBox.Text = CreditCardNumber;
            var index = 0;
            for (; index < _cardTypeDropDownList.Items.Count; ++index)
            {
                if (_cardTypeDropDownList.Items[index].Value == creditCardType.ToString())
                    break;
            }
            _cardTypeDropDownList.SelectedIndex = index;

            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();
        }

        private ReadOnlyUrl GetNewOrderUrl(string couponCode)
        {
            var newOrderUrl = _newOrderUrl.AsNonReadOnly();
            newOrderUrl.QueryString["couponCode"] = couponCode;
            return newOrderUrl;
        }
    }
}
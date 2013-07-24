using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Affiliations;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.NewOrders
{
    [TestClass]
    public abstract class NewOrderTests
        : ProductsTests
    {
        protected const decimal BundleDiscount = 0.1m;
        protected const decimal TaxRate = 0.1m;
        protected const decimal AmexSurcharge = 0.025m;
        protected const decimal VecciDiscount = 0.2m;
        private readonly IOrdersQuery _ordersQuery = Resolve<IOrdersQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        protected HtmlCheckBoxTester _useDiscountCheckBox;
        protected HtmlTextBoxTester _couponCodeTextBox;
        protected HtmlHiddenTester _couponIdTextBox;

        [TestInitialize]
        public void NewOrderTestsInitialize()
        {
            _emailServer.ClearEmails();
            _useDiscountCheckBox = new HtmlCheckBoxTester(Browser, "UseDiscount");
            _couponCodeTextBox = new HtmlTextBoxTester(Browser, "CouponCode");
            _couponIdTextBox = new HtmlHiddenTester(Browser, "CouponId");
        }

        protected void Pay(Guid instanceId, Guid? couponId, CreditCardType creditCardType, bool useDiscountVisible, bool? useDiscount)
        {
            // Payment.

            AssertUrl(GetPaymentUrl(instanceId));

            if (couponId != null)
            {
                // Simulate applying a coupon code.

                Assert.AreEqual(string.Empty, _couponCodeTextBox.Text);
                _couponIdTextBox.Text = couponId.Value.ToString();
            }

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

        protected void AssertPaymentPage(Guid instanceId, IList<Product> products, IList<OrderAdjustment> adjustments, bool useDiscountVisible)
        {
            AssertUrl(GetPaymentUrl(instanceId));
            AssertUseDiscountVisibility(useDiscountVisible);

            // The surcharge adjustment does not appear on this page.

            if (adjustments[adjustments.Count - 1] is SurchargeAdjustment)
                adjustments = adjustments.Take(adjustments.Count - 1).ToList();

            AssertAdjustments(Browser.CurrentHtml.DocumentNode, products, adjustments);
        }

        protected void AssertReceiptPage(Guid instanceId, IList<Product> products, IList<OrderAdjustment> adjustments)
        {
            AssertUrl(GetReceiptUrl(instanceId));
            AssertAdjustments(Browser.CurrentHtml.DocumentNode, products, adjustments);
        }

        protected void AssertOrdersPage(IList<OrderAdjustment> adjustments)
        {
            Get(_ordersUrl);

            var trs = Browser.CurrentHtml.DocumentNode.SelectNodes("//table/tbody/tr");
            Assert.IsNotNull(trs);
            Assert.AreEqual(1, trs.Count);

            // The total for the order is the last adjustment value.

            Assert.AreEqual(GetPriceDisplayText(adjustments[adjustments.Count - 1].AdjustedPrice), trs[0].SelectSingleNode("td[position()=3]").InnerText);
        }

        protected void AssertOrderPage(Guid employerId, IList<Product> products, IList<OrderAdjustment> adjustments)
        {
            var order = _ordersQuery.GetOrders(employerId)[0];
            Get(GetOrderUrl(order.Id));

            AssertAdjustments(Browser.CurrentHtml.DocumentNode, products, adjustments);
        }

        protected void AssertAdjustments(HtmlNode document, IList<Product> products, IList<OrderAdjustment> adjustments)
        {
            // Check the table.

            var trs = document.SelectNodes("//table/tbody/tr");

            // 2 is for the excl Gst and incl Gst totals.

            Assert.IsNotNull(trs);
            Assert.AreEqual(products.Count + adjustments.Count + 2, trs.Count);

            // Check products.

            HtmlNode tr;
            for (var index = 0; index < products.Count; ++index)
            {
                var product = products[index];
                tr = trs[index];

                Assert.AreEqual(_creditsQuery.GetCredit(GetPrimaryAdjustment(product).CreditId).Description, tr.SelectSingleNode("td[position()=1]").InnerText);
                Assert.AreEqual(GetPriceDisplayText(product.Price), tr.SelectSingleNode("td[position()=3]").InnerText);
            }

            // Check total excluding GST.

            tr = trs[products.Count];
            Assert.AreEqual("Total (excl. GST)", tr.SelectSingleNode("td[position()=2]").InnerText);
            Assert.AreEqual(GetPriceDisplayText((from p in products select p.Price).Sum()), tr.SelectSingleNode("td[position()=3]").InnerText);

            // Check adjustments.

            for (var index = 0; index < adjustments.Count; ++index)
            {
                var adjustment = adjustments[index];
                tr = trs[products.Count + 1 + index];

                Assert.AreEqual(GetAdjustmentText(products, adjustment), tr.SelectSingleNode("td[position()=2]").InnerText);
                Assert.AreEqual(GetPriceDisplayText(adjustment.AdjustedPrice - adjustment.InitialPrice), tr.SelectSingleNode("td[position()=3]").InnerText);
            }

            // Check total including GST.

            tr = trs[products.Count + 1 + adjustments.Count];
            Assert.AreEqual("Total amount payable (incl. GST)", tr.SelectSingleNode("td[position()=2]").InnerText);
            Assert.AreEqual(GetPriceDisplayText(adjustments[adjustments.Count - 1].AdjustedPrice), tr.SelectSingleNode("td[position()=3]").InnerText);
        }

        protected static ProductCreditAdjustment GetPrimaryAdjustment(Product product)
        {
            return product.CreditAdjustments.Count == 1
                ? product.CreditAdjustments[0]
                : (from a in product.CreditAdjustments where a.Quantity != null select a).First();
        }

        protected void AssertOrder(Guid employerId, IList<OrderAdjustment> adjustments)
        {
            var orders = _ordersQuery.GetOrders(employerId);
            Assert.AreEqual(1, orders.Count);
            var order = orders[0];

            // Check prices on order.

            Assert.AreEqual(adjustments[0].InitialPrice, order.Price);
            Assert.AreEqual(Math.Round(adjustments[adjustments.Count - 1].AdjustedPrice, 2), order.AdjustedPrice);

            // Check each individual adjustment.

            Assert.AreEqual(adjustments.Count, order.Adjustments.Count);
            for (var index = 0; index < order.Adjustments.Count; ++index)
            {
                var adjustment = adjustments[index];
                var orderAdjustment = order.Adjustments[index];

                Assert.AreEqual(Math.Round(adjustment.InitialPrice, 2), orderAdjustment.InitialPrice);
                Assert.AreEqual(Math.Round(adjustment.AdjustedPrice, 2), orderAdjustment.AdjustedPrice);

                if (adjustment is TaxAdjustment)
                {
                    Assert.AreEqual(((TaxAdjustment)adjustment).TaxRate, ((TaxAdjustment)orderAdjustment).TaxRate);
                }
                else if (adjustment is SurchargeAdjustment)
                {
                    Assert.AreEqual(((SurchargeAdjustment)adjustment).Surcharge, ((SurchargeAdjustment)orderAdjustment).Surcharge);
                    Assert.AreEqual(((SurchargeAdjustment)adjustment).CreditCardType, ((SurchargeAdjustment)orderAdjustment).CreditCardType);
                }
                else if (adjustment is BundleAdjustment)
                {
                    Assert.AreEqual(((BundleAdjustment)adjustment).Percentage, ((BundleAdjustment)orderAdjustment).Percentage);
                }
                else if (adjustment is CouponAdjustment)
                {
                    if (((CouponAdjustment)adjustment).Amount is PercentageAdjustmentAmount)
                        Assert.AreEqual(((PercentageAdjustmentAmount)((CouponAdjustment)adjustment).Amount).PercentageChange, ((PercentageAdjustmentAmount)((CouponAdjustment)orderAdjustment).Amount).PercentageChange);
                    else
                        Assert.AreEqual(((FixedAdjustmentAmount)((CouponAdjustment)adjustment).Amount).FixedChange, ((FixedAdjustmentAmount)((CouponAdjustment)orderAdjustment).Amount).FixedChange);
                    Assert.AreEqual(((CouponAdjustment)adjustment).CouponCode, ((CouponAdjustment)orderAdjustment).CouponCode);
                }
                else if (adjustment is VecciDiscountAdjustment)
                {
                    Assert.AreEqual(((VecciDiscountAdjustment)adjustment).Percentage, ((VecciDiscountAdjustment)orderAdjustment).Percentage);
                }
                else
                {
                    Assert.Fail("Unknown adjustment type.");
                }
            }
        }

        protected void AssertEmail(IList<Product> products, IList<OrderAdjustment> adjustments)
        {
            var email = _emailServer.AssertEmailSent();

            var document = new HtmlDocument();
            document.LoadHtml(email.AlternateViews[0].Body);

            AssertAdjustments(document.DocumentNode, products, adjustments);
        }

        private static string GetAdjustmentText(ICollection<Product> products, OrderAdjustment adjustment)
        {
            if (adjustment is TaxAdjustment)
                return "Add GST";
            if (adjustment is SurchargeAdjustment)
                return "Add American Express surcharge (" + AmexSurcharge.ToString("P1") + ")";
            if (adjustment is BundleAdjustment)
                return "Less " + BundleDiscount.ToString("P0") + " bundle discount";
            if (adjustment is VecciDiscountAdjustment)
                return "Less VECCI Member discount (" + VecciDiscount.ToString("P0") + ")";
            if (adjustment is CouponAdjustment)
            {
                var couponAdjustment = (CouponAdjustment) adjustment;
                var text = couponAdjustment.Amount is PercentageAdjustmentAmount
                    ? "Less " + ((PercentageAdjustmentAmount) ((CouponAdjustment) adjustment).Amount).PercentageChange.ToString( "P0") + " coupon discount"
                    : "Less $" + ((FixedAdjustmentAmount) ((CouponAdjustment) adjustment).Amount).FixedChange + " coupon discount";

                if (products.Count <= 1 || ((CouponAdjustment)adjustment).ProductId == null)
                    return text;
                return text + " (applied to " + (from p in products where p.Id == ((CouponAdjustment) adjustment).ProductId select p).Single ().Description + " only)";
            }
            return string.Empty;
        }

        protected static string GetPriceDisplayText(decimal price)
        {
            price = Math.Round(price, Domain.Currency.AUD.CultureInfo.NumberFormat.CurrencyDecimalDigits);

            // Show decimal places always.

            return price.ToString("C", Domain.Currency.AUD.CultureInfo);
            //            return Math.Round(price) == price
            //                ? price.ToString("C0", Domain.Currency.AUD.CultureInfo)
            //                : price.ToString("C", Domain.Currency.AUD.CultureInfo);
        }

        protected void AssertUseDiscountVisibility(bool useDiscountVisible)
        {
            var div = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='shadowed-section shadowed_section section']");
            Assert.IsNotNull(div);
            var style = div.Attributes["style"];
            if (useDiscountVisible)
            {
                Assert.IsNull(style);
            }
            else
            {
                Assert.IsNotNull(style);
                Assert.AreEqual("display: none;", style.Value);
            }
        }

    }
}
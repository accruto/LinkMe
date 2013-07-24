using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Affiliations;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.NewOrders
{
    [TestClass]
    public class NewOrderPriceTests
        : NewOrderTests
    {
        private const string OrganisatonName = "Organisation";
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        [TestMethod]
        public void TestContactCredits()
        {
            TestNewOrder("Contacts40", CreditCardType.MasterCard);
        }

        [TestMethod]
        public void TestVisa()
        {
            TestNewOrder("Contacts40", CreditCardType.Visa);
        }

        [TestMethod]
        public void TestAmex()
        {
            TestNewOrder("Contacts40", CreditCardType.Amex);
        }

        [TestMethod]
        public void TestCommunityNoDiscount()
        {
            TestCommunityNewOrder("Contacts40", CreditCardType.MasterCard, false);
        }

        [TestMethod]
        public void TestCommunityDiscount()
        {
            TestCommunityNewOrder("Contacts40", CreditCardType.MasterCard, true);
        }

        [TestMethod]
        public void TestCommunityNoDiscountAmex()
        {
            TestCommunityNewOrder("Contacts40", CreditCardType.Amex, false);
        }

        [TestMethod]
        public void TestCommunityDiscountAmex()
        {
            TestCommunityNewOrder("Contacts40", CreditCardType.Amex, true);
        }

        [TestMethod]
        public void TestCommunityLogInNewOrder()
        {
            var data = TestCommunity.Vecci.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            const CreditCardType creditCardType = CreditCardType.Amex;
            var product = _productsQuery.GetProduct("Contacts10");
            var products = new[] {product};
            var price = product.Price;
            var adjustments = GetAdjustments(products, price, creditCardType);
            var discountedAdjustments = GetDiscountedAdjustments(price, creditCardType);

            // Login as employer associated with the community.

            var organisation = new Organisation { Name = OrganisatonName, AffiliateId = community.Id };
            _organisationsCommand.CreateOrganisation(organisation);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);

            LogIn(employer);
            var instanceId = Choose("Contacts10");
            AssertPaymentPage(instanceId, products, adjustments, true);
            Pay(instanceId, null, creditCardType, true, true);
            AssertReceiptPage(instanceId, products, discountedAdjustments);
            AssertOrdersPage(discountedAdjustments);
            AssertEmail(products, discountedAdjustments);
            AssertOrder(employer.Id, discountedAdjustments);
            LogOut();

            // Login as employer not associated with the community.

            product = _productsQuery.GetProduct("Contacts40");
            products = new[] { product };
            price = product.Price;
            adjustments = GetAdjustments(products, price, creditCardType);

            organisation = new Organisation { Name = OrganisatonName, AffiliateId = null };
            _organisationsCommand.CreateOrganisation(organisation);
            employer = _employerAccountsCommand.CreateTestEmployer(1, organisation);

            ClearCookies(new ReadOnlyApplicationUrl("~/"));
            LogIn(employer);
            instanceId = Choose("Contacts40");
            AssertPaymentPage(instanceId, products, adjustments, false);
            Pay(instanceId, null, creditCardType, false, null);
            AssertReceiptPage(instanceId, products, adjustments);
            AssertOrdersPage(adjustments);
            AssertEmail(products, adjustments);
            AssertOrder(employer.Id, adjustments);
        }

        private void TestNewOrder(string contactProductName, CreditCardType creditCardType)
        {
            var products = GetProducts(contactProductName);
            var price = (from p in products select p.Price).Sum();
            var adjustments = GetAdjustments(products, price, creditCardType);

            // Choose.

            var employer = LogIn(false);
            var instanceId = Choose(contactProductName);
            AssertPaymentPage(instanceId, products, adjustments, false);

            // Pay.

            Pay(instanceId, null, creditCardType, false, null);
            AssertReceiptPage(instanceId, products, adjustments);
            AssertOrdersPage(adjustments);
            AssertOrderPage(employer.Id, products, adjustments);
            AssertEmail(products, adjustments);

            AssertOrder(employer.Id, adjustments);
        }

        private IList<Product> GetProducts(string contactProductName)
        {
            var products = new List<Product>();
            if (!string.IsNullOrEmpty(contactProductName))
                products.Add(_productsQuery.GetProduct(contactProductName));
            return products;
        }

        private void TestCommunityNewOrder(string contactProductName, CreditCardType creditCardType, bool useDiscount)
        {
            var products = GetProducts(contactProductName);
            var data = TestCommunity.Vecci.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            var price = (from p in products select p.Price).Sum();
            var adjustments = GetAdjustments(products, price, creditCardType);
            var discountedAdjustments = GetDiscountedAdjustments(price, creditCardType);

            var employer = LogIn(false);

            // Hit the landing page.

            var url = _verticalsCommand.GetCommunityPathUrl(community, "employers/Employer.aspx");
            Get(url);

            // Choose.

            var instanceId = Choose(contactProductName);
            AssertPaymentPage(instanceId, products, adjustments, true);

            // Pay.

            Pay(instanceId, null, creditCardType, true, useDiscount);
            AssertReceiptPage(instanceId, products, useDiscount ? discountedAdjustments : adjustments);
            AssertOrdersPage(useDiscount ? discountedAdjustments : adjustments);
            AssertEmail(products, useDiscount ? discountedAdjustments : adjustments);

            AssertOrder(employer.Id, useDiscount ? discountedAdjustments : adjustments);
        }

        private static IList<OrderAdjustment> GetAdjustments(ICollection<Product> products, decimal price, CreditCardType creditCardType)
        {
            var adjustments = new List<OrderAdjustment>();

            // Set up expected adjustments.

            var adjustedPrice = price;
            if (products.Count > 1)
            {
                adjustedPrice = (1 - BundleDiscount) * price;
                adjustments.Add(new BundleAdjustment { InitialPrice = price, AdjustedPrice = adjustedPrice, Percentage = BundleDiscount });
            }

            price = adjustedPrice;
            adjustedPrice = (1 + TaxRate) * adjustedPrice;
            adjustments.Add(new TaxAdjustment { InitialPrice = price, AdjustedPrice = adjustedPrice, TaxRate = TaxRate });

            if (creditCardType == CreditCardType.Amex)
            {
                price = adjustedPrice;
                adjustedPrice = (1 + AmexSurcharge) * price;
                adjustments.Add(new SurchargeAdjustment { InitialPrice = price, AdjustedPrice = adjustedPrice, Surcharge = AmexSurcharge, CreditCardType = creditCardType });
            }

            return adjustments;
        }

        private static IList<OrderAdjustment> GetDiscountedAdjustments(decimal price, CreditCardType creditCardType)
        {
            // Add the discount up front.

            var discountPrice = (1 - VecciDiscount) * price;
            IList<OrderAdjustment> adjustments = new List<OrderAdjustment>
                                                     {
                                                         new VecciDiscountAdjustment { InitialPrice = price, AdjustedPrice = discountPrice, Percentage = VecciDiscount }
                                                     };

            // Set up expected adjustments.

            var taxPrice = (1 + TaxRate) * discountPrice;
            adjustments.Add(new TaxAdjustment { InitialPrice = discountPrice, AdjustedPrice = taxPrice, TaxRate = TaxRate });

            if (creditCardType == CreditCardType.Amex)
            {
                var surchargePrice = (1 + AmexSurcharge) * taxPrice;
                adjustments.Add(new SurchargeAdjustment { InitialPrice = taxPrice, AdjustedPrice = surchargePrice, Surcharge = AmexSurcharge, CreditCardType = creditCardType });
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
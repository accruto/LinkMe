using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.ViewOrders
{
    public abstract class ViewOrdersTests
        : ProductsTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IOrdersCommand _ordersCommand = Resolve<IOrdersCommand>();
        private readonly IOrdersRepository _ordersRepository = Resolve<IOrdersRepository>();

        [TestMethod]
        public void TestEmployerContactOrder()
        {
            TestEmployerOrder("Contacts5", false);
        }

        [TestMethod]
        public void TestEmployerContactOrderDisabledProduct()
        {
            TestEmployerOrder("Contacts3", true);
        }

        [TestMethod]
        public void TestEmployerApplicantOrder()
        {
            TestEmployerOrder("Applicants20", false);
        }

        [TestMethod]
        public void TestEmployerApplicantOrderDisabledProduct()
        {
            TestEmployerOrder("ApplicantPack10", true);
        }

        [TestMethod]
        public void TestEmployerJobAdOrder()
        {
            TestEmployerOrder("JobAds20", false);
        }

        [TestMethod]
        public void TestEmployerJobAdOrderDisabledProduct()
        {
            TestEmployerOrder("JobAdPack20", true);
        }

        [TestMethod]
        public void TestEmployerBundledOrderDisabledProduct()
        {
            TestEmployerOrder("Applicants10Contacts3", true);
        }

        [TestMethod]
        public void TestContactAndApplicantEmployerOrders()
        {
            Test2EmployerOrders("Contacts10", "Applicants20");
        }

        [TestMethod]
        public void TestContactAndJobAdEmployerOrders()
        {
            Test2EmployerOrders("Contacts10", "JobAds20");
        }

        [TestMethod]
        public void TestOrderNotFound()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            // Look for non-existant order.

            var id = Guid.NewGuid();
            var url = GetOrderUrl(id);
            Get(HttpStatusCode.NotFound, url);
            AssertUrl(url);
            AssertPageContains("The order with id '" + id + "' cannot be found.");
        }

        [TestMethod]
        public void TestOrganisationContactOrder()
        {
            TestOrganisationOrder("Contacts5", false);
        }

        [TestMethod]
        public void TestOrganisationApplicantOrder()
        {
            TestOrganisationOrder("Applicants20", false);
        }

        [TestMethod]
        public void TestOrganisationJobAdOrder()
        {
            TestOrganisationOrder("JobAds20", false);
        }

        [TestMethod]
        public void TestOrganisationBundledOrder()
        {
            TestOrganisationOrder("Applicants10Contacts3", true);
        }

        [TestMethod]
        public void TestContactAndApplicantOrganisationOrders()
        {
            Test2OrganisationOrders("Contacts10", "Applicants20");
        }

        [TestMethod]
        public void TestContactAndJobAdOrganisationOrders()
        {
            Test2OrganisationOrders("Contacts10", "JobAds20");
        }

        [TestMethod]
        public void TestEmployerContactOrganisationApplicantOrders()
        {
            TestEmployerOrganisationOrders("Contacts5", "Applicants20");
        }

        [TestMethod]
        public void TestEmployerContactOrganisationJobAdOrders()
        {
            TestEmployerOrganisationOrders("Contacts5", "JobAds20");
        }

        [TestMethod]
        public void TestNoReceiptOrder()
        {
            var employer = CreateEmployer();
            var product = _productsCommand.GetProduct("Contacts5");

            // Create an order but don't purchase it, ie simulate the payment gateway refusing payment.

            var creditCard = CreateCreditCard();
            var order = _ordersCommand.PrepareOrder(new[] { product.Id }, null, null, creditCard.CardType);
            order.OwnerId = employer.Id;
            order.PurchaserId = employer.Id;
            _ordersRepository.CreateOrder(order);

            // Check credits.

            LogIn(employer);

            // Check orders.

            Get(_ordersUrl);

            // Since the order has not been placed it should not show up in the orders page.

            AssertNoOrders();

            // Check order.

            var orderUrl = GetOrderUrl(order.Id);
            Get(HttpStatusCode.NotFound, orderUrl);
        }

        private void TestEmployerOrder(string productName, bool disabled)
        {
            var employer = CreateEmployer();

            // Use the command instead of the query just in case the product is disabled.
            
            var product = _productsCommand.GetProduct(productName);

            Order order;
            try
            {
                // Temprarily enable the product so that the order can be purchased.

                if (disabled)
                    _productsCommand.EnableProduct(product.Id);

                order = PurchaseOrder(employer.Id, employer, product);
            }
            finally
            {
                if (disabled)
                    _productsCommand.DisableProduct(product.Id);
            }

            // Check credits.

            LogIn(employer);
            var orderUrl = GetOrderUrl(order.Id);
            Get(_creditsUrl);
            AssertEmployerCredits(order, orderUrl, product.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product).Duration.Value).Date);

            // Check orders.

            Get(_ordersUrl);
            AssertOrders(order, orderUrl);

            // Check order.

            Get(orderUrl);
            AssertOrder(order, orderUrl);
            AssertOrder(orderUrl, product);

            // Check credit summary.

            Get(_searchUrl);
            AssertCreditSummary(product.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product).Duration.Value).Date);
        }

        private void TestOrganisationOrder(string productName, bool disabled)
        {
            var employer = CreateEmployer();
            var organisation = GetOrderOrganisation(employer);

            // Credits cannot be ordered for some types of organisations, i.e. unverified.

            var product = _productsCommand.GetProduct(productName);
            Order order = null;

            if (organisation != null)
            {
                if (disabled)
                    _productsCommand.EnableProduct(product.Id);

                try
                {
                    order = PurchaseOrder(organisation.Id, employer, product);
                }
                finally
                {
                    if (disabled)
                        _productsCommand.DisableProduct(product.Id);
                }
            }

            // Check credits.

            LogIn(employer);
            Get(_creditsUrl);

            if (organisation == null)
            {
                AssertNoOrganisationOrders();
            }
            else
            {
                var orderUrl = GetOrderUrl(order.Id);
                AssertOrganisationCredits(product.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product).Duration.Value).Date);

                // Check that the order at this time cannot be viewed.

                Get(HttpStatusCode.NotFound, orderUrl);
                AssertPageContains("cannot be found");

                Get(_searchUrl);
                AssertCreditSummary(product.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product).Duration.Value).Date);
            }
        }

        private void Test2EmployerOrders(string product1Name, string product2Name)
        {
            var employer = CreateEmployer();
            var product1 = _productsQuery.GetProduct(product1Name);
            var product2 = _productsQuery.GetProduct(product2Name);

            var order1 = PurchaseOrder(employer.Id, employer, product1);
            var order2 = PurchaseOrder(employer.Id, employer, product2);

            // Check credits.

            LogIn(employer);
            var order1Url = GetOrderUrl(order1.Id);
            var order2Url = GetOrderUrl(order2.Id);
            Get(_creditsUrl);
            AssertEmployerCredits(order1, order1Url, product1.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product1).Duration.Value).Date);
            AssertEmployerCredits(order2, order2Url, product2.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product2).Duration.Value).Date);

            // Check orders.

            Get(_ordersUrl);
            AssertOrders(order1, order1Url);
            AssertOrders(order2, order2Url);

            // Check order.

            Get(order1Url);
            AssertOrder(order1, order1Url);
            AssertOrder(order1Url, product1);

            Get(order2Url);
            AssertOrder(order2, order2Url);
            AssertOrder(order2Url, product2);

            Get(_searchUrl);
            var creditAdjustments = Combine(product1.CreditAdjustments, product2.CreditAdjustments);
            AssertCreditSummary(creditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product2).Duration.Value));
        }

        private void Test2OrganisationOrders(string product1Name, string product2Name)
        {
            var employer = CreateEmployer();
            var organisation = GetOrderOrganisation(employer);

            var product1 = _productsQuery.GetProduct(product1Name);
            var product2 = _productsQuery.GetProduct(product2Name);

            Order order1 = null;
            Order order2 = null;

            if (organisation != null)
            {
                order1 = PurchaseOrder(organisation.Id, employer, product1);
                order2 = PurchaseOrder(organisation.Id, employer, product2);
            }

            // Check credits.

            LogIn(employer);
            Get(_creditsUrl);

            if (organisation == null)
            {
                AssertNoOrganisationOrders();
            }
            else
            {
                AssertOrganisationCredits(product1.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product1).Duration.Value));
                AssertOrganisationCredits(product2.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product2).Duration.Value));

                // Check order.

                var order1Url = GetOrderUrl(order1.Id);
                var order2Url = GetOrderUrl(order2.Id);

                Get(HttpStatusCode.NotFound, order1Url);
                AssertPageContains("cannot be found");

                Get(HttpStatusCode.NotFound, order2Url);
                AssertPageContains("cannot be found");

                Get(_searchUrl);
                var creditAdjustments = Combine(product1.CreditAdjustments, product2.CreditAdjustments);
                AssertCreditSummary(creditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product2).Duration.Value));
            }
        }

        private void TestEmployerOrganisationOrders(string product1Name, string product2Name)
        {
            var employer = CreateEmployer();
            var organisation = GetOrderOrganisation(employer);

            var product1 = _productsQuery.GetProduct(product1Name);
            var product2 = _productsQuery.GetProduct(product2Name);

            var order1 = PurchaseOrder(employer.Id, employer, product1);
            Order order2 = null;
            if (organisation != null)
                order2 = PurchaseOrder(organisation.Id, employer, product2);

            // Check credits.

            LogIn(employer);
            Get(_creditsUrl);

            var order1Url = GetOrderUrl(order1.Id);
            AssertEmployerCredits(order1, order1Url, product1.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product1).Duration.Value).Date);

            if (organisation == null)
            {
                AssertNoOrganisationOrders();
            }
            else
            {
                AssertOrganisationCredits(product2.CreditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product2).Duration.Value).Date);

                // Check order.

                var order2Url = GetOrderUrl(order2.Id);

                Get(HttpStatusCode.NotFound, order2Url);
                AssertPageContains("cannot be found");
            }

            Get(_searchUrl);
            var creditAdjustments = organisation != null
                ? Combine(product1.CreditAdjustments, product2.CreditAdjustments)
                : product1.CreditAdjustments;
            AssertCreditSummary(creditAdjustments, DateTime.Now.Add(GetPrimaryAdjustment(product2).Duration.Value).Date);
        }

        private void AssertNoOrganisationOrders()
        {
            AssertPageDoesNotContain("Organisation credit allocations");
        }

        protected Order PurchaseOrder(Guid ownerId, Employer employer, Product product)
        {
            // Purchase.

            var creditCard = CreateCreditCard();
            var order = _ordersCommand.PrepareOrder(new[] { product.Id }, null, null, creditCard.CardType);
            _ordersCommand.PurchaseOrder(ownerId, order, CreatePurchaser(employer), creditCard);
            return order;
        }

        protected void AssertEmployerCredits(Order order, ReadOnlyUrl orderUrl, IEnumerable<ProductCreditAdjustment> creditAdjustments, DateTime expiryDate)
        {
            AssertUrl(_creditsUrl);

            foreach (var creditAdjustment in creditAdjustments)
            {
                var found = false;

                var credit = _creditsQuery.GetCredit(creditAdjustment.CreditId);
                var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@id='personal-credits']//tr");
                Assert.IsNotNull(trNodes);
                foreach (var trNode in trNodes)
                {
                    var childNodes = trNode.ChildNodes.Where(n => !string.IsNullOrEmpty(n.InnerText.Trim())).ToList();
                    if (childNodes[0].InnerText == credit.ShortDescription
                        && childNodes[1].InnerText == (creditAdjustment.Quantity == null ? "unlimited" : creditAdjustment.Quantity.ToString())
                        && childNodes[2].InnerText == expiryDate.ToShortDateString()
                        && childNodes[3].InnerHtml.Equals(GetOrderLink(order, orderUrl), StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    Assert.Fail("Credit not found.");
            }
        }

        protected void AssertOrganisationCredits(IEnumerable<ProductCreditAdjustment> creditAdjustments, DateTime expiryDate)
        {
            AssertUrl(_creditsUrl);

            foreach (var creditAdjustment in creditAdjustments)
            {
                var found = false;

                var credit = _creditsQuery.GetCredit(creditAdjustment.CreditId);
                var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@id='organisation-credits']//tr");
                Assert.IsNotNull(trNodes);
                foreach (var trNode in trNodes)
                {
                    var childNodes = trNode.ChildNodes.Where(n => !string.IsNullOrEmpty(n.InnerText.Trim())).ToList();
                    if (childNodes[1].InnerText == credit.ShortDescription
                        && childNodes[2].InnerText == (creditAdjustment.Quantity == null ? "unlimited" : creditAdjustment.Quantity.ToString())
                        && childNodes[3].InnerText == expiryDate.ToShortDateString())
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    Assert.Fail("Credit not found.");
            }
        }

        private void AssertOrders(Order order, ReadOnlyUrl orderUrl)
        {
            AssertUrl(_ordersUrl);

            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//tr");
            Assert.IsNotNull(trNodes);
            foreach (var trNode in trNodes)
            {
                var childNodes = trNode.ChildNodes.Where(n => !string.IsNullOrEmpty(n.InnerText.Trim())).ToList();
                if (childNodes[0].InnerHtml.Equals(GetOrderLink(order, orderUrl), StringComparison.OrdinalIgnoreCase)
                    && childNodes[1].InnerText == order.Time.ToShortDateString() + " " + order.Time.ToShortTimeString()
                    && childNodes[2].InnerText == order.AdjustedPrice.ToString("C", order.Currency.CultureInfo))
                    return;
            }

            Assert.Fail("Order not found.");
        }

        private void AssertNoOrders()
        {
            AssertUrl(_ordersUrl);

            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']");
            Assert.IsNotNull(trNodes);

            // 1 for the header.

            Assert.AreEqual(1, trNodes.Count);
        }

        private void AssertOrder(Order order, ReadOnlyUrl orderUrl)
        {
            AssertUrl(orderUrl);
            AssertOrder(order.Price, order.Currency, "Total (excl. GST)");
            AssertOrder(order.AdjustedPrice, order.Currency, "Total amount payable (incl. GST)");
        }

        private void AssertOrder(IFormattable price, Currency currency, string text)
        {
            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//tr");
            Assert.IsNotNull(trNodes);
            foreach (var trNode in trNodes)
            {
                var childNodes = trNode.ChildNodes.Where(n => !string.IsNullOrEmpty(n.InnerText.Trim())).ToList();
                if (childNodes[0].InnerText == text
                    && childNodes[1].InnerText == price.ToString("C", currency.CultureInfo))
                    return;
            }

            Assert.Fail("Price not found.");
        }

        private void AssertOrder(ReadOnlyUrl orderUrl, Product product)
        {
            AssertUrl(orderUrl);

            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//tr");
            Assert.IsNotNull(trNodes);
            foreach (var trNode in trNodes)
            {
                var childNodes = trNode.ChildNodes.Where(n => !string.IsNullOrEmpty(n.InnerText.Trim())).ToList();
                if (childNodes[0].InnerText == product.Description
                    && childNodes[2].InnerText == product.Price.ToString("C", product.Currency.CultureInfo))
                    return;
            }

            Assert.Fail("Product not found.");
        }

        protected static ProductCreditAdjustment GetPrimaryAdjustment(Product product)
        {
            return product.CreditAdjustments.Count == 1
                ? product.CreditAdjustments[0]
                : (from a in product.CreditAdjustments where a.Quantity != null select a).First();
        }

        protected void AssertCreditSummary(IEnumerable<ProductCreditAdjustment> creditAdjustments, DateTime expiryDate)
        {
            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='credit-summary']//tr");
            Assert.IsNotNull(trNodes);

            foreach (var creditAdjustment in creditAdjustments)
            {
                // Unlimited quantities are not shown here.

                if (creditAdjustment.Quantity == null)
                    continue;

                var found = false;
                var credit = _creditsQuery.GetCredit(creditAdjustment.CreditId);

                foreach (var trNode in trNodes)
                {
                    var description = trNode.SelectSingleNode("th[@class='credit-item']/a").InnerText;
                    if (description == credit.Description)
                    {
                        Assert.AreEqual(creditAdjustment.Quantity + " credits", trNode.SelectSingleNode("td[@class='credits']").InnerText);
                        Assert.AreEqual("Expires " + expiryDate.ToShortDateString(), trNode.SelectSingleNode("td[@class='expiry']").InnerText.Trim());
                        found = true;
                        break;
                    }
                }

                if (!found)
                    Assert.Fail("Credit not found.");
            }
        }

        private static string GetOrderLink(Order order, ReadOnlyUrl orderUrl)
        {
            return "<a href=\"" + orderUrl.Path + "\">" + order.ConfirmationCode + "</a>";
        }

        private static Purchaser CreatePurchaser(IEmployer employer)
        {
            return new Purchaser
                       {
                           Id = employer.Id,
                           EmailAddress = employer.EmailAddress.Address,
                           IpAddress = "127.0.0.1",
                       };
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

        protected static IEnumerable<ProductCreditAdjustment> Combine(IEnumerable<ProductCreditAdjustment> adjustments1, IEnumerable<ProductCreditAdjustment> adjustments2)
        {
            var creditAdjustments = (from a in adjustments1
                                     select new ProductCreditAdjustment { CreditId = a.CreditId, Duration = a.Duration, Quantity = a.Quantity}).ToList();

            foreach (var creditAdjustment in adjustments2)
            {
                var creditId = creditAdjustment.CreditId;
                var existingCreditAdjustment = (from a in creditAdjustments where a.CreditId == creditId select a).SingleOrDefault();
                if (existingCreditAdjustment == null)
                {
                    creditAdjustments.Add(new ProductCreditAdjustment { CreditId = creditAdjustment.CreditId, Duration = creditAdjustment.Duration, Quantity = creditAdjustment.Quantity });
                }
                else
                {
                    if (existingCreditAdjustment.Quantity != null)
                    {
                        if (creditAdjustment.Quantity == null)
                            existingCreditAdjustment.Quantity = null;
                        else
                            existingCreditAdjustment.Quantity += creditAdjustment.Quantity.Value;
                    }
                }
            }

            return creditAdjustments;
        }

        protected abstract Employer CreateEmployer();
        protected abstract IOrganisation GetOrderOrganisation(Employer employer);
    }
}
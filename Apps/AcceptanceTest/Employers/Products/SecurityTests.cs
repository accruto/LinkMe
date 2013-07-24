using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products
{
    [TestClass]
    public class SecurityTests
        : ProductsTests
    {
        private readonly IOrdersCommand _ordersCommand = Resolve<IOrdersCommand>();
        private ReadOnlyUrl _loginUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _loginUrl = new ReadOnlyApplicationUrl(true, "~/employers/login");
        }

        [TestMethod]
        public void TestCreditsLoggedInVerified()
        {
            TestCreditsLoggedIn(true);
        }

        [TestMethod]
        public void TestCreditsLoggedInUnverified()
        {
            TestCreditsLoggedIn(false);
        }

        [TestMethod]
        public void TestOrdersLoggedInVerified()
        {
            TestOrdersLoggedIn(true);
        }

        [TestMethod]
        public void TestOrdersLoggedInUnverified()
        {
            TestOrdersLoggedIn(false);
        }

        [TestMethod]
        public void TestOrderLoggedInVerified()
        {
            TestOrderLoggedIn(true);
        }

        [TestMethod]
        public void TestOrderLoggedInUnverified()
        {
            TestOrderLoggedIn(false);
        }

        [TestMethod]
        public void TestSomeoneElsesOrderVerified()
        {
            TestSomeoneElsesOrder(true);
        }

        [TestMethod]
        public void TestSomeoneElsesOrderUnverified()
        {
            TestSomeoneElsesOrder(false);
        }

        private void TestCreditsLoggedIn(bool verified)
        {
            var employer = CreateEmployer(1, verified);
            TestLoggedIn(employer, _creditsUrl);
        }

        private void TestOrdersLoggedIn(bool verified)
        {
            var employer = CreateEmployer(1, verified);
            TestLoggedIn(employer, _ordersUrl);
        }

        private void TestOrderLoggedIn(bool verified)
        {
            var employer = CreateEmployer(1, verified);
            var creditCard = new CreditCard
            {
                CardHolderName = CardHolderName,
                CardNumber = CreditCardNumber,
                CardType = CreditCardType.MasterCard,
                Cvv = Cvv,
                ExpiryDate = new ExpiryDate(DateTime.Now.AddYears(1))
            };

            var order = _ordersCommand.PrepareOrder(new[] { _productsQuery.GetProducts()[0].Id }, null, null, creditCard.CardType);
            _ordersCommand.PurchaseOrder(employer.Id, order, CreatePurchaser(employer), creditCard);

            // Try to access the page.

            var url = _orderUrl.AsNonReadOnly();
            url.Path = url.Path + "/";
            url = new ApplicationUrl(url, order.Id.ToString());

            TestLoggedIn(employer, url);
        }

        private void TestSomeoneElsesOrder(bool verified)
        {
            var employer1 = CreateEmployer(1, verified);
            var employer2 = CreateEmployer(2, verified);

            var creditCard = new CreditCard
            {
                CardHolderName = CardHolderName,
                CardNumber = CreditCardNumber,
                CardType = CreditCardType.MasterCard,
                Cvv = Cvv,
                ExpiryDate = new ExpiryDate(DateTime.Now.AddYears(1))
            };

            var order = _ordersCommand.PrepareOrder(new[] { _productsQuery.GetProducts()[0].Id }, null, null, creditCard.CardType);
            _ordersCommand.PurchaseOrder(employer1.Id, order, CreatePurchaser(employer1), creditCard);

            // Try to access the page.

            var url = _orderUrl.AsNonReadOnly();
            url.Path = url.Path + "/";
            url = new ApplicationUrl(url, order.Id.ToString());

            // Log in as owner.

            LogIn(employer1);
            Get(url);
            AssertUrl(url);
            LogOut();

            // Log in as other.

            LogIn(employer2);
            Get(HttpStatusCode.NotFound, url);
            AssertUrl(url);
            AssertPageContains("cannot be found");
        }

        private void TestLoggedIn(Employer employer, ReadOnlyUrl url)
        {
            // Try to access the page.

            Get(url);
            AssertPath(_loginUrl);

            // Login.

            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = Password;
            _loginButton.Click();
            AssertUrl(url);
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
    }
}

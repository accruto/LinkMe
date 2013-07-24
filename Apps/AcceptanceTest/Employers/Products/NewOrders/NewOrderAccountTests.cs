using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Users.Employers.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.NewOrders
{
    [TestClass]
    public class NewOrderAccountTests
        : NewOrderTests
    {
        private readonly IEmployerAccountsQuery _employerAccountsQuery = Resolve<IEmployerAccountsQuery>();

        [TestMethod]
        public void TestJoin()
        {
            TestNewOrder(Join, () => _employerAccountsQuery.GetEmployer(LoginId));
        }

        [TestMethod]
        public void TestLoggedIn()
        {
            var employer = CreateEmployer(LoginId, false);
            LogIn(employer);

            TestNewOrder(AlreadyLoggedIn, () => employer);
        }

        [TestMethod]
        public void TestLoggingIn()
        {
            var employer = CreateEmployer(LoginId, false);
            TestNewOrder(LogIn, () => employer);
        }

        private static void AlreadyLoggedIn()
        {
        }

        private void LogIn()
        {
            _loginIdTextBox.Text = LoginId;
            _passwordTextBox.Text = Password;
            _loginButton.Click();
        }

        private void Join()
        {
            _joinLoginIdTextBox.Text = LoginId;
            _joinPasswordTextBox.Text = Password;
            _joinConfirmPasswordTextBox.Text = Password;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _emailAddressTextBox.Text = EmailAddress;
            _phoneNumberTextBox.Text = PhoneNumber;
            _organisationNameTextBox.Text = OrganisationName;
            _locationTextBox.Text = Location;
            _acceptTermsCheckBox.IsChecked = true;

            _joinButton.Click();
        }

        private void TestNewOrder(Action accountAction, Func<Employer> getEmployer)
        {
            var product = _productsQuery.GetProduct("Contacts40");
            var products = new[] {product};
            var adjustments = GetAdjustments(product);

            // Choose.

            var instanceId = Choose(product);

            // Do what needs to be done.

            accountAction();
            AssertPaymentPage(instanceId, products, adjustments, false);

            // Pay.

            Pay(instanceId, null, CreditCardType.MasterCard, false, null);
            AssertReceiptPage(instanceId, products, adjustments);
            AssertOrdersPage(adjustments);

            var employer = getEmployer();
            AssertOrderPage(employer.Id, products, adjustments);
            AssertEmail(products, adjustments);

            AssertOrder(employer.Id, adjustments);
        }

        private Guid Choose(Product product)
        {
            Get(GetChooseUrl());
            SelectContactProduct(product);
            _purchaseButton.Click();
            return GetInstanceId();
        }

        private static IList<OrderAdjustment> GetAdjustments(Product product)
        {
            var adjustments = new List<OrderAdjustment>();

            // Set up expected adjustments.

            var adjustedPrice = (1 + TaxRate) * product.Price;
            adjustments.Add(new TaxAdjustment { InitialPrice = product.Price, AdjustedPrice = adjustedPrice, TaxRate = TaxRate });

            return adjustments;
        }
    }
}
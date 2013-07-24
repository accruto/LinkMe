using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.NewOrders
{
    [TestClass]
    public class NewOrderDataTests
        : NewOrderTests
    {
        [TestMethod]
        public void TestChooseDataNotLoggedIn()
        {
            TestChooseData(false);
        }

        [TestMethod]
        public void TestChooseDataLoggedInVerified()
        {
            TestChooseDataLoggedIn(true);
        }

        [TestMethod]
        public void TestChooseDataLoggedInUnverified()
        {
            TestChooseDataLoggedIn(false);
        }

        [TestMethod]
        public void TestJoinDataPrevious()
        {
            // Navigate to page.

            Get(GetChooseUrl());
            SelectContactProduct(_productsQuery.GetProduct("Contacts5"));
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            // Fill in and go back.

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

            _backButton.Click();
            AssertUrl(GetChooseUrl(instanceId));
            _purchaseButton.Click();

            // Password and confirm password will be filled.

            Assert.AreEqual(string.Empty, _loginIdTextBox.Text);
            Assert.AreEqual(string.Empty, _passwordTextBox.Text);
            Assert.AreEqual(false, _rememberMeCheckBox.IsChecked);

            Assert.AreEqual(LoginId, _joinLoginIdTextBox.Text);
            Assert.AreEqual(Password, _joinPasswordTextBox.Text);
            Assert.AreEqual(Password, _joinConfirmPasswordTextBox.Text);
            Assert.AreEqual(FirstName, _firstNameTextBox.Text);
            Assert.AreEqual(LastName, _lastNameTextBox.Text);
            Assert.AreEqual(EmailAddress, _emailAddressTextBox.Text);
            Assert.AreEqual(PhoneNumber, _phoneNumberTextBox.Text);
            Assert.AreEqual(OrganisationName, _organisationNameTextBox.Text);
            Assert.AreEqual(Location, _locationTextBox.Text);
            Assert.AreEqual(true, _acceptTermsCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestJoinDataNext()
        {
            // Navigate to page.

            Get(GetChooseUrl());
            SelectContactProduct(_productsQuery.GetProduct("Contacts5"));
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            // Fill in and go forward.

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
            AssertUrl(GetPaymentUrl(instanceId));
            _backButton.Click();

            // Password, confirm password will be filled.

            Assert.AreEqual(string.Empty, _loginIdTextBox.Text);
            Assert.AreEqual(string.Empty, _passwordTextBox.Text);
            Assert.AreEqual(false, _rememberMeCheckBox.IsChecked);

            Assert.AreEqual(LoginId, _joinLoginIdTextBox.Text);
            Assert.AreEqual(Password, _joinPasswordTextBox.Text);
            Assert.AreEqual(Password, _joinConfirmPasswordTextBox.Text);
            Assert.AreEqual(FirstName, _firstNameTextBox.Text);
            Assert.AreEqual(LastName, _lastNameTextBox.Text);
            Assert.AreEqual(EmailAddress, _emailAddressTextBox.Text);
            Assert.AreEqual(PhoneNumber, _phoneNumberTextBox.Text);
            Assert.AreEqual(OrganisationName, _organisationNameTextBox.Text);
            Assert.AreEqual(Location, _locationTextBox.Text);
            Assert.AreEqual(true, _acceptTermsCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestLoginDataPrevious()
        {
            var employer = CreateEmployer(1, true);

            // Navigate to page.

            Get(GetChooseUrl());
            SelectContactProduct(_productsQuery.GetProduct("Contacts5"));
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            // Fill in and go back.

            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = Password;
            _rememberMeCheckBox.IsChecked = true;

            _backButton.Click();
            AssertUrl(GetChooseUrl(instanceId));
            _purchaseButton.Click();

            // Password will be filled.

            Assert.AreEqual(employer.GetLoginId(), _loginIdTextBox.Text);
            Assert.AreEqual(Password, _passwordTextBox.Text);
            Assert.AreEqual(true, _rememberMeCheckBox.IsChecked);

            Assert.AreEqual(string.Empty, _joinLoginIdTextBox.Text);
            Assert.AreEqual(string.Empty, _joinPasswordTextBox.Text);
            Assert.AreEqual(string.Empty, _joinConfirmPasswordTextBox.Text);
            Assert.AreEqual(string.Empty, _firstNameTextBox.Text);
            Assert.AreEqual(string.Empty, _lastNameTextBox.Text);
            Assert.AreEqual(string.Empty, _emailAddressTextBox.Text);
            Assert.AreEqual(string.Empty, _phoneNumberTextBox.Text);
            Assert.AreEqual(string.Empty, _organisationNameTextBox.Text);
            Assert.AreEqual(string.Empty, _locationTextBox.Text);
            Assert.AreEqual(false, _acceptTermsCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestLoginDataNext()
        {
            var employer = CreateEmployer(1, true);

            // Navigate to page.

            Get(GetChooseUrl());
            SelectContactProduct(_productsQuery.GetProduct("Contacts5"));
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            // Fill in and go forward.

            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = Password;
            _rememberMeCheckBox.IsChecked = true;

            _loginButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            _backButton.Click();

            // Password will be filled.

            Assert.AreEqual(employer.GetLoginId(), _loginIdTextBox.Text);
            Assert.AreEqual(Password, _passwordTextBox.Text);
            Assert.AreEqual(true, _rememberMeCheckBox.IsChecked);

            Assert.AreEqual(string.Empty, _joinLoginIdTextBox.Text);
            Assert.AreEqual(string.Empty, _joinPasswordTextBox.Text);
            Assert.AreEqual(string.Empty, _joinConfirmPasswordTextBox.Text);
            Assert.AreEqual(string.Empty, _firstNameTextBox.Text);
            Assert.AreEqual(string.Empty, _lastNameTextBox.Text);
            Assert.AreEqual(string.Empty, _emailAddressTextBox.Text);
            Assert.AreEqual(string.Empty, _phoneNumberTextBox.Text);
            Assert.AreEqual(string.Empty, _organisationNameTextBox.Text);
            Assert.AreEqual(string.Empty, _locationTextBox.Text);
            Assert.AreEqual(false, _acceptTermsCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestPaymentDataNotLoggedIn()
        {
            // Navigate to page.

            Get(GetChooseUrl());
            SelectContactProduct(_productsQuery.GetProduct("Contacts5"));
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

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
            AssertUrl(GetPaymentUrl(instanceId));

            // Fill in and go back.

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;

            var expiryDate = new ExpiryDate(DateTime.Now.AddMonths(1));
            _cardTypeDropDownList.SelectedIndex = 1;
            _expiryMonthDropDownList.SelectedIndex = expiryDate.Month % 12;
            _expiryYearDropDownList.SelectedIndex = 3;

            _backButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            _joinButton.Click();

            // Check things are maintained.

            Assert.AreEqual(CreditCardNumber, _cardNumberTextBox.Text);
            Assert.AreEqual(Cvv, _cvvTextBox.Text);
            Assert.AreEqual(CardHolderName, _cardHolderNameTextBox.Text);
            Assert.AreEqual(1, _cardTypeDropDownList.SelectedIndex);
            Assert.AreEqual(expiryDate.Month % 12, _expiryMonthDropDownList.SelectedIndex);
            Assert.AreEqual(3, _expiryYearDropDownList.SelectedIndex);
            Assert.AreEqual(true, _authoriseCreditCardCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestPaymentDataLoggedInVerified()
        {
            TestPaymentDataLoggedIn(true);
        }

        [TestMethod]
        public void TestPaymentDataLoggedInUnverified()
        {
            TestPaymentDataLoggedIn(false);
        }

        private void TestChooseDataLoggedIn(bool verified)
        {
            LogIn(verified);
            TestChooseData(true);
        }

        private void TestPaymentDataLoggedIn(bool verified)
        {
            LogIn(verified);

            // Navigate to page.

            Get(GetChooseUrl());
            SelectContactProduct(_productsQuery.GetProduct("Contacts5"));
            
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            AssertUrl(GetPaymentUrl(instanceId));

            // Fill in and go back.

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;

            var expiryDate = new ExpiryDate(DateTime.Now.AddMonths(1));
            _cardTypeDropDownList.SelectedIndex = 1;
            _expiryMonthDropDownList.SelectedIndex = expiryDate.Month % 12;
            _expiryYearDropDownList.SelectedIndex = 3;

            _authoriseCreditCardCheckBox.IsChecked = true;

            _backButton.Click();
            AssertUrl(GetChooseUrl(instanceId));
            _purchaseButton.Click();

            // Check things are maintained.

            Assert.AreEqual(CreditCardNumber, _cardNumberTextBox.Text);
            Assert.AreEqual(Cvv, _cvvTextBox.Text);
            Assert.AreEqual(CardHolderName, _cardHolderNameTextBox.Text);
            Assert.AreEqual(1, _cardTypeDropDownList.SelectedIndex);
            Assert.AreEqual(expiryDate.Month % 12, _expiryMonthDropDownList.SelectedIndex);
            Assert.AreEqual(3, _expiryYearDropDownList.SelectedIndex);
            Assert.AreEqual(true, _authoriseCreditCardCheckBox.IsChecked);
        }

        private void TestChooseData(bool loggedIn)
        {
            var contact5Product = _productsQuery.GetProduct("Contacts5");
            var contact40Product = _productsQuery.GetProduct("Contacts40");

            // Navigate to page and check defaults.

            Get(GetChooseUrl());
            Assert.AreEqual(contact5Product.Id, GetSelectedContactProductId());

            // Check a candidate contact.

            SelectContactProduct(contact40Product);
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            AssertUrl(loggedIn ? GetPaymentUrl(instanceId) : GetAccountUrl(instanceId));
            _backButton.Click();
            AssertUrl(GetChooseUrl(instanceId));
            Assert.AreEqual(contact40Product.Id, GetSelectedContactProductId());

            // Check another candidate contact.

            SelectContactProduct(contact5Product);
            _purchaseButton.Click();
            AssertUrl(loggedIn ? GetPaymentUrl(instanceId) : GetAccountUrl(instanceId));
            _backButton.Click();
            Assert.AreEqual(contact5Product.Id, GetSelectedContactProductId());

            // Check another candidate contact.

            SelectContactProduct(contact40Product);
            _purchaseButton.Click();
            AssertUrl(loggedIn ? GetPaymentUrl(instanceId) : GetAccountUrl(instanceId));
            _backButton.Click();
            Assert.AreEqual(contact40Product.Id, GetSelectedContactProductId());
        }
    }
}
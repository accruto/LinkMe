using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.NewOrders
{
    [TestClass]
    public class NewOrderValidationTests
        : NewOrderTests
    {
        [TestMethod]
        public void TestContactCreditProductChosen()
        {
            TestProductChosen(_productsQuery.GetProduct("Contacts40"));
        }

        [TestMethod]
        public void TestAccountDefaults()
        {
            GetToAccountPage();

            Assert.AreEqual(string.Empty, _loginIdTextBox.Text);
            Assert.AreEqual(string.Empty, _passwordTextBox.Text);
            Assert.AreEqual(false, _rememberMeCheckBox.IsChecked);
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
        public void TestAccountPreviousDefault()
        {
            var instanceId = GetToAccountPage();

            // Should be able to go back without having filled anything in.

            _backButton.Click();
            AssertUrl(GetChooseUrl(instanceId));
        }

        [TestMethod]
        public void TestLoginLoginId()
        {
            var employer = CreateEmployer(1, false);

            var instanceId = GetToAccountPage();
            FillLoginPage(employer);

            // Fail.

            AssertLoginValidationError(instanceId, "The username is required.", v => _loginIdTextBox.Text = v, string.Empty);

            // Pass.

            AssertNoLoginValidationError(instanceId, v => _loginIdTextBox.Text = v, employer.GetLoginId());
        }

        [TestMethod]
        public void TestLoginPassword()
        {
            var employer = CreateEmployer(1, false);

            var instanceId = GetToAccountPage();
            FillLoginPage(employer);

            // Fail.

            AssertLoginValidationError(instanceId, "The password is required.", v => _passwordTextBox.Text = v, string.Empty);

            // Pass.

            AssertNoLoginValidationError(instanceId, v => _passwordTextBox.Text = v, Password);
        }

        [TestMethod]
        public void TestLoginFail()
        {
            var employer = CreateEmployer(1, false);

            var instanceId = GetToAccountPage();

            // Bad user name, password.

            _loginIdTextBox.Text = "abcdefg";
            _passwordTextBox.Text = "hijklmno";
            _loginButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage("Login failed. Please try again.");

            // Bad user name.

            _loginIdTextBox.Text = "abcdefg";
            _passwordTextBox.Text = Password;
            _loginButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage("Login failed. Please try again.");

            // Bad password.

            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = "hijklmno";
            _loginButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage("Login failed. Please try again.");

            // Pass.

            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = Password;
            _loginButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertNoErrorMessages();
        }

        [TestMethod]
        public void TestJoinLoginId()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();

            // Fail.

            AssertJoinValidationError(instanceId, "The username is required.", v => _joinLoginIdTextBox.Text = v, string.Empty);
            AssertJoinValidationError(instanceId, "The username must be no more than 320 characters in length.", v => _joinLoginIdTextBox.Text = v, new string('a', 321));

            // Pass.

            AssertNoJoinValidationError(instanceId, v => _joinLoginIdTextBox.Text = v, new string('a', 20));
        }

        [TestMethod]
        public void TestJoinPassword()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();

            // Fail.

            AssertJoinValidationError(instanceId, v => _joinPasswordTextBox.Text = v, string.Empty, v => _joinConfirmPasswordTextBox.Text = v, Password, "The password is required.", "The confirm password and password must match.");
            AssertJoinValidationError(instanceId, v => _joinPasswordTextBox.Text = v, Password, v => _joinConfirmPasswordTextBox.Text = v, string.Empty, "The confirm password is required.", "The confirm password and password must match.");
            AssertJoinValidationError(instanceId, v => _joinPasswordTextBox.Text = v, new string('a', 3), v => _joinConfirmPasswordTextBox.Text = v, Password, "The password must be between 6 and 50 characters in length.", "The confirm password and password must match.");
            AssertJoinValidationError(instanceId, v => _joinPasswordTextBox.Text = v, new string('a', 60), v => _joinConfirmPasswordTextBox.Text = v, Password, "The password must be between 6 and 50 characters in length.", "The confirm password and password must match.");
            AssertJoinValidationError(instanceId, v => _joinPasswordTextBox.Text = v, new string('a', 20), v => _joinConfirmPasswordTextBox.Text = v, new string('b', 20), "The confirm password and password must match.");

            // Pass.

            AssertNoJoinValidationError(instanceId, v => _joinPasswordTextBox.Text = v, Password, v => _joinConfirmPasswordTextBox.Text = v, Password);
        }

        [TestMethod]
        public void TestJoinFirstName()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();

            // Fail.

            AssertJoinValidationError(instanceId, "The first name is required.", v => _firstNameTextBox.Text = v, string.Empty);
            AssertJoinValidationError(instanceId, "The first name must be between 2 and 30 characters in length and not have any invalid characters.", v => _firstNameTextBox.Text = v, new string('a', 1));
            AssertJoinValidationError(instanceId, "The first name must be between 2 and 30 characters in length and not have any invalid characters.", v => _firstNameTextBox.Text = v, new string('a', 31));
            AssertJoinValidationError(instanceId, "The first name must be between 2 and 30 characters in length and not have any invalid characters.", v => _firstNameTextBox.Text = v, "abc??def");

            // Pass.

            AssertNoJoinValidationError(instanceId, v => _firstNameTextBox.Text = v, new string('a', 20));
        }

        [TestMethod]
        public void TestJoinLastName()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();

            // Fail.

            AssertJoinValidationError(instanceId, "The last name is required.", v => _lastNameTextBox.Text = v, string.Empty);
            AssertJoinValidationError(instanceId, "The last name must be between 2 and 30 characters in length and not have any invalid characters.", v => _lastNameTextBox.Text = v, new string('a', 1));
            AssertJoinValidationError(instanceId, "The last name must be between 2 and 30 characters in length and not have any invalid characters.", v => _lastNameTextBox.Text = v, new string('a', 31));
            AssertJoinValidationError(instanceId, "The last name must be between 2 and 30 characters in length and not have any invalid characters.", v => _lastNameTextBox.Text = v, "abc??def");

            // Pass.

            AssertNoJoinValidationError(instanceId, v => _lastNameTextBox.Text = v, new string('a', 20));
        }

        [TestMethod]
        public void TestJoinEmailAddress()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();

            // Fail.

            AssertJoinValidationError(instanceId, "The email address is required.", v => _emailAddressTextBox.Text = v, string.Empty);
            AssertJoinValidationError(instanceId, "The email address must be valid and have less than 320 characters.", v => _emailAddressTextBox.Text = v, "xxx@");
            AssertJoinValidationError(instanceId, "The email address must be valid and have less than 320 characters.", v => _emailAddressTextBox.Text = v, "asdd@???");

            // Pass.

            AssertNoJoinValidationError(instanceId, v => _emailAddressTextBox.Text = v, EmailAddress);
        }

        [TestMethod]
        public void TestJoinPhoneNumber()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();

            // Fail.

            AssertJoinValidationError(instanceId, "The phone number is required.", v => _phoneNumberTextBox.Text = v, string.Empty);
            AssertJoinValidationError(instanceId, "The phone number must be between 8 and 20 characters in length and not have any invalid characters.", v => _phoneNumberTextBox.Text = v, new string('1', 6));
            AssertJoinValidationError(instanceId, "The phone number must be between 8 and 20 characters in length and not have any invalid characters.", v => _phoneNumberTextBox.Text = v, new string('1', 25));
            AssertJoinValidationError(instanceId, "The phone number must be between 8 and 20 characters in length and not have any invalid characters.", v => _phoneNumberTextBox.Text = v, "123abc567");

            // Pass.

            AssertNoJoinValidationError(instanceId, v => _phoneNumberTextBox.Text = v, PhoneNumber);
        }

        [TestMethod]
        public void TestJoinOrganisationName()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();

            // Fail.

            AssertJoinValidationError(instanceId, "The organisation name is required.", v => _organisationNameTextBox.Text = v, string.Empty);
            AssertJoinValidationError(instanceId, "The organisation name must be between 1 and 100 characters in length and not have any invalid characters.", v => _organisationNameTextBox.Text = v, new string('1', 105));

            // Pass.

            AssertNoJoinValidationError(instanceId, v => _organisationNameTextBox.Text = v, OrganisationName);
        }

        [TestMethod]
        public void TestJoinLocation()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();

            // Fail.

            AssertJoinValidationError(instanceId, "The location is required.", v => _locationTextBox.Text = v, string.Empty);

            // Pass.

            AssertNoJoinValidationError(instanceId, v => _locationTextBox.Text = v, Location);
        }

        [TestMethod]
        public void TestJoinAcceptTerms()
        {
            var instanceId = GetToAccountPage();
            _joinLoginIdTextBox.Text = LoginId;
            _joinPasswordTextBox.Text = Password;
            _joinConfirmPasswordTextBox.Text = Password;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _emailAddressTextBox.Text = EmailAddress;
            _phoneNumberTextBox.Text = PhoneNumber;
            _organisationNameTextBox.Text = OrganisationName;
            _locationTextBox.Text = Location;

            // Going previous should not validate.

            _backButton.Click();
            AssertUrl(GetChooseUrl(instanceId));
            AssertNoErrorMessages();
            _purchaseButton.Click();

            // Going next should validate.

            _joinButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage("Please accept the terms and conditions.");

            // Explicitly set it to false.

            _acceptTermsCheckBox.IsChecked = false;
            _joinButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage("Please accept the terms and conditions.");

            // Set it to true.

            _acceptTermsCheckBox.IsChecked = true;
            _joinButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertNoErrorMessages();
        }

        [TestMethod]
        public void TestMultipleJoinFields()
        {
            var instanceId = GetToAccountPage();

            // Nothing filled in.

            _joinButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessages(
                "The username is required.",
                "The password is required.",
                "The confirm password is required.",
                "The first name is required.",
                "The last name is required.",
                "The email address is required.",
                "The phone number is required.",
                "The organisation name is required.",
                "The location is required.",
                "Please accept the terms and conditions.");

            // Multiple errors.

            _joinLoginIdTextBox.Text = new string('a', 321);
            _joinPasswordTextBox.Text = new string('a', 3);
            _joinConfirmPasswordTextBox.Text = new string('a', 3);
            _firstNameTextBox.Text = new string('a', 1);
            _lastNameTextBox.Text = "abc??def";
            _emailAddressTextBox.Text = string.Empty;
            _phoneNumberTextBox.Text = new string('1', 25);
            _organisationNameTextBox.Text = string.Empty;
            _loginIdTextBox.Text = string.Empty;

            _joinButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessages(
                "The username must be no more than 320 characters in length.",
                "The password must be between 6 and 50 characters in length.",
                "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The email address is required.",
                "The phone number must be between 8 and 20 characters in length and not have any invalid characters.",
                "The organisation name is required.",
                "The location is required.",
                "Please accept the terms and conditions.");

            // Some errors.

            _joinLoginIdTextBox.Text = new string('a', 321);
            _joinPasswordTextBox.Text = new string('a', 3);
            _joinConfirmPasswordTextBox.Text = new string('a', 3);
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = "abc??def";
            _emailAddressTextBox.Text = EmailAddress;
            _phoneNumberTextBox.Text = new string('1', 25);
            _organisationNameTextBox.Text = OrganisationName;
            _locationTextBox.Text = Location;

            _joinButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessages(
                "The username must be no more than 320 characters in length.",
                "The password must be between 6 and 50 characters in length.",
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The phone number must be between 8 and 20 characters in length and not have any invalid characters.",
                "Please accept the terms and conditions.");
        }

        [TestMethod]
        public void TestPaymentDefaults()
        {
            GetToPaymentPage();

            Assert.AreEqual(string.Empty, _cardNumberTextBox.Text);
            Assert.AreEqual(string.Empty, _cvvTextBox.Text);
            Assert.AreEqual(string.Empty, _cardHolderNameTextBox.Text);
            Assert.AreEqual(0, _cardTypeDropDownList.SelectedIndex);
            Assert.AreEqual(((CreditCardType)0).ToString(), _cardTypeDropDownList.SelectedItem.Text);
            var expiryDate = new ExpiryDate(DateTime.Now.AddMonths(1));
            Assert.AreEqual(expiryDate.Month - 1, _expiryMonthDropDownList.SelectedIndex);
            Assert.AreEqual(expiryDate.Month.ToString("D2"), _expiryMonthDropDownList.SelectedItem.Text);
            Assert.AreEqual(expiryDate.Month == 1 ? 1 : 0, _expiryYearDropDownList.SelectedIndex);
            Assert.AreEqual((expiryDate.Year % 100).ToString("D2"), _expiryYearDropDownList.SelectedItem.Text);
        }

        [TestMethod]
        public void TestPaymentPreviousDefault()
        {
            var instanceId = GetToPaymentPage();

            // Should be able to go back without having filled anything in.

            _backButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
        }

        [TestMethod]
        public void TestCreditCardNumber()
        {
            var instanceId = GetToPaymentPage();
            FillPaymentPage();

            // Fail.

            AssertPaymentValidationError(instanceId, "The credit card number is required.", v => _cardNumberTextBox.Text = v, string.Empty);
            AssertPaymentValidationError(instanceId, "The credit card number must be between 13 and 16 characters in length and not have any invalid characters.", v => _cardNumberTextBox.Text = v, "12345678a90123");
            AssertPaymentValidationError(instanceId, "The credit card number must be between 13 and 16 characters in length and not have any invalid characters.", v => _cardNumberTextBox.Text = v, "1234");
            AssertPaymentValidationError(instanceId, "The credit card number must be between 13 and 16 characters in length and not have any invalid characters.", v => _cardNumberTextBox.Text = v, "12345678901234567");

            // Pass.

            AssertNoPaymentValidationError(instanceId, v => _cardNumberTextBox.Text = v, CreditCardNumber);
        }

        [TestMethod]
        public void TestCvv()
        {
            var instanceId = GetToPaymentPage();
            FillPaymentPage();

            // Fail.

            AssertPaymentValidationError(instanceId, "The security code is required.", v => _cvvTextBox.Text = v, string.Empty);
            AssertPaymentValidationError(instanceId, "The security code must be between 3 and 4 characters in length and not have any invalid characters.", v => _cvvTextBox.Text = v, "123a");
            AssertPaymentValidationError(instanceId, "The security code must be between 3 and 4 characters in length and not have any invalid characters.", v => _cvvTextBox.Text = v, "12");
            AssertPaymentValidationError(instanceId, "The security code must be between 3 and 4 characters in length and not have any invalid characters.", v => _cvvTextBox.Text = v, "12345678901234567");

            // Pass.

            AssertNoPaymentValidationError(instanceId, v => _cvvTextBox.Text = v, Cvv);
        }

        [TestMethod]
        public void TestCardHolderName()
        {
            var instanceId = GetToPaymentPage();
            FillPaymentPage();

            // Fail.

            AssertPaymentValidationError(instanceId, "The card holder name is required.", v => _cardHolderNameTextBox.Text = v, string.Empty);
            AssertPaymentValidationError(instanceId, "The card holder name must be no more than 100 characters in length.", v => _cardHolderNameTextBox.Text = v, new string('a', 120));

            // Pass.

            AssertNoPaymentValidationError(instanceId, v => _cardHolderNameTextBox.Text = v, CardHolderName);
        }

        [TestMethod]
        public void TestJoinDuplicateLoginId()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();

            // Create an existing account.

            var employer = _employerAccountsCommand.CreateTestEmployer(LoginId, FirstName, LastName, _organisationsCommand.CreateTestVerifiedOrganisation(OrganisationName));

            // Join with the same login.

            _joinLoginIdTextBox.Text = employer.GetLoginId();
            _joinButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage("The username is already being used.");

            _backButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage("The username is already being used.");
        }

        [TestMethod]
        public void TestAuthoriseCreditCard()
        {
            var instanceId = GetToPaymentPage();
            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;

            // Going previous should not validate.

            _backButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertNoErrorMessages();
            _joinButton.Click();

            // Going next should validate.

            _purchaseButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertErrorMessage("Please authorise the credit card charge.");

            // Explicitly set it to false.

            _authoriseCreditCardCheckBox.IsChecked = false;
            _purchaseButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertErrorMessage("Please authorise the credit card charge.");

            // Set it to true.

            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();
            AssertUrl(GetReceiptUrl(instanceId));
            AssertNoErrorMessages();
        }

        [TestMethod]
        public void TestPaymentDuplicateLoginId()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();

            // Join with the same login.

            _joinLoginIdTextBox.Text = LoginId;
            _joinButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertNoErrorMessages();

            // Now create an existing account.

            _employerAccountsCommand.CreateTestEmployer(LoginId, FirstName, LastName, _organisationsCommand.CreateTestVerifiedOrganisation(OrganisationName));

            FillPaymentPage();
            _purchaseButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertErrorMessage("The username is already being used.");
        }

        private void TestProductChosen(Product contactProduct)
        {
            Get(GetChooseUrl());
            SelectContactProduct(contactProduct);
            _purchaseButton.Click();

            var instanceId = GetInstanceId();
            AssertUrl(GetAccountUrl(instanceId));
            AssertNoErrorMessages();
        }

        private void AssertNoLoginValidationError(Guid instanceId, Action<string> action, string value)
        {
            // Go next.

            action(value);
            _loginButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertNoErrorMessages();
            _backButton.Click();

            // Go previous.

            action(value);
            _backButton.Click();
            AssertUrl(GetChooseUrl(instanceId));
            AssertNoErrorMessages();
        }

        private void AssertLoginValidationError(Guid instanceId, string message, Action<string> action, string value)
        {
            // Go next.

            action(value);
            _loginButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage(message);

            // Go previous.

            action(value);
            _backButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage(message);
        }

        private void AssertNoJoinValidationError(Guid instanceId, Action<string> action, string value)
        {
            // Go next.

            action(value);
            _joinButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertNoErrorMessages();
            _backButton.Click();

            // Go previous.

            action(value);
            _backButton.Click();
            AssertUrl(GetChooseUrl(instanceId));
            AssertNoErrorMessages();
        }

        private void AssertNoJoinValidationError(Guid instanceId, Action<string> action1, string value1, Action<string> action2, string value2)
        {
            // Go next.

            action1(value1);
            action2(value2);
            _joinButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertNoErrorMessages();
            _backButton.Click();

            // Go previous.

            action1(value1);
            action2(value2);
            _backButton.Click();
            AssertUrl(GetChooseUrl(instanceId));
            AssertNoErrorMessages();
        }

        private void AssertJoinValidationError(Guid instanceId, string message, Action<string> action, string value)
        {
            // Go next.

            action(value);
            _joinButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage(message);

            // Go previous.

            action(value);
            _backButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessage(message);
        }

        private void AssertJoinValidationError(Guid instanceId, Action<string> action1, string value1, Action<string> action2, string value2, params string[] messages)
        {
            // Go next.

            action1(value1);
            action2(value2);
            _joinButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessages(messages);

            // Go previous.

            action1(value1);
            action2(value2);
            _backButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertErrorMessages(messages);
        }

        private Guid GetToAccountPage()
        {
            Get(GetChooseUrl());
            _purchaseButton.Click();
            return GetInstanceId();
        }

        private void FillJoinPage()
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
        }

        private void FillLoginPage(Employer employer)
        {
            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = Password;
            _rememberMeCheckBox.IsChecked = true;
        }

        private void AssertPaymentValidationError(Guid instanceId, string message, Action<string> action, string value)
        {
            // Go previous.

            action(value);
            _backButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertErrorMessage(message);

            // Go next.

            action(value);
            _purchaseButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            AssertErrorMessage(message);
        }

        private void AssertNoPaymentValidationError(Guid instanceId, Action<string> action, string value)
        {
            // Go previous.

            action(value);
            _backButton.Click();
            AssertUrl(GetAccountUrl(instanceId));
            AssertNoErrorMessages();
            _joinButton.Click();

            // Go next.

            action(value);
            _purchaseButton.Click();
            AssertUrl(GetReceiptUrl(instanceId));
            AssertNoErrorMessages();
        }

        private Guid GetToPaymentPage()
        {
            var instanceId = GetToAccountPage();
            FillJoinPage();
            _joinButton.Click();
            AssertUrl(GetPaymentUrl(instanceId));
            return instanceId;
        }

        private void FillPaymentPage()
        {
            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
        }
    }
}
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.NewOrders
{
    [TestClass]
    public class NewOrderWizardTests
        : NewOrderTests
    {
        [TestMethod]
        public void TestStepsNotLoggedIn()
        {
            // Choose.

            Get(GetChooseUrl());
            AssertUrl(GetChooseUrl());

            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            // Join.

            AssertUrl(GetAccountUrl(instanceId));

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

            // Payment.

            AssertUrl(GetPaymentUrl(instanceId));

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            // Receipt.

            AssertUrl(GetReceiptUrl(instanceId));
        }

        [TestMethod]
        public void TestStepsLoggedInVerified()
        {
            TestStepsLoggedIn(true);
        }

        [TestMethod]
        public void TestStepsLoggedInUnverified()
        {
            TestStepsLoggedIn(false);
        }

        [TestMethod]
        public void TestNewOrder()
        {
            // NewOrder is the trigger remove any existing order and start again.

            Get(_newOrderUrl);
            AssertUrlWithoutQuery(GetChooseUrl());
            var instanceId = GetInstanceId();

            var product = _productsQuery.GetProduct("Contacts40");
            SelectContactProduct(product);
            _purchaseButton.Click();

            // Reset back to defaults.

            Get(_newOrderUrl);
            AssertUrlWithoutQuery(GetChooseUrl());
            Assert.AreNotEqual(instanceId, GetInstanceId());

            Assert.AreEqual(_productsQuery.GetProduct("Contacts5").Id.ToString(), _contactProductIdDropDownList.SelectedItem.Value);
        }

        [TestMethod]
        public void TestHitAccountFirstNotLoggedIn()
        {
            Get(GetAccountUrl(null));
            AssertUrlWithoutQuery(GetChooseUrl());
        }

        [TestMethod]
        public void TestHitPaymentFirstNotLoggedIn()
        {
            Get(GetPaymentUrl(null));
            AssertUrlWithoutQuery(GetChooseUrl());
        }

        [TestMethod]
        public void TestHitReceiptFirstNotLoggedIn()
        {
            Get(GetReceiptUrl(null));
            AssertUrlWithoutQuery(GetChooseUrl());
        }

        [TestMethod]
        public void TestHitAccountFirstLoggedInVerified()
        {
            TestHitAccountFirstLoggedIn(true);
        }

        [TestMethod]
        public void TestHitAccountFirstLoggedInUnverified()
        {
            TestHitAccountFirstLoggedIn(false);
        }

        [TestMethod]
        public void TestHitPaymentFirstLoggedInVerified()
        {
            TestHitPaymentFirstLoggedIn(true);
        }

        [TestMethod]
        public void TestHitPaymentFirstLoggedInUnverified()
        {
            TestHitPaymentFirstLoggedIn(false);
        }

        [TestMethod]
        public void TestHitReceiptFirstLoggedInVerified()
        {
            TestHitReceiptFirstLoggedIn(true);
        }

        [TestMethod]
        public void TestHitReceiptFirstLoggedInUnverified()
        {
            TestHitReceiptFirstLoggedIn(false);
        }

        [TestMethod]
        public void TestAccountLoginCancel()
        {
            var employer = CreateEmployer(1, false);

            // Make a selection.

            Get(GetChooseUrl());
            var product = _productsQuery.GetProduct("Contacts40");
            SelectContactProduct(product);
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = Password;
            _loginButton.Click();
            _backButton.Click();

            // Cancel.

            AssertUrl(GetAccountUrl(instanceId));
            _cancelButton.Click();
            AssertUrl(_searchUrl);

            // Start again.

            Get(GetAccountUrl(instanceId));
            AssertUrlWithoutQuery(GetChooseUrl());

            Assert.AreNotEqual(instanceId, GetInstanceId());
            Assert.AreEqual(_productsQuery.GetProduct("Contacts5").Id.ToString(), _contactProductIdDropDownList.SelectedItem.Value);
        }

        [TestMethod]
        public void TestAccountJoinCancel()
        {
            // Make a selection.

            Get(GetChooseUrl());
            var product = _productsQuery.GetProduct("Contacts40");
            SelectContactProduct(product);
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
            _backButton.Click();

            // Cancel.

            AssertUrl(GetAccountUrl(instanceId));
            _cancelButton.Click();
            AssertPath(EmployerHomeUrl);

            // Start again.

            Get(GetAccountUrl(instanceId));
            AssertUrlWithoutQuery(GetChooseUrl());

            Assert.AreNotEqual(instanceId, GetInstanceId());
            Assert.AreEqual(_productsQuery.GetProduct("Contacts5").Id.ToString(), _contactProductIdDropDownList.SelectedItem.Value);
        }

        [TestMethod]
        public void TestPaymentCancelNotLoggedIn()
        {
            // Make a selection.

            Get(GetChooseUrl());
            var product = _productsQuery.GetProduct("Contacts40");
            SelectContactProduct(product);
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

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;

            _backButton.Click();
            _joinButton.Click();

            // Cancel.

            AssertUrl(GetPaymentUrl(instanceId));
            _cancelButton.Click();
            AssertPath(EmployerHomeUrl);

            // Start again.

            Get(GetPaymentUrl(instanceId));
            AssertUrlWithoutQuery(GetChooseUrl());

            Assert.AreNotEqual(instanceId, GetInstanceId());
            Assert.AreEqual(_productsQuery.GetProduct("Contacts5").Id.ToString(), _contactProductIdDropDownList.SelectedItem.Value);
        }

        [TestMethod]
        public void TestPaymentCancelLoggedInVerified()
        {
            TestPaymentCancelLoggedIn(true);
        }

        [TestMethod]
        public void TestPaymentCancelLoggedInUnverified()
        {
            TestPaymentCancelLoggedIn(false);
        }

        [TestMethod]
        public void TestCompleteVerified()
        {
            TestComplete(true);
        }

        [TestMethod]
        public void TestCompleteUnverified()
        {
            TestComplete(false);
        }

        [TestMethod]
        public void TestWizardStepsNotLoggedIn()
        {
            var wizardSteps = new [] {"Choose credits", "Log in / Join", "Payment", "Receipt"};

            // Choose.

            Get(GetChooseUrl());
            AssertWizardSteps(wizardSteps, 0);

            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            _purchaseButton.Click();

            // Join.

            AssertWizardSteps(wizardSteps, 1);

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

            // Payment.

            AssertWizardSteps(wizardSteps, 2);

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            // Receipt.

            AssertWizardSteps(wizardSteps, 3);
        }

        [TestMethod]
        public void TestWizardStepsLoggedInVerified()
        {
            TestWizardStepsLoggedIn(true);
        }

        [TestMethod]
        public void TestWizardStepsLoggedInUnverified()
        {
            TestWizardStepsLoggedIn(false);
        }

        [TestMethod]
        public void TestWizardStepsLoggingInVerified()
        {
            TestWizardStepsLoggingIn(true);
        }

        [TestMethod]
        public void TestWizardStepsLoggingInUnverified()
        {
            TestWizardStepsLoggingIn(false);
        }

        [TestMethod]
        public void TestNavNotLoggedIn()
        {
            Get(_newOrderUrl);
            AssertUrlWithoutQuery(GetChooseUrl());
            var instanceId = GetInstanceId();

            // Create an order.

            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            _purchaseButton.Click();

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

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            // Access again.

            Get(_newOrderUrl);
            AssertUrlWithoutQuery(GetChooseUrl());
            Assert.AreNotEqual(instanceId, GetInstanceId());
        }

        [TestMethod]
        public void TestNavLoggedInNotVerified()
        {
            TestNavLoggedIn(false);
        }

        [TestMethod]
        public void TestNavLoggedInVerified()
        {
            TestNavLoggedIn(true);
        }

        private void TestStepsLoggedIn(bool verified)
        {
            LogIn(verified);

            // Choose.

            Get(GetChooseUrl());
            AssertUrl(GetChooseUrl());

            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            // Payment.

            AssertUrl(GetPaymentUrl(instanceId));

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            // Receipt.

            AssertUrl(GetReceiptUrl(instanceId));
        }

        private void TestNavLoggedIn(bool verified)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(1, verified ? _organisationsCommand.CreateTestVerifiedOrganisation(0) : _organisationsCommand.CreateTestOrganisation(0));
            LogIn(employer);

            Get(_newOrderUrl);
            AssertUrlWithoutQuery(GetChooseUrl());
            var instanceId = GetInstanceId();

            // Create an order.

            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            _purchaseButton.Click();

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            // Access again.

            Get(_newOrderUrl);
            AssertUrlWithoutQuery(GetChooseUrl());
            Assert.AreNotEqual(instanceId, GetInstanceId());
        }

        private void TestHitAccountFirstLoggedIn(bool verified)
        {
            LogIn(verified);
            Get(GetAccountUrl(null));
            AssertUrlWithoutQuery(GetChooseUrl());
        }

        private void TestHitPaymentFirstLoggedIn(bool verified)
        {
            LogIn(verified);
            Get(GetPaymentUrl(null));
            AssertUrlWithoutQuery(GetChooseUrl());
        }

        private void TestHitReceiptFirstLoggedIn(bool verified)
        {
            LogIn(verified);
            Get(GetReceiptUrl(null));
            AssertUrlWithoutQuery(GetChooseUrl());
        }

        private void TestPaymentCancelLoggedIn(bool verified)
        {
            LogIn(verified);

            // Make a selection.

            Get(GetChooseUrl());
            var product = _productsQuery.GetProduct("Contacts40");
            SelectContactProduct(product);
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;

            _backButton.Click();
            _purchaseButton.Click();

            // Cancel.

            AssertUrl(GetPaymentUrl(instanceId));
            _cancelButton.Click();
            AssertUrl(_searchUrl);

            // Start again.

            Get(GetPaymentUrl(instanceId));
            AssertUrlWithoutQuery(GetChooseUrl());

            Assert.AreNotEqual(instanceId, GetInstanceId());
            Assert.AreEqual(_productsQuery.GetProduct("Contacts5").Id.ToString(), _contactProductIdDropDownList.SelectedItem.Value);
        }

        private void TestComplete(bool verified)
        {
            LogIn(verified);

            // Make a selection.

            Get(GetChooseUrl());
            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            // Now complete.

            Get(GetChooseUrl(instanceId));
            AssertUrl(GetReceiptUrl(instanceId));

            Get(GetAccountUrl(instanceId));
            AssertUrl(GetReceiptUrl(instanceId));

            Get(GetPaymentUrl(instanceId));
            AssertUrl(GetReceiptUrl(instanceId));

            Get(GetReceiptUrl(instanceId));
            AssertUrl(GetReceiptUrl(instanceId));
        }

        private void TestWizardStepsLoggedIn(bool verified)
        {
            var wizardSteps = new[] { "Choose credits", "Payment", "Receipt" };

            LogIn(verified);

            // Choose.

            Get(GetChooseUrl());
            AssertWizardSteps(wizardSteps, 0);

            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            _purchaseButton.Click();

            // Payment.

            AssertWizardSteps(wizardSteps, 1);

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            // Receipt.

            AssertWizardSteps(wizardSteps, 2);
        }

        private void TestWizardStepsLoggingIn(bool verified)
        {
            var wizardSteps = new[] { "Choose credits", "Log in / Join", "Payment", "Receipt" };
            var employer = CreateEmployer(1, verified);

            // Choose.

            Get(GetChooseUrl());
            AssertWizardSteps(wizardSteps, 0);

            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            _purchaseButton.Click();

            // Log in.

            AssertWizardSteps(wizardSteps, 1);

            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = Password;
            _loginButton.Click();

            // Payment.

            AssertWizardSteps(wizardSteps, 2);

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            // Receipt.

            AssertWizardSteps(wizardSteps, 3);
        }

        private void AssertWizardSteps(IList<string> wizardSteps, int selectedIndex)
        {
            var liNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@id='wizardsteps']/ol/li");
            Assert.IsNotNull(liNodes);
            Assert.AreEqual(wizardSteps.Count, liNodes.Count);

            for (var index = 0; index < wizardSteps.Count; ++index)
            {
                var liNode = liNodes[index];
                Assert.AreEqual(wizardSteps[index], liNode.InnerText.Trim());

                if (index == selectedIndex)
                {
                    if (index == 0)
                        Assert.AreEqual("selected first first-selected", liNode.Attributes["class"].Value);
                    else if (index == wizardSteps.Count - 1)
                        Assert.AreEqual("selected last last-selected", liNode.Attributes["class"].Value);
                    else
                        Assert.AreEqual("selected", liNode.Attributes["class"].Value);
                }
            }
        }
    }
}
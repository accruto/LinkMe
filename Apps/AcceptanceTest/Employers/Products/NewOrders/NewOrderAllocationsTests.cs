using System;
using LinkMe.Apps.Agents.Users.Employers.Queries;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.NewOrders
{
    [TestClass]
    public class NewOrderAllocationsTests
        : NewOrderTests
    {
        private readonly IEmployerAccountsQuery _employerAccountsQuery = Resolve<IEmployerAccountsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly IOrdersQuery _ordersQuery = Resolve<IOrdersQuery>();

        private ReadOnlyUrl _settingsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/employers/settings");
        }

        [TestMethod]
        public void TestContactCreditsNotLoggedIn()
        {
            Get(GetChooseUrl());
            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            PurchaseOrder(false);

            // Check.

            var employer = _employerAccountsQuery.GetEmployer(LoginId);
            var expiryDate = DateTime.Now.AddDays(365).Date;
            AssertCredits<ContactCredit>(employer.Id, 40, expiryDate);
            AssertOrder(employer.Id, _productsQuery.GetProduct("Contacts40").Id);
        }

        [TestMethod]
        public void TestContactCreditsLoggedInVerified()
        {
            TestContactCreditsLoggedIn(true);
        }

        [TestMethod]
        public void TestContactCreditsLoggedInUnverified()
        {
            TestContactCreditsLoggedIn(false);
        }

        private void TestContactCreditsLoggedIn(bool verified)
        {
            var employer = LogIn(verified);
            Get(GetChooseUrl());
            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            PurchaseOrder(true);

            // Check.

            var expiryDate = DateTime.Now.AddDays(365).Date;
            AssertCredits<ContactCredit>(employer.Id, 40, expiryDate);
            AssertOrder(employer.Id, _productsQuery.GetProduct("Contacts40").Id);
        }

        private void PurchaseOrder(bool loggedIn)
        {
            // Choose.

            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            if (!loggedIn)
            {
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
            }

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

        private void AssertCredits<T>(Guid employerId, int? quantity, DateTime? expiryDate)
            where T : Credit
        {
            // Check credits.

            var allocations = _allocationsQuery.GetAllocationsByOwnerId(employerId);
            Assert.AreEqual(1, allocations.Count);
            var allocation = allocations[0];
            var credit = _creditsQuery.GetCredit<T>();
            Assert.AreEqual(credit.Id, allocation.CreditId);
            Assert.AreEqual(quantity, allocation.RemainingQuantity);
            Assert.AreEqual(expiryDate, allocation.ExpiryDate);

            AssertViewCredits(credit, allocation);
        }

        private static void GetAllocations(Credit credit, Allocation allocation, ref Allocation contactAllocation, ref Allocation applicantAllocation, ref Allocation jobAdAllocation)
        {
            if (credit is ContactCredit)
                contactAllocation = allocation;
            else if (credit is ApplicantCredit)
                applicantAllocation = allocation;
            else
                jobAdAllocation = allocation;
        }

        private void AssertViewCredits(Credit credit, Allocation allocation)
        {
            Get(_settingsUrl);

            Allocation contactAllocation = null;
            Allocation applicantAllocation = null;
            Allocation jobAdAllocation = null;
            GetAllocations(credit, allocation, ref contactAllocation, ref applicantAllocation, ref jobAdAllocation);

            AssertContactCredit(contactAllocation);
            AssertApplicantCredit(applicantAllocation);
            AssertJobAdCredit(jobAdAllocation);
        }

        private static string GetAllocationText(string credit, Allocation allocation)
        {
            return credit
                + ": "
                + (allocation.RemainingQuantity == null ? "unlimited" : allocation.RemainingQuantity + " remaining")
                + ", expiring on "
                + allocation.ExpiryDate.Value.ToShortDateString();
        }

        private void AssertJobAdCredit(Allocation allocation)
        {
            AssertPageContains(allocation != null ? GetAllocationText("Job ads", allocation) : "Job ads: none remaining");
        }

        private void AssertApplicantCredit(Allocation allocation)
        {
            AssertPageContains(allocation != null
                ? GetAllocationText("Applicants", allocation)
                : "Applicants: none remaining");
        }

        private void AssertContactCredit(Allocation allocation)
        {
            AssertPageContains(allocation != null
                ? GetAllocationText("Contacts", allocation)
                : "Contacts: none remaining");
        }

        private void AssertOrder(Guid employerId, Guid productId)
        {
            var orders = _ordersQuery.GetOrders(employerId);
            Assert.AreEqual(1, orders.Count);
            var order = orders[0];
            Assert.AreEqual(1, order.Items.Count);
            var item = order.Items[0];
            Assert.AreEqual(productId, item.ProductId);
        }
    }
}
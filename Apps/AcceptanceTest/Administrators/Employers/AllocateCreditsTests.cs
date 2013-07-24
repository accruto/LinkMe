using System;
using System.Globalization;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers
{
    [TestClass]
    public class AllocateCreditsTests
        : CreditsTests
    {
        private HtmlDropDownListTester _creditIdDropDownList;
        private HtmlTextBoxTester _quantityTextBox;
        private HtmlTextBoxTester _expiryDateTextBox;
        private HtmlButtonTester _addButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _creditIdDropDownList = new HtmlDropDownListTester(Browser, "CreditId");
            _quantityTextBox = new HtmlTextBoxTester(Browser, "Quantity");
            _expiryDateTextBox = new HtmlTextBoxTester(Browser, "ExpiryDate");
            _addButton = new HtmlButtonTester(Browser, "add");
        }

        [TestMethod]
        public void TestAllocateUnlimitedExpiryUnlimitedQuantity()
        {
            TestAllocateExpiryQuantity(_creditsQuery.GetCredit<ContactCredit>().Id, null, null);
        }

        [TestMethod]
        public void TestAllocateUnlimitedExpiryLimitedQuantity()
        {
            TestAllocateExpiryQuantity(_creditsQuery.GetCredit<ContactCredit>().Id, null, 23);
        }

        [TestMethod]
        public void TestAllocateLimitedExpiryUnlimitedQuantity()
        {
            TestAllocateExpiryQuantity(_creditsQuery.GetCredit<ContactCredit>().Id, DateTime.Today.AddDays(15), null);
        }

        [TestMethod]
        public void TestAllocateLimitedExpiryLimitedQuantity()
        {
            TestAllocateExpiryQuantity(_creditsQuery.GetCredit<ContactCredit>().Id, DateTime.Today.AddDays(15), 23);
        }

        [TestMethod]
        public void TestRemoveCredits()
        {
            // Create the employer with limited credits.

            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            // LogIn and browse to the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);
            Get(GetCreditsUrl(employer));

            // Allocate.

            Allocate(_creditsQuery.GetCredit<ContactCredit>().Id, null, 20);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var allocations = _allocationsQuery.GetActiveAllocations(employer.Id);
            Assert.AreEqual(1, allocations.Count);
            AssertSavedAllocation(credit, false, 20, 20, false, allocations[0]);
            allocations = _allocationsQuery.GetAllocationsByOwnerId(employer.Id);
            Assert.AreEqual(1, allocations.Count);
            AssertSavedAllocation(credit, false, 20, 20, false, allocations[0]);

            // Deallocate them.

            var allocation = _allocationsQuery.GetAllocationsByOwnerId(employer.Id)[0];
            Post(GetDeallocateUrl(employer, allocation));

            // Check what has been saved.

            allocations = _allocationsQuery.GetActiveAllocations(employer.Id);
            Assert.AreEqual(0, allocations.Count);

            allocations = _allocationsQuery.GetAllocationsByOwnerId(employer.Id);
            Assert.AreEqual(1, allocations.Count);
            AssertSavedAllocation(credit, false, 20, 20, true, allocations[0]);
        }

        [TestMethod]
        public void TestLimitedApplicantCredits()
        {
            var expiryDate = DateTime.Now.Date.AddYears(1);
            TestJobCredits(_creditsQuery.GetCredit<ApplicantCredit>(), expiryDate, 100, expiryDate, null);
        }

        [TestMethod]
        public void TestUnlimitedApplicantCredits()
        {
            var expiryDate = DateTime.Now.Date.AddYears(1);
            TestJobCredits(_creditsQuery.GetCredit<ApplicantCredit>(), expiryDate, null, expiryDate, null);
        }

        [TestMethod]
        public void TestLimitedJobAdCredits()
        {
            var expiryDate = DateTime.Now.Date.AddYears(1);
            TestJobCredits(_creditsQuery.GetCredit<JobAdCredit>(), expiryDate.AddMonths(2), null, expiryDate, 100);
        }

        [TestMethod]
        public void TestUnlimitedJobAdCredits()
        {
            var expiryDate = DateTime.Now.Date.AddYears(1);
            TestJobCredits(_creditsQuery.GetCredit<JobAdCredit>(), expiryDate.AddMonths(2), null, expiryDate, null);
        }

        private void TestJobCredits(Credit credit, DateTime applicantsExpiryDate, int? applicantsQuantity, DateTime jobAdsExpiryDate, int? jobAdsQuantity)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            // LogIn and browse to the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);
            Get(GetCreditsUrl(employer));

            if (credit is ApplicantCredit)
                Allocate(credit.Id, applicantsExpiryDate, applicantsQuantity);
            else
                Allocate(credit.Id, jobAdsExpiryDate, jobAdsQuantity);

            // Check what is saved.

            var allocations = _allocationsQuery.GetActiveAllocations(employer.Id);
            Assert.AreEqual(2, allocations.Count);

            var allocation = (from a in allocations where a.CreditId == _creditsQuery.GetCredit<ApplicantCredit>().Id select a).Single();
            Assert.AreEqual(applicantsExpiryDate, allocation.ExpiryDate.Value);
            Assert.AreEqual(applicantsQuantity, allocation.InitialQuantity);

            allocation = (from a in allocations where a.CreditId == _creditsQuery.GetCredit<JobAdCredit>().Id select a).Single();
            Assert.AreEqual(jobAdsExpiryDate, allocation.ExpiryDate.Value);
            Assert.AreEqual(jobAdsQuantity, allocation.InitialQuantity);
        }

        private void TestAllocateExpiryQuantity(Guid creditId, DateTime? expiryDate, int? quantity)
        {
            // Create the employer with limited credits.

            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            // LogIn and browse to the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);
            Get(GetCreditsUrl(employer));

            // Allocate.

            Allocate(employer.Id, creditId, expiryDate, quantity);
        }

        private void Allocate(Guid employerId, Guid creditId, DateTime? expiryDate, int? quantity)
        {
            Allocate(creditId, expiryDate, quantity);

            AssertPageContains(expiryDate == null ? "never" : expiryDate.Value.ToShortDateString());
            AssertPageContains(quantity == null ? "unlimited" : quantity.ToString());

            // Contact credits is selected by default.

            Assert.AreEqual(1, _creditIdDropDownList.SelectedIndex);
            Assert.AreEqual("", _quantityTextBox.Text);
            Assert.AreEqual("", _expiryDateTextBox.Text);

            // Check what is saved.

            var allocations = _allocationsQuery.GetActiveAllocations(employerId);
            Assert.AreEqual(1, allocations.Count);

            var allocation = allocations[0];
            Assert.AreEqual(creditId, allocation.CreditId);

            if (expiryDate == null)
            {
                Assert.IsTrue(allocation.NeverExpires);
                Assert.AreEqual(null, allocation.ExpiryDate);
            }
            else
            {
                Assert.IsTrue(!allocation.NeverExpires);
                Assert.AreEqual(expiryDate.Value, allocation.ExpiryDate.Value);
            }

            if (quantity == null)
            {
                Assert.IsTrue(allocation.IsUnlimited);
                Assert.AreEqual(null, allocation.RemainingQuantity);
            }
            else
            {
                Assert.IsTrue(!allocation.IsUnlimited);
                Assert.AreEqual(quantity.Value, allocation.RemainingQuantity.Value);
            }
        }

        private void Allocate(Guid creditId, DateTime? expiryDate, int? quantity)
        {
            Allocate(creditId, expiryDate == null ? string.Empty : expiryDate.Value.ToShortDateString(), quantity == null ? string.Empty : quantity.Value.ToString(CultureInfo.InvariantCulture));
        }

        private void Allocate(Guid creditId, string expiryDate, string quantity)
        {
            // Fill in fields.

            _creditIdDropDownList.SelectedValue = creditId.ToString();
            _expiryDateTextBox.Text = expiryDate;
            _quantityTextBox.Text = quantity;

            // Add them.

            _addButton.Click();
        }

        private static void AssertSavedAllocation(Credit credit, bool hasExpirationDate, int? initialQuantity, int? remainingQuantity, bool isDeallocated, Allocation allocation)
        {
            Assert.AreEqual(credit.Id, allocation.CreditId);
            if (hasExpirationDate)
                Assert.IsNotNull(allocation.ExpiryDate);
            else
                Assert.IsNull(allocation.ExpiryDate);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(remainingQuantity, allocation.RemainingQuantity);
            if (isDeallocated)
                Assert.IsNotNull(allocation.DeallocatedTime);
            else
                Assert.IsNull(allocation.DeallocatedTime);
        }
    }
}

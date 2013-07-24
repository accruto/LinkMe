using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Organisations
{
    [TestClass]
    public class CreditsTests
        : OrganisationsTests
    {
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

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
        public void TestUnverifiedOrganisationNoCredits()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestOrganisation(1);
            TestNoCredits(administrator, organisation);
        }

        [TestMethod]
        public void TestVerifiedOrganisationNoCredits()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            TestNoCredits(administrator, organisation);
        }

        [TestMethod]
        public void TestUnverifiedOrganisationCredits()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestOrganisation(1);
            TestCredits(administrator, organisation);
        }

        [TestMethod]
        public void TestVerifiedOrganisationCredits()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            TestCredits(administrator, organisation);
        }

        [TestMethod]
        public void TestAddUnlimitedNeverCredits()
        {
            TestAddCredits(null, null);
        }

        [TestMethod]
        public void TestAddLimitedNeverCredits()
        {
            TestAddCredits(10, null);
        }

        [TestMethod]
        public void TestAddUnlimitedLimitedCredits()
        {
            TestAddCredits(null, DateTime.Now.Date.AddDays(10));
        }

        [TestMethod]
        public void TestAddLimitedLimitedCredits()
        {
            TestAddCredits(10, DateTime.Now.Date.AddDays(10));
        }

        [TestMethod]
        public void TestAddCredits()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            Get(GetCreditsUrl(organisation));

            // Add some.

            Credit credit = _creditsQuery.GetCredit<ContactCredit>();
            var allocations = new List<Allocation>();
            int? quantity = null;
            DateTime? expiryDate = null;

            _creditIdDropDownList.SelectedIndex = 1;
            _addButton.Click();

            allocations.Add(new Allocation { CreditId = credit.Id, InitialQuantity = quantity, ExpiryDate = expiryDate });
            AssertAllocations(organisation.Id, allocations);

            // Add more.

            credit = _creditsQuery.GetCredit<JobAdCredit>();
            quantity = 20;
            expiryDate = DateTime.Now.Date.AddYears(1);

            _creditIdDropDownList.SelectedValue = credit.Id.ToString();
            _quantityTextBox.Text = quantity.ToString();
            _expiryDateTextBox.Text = expiryDate.Value.ToString("dd/MM/yyyy");
            _addButton.Click();

            allocations.Add(new Allocation { CreditId = credit.Id, RemainingQuantity = quantity, ExpiryDate = expiryDate });
            AssertAllocations(organisation.Id, allocations);

            // Add more.

            credit = _creditsQuery.GetCredit<ApplicantCredit>();
            quantity = null;
            expiryDate = DateTime.Now.Date.AddYears(1);

            _creditIdDropDownList.SelectedValue = credit.Id.ToString();
            _quantityTextBox.Text = string.Empty;
            _expiryDateTextBox.Text = expiryDate.Value.ToString("dd/MM/yyyy");
            _addButton.Click();

            allocations.Add(new Allocation { CreditId = credit.Id, RemainingQuantity = quantity, ExpiryDate = expiryDate });
            AssertAllocations(organisation.Id, allocations);
        }

        [TestMethod]
        public void TestDeallocateCredits()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            // Give them a bit of everything everything.

            var contactCredit = _creditsQuery.GetCredit<ContactCredit>();
            var applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();

            var contactAllocation = Allocate(organisation, contactCredit, null, DateTime.Now.AddYears(1));
            var applicantAllocation = Allocate(organisation, applicantCredit, 20, DateTime.Now.AddYears(1));
            var jobAdAllocation = Allocate(organisation, jobAdCredit, 100, null);

            LogIn(administrator);
            Get(GetCreditsUrl(organisation));

            AssertAllocations(organisation.Id, contactAllocation, applicantAllocation, jobAdAllocation);

            // Start deallocating.

            Post(GetDeallocateUrl(organisation, contactAllocation));
            Get(GetCreditsUrl(organisation));
            contactAllocation.DeallocatedTime = DateTime.Now;
            AssertAllocations(organisation.Id, contactAllocation, applicantAllocation, jobAdAllocation);

            Post(GetDeallocateUrl(organisation, applicantAllocation));
            Get(GetCreditsUrl(organisation));
            applicantAllocation.DeallocatedTime = DateTime.Now;
            AssertAllocations(organisation.Id, contactAllocation, applicantAllocation, jobAdAllocation);

            Post(GetDeallocateUrl(organisation, jobAdAllocation));
            Get(GetCreditsUrl(organisation));
            jobAdAllocation.DeallocatedTime = DateTime.Now;
            AssertAllocations(organisation.Id, contactAllocation, applicantAllocation, jobAdAllocation);
        }

        private void TestAddCredits(int? quantity, DateTime? expiryDate)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            Get(GetCreditsUrl(organisation));

            var credit = _creditsQuery.GetCredit<ContactCredit>();

            // Add a contact credit.

            _creditIdDropDownList.SelectedIndex = 1;
            _quantityTextBox.Text = quantity == null ? string.Empty : quantity.Value.ToString(CultureInfo.InvariantCulture);
            _expiryDateTextBox.Text = expiryDate == null ? string.Empty : expiryDate.Value.ToString("dd/MM/yyyy");
            _addButton.Click();

            AssertAllocations(organisation.Id, new Allocation { OwnerId = organisation.Id, CreditId = credit.Id, RemainingQuantity = quantity, ExpiryDate = expiryDate });
        }

        private void TestNoCredits(IUser administrator, IOrganisation organisation)
        {
            LogIn(administrator);
            Get(GetCreditsUrl(organisation));
            AssertPageContains("There are no credits allocated to this organisation.");
        }

        private void TestCredits(IUser administrator, IOrganisation organisation)
        {
            // Allocate.

            var contactCredit = _creditsQuery.GetCredit<ContactCredit>();
            var applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();

            var allocations = new List<Allocation>();
            if (organisation.IsVerified)
            {
                // Give them unlimited everything.

                allocations.Add(Allocate(organisation, contactCredit, null, DateTime.Now.AddYears(1)));
                allocations.Add(Allocate(organisation, applicantCredit, null, DateTime.Now.AddYears(1)));
                allocations.Add(Allocate(organisation, jobAdCredit, null, DateTime.Now.AddYears(1)));
            }
            else
            {
                // Give them a package.

                allocations.Add(Allocate(organisation, contactCredit, 30, DateTime.Now.AddYears(1)));
                allocations.Add(Allocate(organisation, applicantCredit, 400, DateTime.Now.AddYears(1)));
                allocations.Add(Allocate(organisation, jobAdCredit, null, DateTime.Now.AddYears(1)));
            }

            LogIn(administrator);
            Get(GetCreditsUrl(organisation));

            AssertPageDoesNotContain("There are no credits allocated to this organisation.");
            AssertAllocations(organisation.Id, allocations);
        }

        private void AssertAllocations(Guid organisationId, params Allocation[] expectedAllocations)
        {
            AssertAllocations(organisationId, (ICollection<Allocation>) expectedAllocations);
        }

        private void AssertAllocations(Guid organisationId, ICollection<Allocation> expectedAllocations)
        {
            AssertPageAllocations(expectedAllocations);
            AssertSavedAllocations(organisationId, expectedAllocations);
        }

        private void AssertSavedAllocations(Guid organisationId, ICollection<Allocation> expectedAllocations)
        {
            var allocations = _allocationsQuery.GetAllocationsByOwnerId(organisationId);
            AssertAllocations(expectedAllocations, allocations);
        }

        private void AssertPageAllocations(ICollection<Allocation> expectedAllocations)
        {
            // Get all allocations from the page.

            var allocations = new List<Allocation>();
            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            if (trNodes != null)
            {
                foreach (var trNode in trNodes)
                {
                    var credit = GetCredit(trNode.SelectSingleNode("td[position()=1]").InnerText);
                    var quantity = GetQuantity(trNode.SelectSingleNode("td[position()=4]").InnerText);
                    var expiryDate = GetExpiryDate(trNode.SelectSingleNode("td[position()=5]").InnerText);
                    allocations.Add(new Allocation { CreditId = credit.Id, ExpiryDate = expiryDate, RemainingQuantity = quantity });
                }
            }

            AssertAllocations(expectedAllocations, allocations);
        }

        private static void AssertAllocations(ICollection<Allocation> expectedAllocations, ICollection<Allocation> allocations)
        {
            Assert.AreEqual(expectedAllocations.Count, allocations.Count);

            // Check that they are the same.

            foreach (var expectedAllocation in expectedAllocations)
                Assert.IsTrue((from a in allocations
                        where a.CreditId == expectedAllocation.CreditId
                        && a.RemainingQuantity == expectedAllocation.RemainingQuantity
                        && a.ExpiryDate == expectedAllocation.ExpiryDate
                        select a).Any());

            foreach (var allocation in allocations)
                Assert.IsTrue((from a in expectedAllocations
                        where a.CreditId == allocation.CreditId
                        && a.RemainingQuantity == allocation.RemainingQuantity
                        && a.ExpiryDate == allocation.ExpiryDate
                        select a).Any());
        }

        private static DateTime? GetExpiryDate(string text)
        {
            if (text == "never")
                return null;
            return DateTime.Parse(text);
        }

        private static int? GetQuantity(string text)
        {
            if (text == "unlimited")
                return null;
            return int.Parse(text);
        }

        private Credit GetCredit(string text)
        {
            return (from c in _creditsQuery.GetCredits() where c.ShortDescription == text select c).Single();
        }

        private Allocation Allocate(IOrganisation organisation, Credit credit, int? quantity, DateTime? expiryDate)
        {
            var allocation = new Allocation { OwnerId = organisation.Id, CreditId = credit.Id, ExpiryDate = expiryDate, InitialQuantity = quantity };
            _allocationsCommand.CreateAllocation(allocation);
            return allocation;
        }
    }
}

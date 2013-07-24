using System;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers
{
    [TestClass]
    public class ViewAllocationsTests
        : CreditsTests
    {
        [TestMethod]
        public void TestUnlimitedExpiryUnlimitedQuantityDisplay()
        {
            TestExpiryQuantityDisplay(null, null);
        }

        [TestMethod]
        public void TestUnlimitedExpiryLimitedQuantityDisplay()
        {
            TestExpiryQuantityDisplay(null, 23);
        }

        [TestMethod]
        public void TestLimitedExpiryUnlimitedQuantityDisplay()
        {
            TestExpiryQuantityDisplay(DateTime.Today.AddDays(15), null);
        }

        [TestMethod]
        public void TestLimitedExpiryLimitedQuantityDisplay()
        {
            TestExpiryQuantityDisplay(DateTime.Today.AddDays(15), 23);
        }

        private void TestExpiryQuantityDisplay(DateTime? expiryDate, int? quantity)
        {
            // Create the employer with unlimited credits.

            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, ExpiryDate = expiryDate, InitialQuantity = quantity });

            // LogIn and browse to the employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);
            Get(GetCreditsUrl(employer));

            // Assert that everything is as it should be.

            AssertAllocation(_creditsQuery.GetCredit<ContactCredit>(), expiryDate, quantity);
        }
    }
}

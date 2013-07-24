using System;
using LinkMe.Domain.Credits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Credits
{
    [TestClass]
    public class ExpiringAllocationsTests
        : CreditsTests
    {
        [TestMethod]
        public void TestNoAllocations()
        {
            Assert.AreEqual(0, _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10)).Count);
        }

        [TestMethod]
        public void TestExpiringLimitedAllocation()
        {
            TestExpiringAllocation(10);
        }

        [TestMethod]
        public void TestNotExpiringLimitedAllocation()
        {
            TestNotExpiringAllocation(10);
        }

        [TestMethod]
        public void TestExpiredLimitedAllocation()
        {
            TestExpiredAllocation(10);
        }

        [TestMethod]
        public void TestExpiringUnlimitedAllocation()
        {
            TestExpiringAllocation(null);
        }

        [TestMethod]
        public void TestNotExpiringUnlimitedAllocation()
        {
            TestNotExpiringAllocation(null);
        }

        [TestMethod]
        public void TestExpiredUnlimitedAllocation()
        {
            TestExpiredAllocation(null);
        }

        [TestMethod]
        public void TestMultipleCredits()
        {
            var ownerId = Guid.NewGuid();
            var expiryDate = DateTime.Today.Date.AddDays(5);
            const int contactQuantity = 10;
            const int applicantQuantity = 15;
            CreateAllocation<ContactCredit>(ownerId, expiryDate, contactQuantity);
            CreateAllocation<ApplicantCredit>(ownerId, expiryDate, applicantQuantity);

            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(1, allocations.Count);
            AssertAllocation(expiryDate, contactQuantity, allocations[ownerId]);

            allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ApplicantCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(1, allocations.Count);
            AssertAllocation(expiryDate, applicantQuantity, allocations[ownerId]);

            allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<JobAdCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(0, allocations.Count);
        }

        [TestMethod]
        public void TestMultipleBothExpiredAllocations()
        {
            var ownerId = Guid.NewGuid();
            var expiryDate = DateTime.Today.Date.AddDays(5);
            const int quantity1 = 10;
            const int quantity2 = 15;
            CreateAllocation<ContactCredit>(ownerId, expiryDate, quantity1);
            CreateAllocation<ContactCredit>(ownerId, expiryDate, quantity2);

            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(1, allocations.Count);
            AssertAllocation(expiryDate, quantity1 + quantity2, allocations[ownerId]);
        }

        [TestMethod]
        public void TestMultipleOneExpiredAllocations()
        {
            var ownerId = Guid.NewGuid();
            var expiryDate1 = DateTime.Today.Date.AddDays(5);
            var expiryDate2 = DateTime.Today.Date.AddDays(15);
            const int quantity1 = 10;
            const int quantity2 = 15;
            CreateAllocation<ContactCredit>(ownerId, expiryDate1, quantity1);
            CreateAllocation<ContactCredit>(ownerId, expiryDate2, quantity2);

            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(0, allocations.Count);
        }

        [TestMethod]
        public void TestMultipleNeitherExpiredAllocations()
        {
            var ownerId = Guid.NewGuid();
            var expiryDate1 = DateTime.Today.Date.AddDays(15);
            var expiryDate2 = DateTime.Today.Date.AddDays(15);
            const int quantity1 = 10;
            const int quantity2 = 15;
            CreateAllocation<ContactCredit>(ownerId, expiryDate1, quantity1);
            CreateAllocation<ContactCredit>(ownerId, expiryDate2, quantity2);

            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(0, allocations.Count);
        }

        [TestMethod]
        public void TestMultipleBothExpiredOwners()
        {
            var ownerId1 = Guid.NewGuid();
            var ownerId2 = Guid.NewGuid();
            var expiryDate = DateTime.Today.Date.AddDays(5);
            const int quantity1 = 10;
            const int quantity2 = 15;
            CreateAllocation<ContactCredit>(ownerId1, expiryDate, quantity1);
            CreateAllocation<ContactCredit>(ownerId2, expiryDate, quantity2);

            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(2, allocations.Count);
            AssertAllocation(expiryDate, quantity1, allocations[ownerId1]);
            AssertAllocation(expiryDate, quantity2, allocations[ownerId2]);
        }

        [TestMethod]
        public void TestMultipleOneExpiredOwners()
        {
            var ownerId1 = Guid.NewGuid();
            var ownerId2 = Guid.NewGuid();
            var expiryDate1 = DateTime.Today.Date.AddDays(5);
            var expiryDate2 = DateTime.Today.Date.AddDays(15);
            const int quantity1 = 10;
            const int quantity2 = 15;
            CreateAllocation<ContactCredit>(ownerId1, expiryDate1, quantity1);
            CreateAllocation<ContactCredit>(ownerId2, expiryDate2, quantity2);

            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(1, allocations.Count);
            AssertAllocation(expiryDate1, quantity1, allocations[ownerId1]);
        }

        [TestMethod]
        public void TestMultipleNeitherExpiredOwners()
        {
            var ownerId1 = Guid.NewGuid();
            var ownerId2 = Guid.NewGuid();
            var expiryDate1 = DateTime.Today.Date.AddDays(15);
            var expiryDate2 = DateTime.Today.Date.AddDays(20);
            const int quantity1 = 10;
            const int quantity2 = 15;
            CreateAllocation<ContactCredit>(ownerId1, expiryDate1, quantity1);
            CreateAllocation<ContactCredit>(ownerId2, expiryDate2, quantity2);

            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(0, allocations.Count);
        }

        private void TestExpiringAllocation(int? quantity)
        {
            var ownerId = Guid.NewGuid();
            var expiryDate = DateTime.Today.Date.AddDays(5);
            CreateAllocation<ContactCredit>(ownerId, expiryDate, quantity);
            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(1, allocations.Count);
            AssertAllocation(expiryDate, quantity, allocations[ownerId]);
        }

        private void TestNotExpiringAllocation(int? quantity)
        {
            var ownerId = Guid.NewGuid();
            var expiryDate = DateTime.Today.Date.AddDays(15);
            CreateAllocation<ContactCredit>(ownerId, expiryDate, quantity);
            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(0, allocations.Count);
        }

        private void TestExpiredAllocation(int? quantity)
        {
            var ownerId = Guid.NewGuid();
            var expiryDate = DateTime.Today.Date.AddDays(-15);
            CreateAllocation<ContactCredit>(ownerId, expiryDate, quantity);
            var allocations = _employerCreditsQuery.GetEffectiveExpiringAllocations<ContactCredit>(DateTime.Today.Date.AddDays(10));
            Assert.AreEqual(0, allocations.Count);
        }

        private void CreateAllocation<TCredit>(Guid ownerId, DateTime? expiryDate, int? quantity)
            where TCredit : Credit
        {
            _allocationsCommand.CreateAllocation(new Allocation
                                                     {
                                                         CreditId = _creditsQuery.GetCredit<TCredit>().Id,
                                                         OwnerId = ownerId,
                                                         ExpiryDate = expiryDate,
                                                         InitialQuantity = quantity
                                                     });
        }

        private static void AssertAllocation(DateTime? expiryDate, int? quantity, Allocation allocation)
        {
            Assert.AreEqual(expiryDate, allocation.ExpiryDate);
            Assert.AreEqual(quantity, allocation.InitialQuantity);
        }
    }
}
using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Credits
{
    [TestClass]
    public class AllocateTests
        : CreditsTests
    {
        [TestMethod]
        public void TestNoAdjustments()
        {
            var owner = new Employer { Id = Guid.NewGuid() };

            // Check.

            Assert.AreEqual(0, _allocationsQuery.GetAllocationsByOwnerId(owner.Id).Count);
            Assert.AreEqual(0, _allocationsQuery.GetActiveAllocations(owner.Id).Count);
            AssertNoAllocations(owner.Id);
            AssertNoActiveAllocations<ContactCredit>(owner);
            AssertNoActiveAllocations<JobAdCredit>(owner);
            AssertNoActiveAllocations<ApplicantCredit>(owner);
        }

        [TestMethod]
        public void TestUnlimitedNeverExpireAllocate()
        {
            TestAllocate(null, null);
        }

        [TestMethod]
        public void TestLimitedNeverExpireAllocate()
        {
            TestAllocate(null, 20);
        }

        [TestMethod]
        public void TestUnlimitedExpireAllocate()
        {
            TestAllocate(DateTime.Now.AddDays(2), null);
        }

        [TestMethod]
        public void TestLimitedExpireAllocate()
        {
            TestAllocate(DateTime.Now.AddDays(2), 33);
        }

        [TestMethod]
        public void TestUnlimitedUnlimitedMultipleAllocate()
        {
            TestMultipleAllocate(null, null, null, null);
        }

        [TestMethod]
        public void TestUnlimitedLimitedMultipleAllocate()
        {
            TestMultipleAllocate(null, null, null, 20);
        }

        [TestMethod]
        public void TestLimitedUnlimitedMultipleAllocate()
        {
            TestMultipleAllocate(null, 20, null, null);
        }

        [TestMethod]
        public void TestLimitedLimitedMultipleAllocate()
        {
            TestMultipleAllocate(null, 10, null, 20);
        }

        [TestMethod]
        public void TestExpiringExpiringMultipleAllocate()
        {
            TestMultipleAllocate(DateTime.Now.AddDays(10), 10, DateTime.Now.AddDays(20), 20);
        }

        [TestMethod]
        public void TestNeverExpiringMultipleAllocate()
        {
            TestMultipleAllocate(null, 10, DateTime.Now.AddDays(20), 20);
        }

        [TestMethod]
        public void TestExpiredExpiringMultipleAllocate()
        {
            TestMultipleAllocate(DateTime.Now.AddDays(-10), 10, DateTime.Now.AddDays(20), 20);
        }

        [TestMethod]
        public void TestExpiredExpiredMultipleAllocate()
        {
            TestMultipleAllocate(DateTime.Now.AddDays(-10), 10, DateTime.Now.AddDays(-5), 20);
        }

        [TestMethod]
        public void TestUnlimitedDeallocate()
        {
            TestDeallocate(null);
        }

        [TestMethod]
        public void TestLimitedDeallocate()
        {
            TestDeallocate(20);
        }

        [TestMethod]
        public void TestReference()
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var owner = new Employer { Id = Guid.NewGuid() };

            // Allocate.

            var referenceId1 = Guid.NewGuid();
            DateTime? expiryDate1 = null;
            const int quantity1 = 20;
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = expiryDate1, InitialQuantity = quantity1, ReferenceId = referenceId1 });

            // Check.

            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1, referenceId1);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, quantity1, referenceId1);
            AssertNoActiveAllocations<JobAdCredit>(owner);
            AssertNoActiveAllocations<ApplicantCredit>(owner);

            // Allocate again.

            var referenceId2 = Guid.NewGuid();
            DateTime? expiryDate2 = DateTime.Now.AddDays(50);
            int? quantity2 = null;
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = expiryDate2, InitialQuantity = quantity2, ReferenceId = referenceId2 });

            // Check.

            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1, referenceId1, expiryDate2, quantity2, quantity2, referenceId2);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, quantity1, referenceId1, expiryDate2, quantity2, quantity2, referenceId2);
            AssertNoActiveAllocations<JobAdCredit>(owner);
            AssertNoActiveAllocations<ApplicantCredit>(owner);
        }

        private void TestDeallocate(int? quantity)
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var owner = new Employer { Id = Guid.NewGuid() };

            // Allocate first.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = credit.Id, InitialQuantity = quantity });

            AssertAllocations<ContactCredit>(owner.Id, null, quantity, quantity, false, null);
            AssertActiveAllocations<ContactCredit>(owner, null, quantity, quantity, false, null, null);
            AssertNoActiveAllocations<JobAdCredit>(owner);
            AssertNoActiveAllocations<ApplicantCredit>(owner);

            // Deallocate.

            var allocation = _allocationsQuery.GetAllocationsByOwnerId(owner.Id)[0];
            _allocationsCommand.Deallocate(allocation.Id);

            AssertAllocations<ContactCredit>(owner.Id, null, quantity, allocation.RemainingQuantity, true, null);
            AssertNoActiveAllocations<ContactCredit>(owner);
            AssertNoActiveAllocations<JobAdCredit>(owner);
            AssertNoActiveAllocations<ApplicantCredit>(owner);
        }

        private void TestAllocate(DateTime? expiryDate, int? quantity)
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var owner = new Employer { Id = Guid.NewGuid() };

            // Add.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = expiryDate, InitialQuantity = quantity });

            // Check.

            AssertAllocations<ContactCredit>(owner.Id, expiryDate, quantity, quantity, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate, quantity, quantity, null);
            AssertNoActiveAllocations<JobAdCredit>(owner);
            AssertNoActiveAllocations<ApplicantCredit>(owner);
        }

        private void TestMultipleAllocate(DateTime? firstExpiryDate, int? firstQuantity, DateTime? secondExpiryDate, int? secondQuantity)
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var owner = new Employer { Id = Guid.NewGuid() };

            // Allocate first.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = firstExpiryDate, InitialQuantity = firstQuantity });

            AssertAllocations<ContactCredit>(owner.Id, firstExpiryDate, firstQuantity, firstQuantity, null);
            if (firstExpiryDate < DateTime.Now)
                AssertNoActiveAllocations<ContactCredit>(owner);
            else
                AssertActiveAllocations<ContactCredit>(owner, firstExpiryDate, firstQuantity, firstQuantity, null);
            AssertNoActiveAllocations<JobAdCredit>(owner);
            AssertNoActiveAllocations<ApplicantCredit>(owner);
            var firstAllocation = _allocationsQuery.GetAllocationsByOwnerId(owner.Id)[0];

            // Allocate second.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = secondExpiryDate, InitialQuantity = secondQuantity });

            AssertAllocations<ContactCredit>(owner.Id, firstExpiryDate, firstQuantity, firstQuantity, null, secondExpiryDate, secondQuantity, secondQuantity, null);

            if (firstExpiryDate < DateTime.Now)
            {
                if (secondExpiryDate < DateTime.Now)
                    AssertNoActiveAllocations<ContactCredit>(owner);
                else
                    AssertActiveAllocations<ContactCredit>(owner, secondExpiryDate, secondQuantity, secondQuantity, null);
            }
            else
            {
                if (secondExpiryDate < DateTime.Now)
                    AssertActiveAllocations<ContactCredit>(owner, firstExpiryDate, firstQuantity, firstQuantity, null);
                else
                    AssertActiveAllocations<ContactCredit>(owner, firstExpiryDate, firstQuantity, firstQuantity, null, secondExpiryDate, secondQuantity, secondQuantity, null);
            }

            AssertNoActiveAllocations<JobAdCredit>(owner);
            AssertNoActiveAllocations<ApplicantCredit>(owner);

            // Deallocate the first.

            _allocationsCommand.Deallocate(firstAllocation.Id);
            AssertAllocations<ContactCredit>(owner.Id, firstExpiryDate, firstQuantity, firstQuantity, true, null, secondExpiryDate, secondQuantity, secondQuantity, false, null);

            if (firstExpiryDate < DateTime.Now)
            {
                if (secondExpiryDate < DateTime.Now)
                    AssertNoActiveAllocations<ContactCredit>(owner);
                else
                    AssertActiveAllocations<ContactCredit>(owner, secondExpiryDate, secondQuantity, secondQuantity, null);
            }
            else
            {
                if (secondExpiryDate < DateTime.Now)
                    AssertActiveAllocations<ContactCredit>(owner, firstExpiryDate, firstQuantity, 0, true, firstQuantity, null);
                else
                    AssertActiveAllocations<ContactCredit>(owner, secondExpiryDate, secondQuantity, secondQuantity, false, null, null);
            }

            AssertNoActiveAllocations<JobAdCredit>(owner);
            AssertNoActiveAllocations<ApplicantCredit>(owner);
        }
    }
}
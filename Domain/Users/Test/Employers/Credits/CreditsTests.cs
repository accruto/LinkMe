using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Credits
{
    [TestClass]
    public abstract class CreditsTests
        : TestClass
    {
        protected readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        protected readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        protected readonly IExercisedCreditsCommand _exercisedCreditsCommand = Resolve<IExercisedCreditsCommand>();
        protected readonly IExercisedCreditsQuery _exercisedCreditsQuery = Resolve<IExercisedCreditsQuery>();
        protected readonly IEmployerCreditsQuery _employerCreditsQuery = Resolve<IEmployerCreditsQuery>();

        [TestInitialize]
        public void CreditsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected void AssertAllocations<T>(Guid ownerId, DateTime? expiryDate, int? initialQuantity, int? remainingQuantity, Guid? referenceId)
            where T : Credit
        {
            AssertAllocations<T>(ownerId, expiryDate, initialQuantity, remainingQuantity, false, referenceId);
        }

        protected void AssertAllocations<T>(Guid ownerId, DateTime? expiryDate, int? initialQuantity, int? remainingQuantity, bool deallocated, Guid? referenceId)
            where T : Credit
        {
            if (expiryDate != null)
                expiryDate = expiryDate.Value.Date;

            // GetAllocations.

            var allocations = _allocationsQuery.GetAllocationsByOwnerId(ownerId);
            Assert.AreEqual(1, allocations.Count);
            AssertAllocation<T>(expiryDate, initialQuantity, remainingQuantity, deallocated, referenceId, allocations[0]);
        }

        protected void AssertActiveAllocations<T>(IEmployer owner, DateTime? expiryDate, int? initialQuantity, int? remainingQuantity, Guid? referenceId)
            where T : Credit
        {
            AssertActiveAllocations<T>(owner, expiryDate, initialQuantity, remainingQuantity, false, null, referenceId);
        }

        protected void AssertActiveAllocations<T>(IEmployer owner, DateTime? expiryDate, int? initialQuantity, int? remainingQuantity, bool deallocated, int? deallocationQuantity, Guid? referenceId)
            where T : Credit
        {
            if (expiryDate != null)
                expiryDate = expiryDate.Value.Date;

            // GetActiveAllocations.

            var allocations = _allocationsQuery.GetActiveAllocations(owner.Id);
            Assert.AreEqual(1, allocations.Count);
            AssertAllocation<T>(expiryDate, initialQuantity, remainingQuantity, deallocated, referenceId, allocations[0]);

            // GetActiveAllocations<T>

            allocations = _allocationsQuery.GetActiveAllocations<T>(owner.Id);
            Assert.AreEqual(1, allocations.Count);
            AssertAllocation<T>(expiryDate, initialQuantity, remainingQuantity, deallocated, referenceId, allocations[0]);

            // GetEffectiveActiveAllocation

            var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<T>(owner);
            Assert.IsNotNull(allocation);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(remainingQuantity, allocation.RemainingQuantity);
            Assert.IsNull(allocation.DeallocatedTime);

            var creditId = _creditsQuery.GetCredit<T>().Id;
            var groupedAllocations = _employerCreditsQuery.GetEffectiveActiveAllocations(owner, new[] { creditId });
            Assert.AreEqual(1, groupedAllocations.Count);

            allocation = groupedAllocations[creditId];
            Assert.IsNotNull(allocation);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(remainingQuantity, allocation.RemainingQuantity);
            Assert.IsNull(allocation.DeallocatedTime);
        }

        protected void AssertAllocations<T>(Guid ownerId, DateTime? expiryDate1, int? initialQuantity1, int? remainingQuantity1, Guid? referenceId1, DateTime? expiryDate2, int? initialQuantity2, int? remainingQuantity2, Guid? referenceId2)
            where T : Credit
        {
            AssertAllocations<T>(ownerId, expiryDate1, initialQuantity1, remainingQuantity1, false, referenceId1, expiryDate2, initialQuantity2, remainingQuantity2, false, referenceId2);
        }

        protected void AssertAllocations<T>(Guid ownerId, DateTime? expiryDate1, int? initialQuantity1, int? remainingQuantity1, bool deallocated1, Guid? referenceId1, DateTime? expiryDate2, int? initialQuantity2, int? remainingQuantity2, bool deallocated2, Guid? referenceId2)
            where T : Credit
        {
            if (expiryDate1 != null)
                expiryDate1 = expiryDate1.Value.Date;
            if (expiryDate2 != null)
                expiryDate2 = expiryDate2.Value.Date;

            // GetAllocations.

            var allocations = _allocationsQuery.GetAllocationsByOwnerId(ownerId);
            Assert.AreEqual(2, allocations.Count);

            // Assumed that expiryDate1 < expiryDate2.

            AssertAllocation<T>(expiryDate1, initialQuantity1, remainingQuantity1, deallocated1, referenceId1, allocations[0]);
            AssertAllocation<T>(expiryDate2, initialQuantity2, remainingQuantity2, deallocated2, referenceId2, allocations[1]);
        }

        protected void AssertActiveAllocations<T>(IEmployer owner, DateTime? expiryDate1, int? initialQuantity1, int? remainingQuantity1, Guid? referenceId1, DateTime? expiryDate2, int? initialQuantity2, int? remainingQuantity2, Guid? referenceId2)
            where T : Credit
        {
            AssertActiveAllocations<T>(owner, expiryDate1, initialQuantity1, remainingQuantity1, false, null, referenceId1, expiryDate2, initialQuantity2, remainingQuantity2, false, null, referenceId2);
        }

        protected void AssertActiveAllocations<T>(IEmployer owner, DateTime? expiryDate1, int? initialQuantity1, int? remainingQuantity1, bool deallocated1, int? deallocationQuantity1, Guid? referenceId1, DateTime? expiryDate2, int? initialQuantity2, int? remainingQuantity2, bool deallocated2, int? deallocationQuantity2, Guid? referenceId2)
            where T : Credit
        {
            if (expiryDate1 != null)
                expiryDate1 = expiryDate1.Value.Date;
            if (expiryDate2 != null)
                expiryDate2 = expiryDate2.Value.Date;

            // Assumed that expiryDate1 < expiryDate2.

            var allocations = _allocationsQuery.GetActiveAllocations(owner.Id);
            Assert.AreEqual(2, allocations.Count);
            AssertAllocation<T>(expiryDate1, initialQuantity1, remainingQuantity1, deallocated1, referenceId1, allocations[0]);
            AssertAllocation<T>(expiryDate2, initialQuantity2, remainingQuantity2, deallocated2, referenceId2, allocations[1]);

            // GetActiveAllocations<T>

            allocations = _allocationsQuery.GetActiveAllocations<T>(owner.Id);
            Assert.AreEqual(2, allocations.Count);
            AssertAllocation<T>(expiryDate1, initialQuantity1, remainingQuantity1, deallocated1, referenceId1, allocations[0]);
            AssertAllocation<T>(expiryDate2, initialQuantity2, remainingQuantity2, deallocated2, referenceId2, allocations[1]);

            // GetEffectiveActiveAllocation

            var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<T>(owner);
            Assert.IsNotNull(allocation);
            Assert.AreEqual(initialQuantity1 == null || initialQuantity2 == null, allocation.InitialQuantity == null);
            Assert.AreEqual(remainingQuantity1 == null || remainingQuantity2 == null, allocation.RemainingQuantity == null);
            Assert.IsNull(allocation.DeallocatedTime);

            var creditId = _creditsQuery.GetCredit<T>().Id;
            var groupedAllocations = _employerCreditsQuery.GetEffectiveActiveAllocations(owner, new[] { creditId });
            Assert.AreEqual(1, groupedAllocations.Count);

            allocation = groupedAllocations[creditId];
            Assert.IsNotNull(allocation);
            Assert.AreEqual(initialQuantity1 == null || initialQuantity2 == null, allocation.InitialQuantity == null);
            Assert.AreEqual(remainingQuantity1 == null || remainingQuantity2 == null, allocation.RemainingQuantity == null);
            Assert.IsNull(allocation.DeallocatedTime);
        }

        protected void AssertNoAllocations(Guid ownerId)
        {
            Assert.AreEqual(0, _allocationsQuery.GetAllocationsByOwnerId(ownerId).Count);
        }

        protected void AssertNoActiveAllocations<T>(IEmployer owner)
            where T : Credit
        {
            Assert.AreEqual(0, _allocationsQuery.GetActiveAllocations<T>(owner.Id).Count);

            var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<T>(owner);
            Assert.AreEqual(0, allocation.InitialQuantity);
            Assert.AreEqual(0, allocation.RemainingQuantity);

            var creditId = _creditsQuery.GetCredit<T>().Id;
            var groupedAllocations = _employerCreditsQuery.GetEffectiveActiveAllocations(owner, new[] { creditId });
            Assert.AreEqual(1, groupedAllocations.Count);

            allocation = groupedAllocations[creditId];
            Assert.AreEqual(0, allocation.InitialQuantity);
            Assert.AreEqual(0, allocation.RemainingQuantity);
        }

        private void AssertAllocation<T>(DateTime? expiryDate, int? initialQuantity, int? remainingQuantity, bool deallocated, Guid? referenceId, Allocation allocation)
            where T : Credit
        {
            var credit = _creditsQuery.GetCredit<T>();
            Assert.AreEqual(credit.Id, allocation.CreditId);
            Assert.AreEqual(expiryDate, allocation.ExpiryDate);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(remainingQuantity, allocation.RemainingQuantity);
            Assert.AreEqual(deallocated, allocation.DeallocatedTime != null);
            Assert.AreEqual(referenceId, allocation.ReferenceId);
        }
    }
}
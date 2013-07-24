using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Credits
{
    [TestClass]
    public class ExerciseTests
        : CreditsTests
    {
        [TestMethod]
        public void TestUnlimitedExercise()
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var owner = new Employer { Id = Guid.NewGuid() };
            var exercisedOnId = Guid.NewGuid();
            var allocation = new Allocation {OwnerId = owner.Id, CreditId = credit.Id};
            _allocationsCommand.CreateAllocation(allocation);

            AssertAllocations<ContactCredit>(owner.Id, null, null, null, null);
            AssertActiveAllocations<ContactCredit>(owner, null, null, null, null);

            // No matter how many times a credit is exercised nothing should change.

            for (var index = 0; index < 20; ++index)
            {
                var exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), false, owner.Id, exercisedOnId, null);
                AssertExercisedCredit(exercisedCreditId, allocation.Id, false);
                AssertAllocations<ContactCredit>(owner.Id, null, null, null, null);
                AssertActiveAllocations<ContactCredit>(owner, null, null, null, null);
            }
        }

        [TestMethod]
        public void TestLimitedExercise()
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var owner = new Employer { Id = Guid.NewGuid() };
            var exercisedOnId = Guid.NewGuid();
            var allocation = new Allocation { OwnerId = owner.Id, CreditId = credit.Id, InitialQuantity = 20 };
            _allocationsCommand.CreateAllocation(allocation);

            AssertAllocations<ContactCredit>(owner.Id, null, 20, 20, null);
            AssertActiveAllocations<ContactCredit>(owner, null, 20, 20, null);

            // Exercise the first credit.

            var exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, exercisedOnId, null);
            AssertExercisedCredit(exercisedCreditId, allocation.Id, true);
            AssertAllocations<ContactCredit>(owner.Id, null, 20, 19, null);
            AssertActiveAllocations<ContactCredit>(owner, null, 20, 19, null);

            // Exercise some more without.

            for (var index = 1; index < 19; ++index)
            {
                exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, exercisedOnId, null);
                AssertExercisedCredit(exercisedCreditId, allocation.Id, true);
                AssertAllocations<ContactCredit>(owner.Id, null, 20, 20 - index - 1, null);
                AssertActiveAllocations<ContactCredit>(owner, null, 20, 20 - index - 1, null);
            }

            // Exercise the last credit.

            exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, exercisedOnId, null);
            AssertExercisedCredit(exercisedCreditId, allocation.Id, true);
            AssertAllocations<ContactCredit>(owner.Id, null, 20, 0, null);
            AssertActiveAllocations<ContactCredit>(owner, null, 20, 0, null);
        }

        [TestMethod]
        public void TestLimitedUnlimitedExercise()
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var owner = new Employer { Id = Guid.NewGuid() };
            var referenceId = Guid.NewGuid();

            // Create limited.

            var expiryDate1 = DateTime.Now.AddDays(1);
            int? quantity1 = 20;
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = expiryDate1, InitialQuantity = quantity1 });

            // Create unlimited.

            var expiryDate2 = DateTime.Now.AddDays(2);
            int? quantity2 = null;
            var allocation = new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = expiryDate2, InitialQuantity = quantity2 };
            _allocationsCommand.CreateAllocation(allocation);

            // No matter how many times a credit is exercised nothing should change because there is one unlimited allocation.

            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1, null, expiryDate2, quantity2, quantity2, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, quantity1, null, expiryDate2, quantity2, quantity2, null);
            for (var index = 0; index < 20; ++index)
            {
                var exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, referenceId, referenceId);
                Assert.AreEqual(allocation.Id, _exercisedCreditsQuery.GetExercisedCredit(exercisedCreditId.Value).AllocationId);
                AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1, null, expiryDate2, quantity2, quantity2, null);
                AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, quantity1, null, expiryDate2, quantity2, quantity2, null);
            }
        }

        [TestMethod]
        public void TestLimitedLimitedExercise()
        {
            Guid? exercisedCreditId;
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var owner = new Employer { Id = Guid.NewGuid() };
            var referenceId = Guid.NewGuid();

            // Create limited.

            var expiryDate1 = DateTime.Now.AddDays(1);
            int? quantity1 = 20;
            var allocation1 = new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = expiryDate1, InitialQuantity = quantity1 };
            _allocationsCommand.CreateAllocation(allocation1);

            // Create unlimited.

            var expiryDate2 = DateTime.Now.AddDays(2);
            int? quantity2 = 10;
            var allocation2 = new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = expiryDate2, InitialQuantity = quantity2 };
            _allocationsCommand.CreateAllocation(allocation2);

            // Should use up the earlier date first.

            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1, null, expiryDate2, quantity2, quantity2, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, quantity1, null, expiryDate2, quantity2, quantity2, null);
            for (var index = 0; index < quantity1 - 1; ++index)
            {
                exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, referenceId, referenceId);
                Assert.AreEqual(allocation1.Id, _exercisedCreditsQuery.GetExercisedCredit(exercisedCreditId.Value).AllocationId);
                AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1 - index - 1, null, expiryDate2, quantity2, quantity2, null);
                AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, quantity1 - index - 1, null, expiryDate2, quantity2, quantity2, null);
            }

            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, 1, null, expiryDate2, quantity2, quantity2, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, 1, null, expiryDate2, quantity2, quantity2, null);

            // Exercise the last credit.

            exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, referenceId, referenceId);
            Assert.AreEqual(allocation1.Id, _exercisedCreditsQuery.GetExercisedCredit(exercisedCreditId.Value).AllocationId);
            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, 0, null, expiryDate2, quantity2, quantity2, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, 0, null, expiryDate2, quantity2, quantity2, null);

            // Use up the later date.

            for (var index = 0; index < quantity2 - 1; ++index)
            {
                exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, referenceId, referenceId);
                Assert.AreEqual(allocation2.Id, _exercisedCreditsQuery.GetExercisedCredit(exercisedCreditId.Value).AllocationId);
                AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, 0, null, expiryDate2, quantity2, quantity2 - index - 1, null);
                AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, 0, null, expiryDate2, quantity2, quantity2 - index - 1, null);
            }

            // Exercise the last credit.

            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, 0, null, expiryDate2, quantity2, 1, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, 0, null, expiryDate2, quantity2, 1, null);

            exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, referenceId, referenceId);
            Assert.AreEqual(allocation2.Id, _exercisedCreditsQuery.GetExercisedCredit(exercisedCreditId.Value).AllocationId);
            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, 0, null, expiryDate2, quantity2, 0, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, 0, null, expiryDate2, quantity2, 0, null);
        }

        [TestMethod]
        public void TestExpiredLimitedExercise()
        {
            Guid? exercisedCreditId;
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var owner = new Employer { Id = Guid.NewGuid() };
            var referenceId = Guid.NewGuid();

            // Create limited.

            var expiryDate1 = DateTime.Now.AddDays(1);
            int? quantity1 = 20;
            var allocation1 = new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = expiryDate1, InitialQuantity = quantity1 };
            _allocationsCommand.CreateAllocation(allocation1);

            // Create unlimited.

            var expiryDate2 = DateTime.Now.AddDays(2);
            int? quantity2 = 10;
            var allocation2 = new Allocation { OwnerId = owner.Id, CreditId = credit.Id, ExpiryDate = expiryDate2, InitialQuantity = quantity2 };
            _allocationsCommand.CreateAllocation(allocation2);

            // Use up some of the earlier date first.

            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1, null, expiryDate2, quantity2, quantity2, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, quantity1, null, expiryDate2, quantity2, quantity2, null);
            for (var index = 0; index < quantity1 / 2 - 1; ++index)
            {
                exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, referenceId, referenceId);
                Assert.AreEqual(allocation1.Id, _exercisedCreditsQuery.GetExercisedCredit(exercisedCreditId.Value).AllocationId);
                AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1 - index - 1, null, expiryDate2, quantity2, quantity2, null);
                AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, quantity1 - index - 1, null, expiryDate2, quantity2, quantity2, null);
            }

            // Deallocate the allocation.

            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1 / 2 + 1, null, expiryDate2, quantity2, quantity2, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate1, quantity1, quantity1 / 2 + 1, null, expiryDate2, quantity2, quantity2, null);

            _allocationsCommand.Deallocate(_allocationsQuery.GetAllocationsByOwnerId(owner.Id)[0].Id);
            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1 / 2 + 1, true, null, expiryDate2, quantity2, quantity2, false, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate2, quantity2, quantity2, null);

            // Use up the later date.

            for (var index = 0; index < quantity2 - 1; ++index)
            {
                exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, referenceId, referenceId);
                Assert.AreEqual(allocation2.Id, _exercisedCreditsQuery.GetExercisedCredit(exercisedCreditId.Value).AllocationId);
                AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1 / 2 + 1, true, null, expiryDate2, quantity2, quantity2 - index - 1, false, null);
                AssertActiveAllocations<ContactCredit>(owner, expiryDate2, quantity2, quantity2 - index - 1, null);
            }

            // Exercise the last credit.

            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1 / 2 + 1, true, null, expiryDate2, quantity2, 1, false, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate2, quantity2, 1, null);

            exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner.Id), true, owner.Id, referenceId, referenceId);
            Assert.AreEqual(allocation2.Id, _exercisedCreditsQuery.GetExercisedCredit(exercisedCreditId.Value).AllocationId);
            AssertAllocations<ContactCredit>(owner.Id, expiryDate1, quantity1, quantity1 / 2 + 1, true, null, expiryDate2, quantity2, 0, false, null);
            AssertActiveAllocations<ContactCredit>(owner, expiryDate2, quantity2, 0, null);
        }

        private void AssertExercisedCredit(Guid? exercisedCreditId, Guid allocationId, bool adjustedAllocation)
        {
            var exercisedCredit = _exercisedCreditsQuery.GetExercisedCredit(exercisedCreditId.Value);
            Assert.AreEqual(allocationId, exercisedCredit.AllocationId);
            Assert.AreEqual(adjustedAllocation, exercisedCredit.AdjustedAllocation);
        }
    }
}
using System;
using LinkMe.Domain.Credits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Credits
{
    [TestClass]
    public class HierarchyTests
        : CreditsTests
    {
        [TestMethod]
        public void TestAdjustUnlimitedOneLevel()
        {
            TestOneLevel(null, true);
        }

        [TestMethod]
        public void TestNoAdjustUnlimitedOneLevel()
        {
            TestOneLevel(null, false);
        }

        [TestMethod]
        public void TestAdjustLimitedOneLevel()
        {
            TestOneLevel(20, true);
        }

        [TestMethod]
        public void TestNoAdjustLimitedOneLevel()
        {
            TestOneLevel(20, false);
        }

        [TestMethod]
        public void TestAdjustLimitedLimitedTwoLevels()
        {
            TestTwoLevels(true, 20, true, 10, true);
        }

        [TestMethod]
        public void TestNoAdjustLimitedLimitedTwoLevels()
        {
            TestTwoLevels(true, 20, true, 10, false);
        }

        [TestMethod]
        public void TestAdjustUnlimitedLimitedTwoLevels()
        {
            TestTwoLevels(true, null, true, 10, true);
        }

        [TestMethod]
        public void TestNoAdjustUnlimitedLimitedTwoLevels()
        {
            TestTwoLevels(true, null, true, 10, false);
        }

        [TestMethod]
        public void TestAdjustLimitedUnlimitedTwoLevels()
        {
            TestTwoLevels(true, 20, true, null, true);
        }

        [TestMethod]
        public void TestNoAdjustLimitedUnlimitedTwoLevels()
        {
            TestTwoLevels(true, 20, true, null, false);
        }

        [TestMethod]
        public void TestAdjustUnlimitedUnlimitedTwoLevels()
        {
            TestTwoLevels(true, null, true, null, true);
        }

        [TestMethod]
        public void TestNoAdjustUnlimitedUnlimitedTwoLevels()
        {
            TestTwoLevels(true, null, true, null, false);
        }

        [TestMethod]
        public void TestAdjustNoneLimitedTwoLevels()
        {
            TestTwoLevels(false, null, true, 10, true);
        }

        [TestMethod]
        public void TestNoAdjustNoneLimitedTwoLevels()
        {
            TestTwoLevels(false, null, true, 10, false);
        }

        [TestMethod]
        public void TestAdjustLimitedNoneTwoLevels()
        {
            TestTwoLevels(true, 10, false, null, true);
        }

        [TestMethod]
        public void TestNoAdjustLimitedNoneTwoLevels()
        {
            TestTwoLevels(true, 10, false, null, false);
        }

        [TestMethod]
        public void TestAdjustNoneUnlimitedTwoLevels()
        {
            TestTwoLevels(false, null, true, null, true);
        }

        [TestMethod]
        public void TestNoAdjustNoneUnlimitedTwoLevels()
        {
            TestTwoLevels(false, null, true, null, false);
        }

        [TestMethod]
        public void TestAdjustUnlimitedNoneTwoLevels()
        {
            TestTwoLevels(true, null, false, null, true);
        }

        [TestMethod]
        public void TestNoAdjustUnlimitedNoneTwoLevels()
        {
            TestTwoLevels(true, null, false, null, false);
        }

        [TestMethod]
        public void TestUnlimitedUnlimitedUnlimitedThreeLevels()
        {
            TestThreeLevels(true, null, true, null, true, null);
        }

        [TestMethod]
        public void TestLimitedUnlimitedUnlimitedThreeLevels()
        {
            TestThreeLevels(true, 20, true, null, true, null);
        }

        [TestMethod]
        public void TestUnlimitedLimitedUnlimitedThreeLevels()
        {
            TestThreeLevels(true, null, true, 20, true, null);
        }

        [TestMethod]
        public void TestUnlimitedUnlimitedLimitedThreeLevels()
        {
            TestThreeLevels(true, null, true, null, true, 20);
        }

        [TestMethod]
        public void TestNoneUnlimitedUnlimitedThreeLevels()
        {
            TestThreeLevels(false, null, true, null, true, null);
        }

        [TestMethod]
        public void TestUnlimitedNoneUnlimitedThreeLevels()
        {
            TestThreeLevels(true, null, false, null, true, null);
        }

        [TestMethod]
        public void TestUnlimitedUnlimitedNoneThreeLevels()
        {
            TestThreeLevels(true, null, true, null, false, null);
        }

        [TestMethod]
        public void TestLimitedLimitedLimitedThreeLevels()
        {
            TestThreeLevels(true, 30, true, 20, true, 10);
        }

        [TestMethod]
        public void TestNoneLimitedLimitedThreeLevels()
        {
            TestThreeLevels(false, null, true, 20, true, 10);
        }

        [TestMethod]
        public void TestLimitedNoneLimitedThreeLevels()
        {
            TestThreeLevels(true, 20, false, null, true, 10);
        }

        [TestMethod]
        public void TestLimitedLimitedNoneThreeLevels()
        {
            TestThreeLevels(true, 20, true, 10, false, null);
        }

        private void TestOneLevel(int? quantity, bool adjustAllocation)
        {
            var exercisedOnId = Guid.NewGuid();

            // Allocate.

            var owner1Id = Guid.NewGuid();
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner1Id, CreditId = credit.Id, InitialQuantity = quantity });

            // Exercise while adjusting.

            var exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(owner1Id), adjustAllocation, Guid.NewGuid(), exercisedOnId, null);
            AssertExercisedCredit(owner1Id, true, quantity, adjustAllocation);
            AssertExercisedCredit(new HierarchyPath(owner1Id), true, exercisedCreditId, exercisedOnId);
        }

        private void TestTwoLevels(bool allocate1, int? quantity1, bool allocate2, int? quantity2, bool adjustAllocation)
        {
            var exercisedOnId = Guid.NewGuid();

            // Allocate.

            var owner1Id = Guid.NewGuid();
            var owner2Id = Guid.NewGuid();
            var credit = _creditsQuery.GetCredit<ContactCredit>();

            if (allocate1)
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner1Id, CreditId = credit.Id, InitialQuantity = quantity1 });
            if (allocate2)
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner2Id, CreditId = credit.Id, InitialQuantity = quantity2 });

            // Exercise while adjusting.

            var exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(new[] { owner1Id, owner2Id }), adjustAllocation, Guid.NewGuid(), exercisedOnId, null);
            AssertExercisedCredit(owner1Id, allocate1, quantity1, adjustAllocation && allocate1);
            AssertExercisedCredit(owner2Id, allocate2, quantity2, adjustAllocation && !allocate1);
            AssertExercisedCredit(new HierarchyPath(owner1Id, owner2Id), true, exercisedCreditId, exercisedOnId);
            AssertExercisedCredit(new HierarchyPath(owner1Id), allocate1, exercisedCreditId, exercisedOnId);
            AssertExercisedCredit(new HierarchyPath(owner2Id), !allocate1, exercisedCreditId, exercisedOnId);
        }

        private void TestThreeLevels(bool allocate1, int? quantity1, bool allocate2, int? quantity2, bool allocate3, int? quantity3)
        {
            var exercisedOnId = Guid.NewGuid();

            // Allocate.

            var owner1Id = Guid.NewGuid();
            var owner2Id = Guid.NewGuid();
            var owner3Id = Guid.NewGuid();
            var credit = _creditsQuery.GetCredit<ContactCredit>();

            if (allocate1)
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner1Id, CreditId = credit.Id, InitialQuantity = quantity1 });
            if (allocate2)
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner2Id, CreditId = credit.Id, InitialQuantity = quantity2 });
            if (allocate3)
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner3Id, CreditId = credit.Id, InitialQuantity = quantity3 });

            // Exercise while adjusting.

            var exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(new[] { owner1Id, owner2Id }), true, Guid.NewGuid(), exercisedOnId, null);
            AssertExercisedCredit(owner1Id, allocate1, quantity1, allocate1);
            AssertExercisedCredit(owner2Id, allocate2, quantity2, !allocate1 && allocate2);
            AssertExercisedCredit(owner3Id, allocate3, quantity3, !allocate1 && !allocate2);
            AssertExercisedCredit(new HierarchyPath(owner1Id, owner2Id, owner3Id), true, exercisedCreditId, exercisedOnId);
            AssertExercisedCredit(new HierarchyPath(owner1Id, owner2Id), allocate1 || allocate2, exercisedCreditId, exercisedOnId);
            AssertExercisedCredit(new HierarchyPath(owner1Id), allocate1, exercisedCreditId, exercisedOnId);
            AssertExercisedCredit(new HierarchyPath(owner2Id, owner3Id), !allocate1, exercisedCreditId, exercisedOnId);
            AssertExercisedCredit(new HierarchyPath(owner2Id), !allocate1 && allocate2, exercisedCreditId, exercisedOnId);
            AssertExercisedCredit(new HierarchyPath(owner3Id), !allocate1 && !allocate2, exercisedCreditId, exercisedOnId);
        }

        private void AssertExercisedCredit(Guid ownerId, bool allocated, int? quantity, bool creditUsed)
        {
            var allocations = _allocationsQuery.GetAllocationsByOwnerId(ownerId);
            Assert.AreEqual(allocated ? 1 : 0, allocations.Count);
            if (allocated)
            {
                if (quantity == null)
                {
                    Assert.AreEqual(null, allocations[0].InitialQuantity);
                    Assert.AreEqual(null, allocations[0].RemainingQuantity);
                }
                else
                {
                    Assert.AreEqual(quantity.Value, allocations[0].InitialQuantity);
                    Assert.AreEqual(creditUsed ? quantity.Value - 1 : quantity.Value, allocations[0].RemainingQuantity);
                }
            }
        }

        private void AssertExercisedCredit(HierarchyPath hierarchyPath, bool expectedHasExercised, Guid? exercisedCreditId, Guid exercisedOnId)
        {
            if (expectedHasExercised)
            {
                Assert.IsTrue(_exercisedCreditsQuery.HasExercisedCredit<ContactCredit>(hierarchyPath, exercisedOnId));
                var exercises = _exercisedCreditsQuery.GetExercisedCredits<ContactCredit>(hierarchyPath);
                Assert.AreEqual(1, exercises.Count);
                Assert.AreEqual(exercisedCreditId.Value, exercises[0].Id);
                Assert.AreEqual(exercisedOnId, exercises[0].ExercisedOnId);
            }
            else
            {
                Assert.IsFalse(_exercisedCreditsQuery.HasExercisedCredit<ContactCredit>(hierarchyPath, exercisedOnId));
                var exercises = _exercisedCreditsQuery.GetExercisedCredits<ContactCredit>(hierarchyPath);
                Assert.AreEqual(0, exercises.Count);
            }
        }
    }
}

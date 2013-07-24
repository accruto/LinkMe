using System;
using LinkMe.Domain.Credits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Credits
{
    [TestClass]
    public class HasExercisedTests
        : CreditsTests
    {
        private readonly ICreditsRepository _repository = Resolve<ICreditsRepository>();

        [TestMethod]
        public void TestOwnerExerciseUnlimitedCredit()
        {
            TestOwnerExerciseCreditWithAllocation(null);
        }

        [TestMethod]
        public void TestOwnerExerciseLimitedCredit()
        {
            TestOwnerExerciseCreditWithAllocation(20);
        }

        [TestMethod]
        public void TestEmployerExerciseUnlimitedCredit()
        {
            TestEmployerExerciseCreditWithAllocation(null);
        }

        [TestMethod]
        public void TestEmployerExerciseLimitedCredit()
        {
            TestEmployerExerciseCreditWithAllocation(20);
        }

        [TestMethod]
        public void TestOwnerExerciseUnlimitedCreditNoAllocation()
        {
            TestOwnerExerciseCreditWithNoAllocation(null);
        }

        [TestMethod]
        public void TestOwnerExerciseLimitedCreditNoAllocation()
        {
            TestOwnerExerciseCreditWithNoAllocation(20);
        }

        [TestMethod]
        public void TestEmployerExerciseUnlimitedCreditNoAllocation()
        {
            TestEmployerExerciseCreditWithoutAllocation(null);
        }

        [TestMethod]
        public void TestEmployerExerciseLimitedCreditNoAllocation()
        {
            TestEmployerExerciseCreditWithoutAllocation(20);
        }

        private void TestOwnerExerciseCreditWithAllocation(int? quantity)
        {
            // Set it up where owner is exerciser.

            var allocationId = Guid.NewGuid();
            var exercisedOnId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var exercisedById = ownerId;
            var hierarchyPath = new HierarchyPath(ownerId);

            ExerciseCredit(quantity, hierarchyPath, ownerId, allocationId, exercisedById, exercisedOnId);

            // Exerciser/owner has access.

            AssertHasExercisedCredit(hierarchyPath, allocationId, exercisedById, exercisedOnId);

            // No-one else should not have access.

            var someoneElseId = Guid.NewGuid();
            hierarchyPath = new HierarchyPath(someoneElseId);
            AssertHasNotExercisedCredit(hierarchyPath, exercisedOnId);
        }

        private void TestOwnerExerciseCreditWithNoAllocation(int? quantity)
        {
            // Set it up where owner is exerciser.

            var exercisedOnId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var exercisedById = ownerId;
            var hierarchyPath = new HierarchyPath(ownerId);

            ExerciseCredit(quantity, hierarchyPath, ownerId, null, exercisedById, exercisedOnId);

            // Exerciser/owner has access.

            AssertHasExercisedCredit(hierarchyPath, null, exercisedById, exercisedOnId);

            // No-one else should not have access.

            var someoneElseId = Guid.NewGuid();
            hierarchyPath = new HierarchyPath(someoneElseId);
            AssertHasNotExercisedCredit(hierarchyPath, exercisedOnId);
        }

        private void TestEmployerExerciseCreditWithAllocation(int? quantity)
        {
            // Set it up where owner is organisation an employer works for.

            var allocationId = Guid.NewGuid();
            var exercisedOnId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var exercisedById = Guid.NewGuid();
            var hierarchyPath = new HierarchyPath(ownerId, exercisedById);

            ExerciseCredit(quantity, hierarchyPath, ownerId, allocationId, exercisedById, exercisedOnId);

            // Exerciser has access.

            AssertHasExercisedCredit(hierarchyPath, allocationId, exercisedById, exercisedOnId);

            // Other employers in the organisation should have access.

            var otherEmployerId = Guid.NewGuid();
            hierarchyPath = new HierarchyPath(ownerId, otherEmployerId);
            AssertHasExercisedCredit(hierarchyPath, allocationId, exercisedById, exercisedOnId);

            // Employers in child branch of the hierarchy should have access.

            var organisationId = Guid.NewGuid();
            hierarchyPath = new HierarchyPath(ownerId, organisationId, otherEmployerId);
            AssertHasExercisedCredit(hierarchyPath, allocationId, exercisedById, exercisedOnId);

            // No-one outside the hierarchy should have access.

            hierarchyPath = new HierarchyPath(otherEmployerId);
            AssertHasNotExercisedCredit(hierarchyPath, exercisedOnId);

            // No-one in another organisation should have access.

            hierarchyPath = new HierarchyPath(organisationId, otherEmployerId);
            AssertHasNotExercisedCredit(hierarchyPath, exercisedOnId);
        }

        private void TestEmployerExerciseCreditWithoutAllocation(int? quantity)
        {
            // Set it up where owner is organisation an employer works for.

            var exercisedOnId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var exercisedById = Guid.NewGuid();
            var hierarchyPath = new HierarchyPath(ownerId, exercisedById);

            ExerciseCredit(quantity, hierarchyPath, ownerId, null, exercisedById, exercisedOnId);

            // Exerciser has access.

            hierarchyPath = new HierarchyPath(exercisedById);
            AssertHasExercisedCredit(hierarchyPath, null, exercisedById, exercisedOnId);

            // Owner does not have access.

            hierarchyPath = new HierarchyPath(ownerId);
            AssertHasNotExercisedCredit(hierarchyPath, exercisedOnId);

            // Other employers in the organisation should not have access.

            var otherEmployerId = Guid.NewGuid();
            hierarchyPath = new HierarchyPath(ownerId, otherEmployerId);
            AssertHasNotExercisedCredit(hierarchyPath, exercisedOnId);

            // Employers in child branch of the hierarchy should not have access.

            var organisationId = Guid.NewGuid();
            hierarchyPath = new HierarchyPath(ownerId, organisationId, otherEmployerId);
            AssertHasNotExercisedCredit(hierarchyPath, exercisedOnId);

            // No-one else outside the hierarchy should have access.

            hierarchyPath = new HierarchyPath(otherEmployerId);
            AssertHasNotExercisedCredit(hierarchyPath, exercisedOnId);

            // No-one else in another organisation outside the organisation should have access.

            hierarchyPath = new HierarchyPath(organisationId, otherEmployerId);
            AssertHasNotExercisedCredit(hierarchyPath, exercisedOnId);
        }

        private void ExerciseCredit(int? quantity, HierarchyPath hierarchyPath, Guid ownerId, Guid? allocationId, Guid exercisedById, Guid exercisedOnId)
        {
            // Allocate credits to owner.

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            if (allocationId != null)
            {
                // Exercise a credit using an allocation.

                var allocation = new Allocation {Id = allocationId.Value, OwnerId = ownerId, CreditId = credit.Id, InitialQuantity = quantity};
                _allocationsCommand.CreateAllocation(allocation);

                // Exercise a credit.

                var exercisedCreditId = _exercisedCreditsCommand.ExerciseCredit(credit.Id, hierarchyPath, true, exercisedById, exercisedOnId, null);
                Assert.AreEqual(allocation.Id, _exercisedCreditsQuery.GetExercisedCredit(exercisedCreditId.Value).AllocationId);
            }
            else
            {
                // To test the support for old candidate access before allocations were introduced directly update the database.
                // A record is kept that the owner has contacted the candidate but that no allocation is used.

                var exercise = new ExercisedCredit
                {
                    Id = Guid.NewGuid(),
                    Time = DateTime.Now,
                    CreditId = credit.Id,
                    ExercisedById = exercisedById,
                    ExercisedOnId = exercisedOnId,
                    AllocationId = null,
                    ReferenceId = null,
                };
                _repository.CreateExercisedCredit(exercise);
            }
        }

        private void AssertHasExercisedCredit(HierarchyPath hierarchyPath, Guid? allocationId, Guid exercisedById, Guid exercisedOnId)
        {
            Assert.IsTrue(_exercisedCreditsQuery.HasExercisedCredit<ContactCredit>(hierarchyPath, exercisedOnId));
            var exercises = _exercisedCreditsQuery.GetExercisedCredits<ContactCredit>(hierarchyPath);
            Assert.AreEqual(1, exercises.Count);
            Assert.AreEqual(exercisedById, exercises[0].ExercisedById);
            Assert.AreEqual(exercisedOnId, exercises[0].ExercisedOnId);
            Assert.AreEqual(allocationId, exercises[0].AllocationId);

            exercises = _exercisedCreditsQuery.GetExercisedCredits<ContactCredit>(hierarchyPath, new[] { exercisedOnId });
            Assert.AreEqual(1, exercises.Count);
            Assert.AreEqual(exercisedById, exercises[0].ExercisedById);
            Assert.AreEqual(exercisedOnId, exercises[0].ExercisedOnId);
            Assert.AreEqual(allocationId, exercises[0].AllocationId);
        }

        private void AssertHasNotExercisedCredit(HierarchyPath hierarchyPath, Guid exercisedOnId)
        {
            Assert.IsFalse(_exercisedCreditsQuery.HasExercisedCredit<ContactCredit>(hierarchyPath, exercisedOnId));
            var exercises = _exercisedCreditsQuery.GetExercisedCredits<ContactCredit>(hierarchyPath);
            Assert.AreEqual(0, exercises.Count);

            exercises = _exercisedCreditsQuery.GetExercisedCredits<ContactCredit>(hierarchyPath, new[] { exercisedOnId });
            Assert.AreEqual(0, exercises.Count);
        }
    }
}

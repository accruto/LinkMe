using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Applicants
{
    public abstract class ApplicantTests
        : ViewsTests
    {
        private const int MaxCreditsPerJobAd = 20;

        [TestMethod]
        public void TestNoCredits()
        {
            TestCredits(false, null);
        }

        [TestMethod]
        public void TestZeroCredits()
        {
            TestCredits(true, 0);
        }

        [TestMethod]
        public void TestSomeCredits()
        {
            TestCredits(true, 20);
        }

        [TestMethod]
        public void TestUnlimitedCredits()
        {
            TestCredits(true, null);
        }

        [TestMethod]
        public void TestSameApplicantSameJobAd()
        {
            const int quantity = 20;

            // Allocate some credits.

            var employer = CreateEmployer(false, null, true, quantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);
            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit.

            var member = _membersCommand.CreateTestMember(0);
            SubmitApplication(member, jobAd);

            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
            Assert.AreEqual(1, exercisedCredits.Count);
            Assert.IsTrue(exercisedCredits[0].AdjustedAllocation);

            var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(quantity, allocation.InitialQuantity);
            Assert.AreEqual(quantity - 1, allocation.RemainingQuantity);

            // Submit again.

            SubmitApplication(member, jobAd);

            exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
            Assert.AreEqual(1, exercisedCredits.Count);
            Assert.IsTrue(exercisedCredits[0].AdjustedAllocation);

            allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(quantity, allocation.InitialQuantity);
            Assert.AreEqual(quantity - 1, allocation.RemainingQuantity);
        }

        [TestMethod]
        public void TestSameApplicantDifferentJobAds()
        {
            const int quantity = 20;

            // Allocate some credits.

            var employer = CreateEmployer(false, null, true, quantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);
            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit.

            var member = _membersCommand.CreateTestMember(0);
            SubmitApplication(member, jobAd);

            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
            Assert.AreEqual(1, exercisedCredits.Count);
            Assert.IsTrue(exercisedCredits[0].AdjustedAllocation);
            var exercisedCreditId = exercisedCredits[0].Id;

            var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(quantity, allocation.InitialQuantity);
            Assert.AreEqual(quantity - 1, allocation.RemainingQuantity);

            // Submit again.

            jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            SubmitApplication(member, jobAd);

            exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
            Assert.AreEqual(2, exercisedCredits.Count);
            Assert.IsTrue((from c in exercisedCredits where c.Id == exercisedCreditId select c).Single().AdjustedAllocation);
            Assert.IsFalse((from c in exercisedCredits where c.Id != exercisedCreditId select c).Single().AdjustedAllocation);

            allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(quantity, allocation.InitialQuantity);
            Assert.AreEqual(quantity - 1, allocation.RemainingQuantity);
        }

        [TestMethod]
        public void TestAllCreditsUsed()
        {
            const int initialQuantity = 20;
            var quantity = initialQuantity;

            // Allocate some credits.

            var employer = CreateEmployer(false, null, true, quantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);
            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit.

            IList<ExercisedCredit> exercisedCredits;
            Allocation allocation;
            for (var index = 0; index < 25; ++index)
            {
                var member = _membersCommand.CreateTestMember(index);
                SubmitApplication(member, jobAd);

                // Assert.

                if (quantity != 0)
                    --quantity;

                exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
                Assert.AreEqual(initialQuantity - quantity, exercisedCredits.Count);
                Assert.IsTrue(exercisedCredits.All(c => c.AdjustedAllocation));

                allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity, allocation.RemainingQuantity);
            }

            exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
            Assert.AreEqual(initialQuantity, exercisedCredits.Count);
            Assert.IsTrue(exercisedCredits.All(c => c.AdjustedAllocation));

            allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(0, allocation.RemainingQuantity);
        }

        [TestMethod]
        public void TestMaxCreditsUsed()
        {
            const int initialQuantity = 100;
            var quantity = initialQuantity;

            // Allocate some credits.

            var employer = CreateEmployer(false, null, true, quantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);
            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit.

            IList<ExercisedCredit> exercisedCredits;
            Allocation allocation;
            for (var index = 0; index < 50; ++index)
            {
                var member = _membersCommand.CreateTestMember(index);
                SubmitApplication(member, jobAd);

                // Assert.

                if (index < MaxCreditsPerJobAd)
                    --quantity;

                exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
                Assert.AreEqual(index + 1, exercisedCredits.Count);
                Assert.AreEqual(Math.Min(index + 1, MaxCreditsPerJobAd), exercisedCredits.Count(c => c.AdjustedAllocation));

                allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity, allocation.RemainingQuantity);
            }

            exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
            Assert.AreEqual(50, exercisedCredits.Count);
            Assert.AreEqual(MaxCreditsPerJobAd, exercisedCredits.Count(c => c.AdjustedAllocation));

            allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(initialQuantity - MaxCreditsPerJobAd, allocation.RemainingQuantity);
        }

        [TestMethod]
        public void TestMaxCreditsUsedMultipleJobAds()
        {
            const int initialQuantity = 100;
            var quantity = initialQuantity;

            // Allocate some credits.

            var employer = CreateEmployer(false, null, true, quantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);
            var jobAd1 = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            var jobAd2 = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit.

            IList<ExercisedCredit> exercisedCredits;
            Allocation allocation;
            for (var index = 0; index < 50; ++index)
            {
                var member1 = _membersCommand.CreateTestMember(2 * index);
                var member2 = _membersCommand.CreateTestMember(2 * index + 1);
                SubmitApplication(member1, jobAd1);
                SubmitApplication(member2, jobAd2);

                // Assert.

                if (index < MaxCreditsPerJobAd)
                    quantity = quantity - 2;

                exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
                Assert.AreEqual(2 * (index + 1), exercisedCredits.Count);
                Assert.AreEqual(2 * Math.Min(index + 1, MaxCreditsPerJobAd), exercisedCredits.Count(c => c.AdjustedAllocation));

                allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity, allocation.RemainingQuantity);
            }

            exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
            Assert.AreEqual(100, exercisedCredits.Count);
            Assert.AreEqual(2 * MaxCreditsPerJobAd, exercisedCredits.Count(c => c.AdjustedAllocation));

            allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(initialQuantity - 2 * MaxCreditsPerJobAd, allocation.RemainingQuantity);
        }

        [TestMethod]
        public void TestMaxCreditsUsedOneApplicantMultipleJobAds()
        {
            const int initialQuantity = 100;
            var quantity = initialQuantity;

            // Allocate some credits.

            var employer = CreateEmployer(false, null, true, quantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);
            var jobAd1 = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            var jobAd2 = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit.

            IList<ExercisedCredit> exercisedCredits;
            Allocation allocation;
            for (var index = 0; index < 50; ++index)
            {
                var member = _membersCommand.CreateTestMember(2 * index);
                SubmitApplication(member, jobAd1);
                SubmitApplication(member, jobAd2);

                // Assert.

                if (index < MaxCreditsPerJobAd)
                    --quantity;

                exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
                Assert.AreEqual(2 * (index + 1), exercisedCredits.Count);
                Assert.AreEqual(Math.Min(index + 1, MaxCreditsPerJobAd), exercisedCredits.Count(c => c.AdjustedAllocation));

                allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity, allocation.RemainingQuantity);
            }

            exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
            Assert.AreEqual(100, exercisedCredits.Count);
            Assert.AreEqual(MaxCreditsPerJobAd, exercisedCredits.Count(c => c.AdjustedAllocation));

            allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(initialQuantity - MaxCreditsPerJobAd, allocation.RemainingQuantity);
        }

        private void TestCredits(bool allocateCredits, int? quantity)
        {
            // Allocate some credits.

            var employer = CreateEmployer(false, null, allocateCredits, quantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);
            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit. This should never fail, even if there are no credits.

            var member = CreateMember(0);
            SubmitApplication(member, jobAd);

            // Assert.

            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ApplicantCredit>(hierarchy);
            var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);

            if (allocateCredits)
            {
                if (quantity == null)
                {
                    Assert.AreEqual(1, exercisedCredits.Count);
                    Assert.IsFalse(exercisedCredits[0].AdjustedAllocation);
                }
                else if (quantity.Value > 0)
                {
                    Assert.AreEqual(1, exercisedCredits.Count);
                    Assert.IsTrue(exercisedCredits[0].AdjustedAllocation);
                }
                else
                {
                    Assert.AreEqual(0, exercisedCredits.Count);
                }

                Assert.AreEqual(quantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity == 0 ? 0 : quantity - 1, allocation.RemainingQuantity);
            }
            else
            {
                Assert.AreEqual(0, exercisedCredits.Count);
                Assert.AreEqual(0, allocation.InitialQuantity);
                Assert.AreEqual(0, allocation.RemainingQuantity);
            }

            // Check.

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Applicant);
        }
    }
}
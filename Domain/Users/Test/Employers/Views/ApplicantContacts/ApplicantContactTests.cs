using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.ApplicantContacts
{
    [TestClass]
    public abstract class ApplicantContactTests
        : ViewsTests
    {
        [TestMethod]
        public void TestSomeContactCreditsNoApplicantCreditsContact()
        {
            TestContact(true, 20, false, null, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsNoApplicantCreditsContact()
        {
            TestContact(true, null, false, null, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsZeroApplicantCreditsContact()
        {
            TestContact(true, 20, true, 0, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsZeroApplicantCreditsContact()
        {
            TestContact(true, null, true, 0, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsSomeApplicantCreditsContact()
        {
            TestContact(true, 20, true, 20, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsSomeApplicantCreditsContact()
        {
            TestContact(true, null, true, 20, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsUnlimitedApplicantCreditsContact()
        {
            TestContact(true, 20, true, null, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsUnlimitedApplicantCreditsContact()
        {
            TestContact(true, null, true, null, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsNoApplicantCreditsContactApply()
        {
            TestContactApply(true, 20, false, null, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsNoApplicantCreditsContactApply()
        {
            TestContactApply(true, null, false, null, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsZeroApplicantCreditsContactApply()
        {
            TestContactApply(true, 20, true, 0, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsZeroApplicantCreditsContactApply()
        {
            TestContactApply(true, null, true, 0, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsSomeApplicantCreditsContactApply()
        {
            TestContactApply(true, 20, true, 20, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsSomeApplicantCreditsContactApply()
        {
            TestContactApply(true, null, true, 20, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsUnlimitedApplicantCreditsContactApply()
        {
            TestContactApply(true, 20, true, null, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsUnlimitedApplicantCreditsContactApply()
        {
            TestContactApply(true, null, true, null, false);
        }

        [TestMethod]
        public void TestNoContactCreditsNoApplicantCreditsApply()
        {
            TestApply(false, null, false, null, false);
        }

        [TestMethod]
        public void TestZeroContactCreditsNoApplicantCreditsApply()
        {
            TestApply(true, 0, false, null, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsNoApplicantCreditsApply()
        {
            TestApply(true, 20, false, null, false);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsNoApplicantCreditsApply()
        {
            TestApply(true, null, false, null, false);
        }

        [TestMethod]
        public void TestNoContactCreditsZeroApplicantCreditsApply()
        {
            TestApply(false, null, true, 0, false);
        }

        [TestMethod]
        public void TestZeroContactCreditsZeroApplicantCreditsApply()
        {
            TestApply(true, 0, true, 0, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsZeroApplicantCreditsApply()
        {
            TestApply(true, 20, true, 0, false);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsZeroApplicantCreditsApply()
        {
            TestApply(true, null, true, 0, false);
        }

        [TestMethod]
        public void TestNoContactCreditsSomeApplicantCreditsApply()
        {
            TestApply(false, null, true, 20, true);
        }

        [TestMethod]
        public void TestZeroContactCreditsSomeApplicantCreditsApply()
        {
            TestApply(true, 0, true, 20, true);
        }

        [TestMethod]
        public void TestSomeContactCreditsSomeApplicantCreditsApply()
        {
            TestApply(true, 20, true, 20, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsSomeApplicantCreditsApply()
        {
            TestApply(true, null, true, 20, true);
        }

        [TestMethod]
        public void TestNoContactCreditsUnlimitedApplicantCreditsApply()
        {
            TestApply(false, null, true, null, false);
        }

        [TestMethod]
        public void TestZeroContactCreditsUnlimitedApplicantCreditsApply()
        {
            TestApply(true, 0, true, null, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsUnlimitedApplicantCreditsApply()
        {
            TestApply(true, 20, true, null, false);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsUnlimitedApplicantCreditsApply()
        {
            TestApply(true, null, true, null, false);
        }

        [TestMethod]
        public void TestNoContactCreditsNoApplicantCreditsApplyContact()
        {
            TestApplyContact(false, null, false, null, true);
        }

        [TestMethod]
        public void TestZeroContactCreditsNoApplicantCreditsApplyContact()
        {
            TestApplyContact(true, 0, false, null, true);
        }

        [TestMethod]
        public void TestSomeContactCreditsNoApplicantCreditsApplyContact()
        {
            TestApplyContact(true, 20, false, null, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsNoApplicantCreditsApplyContact()
        {
            TestApplyContact(true, null, false, null, true);
        }

        [TestMethod]
        public void TestNoContactCreditsZeroApplicantCreditsApplyContact()
        {
            TestApplyContact(false, null, true, 0, false);
        }

        [TestMethod]
        public void TestZeroContactCreditsZeroApplicantCreditsApplyContact()
        {
            TestApplyContact(true, 0, true, 0, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsZeroApplicantCreditsApplyContact()
        {
            TestApplyContact(true, 20, true, 0, false);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsZeroApplicantCreditsApplyContact()
        {
            TestApplyContact(true, null, true, 0, false);
        }

        [TestMethod]
        public void TestNoContactCreditsSomeApplicantCreditsApplyContact()
        {
            TestApplyContact(false, null, true, 20, true);
        }

        [TestMethod]
        public void TestZeroContactCreditsSomeApplicantCreditsApplyContact()
        {
            TestApplyContact(true, 0, true, 20, true);
        }

        [TestMethod]
        public void TestSomeContactCreditsSomeApplicantCreditsApplyContact()
        {
            TestApplyContact(true, 20, true, 20, true);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsSomeApplicantCreditsApplyContact()
        {
            TestApplyContact(true, null, true, 20, true);
        }

        [TestMethod]
        public void TestNoContactCreditsUnlimitedApplicantCreditsApplyContact()
        {
            TestApplyContact(false, null, true, null, false);
        }

        [TestMethod]
        public void TestZeroContactCreditsUnlimitedApplicantCreditsApplyContact()
        {
            TestApplyContact(true, 0, true, null, false);
        }

        [TestMethod]
        public void TestSomeContactCreditsUnlimitedApplicantCreditsApplyContact()
        {
            TestApplyContact(true, 20, true, null, false);
        }

        [TestMethod]
        public void TestUnlimitedContactCreditsUnlimitedApplicantCreditsApplyContact()
        {
            TestApplyContact(true, null, true, null, false);
        }

        private void TestContact(bool allocateContactCredits, int? contactQuantity, bool allocateApplicantCredits, int? applicantQuantity, bool adjustAllocation)
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(allocateContactCredits, contactQuantity, allocateApplicantCredits, applicantQuantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);

            AssertNoCreditExercised<ContactCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ContactCredit>(employer, allocateContactCredits, contactQuantity, false);
            AssertNoCreditExercised<ApplicantCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ApplicantCredit>(employer, allocateApplicantCredits, applicantQuantity, false);
            AssertEffectiveAllocations<ContactCredit, ApplicantCredit>(employer, allocateContactCredits, contactQuantity, false, allocateApplicantCredits, applicantQuantity, false);

            // Contact.

            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member));

            // Check exercised credits.

            AssertCreditExercised<ContactCredit>(member.Id, hierarchy, allocateContactCredits, contactQuantity, adjustAllocation);
            AssertEffectiveAllocation<ContactCredit>(employer, allocateContactCredits, contactQuantity, adjustAllocation);
            AssertNoCreditExercised<ApplicantCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ApplicantCredit>(employer, allocateApplicantCredits, applicantQuantity, false);
            AssertEffectiveAllocations<ContactCredit, ApplicantCredit>(employer, allocateContactCredits, contactQuantity, adjustAllocation, allocateApplicantCredits, applicantQuantity, false);

            // Check current contact status.

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Contacted);

            // Check applicant status.

            Assert.AreEqual(0, _jobAdApplicantsQuery.GetApplications(employer, member.Id).Count);
        }

        private void TestApply(bool allocateContactCredits, int? contactQuantity, bool allocateApplicantCredits, int? applicantQuantity, bool adjustAllocation)
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(allocateContactCredits, contactQuantity, allocateApplicantCredits, applicantQuantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);

            AssertNoCreditExercised<ContactCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ContactCredit>(employer, allocateContactCredits, contactQuantity, false);
            AssertNoCreditExercised<ApplicantCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ApplicantCredit>(employer, allocateApplicantCredits, applicantQuantity, false);
            AssertEffectiveAllocations<ContactCredit, ApplicantCredit>(employer, allocateContactCredits, contactQuantity, false, allocateApplicantCredits, applicantQuantity, false);

            // Apply.

            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            SubmitApplication(member, jobAd);

            // Check exercised credits.

            AssertNoCreditExercised<ContactCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ContactCredit>(employer, allocateContactCredits, contactQuantity, false);
            AssertCreditExercised<ApplicantCredit>(member.Id, hierarchy, allocateApplicantCredits, applicantQuantity, adjustAllocation);
            AssertEffectiveAllocation<ApplicantCredit>(employer, allocateApplicantCredits, applicantQuantity, adjustAllocation);
            AssertEffectiveAllocations<ContactCredit, ApplicantCredit>(employer, allocateContactCredits, contactQuantity, false, allocateApplicantCredits, applicantQuantity, adjustAllocation);

            // Check current contact status.

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Applicant);

            // Check applicant status.

            Assert.AreEqual(1, _jobAdApplicantsQuery.GetApplications(employer, member.Id).Count);
        }

        private void TestContactApply(bool allocateContactCredits, int? contactQuantity, bool allocateApplicantCredits, int? applicantQuantity, bool adjustAllocation)
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(allocateContactCredits, contactQuantity, allocateApplicantCredits, applicantQuantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);

            AssertNoCreditExercised<ContactCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ContactCredit>(employer, allocateContactCredits, contactQuantity, false);
            AssertNoCreditExercised<ApplicantCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ApplicantCredit>(employer, allocateApplicantCredits, applicantQuantity, false);
            AssertEffectiveAllocations<ContactCredit, ApplicantCredit>(employer, allocateContactCredits, contactQuantity, false, allocateApplicantCredits, applicantQuantity, false);

            // Contact.

            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member));

            // Check exercised credits.

            AssertCreditExercised<ContactCredit>(member.Id, hierarchy, allocateContactCredits, contactQuantity, adjustAllocation);
            AssertEffectiveAllocation<ContactCredit>(employer, allocateContactCredits, contactQuantity, adjustAllocation);
            AssertNoCreditExercised<ApplicantCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ApplicantCredit>(employer, allocateApplicantCredits, applicantQuantity, false);
            AssertEffectiveAllocations<ContactCredit, ApplicantCredit>(employer, allocateContactCredits, contactQuantity, adjustAllocation, allocateApplicantCredits, applicantQuantity, false);

            // Apply.

            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            SubmitApplication(member, jobAd);

            AssertCreditExercised<ContactCredit>(member.Id, hierarchy, allocateContactCredits, contactQuantity, adjustAllocation);
            AssertEffectiveAllocation<ContactCredit>(employer, allocateContactCredits, contactQuantity, adjustAllocation);
            AssertCreditExercised<ApplicantCredit>(member.Id, hierarchy, allocateApplicantCredits, applicantQuantity, false);
            AssertEffectiveAllocation<ApplicantCredit>(employer, allocateApplicantCredits, applicantQuantity, false);
            AssertEffectiveAllocations<ContactCredit, ApplicantCredit>(employer, allocateContactCredits, contactQuantity, adjustAllocation, allocateApplicantCredits, applicantQuantity, false);

            // Check current contact status.

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Applicant);

            // Check applicant status.

            Assert.AreEqual(1, _jobAdApplicantsQuery.GetApplications(employer, member.Id).Count);
        }

        private void TestApplyContact(bool allocateContactCredits, int? contactQuantity, bool allocateApplicantCredits, int? applicantQuantity, bool adjustAllocation)
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(allocateContactCredits, contactQuantity, allocateApplicantCredits, applicantQuantity);
            var hierarchy = _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);

            AssertNoCreditExercised<ContactCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ContactCredit>(employer, allocateContactCredits, contactQuantity, false);
            AssertNoCreditExercised<ApplicantCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ApplicantCredit>(employer, allocateApplicantCredits, applicantQuantity, false);
            AssertEffectiveAllocations<ContactCredit, ApplicantCredit>(employer, allocateContactCredits, contactQuantity, false, allocateApplicantCredits, applicantQuantity, false);

            // Apply.

            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            SubmitApplication(member, jobAd);

            // Check exercised credits.

            AssertNoCreditExercised<ContactCredit>(member.Id, hierarchy);
            AssertEffectiveAllocation<ContactCredit>(employer, allocateContactCredits, contactQuantity, false);
            AssertCreditExercised<ApplicantCredit>(member.Id, hierarchy, allocateApplicantCredits, applicantQuantity, adjustAllocation);
            AssertEffectiveAllocation<ApplicantCredit>(employer, allocateApplicantCredits, applicantQuantity, adjustAllocation);
            AssertEffectiveAllocations<ContactCredit, ApplicantCredit>(employer, allocateContactCredits, contactQuantity, false, allocateApplicantCredits, applicantQuantity, adjustAllocation);

            // Contact.

            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member));
            AssertCreditExercised<ContactCredit>(member.Id, hierarchy, allocateContactCredits, contactQuantity, false);
            AssertEffectiveAllocation<ContactCredit>(employer, allocateContactCredits, contactQuantity, false);
            AssertCreditExercised<ApplicantCredit>(member.Id, hierarchy, allocateApplicantCredits, applicantQuantity, adjustAllocation);
            AssertEffectiveAllocation<ApplicantCredit>(employer, allocateApplicantCredits, applicantQuantity, adjustAllocation);
            AssertEffectiveAllocations<ContactCredit, ApplicantCredit>(employer, allocateContactCredits, contactQuantity, false, allocateApplicantCredits, applicantQuantity, adjustAllocation);

            // Check current contact status.

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Applicant);

            // Check applicant status.

            Assert.AreEqual(1, _jobAdApplicantsQuery.GetApplications(employer, member.Id).Count);
        }

        private void AssertNoCreditExercised<T>(Guid memberId, HierarchyPath hierarchy)
            where T : Credit
        {
            Assert.IsFalse(_exercisedCreditsQuery.HasExercisedCredit<T>(hierarchy, memberId));

            // Check exercised credits.

            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<T>(hierarchy);
            Assert.AreEqual(0, exercisedCredits.Count);
        }

        private void AssertCreditExercised<T>(Guid memberId, HierarchyPath hierarchy, bool allocate, int? quantity, bool adjustAllocation)
            where T : Credit
        {
            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<T>(hierarchy);

            if (allocate)
            {
                // If there was an initial non-zero quantity ensure that a credit has been exercised.

                if (quantity == null || quantity.Value != 0)
                {
                    Assert.IsTrue(_exercisedCreditsQuery.HasExercisedCredit<T>(hierarchy, memberId));
                    Assert.AreEqual(1, exercisedCredits.Count);

                    if (adjustAllocation)
                        Assert.IsTrue(exercisedCredits[0].AdjustedAllocation);
                    else
                        Assert.IsFalse(exercisedCredits[0].AdjustedAllocation);
                }
                else
                {
                    Assert.IsFalse(_exercisedCreditsQuery.HasExercisedCredit<T>(hierarchy, memberId));
                    Assert.AreEqual(0, exercisedCredits.Count);
                }
            }
            else
            {
                // No credits exercised and no credit used.

                Assert.IsFalse(_exercisedCreditsQuery.HasExercisedCredit<T>(hierarchy, memberId));
                Assert.AreEqual(0, exercisedCredits.Count);
            }
        }

        private void AssertEffectiveAllocation<T>(IEmployer employer, bool allocate, int? quantity, bool adjustAllocation)
            where T : Credit
        {
            AssertEffectiveAllocation(allocate, quantity, adjustAllocation, _employerCreditsQuery.GetEffectiveActiveAllocation<T>(employer));
        }

        private void AssertEffectiveAllocations<T1, T2>(IEmployer employer, bool allocate1, int? quantity1, bool adjustAllocation1, bool allocate2, int? quantity2, bool adjustAllocation2)
            where T1 : Credit
            where T2 : Credit
        {
            var credit1Id = _creditsQuery.GetCredit<T1>().Id;
            var credit2Id = _creditsQuery.GetCredit<T2>().Id;
            var allocations = _employerCreditsQuery.GetEffectiveActiveAllocations(employer, new[] { credit1Id, credit2Id });
            Assert.AreEqual(2, allocations.Count);
            AssertEffectiveAllocation(allocate1, quantity1, adjustAllocation1, allocations[credit1Id]);
            AssertEffectiveAllocation(allocate2, quantity2, adjustAllocation2, allocations[credit2Id]);
        }

        private static void AssertEffectiveAllocation(bool allocate, int? quantity, bool adjustAllocation, Allocation allocation)
        {
            if (allocate)
            {
                // Check that a credit has been used.

                if (adjustAllocation)
                {
                    Assert.AreEqual(quantity.Value, allocation.InitialQuantity.Value);
                    Assert.AreEqual(quantity.Value - 1, allocation.RemainingQuantity.Value);
                }
                else
                {
                    Assert.AreEqual(quantity, allocation.InitialQuantity);
                    Assert.AreEqual(quantity, allocation.RemainingQuantity);
                }
            }
            else
            {
                Assert.AreEqual(0, allocation.InitialQuantity);
                Assert.AreEqual(0, allocation.RemainingQuantity);
            }
        }
    }
}
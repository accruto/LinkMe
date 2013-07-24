using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Contacts
{
    public abstract class ContactTests
        : ViewsTests
    {
        private readonly IEmployerViewsRepository _employerViewsRepository = Resolve<IEmployerViewsRepository>();

        [TestMethod]
        public void TestAccessMemberViewingPhoneNumberNoCredits()
        {
            TestAccessMemberViewingPhoneNumber(true, false, 0, CanContactStatus.No);
        }

        [TestMethod]
        public void TestAccessMemberViewingPhoneNumberNoCreditsPhoneInvisible()
        {
            TestAccessMemberViewingPhoneNumber(false, false, 0, CanContactStatus.No);
        }

        [TestMethod]
        public void TestAccessMemberViewingPhoneNumberSomeCredits()
        {
            TestAccessMemberViewingPhoneNumber(true, true, 20, CanContactStatus.YesWithCredit);
        }

        [TestMethod]
        public void TestAccessMemberViewingPhoneNumberSomeCreditsPhoneInvisible()
        {
            TestAccessMemberViewingPhoneNumber(false, true, 20, CanContactStatus.YesWithCredit);
        }

        [TestMethod]
        public void TestAccessMemberViewingPhoneNumberUnlimitedCredits()
        {
            TestAccessMemberViewingPhoneNumber(true, true, null, CanContactStatus.YesWithoutCredit);
        }

        [TestMethod]
        public void TestAccessMemberViewingPhoneNumberUnlimitedCreditsPhoneInvisible()
        {
            TestAccessMemberViewingPhoneNumber(false, true, null, CanContactStatus.YesWithoutCredit);
        }

        [TestMethod]
        public void TestAccessMemberViewingPhoneNumberZeroCredits()
        {
            TestAccessMemberViewingPhoneNumber(true, true, 0, CanContactStatus.No);
        }

        [TestMethod]
        public void TestAccessMemberViewingPhoneNumberZeroCreditsPhoneInvisible()
        {
            TestAccessMemberViewingPhoneNumber(false, true, 0, CanContactStatus.No);
        }

        [TestMethod]
        public void TestAccessMemberNoCredits()
        {
            TestAccessMember(true, false, 0, CanContactStatus.No);
        }

        [TestMethod]
        public void TestAccessMemberNoCreditsPhoneInvisible()
        {
            TestAccessMember(false, false, 0, CanContactStatus.No);
        }

        [TestMethod]
        public void TestAccessMemberSomeCredits()
        {
            TestAccessMember(true, true, 20, CanContactStatus.YesWithCredit);
        }

        [TestMethod]
        public void TestAccessMemberSomeCreditsPhoneInvisible()
        {
            TestAccessMember(false, true, 20, CanContactStatus.YesWithCredit);
        }

        [TestMethod]
        public void TestAccessMemberUnlimitedCredits()
        {
            TestAccessMember(true, true, null, CanContactStatus.YesWithoutCredit);
        }

        [TestMethod]
        public void TestAccessMemberUnlimitedCreditsPhoneInvisible()
        {
            TestAccessMember(false, true, null, CanContactStatus.YesWithoutCredit);
        }

        [TestMethod]
        public void TestAccessMemberZeroCredits()
        {
            TestAccessMember(true, true, 0, CanContactStatus.No);
        }

        [TestMethod]
        public void TestAccessMemberZeroCreditsPhoneInvisible()
        {
            TestAccessMember(false, true, 0, CanContactStatus.No);
        }

        [TestMethod]
        public void TestNoAccessMemberNoCredits()
        {
            TestNoAccessMember(true, false, 0, CanContactStatus.YesIfHadCredit, ProfessionalContactDegree.NotContacted);
        }

        [TestMethod]
        public void TestNoAccessMemberNoCreditsPhoneInvisible()
        {
            TestNoAccessMember(false, false, 0, CanContactStatus.YesIfHadCredit, ProfessionalContactDegree.NotContacted);
        }

        [TestMethod]
        public void TestNoAccessMemberSomeCredits()
        {
            TestNoAccessMember(true, true, 20, CanContactStatus.YesWithCredit, ProfessionalContactDegree.NotContacted);
        }

        [TestMethod]
        public void TestNoAccessMemberSomeCreditsPhoneInvisible()
        {
            TestNoAccessMember(false, true, 20, CanContactStatus.YesWithCredit, ProfessionalContactDegree.NotContacted);
        }

        [TestMethod]
        public void TestNoAccessMemberUnlimitedCredits()
        {
            TestNoAccessMember(true, true, null, CanContactStatus.YesWithoutCredit, ProfessionalContactDegree.Contacted);
        }

        [TestMethod]
        public void TestNoAccessMemberUnlimitedCreditsPhoneInvisible()
        {
            TestNoAccessMember(false, true, null, CanContactStatus.YesWithoutCredit, ProfessionalContactDegree.Contacted);
        }

        [TestMethod]
        public void TestNoAccessMemberZeroCredits()
        {
            TestNoAccessMember(true, true, 0, CanContactStatus.YesIfHadCredit, ProfessionalContactDegree.NotContacted);
        }

        [TestMethod]
        public void TestNoAccessMemberZeroCreditsPhoneInvisible()
        {
            TestNoAccessMember(false, true, 0, CanContactStatus.YesIfHadCredit, ProfessionalContactDegree.NotContacted);
        }

        [TestMethod]
        public void TestNoCreditUsedHasBeenAccessed()
        {
            // For old candidates we sometimes did not record the credit usage against unlimited allocations.
            // Should still be able to contact without a credit if they have been accessed.

            var employer = CreateEmployer(true, 10, false, null);
            if (employer == null)
                return;
            var member = CreateMember(1);

            AssertView(employer, member, CanContactStatus.YesWithCredit, true, ProfessionalContactDegree.NotContacted);

            // Record an access but don't use a credit.

            var access = new MemberAccess
            {
                Reason = MemberAccessReason.PhoneNumberViewed,
                EmployerId = employer.Id,
                MemberId = member.Id,
                ExercisedCreditId = null,
            };

            access.Prepare();
            access.Validate();
            _employerViewsRepository.CreateMemberAccess(access);

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Contacted);
        }

        private void TestNoAccessMember(bool phoneVisible, bool allocateCredits, int? quantity, CanContactStatus canContact, ProfessionalContactDegree contactDegree)
        {
            var member = CreateMember(1, phoneVisible);
            var employer = CreateEmployer(allocateCredits, quantity, false, null);
            AssertView(employer, member, canContact, phoneVisible, contactDegree);
        }

        private void CheckCanAccessMember(IEmployer employer, ProfessionalView view)
        {
            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, view, MemberAccessReason.MessageSent);
        }

        private void AccessMember(IEmployer employer, ProfessionalView view)
        {
            _employerMemberViewsCommand.AccessMember(_app, employer, view, MemberAccessReason.MessageSent);
        }

        private void CheckCanAccessMemberViewingPhoneNumber(IEmployer employer, ProfessionalView view)
        {
            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, view, MemberAccessReason.PhoneNumberViewed);
        }

        private void AccessMemberViewingPhoneNumber(IEmployer employer, ProfessionalView view)
        {
            _employerMemberViewsCommand.AccessMember(_app, employer, view, MemberAccessReason.PhoneNumberViewed);
        }

        private void TestAccessMember(bool phoneVisible, bool allocateCredits, int? quantity, CanContactStatus canContact)
        {
            TestContact(
                phoneVisible,
                allocateCredits,
                quantity,
                canContact,
                false,
                (e, v) =>
                {
                    CheckCanAccessMember(e, v);
                    AccessMember(e, v);
                });
        }

        private void TestAccessMemberViewingPhoneNumber(bool phoneVisible, bool allocateCredits, int? quantity, CanContactStatus canContact)
        {
            TestContact(
                phoneVisible,
                allocateCredits,
                quantity,
                canContact,
                true,
                (e, v) =>
                {
                    CheckCanAccessMemberViewingPhoneNumber(e, v);
                    AccessMemberViewingPhoneNumber(e, v);
                });
        }

        private void TestContact(bool phoneVisible, bool allocateCredits, int? quantity, CanContactStatus canContact, bool isAccessingByPhone, Action<IEmployer, ProfessionalView> access)
        {
            // Set up member.

            var member = CreateMember(1, phoneVisible);

            // Set up employer.

            var employer = CreateEmployer(allocateCredits, quantity, false, null);
            var hierarchy = employer == null
                ? null
                : _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);

            try
            {
                ProfessionalContactDegree contactDegree;

                // Access them.

                var view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
                access(employer, view);

                // Check what happened to he credits.

                if (canContact == CanContactStatus.No || (isAccessingByPhone && !phoneVisible))
                {
                    AssertNoCredit(phoneVisible, quantity, isAccessingByPhone, employer, hierarchy);
                    contactDegree = canContact == CanContactStatus.YesWithoutCredit ? ProfessionalContactDegree.Contacted : ProfessionalContactDegree.NotContacted;
                }
                else if (canContact == CanContactStatus.YesWithCredit)
                {
                    AssertWithCredit(quantity, employer, hierarchy);

                    // Credit has been used on the member now.

                    canContact = CanContactStatus.YesWithoutCredit;
                    contactDegree = ProfessionalContactDegree.Contacted;
                }
                else
                {
                    AssertWithoutCredit(employer, hierarchy);
                    contactDegree = ProfessionalContactDegree.Contacted;
                }

                // Check everything after the contact.

                AssertView(employer, member, canContact, phoneVisible, contactDegree);

                // Contact again.

                access(employer, view);

                // Check everything after the contact.

                AssertView(employer, member, canContact, phoneVisible, contactDegree);
            }
            catch (NotVisibleException)
            {
                Assert.IsFalse(phoneVisible);
            }
            catch (InsufficientCreditsException)
            {
                Assert.IsTrue(canContact == CanContactStatus.No || employer == null);
            }
        }

        private Member CreateMember(int index, bool phoneVisible)
        {
            var member = CreateMember(index);

            member.VisibilitySettings.Professional.EmploymentVisibility = phoneVisible
                ? member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.PhoneNumbers)
                : member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);
            _membersCommand.UpdateMember(member);

            return member;
        }

        private void AssertNoCredit(bool phoneVisible, int? quantity, bool isAccessingByPhone, IEmployer employer, HierarchyPath hierarchy)
        {
            if (isAccessingByPhone && !phoneVisible)
            {
                if (employer != null)
                {
                    // Member settings are the reason that the user cannot be unlocked.

                    // No credit has been exercised.

                    var exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ContactCredit>(hierarchy);
                    Assert.AreEqual(0, exercisedCredits.Count);

                    // No allocations have been adjusted.

                    var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ContactCredit>(employer);
                    Assert.AreEqual(quantity, allocation.InitialQuantity);
                    Assert.AreEqual(quantity, allocation.RemainingQuantity);
                }
            }
            else
            {
                Assert.Fail("Should not have been able to contact.");
            }
        }

        private void AssertWithCredit(int? quantity, IEmployer employer, HierarchyPath hierarchy)
        {
            // A credit should have been exercised.

            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ContactCredit>(hierarchy);
            Assert.AreEqual(1, exercisedCredits.Count);

            // An allocation should have beedn decremented.

            Assert.IsTrue(exercisedCredits[0].AdjustedAllocation);

            var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ContactCredit>(employer);
            Assert.AreEqual(quantity.Value, allocation.InitialQuantity.Value);
            Assert.AreEqual(quantity.Value - 1, allocation.RemainingQuantity.Value);
        }

        private void AssertWithoutCredit(IEmployer employer, HierarchyPath hierarchy)
        {
            // A credit should have been exercised.

            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits<ContactCredit>(hierarchy);
            Assert.AreEqual(1, exercisedCredits.Count);

            // No allocation should have been decremented though.

            Assert.IsFalse(exercisedCredits[0].AdjustedAllocation);

            var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ContactCredit>(employer);
            Assert.IsNull(allocation.InitialQuantity);
            Assert.IsNull(allocation.RemainingQuantity);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates.PhoneNumbers
{
    public abstract class CreditsTests
        : PhoneNumberTests
    {
        private const string MobilePhoneNumberFormat = "1999999{0}";
        private const string HomePhoneNumberFormat = "2999999{0}";
        private const string WorkPhoneNumberFormat = "3999999{0}";

        [TestMethod]
        public void TestVisible()
        {
            const bool isResumeVisible = true;
            const bool isPhoneNumberVisible = true;
            const bool hasPhoneNumbers = true;
            TestPhoneNumbers(isResumeVisible, isPhoneNumberVisible, hasPhoneNumbers);
        }

        [TestMethod]
        public void TestNoPhoneNumber()
        {
            const bool isResumeVisible = true;
            const bool isPhoneNumberVisible = true;
            const bool hasPhoneNumbers = false;
            TestPhoneNumbers(isResumeVisible, isPhoneNumberVisible, hasPhoneNumbers);
        }

        [TestMethod]
        public void TestResumeNotVisible()
        {
            const bool isResumeVisible = false;
            const bool isPhoneNumberVisible = true;
            const bool hasPhoneNumbers = true;
            TestPhoneNumbers(isResumeVisible, isPhoneNumberVisible, hasPhoneNumbers);
        }

        [TestMethod]
        public void TestPhoneNumberNotVisible()
        {
            const bool isResumeVisible = true;
            const bool isPhoneNumberVisible = false;
            const bool hasPhoneNumbers = true;
            TestPhoneNumbers(isResumeVisible, isPhoneNumberVisible, hasPhoneNumbers);
        }

        [TestMethod]
        public void TestResumeNotVisibleNoPhoneNumber()
        {
            const bool isResumeVisible = false;
            const bool isPhoneNumberVisible = true;
            const bool hasPhoneNumbers = false;
            TestPhoneNumbers(isResumeVisible, isPhoneNumberVisible, hasPhoneNumbers);
        }

        [TestMethod]
        public void TestPhoneNumberNotVisibleNoPhoneNumber()
        {
            const bool isResumeVisible = true;
            const bool isPhoneNumberVisible = false;
            const bool hasPhoneNumbers = false;
            TestPhoneNumbers(isResumeVisible, isPhoneNumberVisible, hasPhoneNumbers);
        }

        protected abstract Employer CreateEmployer(Member member);
        protected abstract bool HasBeenAccessed { get; }
        protected abstract CanContactStatus CanContact { get; }

        private void TestPhoneNumbers(bool isResumeVisible, bool isPhoneNumberVisible, bool hasPhoneNumbers)
        {
            var member = CreateMember();
            var employer = CreateEmployer(member);
            UpdateMember(member, isResumeVisible, isPhoneNumberVisible, hasPhoneNumbers);

            LogIn(employer);

            var canContact = CanContact;
            var hasBeenAccessed = HasBeenAccessed;
            AssertCandidate(Candidate(member.Id), canContact, hasBeenAccessed, isResumeVisible, isPhoneNumberVisible, hasPhoneNumbers, member.PhoneNumbers);

            // Access the phone numbers.

            var model = PhoneNumbers(member.Id);
            if (canContact != CanContactStatus.No && canContact != CanContactStatus.YesIfHadCredit && isResumeVisible && isPhoneNumberVisible && hasPhoneNumbers)
            {
                // Status should be changed.

                canContact = CanContactStatus.YesWithoutCredit;
                hasBeenAccessed = true;

                AssertJsonSuccess(model);
                AssertCandidate(model, canContact, true, true, true, true, member.PhoneNumbers);
            }
            else
            {
                if (!isResumeVisible || !isPhoneNumberVisible || !hasPhoneNumbers)
                    AssertJsonError(model, null, "The candidate details have been hidden by the candidate.");
                else
                    AssertJsonError(model, null, "You need 1 credit to perform this action but you have none available.");
            }

            AssertCandidate(Candidate(member.Id), canContact, hasBeenAccessed, isResumeVisible, isPhoneNumberVisible, hasPhoneNumbers, member.PhoneNumbers);
        }

        private void UpdateMember(Member member, bool isResumeVisible, bool isPhoneNumberVisible, bool hasPhoneNumbers)
        {
            if (hasPhoneNumbers)
            {
                member.PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber {Number = string.Format(MobilePhoneNumberFormat, 0), Type = PhoneNumberType.Mobile},
                    new PhoneNumber {Number = string.Format(HomePhoneNumberFormat, 0), Type = PhoneNumberType.Home},
                    new PhoneNumber {Number = string.Format(WorkPhoneNumberFormat, 0), Type = PhoneNumberType.Work},
                };
            }
            else
            {
                member.PhoneNumbers = null;
            }

            member.VisibilitySettings.Professional.EmploymentVisibility = isResumeVisible
                ? member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Resume)
                : member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Resume);

            member.VisibilitySettings.Professional.EmploymentVisibility = isPhoneNumberVisible
                ? member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.PhoneNumbers)
                : member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);

            _memberAccountsCommand.UpdateMember(member);
        }

        private static void AssertCandidate(CandidateResponseModel model, CanContactStatus canContact, bool hasBeenAccessed, bool isResumeVisible, bool isPhoneNumberVisible, bool hasPhoneNumbers, ICollection<PhoneNumber> phoneNumbers)
        {
            // Cannot contact if they have hidden their resume.

            canContact = isResumeVisible ? canContact : CanContactStatus.No;

            // Cannot contact by phone if also they don't have phone numbers or have hidden them.

            var canContactByPhone = canContact == CanContactStatus.No
                ? CanContactStatus.No
                : !isPhoneNumberVisible
                    ? CanContactStatus.No
                    : phoneNumbers == null || phoneNumbers.Count == 0
                        ? CanContactStatus.No
                        : canContact;

            Assert.AreEqual(canContact, model.Candidate.CanContact);
            Assert.AreEqual(canContactByPhone, model.Candidate.CanContactByPhone);
            Assert.AreEqual(hasBeenAccessed, model.Candidate.HasBeenAccessed);

            // Must have phone numbers and be visible and also have been accessed.

            if (isResumeVisible && isPhoneNumberVisible && hasPhoneNumbers && hasBeenAccessed)
            {
                Assert.IsTrue(phoneNumbers != null && phoneNumbers.Count > 0);
                Assert.IsTrue((from p in phoneNumbers select p.Number).SequenceEqual(model.Candidate.PhoneNumbers));
            }
            else
            {
                Assert.IsNull(model.Candidate.PhoneNumbers);
            }
        }
    }
}
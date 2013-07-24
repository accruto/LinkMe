using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class SecondaryPhoneTests
        : FieldsTests
    {
        private const string NewPhoneNumber = "2222 2222";
        private const PhoneNumberType NewPhoneNumberType = PhoneNumberType.Work;

        protected override void AssertManualDefault()
        {
            AssertDefault();
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            AssertDefault();
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
            member.PhoneNumbers = new List<PhoneNumber> { member.PhoneNumbers[0], new PhoneNumber { Number = new string('a', 5), Type = PhoneNumberType.Mobile } };
            TestErrors(instanceId, member, candidate, resume, "The secondary phone number must be between 8 and 20 characters in length and not have any invalid characters.");
            member.PhoneNumbers = new List<PhoneNumber> { member.PhoneNumbers[0], new PhoneNumber { Number = new string('a', 40), Type = PhoneNumberType.Mobile } };
            TestErrors(instanceId, member, candidate, resume, "The secondary phone number must be between 8 and 20 characters in length and not have any invalid characters.");
            member.PhoneNumbers = new List<PhoneNumber> { member.PhoneNumbers[0], new PhoneNumber { Number = "%#%7797897@#$", Type = PhoneNumberType.Mobile } };
            TestErrors(instanceId, member, candidate, resume, "The secondary phone number must be between 8 and 20 characters in length and not have any invalid characters.");
            member.PhoneNumbers = new List<PhoneNumber> { member.PhoneNumbers[0] };
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.AreEqual(1, member.PhoneNumbers.Count);
            member.PhoneNumbers.Add(new PhoneNumber { Number = NewPhoneNumber, Type = NewPhoneNumberType });
        }

        private void AssertDefault()
        {
            Assert.AreEqual(string.Empty, _secondaryPhoneNumberTextBox.Text);
            Assert.IsTrue(_secondaryMobileRadioButton.IsChecked);
            Assert.IsFalse(_secondaryHomeRadioButton.IsChecked);
            Assert.IsFalse(_secondaryWorkRadioButton.IsChecked);
        }
    }
}

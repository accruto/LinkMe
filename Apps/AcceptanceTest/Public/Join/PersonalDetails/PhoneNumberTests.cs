using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class PhoneNumberTests
        : FieldsTests
    {
        private const string NewPhoneNumber = "2222 2222";
        private const PhoneNumberType NewPhoneNumberType = PhoneNumberType.Home;

        protected override void AssertManualDefault()
        {
            AssertDefault(string.Empty, PhoneNumberType.Mobile);
        }

        protected override void AssertManualJoinedDefault(IMember member)
        {
            AssertDefault(string.Empty, PhoneNumberType.Mobile);
        }

        protected override void AssertUploadDefault(IMember member)
        {
            AssertDefault(member.PhoneNumbers[0].Number, member.PhoneNumbers[0].Type);
        }

        protected override void AssertUploadJoinedDefault(IMember member)
        {
            AssertDefault(member.PhoneNumbers[0].Number, member.PhoneNumbers[0].Type);
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn)
        {
            member.PhoneNumbers = null;
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "Please provide a phone number.");

            member.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = new string('a', 5), Type = PhoneNumberType.Mobile } };
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");
            member.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = new string('a', 40), Type = PhoneNumberType.Mobile } };
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");
            member.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = "%#%7797897@#$", Type = PhoneNumberType.Mobile } };
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            if (member.PhoneNumbers != null)
            {
                Assert.AreEqual(1, member.PhoneNumbers.Count);
                Assert.AreNotEqual(NewPhoneNumber, member.PhoneNumbers[0].Number);
                Assert.AreNotEqual(NewPhoneNumberType, member.PhoneNumbers[0].Type);
            }
            member.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = NewPhoneNumber, Type = NewPhoneNumberType } };
        }

        private void AssertDefault(string number, PhoneNumberType type)
        {
            Assert.AreEqual(number, _phoneNumberTextBox.Text);
            Assert.AreEqual(type == PhoneNumberType.Mobile, _mobileRadioButton.IsChecked);
            Assert.AreEqual(type == PhoneNumberType.Home, _homeRadioButton.IsChecked);
            Assert.AreEqual(type == PhoneNumberType.Work, _workRadioButton.IsChecked);
        }
    }
}
using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class PhoneNumberTests
        : EditJobAdTests
    {
        private const string PhoneNumber = "99999999";
        private const string ChangedPhoneNumber = "44444444";

        protected override void SetValue(JobAd jobAd)
        {
            if (jobAd.ContactDetails == null)
                jobAd.ContactDetails = new ContactDetails { PhoneNumber = PhoneNumber };
            else
                jobAd.ContactDetails.PhoneNumber = PhoneNumber;
        }

        protected override void SetDisplayValue()
        {
            _phoneNumberTextBox.Text = ChangedPhoneNumber;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedPhoneNumber, jobAd.ContactDetails.PhoneNumber);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _phoneNumberTextBox.Text = new string('1', 5), "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");
            AssertErrorValue(save, jobAdId, () => _phoneNumberTextBox.Text = new string('1', 35), "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");
            AssertErrorValue(save, jobAdId, () => _phoneNumberTextBox.Text = "d&*))#", "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");
        }
    }
}

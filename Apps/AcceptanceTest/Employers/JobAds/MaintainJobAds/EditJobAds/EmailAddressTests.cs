using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class EmailAddressTests
        : EditJobAdTests
    {
        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string ChangedEmailAddress = "msimpson@test.linkme.net.au";

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.ContactDetails = new ContactDetails
            {
                EmailAddress = EmailAddress,
            };
        }

        protected override void SetDisplayValue()
        {
            _emailAddressTextBox.Text = ChangedEmailAddress;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedEmailAddress, jobAd.ContactDetails.EmailAddress);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _emailAddressTextBox.Text = "", "The email address is required.");
            AssertErrorValue(save, jobAdId, () => _emailAddressTextBox.Text = "d&*))#", "The email address must be valid and have less than 320 characters.");
        }
    }
}

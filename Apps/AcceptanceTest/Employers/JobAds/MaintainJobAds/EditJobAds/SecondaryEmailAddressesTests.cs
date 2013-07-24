using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class SecondaryEmailAddressesTests
        : EditJobAdTests
    {
        private const string SecondaryEmailAddresses = "bgumble@test.linkme.net.au";
        private const string ChangedSecondaryEmailAddresses = "moe@test.linkme.net.au";

        protected override void SetValue(JobAd jobAd)
        {
            if (jobAd.ContactDetails == null)
                jobAd.ContactDetails = new ContactDetails { SecondaryEmailAddresses = SecondaryEmailAddresses };
            else
                jobAd.ContactDetails.SecondaryEmailAddresses = SecondaryEmailAddresses;
        }

        protected override void SetDisplayValue()
        {
            _secondaryEmailAddressesTextBox.Text = ChangedSecondaryEmailAddresses;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedSecondaryEmailAddresses, jobAd.ContactDetails.SecondaryEmailAddresses);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _secondaryEmailAddressesTextBox.Text = "d&*))#", "The secondary email addresses must be valid and have less than 320 characters.");
        }
    }
}

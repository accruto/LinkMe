using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class FaxNumberTests
        : EditJobAdTests
    {
        private const string FaxNumber = "88888888";
        private const string ChangedFaxNumber = "77777777";

        protected override void SetValue(JobAd jobAd)
        {
            if (jobAd.ContactDetails == null)
                jobAd.ContactDetails = new ContactDetails { FaxNumber = FaxNumber };
            else
                jobAd.ContactDetails.FaxNumber = FaxNumber;
        }

        protected override void SetDisplayValue()
        {
            _faxNumberTextBox.Text = ChangedFaxNumber;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedFaxNumber, jobAd.ContactDetails.FaxNumber);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _faxNumberTextBox.Text = new string('1', 5), "The fax number must be between 8 and 20 characters in length and not have any invalid characters.");
            AssertErrorValue(save, jobAdId, () => _faxNumberTextBox.Text = new string('1', 35), "The fax number must be between 8 and 20 characters in length and not have any invalid characters.");
            AssertErrorValue(save, jobAdId, () => _faxNumberTextBox.Text = "d&*))#", "The fax number must be between 8 and 20 characters in length and not have any invalid characters.");
        }
    }
}

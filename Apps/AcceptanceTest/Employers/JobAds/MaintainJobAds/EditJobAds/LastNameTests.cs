using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class LastNameTests
        : EditJobAdTests
    {
        private const string LastName = "Simpson";
        private const string ChangedLastName = "Gumble";

        protected override void SetValue(JobAd jobAd)
        {
            if (jobAd.ContactDetails == null)
                jobAd.ContactDetails = new ContactDetails { LastName = LastName };
            else
                jobAd.ContactDetails.LastName = LastName;
        }

        protected override void SetDisplayValue()
        {
            _lastNameTextBox.Text = ChangedLastName;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedLastName, jobAd.ContactDetails.LastName);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _lastNameTextBox.Text = new string('a', 1), "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertErrorValue(save, jobAdId, () => _lastNameTextBox.Text = new string('a', 35), "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertErrorValue(save, jobAdId, () => _lastNameTextBox.Text = "d&*))#", "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
        }
    }
}

using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class FirstNameTests
        : EditJobAdTests
    {
        private const string FirstName = "Homer";
        private const string ChangedFirstName = "Marge";

        protected override void SetValue(JobAd jobAd)
        {
            if (jobAd.ContactDetails == null)
                jobAd.ContactDetails = new ContactDetails { FirstName = FirstName };
            else
                jobAd.ContactDetails.FirstName = FirstName;
        }

        protected override void SetDisplayValue()
        {
            _firstNameTextBox.Text = ChangedFirstName;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedFirstName, jobAd.ContactDetails.FirstName);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _firstNameTextBox.Text = new string('a', 1), "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertErrorValue(save, jobAdId, () => _firstNameTextBox.Text = new string('a', 35), "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertErrorValue(save, jobAdId, () => _firstNameTextBox.Text = "d&*))#", "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
        }
    }
}

using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class PhoneNumberTests
        : CreateJobAdTests
    {
        private const string PhoneNumber = "99999999";

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual(employer == null ? "" : employer.PhoneNumber.Number, _phoneNumberTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.AreEqual(employer == null ? null : employer.PhoneNumber.Number, jobAd.ContactDetails.PhoneNumber);
        }

        protected override void SetDisplayValue()
        {
            _phoneNumberTextBox.Text = PhoneNumber;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(PhoneNumber, jobAd.ContactDetails.PhoneNumber);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _phoneNumberTextBox.Text = new string('1', 5), "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");
            AssertErrorValue(save, employer, () => _phoneNumberTextBox.Text = new string('1', 35), "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");
            AssertErrorValue(save, employer, () => _phoneNumberTextBox.Text = "d&*))#", "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");
        }
    }
}

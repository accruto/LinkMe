using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class EmailAddressTests
        : CreateJobAdTests
    {
        private const string EmailAddress = "hsimpson@test.linkme.net.au";

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual(employer == null ? "" : employer.EmailAddress.Address, _emailAddressTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.AreEqual(employer == null ? DefaultEmailAddress : employer.EmailAddress.Address, jobAd.ContactDetails.EmailAddress);
        }

        protected override void SetDisplayValue()
        {
            _emailAddressTextBox.Text = EmailAddress;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(EmailAddress, jobAd.ContactDetails.EmailAddress);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _emailAddressTextBox.Text = "", "The email address is required.");
            AssertErrorValue(save, employer, () => _emailAddressTextBox.Text = "d&*))#", "The email address must be valid and have less than 320 characters.");
        }
    }
}

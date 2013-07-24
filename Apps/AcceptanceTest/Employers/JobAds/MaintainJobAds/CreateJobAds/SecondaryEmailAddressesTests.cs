using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class SecondaryEmailAddressesTests
        : CreateJobAdTests
    {
        private const string SecondaryEmailAddresses = "bgumble@test.linkme.net.au";

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _secondaryEmailAddressesTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsNull(jobAd.ContactDetails.SecondaryEmailAddresses);
        }

        protected override void SetDisplayValue()
        {
            _secondaryEmailAddressesTextBox.Text = SecondaryEmailAddresses;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(SecondaryEmailAddresses, jobAd.ContactDetails.SecondaryEmailAddresses);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _secondaryEmailAddressesTextBox.Text = "d&*))#", "The secondary email addresses must be valid and have less than 320 characters.");
        }
    }
}

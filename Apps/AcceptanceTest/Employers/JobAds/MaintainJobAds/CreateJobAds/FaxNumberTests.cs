using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class FaxNumberTests
        : CreateJobAdTests
    {
        private const string FaxNumber = "88888888";

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _faxNumberTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsNull(jobAd.ContactDetails.FaxNumber);
        }

        protected override void SetDisplayValue()
        {
            _faxNumberTextBox.Text = FaxNumber;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(FaxNumber, jobAd.ContactDetails.FaxNumber);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _faxNumberTextBox.Text = new string('1', 5), "The fax number must be between 8 and 20 characters in length and not have any invalid characters.");
            AssertErrorValue(save, employer, () => _faxNumberTextBox.Text = new string('1', 35), "The fax number must be between 8 and 20 characters in length and not have any invalid characters.");
            AssertErrorValue(save, employer, () => _faxNumberTextBox.Text = "d&*))#", "The fax number must be between 8 and 20 characters in length and not have any invalid characters.");
        }
    }
}

using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class LastNameTests
        : CreateJobAdTests
    {
        private const string LastName = "Simpson";

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual(employer == null ? "" : employer.LastName, _lastNameTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.AreEqual(employer == null ? null : employer.LastName, jobAd.ContactDetails.LastName);
        }

        protected override void SetDisplayValue()
        {
            _lastNameTextBox.Text = LastName;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(LastName, jobAd.ContactDetails.LastName);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _lastNameTextBox.Text = new string('a', 1), "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertErrorValue(save, employer, () => _lastNameTextBox.Text = new string('a', 35), "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertErrorValue(save, employer, () => _lastNameTextBox.Text = "d&*))#", "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
        }
    }
}

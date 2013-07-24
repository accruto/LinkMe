using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class FirstNameTests
        : CreateJobAdTests
    {
        private const string FirstName = "Homer";

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual(employer == null ? "" : employer.FirstName, _firstNameTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.AreEqual(employer == null ? null : employer.FirstName, jobAd.ContactDetails.FirstName);
        }

        protected override void SetDisplayValue()
        {
            _firstNameTextBox.Text = FirstName;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(FirstName, jobAd.ContactDetails.FirstName);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _firstNameTextBox.Text = new string('a', 1), "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertErrorValue(save, employer, () => _firstNameTextBox.Text = new string('a', 35), "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertErrorValue(save, employer, () => _firstNameTextBox.Text = "d&*))#", "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
        }
    }
}

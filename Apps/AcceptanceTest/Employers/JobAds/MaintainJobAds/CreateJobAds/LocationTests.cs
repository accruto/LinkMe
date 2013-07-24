using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class LocationTests
        : CreateJobAdTests
    {
        private const string Location = "Norlane VIC 3214";

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _locationTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.AreEqual(DefaultLocation, jobAd.Description.Location.ToString());
        }

        protected override void SetDisplayValue()
        {
            _locationTextBox.Text = Location;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(Location, jobAd.Description.Location.ToString());
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _locationTextBox.Text = "", "The location is required.");
        }
    }
}

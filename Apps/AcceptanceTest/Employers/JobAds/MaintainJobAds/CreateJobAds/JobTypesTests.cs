using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class JobTypesTests
        : CreateJobAdTests
    {
        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.IsFalse(_fullTimeCheckBox.IsChecked);
            Assert.IsFalse(_partTimeCheckBox.IsChecked);
            Assert.IsFalse(_contractCheckBox.IsChecked);
            Assert.IsFalse(_tempCheckBox.IsChecked);
            Assert.IsFalse(_jobShareCheckBox.IsChecked);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.AreEqual(DefaultJobTypes, jobAd.Description.JobTypes);
        }

        protected override void SetDisplayValue()
        {
            _fullTimeCheckBox.IsChecked = true;
            _partTimeCheckBox.IsChecked = true;
            _contractCheckBox.IsChecked = true;
            _tempCheckBox.IsChecked = true;
            _jobShareCheckBox.IsChecked = false;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(JobTypes.FullTime | JobTypes.PartTime | JobTypes.Contract | JobTypes.Temp, jobAd.Description.JobTypes);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => { _fullTimeCheckBox.IsChecked = false; _partTimeCheckBox.IsChecked = false; _contractCheckBox.IsChecked = false; _tempCheckBox.IsChecked = false; _jobShareCheckBox.IsChecked = false; }, "The job type is required.");
        }
    }
}

using System;
using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class JobTypesTests
        : EditJobAdTests
    {
        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.JobTypes = JobTypes.FullTime | JobTypes.PartTime | JobTypes.Contract | JobTypes.Temp;
        }

        protected override void SetDisplayValue()
        {
            _fullTimeCheckBox.IsChecked = false;
            _partTimeCheckBox.IsChecked = false;
            _contractCheckBox.IsChecked = false;
            _tempCheckBox.IsChecked = false;
            _jobShareCheckBox.IsChecked = true;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(JobTypes.JobShare, jobAd.Description.JobTypes);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => { _fullTimeCheckBox.IsChecked = false; _partTimeCheckBox.IsChecked = false; _contractCheckBox.IsChecked = false; _tempCheckBox.IsChecked = false; _jobShareCheckBox.IsChecked = false; }, "The job type is required.");
        }
    }
}

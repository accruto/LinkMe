using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class ResidencyRequiredTests
        : EditJobAdTests
    {
        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.ResidencyRequired = false;
        }

        protected override void SetDisplayValue()
        {
            _residencyRequiredCheckBox.IsChecked = true;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.IsTrue(jobAd.Description.ResidencyRequired);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
        }
    }
}

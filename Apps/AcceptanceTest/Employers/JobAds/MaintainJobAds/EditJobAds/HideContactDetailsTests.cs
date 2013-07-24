using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class HideContactDetailsTests
        : EditJobAdTests
    {
        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Visibility.HideContactDetails = true;
        }

        protected override void SetDisplayValue()
        {
            _hideContactDetailsCheckBox.IsChecked = false;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.IsFalse(jobAd.Visibility.HideContactDetails);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
        }
    }
}

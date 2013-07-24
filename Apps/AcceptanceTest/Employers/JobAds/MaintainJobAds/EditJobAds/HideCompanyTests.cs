using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class HideCompanyTests
        : EditJobAdTests
    {
        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Visibility.HideCompany = true;
        }

        protected override void SetDisplayValue()
        {
            _hideCompanyCheckBox.IsChecked = false;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.IsFalse(jobAd.Visibility.HideCompany);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
        }
    }
}

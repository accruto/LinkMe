using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class ExpiryDateTests
        : EditJobAdTests
    {
        protected override void SetValue(JobAd jobAd)
        {
            jobAd.ExpiryTime = DateTime.Now.Date.AddDays(20);
        }

        protected override void SetDisplayValue()
        {
            _expiryTimeTextBox.Text = DateTime.Now.Date.AddDays(10).ToString("d");
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(DateTime.Now.Date.AddDays(10).AddDays(1).AddSeconds(-1), jobAd.ExpiryTime);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _expiryTimeTextBox.Text = DateTime.Now.Date.AddDays(40).ToString("d"), "The job ad expiry date cannot be later than 14 days from now.");
        }
    }
}

using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class ExpiryDateTests
        : CreateJobAdTests
    {
        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual(DateTime.Now.Date.AddDays(14).ToString("d"), _expiryTimeTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsNull(jobAd.ExpiryTime);
        }

        protected override void SetDisplayValue()
        {
            _expiryTimeTextBox.Text = DateTime.Now.Date.AddDays(11).ToString("d");
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(DateTime.Now.Date.AddDays(11).AddDays(1).AddSeconds(-1), jobAd.ExpiryTime);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _expiryTimeTextBox.Text = DateTime.Now.Date.AddDays(40).ToString("d"), "The job ad expiry date cannot be later than 14 days from now.");
        }
    }
}

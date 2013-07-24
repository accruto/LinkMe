using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class HideCompanyTests
        : CreateJobAdTests
    {
        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.IsFalse(_hideCompanyCheckBox.IsChecked);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsFalse(jobAd.Visibility.HideCompany);
        }

        protected override void SetDisplayValue()
        {
            _hideCompanyCheckBox.IsChecked = true;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.IsTrue(jobAd.Visibility.HideCompany);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
        }
    }
}

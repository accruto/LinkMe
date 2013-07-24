using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class HideContactDetailsTests
        : CreateJobAdTests
    {
        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.IsFalse(_hideContactDetailsCheckBox.IsChecked);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsFalse(jobAd.Visibility.HideContactDetails);
        }

        protected override void SetDisplayValue()
        {
            _hideContactDetailsCheckBox.IsChecked = true;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.IsTrue(jobAd.Visibility.HideContactDetails);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
        }
    }
}

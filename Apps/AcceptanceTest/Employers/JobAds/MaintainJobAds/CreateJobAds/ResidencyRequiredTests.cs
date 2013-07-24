using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class ResidencyRequiredTests
        : CreateJobAdTests
    {
        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.IsTrue(_residencyRequiredCheckBox.IsChecked);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsTrue(jobAd.Description.ResidencyRequired);
        }

        protected override void SetDisplayValue()
        {
            _residencyRequiredCheckBox.IsChecked = false;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.IsFalse(jobAd.Description.ResidencyRequired);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
        }
    }
}

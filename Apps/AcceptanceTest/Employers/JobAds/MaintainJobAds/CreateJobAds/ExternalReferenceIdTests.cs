using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class ExternalReferenceIdTests
        : CreateJobAdTests
    {
        private const string ExternalReferenceId = "REF/001";

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _externalReferenceIdTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsNull(jobAd.Integration.ExternalReferenceId);
        }

        protected override void SetDisplayValue()
        {
            _externalReferenceIdTextBox.Text = ExternalReferenceId;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ExternalReferenceId, jobAd.Integration.ExternalReferenceId);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _externalReferenceIdTextBox.Text = new string('a', 75), "The external reference id must be no more than 50 characters in length.");
        }
    }
}

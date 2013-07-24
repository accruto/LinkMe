using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class ExternalReferenceIdTests
        : EditJobAdTests
    {
        private const string ExternalReferenceId = "REF/001";
        private const string ChangedExternalReferenceId = "REF/001";

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Integration.ExternalReferenceId = ExternalReferenceId;
        }

        protected override void SetDisplayValue()
        {
            _externalReferenceIdTextBox.Text = ChangedExternalReferenceId;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedExternalReferenceId, jobAd.Integration.ExternalReferenceId);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _externalReferenceIdTextBox.Text = new string('a', 75), "The external reference id must be no more than 50 characters in length.");
        }
    }
}

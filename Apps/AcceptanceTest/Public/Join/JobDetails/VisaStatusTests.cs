using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class VisaStatusTests
        : FieldsTests
    {
        protected override void AssertManualDefault()
        {
            AssertDefault();
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            AssertDefault();
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.AreNotEqual(candidate.VisaStatus, VisaStatus.RestrictedWorkVisa);
            candidate.VisaStatus = VisaStatus.RestrictedWorkVisa;
        }

        private void AssertDefault()
        {
            Assert.IsFalse(_notApplicableRadioButton.IsChecked);
            Assert.IsFalse(_citizenRadioButton.IsChecked);
            Assert.IsFalse(_unrestrictedWorkVisaRadioButton.IsChecked);
            Assert.IsFalse(_restrictedWorkVisaRadioButton.IsChecked);
            Assert.IsFalse(_noWorkVisaRadioButton.IsChecked);
        }
    }
}

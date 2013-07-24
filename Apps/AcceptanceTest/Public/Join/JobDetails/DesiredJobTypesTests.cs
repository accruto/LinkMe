using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class DesiredJobTypesTests
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
            Assert.AreNotEqual(candidate.DesiredJobTypes, JobTypes.FullTime | JobTypes.Temp);
            candidate.DesiredJobTypes = JobTypes.FullTime | JobTypes.Temp;
        }

        private void AssertDefault()
        {
            Assert.IsFalse(_fullTimeCheckBox.IsChecked);
            Assert.IsFalse(_partTimeCheckBox.IsChecked);
            Assert.IsFalse(_contractCheckBox.IsChecked);
            Assert.IsFalse(_tempCheckBox.IsChecked);
            Assert.IsFalse(_jobShareCheckBox.IsChecked);
        }
    }
}

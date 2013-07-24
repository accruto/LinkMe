using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class GenderTests
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
            member.Gender = Gender.Unspecified;
            TestErrors(instanceId, member, candidate, resume, "The gender is required.");
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.AreNotEqual(member.Gender, Gender.Male);
            member.Gender = Gender.Male;
        }

        private void AssertDefault()
        {
            Assert.IsFalse(_maleRadioButton.IsChecked);
            Assert.IsFalse(_femaleRadioButton.IsChecked);
        }
    }
}

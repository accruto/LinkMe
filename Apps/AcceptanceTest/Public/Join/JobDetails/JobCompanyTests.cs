using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class JobCompanyTests
        : FieldsTests
    {
        private const string NewJobCompany = "Acme";

        protected override void AssertManualDefault()
        {
            Assert.AreEqual(string.Empty, _jobCompanyTextBox.Text);
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            Assert.IsFalse(_jobCompanyTextBox.IsVisible);
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            if (resumeUploaded)
                return;

            if (resume.Jobs != null && resume.Jobs.Count == 1)
                Assert.AreNotEqual(resume.Jobs[0].Company, NewJobCompany);
            resume.Jobs = new[] { new Job { Company = NewJobCompany } };
        }
    }
}

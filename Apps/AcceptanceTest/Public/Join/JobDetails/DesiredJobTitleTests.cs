using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class DesiredJobTitleTests
        : FieldsTests
    {
        private const string NewDesiredJobTitle = "Archeologist";

        protected override void AssertManualDefault()
        {
            Assert.AreEqual(string.Empty, _desiredJobTitleTextBox.Text);
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            Assert.AreEqual(string.Empty, _desiredJobTitleTextBox.Text);
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.AreNotEqual(candidate.DesiredJobTitle, NewDesiredJobTitle);
            candidate.DesiredJobTitle = NewDesiredJobTitle;
        }
    }
}

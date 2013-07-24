using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class RelocationPreferenceTests
        : FieldsTests
    {
        protected override void AssertManualDefault()
        {
            AssertDefaults();
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            AssertDefaults();
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.AreNotEqual(candidate.RelocationPreference, RelocationPreference.WouldConsider);
            candidate.RelocationPreference = RelocationPreference.WouldConsider;
        }

        private void AssertDefaults()
        {
            Assert.IsTrue(_relocationPreferenceNoRadioButton.IsChecked);
            Assert.IsFalse(_relocationPreferenceYesRadioButton.IsChecked);
            Assert.IsFalse(_relocationPreferenceWouldConsiderRadioButton.IsChecked);
        }
    }
}

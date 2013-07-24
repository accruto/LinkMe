using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class CitizenshipTests
        : FieldsTests
    {
        private const string NewCitizenship = "Dutch";
        private const string NewCitizenship2 = "Polish";

        protected override void AssertManualDefault()
        {
            Assert.AreEqual(string.Empty, _citizenshipTextBox.Text);
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            Assert.AreEqual(resume.Citizenship, _citizenshipTextBox.Text);
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.AreNotEqual(resume.Citizenship, NewCitizenship);
            resume.Citizenship = NewCitizenship;
        }

        [TestMethod]
        public void TestResume()
        {
            // On the server side the resume is worked in response to whether it is empty or not so explicitly test that.

            // Get to job details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Creates the resume.

            UpdateMember(member, Gender, DateOfBirth);
            var resume = CreateResume();
            resume.Citizenship = NewCitizenship;
            SubmitJobDetails(instanceId, member, candidate, resume, true, null, false);
            AssertMember(member, candidate, resume, null, true, null);

            // Updates the resume.

            Get(GetJobDetailsUrl(instanceId));
            resume.Citizenship = NewCitizenship2;
            SubmitJobDetails(instanceId, member, candidate, resume, true, null, false);
            AssertMember(member, candidate, resume, null, true, null);

            // Deletes the resume.

            Get(GetJobDetailsUrl(instanceId));
            resume.Citizenship = null;
            SubmitJobDetails(instanceId, member, candidate, resume, true, null, false);
            AssertMember(member, candidate, resume, null, true, null);
        }
    }
}

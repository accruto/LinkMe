using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    public abstract class FieldsTests
        : JoinTests
    {
        [TestMethod]
        public void TestManualDefault()
        {
            // Get to personal details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            // Get to job details page.

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Assert.

            AssertUrl(GetJobDetailsUrl(instanceId));
            AssertManualDefault();
        }

        [TestMethod]
        public void TestUploadDefault()
        {
            var testResume = TestResume.Complete;
            var parsedResume = testResume.GetParsedResume();

            // Get to personal details page by uploading resume.

            var member = CreateMember(parsedResume.FirstName, parsedResume.LastName, parsedResume.EmailAddresses[0].Address);
            UpdateMember(member, parsedResume.PhoneNumbers[0].Number, parsedResume.PhoneNumbers[0].Type, parsedResume.Address.Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            UploadResume(testResume);
            var instanceId = GetInstanceId();

            UpdateMember(member, Gender, parsedResume.DateOfBirth);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Assert.

            AssertUrl(GetJobDetailsUrl(instanceId));
            AssertUploadDefault(member, parsedResume.Resume);
        }

        [TestMethod]
        public void TestErrors()
        {
            // Get to job details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            var resume = CreateResume();
            SubmitPersonalDetails(instanceId, member, candidate, Password);
            UpdateMember(member, Gender, DateOfBirth);

            // Test.

            TestErrors(instanceId, member, candidate, resume);
        }

        [TestMethod]
        public void TestManual()
        {
            // Get to job details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Change the value from the default to ensure it is saved.

            UpdateMember(member, Gender, DateOfBirth);
            var resume = CreateResume();
            var sendSuggestedJobs = true;
            int? referralSourceId = null;
            Update(member, candidate, resume, ref sendSuggestedJobs, ref referralSourceId, false);

            // Submit.

            SubmitJobDetails(instanceId, member, candidate, resume, sendSuggestedJobs, referralSourceId, false);

            // Assert.

            AssertMember(member, candidate, resume, null, sendSuggestedJobs, referralSourceId);
            Get(GetJobDetailsUrl(instanceId));

            AssertJobDetails(instanceId, member, candidate, resume, sendSuggestedJobs, referralSourceId, false);
        }

        [TestMethod]
        public void TestUpload()
        {
            var testResume = TestResume.Complete;
            var parsedResume = testResume.GetParsedResume();

            // Get to job details page by uploading resume.

            var member = CreateMember(parsedResume.FirstName, parsedResume.LastName, parsedResume.EmailAddresses[0].Address);
            UpdateMember(member, parsedResume.PhoneNumbers[0].Number, parsedResume.PhoneNumbers[0].Type, parsedResume.Address.Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            var fileReferenceId = UploadResume(testResume);
            var instanceId = GetInstanceId();

            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Change the value from the default to ensure it is saved.

            UpdateMember(member, Gender, parsedResume.DateOfBirth);
            var resume = parsedResume.Resume;
            var sendSuggestedJobs = true;
            int? referralSourceId = null;
            Update(member, candidate, resume, ref sendSuggestedJobs, ref referralSourceId, true);

            // Submit.

            SubmitJobDetails(instanceId, member, candidate, resume, sendSuggestedJobs, referralSourceId, true);

            // Assert.

            AssertMember(member, candidate, resume, fileReferenceId, sendSuggestedJobs, referralSourceId);
            Get(GetJobDetailsUrl(instanceId));
            AssertJobDetails(instanceId, member, candidate, resume, sendSuggestedJobs, referralSourceId, true);
        }

        [TestMethod]
        public void TestUpdate()
        {
            // Get to job details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Submit.

            var resume = CreateResume();
            SubmitJobDetails(instanceId, member, candidate, resume, true, null, false);

            // Go back and update.

            Get(GetJobDetailsUrl(instanceId));
            AssertJobDetails(instanceId, member, candidate, resume, true, null, false);

            // Check errors are still checked.

            UpdateMember(member, Gender, DateOfBirth);
            TestErrors(instanceId, member, candidate, resume);

            // Make a change.

            UpdateMember(member, Gender, DateOfBirth);
            var sendSuggestedJobs = true;
            int? referralSourceId = null;
            Update(member, candidate, resume, ref sendSuggestedJobs, ref referralSourceId, false);
            SubmitJobDetails(instanceId, member, candidate, resume, sendSuggestedJobs, referralSourceId, false);
            AssertMember(member, candidate, resume, null, sendSuggestedJobs, referralSourceId);

            // Go back and check.

            Get(GetJobDetailsUrl(instanceId));
            AssertJobDetails(instanceId, member, candidate, resume, sendSuggestedJobs, referralSourceId, false);

            AssertUpdate(false);
        }

        protected abstract void AssertManualDefault();
        protected abstract void AssertUploadDefault(IMember member, IResume resume);
        protected abstract void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume);
        protected abstract void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded);

        protected virtual void AssertUpdate(bool alreadyJoined)
        {
        }

        protected void TestErrors(Guid instanceId, IMember member, ICandidate candidate, IResume resume, params string[] expectedErrorMessages)
        {
            EnterJobDetails(instanceId, member, candidate, resume, true, null, false);
            Browser.Submit(_jobDetailsFormId);
            AssertUrl(GetJobDetailsUrl(instanceId));

            AssertErrorMessages(expectedErrorMessages);
            AssertJobDetails(instanceId, member, candidate, resume, true, null, false);
        }
    }
}
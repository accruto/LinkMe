using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    public abstract class FieldsTests
        : JoinTests
    {
        [TestMethod]
        public void TestManualDefault()
        {
            // Get to the personal details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            // Assert.

            AssertUrl(GetPersonalDetailsUrl(instanceId));
            AssertManualDefault();
        }

        [TestMethod]
        public void TestUploadDefault()
        {
            var testResume = TestResume.Complete;
            var parsedResume = testResume.GetParsedResume();

            // Get to the personal details page by uploading a resume.

            var member = CreateMember(parsedResume.FirstName, parsedResume.LastName, parsedResume.EmailAddresses[0].Address);
            UpdateMember(member, parsedResume.PhoneNumbers[0].Number, parsedResume.PhoneNumbers[0].Type, parsedResume.Address.Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            UploadResume(testResume);
            var instanceId = GetInstanceId();

            // Assert.

            AssertUrl(GetPersonalDetailsUrl(instanceId));
            AssertUploadDefault(member);
        }

        [TestMethod]
        public void TestManualJoinedDefault()
        {
            // Join.

            var member = CreateMember(FirstName, LastName, EmailAddress);
            HomeJoin(member, Password);

            // Get to the personal details page.

            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            // Assert.

            AssertUrl(GetPersonalDetailsUrl(instanceId));
            AssertManualJoinedDefault(member);
        }

        [TestMethod]
        public void TestUploadJoinedDefault()
        {
            var testResume = TestResume.Complete;
            var parsedResume = testResume.GetParsedResume();

            // Join.

            var member = CreateMember(FirstName, LastName, EmailAddress);
            HomeJoin(member, Password);

            // Get to the personal details page by uploading a resume.

            UpdateMember(member, parsedResume.PhoneNumbers[0].Number, parsedResume.PhoneNumbers[0].Type, parsedResume.Address.Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            UploadResume(testResume);
            var instanceId = GetInstanceId();

            // Assert.

            AssertUrl(GetPersonalDetailsUrl(instanceId));
            AssertUploadJoinedDefault(member);
        }

        [TestMethod]
        public void TestErrors()
        {
            // Get to the personal details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            // Test.

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            TestErrors(instanceId, member, candidate, false);
        }

        [TestMethod]
        public void TestManual()
        {
            // Get to the personal details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            // Change the value from the default to ensure it is saved.

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            var resume = CreateResume();
            var password = Password;
            Update(member, candidate, ref password, false);

            // Submit.

            SubmitPersonalDetails(instanceId, member, candidate, password, password, true);

            // Assert.

            AssertMember(member, candidate, resume, null, true, null);
            Get(GetPersonalDetailsUrl(instanceId));
            AssertPersonalDetails(instanceId, member, candidate);
        }

        [TestMethod]
        public void TestManualJoined()
        {
            // Join.

            var member = CreateMember(FirstName, LastName, EmailAddress);
            HomeJoin(member, Password);

            // Get to the personal details page.

            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            // Change the value from the default to ensure it is saved.

            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            var resume = CreateResume();
            var password = Password;
            Update(member, candidate, ref password, false);

            // Submit.

            SubmitPersonalDetails(instanceId, member, candidate);

            // Assert.

            AssertMember(member, candidate, resume, null, true, null);
            Get(GetPersonalDetailsUrl(instanceId));
            AssertPersonalDetails(instanceId, member, candidate);
        }

        [TestMethod]
        public void TestUpload()
        {
            var testResume = TestResume.Complete;
            var parsedResume = testResume.GetParsedResume();

            // Get to the personal details page by uploading a resume.

            var fileReferenceId = UploadResume(testResume);
            var instanceId = GetInstanceId();

            // Change the value from the default to ensure it is saved.

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, parsedResume.PhoneNumbers[0].Number, parsedResume.PhoneNumbers[0].Type, parsedResume.Address.Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            var password = Password;
            Update(member, candidate, ref password, true);

            // Submit.

            SubmitPersonalDetails(instanceId, member, candidate, password, password, true);

            // Assert.

            AssertMember(member, candidate, parsedResume.Resume, fileReferenceId, true, null);
            Get(GetPersonalDetailsUrl(instanceId));
            AssertPersonalDetails(instanceId, member, candidate);
        }

        [TestMethod]
        public void TestUploadJoined()
        {
            var testResume = TestResume.Complete;
            var parsedResume = testResume.GetParsedResume();

            // Join.

            var member = CreateMember(FirstName, LastName, EmailAddress);
            HomeJoin(member, Password);

            // Get to the personal details page by uploading a resume.

            UpdateMember(member, parsedResume.PhoneNumbers[0].Number, parsedResume.PhoneNumbers[0].Type, parsedResume.Address.Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            var fileReferenceId = UploadResume(testResume);
            var instanceId = GetInstanceId();

            // Change the value from the default to ensure it is saved.

            var password = Password;
            Update(member, candidate, ref password, true);

            // Submit.

            SubmitPersonalDetails(instanceId, member, candidate);

            // Assert.

            AssertMember(member, candidate, parsedResume.Resume, fileReferenceId, true, null);
            Get(GetPersonalDetailsUrl(instanceId));
            AssertPersonalDetails(instanceId, member, candidate);
        }

        [TestMethod]
        public void TestUpdate()
        {
            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            // Submit the default values.

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            var resume = CreateResume();

            SubmitPersonalDetails(instanceId, member, candidate, Password, Password, true);

            // Go back and update.

            Get(GetPersonalDetailsUrl(instanceId));
            AssertPersonalDetails(instanceId, member, candidate);

            // Check errors are still checked.

            var password = Password;
            TestErrors(instanceId, member, candidate, true);

            // Make a change.

            Update(member, candidate, ref password, false);
            SubmitPersonalDetails(instanceId, member, candidate);
            AssertMember(member, candidate, resume, null, true, null);

            // Go back and check.

            Get(GetPersonalDetailsUrl(instanceId));
            AssertPersonalDetails(instanceId, member, candidate);

            AssertUpdate(false);
        }

        protected abstract void AssertManualDefault();
        protected abstract void AssertManualJoinedDefault(IMember member);
        protected abstract void AssertUploadDefault(IMember member);
        protected abstract void AssertUploadJoinedDefault(IMember member);

        protected abstract void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn);
        protected abstract void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded);

        protected virtual void AssertUpdate(bool alreadyJoined)
        {
        }

        protected void TestErrors(Guid instanceId, IMember member, ICandidate candidate, string password, string confirmPassword, bool acceptTerms, params string[] expectedErrorMessages)
        {
            SubmitPersonalDetails(instanceId, member, candidate, password, confirmPassword, acceptTerms);
            AssertUrl(GetPersonalDetailsUrl(instanceId));

            AssertErrorMessages(expectedErrorMessages);
            AssertPersonalDetails(instanceId, member, candidate, password, confirmPassword, acceptTerms);
        }

        protected void TestErrors(Guid instanceId, IMember member, ICandidate candidate, bool alreadyLoggedIn, params string[] expectedErrorMessages)
        {
            if (alreadyLoggedIn)
            {
                SubmitPersonalDetails(instanceId, member, candidate);
                AssertUrl(GetPersonalDetailsUrl(instanceId));

                AssertErrorMessages(expectedErrorMessages);
                AssertPersonalDetails(instanceId, member, candidate);
            }
            else
            {
                TestErrors(instanceId, member, candidate, Password, Password, true, expectedErrorMessages);
            }
        }
    }
}
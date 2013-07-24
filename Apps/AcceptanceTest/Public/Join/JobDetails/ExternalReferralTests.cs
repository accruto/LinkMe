using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class ExternalReferralTests
        : FieldsTests
    {
        private const int NewSourceId1 = 8; // Radio.
        private const int NewSourceId2 = 6; // Newspaper.

        protected override void AssertManualDefault()
        {
            Assert.AreEqual(string.Empty, _externalReferralSourceIdDropDownListTester.SelectedItem.Value);
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            Assert.AreEqual(string.Empty, _externalReferralSourceIdDropDownListTester.SelectedItem.Value);
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.IsNull(referralSourceId);
            referralSourceId = NewSourceId1;
        }

        [TestMethod]
        public void TestUpdates()
        {
            // On the back end the various updates result in creation/updation/deletion of referrals so test all them.

            // Get to job details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Creates the referral.

            UpdateMember(member, Gender, DateOfBirth);
            int? referralSourceId = NewSourceId1;
            var resume = CreateResume();
            SubmitJobDetails(instanceId, member, candidate, resume, true, referralSourceId, false);
            AssertMember(member, candidate, resume, null, true, referralSourceId);

            // Updates the referral.

            Get(GetJobDetailsUrl(instanceId));
            referralSourceId = NewSourceId2;
            SubmitJobDetails(instanceId, member, candidate, resume, true, referralSourceId, false);
            AssertMember(member, candidate, resume, null, true, referralSourceId);

            // Deletes the referral.

            Get(GetJobDetailsUrl(instanceId));
            referralSourceId = null;
            SubmitJobDetails(instanceId, member, candidate, resume, true, referralSourceId, false);
            AssertMember(member, candidate, resume, null, true, referralSourceId);
        }
    }
}

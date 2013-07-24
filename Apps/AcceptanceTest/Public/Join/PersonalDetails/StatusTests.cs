using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class StatusTests
        : FieldsTests
    {
        protected override void AssertManualDefault()
        {
            AssertDefault();
        }

        protected override void AssertManualJoinedDefault(IMember member)
        {
            AssertDefault();
        }

        protected override void AssertUploadDefault(IMember member)
        {
            AssertDefault();
        }

        protected override void AssertUploadJoinedDefault(IMember member)
        {
            AssertDefault();
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn)
        {
            // If alreayd logged in then there is actually no way to uncheck all options so no way to test the error.

            if (!alreadyLoggedIn)
            {
                candidate.Status = CandidateStatus.Unspecified;
                TestErrors(instanceId, member, candidate, alreadyLoggedIn, "Please indicate your availability.");
            }
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            Assert.AreNotEqual(candidate.Status, CandidateStatus.NotLooking);
            candidate.Status = CandidateStatus.NotLooking;
        }

        private void AssertDefault()
        {
            Assert.IsFalse(_notLookingRadioButton.IsChecked);
            Assert.IsFalse(_openToOffersRadioButton.IsChecked);
            Assert.IsFalse(_activelyLookingRadioButton.IsChecked);
            Assert.IsFalse(_availableNowRadioButton.IsChecked);
        }
    }
}
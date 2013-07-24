using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class AcceptTermsTests
        : FieldsTests
    {
        protected override void AssertManualDefault()
        {
            Assert.IsFalse(_acceptTermsCheckBox.IsChecked);
        }

        protected override void AssertManualJoinedDefault(IMember member)
        {
            // Already supplied so should not be shown.

            Assert.IsFalse(_acceptTermsCheckBox.IsVisible);
        }

        protected override void AssertUploadDefault(IMember member)
        {
            Assert.IsFalse(_acceptTermsCheckBox.IsChecked);
        }

        protected override void AssertUploadJoinedDefault(IMember member)
        {
            // Already supplied so should not be shown.

            Assert.IsFalse(_acceptTermsCheckBox.IsVisible);
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn)
        {
            if (!alreadyLoggedIn)
                TestErrors(instanceId, member, candidate, Password, Password, false, "Please accept the terms and conditions.");
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
        }
    }
}
using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class PasswordTests
        : FieldsTests
    {
        private const string NewPassword = "abcdef";

        protected override void AssertManualDefault()
        {
            Assert.AreEqual(string.Empty, _passwordTextBox.Text);
            Assert.AreEqual(string.Empty, _confirmPasswordTextBox.Text);
        }

        protected override void AssertManualJoinedDefault(IMember member)
        {
            // Already supplied so should not be shown.

            Assert.IsFalse(_passwordTextBox.IsVisible);
            Assert.IsFalse(_confirmPasswordTextBox.IsVisible);
        }

        protected override void AssertUploadDefault(IMember member)
        {
            Assert.AreEqual(string.Empty, _passwordTextBox.Text);
            Assert.AreEqual(string.Empty, _confirmPasswordTextBox.Text);
        }

        protected override void AssertUploadJoinedDefault(IMember member)
        {
            // Already supplied so should not be shown.

            Assert.IsFalse(_passwordTextBox.IsVisible);
            Assert.IsFalse(_confirmPasswordTextBox.IsVisible);
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn)
        {
            if (!alreadyLoggedIn)
            {
                TestErrors(instanceId, member, candidate, string.Empty, Password, true, "Please provide a password.", "The confirm password and password must match.");
                TestErrors(instanceId, member, candidate, Password, string.Empty, true, "Please provide a confirm password.", "The confirm password and password must match.");
                TestErrors(instanceId, member, candidate, Password, NewPassword, true, "The confirm password and password must match.");
                TestErrors(instanceId, member, candidate, new string('a', 4), new string('a', 4), true, "The password must be between 6 and 50 characters in length.");
                TestErrors(instanceId, member, candidate, new string('a', 400), new string('a', 400), true, "The password must be between 6 and 50 characters in length.");
            }
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            password = NewPassword;
        }
    }
}
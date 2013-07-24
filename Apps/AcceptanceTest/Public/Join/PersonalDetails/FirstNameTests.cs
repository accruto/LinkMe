using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class FirstNameTests
        : FieldsTests
    {
        private const string NewFirstName = "Barney";

        protected override void AssertManualDefault()
        {
            AssertDefault(string.Empty);
        }

        protected override void AssertManualJoinedDefault(IMember member)
        {
            AssertDefault(member.FirstName);
        }

        protected override void AssertUploadDefault(IMember member)
        {
            AssertDefault(member.FirstName);
        }

        protected override void AssertUploadJoinedDefault(IMember member)
        {
            AssertDefault(member.FirstName);
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn)
        {
            member.FirstName = string.Empty;
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "Please provide a first name.");
            member.FirstName = new string('a', 1);
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
            member.FirstName = new string('a', 100);
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            Assert.AreNotEqual(member.FirstName, NewFirstName);
            member.FirstName = NewFirstName;
        }

        private void AssertDefault(string firstName)
        {
            Assert.AreEqual(firstName, _firstNameTextBox.Text);
        }
    }
}
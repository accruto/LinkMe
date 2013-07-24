using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class LastNameTests
        : FieldsTests
    {
        private const string NewLastName = "Gumble";

        protected override void AssertManualDefault()
        {
            AssertDefault(string.Empty);
        }

        protected override void AssertManualJoinedDefault(IMember member)
        {
            AssertDefault(member.LastName);
        }

        protected override void AssertUploadDefault(IMember member)
        {
            AssertDefault(member.LastName);
        }

        protected override void AssertUploadJoinedDefault(IMember member)
        {
            AssertDefault(member.LastName);
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn)
        {
            member.LastName = string.Empty;
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "Please provide a last name.");
            member.LastName = new string('a', 1);
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
            member.LastName = new string('a', 200);
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            Assert.AreNotEqual(member.LastName, NewLastName);
            member.LastName = NewLastName;
        }

        private void AssertDefault(string lastName)
        {
            Assert.AreEqual(lastName, _lastNameTextBox.Text);
        }
    }
}
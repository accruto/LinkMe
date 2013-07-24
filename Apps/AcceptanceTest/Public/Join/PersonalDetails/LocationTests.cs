using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class LocationTests
        : FieldsTests
    {
        private const string NewLocation = "Sydney NSW 2000";

        protected override void AssertManualDefault()
        {
            AssertDefault(string.Empty);
        }

        protected override void AssertManualJoinedDefault(IMember member)
        {
            AssertDefault(string.Empty);
        }

        protected override void AssertUploadDefault(IMember member)
        {
            AssertDefault(member.Address.Location.ToString());
        }

        protected override void AssertUploadJoinedDefault(IMember member)
        {
            AssertDefault(member.Address.Location.ToString());
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn)
        {
            member.Address.Location = _locationQuery.ResolveLocation(member.Address.Location.Country, null);
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "Please provide a location.");
            member.Address.Location = _locationQuery.ResolveLocation(member.Address.Location.Country, "London");
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "The location must be a valid postal location.");
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            Assert.AreNotEqual(member.Address.Location.ToString(), NewLocation);
            member.Address.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), NewLocation);
        }

        private void AssertDefault(string location)
        {
            Assert.AreEqual(location, _locationTextBox.Text);
        }
    }
}
using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class CountryTests
        : FieldsTests
    {
        private const string NewCountry = "New Zealand";

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
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            var country = _locationQuery.GetCountry(NewCountry);
            Assert.AreNotEqual(member.Address.Location.Country.Id, country.Id);
            member.Address.Location = _locationQuery.ResolveLocation(country, member.Address.Location.ToString());
        }

        private void AssertDefault()
        {
            Assert.AreEqual(_locationQuery.GetCountry(Country).Id.ToString(), _countryIdDropDownList.SelectedItem.Value);
        }
    }
}
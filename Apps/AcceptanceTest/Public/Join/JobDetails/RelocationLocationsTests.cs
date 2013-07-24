using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class RelocationLocationsTests
        : FieldsTests
    {
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();

        private const string Country1 = "Malaysia";
        private const string Country2 = "India";
        private const string CountryLocation = "Australia";
        private const string CountrySubdivision1 = "Western Australia";
        private const string CountrySubdivision2 = "New South Wales";
        private const string Region1 = "Melbourne";
        private const string Region2 = "Gold Coast";

        protected override void AssertManualDefault()
        {
            AssertDefault();
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            AssertDefault();
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.AreEqual(0, GetSelectedRelocationCountryIds().Count);
            candidate.RelocationLocations = new List<LocationReference> { _locationQuery.ResolveLocation(_locationQuery.GetCountry(2), null) };
        }

        [TestMethod]
        public void TestDefaultCountry()
        {
            Test(GetCountry(CountryLocation));
        }

        [TestMethod]
        public void TestCountry()
        {
            Test(GetCountry(Country1));
        }

        [TestMethod]
        public void TestMultipleCountries()
        {
            Test(GetCountry(Country1), GetCountry(Country2));
        }

        [TestMethod]
        public void TestCountrySubdivision()
        {
            Test(GetCountrySubdivision(CountryLocation, CountrySubdivision1));
        }

        [TestMethod]
        public void TestMultipleCountrySubdivisions()
        {
            Test(GetCountrySubdivision(CountryLocation, CountrySubdivision1), GetCountrySubdivision(CountryLocation, CountrySubdivision2));
        }

        [TestMethod]
        public void TestRegion()
        {
            Test(GetRegion(CountryLocation, Region1));
        }

        [TestMethod]
        public void TestMultipleRegions()
        {
            Test(GetRegion(CountryLocation, Region1), GetRegion(CountryLocation, Region2));
        }

        [TestMethod]
        public void TestAll()
        {
            Test(
                GetCountry(CountryLocation),
                GetCountry(Country1),
                GetCountry(Country2),
                GetCountrySubdivision(CountryLocation, CountrySubdivision1),
                GetCountrySubdivision(CountryLocation, CountrySubdivision2),
                GetRegion(CountryLocation, Region1),
                GetRegion(CountryLocation, Region2));
        }

        private LocationReference GetRegion(string country, string name)
        {
            return new LocationReference(_locationQuery.GetRegion(_locationQuery.GetCountry(country), name));
        }

        private LocationReference GetCountrySubdivision(string country, string name)
        {
            return new LocationReference(_locationQuery.GetCountrySubdivision(_locationQuery.GetCountry(country), name));
        }

        private LocationReference GetCountry(string name)
        {
            return _locationQuery.ResolveLocation(_locationQuery.GetCountry(name), null);
        }

        private void Test(params LocationReference[] locations)
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

            // Enter the locations.

            UpdateMember(member, Gender, DateOfBirth);
            var resume = CreateResume();
            candidate.RelocationLocations = locations;
            EnterJobDetails(instanceId, member, candidate, resume, true, null, false);
            Browser.Submit(_jobDetailsFormId);

            // Check the page.

            Get(GetJobDetailsUrl(instanceId));
            AssertJobDetails(instanceId, member, candidate, resume, true, null, false);

            // Check what is saved.

            AssertLocations(EmailAddress, locations);
        }

        private void AssertLocations(string emailAddress, ICollection<LocationReference> expectedLocations)
        {
            var candidate = _candidatesQuery.GetCandidate(_loginCredentialsQuery.GetUserId(emailAddress).Value);
            Assert.AreEqual(expectedLocations.Count, candidate.RelocationLocations.Count);
            foreach (var expectedLocation in expectedLocations)
                Assert.IsTrue((from l in candidate.RelocationLocations where Equals(l, expectedLocation) select l).Any());
        }

        private void AssertDefault()
        {
            Assert.AreEqual(0, GetSelectedRelocationCountryIds().Count);
            Assert.AreEqual(0, GetSelectedRelocationCountryLocationIds().Count);
        }
    }
}

using System.Collections.Specialized;
using System.Linq;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class DesiredJobTests
        : ApiTests
    {
        private ReadOnlyUrl _desiredJobUrl;

        private const string NewDesiredJobTitle = "My new desired job title";
        private const string UpdatedDesiredJobTitle = "My updated desired job title";
        private static readonly Salary NewDesiredSalary = new Salary {LowerBound = 50000, Rate = SalaryRate.Year, Currency = Currency.AUD};
        private static readonly Salary UpdatedDesiredSalary = new Salary { LowerBound = 75, Rate = SalaryRate.Hour, Currency = Currency.AUD };
        private const string Country1 = "Malaysia";
        private const string Country2 = "India";
        private const string CountryLocation = "Australia";
        private const string CountrySubdivision1 = "Western Australia";
        private const string CountrySubdivision2 = "New South Wales";
        private const string Region1 = "Melbourne";
        private const string Region2 = "Gold Coast";

        [TestInitialize]
        public void TestInitialize()
        {
            _desiredJobUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/desiredjob");
        }

        [TestMethod]
        public void TestRequired()
        {
            var member = CreateMember();
            LogIn(member);

            var model = DesiredJob(new NameValueCollection());
            AssertJsonError(model, "DesiredSalaryLowerBound", "The salary is required.");
        }

        [TestMethod]
        public void TestDesiredJobTitle()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            candidate.DesiredSalary = NewDesiredSalary;
            candidate.DesiredJobTitle = NewDesiredJobTitle;
            var parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // Change it.

            candidate.DesiredJobTitle = UpdatedDesiredJobTitle;
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // Reset it.

            candidate.DesiredJobTitle = null;
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);
        }

        [TestMethod]
        public void TestDesiredJobTypes()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            candidate.DesiredSalary = NewDesiredSalary;
            candidate.DesiredJobTypes = JobTypes.JobShare | JobTypes.PartTime;
            var parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // Change it.

            candidate.DesiredJobTypes = JobTypes.JobShare | JobTypes.FullTime | JobTypes.Contract;
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // Reset it.

            candidate.DesiredJobTitle = null;
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);
        }

        [TestMethod]
        public void TestCandidateStatus()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            candidate.DesiredSalary = NewDesiredSalary;
            candidate.Status = CandidateStatus.AvailableNow;
            var parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // Change it.

            candidate.Status = CandidateStatus.NotLooking;
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);
        }

        [TestMethod]
        public void TestDesiredSalary()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            candidate.DesiredSalary = NewDesiredSalary;
            var parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // Change it.

            candidate.DesiredSalary = UpdatedDesiredSalary;
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);
        }

        [TestMethod]
        public void TestSalaryVisibility()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Reset it.

            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Salary);
            candidate.DesiredSalary = NewDesiredSalary;
            var parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // Set it.

            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Salary);
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);
        }

        [TestMethod]
        public void TestSendSuggestedJobs()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            candidate.DesiredSalary = NewDesiredSalary;
            candidate.DesiredJobTitle = NewDesiredJobTitle;
            var parameters = GetParameters(member, candidate, true);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, true);

            // Reset it.

            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Salary);
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);
        }

        [TestMethod]
        public void TestRelocationPreference()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Change it.

            candidate.DesiredSalary = NewDesiredSalary;
            candidate.RelocationPreference = RelocationPreference.Yes;
            var parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // Change it again.

            candidate.RelocationPreference = RelocationPreference.WouldConsider;
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);
        }

        [TestMethod]
        public void TestRelocationLocations()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // 1 country.

            candidate.DesiredSalary = NewDesiredSalary;
            candidate.RelocationLocations = new[] { GetCountry(Country1) };
            var parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // 2 countries.

            candidate.RelocationLocations = new[] { GetCountry(Country1), GetCountry(Country2) };
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // 1 subdivision.

            candidate.RelocationLocations = new[] { GetCountrySubdivision(CountryLocation, CountrySubdivision1) };
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // 2 subdivisions.

            candidate.RelocationLocations = new[] { GetCountrySubdivision(CountryLocation, CountrySubdivision1), GetCountrySubdivision(CountryLocation, CountrySubdivision2) };
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // 1 region.

            candidate.RelocationLocations = new[] { GetRegion(CountryLocation, Region1) };
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // 2 regions.

            candidate.RelocationLocations = new[] { GetRegion(CountryLocation, Region1), GetRegion(CountryLocation, Region2) };
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // All.

            candidate.RelocationLocations = new[] { GetCountry(Country1), GetCountry(Country2), GetCountrySubdivision(CountryLocation, CountrySubdivision1), GetCountrySubdivision(CountryLocation, CountrySubdivision2), GetRegion(CountryLocation, Region1), GetRegion(CountryLocation, Region2) };
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);

            // None.

            candidate.RelocationLocations = null;
            parameters = GetParameters(member, candidate, false);
            AssertJsonSuccess(DesiredJob(parameters));
            AssertMember(member, candidate, null, false);
        }

        private LocationReference GetCountry(string country)
        {
            return _locationQuery.ResolveLocation(_locationQuery.GetCountry(country), null);
        }

        private LocationReference GetCountrySubdivision(string country, string name)
        {
            return new LocationReference(_locationQuery.GetCountrySubdivision(_locationQuery.GetCountry(country), name));
        }

        private LocationReference GetRegion(string country, string name)
        {
            return new LocationReference(_locationQuery.GetRegion(_locationQuery.GetCountry(country), name));
        }

        private static NameValueCollection GetParameters(IMember member, ICandidate candidate, bool sendSuggestedJobs)
        {
            var parameters = new NameValueCollection
            {
                {"DesiredJobTitle", candidate.DesiredJobTitle},
                {"DesiredSalaryLowerBound", candidate.DesiredSalary == null || candidate.DesiredSalary.LowerBound == null ? null : candidate.DesiredSalary.LowerBound.Value.ToString()},
                {"DesiredSalaryRate", candidate.DesiredSalary == null ? null : candidate.DesiredSalary.Rate.ToString()},
                {"FullTime", candidate.DesiredJobTypes.IsFlagSet(JobTypes.FullTime) ? "true" : "false"},
                {"PartTime", candidate.DesiredJobTypes.IsFlagSet(JobTypes.PartTime) ? "true" : "false"},
                {"Contract", candidate.DesiredJobTypes.IsFlagSet(JobTypes.Contract) ? "true" : "false"},
                {"Temp", candidate.DesiredJobTypes.IsFlagSet(JobTypes.Temp) ? "true" : "false"},
                {"JobShare", candidate.DesiredJobTypes.IsFlagSet(JobTypes.JobShare) ? "true" : "false"},
                {"Status", candidate.Status.ToString()},
                {"IsSalaryNotVisible", member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Salary) ? "false" : "true"},
                {"SendSuggestedJobs", sendSuggestedJobs ? "true" : "false"},
                {"RelocationPreference", candidate.RelocationPreference.ToString()},
            };

            if (candidate.RelocationLocations != null && candidate.RelocationLocations.Count > 0)
            {
                foreach (var location in candidate.RelocationLocations.Where(l => l.IsCountry))
                    parameters.Add("RelocationCountryIds", location.Country.Id.ToString());
                foreach (var location in candidate.RelocationLocations.Where(l => !l.IsCountry))
                    parameters.Add("RelocationCountryLocationIds", location.NamedLocation.Id.ToString());
            }

            return parameters;
        }

        private JsonResponseModel DesiredJob(NameValueCollection parameters)
        {
            return Deserialize<JsonResponseModel>(Post(_desiredJobUrl, parameters));
        }

        protected override JsonResponseModel Call()
        {
            return DesiredJob(new NameValueCollection());
        }
    }
}

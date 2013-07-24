using System.Collections.Generic;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Partials
{
    [TestClass]
    public abstract class PartialProfileTests
        : ProfileTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private ReadOnlyUrl _loginUrl;

        private const string Country = "Australia";
        private const string Location = "Norlane VIC 3214";
        private const string PrimaryEmailAddress = "member0@test.linkme.net.au";
        private const string SecondaryEmailAddress = "moe@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string PrimaryPhoneNumber = "88888888";
        private const string SecondaryPhoneNumber = "77777777";
        private const string Password = "password";
        private const string DesiredJobTitle = "Archeologist";
        private const decimal SalaryLowerBound = 50000;
        private const string RelocationLocation = "Sydney";

        [TestInitialize]
        public void PartialProfileTestsInitialize()
        {
            _loginUrl = new ReadOnlyApplicationUrl(true, "~/login");
        }

        [TestMethod]
        public void TestAuthorisation()
        {
            // Not logged in.

            var url = GetPartialUrl();
            Get(url);

            var loginUrl = _loginUrl.AsNonReadOnly();
            loginUrl.QueryString["returnUrl"] = url.PathAndQuery;
            AssertUrl(loginUrl);
        }

        [TestMethod]
        public void TestCompleteMember()
        {
            Member member;
            Candidate candidate;
            Resume resume;
            CreateMember(TestResume.Complete, out member, out candidate, out resume);

            LogIn(member);
            Get(_profileUrl);
            Get(GetPartialUrl());
            AssertMember(member, candidate, resume);
        }

        [TestMethod]
        public void TestMinimalMember()
        {
            Member member;
            Candidate candidate;
            Resume resume;
            CreateMinimalMember(out member, out candidate, out resume);

            LogIn(member);
            Get(_profileUrl);
            Get(GetPartialUrl());
            AssertMember(member, candidate, resume);
        }

        protected abstract ReadOnlyUrl GetPartialUrl();
        protected abstract void AssertMember(IMember member, ICandidate candidate, IResume resume);

        protected override Member CreateMember()
        {
            // Crate a member with everything set.

            var member = new Member
            {
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location) },
                DateOfBirth = new PartialDate(1970, 1),
                EmailAddresses = new[]
                {
                    new EmailAddress { Address = PrimaryEmailAddress },
                    new EmailAddress { Address = SecondaryEmailAddress }
                },
                EthnicStatus = EthnicStatus.Aboriginal,
                FirstName = FirstName,
                Gender = Gender.Male,
                LastName = LastName,
                PhoneNumbers = new[]
                {
                    new PhoneNumber { Number = PrimaryPhoneNumber, Type = PhoneNumberType.Home },
                    new PhoneNumber { Number = SecondaryPhoneNumber, Type = PhoneNumberType.Work }
                },
                VisibilitySettings = new VisibilitySettings(),
                IsActivated = true,
                IsEnabled = true,
            };

            var credentials = new LoginCredentials
            {
                LoginId = PrimaryEmailAddress,
                PasswordHash = LoginCredentials.HashToString(Password)
            };

            _memberAccountsCommand.CreateMember(member, credentials, null);
            return member;
        }

        private Member CreateMinimalMember()
        {
            // Crate a member with everything set.

            var member = new Member
            {
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), null) },
                EmailAddresses = new[]
                {
                    new EmailAddress { Address = PrimaryEmailAddress }
                },
                FirstName = FirstName,
                LastName = LastName,
                VisibilitySettings = new VisibilitySettings(),
                IsActivated = true,
                IsEnabled = true,
            };

            var credentials = new LoginCredentials
            {
                LoginId = PrimaryEmailAddress,
                PasswordHash = LoginCredentials.HashToString(Password)
            };

            _memberAccountsCommand.CreateMember(member, credentials, null);
            return member;
        }

        protected void CreateMember(TestResume testResume, out Member member, out Candidate candidate, out Resume resume)
        {
            // Member.

            member = CreateMember();

            // Candidate.

            candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredJobTitle = DesiredJobTitle;
            candidate.DesiredJobTypes = JobTypes.FullTime | JobTypes.JobShare;
            candidate.DesiredSalary = new Salary { LowerBound = SalaryLowerBound, Rate = SalaryRate.Year, Currency = Currency.AUD };
            candidate.HighestEducationLevel = EducationLevel.Postgraduate;
            var industries = _industriesQuery.GetIndustries();
            candidate.Industries = new List<Industry> { industries[1], industries[2] };
            candidate.RecentProfession = Profession.Science;
            candidate.RecentSeniority = Seniority.MidSenior;
            candidate.RelocationPreference = RelocationPreference.Yes;
            var country = _locationQuery.GetCountry(Country);
            candidate.RelocationLocations = new List<LocationReference> { _locationQuery.ResolveLocation(country, RelocationLocation) };
            candidate.Status = CandidateStatus.AvailableNow;
            candidate.VisaStatus = VisaStatus.RestrictedWorkVisa;

            _candidatesCommand.UpdateCandidate(candidate);

            // Resume.

            resume = testResume.GetParsedResume().Resume;
            _candidateResumesCommand.CreateResume(candidate, resume);
        }

        private void CreateMinimalMember(out Member member, out Candidate candidate, out Resume resume)
        {
            // Member.

            member = CreateMinimalMember();

            // Candidate.

            candidate = _candidatesQuery.GetCandidate(member.Id);

            // Resume.

            resume = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Members.Status;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Models.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class ProfileStatusTests
        : ApiTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string DesiredJobTitle = "Archeologist";
        private const string Country = "Australia";
        private const string Location = "Norlane VIC 3214";
        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string PhoneNumber = "99999999";
        private const string Objective = "My objective";
        private const string Industry = "Accounting";
        private const string JobTitle = "Nuclear Technician";
        private const string Major = "Nuclear Physics";

        // All the APIs return the profile status but just use the visibility API for testing.

        private ReadOnlyUrl _visibilityUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _visibilityUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/visibility");
        }

        [TestMethod]
        public void TestBasicsComplete()
        {
            var member = CreateMember();
            AssertStatus(member, 25, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestDesiredJobComplete()
        {
            var member = CreateMember();
            UpdateCandidate(member.Id, c => c.DesiredJobTitle = DesiredJobTitle);
            AssertStatus(member, 30, MemberItem.DesiredJob, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestDesiredSalaryComplete()
        {
            var member = CreateMember();
            UpdateCandidate(member.Id, c => c.DesiredSalary = new Salary { LowerBound = 100, UpperBound = 200, Rate = SalaryRate.Year });
            AssertStatus(member, 35, MemberItem.DesiredSalary, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestPhoneNumberComplete()
        {
            var member = CreateMember();
            UpdateMember(member, m => m.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = PhoneNumber, Type = PhoneNumberType.Mobile } });
            AssertStatus(member, 35, MemberItem.PhoneNumber, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestObjectiveComplete()
        {
            var member = CreateMember();
            UpdateResume(member.Id, r => r.Objective = Objective);
            AssertStatus(member, 30, MemberItem.Objective, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestIndustriesComplete()
        {
            var member = CreateMember();
            UpdateCandidate(member.Id, c => c.Industries = new List<Industry> { _industriesQuery.GetIndustry(Industry) });
            AssertStatus(member, 30, MemberItem.Industries, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestJobsComplete()
        {
            var member = CreateMember();
            UpdateResume(member.Id, r => r.Jobs = new List<Job> { new Job { Title = JobTitle } });
            AssertStatus(member, 35, MemberItem.Jobs, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestSchoolsComplete()
        {
            var member = CreateMember();
            UpdateResume(member.Id, r => r.Schools = new List<School> { new School { Major = Major } });
            AssertStatus(member, 35, MemberItem.Schools, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestRecentProfessionComplete()
        {
            var member = CreateMember();
            UpdateCandidate(member.Id, c => c.RecentProfession = Profession.Administration);
            AssertStatus(member, 30, MemberItem.RecentProfession, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestRecentSeniorityComplete()
        {
            var member = CreateMember();
            UpdateCandidate(member.Id, c => c.RecentSeniority = Seniority.MidSenior);
            AssertStatus(member, 30, MemberItem.RecentSeniority, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestHighestEducationLevelComplete()
        {
            var member = CreateMember();
            UpdateCandidate(member.Id, c => c.HighestEducationLevel = EducationLevel.Postgraduate);
            AssertStatus(member, 30, MemberItem.HighestEducationLevel, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestVisaStatusComplete()
        {
            var member = CreateMember();
            UpdateCandidate(member.Id, c => c.VisaStatus = VisaStatus.RestrictedWorkVisa);
            AssertStatus(member, 30, MemberItem.VisaStatus, MemberItem.Name, MemberItem.EmailAddress, MemberItem.Address, MemberItem.Status);
        }

        [TestMethod]
        public void TestAllComplete()
        {
            var member = CreateMember();
            member.PhoneNumbers = new List<PhoneNumber> {new PhoneNumber {Number = PhoneNumber, Type = PhoneNumberType.Mobile}};
            _memberAccountsCommand.UpdateMember(member);

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredJobTitle = DesiredJobTitle;
            candidate.DesiredSalary = new Salary {LowerBound = 100, UpperBound = 200, Rate = SalaryRate.Year};
            candidate.Status = CandidateStatus.ActivelyLooking;
            candidate.Industries = new List<Industry> {_industriesQuery.GetIndustry(Industry)};
            candidate.RecentProfession = Profession.Administration;
            candidate.RecentSeniority = Seniority.MidSenior;
            candidate.HighestEducationLevel = EducationLevel.Postgraduate;
            candidate.VisaStatus = VisaStatus.RestrictedWorkVisa;
            _candidatesCommand.UpdateCandidate(candidate);

            var resume = new Resume
            {
                Objective = Objective,
                Jobs = new List<Job> { new Job { Title = JobTitle } },
                Schools = new List<School> { new School { Major = Major } },
            };
            _candidateResumesCommand.CreateResume(candidate, resume);

            AssertStatus(member, 100, MemberItem.Name, MemberItem.DesiredJob, MemberItem.DesiredSalary, MemberItem.Address, MemberItem.EmailAddress, MemberItem.PhoneNumber, MemberItem.Status, MemberItem.Objective, MemberItem.Industries, MemberItem.Jobs, MemberItem.Schools, MemberItem.RecentProfession, MemberItem.RecentSeniority, MemberItem.HighestEducationLevel, MemberItem.VisaStatus);
        }

        private void UpdateResume(Guid memberId, Action<Resume> updateResume)
        {
            var resume = new Resume();
            updateResume(resume);
            _candidateResumesCommand.CreateResume(_candidatesQuery.GetCandidate(memberId), resume);
        }

        private void UpdateCandidate(Guid memberId, Action<Candidate> updateCandidate)
        {
            var candidate = _candidatesQuery.GetCandidate(memberId);
            updateCandidate(candidate);
            _candidatesCommand.UpdateCandidate(candidate);
        }

        private void UpdateMember(Member member, Action<Member> updateMember)
        {
            updateMember(member);
            _memberAccountsCommand.UpdateMember(member);
        }

        private void AssertStatus(IUser member, int expectedPercentComplete, params MemberItem[] expectedCompleteItems)
        {
            LogIn(member);
            var response = Visibility(new NameValueCollection());

            Assert.AreEqual(expectedPercentComplete, response.Profile.PercentComplete);

            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Name), response.Profile.MemberStatus.IsNameComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.DesiredJob), response.Profile.MemberStatus.IsDesiredJobComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.DesiredSalary), response.Profile.MemberStatus.IsDesiredSalaryComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Address), response.Profile.MemberStatus.IsAddressComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.EmailAddress), response.Profile.MemberStatus.IsEmailAddressComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.PhoneNumber), response.Profile.MemberStatus.IsPhoneNumberComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Status), response.Profile.MemberStatus.IsStatusComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Objective), response.Profile.MemberStatus.IsObjectiveComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Industries), response.Profile.MemberStatus.IsIndustriesComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Jobs), response.Profile.MemberStatus.IsJobsComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Schools), response.Profile.MemberStatus.IsSchoolsComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.RecentProfession), response.Profile.MemberStatus.IsRecentProfessionComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.RecentSeniority), response.Profile.MemberStatus.IsRecentSeniorityComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.HighestEducationLevel), response.Profile.MemberStatus.IsHighestEducationLevelComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.VisaStatus), response.Profile.MemberStatus.IsVisaStatusComplete);
        }

        protected override Member CreateMember()
        {
            // These must always be set.

            var member = new Member
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = EmailAddress } },
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location) },
            };
            var credentials = new LoginCredentials
            {
                LoginId = EmailAddress,
                PasswordHash = LoginCredentials.HashToString(member.GetPassword()),
            };
            _memberAccountsCommand.CreateMember(member, credentials, null);
            return member;
        }

        protected override JsonResponseModel Call()
        {
            return Visibility(new NameValueCollection());
        }

        private JsonProfileModel Visibility(NameValueCollection parameters)
        {
            return Deserialize<JsonProfileModel>(Post(_visibilityUrl, parameters));
        }
    }
}

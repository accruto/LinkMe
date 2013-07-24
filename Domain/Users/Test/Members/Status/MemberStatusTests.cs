using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Members.Status;
using LinkMe.Domain.Users.Members.Status.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Status
{
    [TestClass]
    public class MemberStatusTests
        : TestClass
    {
        private readonly IMemberStatusQuery _memberStatusQuery = Resolve<IMemberStatusQuery>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

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

        [TestMethod]
        public void TestNoneComplete()
        {
            var member = new Member();
            var candidate = new Candidate();
            AssertStatus(member, candidate, null, 0);
        }

        [TestMethod]
        public void TestNameComplete()
        {
            var member = new Member { FirstName = FirstName };
            var candidate = new Candidate();
            AssertStatus(member, candidate, null, 0);

            member = new Member { LastName = LastName };
            AssertStatus(member, candidate, null, 0);

            member = new Member { FirstName = FirstName, LastName = LastName };
            AssertStatus(member, candidate, null, 0, MemberItem.Name);
        }

        [TestMethod]
        public void TestDesiredJobComplete()
        {
            var member = new Member();
            var candidate = new Candidate { DesiredJobTitle = DesiredJobTitle };
            AssertStatus(member, candidate, null, 5, MemberItem.DesiredJob);
        }

        [TestMethod]
        public void TestDesiredSalaryComplete()
        {
            var member = new Member();
            var candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 100, UpperBound = 200, Rate = SalaryRate.Year } };
            AssertStatus(member, candidate, null, 10, MemberItem.DesiredSalary);
        }

        [TestMethod]
        public void TestAddressComplete()
        {
            var member = new Member { Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location) } };
            var candidate = new Candidate();
            AssertStatus(member, candidate, null, 10, MemberItem.Address);
        }

        [TestMethod]
        public void TestEmailAddressComplete()
        {
            var member = new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = EmailAddress } } };
            var candidate = new Candidate();
            AssertStatus(member, candidate, null, 5, MemberItem.EmailAddress);
        }

        [TestMethod]
        public void TestPhoneNumberComplete()
        {
            var member = new Member { PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = PhoneNumber, Type = PhoneNumberType.Mobile } } };
            var candidate = new Candidate();
            AssertStatus(member, candidate, null, 10, MemberItem.PhoneNumber);
        }

        [TestMethod]
        public void TestStatusComplete()
        {
            var member = new Member();
            var candidate = new Candidate { Status = CandidateStatus.ActivelyLooking };
            AssertStatus(member, candidate, null, 10, MemberItem.Status);
        }

        [TestMethod]
        public void TestObjectiveComplete()
        {
            var member = new Member();
            var candidate = new Candidate();
            var resume = new Resume { Objective = Objective };
            AssertStatus(member, candidate, resume, 5, MemberItem.Objective);
        }

        [TestMethod]
        public void TestIndustriesComplete()
        {
            var member = new Member();
            var candidate = new Candidate { Industries = new List<Industry> { _industriesQuery.GetIndustry(Industry) } };
            AssertStatus(member, candidate, null, 5, MemberItem.Industries);
        }

        [TestMethod]
        public void TestJobsComplete()
        {
            var member = new Member();
            var candidate = new Candidate();
            var resume = new Resume { Jobs = new List<Job> { new Job { Title = JobTitle } } };
            AssertStatus(member, candidate, resume, 10, MemberItem.Jobs);
        }

        [TestMethod]
        public void TestSchoolsComplete()
        {
            var member = new Member();
            var candidate = new Candidate();
            var resume = new Resume { Schools = new List<School> { new School { Major = Major } } };
            AssertStatus(member, candidate, resume, 10, MemberItem.Schools);
        }

        [TestMethod]
        public void TestRecentProfessionComplete()
        {
            var member = new Member();
            var candidate = new Candidate { RecentProfession = Profession.Administration };
            AssertStatus(member, candidate, null, 5, MemberItem.RecentProfession);
        }

        [TestMethod]
        public void TestRecentSeniorityComplete()
        {
            var member = new Member();
            var candidate = new Candidate { RecentSeniority = Seniority.MidSenior };
            AssertStatus(member, candidate, null, 5, MemberItem.RecentSeniority);
        }

        [TestMethod]
        public void TestHighestEducationLevelComplete()
        {
            var member = new Member();
            var candidate = new Candidate { HighestEducationLevel = EducationLevel.Postgraduate };
            AssertStatus(member, candidate, null, 5, MemberItem.HighestEducationLevel);
        }

        [TestMethod]
        public void TestVisaStatusComplete()
        {
            var member = new Member();
            var candidate = new Candidate { VisaStatus = VisaStatus.RestrictedWorkVisa };
            AssertStatus(member, candidate, null, 5, MemberItem.VisaStatus);
        }

        [TestMethod]
        public void TestAllComplete()
        {
            var member = new Member
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location) },
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = EmailAddress } },
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = PhoneNumber, Type = PhoneNumberType.Mobile } },
            };
            var candidate = new Candidate
            {
                DesiredJobTitle = DesiredJobTitle,
                DesiredSalary = new Salary { LowerBound = 100, UpperBound = 200, Rate = SalaryRate.Year },
                Status = CandidateStatus.ActivelyLooking,
                Industries = new List<Industry> { _industriesQuery.GetIndustry(Industry) },
                RecentProfession = Profession.Administration,
                RecentSeniority = Seniority.MidSenior,
                HighestEducationLevel = EducationLevel.Postgraduate,
                VisaStatus = VisaStatus.RestrictedWorkVisa,
            };
            var resume = new Resume
            {
                Objective = Objective,
                Jobs = new List<Job> { new Job { Title = JobTitle } },
                Schools = new List<School> { new School { Major = Major } },
            };

            AssertStatus(member, candidate, resume, 100, MemberItem.Name, MemberItem.DesiredJob, MemberItem.DesiredSalary, MemberItem.Address, MemberItem.EmailAddress, MemberItem.PhoneNumber, MemberItem.Status, MemberItem.Objective, MemberItem.Industries, MemberItem.Jobs, MemberItem.Schools, MemberItem.RecentProfession, MemberItem.RecentSeniority, MemberItem.HighestEducationLevel, MemberItem.VisaStatus);
        }

        private void AssertStatus(IMember member, ICandidate candidate, IResume resume, int expectedPercentComplete, params MemberItem[] expectedCompleteItems)
        {
            Assert.AreEqual(expectedPercentComplete, _memberStatusQuery.GetPercentComplete(member, candidate, resume));

            var status = _memberStatusQuery.GetMemberStatus(member, candidate, resume);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Name), status.IsNameComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.DesiredJob), status.IsDesiredJobComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.DesiredSalary), status.IsDesiredSalaryComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Address), status.IsAddressComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.EmailAddress), status.IsEmailAddressComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.PhoneNumber), status.IsPhoneNumberComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Status), status.IsStatusComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Objective), status.IsObjectiveComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Industries), status.IsIndustriesComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Jobs), status.IsJobsComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.Schools), status.IsSchoolsComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.RecentProfession), status.IsRecentProfessionComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.RecentSeniority), status.IsRecentSeniorityComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.HighestEducationLevel), status.IsHighestEducationLevelComplete);
            Assert.AreEqual(expectedCompleteItems.Contains(MemberItem.VisaStatus), status.IsVisaStatusComplete);
        }
    }
}

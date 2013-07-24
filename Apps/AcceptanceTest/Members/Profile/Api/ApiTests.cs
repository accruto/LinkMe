using System;
using System.Linq;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    public abstract class ApiTests
        : ProfileTests
    {
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        protected readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        [TestMethod]
        public void TestAuthorisation()
        {
            // Not logged in.

            var model = Call();
            AssertJsonError(model, null, "100", "The user is not logged in.");
        }

        protected abstract JsonResponseModel Call();

        protected override Member CreateMember()
        {
            var member = base.CreateMember();

            // As the visa status is required set that as well.

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.VisaStatus = VisaStatus.RestrictedWorkVisa;
            _candidatesCommand.UpdateCandidate(candidate);

            return member;
        }

        protected void AssertMember(IMember expectedMember, ICandidate expectedCandidate, IResume expectedResume, bool expectedSendSuggestedJobs)
        {
            AssertCredentials(expectedMember);
            AssertMember(expectedMember);

            var candidate = _candidatesQuery.GetCandidate(expectedCandidate.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

            AssertCandidate(expectedCandidate, candidate);
            AssertResume(expectedResume, resume);
            AssertSuggestedJobs(expectedSendSuggestedJobs, expectedMember.Id);
        }

        private void AssertCredentials(IMember expectedMember)
        {
            var credentials = _loginCredentialsQuery.GetCredentials(expectedMember.Id);
            Assert.AreEqual(expectedMember.EmailAddresses[0].Address, credentials.LoginId);
        }

        private static void AssertCandidate(ICandidate expectedCandidate, Candidate candidate)
        {
            Assert.AreEqual(expectedCandidate.DesiredJobTitle, candidate.DesiredJobTitle);
            Assert.AreEqual(expectedCandidate.DesiredJobTypes, candidate.DesiredJobTypes);
            Assert.AreEqual(expectedCandidate.DesiredSalary, candidate.DesiredSalary);
            Assert.AreEqual(expectedCandidate.HighestEducationLevel, candidate.HighestEducationLevel);
            Assert.AreEqual(expectedCandidate.RecentProfession, candidate.RecentProfession);
            Assert.AreEqual(expectedCandidate.RecentSeniority, candidate.RecentSeniority);
            Assert.AreEqual(expectedCandidate.Status, candidate.Status);
            Assert.AreEqual(expectedCandidate.VisaStatus, candidate.VisaStatus);

            if (expectedCandidate.Industries == null)
            {
                Assert.IsNull(candidate.Industries);
            }
            else
            {
                Assert.AreEqual(expectedCandidate.Industries.Count, candidate.Industries.Count);
                foreach (var expectedIndustry in expectedCandidate.Industries)
                {
                    var expectedIndustryId = expectedIndustry.Id;
                    Assert.IsTrue((from i in candidate.Industries where i.Id == expectedIndustryId select i).Any());
                }
            }

            Assert.AreEqual(expectedCandidate.RelocationPreference, candidate.RelocationPreference);
            if (expectedCandidate.RelocationLocations == null)
            {
                Assert.IsNull(candidate.RelocationLocations);
            }
            else
            {
                Assert.AreEqual(expectedCandidate.RelocationLocations.Count, candidate.RelocationLocations.Count);
                foreach (var expectedLocation in expectedCandidate.RelocationLocations)
                {
                    var location = expectedLocation;
                    Assert.IsTrue((from l in candidate.RelocationLocations where Equals(l, location) select l).Any());
                }
            }
        }

        private void AssertMember(IMember expectedMember)
        {
            var member = _membersQuery.GetMember(expectedMember.Id);
            Assert.IsNotNull(member);

            Assert.AreEqual(expectedMember.FirstName, member.FirstName);
            Assert.AreEqual(expectedMember.LastName, member.LastName);
            Assert.AreEqual(expectedMember.AffiliateId, member.AffiliateId);
            Assert.AreEqual(expectedMember.Address.Location.Country.Id, member.Address.Location.Country.Id);
            Assert.AreEqual(expectedMember.Address.Location.ToString(), member.Address.Location.ToString());

            var expectedDateOfBirth = expectedMember.DateOfBirth == null
                ? (PartialDate?)null
                : expectedMember.DateOfBirth.Value.Month == null
                    ? new PartialDate(expectedMember.DateOfBirth.Value.Year)
                    : new PartialDate(expectedMember.DateOfBirth.Value.Year, expectedMember.DateOfBirth.Value.Month.Value);
            var dateOfBirth = member.DateOfBirth == null
                ? (PartialDate?)null
                : member.DateOfBirth.Value.Month == null
                    ? new PartialDate(member.DateOfBirth.Value.Year)
                    : new PartialDate(member.DateOfBirth.Value.Year, member.DateOfBirth.Value.Month.Value);
            Assert.AreEqual(expectedDateOfBirth, dateOfBirth);

            Assert.AreEqual(expectedMember.EthnicStatus, member.EthnicStatus);
            Assert.AreEqual(expectedMember.Gender, member.Gender);
            Assert.AreEqual(expectedMember.IsActivated, member.IsActivated);
            Assert.AreEqual(expectedMember.IsEnabled, member.IsEnabled);
            Assert.AreEqual(expectedMember.PhotoId, member.PhotoId);
            Assert.AreEqual(expectedMember.VisibilitySettings.Personal.FirstDegreeVisibility, member.VisibilitySettings.Personal.FirstDegreeVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Personal.SecondDegreeVisibility, member.VisibilitySettings.Personal.SecondDegreeVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Personal.PublicVisibility, member.VisibilitySettings.Personal.PublicVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Professional.EmploymentVisibility, member.VisibilitySettings.Professional.EmploymentVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Professional.PublicVisibility, member.VisibilitySettings.Professional.PublicVisibility);

            if (expectedMember.EmailAddresses == null)
            {
                Assert.IsNull(member.EmailAddresses);
            }
            else
            {
                Assert.IsNotNull(member.EmailAddresses);
                Assert.AreEqual(expectedMember.EmailAddresses.Count, member.EmailAddresses.Count);
                for (var index = 0; index < expectedMember.EmailAddresses.Count; ++index)
                    Assert.AreEqual(expectedMember.EmailAddresses[index], member.EmailAddresses[index]);
            }

            if (expectedMember.PhoneNumbers == null)
            {
                Assert.IsNull(member.PhoneNumbers);
            }
            else
            {
                Assert.AreEqual(expectedMember.PhoneNumbers.Count, member.PhoneNumbers.Count);
                for (var index = 0; index < expectedMember.PhoneNumbers.Count; ++index)
                {
                    Assert.AreEqual(expectedMember.PhoneNumbers[index].Number, member.PhoneNumbers[index].Number);
                    Assert.AreEqual(expectedMember.PhoneNumbers[index].Type, member.PhoneNumbers[index].Type);
                }
            }
        }

        private static void AssertResume(IResume expectedResume, Resume resume)
        {
            if (expectedResume == null)
            {
                Assert.IsNull(resume);
            }
            else
            {
                Assert.IsNotNull(resume);

                Assert.AreEqual(expectedResume.Skills, resume.Skills);
                Assert.AreEqual(expectedResume.Objective, resume.Objective);
                Assert.AreEqual(expectedResume.Summary, resume.Summary);
                Assert.AreEqual(expectedResume.Other, resume.Other);
                Assert.AreEqual(expectedResume.Citizenship, resume.Citizenship);
                Assert.AreEqual(expectedResume.Affiliations, resume.Affiliations);
                Assert.AreEqual(expectedResume.Professional, resume.Professional);
                Assert.AreEqual(expectedResume.Interests, resume.Interests);
                Assert.AreEqual(expectedResume.Referees, resume.Referees);

                if (expectedResume.Courses == null)
                {
                    Assert.IsNull(resume.Courses);
                }
                else
                {
                    Assert.AreEqual(expectedResume.Courses.Count, resume.Courses.Count);
                    for (var index = 0; index < expectedResume.Courses.Count; ++index)
                        Assert.AreEqual(expectedResume.Courses[index], resume.Courses[index]);
                }

                if (expectedResume.Awards == null)
                {
                    Assert.IsNull(resume.Awards);
                }
                else
                {
                    Assert.AreEqual(expectedResume.Awards.Count, resume.Awards.Count);
                    for (var index = 0; index < expectedResume.Awards.Count; ++index)
                        Assert.AreEqual(expectedResume.Awards[index], resume.Awards[index]);
                }

                if (expectedResume.Schools == null)
                {
                    Assert.IsNull(resume.Schools);
                }
                else
                {
                    Assert.AreEqual(expectedResume.Schools.Count, resume.Schools.Count);
                    for (var index = 0; index < expectedResume.Schools.Count; ++index)
                        Assert.AreEqual(expectedResume.Schools[index], resume.Schools[index]);
                }

                if (expectedResume.Jobs == null)
                {
                    Assert.IsNull(resume.Jobs);
                }
                else
                {
                    Assert.AreEqual(expectedResume.Jobs.Count, resume.Jobs.Count);
                    for (var index = 0; index < expectedResume.Jobs.Count; ++index)
                        Assert.AreEqual(expectedResume.Jobs[index], resume.Jobs[index]);
                }
            }
        }

        private void AssertSuggestedJobs(bool expectedSendSuggestedJobs, Guid memberId)
        {
            var category = _settingsQuery.GetCategory("SuggestedJobs");
            var frequency = Frequency.Never;
            var settings = _settingsQuery.GetSettings(memberId);
            
            if (settings != null)
            {
                var setting = settings.CategorySettings.Where(c => c.CategoryId == category.Id).FirstOrDefault();

                if (setting != null  && setting.Frequency.HasValue)
                    frequency = setting.Frequency.Value;
            }

            Assert.AreEqual(expectedSendSuggestedJobs, frequency != Frequency.Never);
        }
    }
}

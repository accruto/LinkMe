using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Views
{
    [TestClass]
    public class EmployerMemberViewsTests
        : ProfessionalViewsTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        private const CandidateStatus Status = CandidateStatus.OpenToOffers;
        private const string DesiredJobTitle = "My desired job title";
        private const JobTypes DesiredJobTypes = JobTypes.Contract;
        private static readonly Salary DesiredSalary = new Salary {LowerBound = 120000, UpperBound = 150000, Rate = SalaryRate.Year, Currency = Currency.AUD};
        private readonly IList<Industry> _industries;
        private readonly IList<LocationReference> _relocationLocations;
        private static readonly RelocationPreference RelocationPreference = RelocationPreference.Yes;
        private static readonly EducationLevel HighestEducationLevel = EducationLevel.HighSchool;
        private static readonly Seniority RecentSeniority = Seniority.Internship;
        private static readonly Profession RecentProfession = Profession.Strategy;
        private static readonly VisaStatus VisaStatus = VisaStatus.RestrictedWorkVisa;

        private const string Skills = "My skills";
        private const string Objective = "My objective";
        private const string Summary = "My summary";
        private const string Other = "My other";
        private const string Citizenship = "My citizenship";
        private const string Affiliations = "My affiliations";
        private const string Professional = "My professional";
        private const string Interests = "My interests";
        private const string Referees = "My referees";

        private static readonly IList<School> Schools = new[]
        {
            new School
            {
                City = "My city",
                CompletionDate = new PartialCompletionDate(new PartialDate(2010, 12)),
                Country = "My country",
                Degree = "My degree",
                Description = "My description",
                Institution = "My institution",
                Major = "My major",
            }
        };

        private static readonly IList<string> Courses = new[]
        {
            "My course 1",
            "My course 2"
        };

        private static readonly IList<string> Awards = new[]
        {
            "My award 1",
            "My award 2"
        };

        private static readonly IList<Job> Jobs = new[]
        {
            new Job {Company = "My company name", Dates = new PartialDateRange(new PartialDate(2010, 1)), Description = "My description 1", Title = "My title 1"},
            new Job {Company = "My company name", Dates = new PartialDateRange(new PartialDate(2009, 1), new PartialDate(2009, 12)), Description = "My description 2", Title = "My title 2"},
            new Job {Company = "My company name", Dates = new PartialDateRange(new PartialDate(2008, 1), new PartialDate(2008, 12)), Description = "My description 3", Title = "My title 3"}
        };

        public EmployerMemberViewsTests()
        {
            _industries = new List<Industry> {_industriesQuery.GetIndustries()[2]};
            _relocationLocations = new List<LocationReference> {_locationQuery.ResolveLocation(_locationQuery.GetCountry(1), "Melbourne VIC 3000")};
        }

        // Candidate.

        [TestMethod]
        public void TestResumeOnOthersOnCandidate()
        {
            AssertCandidate(true, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertCandidate(true, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertCandidate(true, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffCandidate()
        {
            AssertCandidate(false, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertCandidate(false, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertCandidate(false, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnCandidate()
        {
            AssertCandidate(false, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertCandidate(false, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertCandidate(true, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffCandidate()
        {
            AssertCandidate(false, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertCandidate(false, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertCandidate(false, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        // Resume.

        [TestMethod]
        public void TestResumeOnOthersOnResume()
        {
            AssertResume(true, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertResume(true, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertResume(true, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffResume()
        {
            AssertResume(true, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertResume(true, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertResume(true, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnResume()
        {
            AssertResume(false, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertResume(false, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertResume(true, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffResume()
        {
            AssertResume(false, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertResume(false, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertResume(true, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        // Jobs.

        [TestMethod]
        public void TestResumeOnOthersOnJobs()
        {
            AssertJobs(true, false, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertJobs(true, true, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertJobs(true, true, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffJobs()
        {
            AssertJobs(true, false, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertJobs(true, false, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertJobs(true, true, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnJobs()
        {
            AssertJobs(false, false, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertJobs(false, false, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertJobs(true, true, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffJobs()
        {
            AssertJobs(false, false, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertJobs(false, false, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertJobs(true, true, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        // Referees work differently, and is only determined by whether or not the employer has already paid.

        [TestMethod]
        public void TestResumeOnOthersOnReferees()
        {
            AssertReferees(true, false, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertReferees(true, true, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertReferees(true, true, CreateView(CreateMember(true, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffReferees()
        {
            AssertReferees(true, false, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertReferees(true, true, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertReferees(true, true, CreateView(CreateMember(true, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnReferees()
        {
            AssertReferees(false, false, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertReferees(false, false, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertReferees(true, true, CreateView(CreateMember(false, true), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffReferees()
        {
            AssertReferees(false, false, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.NotContacted, false));
            AssertReferees(false, false, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Contacted, true));
            AssertReferees(true, true, CreateView(CreateMember(false, false), CreateCandidate(), CreateResume(), ProfessionalContactDegree.Applicant, false));
        }

        private void AssertCandidate(bool canAccessSalary, ICandidate candidate)
        {
            // Should be able to access all these no matter what.

            Assert.AreEqual(Status, candidate.Status);
            Assert.AreEqual(DesiredJobTitle, candidate.DesiredJobTitle);
            Assert.AreEqual(DesiredJobTypes, candidate.DesiredJobTypes);

            if (canAccessSalary)
                Assert.AreEqual(DesiredSalary, candidate.DesiredSalary);
            else
                Assert.IsNull(candidate.DesiredSalary);

            Assert.IsTrue(_industries.CollectionEqual(candidate.Industries));
            Assert.IsTrue(_relocationLocations.CollectionEqual(candidate.RelocationLocations));
            Assert.AreEqual(RelocationPreference, candidate.RelocationPreference);
            Assert.AreEqual(HighestEducationLevel, candidate.HighestEducationLevel);
            Assert.AreEqual(RecentSeniority, candidate.RecentSeniority);
            Assert.AreEqual(RecentProfession, candidate.RecentProfession);
            Assert.AreEqual(VisaStatus, candidate.VisaStatus);
        }

        private static void AssertResume(bool canAccess, EmployerMemberView view)
        {
            // Check resume.

            if (canAccess)
            {
                Assert.AreEqual(Skills, view.Resume.Skills);
                Assert.AreEqual(Objective, view.Resume.Objective);
                Assert.AreEqual(Summary, view.Resume.Summary);
                Assert.AreEqual(Other, view.Resume.Other);
                Assert.AreEqual(Citizenship, view.Resume.Citizenship);
                Assert.AreEqual(Affiliations, view.Resume.Affiliations);
                Assert.AreEqual(Professional, view.Resume.Professional);
                Assert.AreEqual(Interests, view.Resume.Interests);

                Assert.IsTrue(Awards.SequenceEqual(view.Resume.Awards));
                Assert.IsTrue(Courses.SequenceEqual(view.Resume.Courses));

                var schools = view.Resume.Schools;
                Assert.AreEqual(Schools.Count, schools.Count);
                for (var index = 0; index < schools.Count; ++index)
                {
                    Assert.AreEqual(Schools[index].City, schools[index].City);
                    Assert.AreEqual(Schools[index].CompletionDate.End, schools[index].CompletionDate.End);
                    Assert.AreEqual(Schools[index].Country, schools[index].Country);
                    Assert.AreEqual(Schools[index].Degree, schools[index].Degree);
                    Assert.AreEqual(Schools[index].Description, schools[index].Description);
                    Assert.AreEqual(Schools[index].Institution, schools[index].Institution);
                    Assert.AreEqual(Schools[index].Major, schools[index].Major);
                }
            }
            else
            {
                Assert.IsNull(view.Resume);
            }
        }

        private static void AssertJobs(bool canAccessResume, bool canAccessRecentEmployers, EmployerMemberView view)
        {
            // Check resume.

            if (canAccessResume)
            {
                // Reference equals not good enough for this one, check the jobs explicitly.

                var jobs = view.Resume.Jobs;
                Assert.AreEqual(Jobs.Count, jobs.Count);
                for (var index = 0; index < jobs.Count; ++index)
                {
                    Assert.AreEqual(Jobs[index].Dates.Start, jobs[index].Dates.Start);
                    Assert.AreEqual(Jobs[index].Dates.End, jobs[index].Dates.End);
                    Assert.AreEqual(Jobs[index].Description, jobs[index].Description);
                    Assert.AreEqual(Jobs[index].Title, jobs[index].Title);
                }

                // The company names are subject to settings, but only the current and previous job.
                // In this test case that means the first two jobs.

                if (canAccessRecentEmployers)
                {
                    Assert.AreEqual(Jobs[0].Company, jobs[0].Company);
                    Assert.AreEqual(Jobs[1].Company, jobs[1].Company);
                    Assert.AreEqual(Jobs[2].Company, jobs[2].Company);
                }
                else
                {
                    Assert.IsNull(jobs[0].Company);
                    Assert.IsNull(jobs[1].Company);
                    Assert.AreEqual(Jobs[2].Company, jobs[2].Company);
                }
            }
            else
            {
                Assert.IsFalse(canAccessRecentEmployers);
                Assert.IsNull(view.Resume);
            }
        }

        private static void AssertReferees(bool canAccessResume, bool canAccessReferees, EmployerMemberView view)
        {
            // Check resume.

            if (canAccessResume)
            {
                if (canAccessReferees)
                    Assert.AreEqual(Referees, view.Resume.Referees);
                else
                    Assert.IsNull(view.Resume.Referees);
            }
            else
            {
                Assert.IsNull(view.Resume);
            }
        }

        private Candidate CreateCandidate()
        {
            return new Candidate
            {
                DesiredJobTitle = DesiredJobTitle,
                DesiredJobTypes = DesiredJobTypes,
                DesiredSalary = DesiredSalary,
                Industries = _industries,
                RelocationLocations = _relocationLocations,
                RelocationPreference = RelocationPreference,
                HighestEducationLevel = HighestEducationLevel,
                RecentSeniority = RecentSeniority,
                RecentProfession = RecentProfession,
                VisaStatus = VisaStatus,
                Status = Status,
            };
        }

        private static Resume CreateResume()
        {
            return new Resume
            {
                Skills = Skills,
                Objective = Objective,
                Summary = Summary,
                Other = Other,
                Citizenship = Citizenship,
                Affiliations = Affiliations,
                Professional = Professional,
                Interests = Interests,
                Referees = Referees,
                Schools = Schools,
                Courses = Courses,
                Awards = Awards,
                Jobs = Jobs,
            };
        }

        protected override ProfessionalView CreateView(Member member, int? contactCredits, ProfessionalContactDegree contactDegree, bool hasBeenAccessed, bool isRepresented)
        {
            return new EmployerMemberView(member, contactCredits, contactDegree, hasBeenAccessed, isRepresented);
        }

        private static EmployerMemberView CreateView(Member member, Candidate candidate, Resume resume, ProfessionalContactDegree contactDegree, bool hasBeenAccessed)
        {
            return new EmployerMemberView(member, candidate, resume, null, contactDegree, hasBeenAccessed, false);
        }
    }
}
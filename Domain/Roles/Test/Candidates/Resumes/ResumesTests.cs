using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Resumes
{
    [TestClass]
    public class ResumesTests
        : TestClass
    {
        private const string OtherFormat = "This is the other {0}";
        private const string DescriptionFormat = "This is the description {0}";
        private const string Title = "This is the title";
        private const string Company = "This is the company";
        private const string Institution = "This is the institution";
        private const string Degree = "This is the degree";

        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();

        [TestInitialize]
        public void ResumesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoResume()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            Assert.IsNull(candidate.ResumeId);
        }

        [TestMethod]
        public void TestEmptyResume()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            _candidateResumesCommand.CreateResume(candidate, new Resume());
            AssertResume(candidate.ResumeId.Value, null, null, null);
        }

        [TestMethod]
        public void TestUpdateEmptyResume()
        {
            var other = string.Format(OtherFormat, 1);
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            _candidateResumesCommand.CreateResume(candidate, new Resume { Other = other });
            AssertResume(candidate.ResumeId.Value, other, null, null);

            _candidateResumesCommand.CreateResume(candidate, new Resume());
            AssertResume(candidate.ResumeId.Value, null, null, null);
        }

        [TestMethod]
        public void TestCreateResume()
        {
            var other1 = string.Format(OtherFormat, 1);
            var candidate1 = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate1);
            _candidateResumesCommand.CreateResume(candidate1, new Resume { Other = other1 });

            var other2 = string.Format(OtherFormat, 2);
            var candidate2 = new Candidate {Id = Guid.NewGuid()};
            _candidatesCommand.CreateCandidate(candidate2);
            _candidateResumesCommand.CreateResume(candidate2, new Resume { Other = other2 });

            AssertResume(candidate1.ResumeId.Value, other1, null, null);
            AssertResume(candidate2.ResumeId.Value, other2, null, null);
            AssertResumes(candidate1.ResumeId, other1, null, null, candidate2.ResumeId, other2, null, null);
        }

        [TestMethod]
        public void TestUpdateResume()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            Assert.IsNull(candidate.ResumeId);

            var other = string.Format(OtherFormat, 1);
            _candidateResumesCommand.CreateResume(candidate, new Resume { Other = other });
            Assert.IsNotNull(candidate.ResumeId);
            AssertResume(candidate.ResumeId.Value, other, null, null);
        }

        [TestMethod]
        public void TestCreateJob()
        {
            var description1 = string.Format(DescriptionFormat, 1);
            var candidate1 = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate1);
            var resume = new Resume
                             {
                                 Jobs = new List<Job> { new Job { Description = description1, Title = Title, Company = Company, Dates = new PartialDateRange() } },
                             };
            _candidateResumesCommand.CreateResume(candidate1, resume);

            var description2 = string.Format(DescriptionFormat, 2);
            var candidate2 = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate2);
            resume = new Resume
                         {
                             Jobs = new List<Job> { new Job { Description = description2, Title = Title, Company = Company, Dates = new PartialDateRange() } },
                         };
            _candidateResumesCommand.CreateResume(candidate2, resume);

            AssertResume(candidate1.ResumeId.Value, null, description1, null);
            AssertResume(candidate2.ResumeId.Value, null, description2, null);
            AssertResumes(candidate1.ResumeId, null, description1, null, candidate2.ResumeId, null, description2, null);
        }

        [TestMethod]
        public void TestUpdateNoResumeToJob()
        {
            var description = string.Format(DescriptionFormat, 1);
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            Assert.IsNull(candidate.ResumeId);

            var resume = new Resume
            {
                Jobs = new List<Job> { new Job { Description = description, Title = Title, Company = Company, Dates = new PartialDateRange() } },
            };
            _candidateResumesCommand.CreateResume(candidate, resume);

            Assert.IsNotNull(candidate.ResumeId);
            AssertResume(candidate.ResumeId.Value, null, description, null);
        }

        [TestMethod]
        public void TestUpdateNoJobToJob()
        {
            var other = string.Format(OtherFormat, 1);
            var description = string.Format(DescriptionFormat, 1);
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            var resume = new Resume { Other = other };
            _candidateResumesCommand.CreateResume(candidate, resume);
            AssertResume(candidate.ResumeId.Value, other, null, null);

            resume.Jobs = new List<Job> { new Job { Description = description, Title = Title, Company = Company, Dates = new PartialDateRange() } };
            _candidateResumesCommand.UpdateResume(candidate, resume);

            AssertResume(candidate.ResumeId.Value, other, description, null);
        }

        [TestMethod]
        public void TestUpdateJobToJob()
        {
            var other = string.Format(OtherFormat, 1);
            var description1 = string.Format(DescriptionFormat, 1);
            var description2 = string.Format(DescriptionFormat, 1);
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            var resume = new Resume
                             {
                                 Other = other,
                                 Jobs = new List<Job> { new Job { Description = description1, Title = Title, Company = Company, Dates = new PartialDateRange() } },
                             };
            _candidateResumesCommand.CreateResume(candidate, resume);
            AssertResume(candidate.ResumeId.Value, other, description1, null);

            resume.Jobs = new List<Job> { new Job { Description = description2, Title = Title, Company = Company, Dates = new PartialDateRange() } };
            _candidateResumesCommand.UpdateResume(candidate, resume);

            AssertResume(candidate.ResumeId.Value, other, description2, null);
        }

        [TestMethod]
        public void TestUpdateJobToNoJob()
        {
            var other = string.Format(OtherFormat, 1);
            var description = string.Format(DescriptionFormat, 1);
            var candidate = new Candidate {Id = Guid.NewGuid()};
            _candidatesCommand.CreateCandidate(candidate);
            var resume = new Resume
                             {
                                 Other = other,
                                 Jobs = new List<Job> { new Job { Description = description, Title = Title, Company = Company, Dates = new PartialDateRange() } },
                             };
            _candidateResumesCommand.CreateResume(candidate, resume);
            AssertResume(candidate.ResumeId.Value, other, description, null);

            resume.Jobs = null;
            _candidateResumesCommand.UpdateResume(candidate, resume);

            AssertResume(candidate.ResumeId.Value, other, null, null);
        }

        [TestMethod]
        public void TestUpdateJob()
        {
            var other = string.Format(OtherFormat, 1);
            var description1 = string.Format(DescriptionFormat, 1);
            var description2 = string.Format(DescriptionFormat, 2);
            var candidate = new Candidate {Id = Guid.NewGuid()};
            _candidatesCommand.CreateCandidate(candidate);
            var resume = new Resume
                             {
                                 Other = other,
                                 Jobs = new List<Job> { new Job { Description = description1, Title = Title, Company = Company, Dates = new PartialDateRange() } },
                             };
            _candidateResumesCommand.CreateResume(candidate, resume);
            AssertResume(candidate.ResumeId.Value, other, description1, null);

            resume.Jobs[0].Description = description2;
            _candidateResumesCommand.UpdateResume(candidate, resume);

            AssertResume(candidate.ResumeId.Value, other, description2, null);
        }

        [TestMethod]
        public void TestCreateSchool()
        {
            var description1 = string.Format(DescriptionFormat, 1);
            var candidate1 = new Candidate {Id = Guid.NewGuid()};
            _candidatesCommand.CreateCandidate(candidate1);
            var resume = new Resume
                             {
                                 Schools = new List<School> { new School { Description = description1, Degree = Degree, Institution = Institution, CompletionDate = new PartialCompletionDate() } },
                             };
            _candidateResumesCommand.CreateResume(candidate1, resume);

            var description2 = string.Format(DescriptionFormat, 2);
            var candidate2 = new Candidate { Id = Guid.NewGuid() }; 
            _candidatesCommand.CreateCandidate(candidate2);
            resume = new Resume
                         {
                             Schools = new List<School> { new School { Description = description2, Degree = Degree, Institution = Institution, CompletionDate = new PartialCompletionDate() } },
                         };
            _candidateResumesCommand.CreateResume(candidate2, resume);

            AssertResume(candidate1.ResumeId.Value, null, null, description1);
            AssertResume(candidate2.ResumeId.Value, null, null, description2);
            AssertResumes(candidate1.ResumeId, null, null, description1, candidate2.ResumeId, null, null, description2);
        }

        [TestMethod]
        public void TestUpdateNoResumeToSchool()
        {
            var description = string.Format(DescriptionFormat, 1);
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            Assert.IsNull(candidate.ResumeId);

            var resume = new Resume
            {
                Schools = new List<School> { new School { Description = description, Degree = Degree, Institution = Institution, CompletionDate = new PartialCompletionDate() } },
            };
            _candidateResumesCommand.CreateResume(candidate, resume);

            Assert.IsNotNull(candidate.ResumeId);
            AssertResume(candidate.ResumeId.Value, null, null, description);
        }

        [TestMethod]
        public void TestUpdateNoSchoolToSchool()
        {
            var other = string.Format(OtherFormat, 1);
            var description = string.Format(DescriptionFormat, 1);
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            var resume = new Resume { Other = other };
            _candidateResumesCommand.CreateResume(candidate, resume);
            AssertResume(candidate.ResumeId.Value, other, null, null);

            resume.Schools = new List<School> { new School { Description = description, Degree = Degree, Institution = Institution, CompletionDate = new PartialCompletionDate() } };
            _candidateResumesCommand.UpdateResume(candidate, resume);

            AssertResume(candidate.ResumeId.Value, other, null, description);
        }

        [TestMethod]
        public void TestUpdateSchoolToSchool()
        {
            var other = string.Format(OtherFormat, 1);
            var description1 = string.Format(DescriptionFormat, 1);
            var description2 = string.Format(DescriptionFormat, 1);
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            var resume = new Resume
                             {
                                 Other = other,
                                 Schools = new List<School> { new School { Description = description1, Degree = Degree, Institution = Institution, CompletionDate = new PartialCompletionDate() } },
                             };
            _candidateResumesCommand.CreateResume(candidate, resume);
            AssertResume(candidate.ResumeId.Value, other, null, description1);

            resume.Schools = new List<School> { new School { Description = description2, Degree = Degree, Institution = Institution, CompletionDate = new PartialCompletionDate() } };
            _candidateResumesCommand.UpdateResume(candidate, resume);

            AssertResume(candidate.ResumeId.Value, other, null, description2);
        }

        [TestMethod]
        public void TestUpdateSchoolToNoSchool()
        {
            var other = string.Format(OtherFormat, 1);
            var description = string.Format(DescriptionFormat, 1);
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            var resume = new Resume
                             {
                                 Other = other,
                                 Schools = new List<School> { new School { Description = description, Degree = Degree, Institution = Institution, CompletionDate = new PartialCompletionDate() } },
                             };
            _candidateResumesCommand.CreateResume(candidate, resume);
            AssertResume(candidate.ResumeId.Value, other, null, description);

            resume.Schools = null;
            _candidateResumesCommand.UpdateResume(candidate, resume);

            AssertResume(candidate.ResumeId.Value, other, null, null);
        }

        [TestMethod]
        public void TestUpdateSchool()
        {
            var other = string.Format(OtherFormat, 1);
            var description1 = string.Format(DescriptionFormat, 1);
            var description2 = string.Format(DescriptionFormat, 2);
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            var resume = new Resume
                             {
                                 Other = other,
                                 Schools = new List<School> { new School { Description = description1, Degree = Degree, Institution = Institution, CompletionDate = new PartialCompletionDate() } },
                             };
            _candidateResumesCommand.CreateResume(candidate, resume);
            AssertResume(candidate.ResumeId.Value, other, null, description1);

            resume.Schools[0].Description = description2;
            _candidateResumesCommand.UpdateResume(candidate, resume);

            AssertResume(candidate.ResumeId.Value, other, null, description2);
        }

        private void AssertResume(Guid resumeId, string other, string jobDescription, string schoolDescription)
        {
            var resume = _resumesQuery.GetResume(resumeId);
            Assert.IsNotNull(resume);
            AssertResume(resume, other, jobDescription, schoolDescription);

            var resumes = _resumesQuery.GetResumes(new[] { resumeId });
            Assert.AreEqual(1, resumes.Count);
            AssertResume(resumes[0], other, jobDescription, schoolDescription);
        }

        private void AssertResumes(Guid? resumeId1, string other1, string jobDescription1, string schoolDescription1, Guid? resumeId2, string other2, string jobDescription2, string schoolDescription2)
        {
            var resumes = _resumesQuery.GetResumes(new[] { resumeId1.Value, resumeId2.Value });
            Assert.AreEqual(2, resumes.Count);
            AssertResume((from r in resumes where r.Id == resumeId1.Value select r).Single(), other1, jobDescription1, schoolDescription1);
            AssertResume((from r in resumes where r.Id == resumeId2.Value select r).Single(), other2, jobDescription2, schoolDescription2);
        }

        private static void AssertResume(Resume resume, string other, string jobDescription, string schoolDescription)
        {
            Assert.IsNotNull(resume);
            Assert.AreEqual(resume.Other, other);

            if (jobDescription == null)
            {
                Assert.IsNull(resume.Jobs);
            }
            else
            {
                Assert.AreEqual(1, resume.Jobs.Count);
                Assert.AreEqual(jobDescription, resume.Jobs[0].Description);
            }

            if (schoolDescription == null)
            {
                Assert.IsNull(resume.Schools);
            }
            else
            {
                Assert.AreEqual(1, resume.Schools.Count);
                Assert.AreEqual(schoolDescription, resume.Schools[0].Description);
            }
        }
    }
}
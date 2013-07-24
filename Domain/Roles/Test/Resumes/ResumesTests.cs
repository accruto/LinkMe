using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Resumes
{
    [TestClass]
    public class ResumesTests
        : TestClass
    {
        private readonly IResumesCommand _resumesCommand = Resolve<IResumesCommand>();
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();

        private const string Title = "This is the title";
        private const string Company = "This is the company";
        private const string Institution = "This is the institution";
        private const string Degree = "This is the degree";

        [TestInitialize]
        public void ResumesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreateSchoolCompletionDate()
        {
            // Set end date.

            var resume = new Resume { Schools = new List<School> { new School { CompletionDate = new PartialCompletionDate(new PartialDate(2000, 1)), Degree = Degree, Institution = Institution } } };
            _resumesCommand.CreateResume(resume);
            AssertCompletionDate(new PartialCompletionDate(new PartialDate(2000, 1)), resume.Id);

            // No end date.

            resume = new Resume { Schools = new List<School> { new School { CompletionDate = new PartialCompletionDate(), Degree = Degree, Institution = Institution } } };
            _resumesCommand.CreateResume(resume);
            AssertCompletionDate(new PartialCompletionDate(), resume.Id);
        }

        [TestMethod]
        public void TestUpdateSchoolCompletionDate()
        {
            // Set end date.

            var resume = new Resume { Schools = new List<School> { new School { CompletionDate = new PartialCompletionDate(new PartialDate(2000, 1)), Degree = Degree, Institution = Institution } } };
            _resumesCommand.CreateResume(resume);
            AssertCompletionDate(new PartialCompletionDate(new PartialDate(2000, 1)), resume.Id);

            // No end date.

            resume.Schools[0].CompletionDate = new PartialCompletionDate();
            _resumesCommand.UpdateResume(resume);
            AssertCompletionDate(new PartialCompletionDate(), resume.Id);
        }

        [TestMethod]
        public void TestCreateJobCompletionDate()
        {
            // Set dates.

            var resume = new Resume { Jobs = new List<Job> { new Job { Dates = new PartialDateRange(new PartialDate(2000, 1), new PartialDate(2001, 1)), Title = Title, Company = Company } } };
            _resumesCommand.CreateResume(resume);
            AssertDates(new PartialDateRange(new PartialDate(2000, 1), new PartialDate(2001, 1)), resume.Id);

            // No start date.

            resume = new Resume { Jobs = new List<Job> { new Job { Dates = new PartialDateRange(null, new PartialDate(2000, 1)), Title = Title, Company = Company } } };
            _resumesCommand.CreateResume(resume);
            AssertDates(new PartialDateRange(null, new PartialDate(2000, 1)), resume.Id);

            // No end date.

            resume = new Resume { Jobs = new List<Job> { new Job { Dates = new PartialDateRange(new PartialDate(2000, 1)), Title = Title, Company = Company } } };
            _resumesCommand.CreateResume(resume);
            AssertDates(new PartialDateRange(new PartialDate(2000, 1)), resume.Id);

            // No dates.

            resume = new Resume { Jobs = new List<Job> { new Job { Dates = new PartialDateRange(), Title = Title, Company = Company } } };
            _resumesCommand.CreateResume(resume);
            AssertDates(new PartialDateRange(), resume.Id);
        }

        [TestMethod]
        public void TestUpdateJobCompletionDate()
        {
            // No dates.

            var resume = new Resume { Jobs = new List<Job> { new Job { Dates = new PartialDateRange(new PartialDate(2000, 1), new PartialDate(2001, 1)), Title = Title, Company = Company } } };
            _resumesCommand.CreateResume(resume);
            AssertDates(new PartialDateRange(new PartialDate(2000, 1), new PartialDate(2001, 1)), resume.Id);

            // No start date.

            resume.Jobs[0].Dates = new PartialDateRange(null, new PartialDate(2001, 1));
            _resumesCommand.UpdateResume(resume);
            AssertDates(new PartialDateRange(null, new PartialDate(2001, 1)), resume.Id);

            // No end date.

            resume.Jobs[0].Dates = new PartialDateRange(new PartialDate(2000, 1));
            _resumesCommand.UpdateResume(resume);
            AssertDates(new PartialDateRange(new PartialDate(2000, 1)), resume.Id);

            // No dates.

            resume.Jobs[0].Dates = new PartialDateRange();
            _resumesCommand.UpdateResume(resume);
            AssertDates(new PartialDateRange(), resume.Id);
        }

        private void AssertCompletionDate(CompletionTime<PartialDate> expectedCompletionDate, Guid resumeId)
        {
            var school = _resumesQuery.GetResume(resumeId).Schools[0];
            if (expectedCompletionDate != null)
            {
                Assert.IsNotNull(school.CompletionDate);
                Assert.AreEqual(expectedCompletionDate.End, school.CompletionDate.End);
            }
            else
            {
                Assert.IsNull(school.CompletionDate);
            }

            // This is really a test for PartialCompletionDate.Equals.

            Assert.AreEqual(expectedCompletionDate, school.CompletionDate);
        }

        private void AssertDates(TimeRange<PartialDate> expectedDates, Guid resumeId)
        {
            var job = _resumesQuery.GetResume(resumeId).Jobs[0];
            if (expectedDates != null)
            {
                Assert.IsNotNull(job.Dates);
                Assert.AreEqual(expectedDates.Start, job.Dates.Start);
                Assert.AreEqual(expectedDates.End, job.Dates.End);
            }
            else
            {
                Assert.IsNull(job.Dates);
            }

            // This is really a test for PartialDateRange.Equals.

            Assert.AreEqual(expectedDates, job.Dates);
        }
    }
}

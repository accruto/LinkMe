using System.Collections.Generic;
using System.IO;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Resumes
{
    [TestClass]
    public class MockParseTests
        : TestClass
    {
        // Tests that the MockLensCommand returns what is expected.

        private readonly IParseResumesCommand _parseResumesCommand = Resolve<IParseResumesCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();

        [TestInitialize]
        public void MockParseTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCompleteResume()
        {
            // Save to a file and read it from there.

            ParsedResume parsedResume;

            const string fileName = "resume.doc";
            using (var files = _filesCommand.SaveTempFile(TestResume.Complete.GetData(), fileName))
            {
                var path = files.FilePaths[0];
                using (var stream = File.OpenRead(path))
                {
                    parsedResume = _parseResumesCommand.ParseResume(stream, fileName);
                }
            }

            AssertParsedResume(GetCompleteParsedResume(), parsedResume);
        }

        [TestMethod, ExpectedException(typeof(InvalidResumeException), "The resume is invalid.")]
        public void TestInvalidResume()
        {
            const string fileName = "invalidresume.doc";
            var data = TestResume.Invalid.GetData();

            _parseResumesCommand.ParseResume(data, fileName);
        }

        private static void AssertParsedResume(ParsedResume expectedParsedResume, ParsedResume parsedResume)
        {
            Assert.AreEqual(expectedParsedResume.FirstName, parsedResume.FirstName);
            Assert.AreEqual(expectedParsedResume.LastName, parsedResume.LastName);
            Assert.AreEqual(expectedParsedResume.Address, parsedResume.Address);
            Assert.AreEqual(expectedParsedResume.DateOfBirth, parsedResume.DateOfBirth);

            Assert.AreEqual(expectedParsedResume.Resume.Skills, parsedResume.Resume.Skills);
            Assert.AreEqual(expectedParsedResume.Resume.Objective, parsedResume.Resume.Objective);
            Assert.AreEqual(expectedParsedResume.Resume.Summary, parsedResume.Resume.Summary);
            Assert.AreEqual(expectedParsedResume.Resume.Other, parsedResume.Resume.Other);
            Assert.AreEqual(expectedParsedResume.Resume.Citizenship, parsedResume.Resume.Citizenship);
            Assert.AreEqual(expectedParsedResume.Resume.Affiliations, parsedResume.Resume.Affiliations);
            Assert.AreEqual(expectedParsedResume.Resume.Professional, parsedResume.Resume.Professional);
            Assert.AreEqual(expectedParsedResume.Resume.Interests, parsedResume.Resume.Interests);
            Assert.AreEqual(expectedParsedResume.Resume.Referees, parsedResume.Resume.Referees);

            Assert.IsTrue(expectedParsedResume.EmailAddresses.NullableCollectionEqual(parsedResume.EmailAddresses));
            Assert.IsTrue(expectedParsedResume.PhoneNumbers.NullableCollectionEqual(parsedResume.PhoneNumbers));

            if (expectedParsedResume.Resume.Courses == null)
            {
                Assert.IsNull(parsedResume.Resume.Courses);
            }
            else
            {
                Assert.AreEqual(expectedParsedResume.Resume.Courses.Count, parsedResume.Resume.Courses.Count);
                for (var index = 0; index < expectedParsedResume.Resume.Courses.Count; ++index)
                    Assert.AreEqual(expectedParsedResume.Resume.Courses[index], parsedResume.Resume.Courses[index]);
            }

            if (expectedParsedResume.Resume.Awards == null)
            {
                Assert.IsNull(parsedResume.Resume.Awards);
            }
            else
            {
                Assert.AreEqual(expectedParsedResume.Resume.Awards.Count, parsedResume.Resume.Awards.Count);
                for (var index = 0; index < expectedParsedResume.Resume.Awards.Count; ++index)
                    Assert.AreEqual(expectedParsedResume.Resume.Awards[index], parsedResume.Resume.Awards[index]);
            }

            if (expectedParsedResume.Resume.Schools == null)
            {
                Assert.IsNull(parsedResume.Resume.Schools);
            }
            else
            {
                Assert.AreEqual(expectedParsedResume.Resume.Schools.Count, parsedResume.Resume.Schools.Count);
                for (var index = 0; index < expectedParsedResume.Resume.Schools.Count; ++index)
                    AssertSchool(expectedParsedResume.Resume.Schools[index], parsedResume.Resume.Schools[index]);
            }

            if (expectedParsedResume.Resume.Jobs == null)
            {
                Assert.IsNull(parsedResume.Resume.Jobs);
            }
            else
            {
                Assert.AreEqual(expectedParsedResume.Resume.Jobs.Count, parsedResume.Resume.Jobs.Count);
                for (var index = 0; index < expectedParsedResume.Resume.Jobs.Count; ++index)
                    AssertJob(expectedParsedResume.Resume.Jobs[index], parsedResume.Resume.Jobs[index]);
            }
        }

        private static void AssertJob(IJob expectedJob, IJob job)
        {
            Assert.AreEqual(expectedJob.Title, job.Title);
            Assert.AreEqual(expectedJob.Description, job.Description);
            Assert.AreEqual(expectedJob.Company, job.Company);
            Assert.AreEqual(expectedJob.Dates, job.Dates);
        }

        private static void AssertSchool(ISchool expectedSchool, ISchool school)
        {
            Assert.AreEqual(expectedSchool.CompletionDate, school.CompletionDate);
            Assert.AreEqual(expectedSchool.Institution, school.Institution);
            Assert.AreEqual(expectedSchool.Degree, school.Degree);
            Assert.AreEqual(expectedSchool.Major, school.Major);
            Assert.AreEqual(expectedSchool.Description, school.Description);
            Assert.AreEqual(expectedSchool.City, school.City);
            Assert.AreEqual(expectedSchool.Country, school.Country);
        }

        private static ParsedResume GetCompleteParsedResume()
        {
            return new ParsedResume
            {
                FirstName = "Homer",
                LastName = "Simpson",
                DateOfBirth = new PartialDate(1960, 11),
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = "hsimpson@test.linkme.net.au", IsVerified = false } },
                Address = new ParsedAddress { Location = "South Yarra VIC 3141", Street = "1414 Evergreen Terrace" },
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = "5555 5555", Type = PhoneNumberType.Work } },
                Resume = new Resume
                {
                    Affiliations = "Some affiliations",
                    Awards = new List<string> { "Award1", "Award2" },
                    Citizenship = "American",
                    Courses = new List<string> { "Course1", "Course2" },
                    Interests = "Some interests",
                    Objective = "Some objective",
                    Other = "Some other",
                    Professional = "Some professional",
                    Referees = "Some referees",
                    Skills = "Some skills",
                    Summary = "Some summary",
                    Jobs = new List<Job>
                    {
                        new Job
                        {
                            Company = "Springfield Nuclear",
                            Description = "Nuclear technician",
                            Title = "The nuclear technician",
                            Dates = new PartialDateRange(new PartialDate(1990, 2)),
                        }
                    },
                    Schools = new List<School>
                    {
                        new School
                        {
                            City = "Springfield",
                            Country = "USA",
                            CompletionDate = new PartialCompletionDate(new PartialDate(1984, 5)),
                            Degree = "Science",
                            Description = "Nuclear physics",
                            Institution = "Mann Eye Institute",
                            Major = "Physics",
                        }
                    }
                }
            };
        }
    }
}

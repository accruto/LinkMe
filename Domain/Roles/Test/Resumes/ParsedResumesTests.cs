using System;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Resumes
{
    [TestClass]
    public class ParsedResumesTests
        : TestClass
    {
        private const string OtherFormat = "This is the other {0}";

        private readonly IParsedResumesCommand _parsedResumesCommand = Resolve<IParsedResumesCommand>();
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();

        [TestInitialize]
        public void ParsedResumesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoResume()
        {
            var parsedResume = new ParsedResume { Id = Guid.NewGuid() };
            _parsedResumesCommand.CreateParsedResume(parsedResume);
            AssertParsedResume(parsedResume, _resumesQuery.GetParsedResume(parsedResume.Id));
        }

        [TestMethod]
        public void TestEmptyResume()
        {
            var parsedResume = new ParsedResume { Id = Guid.NewGuid(), Resume = new Resume() };
            _parsedResumesCommand.CreateParsedResume(parsedResume);
            AssertParsedResume(parsedResume, _resumesQuery.GetParsedResume(parsedResume.Id));
        }

        [TestMethod]
        public void TestResume()
        {
            var other = string.Format(OtherFormat, 1);
            var parsedResume = new ParsedResume { Id = Guid.NewGuid(), Resume = new Resume { Other = other } };
            _parsedResumesCommand.CreateParsedResume(parsedResume);

            AssertParsedResume(parsedResume, _resumesQuery.GetParsedResume(parsedResume.Id));
            AssertResume(parsedResume.Resume, _resumesQuery.GetResume(parsedResume.Resume.Id));
        }

        [TestMethod]
        public void TestDeleteNoResume()
        {
            var parsedResume = new ParsedResume { Id = Guid.NewGuid() };
            _parsedResumesCommand.CreateParsedResume(parsedResume);
            AssertParsedResume(parsedResume, _resumesQuery.GetParsedResume(parsedResume.Id));

            _parsedResumesCommand.DeleteParsedResume(parsedResume.Id);
            Assert.IsNull(_resumesQuery.GetParsedResume(parsedResume.Id));
        }

        [TestMethod]
        public void TestDeleteEmptyResume()
        {
            var parsedResume = new ParsedResume { Id = Guid.NewGuid(), Resume = new Resume() };
            _parsedResumesCommand.CreateParsedResume(parsedResume);

            AssertParsedResume(parsedResume, _resumesQuery.GetParsedResume(parsedResume.Id));

            _parsedResumesCommand.DeleteParsedResume(parsedResume.Id);
            Assert.IsNull(_resumesQuery.GetParsedResume(parsedResume.Id));
        }

        [TestMethod]
        public void TestDeleteResume()
        {
            var other = string.Format(OtherFormat, 1);
            var parsedResume = new ParsedResume { Id = Guid.NewGuid(), Resume = new Resume { Other = other } };
            _parsedResumesCommand.CreateParsedResume(parsedResume);

            AssertParsedResume(parsedResume, _resumesQuery.GetParsedResume(parsedResume.Id));
            AssertResume(parsedResume.Resume, _resumesQuery.GetResume(parsedResume.Resume.Id));

            _parsedResumesCommand.DeleteParsedResume(parsedResume.Id);
            Assert.IsNull(_resumesQuery.GetParsedResume(parsedResume.Id));
            Assert.IsNull(_resumesQuery.GetResume(parsedResume.Resume.Id));
        }

        private static void AssertParsedResume(ParsedResume expectedParsedResume, ParsedResume parsedResume)
        {
            Assert.AreEqual(expectedParsedResume.FirstName, parsedResume.FirstName);
            Assert.AreEqual(expectedParsedResume.LastName, parsedResume.LastName);
            Assert.AreEqual(expectedParsedResume.Address, parsedResume.Address);
            Assert.AreEqual(expectedParsedResume.DateOfBirth, parsedResume.DateOfBirth);
            Assert.IsTrue(expectedParsedResume.PhoneNumbers.NullableCollectionEqual(parsedResume.PhoneNumbers));
            Assert.IsTrue(expectedParsedResume.EmailAddresses.NullableCollectionEqual(parsedResume.EmailAddresses));
            AssertResume(expectedParsedResume.Resume, parsedResume.Resume);
        }

        private static void AssertResume(IResume expectedResume, IResume resume)
        {
            if (expectedResume == null)
            {
                Assert.IsNull(resume);
            }
            else
            {
                Assert.AreEqual(expectedResume.Other, resume.Other);
            }
        }
    }
}
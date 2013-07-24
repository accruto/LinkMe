using System;
using System.IO;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Resumes
{
    [TestClass]
    public class CandidateResumesTests
        : TestClass
    {
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery = Resolve<ICandidateResumeFilesQuery>();
        private readonly IParseResumesCommand _parseResumesCommand = Resolve<IParseResumesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        [TestInitialize]
        public void CandidateResumesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCompleteResume()
        {
            var candidate = new Candidate {Id = Guid.NewGuid()};
            _candidatesCommand.CreateCandidate(candidate);

            const string fileName = "resume.doc";
            var data = TestResume.Complete.GetData();

            FileReference fileReference;
            using (var stream = new MemoryStream(data))
            {
                fileReference = _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }

            var resume = _parseResumesCommand.ParseResume(data, fileName).Resume;
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);

            AssertResume(_candidatesCommand.GetCandidate(candidate.Id), resume, fileReference);
        }

        [TestMethod, ExpectedException(typeof(InvalidResumeException), "The resume is invalid.")]
        public void TestInvalidResume()
        {
            var candidate = new Candidate {Id = Guid.NewGuid()};
            _candidatesCommand.CreateCandidate(candidate);

            const string fileName = "invalidresume.doc";
            var data = TestResume.Invalid.GetData();

            FileReference fileReference;
            using (var stream = new MemoryStream(data))
            {
                fileReference = _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }
            _candidateResumesCommand.CreateResume(candidate, _parseResumesCommand.ParseResume(data, fileName).Resume, fileReference);
        }

        [TestMethod]
        public void TestReplaceResume()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            Assert.IsNull(candidate.ResumeId);

            // Create resume.

            var resume1 = TestResume.Complete.GetParsedResume().Resume;
            Assert.AreEqual(Guid.Empty, resume1.Id);
            _candidateResumesCommand.CreateResume(candidate, resume1);

            Assert.AreNotEqual(Guid.Empty, resume1.Id);
            Assert.AreEqual(resume1.Id, _candidatesCommand.GetCandidate(candidate.Id).ResumeId.Value);

            // Replace with another resume.

            var resume2 = TestResume.NoEmailAddress.GetParsedResume().Resume;
            Assert.AreEqual(Guid.Empty, resume2.Id);
            _candidateResumesCommand.CreateResume(candidate, resume2);

            Assert.AreNotEqual(Guid.Empty, resume2.Id);
            Assert.AreEqual(resume2.Id, _candidatesCommand.GetCandidate(candidate.Id).ResumeId.Value);

            // Resume id should be the same as it is updated in place (at least while only one resume is supported).

            Assert.AreEqual(resume1.Id, resume2.Id);
        }

        [TestMethod]
        public void TestReplaceResumeFile()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            Assert.IsNull(candidate.ResumeId);

            // Create resume.

            const string fileName = "resume.doc";
            var data = TestResume.Complete.GetData();
            FileReference fileReference1;
            using (var stream = new MemoryStream(data))
            {
                fileReference1 = _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }

            var resume1 = _parseResumesCommand.ParseResume(data, fileName).Resume;
            Assert.AreEqual(Guid.Empty, resume1.Id);
            _candidateResumesCommand.CreateResume(candidate, resume1, fileReference1);

            Assert.AreNotEqual(Guid.Empty, resume1.Id);
            Assert.AreEqual(resume1.Id, _candidatesCommand.GetCandidate(candidate.Id).ResumeId.Value);

            // Replace with another resume.

            data = TestResume.NoEmailAddress.GetData();
            FileReference fileReference2;
            using (var stream = new MemoryStream(data))
            {
                fileReference2 = _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }

            var resume2 = _parseResumesCommand.ParseResume(data, fileName).Resume;
            Assert.AreEqual(Guid.Empty, resume2.Id);
            _candidateResumesCommand.CreateResume(candidate, resume2, fileReference2);

            Assert.AreNotEqual(Guid.Empty, resume2.Id);
            Assert.AreEqual(resume2.Id, _candidatesCommand.GetCandidate(candidate.Id).ResumeId.Value);

            // Resume id should be the same as it is updated in place (at least while only one resume is supported).

            Assert.AreEqual(resume1.Id, resume2.Id);
        }

        private void AssertResume(ICandidate candidate, IResume resume, FileReference fileReference)
        {
            Assert.IsNotNull(resume);
            var resumeFile = _candidateResumeFilesQuery.GetResumeFile(candidate.Id, fileReference.Id);
            Assert.AreEqual(fileReference.Id, resumeFile.FileReferenceId);
        }
    }
}
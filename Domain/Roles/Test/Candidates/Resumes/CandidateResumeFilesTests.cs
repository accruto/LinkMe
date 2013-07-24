using System;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Test.Files;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Resumes
{
    [TestClass]
    public class CandidateResumeFilesTests
        : TestClass
    {
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery = Resolve<ICandidateResumeFilesQuery>();
        private readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand = Resolve<ICandidateResumeFilesCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();

        [TestInitialize]
        public void CandidateResumeFilesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreateResumeFile()
        {
            var candidateId = Guid.NewGuid();
            _candidatesCommand.CreateCandidate(new Candidate { Id = candidateId });

            var file = _filesCommand.CreateTestFile(FileType.Resume);
            var resumeFileReference = new ResumeFileReference { FileReferenceId = file.Id };
            _candidateResumeFilesCommand.CreateResumeFile(candidateId, resumeFileReference);

            Assert.AreNotEqual(Guid.Empty, resumeFileReference.Id);
            Assert.AreNotEqual(DateTime.MinValue, resumeFileReference.UploadedTime);
            Assert.AreNotEqual(DateTime.MinValue, resumeFileReference.LastUsedTime);

            AssertResumeFile(resumeFileReference, _candidateResumeFilesQuery.GetResumeFile(candidateId, file.Id));
            var resumeFileReferences = _candidateResumeFilesQuery.GetResumeFiles(candidateId);
            Assert.AreEqual(1, resumeFileReferences.Count);
            AssertResumeFile(resumeFileReference, resumeFileReferences[0]);
        }

        [TestMethod]
        public void TestCreateResumeFiles()
        {
            var candidateId = Guid.NewGuid();
            _candidatesCommand.CreateCandidate(new Candidate { Id = candidateId });

            var file1 = _filesCommand.CreateTestFile(1, FileType.Resume);
            var resumeFileReference1 = new ResumeFileReference { FileReferenceId = file1.Id };
            _candidateResumeFilesCommand.CreateResumeFile(candidateId, resumeFileReference1);

            var file2 = _filesCommand.CreateTestFile(2, FileType.Resume);
            var resumeFileReference2 = new ResumeFileReference { FileReferenceId = file2.Id };
            _candidateResumeFilesCommand.CreateResumeFile(candidateId, resumeFileReference2);

            AssertResumeFile(resumeFileReference1, _candidateResumeFilesQuery.GetResumeFile(candidateId, file1.Id));
            AssertResumeFile(resumeFileReference2, _candidateResumeFilesQuery.GetResumeFile(candidateId, file2.Id));

            var resumeFileReferences = _candidateResumeFilesQuery.GetResumeFiles(candidateId);
            Assert.AreEqual(2, resumeFileReferences.Count);
            if (resumeFileReferences[0].Id == resumeFileReference1.Id)
            {
                AssertResumeFile(resumeFileReference1, resumeFileReferences[0]);
                AssertResumeFile(resumeFileReference2, resumeFileReferences[1]);
            }
            else
            {
                AssertResumeFile(resumeFileReference1, resumeFileReferences[1]);
                AssertResumeFile(resumeFileReference2, resumeFileReferences[0]);
            }
        }

        private static void AssertResumeFile(ResumeFileReference expectedResumeFileReference, ResumeFileReference resumeFileReference)
        {
            Assert.AreEqual(expectedResumeFileReference.Id, resumeFileReference.Id);
            Assert.AreEqual(expectedResumeFileReference.FileReferenceId, resumeFileReference.FileReferenceId);

            // Only compare dates because some precision is lost in saving to database.

            Assert.AreEqual(expectedResumeFileReference.UploadedTime.Date, resumeFileReference.UploadedTime.Date);
            Assert.AreEqual(expectedResumeFileReference.LastUsedTime.Date, resumeFileReference.LastUsedTime.Date);
        }
    }
}
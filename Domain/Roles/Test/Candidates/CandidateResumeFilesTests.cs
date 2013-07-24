using System;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Test.Files;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using NUnit.Framework;

namespace LinkMe.Domain.Roles.Test.Candidates
{
    [TestFixture]
    public class CandidateResumeFilesTests
    {
        private readonly IFilesCommand _filesCommand = Container.Current.Resolve<IFilesCommand>();
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery = Container.Current.Resolve<ICandidateResumeFilesQuery>();
        private readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand = Container.Current.Resolve<ICandidateResumeFilesCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Container.Current.Resolve<ICandidatesCommand>();
        private readonly ICandidatesRepository _candidatesRepository = Container.Current.Resolve<ICandidatesRepository>();

        [SetUp]
        public void SetUp()
        {
            Container.Current.Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [Test]
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
            AssertResumeFile(resumeFileReference, _candidateResumeFilesQuery.GetLastUsedResumeFile(candidateId));
        }

        [Test]
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

            resumeFileReference1.LastUsedTime = DateTime.Now.AddDays(-1);
            _candidatesRepository.UpdateResumeFile(resumeFileReference1);

            resumeFileReference2.LastUsedTime = DateTime.Now.AddDays(-2);
            _candidatesRepository.UpdateResumeFile(resumeFileReference2);

            AssertResumeFile(resumeFileReference1, _candidateResumeFilesQuery.GetResumeFile(candidateId, file1.Id));
            AssertResumeFile(resumeFileReference2, _candidateResumeFilesQuery.GetResumeFile(candidateId, file2.Id));

            var resumeFileReferences = _candidateResumeFilesQuery.GetResumeFiles(candidateId);
            Assert.AreEqual(2, resumeFileReferences.Count);

            // 1 should come first because it was used last.

            AssertResumeFile(resumeFileReference1, resumeFileReferences[0]);
            AssertResumeFile(resumeFileReference2, resumeFileReferences[1]);

            AssertResumeFile(resumeFileReference1, _candidateResumeFilesQuery.GetLastUsedResumeFile(candidateId));
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

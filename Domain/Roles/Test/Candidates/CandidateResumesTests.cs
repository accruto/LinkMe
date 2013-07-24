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
using LinkMe.Framework.Utility.Unity;
using NUnit.Framework;

namespace LinkMe.Domain.Roles.Test.Candidates
{
    [TestFixture]
    public class CandidateResumesTests
    {
        private readonly IFilesCommand _filesCommand = Container.Current.Resolve<IFilesCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Container.Current.Resolve<ICandidatesCommand>();
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery = Container.Current.Resolve<ICandidateResumeFilesQuery>();
        private readonly IParseResumesCommand _parseResumesCommand = Container.Current.Resolve<IParseResumesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Container.Current.Resolve<ICandidateResumesCommand>();

        [SetUp]
	    public void SetUp()
		{
			Container.Current.Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [Test]
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

        [Test, ExpectedException(typeof(InvalidResumeException), ExpectedMessage = "The resume is invalid.")]
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

        private void AssertResume(ICandidate candidate, IResume resume, FileReference fileReference)
        {
            Assert.IsNotNull(resume);
            var resumeFile = _candidateResumeFilesQuery.GetResumeFile(candidate.Id, fileReference.Id);
            Assert.AreEqual(fileReference.Id, resumeFile.FileReferenceId);
        }
    }
}

using System;
using System.IO;
using System.Threading;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Roles.Candidates;
using LinkMe.Query.Reports.Roles.Candidates.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Users.Members
{
    [TestClass]
    public class ResumeEventsTests
        : TestClass
    {
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly IParseResumesCommand _parseResumesCommand = Resolve<IParseResumesCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IResumeReportsQuery _resumeReportsQuery = Resolve<IResumeReportsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUploadResume()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var fileReference = GetResumeFile();
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);

            AssertEvents(candidate.Id, resume.Id, new ResumeUploadEvent());
        }

        [TestMethod]
        public void TestReloadResume()
        {
            // Upload a resume.

            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var fileReference = GetResumeFile();
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);

            // Reload a resume waiting for a different timestamp.

            Thread.Sleep(20);
            fileReference = GetResumeFile();
            resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);

            AssertEvents(candidate.Id, resume.Id, new ResumeUploadEvent(), new ResumeReloadEvent());
        }

        [TestMethod]
        public void TestUploadEditResume()
        {
            // Upload.

            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var fileReference = GetResumeFile();
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);

            // Edit.

            Thread.Sleep(20);
            _candidateResumesCommand.UpdateResume(candidate, resume);

            AssertEvents(candidate.Id, resume.Id, new ResumeUploadEvent(), new ResumeEditEvent { ResumeCreated = false });
        }

        [TestMethod]
        public void TestEditResume()
        {
            // Edit.

            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = new Resume { Citizenship = "Something" };
            _candidateResumesCommand.CreateResume(candidate, resume);

            AssertEvents(candidate.Id, resume.Id, new ResumeEditEvent { ResumeCreated = true });
        }

        [TestMethod]
        public void TestEditReloadResume()
        {
            // Edit.

            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = new Resume { Citizenship = "Something" };
            _candidateResumesCommand.CreateResume(candidate, resume);

            // Reload a resume waiting for a different timestamp.

            Thread.Sleep(20);
            var fileReference = GetResumeFile();
            resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);

            AssertEvents(candidate.Id, resume.Id, new ResumeEditEvent { ResumeCreated = true }, new ResumeReloadEvent());
        }

        [TestMethod]
        public void TestEditEditResume()
        {
            // Edit.

            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = new Resume { Citizenship = "Something" };
            _candidateResumesCommand.CreateResume(candidate, resume);

            // Edit again.

            Thread.Sleep(20);
            _candidateResumesCommand.UpdateResume(candidate, resume);

            AssertEvents(candidate.Id, resume.Id, new ResumeEditEvent { ResumeCreated = true }, new ResumeEditEvent { ResumeCreated = false });
        }

        private void AssertEvents(Guid candidateId, Guid resumeId, params ResumeEvent[] expectedEvents)
        {
            var events = _resumeReportsQuery.GetResumeEvents(candidateId);
            Assert.AreEqual(expectedEvents.Length, events.Count);

            for (var index = 0; index < expectedEvents.Length; ++index)
            {
                Assert.AreEqual(candidateId, events[index].CandidateId);
                Assert.AreEqual(resumeId, events[index].ResumeId);
                Assert.IsInstanceOfType(events[index], expectedEvents[index].GetType());

                var resumeEditEvent = expectedEvents[index] as ResumeEditEvent;
                if (resumeEditEvent != null)
                    Assert.AreEqual(resumeEditEvent.ResumeCreated, ((ResumeEditEvent)events[index]).ResumeCreated);
            }
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        private FileReference GetResumeFile()
        {
            const string fileName = "resume.doc";
            var data = TestResume.Complete.GetData();
            using (var stream = new MemoryStream(data))
            {
                return _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }
        }
    }
}

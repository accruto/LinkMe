using System;
using System.IO;
using LinkMe.Domain;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Roles.Candidates;
using LinkMe.Query.Reports.Roles.Candidates.Commands;
using LinkMe.Query.Reports.Roles.Candidates.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Roles.Candidates
{
    [TestClass]
    public class ResumeEventsTests
        : TestClass
    {
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IResumeReportsCommand _resumeReportsCommand = Resolve<IResumeReportsCommand>();
        private readonly IResumeReportsQuery _resumeReportsQuery = Resolve<IResumeReportsQuery>();

        [TestInitialize]
        public void ResumeEventsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUploadTimeAgo()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Uploaded long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-10) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadYesterday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(1, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadToday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload today.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddMinutes(-1) });

            Assert.AreEqual(0, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadTimeAgoReloadTimeAgo()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-10) });

            // Reload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-10).AddMinutes(1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadTimeAgoReloadYesterday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-10) });

            // Reload yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadTimeAgoReloadToday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-10) });

            // Reload today.

            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddMinutes(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadTimeAgoEditTimeAgo()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-10) });

            // Reload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = false, Time = DateTime.Now.AddDays(-10).AddMinutes(1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadTimeAgoEditYesterday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-10) });

            // Reload yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = false, Time = DateTime.Now.AddDays(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadTimeAgoEditToday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-10) });

            // Reload today.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = false, Time = DateTime.Now.AddMinutes(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadYesterdayReloadYesterday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-1) });

            // Reload yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-1).AddMinutes(1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(1, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadYesterdayReloadToday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-1) });

            // Reload today.

            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddMinutes(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(1, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadYesterdayEditYesterday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-1) });

            // Reload yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = false, Time = DateTime.Now.AddDays(-1).AddMinutes(1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(1, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestUploadYesterdayEditToday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResumeFile();

            // Upload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-1) });

            // Reload today.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = false, Time = DateTime.Now.AddMinutes(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(1, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditTimeAgo()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-10) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditYesterday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(1, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditToday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit today.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddMinutes(-1) });

            Assert.AreEqual(0, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditTimeAgoReloadTimeAgo()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-10) });

            // Reload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-10).AddMinutes(1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditTimeAgoReloadYesterday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-10) });

            // Reload yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditTimeAgoReloadToday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-10) });

            // Reload today.

            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddMinutes(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditTimeAgoEditTimeAgo()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-10) });

            // Reload long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = false, Time = DateTime.Now.AddDays(-10).AddMinutes(1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditTimeAgoEditYesterday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-10) });

            // Reload yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = false, Time = DateTime.Now.AddDays(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditTimeAgoEditToday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-10) });

            // Reload today.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = false, Time = DateTime.Now.AddMinutes(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(0, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditYesterdayReloadYesterday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-1) });

            // Reload yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddDays(-1).AddMinutes(1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(1, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditYesterdayReloadToday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-1) });

            // Reload today.

            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = DateTime.Now.AddMinutes(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(1, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditYesterdayEditYesterday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-1) });

            // Reload yesterday.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = false, Time = DateTime.Now.AddDays(-1).AddMinutes(1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(1, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        [TestMethod]
        public void TestEditYesterdayEditToday()
        {
            // Created long time ago.

            var candidate = CreateCandidateWithResume();

            // Edit long time ago.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-1) });

            // Reload today.

            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = false, Time = DateTime.Now.AddMinutes(-1) });

            Assert.AreEqual(1, _resumeReportsQuery.GetResumes(DateTime.Now.Date));
            Assert.AreEqual(1, _resumeReportsQuery.GetNewResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetUploadedResumes(DayRange.Yesterday));
            Assert.AreEqual(0, _resumeReportsQuery.GetReloadedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetEditedResumes(DayRange.Yesterday));
            Assert.AreEqual(1, _resumeReportsQuery.GetUpdatedResumes(DayRange.Yesterday));
        }

        private Candidate CreateCandidateWithResumeFile()
        {
            // Create member.

            var member = _membersCommand.CreateTestMember(0);
            member.CreatedTime = DateTime.Now.AddDays(-10);
            _membersCommand.UpdateMember(member);

            // Create candidate.

            var candidate = new Candidate { Id = member.Id };
            _candidatesCommand.CreateCandidate(candidate);

            // Create resume file.

            var fileReference = GetResumeFile();
            var resume = new Resume { Citizenship = "Something" };
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);

            return candidate;
        }

        private Candidate CreateCandidateWithResume()
        {
            // Create member.

            var member = _membersCommand.CreateTestMember(0);
            member.CreatedTime = DateTime.Now.AddDays(-10);
            _membersCommand.UpdateMember(member);

            // Create candidate.

            var candidate = new Candidate { Id = member.Id };
            _candidatesCommand.CreateCandidate(candidate);

            // Create resume.

            var resume = new Resume { Citizenship = "Something" };
            _candidateResumesCommand.CreateResume(candidate, resume);

            return candidate;
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

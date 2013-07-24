using System;
using System.Collections.Generic;
using System.Threading;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.Notes
{
    [TestClass]
    public class JobAdNotesTests
        : TestClass
    {
        private readonly IMemberJobAdNotesCommand _memberJobAdNotesCommand = Resolve<IMemberJobAdNotesCommand>();
        private readonly IMemberJobAdNotesQuery _memberJobAdNotesQuery = Resolve<IMemberJobAdNotesQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private const string TextFormat = "Here's a new note {0}";

        [TestInitialize]
        public void JobAdNotesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNote()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var noteCreator = _membersCommand.CreateTestMember(1);

            // Add one.

            var note = CreateNote(1, noteCreator, jobAd.Id);

            AssertNotes(noteCreator, jobAd.Id, new[] { note }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd.Id);
        }

        [TestMethod]
        public void TestNotes()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var noteCreator = _membersCommand.CreateTestMember(1);

            // Add one.

            var note1 = CreateNote(1, noteCreator, jobAd.Id);
            AssertNotes(noteCreator, jobAd.Id, new[] { note1 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd.Id);

            // Add another.

            NoteCreationDelay();
            var note2 = CreateNote(2, noteCreator, jobAd.Id);

            AssertNotes(noteCreator, jobAd.Id, new[] { note2, note1 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd.Id);

            // Update the first, order should be reversed.

            NoteCreationDelay();
            note1.Text = string.Format(TextFormat, 3);
            _memberJobAdNotesCommand.UpdateNote(noteCreator, note1);

            AssertNotes(noteCreator, jobAd.Id, new[] { note1, note2 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd.Id);

            // Delete the first.

            _memberJobAdNotesCommand.DeleteNote(noteCreator, note1.Id);

            AssertNotes(noteCreator, jobAd.Id, new[] { note2 }, new[] { note1 });
            AssertHasNotes(noteCreator, jobAd.Id);
        }

        [TestMethod]
        public void Test2JobAdNotes()
        {
            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);

            var noteCreator = _membersCommand.CreateTestMember(1);

            // Add one.

            var note1 = CreateNote(1, noteCreator, jobAd1.Id);

            AssertNotes(noteCreator, jobAd1.Id, new[] { note1 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd1.Id);

            // Add another.

            NoteCreationDelay();
            var note2 = CreateNote(2, noteCreator, jobAd2.Id);

            AssertNotes(noteCreator, jobAd1.Id, new[] { note1 }, new MemberJobAdNote[0]);
            AssertNotes(noteCreator, jobAd2.Id, new[] { note2 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd1.Id, jobAd2.Id);

            // Update the first, order should be reversed.

            NoteCreationDelay();
            note1.Text = string.Format(TextFormat, 3);
            _memberJobAdNotesCommand.UpdateNote(noteCreator, note1);

            AssertNotes(noteCreator, jobAd1.Id, new[] { note1 }, new MemberJobAdNote[0]);
            AssertNotes(noteCreator, jobAd2.Id, new[] { note2 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd1.Id, jobAd2.Id);

            // Delete the first.

            _memberJobAdNotesCommand.DeleteNote(noteCreator, note1.Id);

            AssertNotes(noteCreator, jobAd1.Id, new MemberJobAdNote[0], new[] { note1 });
            AssertHasNotes(noteCreator, jobAd2.Id);
            AssertNotes(noteCreator, jobAd2.Id, new[] { note2 }, new MemberJobAdNote[0]);
        }

        [TestMethod]
        public void TestNoteOwner()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var noteCreator = _membersCommand.CreateTestMember(1);
            var noteOtherCreator = _membersCommand.CreateTestMember(2);

            // Add a private one.

            var note1 = CreateNote(1, noteCreator, jobAd.Id);

            // Add another.

            NoteCreationDelay();
            var note2 = CreateNote(2, noteCreator, jobAd.Id);

            AssertNotes(noteCreator, jobAd.Id, new[] { note2, note1 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd.Id);

            // Try to update the first text as the reader.

            const string updatedText = "Updated first note";
            var note = _memberJobAdNotesQuery.GetNote(noteCreator, note1.Id);
            note.Text = updatedText;

            try
            {
                _memberJobAdNotesCommand.UpdateNote(noteOtherCreator, note);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNotes(noteCreator, jobAd.Id, new[] { note2, note1 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd.Id);

            // Try to update the second text as the reader.

            note = _memberJobAdNotesQuery.GetNote(noteCreator, note2.Id);
            note.Text = updatedText;

            try
            {
                _memberJobAdNotesCommand.UpdateNote(noteOtherCreator, note);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNotes(noteCreator, jobAd.Id, new[] { note2, note1 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd.Id);

            // Try to delete the first one.

            try
            {
                _memberJobAdNotesCommand.DeleteNote(noteOtherCreator, note1.Id);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNotes(noteCreator, jobAd.Id, new[] { note2, note1 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd.Id);

            // Try to delete the second one.

            try
            {
                _memberJobAdNotesCommand.DeleteNote(noteOtherCreator, note2.Id);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNotes(noteCreator, jobAd.Id, new[] { note2, note1 }, new MemberJobAdNote[0]);
            AssertHasNotes(noteCreator, jobAd.Id);
        }

        private MemberJobAdNote CreateNote(int index, IMember noteCreator, Guid memberId)
        {
            var note = new MemberJobAdNote { JobAdId = memberId, Text = string.Format(TextFormat, index) };
            _memberJobAdNotesCommand.CreateNote(noteCreator, note);
            return note;
        }

        private static void NoteCreationDelay()
        {
            Thread.Sleep(20);
        }

        private void AssertNotes(IMember member, Guid memberId, MemberJobAdNote[] accessibleNotes, IEnumerable<MemberJobAdNote> notAccessibleNotes)
        {
            // GetCandidateNote

            foreach (var note in accessibleNotes)
                AssertNote(note, _memberJobAdNotesQuery.GetNote(member, note.Id));
            foreach (var note in notAccessibleNotes)
                Assert.IsNull(_memberJobAdNotesQuery.GetNote(member, note.Id));

            // GetNotes

            var notes = _memberJobAdNotesQuery.GetNotes(member, memberId);
            Assert.AreEqual(accessibleNotes.Length, notes.Count);
            for (var index = 0; index < notes.Count; ++index)
                AssertNote(accessibleNotes[index], notes[index]);

            // HasNotes

            Assert.AreEqual(accessibleNotes.Length > 0, _memberJobAdNotesQuery.HasNotes(member, memberId));
        }

        private void AssertHasNotes(IMember member, params Guid[] expectedCandidateIds)
        {
            var candidateIds = _memberJobAdNotesQuery.GetHasNotesJobAdIds(member);
            Assert.IsTrue(expectedCandidateIds.NullableCollectionEqual(candidateIds));
        }

        private static void AssertNote(MemberJobAdNote expectedNote, MemberJobAdNote note)
        {
            Assert.AreEqual(expectedNote.Id, note.Id);
            Assert.AreEqual(expectedNote.MemberId, note.MemberId);
            Assert.AreEqual(expectedNote.JobAdId, note.JobAdId);
            Assert.AreEqual(expectedNote.Text, note.Text);
        }

        private Employer CreateEmployer(int index)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });
            return employer;
        }
    }
}
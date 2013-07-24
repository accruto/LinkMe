using System;
using System.Collections.Generic;
using System.Threading;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Notes
{
    [TestClass]
    public class CandidateNotesTests
        : TestClass
    {
        private readonly ICandidateNotesCommand _candidateNotesCommand = Resolve<ICandidateNotesCommand>();
        private readonly ICandidateNotesQuery _candidateNotesQuery = Resolve<ICandidateNotesQuery>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private const string TextFormat = "Here's a new note {0}";

        [TestInitialize]
        public void CandidateNotesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUnverifiedPrivateNote()
        {
            TestNote(false, false);
        }

        [TestMethod]
        public void TestVerifiedPrivateNote()
        {
            TestNote(true, false);
        }

        [TestMethod]
        public void TestUnverifiedSharedNote()
        {
            TestNote(false, true);
        }

        [TestMethod]
        public void TestVerifiedSharedNote()
        {
            TestNote(true, true);
        }

        [TestMethod]
        public void TestPrivatePrivateNotes()
        {
            TestNotes(false, false);
        }

        [TestMethod]
        public void TestPrivateSharedNotes()
        {
            TestNotes(false, true);
        }

        [TestMethod]
        public void TestSharedSharedNotes()
        {
            TestNotes(true, true);
        }

        [TestMethod]
        public void Test2MembersPrivatePrivateNotes()
        {
            Test2MemberNotes(false, false);
        }

        [TestMethod]
        public void Test2MembersPrivateSharedNotes()
        {
            Test2MemberNotes(false, true);
        }

        [TestMethod]
        public void Test2MembersSharedSharedNotes()
        {
            Test2MemberNotes(true, true);
        }

        [TestMethod]
        public void TestOrganisationSharedNotes()
        {
            var member = _membersCommand.CreateTestMember(1);
            var noteCreator = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            var organisation = noteCreator.Organisation;
            var noteReader = _employersCommand.CreateTestEmployer(2, organisation);

            // Create a private and a shared.

            var note1 = CreateNote(1, noteCreator, member.Id, false);
            NoteCreationDelay();
            var note2 = CreateNote(2, noteCreator, member.Id, true);

            AssertNotes(noteCreator, member.Id, new[] { note2, note1 }, new CandidateNote[0]);
            AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);

            // The creator leaves the company, so they cannot access their own shared notes.

            var otherOrganisation = new Organisation { Name = "OtherOrganisation" };
            _organisationsCommand.CreateOrganisation(otherOrganisation);

            noteCreator.Organisation = otherOrganisation;
            _employersCommand.UpdateEmployer(noteCreator);

            AssertNotes(noteCreator, member.Id, new[] { note1 }, new[] { note2 });
            AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);

            // Create a private while not part of the company.

            var note3 = CreateNote(3, noteCreator, member.Id, false);

            AssertNotes(noteCreator, member.Id, new[] { note3, note1 }, new[] { note2 });
            AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note3, note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);

            // Join again so can see original notes.

            noteCreator.Organisation = organisation;
            _employersCommand.UpdateEmployer(noteCreator);

            AssertNotes(noteCreator, member.Id, new[] { note3, note2, note1 }, new CandidateNote[0]);
            AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note3, note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);

            // Change the latest to shared, so now the second employer can see it.

            _candidateNotesCommand.ShareNote(noteCreator, note3);

            AssertNotes(noteCreator, member.Id, new[] { note3, note2, note1 }, new CandidateNote[0]);
            AssertNotes(noteReader, member.Id, new[] { note3, note2 }, new[] { note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);

            // Make that one private once again.

            _candidateNotesCommand.UnshareNote(noteCreator, note3);

            AssertNotes(noteCreator, member.Id, new[] { note3, note2, note1 }, new CandidateNote[0]);
            AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note3, note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);
        }

        [TestMethod]
        public void TestNoteOwner()
        {
            var member = _membersCommand.CreateTestMember(1);
            var noteCreator = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            var noteReader = _employersCommand.CreateTestEmployer(2, noteCreator.Organisation);

            // Add a private one.

            var note1 = CreateNote(1, noteCreator, member.Id, false);

            // Add a shared one.

            NoteCreationDelay();
            var note2 = CreateNote(2, noteCreator, member.Id, true);

            AssertNotes(noteCreator, member.Id, new[] { note2, note1 }, new CandidateNote[0]);
            AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);

            // Try to update the first text as the reader.

            const string updatedText = "Updated first note";
            var note = _candidateNotesQuery.GetNote(noteCreator, note1.Id);
            note.Text = updatedText;

            try
            {
                _candidateNotesCommand.UpdateNote(noteReader, note);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNotes(noteCreator, member.Id, new[] { note2, note1 }, new CandidateNote[0]);
            AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);

            // Try to update the second text as the reader.

            note = _candidateNotesQuery.GetNote(noteCreator, note2.Id);
            note.Text = updatedText;

            try
            {
                _candidateNotesCommand.UpdateNote(noteReader, note);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNotes(noteCreator, member.Id, new[] { note2, note1 }, new CandidateNote[0]);
            AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);

            // Try to delete the first one.

            try
            {
                _candidateNotesCommand.DeleteNote(noteReader, note1.Id);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNotes(noteCreator, member.Id, new[] { note2, note1 }, new CandidateNote[0]);
            AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);

            // Try to delete the second one.

            try
            {
                _candidateNotesCommand.DeleteNote(noteReader, note2.Id);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNotes(noteCreator, member.Id, new[] { note2, note1 }, new CandidateNote[0]);
            AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
            AssertHasNotes(noteCreator, member.Id);
            AssertHasNotes(noteReader, member.Id);
        }

        private CandidateNote CreateNote(int index, IEmployer noteCreator, Guid memberId, bool isShared)
        {
            var note = new CandidateNote { CandidateId = memberId, Text = string.Format(TextFormat, index) };
            if (isShared)
                _candidateNotesCommand.CreateSharedNote(noteCreator, note);
            else
                _candidateNotesCommand.CreatePrivateNote(noteCreator, note);
            return note;
        }

        private void TestNote(bool verified, bool isShared)
        {
            var member = _membersCommand.CreateTestMember(1);
            var noteCreator = _employersCommand.CreateTestEmployer(1, verified ? _organisationsCommand.CreateTestVerifiedOrganisation(1) : _organisationsCommand.CreateTestOrganisation(1));
            var noteReader = _employersCommand.CreateTestEmployer(2, noteCreator.Organisation);

            // Add one.

            var note = CreateNote(1, noteCreator, member.Id, isShared);

            AssertNotes(noteCreator, member.Id, new[] { note }, new CandidateNote[0]);
            AssertHasNotes(noteCreator, member.Id);
            if (isShared)
            {
                AssertNotes(noteReader, member.Id, new[] {note}, new CandidateNote[0]);
                AssertHasNotes(noteReader, member.Id);
            }
            else
            {
                AssertNotes(noteReader, member.Id, new CandidateNote[0], new[] { note });
                AssertHasNotes(noteReader);
            }
        }

        private void TestNotes(bool isShared1, bool isShared2)
        {
            var member = _membersCommand.CreateTestMember(1);
            var noteCreator = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            var noteReader = _employersCommand.CreateTestEmployer(2, noteCreator.Organisation);

            // Add one.

            var note1 = CreateNote(1, noteCreator, member.Id, isShared1);

            AssertNotes(noteCreator, member.Id, new[] { note1 }, new CandidateNote[0]);
            AssertHasNotes(noteCreator, member.Id);
            if (isShared1)
            {
                AssertNotes(noteReader, member.Id, new[] { note1 }, new CandidateNote[0]);
                AssertHasNotes(noteReader, member.Id);
            }
            else
            {
                AssertNotes(noteReader, member.Id, new CandidateNote[0], new[] {note1});
                AssertHasNotes(noteReader);
            }

            // Add another.

            NoteCreationDelay();
            var note2 = CreateNote(2, noteCreator, member.Id, isShared2);

            AssertNotes(noteCreator, member.Id, new[] { note2, note1 }, new CandidateNote[0]);
            AssertHasNotes(noteCreator, member.Id);
            if (isShared1)
            {
                if (isShared2)
                {
                    AssertNotes(noteReader, member.Id, new[] { note2, note1 }, new CandidateNote[0]);
                    AssertHasNotes(noteReader, member.Id);
                }
                else
                {
                    AssertNotes(noteReader, member.Id, new[] {note1}, new[] {note2});
                    AssertHasNotes(noteReader, member.Id);
                }
            }
            else
            {
                if (isShared2)
                {
                    AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
                    AssertHasNotes(noteReader, member.Id);
                }
                else
                {
                    AssertNotes(noteReader, member.Id, new CandidateNote[0], new[] {note2, note1});
                    AssertHasNotes(noteReader);
                }
            }

            // Update the first, order should be reversed.

            NoteCreationDelay();
            note1.Text = string.Format(TextFormat, 3);
            _candidateNotesCommand.UpdateNote(noteCreator, note1);

            AssertNotes(noteCreator, member.Id, new[] { note1, note2 }, new CandidateNote[0]);
            AssertHasNotes(noteCreator, member.Id);
            if (isShared1)
            {
                if (isShared2)
                {
                    AssertNotes(noteReader, member.Id, new[] { note1, note2 }, new CandidateNote[0]);
                    AssertHasNotes(noteReader, member.Id);
                }
                else
                {
                    AssertNotes(noteReader, member.Id, new[] {note1}, new[] {note2});
                    AssertHasNotes(noteReader, member.Id);
                }
            }
            else
            {
                if (isShared2)
                {
                    AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
                    AssertHasNotes(noteReader, member.Id);
                }
                else
                {
                    AssertNotes(noteReader, member.Id, new CandidateNote[0], new[] {note1, note2});
                    AssertHasNotes(noteReader);
                }
            }

            // Delete the first.

            _candidateNotesCommand.DeleteNote(noteCreator, note1.Id);

            AssertNotes(noteCreator, member.Id, new[] { note2 }, new[] { note1 });
            AssertHasNotes(noteCreator, member.Id);
            if (isShared1)
            {
                if (isShared2)
                {
                    AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
                    AssertHasNotes(noteReader, member.Id);
                }
                else
                {
                    AssertNotes(noteReader, member.Id, new CandidateNote[0], new[] {note1, note2});
                    AssertHasNotes(noteReader);
                }
            }
            else
            {
                if (isShared2)
                {
                    AssertNotes(noteReader, member.Id, new[] { note2 }, new[] { note1 });
                    AssertHasNotes(noteReader, member.Id);
                }
                else
                {
                    AssertNotes(noteReader, member.Id, new CandidateNote[0], new[] {note2, note1});
                    AssertHasNotes(noteReader);
                }
            }
        }

        private void Test2MemberNotes(bool isShared1, bool isShared2)
        {
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var noteCreator = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            var noteReader = _employersCommand.CreateTestEmployer(2, noteCreator.Organisation);

            // Add one.

            var note1 = CreateNote(1, noteCreator, member1.Id, isShared1);

            AssertNotes(noteCreator, member1.Id, new[] { note1 }, new CandidateNote[0]);
            AssertHasNotes(noteCreator, member1.Id);
            if (isShared1)
            {
                AssertNotes(noteReader, member1.Id, new[] { note1 }, new CandidateNote[0]);
                AssertHasNotes(noteReader, member1.Id);
            }
            else
            {
                AssertNotes(noteReader, member1.Id, new CandidateNote[0], new[] {note1});
                AssertHasNotes(noteReader);
            }

            // Add another.

            NoteCreationDelay();
            var note2 = CreateNote(2, noteCreator, member2.Id, isShared2);

            AssertNotes(noteCreator, member1.Id, new[] { note1 }, new CandidateNote[0]);
            AssertNotes(noteCreator, member2.Id, new[] { note2 }, new CandidateNote[0]);
            AssertHasNotes(noteCreator, member1.Id, member2.Id);
            if (isShared1)
            {
                if (isShared2)
                {
                    AssertNotes(noteReader, member1.Id, new[] {note1}, new CandidateNote[0]);
                    AssertNotes(noteReader, member2.Id, new[] { note2 }, new CandidateNote[0]);
                    AssertHasNotes(noteReader, member1.Id, member2.Id);
                }
                else
                {
                    AssertNotes(noteReader, member1.Id, new[] { note1 }, new CandidateNote[0]);
                    AssertNotes(noteReader, member2.Id, new CandidateNote[0], new[] { note2 });
                    AssertHasNotes(noteReader, member1.Id);
                }
            }
            else
            {
                if (isShared2)
                {
                    AssertNotes(noteReader, member1.Id, new CandidateNote[0], new[] {note1});
                    AssertNotes(noteReader, member2.Id, new[] { note2 }, new CandidateNote[0]);
                    AssertHasNotes(noteReader, member2.Id);
                }
                else
                {
                    AssertNotes(noteReader, member1.Id, new CandidateNote[0], new[] { note1 });
                    AssertNotes(noteReader, member2.Id, new CandidateNote[0], new[] { note2 });
                    AssertHasNotes(noteReader);
                }
            }

            // Update the first, order should be reversed.

            NoteCreationDelay();
            note1.Text = string.Format(TextFormat, 3);
            _candidateNotesCommand.UpdateNote(noteCreator, note1);

            AssertNotes(noteCreator, member1.Id, new[] { note1 }, new CandidateNote[0]);
            AssertNotes(noteCreator, member2.Id, new[] { note2 }, new CandidateNote[0]);
            AssertHasNotes(noteCreator, member1.Id, member2.Id);
            if (isShared1)
            {
                if (isShared2)
                {
                    AssertNotes(noteReader, member1.Id, new[] { note1 }, new CandidateNote[0]);
                    AssertNotes(noteReader, member2.Id, new[] { note2 }, new CandidateNote[0]);
                    AssertHasNotes(noteReader, member1.Id, member2.Id);
                }
                else
                {
                    AssertNotes(noteReader, member1.Id, new[] { note1 }, new CandidateNote[0]);
                    AssertNotes(noteReader, member2.Id, new CandidateNote[0], new[] { note2 });
                    AssertHasNotes(noteReader, member1.Id);
                }
            }
            else
            {
                if (isShared2)
                {
                    AssertNotes(noteReader, member1.Id, new CandidateNote[0], new[] {note1});
                    AssertNotes(noteReader, member2.Id, new[] { note2 }, new CandidateNote[0]);
                    AssertHasNotes(noteReader, member2.Id);
                }
                else
                {
                    AssertNotes(noteReader, member1.Id, new CandidateNote[0], new[] { note1 });
                    AssertNotes(noteReader, member2.Id, new CandidateNote[0], new[] { note2 });
                    AssertHasNotes(noteReader);
                }
            }

            // Delete the first.

            _candidateNotesCommand.DeleteNote(noteCreator, note1.Id);

            AssertNotes(noteCreator, member1.Id, new CandidateNote[0], new[] { note1 });
            AssertNotes(noteReader, member1.Id, new CandidateNote[0], new[] { note1 });
            AssertHasNotes(noteCreator, member2.Id);

            AssertNotes(noteCreator, member2.Id, new[] { note2 }, new CandidateNote[0]);
            if (isShared2)
            {
                AssertNotes(noteReader, member2.Id, new[] { note2 }, new CandidateNote[0]);
                AssertHasNotes(noteReader, member2.Id);
            }
            else
            {
                AssertNotes(noteReader, member2.Id, new CandidateNote[0], new[] {note2});
                AssertHasNotes(noteReader);
            }
        }

        private static void NoteCreationDelay()
        {
            Thread.Sleep(20);
        }

        private void AssertNotes(IEmployer employer, Guid memberId, CandidateNote[] accessibleNotes, IEnumerable<CandidateNote> notAccessibleNotes)
        {
            // GetCandidateNote

            foreach (var note in accessibleNotes)
                AssertNote(note, _candidateNotesQuery.GetNote(employer, note.Id));
            foreach (var note in notAccessibleNotes)
                Assert.IsNull(_candidateNotesQuery.GetNote(employer, note.Id));

            // GetNotes

            var notes = _candidateNotesQuery.GetNotes(employer, memberId);
            Assert.AreEqual(accessibleNotes.Length, notes.Count);
            for (var index = 0; index < notes.Count; ++index)
                AssertNote(accessibleNotes[index], notes[index]);

            // HasNotes

            Assert.AreEqual(accessibleNotes.Length > 0, _candidateNotesQuery.HasNotes(employer, memberId));
        }

        private void AssertHasNotes(IEmployer employer, params Guid[] expectedCandidateIds)
        {
            var candidateIds = _candidateNotesQuery.GetHasNotesCandidateIds(employer);
            Assert.IsTrue(expectedCandidateIds.NullableCollectionEqual(candidateIds));
        }

        private static void AssertNote(CandidateNote expectedNote, CandidateNote note)
        {
            Assert.AreEqual(expectedNote.Id, note.Id);
            Assert.AreEqual(expectedNote.RecruiterId, note.RecruiterId);
            Assert.AreEqual(expectedNote.OrganisationId, note.OrganisationId);
            Assert.AreEqual(expectedNote.CandidateId, note.CandidateId);
            Assert.AreEqual(expectedNote.Text, note.Text);
        }
    }
}
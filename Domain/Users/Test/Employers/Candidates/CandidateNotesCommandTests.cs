using System.Threading;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Test.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using NUnit.Framework;

namespace LinkMe.Domain.Users.Test.Employers.Candidates
{
    [TestFixture]
    public class CandidateNotesCommandTests
    {
        private readonly ICandidateNotesCommand _candidateNotesCommand = Container.Current.Resolve<ICandidateNotesCommand>();
        private readonly ICreditsCommand _creditsCommand = Container.Current.Resolve<ICreditsCommand>();
        private readonly IAllocationsCommand _allocationsCommand = Container.Current.Resolve<IAllocationsCommand>();
        private readonly ITestCreateEmployerContactsCommand _createEmployerContactsCommand = new TestCreateEmployerContactsCommand();
        private readonly ITestCreateMemberContactsCommand _createMemberContactsCommand = new TestCreateMemberContactsCommand();
        private readonly IOrganisationsCommand _organisationsCommand = Container.Current.Resolve<IOrganisationsCommand>();

        [SetUp]
        public void SetUp()
        {
            Container.Current.Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [Test]
        public void TestUnverifiedNoCreditsPrivateNote()
        {
            TestPrivateNote<ContactCredit>(false, null);
        }

        [Test]
        public void TestUnverifiedZeroContactCreditsPrivateNote()
        {
            TestPrivateNote<ContactCredit>(false, 0);
        }

        [Test]
        public void TestUnverifiedSomeContactCreditsPrivateNote()
        {
            TestPrivateNote<ContactCredit>(false, 10);
        }

        [Test]
        public void TestUnverifiedZeroApplicantCreditsPrivateNote()
        {
            TestPrivateNote<ApplicantCredit>(false, 0);
        }

        [Test]
        public void TestUnverifiedSomeApplicantCreditsPrivateNote()
        {
            TestPrivateNote<ApplicantCredit>(false, 10);
        }

        [Test]
        public void TestVerifiedNoCreditsPrivateNote()
        {
            TestPrivateNote<ContactCredit>(true, null);
        }

        [Test]
        public void TestVerifiedZeroContactCreditsPrivateNote()
        {
            TestPrivateNote<ContactCredit>(true, 0);
        }

        [Test]
        public void TestVerifiedSomeContactCreditsPrivateNote()
        {
            TestPrivateNote<ContactCredit>(true, 10);
        }

        [Test]
        public void TestVerifiedZeroApplicantCreditsPrivateNote()
        {
            TestPrivateNote<ApplicantCredit>(true, 0);
        }

        [Test]
        public void TestVerifiedSomeApplicantCreditsPrivateNote()
        {
            TestPrivateNote<ApplicantCredit>(true, 10);
        }

        [Test]
        public void TestUnverifiedNoCreditsSharedNote()
        {
            TestSharedNote<ContactCredit>(false, null);
        }

        [Test]
        public void TestUnverifiedZeroContactCreditsSharedNote()
        {
            TestSharedNote<ContactCredit>(false, 0);
        }

        [Test]
        public void TestUnverifiedSomeContactCreditsSharedNote()
        {
            TestSharedNote<ContactCredit>(false, 10);
        }

        [Test]
        public void TestUnverifiedZeroApplicantCreditsSharedNote()
        {
            TestSharedNote<ApplicantCredit>(false, 0);
        }

        [Test]
        public void TestUnverifiedSomeApplicantCreditsSharedNote()
        {
            TestSharedNote<ApplicantCredit>(false, 10);
        }

        [Test]
        public void TestVerifiedNoCreditsSharedNote()
        {
            TestSharedNote<ContactCredit>(true, null);
        }

        [Test]
        public void TestVerifiedZeroContactCreditsSharedNote()
        {
            TestSharedNote<ContactCredit>(true, 0);
        }

        [Test]
        public void TestVerifiedSomeContactCreditsSharedNote()
        {
            TestSharedNote<ContactCredit>(true, 10);
        }

        [Test]
        public void TestVerifiedZeroApplicantCreditsSharedNote()
        {
            TestSharedNote<ApplicantCredit>(true, 0);
        }

        [Test]
        public void TestVerifiedSomeApplicantCreditsSharedNote()
        {
            TestSharedNote<ApplicantCredit>(true, 10);
        }

        [Test]
        public void TestPrivatePrivateNotes()
        {
            TestNotes(false, false);
        }

        [Test]
        public void TestPrivateSharedNotes()
        {
            TestNotes(false, true);
        }

        [Test]
        public void TestSharedSharedNotes()
        {
            TestNotes(true, true);
        }

        [Test]
        public void TestOrganisationSharedNotes()
        {
            var member = _createMemberContactsCommand.CreateContact(1);
            var noteCreator = _createEmployerContactsCommand.CreateContact(1, false);
            _allocationsCommand.CreateAllocation(noteCreator.Id, new Allocation {CreditId = _creditsCommand.GetCredit<ContactCredit>().Id, Quantity = 100});

            var organisation = noteCreator.Organisation;
            var noteReader = _createEmployerContactsCommand.CreateContact(2, organisation);
            _allocationsCommand.CreateAllocation(noteReader.Id, new Allocation {CreditId = _creditsCommand.GetCredit<ApplicantCredit>().Id, Quantity = 100});

            // Create a private note and a shared note.

            const string noteText1 = "Here's a new note";
            var note1 = new CandidateNote {CandidateId = member.Id, Text = noteText1};
            _candidateNotesCommand.CreateCandidateNote(noteCreator, note1);

            NoteCreationDelay();

            const string noteText2 = "A second note";
            var note2 = new CandidateNote { CandidateId = member.Id, Text = noteText2 };
            _candidateNotesCommand.CreateSharedCandidateNote(noteCreator, note2);

            // Check access.

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(true, note2, noteCreator);
            AssertNote(true, note2, noteReader);

            // The note creator leaves the company, so they cannot access their own shared notes.

            var otherOrganisation = new Organisation { Name = "OtherOrganisation" };
            _organisationsCommand.CreateOrganisation(otherOrganisation);

            _createEmployerContactsCommand.UpdateOrganisation(noteCreator, otherOrganisation);

            // Check access.

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(false, note2, noteCreator);
            AssertNote(true, note2, noteReader);

            // Create a private note while not part of the company.

            const string noteText3 = "A third note";
            var note3 = new CandidateNote {CandidateId = member.Id, Text = noteText3};
            _candidateNotesCommand.CreateCandidateNote(noteCreator, note3);

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(false, note2, noteCreator);
            AssertNote(true, note2, noteReader);
            AssertNote(true, note3, noteCreator);
            AssertNote(false, note3, noteReader);

            // Join again so can see original notes.

            _createEmployerContactsCommand.UpdateOrganisation(noteCreator, organisation);

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(true, note2, noteCreator);
            AssertNote(true, note2, noteReader);
            AssertNote(true, note3, noteCreator);
            AssertNote(false, note3, noteReader);

            // Change the latest note to shared, so now the second employer can see it.

            _candidateNotesCommand.ShareCandidateNote(noteCreator, note3);

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(true, note2, noteCreator);
            AssertNote(true, note2, noteReader);
            AssertNote(true, note3, noteCreator);
            AssertNote(true, note3, noteReader);

            // Make that note private once again.

            _candidateNotesCommand.UnshareCandidateNote(noteCreator, note3);

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(true, note2, noteCreator);
            AssertNote(true, note2, noteReader);
            AssertNote(true, note3, noteCreator);
            AssertNote(false, note3, noteReader);
        }

        [Test]
        public void TestNoteOwner()
        {
            var member = _createMemberContactsCommand.CreateContact(1);
            var noteCreator = _createEmployerContactsCommand.CreateContact(1, false);
            _allocationsCommand.CreateAllocation(noteCreator.Id, new Allocation {CreditId = _creditsCommand.GetCredit<ContactCredit>().Id, Quantity = 100});
            var noteReader = _createEmployerContactsCommand.CreateContact(2, noteCreator.Organisation);
            _allocationsCommand.CreateAllocation(noteReader.Id, new Allocation {CreditId = _creditsCommand.GetCredit<ApplicantCredit>().Id, Quantity = 100});

            // Add a private note.

            const string noteText1 = "Here's a new note";
            var note1 = new CandidateNote {CandidateId = member.Id, Text = noteText1};
            _candidateNotesCommand.CreateCandidateNote(noteCreator, note1);

            // Add a shared note.

            NoteCreationDelay();

            const string noteText2 = "A second note";
            var note2 = new CandidateNote { CandidateId = member.Id, Text = noteText2 };
            _candidateNotesCommand.CreateSharedCandidateNote(noteCreator, note2);

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(true, note2, noteCreator);
            AssertNote(true, note2, noteReader);

            // Try to update the first note text as the reader.

            const string updatedText = "Updated first note";
            var note = _candidateNotesCommand.GetCandidateNote(noteCreator, note1.Id);
            note.Text = updatedText;

            try
            {
                _candidateNotesCommand.UpdateCandidateNote(noteReader, note);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(true, note2, noteCreator);
            AssertNote(true, note2, noteReader);

            // Try to update the second note text as the reader.

            note = _candidateNotesCommand.GetCandidateNote(noteCreator, note2.Id);
            note.Text = updatedText;

            try
            {
                _candidateNotesCommand.UpdateCandidateNote(noteReader, note);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(true, note2, noteCreator);
            AssertNote(true, note2, noteReader);

            // Try to delete the first note.

            try
            {
                _candidateNotesCommand.DeleteCandidateNode(noteReader, note1.Id);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(true, note2, noteCreator);
            AssertNote(true, note2, noteReader);

            // Try to delete the second note.

            try
            {
                _candidateNotesCommand.DeleteCandidateNode(noteReader, note2.Id);
                Assert.Fail();
            }
            catch (NoteOwnerPermissionsException)
            {
            }

            AssertNote(true, note1, noteCreator);
            AssertNote(false, note1, noteReader);
            AssertNote(true, note2, noteCreator);
            AssertNote(true, note2, noteReader);
        }

        private void TestPrivateNote<T>(bool verified, int? credits)
            where T : Credit
        {
            TestNote<T>(verified, credits, false);
        }

        private void TestSharedNote<T>(bool verified, int? credits)
            where T : Credit
        {
            TestNote<T>(verified, credits, true);
        }

        private void TestNote<T>(bool verified, int? credits, bool isShared)
            where T : Credit
        {
            var member = _createMemberContactsCommand.CreateContact(1);
            var noteCreator = _createEmployerContactsCommand.CreateContact(1, verified);
            var noteReader = _createEmployerContactsCommand.CreateContact(2, noteCreator.Organisation);

            // Allocate some credits.

            if (credits != null)
                _allocationsCommand.CreateAllocation(noteCreator.Id, new Allocation {CreditId = _creditsCommand.GetCredit<T>().Id, Quantity = credits});
            _allocationsCommand.CreateAllocation(noteReader.Id, new Allocation {CreditId = _creditsCommand.GetCredit<T>().Id});

            // Add one note.

            const string text = "Here's a new note";
            var note = new CandidateNote {CandidateId = member.Id, Text = text};
            if (isShared)
                _candidateNotesCommand.CreateSharedCandidateNote(noteCreator, note);
            else
                _candidateNotesCommand.CreateCandidateNote(noteCreator, note);

            if (!isShared || credits != null)
            {
                AssertNote(true, note, noteCreator);
                AssertNote(isShared, note, noteReader);
            }
            else
            {
                Assert.IsTrue(credits == null);
            }
        }

        private void AssertNote(bool expectAccess, CandidateNote expectedNote, IEmployerContact noteReader)
        {
            var note = _candidateNotesCommand.GetCandidateNote(noteReader, expectedNote.Id);
            if (expectAccess)
                AssertNote(expectedNote, note);
            else
                Assert.IsNull(note);
        }

        private void TestNotes(bool isShared1, bool isShared2)
        {
            var member = _createMemberContactsCommand.CreateContact(1);
            var noteCreator = _createEmployerContactsCommand.CreateContact(1, false);
            _allocationsCommand.CreateAllocation(noteCreator.Id, new Allocation {CreditId = _creditsCommand.GetCredit<ContactCredit>().Id, Quantity = 100});
            var noteReader = _createEmployerContactsCommand.CreateContact(2, noteCreator.Organisation);
            _allocationsCommand.CreateAllocation(noteReader.Id, new Allocation {CreditId = _creditsCommand.GetCredit<ApplicantCredit>().Id, Quantity = 100});

            // Add one note.

            const string noteText1 = "Here's a new note";
            var note1 = new CandidateNote {CandidateId = member.Id, Text = noteText1};
            if (isShared1)
                _candidateNotesCommand.CreateSharedCandidateNote(noteCreator, note1);
            else
                _candidateNotesCommand.CreateCandidateNote(noteCreator, note1);

            AssertNote(true, note1, noteCreator);
            AssertNote(isShared1, note1, noteReader);

            // Add another note.

            NoteCreationDelay();

            const string noteText2 = "A second note";
            var note2 = new CandidateNote { CandidateId = member.Id, Text = noteText2 };
            if (isShared2)
                _candidateNotesCommand.CreateSharedCandidateNote(noteCreator, note2);
            else
                _candidateNotesCommand.CreateCandidateNote(noteCreator, note2);

            AssertNote(true, note2, noteCreator);
            AssertNote(isShared2, note2, noteReader);

            // Check the order.

            AssertNotes(noteCreator, member, note2, note1);
            if (isShared1)
            {
                if (isShared2)
                    AssertNotes(noteReader, member, note2, note1);
                else
                    AssertNotes(noteReader, member, note1);
            }
            else
            {
                if (isShared2)
                    AssertNotes(noteReader, member, note2);
                else
                    AssertNotes(noteReader, member);
            }

            // Update the first note text.

            NoteCreationDelay();
            const string updatedText = "Updated first note";
            note1.Text = updatedText;
            _candidateNotesCommand.UpdateCandidateNote(noteCreator, note1);

            AssertNote(true, note1, noteCreator);
            AssertNote(isShared1, note1, noteReader);
            AssertNote(true, note2, noteCreator);
            AssertNote(isShared2, note2, noteReader);

            // Order should be reversed now.

            AssertNotes(noteCreator, member, note1, note2);
            if (isShared1)
            {
                if (isShared2)
                    AssertNotes(noteReader, member, note1, note2);
                else
                    AssertNotes(noteReader, member, note1);
            }
            else
            {
                if (isShared2)
                    AssertNotes(noteReader, member, note2);
                else
                    AssertNotes(noteReader, member);
            }

            // Delete the first note.

            _candidateNotesCommand.DeleteCandidateNode(noteCreator, note1.Id);

            Assert.IsNull(_candidateNotesCommand.GetCandidateNote(noteCreator, note1.Id));
            Assert.IsNull(_candidateNotesCommand.GetCandidateNote(noteReader, note1.Id));
            AssertNote(true, note2, noteCreator);
            AssertNote(isShared2, note2, noteReader);

            AssertNotes(noteCreator, member, note2);
            if (isShared2)
                AssertNotes(noteReader, member, note2);
            else
                AssertNotes(noteReader, member);
        }

        private void AssertNotes(IEmployerContact employer, IMemberContact member, params CandidateNote[] expectedNotes)
        {
            var notes = _candidateNotesCommand.GetCandidateNotes(employer, member.Id);
            Assert.AreEqual(expectedNotes.Length, notes.Count);
            for (var index = 0; index < expectedNotes.Length; ++index)
                Assert.AreEqual(expectedNotes[index].Id, notes[index].Id);
        }

        private static void NoteCreationDelay()
        {
            Thread.Sleep(20);
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

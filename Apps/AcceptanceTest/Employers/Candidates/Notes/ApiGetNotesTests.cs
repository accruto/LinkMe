using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Notes
{
    [TestClass]
    public class ApiGetNotesTests
        : ApiNotesTests
    {
        private const string TextFormat = "This is the text {0}";

        private static readonly Random Random = new Random();

        [TestMethod]
        public void TestGetSharedNotes()
        {
            const int employerCount = 3;
            var employers = new Employer[employerCount];

            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            for (var index = 0; index < employerCount; ++index)
                employers[index] = _employerAccountsCommand.CreateTestEmployer(index, organisation);

            // Create notes.

            var member = _memberAccountsCommand.CreateTestMember(0);

            const int noteCount = 8;
            var notes = new CandidateNote[noteCount];
            var createdBys = new Employer[noteCount];

            for (var index = 0; index < noteCount; ++index)
            {
                var employer = employers[Random.Next(3)];
                notes[index] = CreateNote(employer, member.Id, index);
                createdBys[index] = employer;
            }

            foreach (var employer in employers)
            {
                LogIn(employer);
                for (var index = 0; index < noteCount; ++index)
                {
                    var model = Note(notes[index].Id);
                    AssertModel(employer == createdBys[index] ? null : createdBys[index], model, notes[index]);
                }

                var notesModel = Notes(member.Id);
                Assert.AreEqual(notes.Length, notesModel.Notes.Count);
                for (var index = 0; index < notes.Length; ++index)
                    AssertModel(employer == createdBys[index] ? null : createdBys[index], notesModel.Notes[index], notes[index]);
            }
        }

        private CandidateNote CreateNote(IEmployer employer, Guid memberId, int index)
        {
            var note = new CandidateNote {CandidateId = memberId, Text = string.Format(TextFormat, index), CreatedTime = DateTime.Now.AddMinutes(-1 * index)};
            _candidateNotesCommand.CreateSharedNote(employer, note);
            return note;
        }
    }
}

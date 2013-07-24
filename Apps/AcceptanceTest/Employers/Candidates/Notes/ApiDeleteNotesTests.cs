using System;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Notes
{
    [TestClass]
    public class ApiDeleteNotesTests
        : ApiNotesTests
    {
        [TestMethod]
        public void TestDeleteNote()
        {
            // Create member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create notes.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var note1 = new CandidateNote { Text = string.Format(NoteTextFormat, 1), CandidateId = member.Id, CreatedTime = DateTime.Now.AddDays(-1) };
            _candidateNotesCommand.CreatePrivateNote(employer, note1);
            var note2 = new CandidateNote { Text = string.Format(NoteTextFormat, 2), CandidateId = member.Id, CreatedTime = DateTime.Now.AddDays(-2) };
            _candidateNotesCommand.CreatePrivateNote(employer, note2);

            // Log in and delete the note.

            LogIn(employer);

            AssertModels(null, Notes(member.Id), note1, note2);
            AssertModel(null, Note(note1.Id), note1);
            AssertModel(null, Note(note2.Id), note2);

            var model = DeleteNote(note1);

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member.Id, note2);

            AssertModels(null, Notes(member.Id), note2);
            AssertModel(null, Note(note2.Id), note2);
        }

        private JsonNoteModel DeleteNote(ContenderNote note)
        {
            var url = new ReadOnlyApplicationUrl(_notesUrl, note.Id + "/delete");

            var response = Post(url);
            return new JavaScriptSerializer().Deserialize<JsonNoteModel>(response);
        }
    }
}

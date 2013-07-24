using System;
using System.Collections.Specialized;
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
    public class ApiEditNotesTests
        : ApiNotesTests
    {
        [TestMethod]
        public void TestEditText()
        {
            // Create member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create notes.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var note1 = new CandidateNote { Text = string.Format(NoteTextFormat, 1), CandidateId = member.Id, CreatedTime = DateTime.Now.AddDays(-1) };
            _candidateNotesCommand.CreatePrivateNote(employer, note1);
            var note2 = new CandidateNote { Text = string.Format(NoteTextFormat, 2), CandidateId = member.Id, CreatedTime = DateTime.Now.AddDays(-2) };
            _candidateNotesCommand.CreatePrivateNote(employer, note2);

            // Log in and edit tex.

            LogIn(employer);

            AssertModels(null, Notes(member.Id), note1, note2);
            AssertModel(null, Note(note1.Id), note1);
            AssertModel(null, Note(note2.Id), note2);

            var newText = string.Format(NoteTextFormat, 3);
            var model = EditNote(note1, newText, null);
            note1.Text = newText;

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member.Id, note1, note2);

            AssertModels(null, Notes(member.Id), note1, note2);
            AssertModel(null, Note(note1.Id), note1);
            AssertModel(null, Note(note2.Id), note2);
        }

        [TestMethod]
        public void TestEditNotSharedToShared()
        {
            // Create member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create notes.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var note1 = new CandidateNote { Text = string.Format(NoteTextFormat, 1), CandidateId = member.Id, CreatedTime = DateTime.Now.AddDays(-1) };
            _candidateNotesCommand.CreatePrivateNote(employer, note1);
            var note2 = new CandidateNote { Text = string.Format(NoteTextFormat, 2), CandidateId = member.Id, CreatedTime = DateTime.Now.AddDays(-2) };
            _candidateNotesCommand.CreatePrivateNote(employer, note2);

            // Log in and edit tex.

            LogIn(employer);

            AssertModels(null, Notes(member.Id), note1, note2);
            AssertModel(null, Note(note1.Id), note1);
            AssertModel(null, Note(note2.Id), note2);

            var model = EditNote(note1, null, true);
            note1.OrganisationId = employer.Organisation.Id;

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member.Id, note1, note2);

            AssertModels(null, Notes(member.Id), note1, note2);
            AssertModel(null, Note(note1.Id), note1);
            AssertModel(null, Note(note2.Id), note2);
        }

        [TestMethod]
        public void TestEditSharedToNotShared()
        {
            // Create member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create notes.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var note1 = new CandidateNote { Text = string.Format(NoteTextFormat, 1), CandidateId = member.Id, CreatedTime = DateTime.Now.AddDays(-1) };
            _candidateNotesCommand.CreatePrivateNote(employer, note1);
            _candidateNotesCommand.ShareNote(employer, note1);

            var note2 = new CandidateNote { Text = string.Format(NoteTextFormat, 2), CandidateId = member.Id, CreatedTime = DateTime.Now.AddDays(-2) };
            _candidateNotesCommand.CreatePrivateNote(employer, note2);

            // Log in and edit tex.

            LogIn(employer);

            AssertModels(null, Notes(member.Id), note1, note2);
            AssertModel(null, Note(note1.Id), note1);
            AssertModel(null, Note(note2.Id), note2);

            var model = EditNote(note1, null, false);
            note1.OrganisationId = null;

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member.Id, note1, note2);

            AssertModels(null, Notes(member.Id), note1, note2);
            AssertModel(null, Note(note1.Id), note1);
            AssertModel(null, Note(note2.Id), note2);
        }

        [TestMethod]
        public void TestEditBoth()
        {
            // Create member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create notes.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var note1 = new CandidateNote { Text = string.Format(NoteTextFormat, 1), CandidateId = member.Id, CreatedTime = DateTime.Now.AddDays(-1) };
            _candidateNotesCommand.CreatePrivateNote(employer, note1);
            var note2 = new CandidateNote { Text = string.Format(NoteTextFormat, 2), CandidateId = member.Id, CreatedTime = DateTime.Now.AddDays(-2) };
            _candidateNotesCommand.CreatePrivateNote(employer, note2);

            // Log in and edit tex.

            LogIn(employer);

            AssertModels(null, Notes(member.Id), note1, note2);
            AssertModel(null, Note(note1.Id), note1);
            AssertModel(null, Note(note2.Id), note2);

            var newText = string.Format(NoteTextFormat, 3);
            var model = EditNote(note1, newText, true);
            note1.Text = newText;
            note1.OrganisationId = employer.Organisation.Id;

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member.Id, note1, note2);

            AssertModels(null, Notes(member.Id), note1, note2);
            AssertModel(null, Note(note1.Id), note1);
            AssertModel(null, Note(note2.Id), note2);
        }

        private JsonResponseModel EditNote(ContenderNote note, string newText, bool? newIsShared)
        {
            var parameters = new NameValueCollection();
            if (newText != null)
                parameters.Add("text", newText);
            if (newIsShared != null)
                parameters.Add("isShared", newIsShared.ToString().ToLower());

            var url = new ReadOnlyApplicationUrl(_notesUrl, note.Id + "/edit");
            return Deserialize<JsonResponseModel>(Post(url, parameters));
        }
    }
}
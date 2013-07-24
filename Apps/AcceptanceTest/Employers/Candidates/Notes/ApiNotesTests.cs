using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Notes
{
    [TestClass]
    public abstract class ApiNotesTests
        : WebTestClass
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly ICandidateNotesCommand _candidateNotesCommand = Resolve<ICandidateNotesCommand>();
        protected readonly ICandidateNotesQuery _candidateNotesQuery = Resolve<ICandidateNotesQuery>();

        protected const string NoteTextFormat = "My note text{0}";
        protected ReadOnlyUrl _notesUrl;

        [TestInitialize]
        public void ApiNotesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _notesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/notes/api/");
        }

        protected JsonNotesModel Notes(Guid candidateId)
        {
            var parameters = new NameValueCollection {{ "candidateId", candidateId.ToString()}};
            var response = Post(_notesUrl, parameters);
            return new JavaScriptSerializer().Deserialize<JsonNotesModel>(response);
        }

        protected JsonNoteModel Note(Guid noteId)
        {
            var url = new ReadOnlyApplicationUrl(_notesUrl, noteId.ToString());
            var response = Post(url);
            return new JavaScriptSerializer().Deserialize<JsonNoteModel>(response);
        }

        protected static void AssertModels(IEmployer createdBy, JsonNotesModel model, params CandidateNote[] expectedNotes)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(expectedNotes.Length, model.Notes.Count);
            for (var index = 0; index < expectedNotes.Length; ++index)
                AssertModel(createdBy, model.Notes[index], expectedNotes[index]);
        }

        protected static void AssertModel(IEmployer createdBy, JsonNoteModel model, CandidateNote expectedNote)
        {
            AssertJsonSuccess(model);
            AssertModel(createdBy, model.Note, expectedNote);
        }

        protected static void AssertModel(IEmployer createdBy, NoteModel model, CandidateNote expectedNote)
        {
            if (expectedNote.Id != Guid.Empty)
                Assert.AreEqual(expectedNote.Id, model.Id);
            Assert.AreEqual(expectedNote.IsShared, model.IsShared);
            Assert.AreEqual(true, model.UpdatedTime != DateTime.MinValue);
            Assert.AreEqual(expectedNote.Text, model.Text);
            Assert.AreEqual(createdBy == null ? null : createdBy.FullName, model.CreatedBy);
        }

        protected void AssertNotes(IEmployer employer, Guid candidateId, params CandidateNote[] expectedNotes)
        {
            var notes = _candidateNotesQuery.GetNotes(employer, candidateId);
            Assert.AreEqual(expectedNotes.Length, notes.Count);
            for (var index = 0; index < expectedNotes.Length; ++index)
            {
                var expectedNote = expectedNotes[index];
                var note = (from n in notes where n.Text == expectedNote.Text select n).Single();
                Assert.IsNotNull(note);
                Assert.AreEqual(expectedNotes[index].IsShared, note.IsShared);
                Assert.AreEqual(expectedNotes[index].OrganisationId, note.IsShared ? employer.Organisation.Id : (Guid?)null);
                Assert.AreEqual(expectedNotes[index].RecruiterId, employer.Id);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Notes
{
    [TestClass]
    public class ApiNewNotesTests
        : ApiNotesTests
    {
        private ReadOnlyUrl _newNoteUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _newNoteUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/notes/api/new");
        }

        [TestMethod]
        public void TestNewNoteNotShared()
        {
            // Create member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create employer and log in.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            LogIn(employer);

            AssertModels(employer, Notes(member.Id));

            var note1 = new CandidateNote {Text = string.Format(NoteTextFormat, 1), RecruiterId = employer.Id };
            var model = CreateNote(new[] { member.Id }, note1.Text, false);

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member.Id, note1);

            AssertModels(null, Notes(member.Id), note1);

            // Add another.

            var note2 = new CandidateNote { Text = string.Format(NoteTextFormat, 2), RecruiterId = employer.Id };
            model = CreateNote(new[] { member.Id }, note2.Text, false);

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member.Id, note1, note2);

            AssertModels(null, Notes(member.Id), note2, note1);
        }

        [TestMethod]
        public void TestNewNoteShared()
        {
            // Create member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create employer and log in.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            LogIn(employer);

            AssertModels(employer, Notes(member.Id));

            var note1 = new CandidateNote { Text = string.Format(NoteTextFormat, 1), RecruiterId = employer.Id, OrganisationId = employer.Organisation.Id };
            var model = CreateNote(new[] { member.Id }, note1.Text, true);

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member.Id, note1);

            AssertModels(null, Notes(member.Id), note1);

            // Add another.

            var note2 = new CandidateNote { Text = string.Format(NoteTextFormat, 2), RecruiterId = employer.Id, OrganisationId = employer.Organisation.Id };
            model = CreateNote(new[] { member.Id }, note2.Text, true);

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member.Id, note1, note2);

            AssertModels(null, Notes(member.Id), note2, note1);
        }

        [TestMethod]
        public void TestNewNotesNotShared()
        {
            // Create member.

            var member0 = _memberAccountsCommand.CreateTestMember(0);
            var member1 = _memberAccountsCommand.CreateTestMember(1);

            // Create employer and log in.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            LogIn(employer);

            AssertModels(employer, Notes(member0.Id));
            AssertModels(employer, Notes(member1.Id));

            var note1 = new CandidateNote { Text = string.Format(NoteTextFormat, 1), RecruiterId = employer.Id };
            var model = CreateNote(new[] { member0.Id, member1.Id }, note1.Text, false);

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member0.Id, note1);
            AssertNotes(employer, member1.Id, note1);

            AssertModels(null, Notes(member0.Id), note1);
            AssertModels(null, Notes(member1.Id), note1);

            // Add another.

            var note2 = new CandidateNote { Text = string.Format(NoteTextFormat, 2), RecruiterId = employer.Id };
            model = CreateNote(new[] { member0.Id, member1.Id }, note2.Text, false);

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member0.Id, note1, note2);
            AssertNotes(employer, member1.Id, note1, note2);

            AssertModels(null, Notes(member0.Id), note2, note1);
            AssertModels(null, Notes(member1.Id), note2, note1);
        }

        [TestMethod]
        public void TestNewNotesShared()
        {
            // Create member.

            var member0 = _memberAccountsCommand.CreateTestMember(0);
            var member1 = _memberAccountsCommand.CreateTestMember(1);

            // Create employer and log in.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            LogIn(employer);

            AssertModels(employer, Notes(member0.Id));
            AssertModels(employer, Notes(member1.Id));

            var note1 = new CandidateNote { Text = string.Format(NoteTextFormat, 1), RecruiterId = employer.Id, OrganisationId = employer.Organisation.Id };
            var model = CreateNote(new[] { member0.Id, member1.Id }, note1.Text, true);

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member0.Id, note1);
            AssertNotes(employer, member1.Id, note1);

            AssertModels(null, Notes(member0.Id), note1);
            AssertModels(null, Notes(member1.Id), note1);

            // Add another.

            var note2 = new CandidateNote { Text = string.Format(NoteTextFormat, 2), RecruiterId = employer.Id, OrganisationId = employer.Organisation.Id };
            model = CreateNote(new[] { member0.Id, member1.Id }, note2.Text, true);

            // Assert.

            AssertJsonSuccess(model);
            AssertNotes(employer, member0.Id, note1, note2);
            AssertNotes(employer, member1.Id, note1, note2);

            AssertModels(null, Notes(member0.Id), note2, note1);
            AssertModels(null, Notes(member1.Id), note2, note1);
        }

        private JsonNoteModel CreateNote(IEnumerable<Guid> candidateIds, string text, bool isShared)
        {
            var parameters = new NameValueCollection
                                 {
                {"text", text},
                {"isShared", isShared.ToString().ToLower()}
            };
            foreach (var candidateId in candidateIds)
                parameters.Add("candidateId", candidateId.ToString());

            var response = Post(_newNoteUrl, parameters);
            return new JavaScriptSerializer().Deserialize<JsonNoteModel>(response);
        }
    }
}
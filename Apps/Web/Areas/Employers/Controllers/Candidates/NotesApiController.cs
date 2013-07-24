using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    [EnsureHttps, ApiEnsureAuthorized(UserType.Employer)]
    public class NotesApiController
        : EmployersApiController
    {
        private readonly IEmployersQuery _employersQuery;
        private readonly ICandidateNotesCommand _candidateNotesCommand;
        private readonly ICandidateNotesQuery _candidateNotesQuery;

        public NotesApiController(IEmployersQuery employersQuery, ICandidateNotesCommand candidateNotesCommand, ICandidateNotesQuery candidateNotesQuery)
        {
            _employersQuery = employersQuery;
            _candidateNotesCommand = candidateNotesCommand;
            _candidateNotesQuery = candidateNotesQuery;
        }

        [HttpPost]
        public ActionResult Notes(Guid candidateId)
        {
            try
            {
                var employer = CurrentEmployer;
                var notes = _candidateNotesQuery.GetNotes(employer, candidateId);

                // Get the names of the creators.

                var recruiterIds = (from n in notes select n.RecruiterId).Distinct().Except(new [] {employer.Id});
                var employers = _employersQuery.GetEmployers(recruiterIds).ToDictionary(e => e.Id, e => e.FullName);

                var models = from n in notes
                             orderby n.CreatedTime descending
                             select new NoteModel
                             {
                                 Id = n.Id,
                                 Text = n.Text,
                                 IsShared = n.IsShared,
                                 UpdatedTime = n.UpdatedTime,
                                 CreatedBy = n.RecruiterId == employer.Id
                                    ? null
                                    : employers[n.RecruiterId],
                                 CanUpdate = _candidateNotesCommand.CanUpdateNote(employer, n),
                                 CanDelete = _candidateNotesCommand.CanDeleteNote(employer, n),
                             };

                return Json(new JsonNotesModel {Notes = models.ToList()});
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult Note(Guid noteId)
        {
            try
            {
                var employer = CurrentEmployer;
                var note = _candidateNotesQuery.GetNote(employer, noteId);
                if (note == null)
                    return JsonNotFound("note");

                return Json(new JsonNoteModel
                {
                    Note = new NoteModel
                    {
                        Id = note.Id,
                        Text = note.Text,
                        IsShared = note.IsShared,
                        UpdatedTime = note.UpdatedTime,
                        CreatedBy = note.RecruiterId == employer.Id ? null : _employersQuery.GetEmployer(note.RecruiterId).FullName,
                        CanUpdate = _candidateNotesCommand.CanUpdateNote(employer, note),
                        CanDelete = _candidateNotesCommand.CanDeleteNote(employer, note),
                    }
                });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult NewNote(Guid[] candidateIds, string text, bool isShared)
        {
            try
            {
                var employer = CurrentEmployer;

                // Create them.

                foreach (var candidateId in candidateIds)
                {
                    var note = new CandidateNote
                    {
                        Text = text,
                        CandidateId = candidateId,
                    };

                    if (isShared)
                        _candidateNotesCommand.CreateSharedNote(employer, note);
                    else
                        _candidateNotesCommand.CreatePrivateNote(employer, note);
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult EditNote(Guid noteId, string text, bool? isShared)
        {
            try
            {
                var employer = CurrentEmployer;

                // Get the note.

                var note = _candidateNotesQuery.GetNote(employer, noteId);
                if (note == null)
                    return JsonNotFound("note");

                // Update the text.

                if (text != null && text != note.Text)
                {
                    note.Text = text;
                    _candidateNotesCommand.UpdateNote(employer, note);
                }

                // Updated the shared status.

                if (isShared != null)
                {
                    if (isShared.Value)
                        _candidateNotesCommand.ShareNote(employer, note);
                    else
                        _candidateNotesCommand.UnshareNote(employer, note);
                }

                return Json(new JsonNoteModel
                {
                    Note = new NoteModel
                    {
                        Id = note.Id,
                        Text = text,
                        IsShared = isShared == null ? note.IsShared : isShared.Value,
                        UpdatedTime = note.UpdatedTime,
                        CreatedBy = note.RecruiterId == employer.Id ? null : _employersQuery.GetEmployer(note.RecruiterId).FullName,
                        CanUpdate = _candidateNotesCommand.CanUpdateNote(employer, note),
                        CanDelete = _candidateNotesCommand.CanDeleteNote(employer, note),
                    }
                });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult DeleteNote(Guid noteId)
        {
            try
            {
                var employer = CurrentEmployer;
                _candidateNotesCommand.DeleteNote(employer, noteId);

                return Json(new JsonResponseModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }
    }
}

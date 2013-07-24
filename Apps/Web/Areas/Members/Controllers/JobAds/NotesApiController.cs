using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    [ApiEnsureAuthorized(UserType.Member)]
    public class NotesApiController
        : MembersApiController
    {
        private readonly IMemberJobAdNotesCommand _memberJobAdNotesCommand;
        private readonly IMemberJobAdNotesQuery _memberJobAdNotesQuery;

        public NotesApiController(IMemberJobAdNotesCommand memberJobAdNotesCommand, IMemberJobAdNotesQuery memberJobAdNotesQuery)
        {
            _memberJobAdNotesCommand = memberJobAdNotesCommand;
            _memberJobAdNotesQuery = memberJobAdNotesQuery;
        }

        [HttpPost]
        public ActionResult Notes(Guid jobAdId)
        {
            try
            {
                var member = CurrentMember;
                var notes = _memberJobAdNotesQuery.GetNotes(member, jobAdId);

                var models = from n in notes
                             orderby n.CreatedTime descending
                             select new NoteModel
                             {
                                 Id = n.Id,
                                 Text = n.Text,
                                 UpdatedTime = n.UpdatedTime,
                                 CanUpdate = true,
                                 CanDelete = true,
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
                var member = CurrentMember;
                var note = _memberJobAdNotesQuery.GetNote(member, noteId);
                if (note == null)
                    return JsonNotFound("note");

                return Json(new JsonNoteModel
                {
                    Note = new NoteModel
                    {
                        Id = note.Id,
                        Text = note.Text,
                        UpdatedTime = note.UpdatedTime,
                        CanUpdate = _memberJobAdNotesCommand.CanUpdateNote(member, note),
                        CanDelete = _memberJobAdNotesCommand.CanDeleteNote(member, note),
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
        public ActionResult NewNote(Guid[] jobAdIds, string text)
        {
            try
            {
                var member = CurrentMember;
                var noteId = Guid.Empty;

                // Create them.

                foreach (var jobAdId in jobAdIds)
                {
                    var note = new MemberJobAdNote
                    {
                        Text = text,
                        JobAdId = jobAdId,
                        MemberId = member.Id,
                    };

                    _memberJobAdNotesCommand.CreateNote(member, note);
                    noteId = note.Id;
                }

                return Json(new JsonConfirmationModel {Id = noteId});
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [HttpPost]
        public ActionResult EditNote(Guid noteId, string text)
        {
            try
            {
                // Get the note.

                var member = CurrentMember;
                var note = _memberJobAdNotesQuery.GetNote(member, noteId);
                if (note == null)
                    return JsonNotFound("note");

                // Update the text.

                if (text != null && text != note.Text)
                {
                    note.Text = text;
                    _memberJobAdNotesCommand.UpdateNote(member, note);
                }

                return Json(new JsonNoteModel
                {
                    Note = new NoteModel
                    {
                        Id = note.Id,
                        Text = text,
                        UpdatedTime = note.UpdatedTime,
                        CanUpdate = _memberJobAdNotesCommand.CanUpdateNote(member, note),
                        CanDelete = _memberJobAdNotesCommand.CanDeleteNote(member, note),
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
                _memberJobAdNotesCommand.DeleteNote(CurrentMember, noteId);
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

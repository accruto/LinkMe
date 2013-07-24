using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Asp.Json.Models
{
    public class NoteModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsShared { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string CreatedBy { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }

    public class JsonNoteModel
        : JsonResponseModel
    {
        public NoteModel Note { get; set; }
    }

    public class JsonNotesModel
        : JsonResponseModel
    {
        public IList<NoteModel> Notes { get; set; }
    }
}
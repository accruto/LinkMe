using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Query.Reports.Roles.Candidates
{
    public abstract class ResumeEvent
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime Time { get; set; }
        public Guid CandidateId { get; set; }
        public Guid ResumeId { get; set; }
    }

    public class ResumeUploadEvent
        : ResumeEvent
    {
    }

    public class ResumeReloadEvent
        : ResumeEvent
    {
    }

    public class ResumeEditEvent
        : ResumeEvent
    {
        public bool ResumeCreated { get; set; }
    }
}
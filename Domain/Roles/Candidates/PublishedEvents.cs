using System;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Domain.Roles.Candidates
{
    public static class PublishedEvents
    {
        public const string PropertiesChanged = "LinkMe.Domain.Roles.Candidates.PropertiesChanged";
        public const string CandidateUpdated = "LinkMe.Domain.Roles.Candidates.CandidateUpdated";
        public const string ResumeUpdated = "LinkMe.Domain.Roles.Candidates.ResumeUpdated";
        public const string ResumeUploaded = "LinkMe.Domain.Roles.Candidates.ResumeUploaded";
        public const string ResumeReloaded = "LinkMe.Domain.Roles.Candidates.ResumeReloaded";
        public const string ResumeEdited = "LinkMe.Domain.Roles.Candidates.ResumeEdited";
    }

    public class CandidateStatusChangedEventArgs
        : PropertyChangedEventArgs<CandidateStatus>
    {
        public CandidateStatusChangedEventArgs(CandidateStatus from, CandidateStatus to)
            : base(from, to)
        {
        }
    }

    public class DesiredJobTitleChangedEventArgs
        : PropertyChangedEventArgs<string>
    {
        public DesiredJobTitleChangedEventArgs(string from, string to)
            : base(from, to)
        {
        }
    }

    public class DesiredJobTypesChangedEventArgs
        : PropertyChangedEventArgs<JobTypes>
    {
        public DesiredJobTypesChangedEventArgs(JobTypes from, JobTypes to)
            : base(from, to)
        {
        }
    }

    public class IndustriesChangedEventArgs
        : PropertyChangedEventArgs<Guid[]>
    {
        public IndustriesChangedEventArgs(Guid[] from, Guid[] to)
            : base(from, to)
        {
        }
    }

    public class ResumeEventArgs
        : EventArgs
    {
        public Guid CandidateId { get; private set; }
        public Guid ResumeId { get; private set; }
        public bool ResumeCreated { get; private set; }

        public ResumeEventArgs(Guid candidateId, Guid resumeId, bool resumeCreated)
        {
            CandidateId = candidateId;
            ResumeId = resumeId;
            ResumeCreated = resumeCreated;
        }
    }

    public class ResumeUpdatedEventArgs
        : EventArgs
    {
        public Guid CandidateId { get; private set; }
        public Guid? ResumeId { get; private set; }

        public ResumeUpdatedEventArgs(Guid candidateId, Guid? resumeId)
        {
            CandidateId = candidateId;
            ResumeId = resumeId;
        }
    }
}

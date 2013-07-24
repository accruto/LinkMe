using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Candidates.Queries
{
    public interface IResumeReportsQuery
    {
        int GetResumes(DateTime time);
        int GetSearchableResumes(DateTime time);

        int GetNewResumes(DateTimeRange timeRange);
        int GetUploadedResumes(DateTimeRange timeRange);
        int GetReloadedResumes(DateTimeRange timeRange);
        int GetEditedResumes(DateTimeRange timeRange);

        int GetUpdatedResumes(DateTimeRange timeRange);
        IList<Guid> GetUpdatedResumeCandidateIds(DateTimeRange timeRange);

        IList<ResumeEvent> GetResumeEvents(Guid candidateId);
    }
}
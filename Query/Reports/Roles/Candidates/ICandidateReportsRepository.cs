using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Candidates
{
    public interface ICandidateReportsRepository
    {
        int GetResumes(DateTime time);
        int GetSearchableResumes(DateTime time);

        int GetNewResumes(DateTimeRange timeRange);
        int GetUploadedResumes(DateTimeRange timeRange);
        int GetReloadedResumes(DateTimeRange timeRange);
        int GetEditedResumes(DateTimeRange timeRange);

        int GetUpdatedResumes(DateTimeRange timeRange);
        IList<Guid> GetUpdatedResumeCandidateIds(DateTimeRange timeRange);

        IList<Guid> GetCandidateStatuses(CandidateStatus status);
        IList<Guid> GetEthnicStatuses(EthnicStatus status);

        void CreateResumeEvent(ResumeEvent evt);
        void DeleteResumeEvent(Guid evtId);
        IList<ResumeEvent> GetResumeEvents(Guid candidateId);
    }
}

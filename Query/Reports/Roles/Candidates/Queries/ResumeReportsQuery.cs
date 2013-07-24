using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Candidates.Queries
{
    public class ResumeReportsQuery
        : IResumeReportsQuery
    {
        private readonly ICandidateReportsRepository _repository;

        public ResumeReportsQuery(ICandidateReportsRepository repository)
        {
            _repository = repository;
        }

        int IResumeReportsQuery.GetResumes(DateTime time)
        {
            return _repository.GetResumes(time);
        }

        int IResumeReportsQuery.GetSearchableResumes(DateTime time)
        {
            return _repository.GetSearchableResumes(time);
        }

        int IResumeReportsQuery.GetNewResumes(DateTimeRange timeRange)
        {
            return _repository.GetNewResumes(timeRange);
        }

        int IResumeReportsQuery.GetUploadedResumes(DateTimeRange timeRange)
        {
            return _repository.GetUploadedResumes(timeRange);
        }

        int IResumeReportsQuery.GetReloadedResumes(DateTimeRange timeRange)
        {
            return _repository.GetReloadedResumes(timeRange);
        }

        int IResumeReportsQuery.GetEditedResumes(DateTimeRange timeRange)
        {
            return _repository.GetEditedResumes(timeRange);
        }

        int IResumeReportsQuery.GetUpdatedResumes(DateTimeRange timeRange)
        {
            return _repository.GetUpdatedResumes(timeRange);
        }

        IList<Guid> IResumeReportsQuery.GetUpdatedResumeCandidateIds(DateTimeRange timeRange)
        {
            return _repository.GetUpdatedResumeCandidateIds(timeRange);
        }

        IList<ResumeEvent> IResumeReportsQuery.GetResumeEvents(Guid candidateId)
        {
            return _repository.GetResumeEvents(candidateId);
        }
    }
}
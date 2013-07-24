using System;
using LinkMe.Query.Reports.Roles.Candidates;
using LinkMe.Query.Reports.Roles.Candidates.Commands;

namespace LinkMe.Apps.Agents.Users.Members.Handlers
{
    public class ResumesHandler
        : IResumesHandler
    {
        private readonly IResumeReportsCommand _resumeReportsCommand;

        public ResumesHandler(IResumeReportsCommand resumeReportsCommand)
        {
            _resumeReportsCommand = resumeReportsCommand;
        }

        void IResumesHandler.OnResumeUploaded(Guid candidateId, Guid resumeId)
        {
            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidateId, ResumeId = resumeId });
        }

        void IResumesHandler.OnResumeReloaded(Guid candidateId, Guid resumeId)
        {
            _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidateId, ResumeId = resumeId });
        }

        void IResumesHandler.OnResumeEdited(Guid candidateId, Guid resumeId, bool resumeCreated)
        {
            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidateId, ResumeId = resumeId, ResumeCreated = resumeCreated });
        }
    }
}

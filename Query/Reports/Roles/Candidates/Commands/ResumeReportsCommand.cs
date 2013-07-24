using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Query.Reports.Roles.Candidates.Commands
{
    public class ResumeReportsCommand
        : IResumeReportsCommand
    {
        private readonly ICandidateReportsRepository _repository;

        public ResumeReportsCommand(ICandidateReportsRepository repository)
        {
            _repository = repository;
        }

        void IResumeReportsCommand.CreateResumeEvent(ResumeEvent evt)
        {
            evt.Prepare();
            evt.Validate();
            _repository.CreateResumeEvent(evt);
        }

        void IResumeReportsCommand.DeleteResumeEvent(Guid evtId)
        {
            _repository.DeleteResumeEvent(evtId);
        }
    }
}

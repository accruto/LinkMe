using System;

namespace LinkMe.Query.Reports.Roles.Candidates.Commands
{
    public interface IResumeReportsCommand
    {
        void CreateResumeEvent(ResumeEvent evt);
        void DeleteResumeEvent(Guid evtId);
    }
}

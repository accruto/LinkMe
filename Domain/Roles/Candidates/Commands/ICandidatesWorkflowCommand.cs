using System;

namespace LinkMe.Domain.Roles.Candidates.Commands
{
    public interface ICandidatesWorkflowCommand
    {
        #region Status Workflow

        Guid? GetStatusWorkflowId(Guid candidateId);
        void AddStatusWorkflow(Guid candidateId, Guid workflowId);
        void DeleteStatusWorkflow(Guid candidateId);

        #endregion

        #region Suggested Jobs Workflow

        Guid? GetSuggestedJobsWorkflowId(Guid candidateId);
        void AddSuggestedJobsWorkflow(Guid candidateId, Guid workflowId);
        void DeleteSuggestedJobsWorkflow(Guid candidateId);

        #endregion

        #region ActivationEmail Workflow

        Guid? GetActivationEmailWorkflowId(Guid memberId);
        void AddActivationEmailWorkflow(Guid memberId, Guid workflowId);
        void DeleteActivationEmailWorkflow(Guid memberId);

        #endregion
    }
}
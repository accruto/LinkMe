using System;

namespace LinkMe.Domain.Roles.Candidates.Commands
{
    public class CandidatesWorkflowCommand
        : ICandidatesWorkflowCommand
    {
        private readonly ICandidatesRepository _repository;

        public CandidatesWorkflowCommand(ICandidatesRepository repository)
        {
            _repository = repository;
        }

        Guid? ICandidatesWorkflowCommand.GetStatusWorkflowId(Guid candidateId)
        {
            return _repository.GetStatusWorkflowId(candidateId);
        }

        void ICandidatesWorkflowCommand.AddStatusWorkflow(Guid candidateId, Guid workflowId)
        {
            _repository.AddStatusWorkflow(candidateId, workflowId);
        }

        void ICandidatesWorkflowCommand.DeleteStatusWorkflow(Guid candidateId)
        {
            _repository.DeleteStatusWorkflow(candidateId);
        }

        Guid? ICandidatesWorkflowCommand.GetSuggestedJobsWorkflowId(Guid candidateId)
        {
            return _repository.GetSuggestedJobsWorkflowId(candidateId);
        }

        void ICandidatesWorkflowCommand.AddSuggestedJobsWorkflow(Guid candidateId, Guid workflowId)
        {
            _repository.AddSuggestedJobsWorkflow(candidateId, workflowId);
        }

        void ICandidatesWorkflowCommand.DeleteSuggestedJobsWorkflow(Guid candidateId)
        {
            _repository.DeleteSuggestedJobsWorkflow(candidateId);
        }

        void ICandidatesWorkflowCommand.AddActivationEmailWorkflow(Guid memberId, Guid workflowId)
        {
            _repository.AddActivationEmailWorkflow(memberId, workflowId);
        }

        void ICandidatesWorkflowCommand.DeleteActivationEmailWorkflow(Guid memberId)
        {
            _repository.DeleteActivationEmailWorkflow(memberId);
        }

        Guid? ICandidatesWorkflowCommand.GetActivationEmailWorkflowId(Guid memberId)
        {
            return _repository.GetActivationEmailWorkflowId(memberId);
        }
    }
}
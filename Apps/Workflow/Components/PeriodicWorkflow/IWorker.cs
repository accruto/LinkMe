using System;

namespace LinkMe.Workflow.Components.PeriodicWorkflow
{
    public interface IWorker
    {
        Guid? GetWorkflowId(Guid userId);
        void AttachWorkflow(Guid userId, Guid workflowId);
        void DetachWorkflow(Guid userId);
        void Run(Guid userId, DateTime lastRunTime);
    }
}

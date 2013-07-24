using System;
using System.Workflow.Activities;

namespace LinkMe.Workflow.Design.PeriodicWorkflow
{
    [ExternalDataExchange]
    public interface IDataExchange
    {
        event EventHandler<DelayEventArgs> DelayChanged;

        void Run(Guid userId, DateTime lastRunTime);
        void CompleteWorkflow(Guid userId);
    }
}
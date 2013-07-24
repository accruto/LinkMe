using System;
using System.Workflow.Activities;

namespace LinkMe.Workflow.Design.PeriodicWorkflow
{
    [Serializable]
    public class DelayEventArgs
        : ExternalDataEventArgs
    {
        public TimeSpan Delay { get; set; }

        public DelayEventArgs(Guid workflowId, TimeSpan delay)
            : base(workflowId)
        {
            Delay = delay;
        }
    }
}
using System;
using System.Workflow.Activities;

namespace LinkMe.Workflow.Design.PeriodicWorkflow
{
    public sealed partial class PeriodicWorkflow : SequentialWorkflowActivity
    {
        public TimeSpan Delay { get; set; }
        public Guid UserId { get; set; }
        public DateTime LastRunTime { get; set; }

        public PeriodicWorkflow()
        {
            InitializeComponent();
        }

        private void SetLastRunTime_ExecuteCode(object sender, EventArgs e)
        {
            LastRunTime = DateTime.Now;
        }

        private void DelayChanged_Invoked(object sender, ExternalDataEventArgs e)
        {
            Delay = ((DelayEventArgs)e).Delay;
        }
    }
}
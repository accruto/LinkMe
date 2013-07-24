using System;
using System.Workflow.Activities;

namespace LinkMe.Workflow.Design.ActivationEmailWorkflow
{
    public sealed partial class ActivationEmailWorkflow : SequentialWorkflowActivity
    {
        public Guid UserId { get; set; }
        public int EmailSeqNo { get; set; }
        public TimeSpan Delay { get; set; }

        public ActivationEmailWorkflow()
        {
            InitializeComponent();
        }

        private void IncrementSeqNo_ExecuteCode(object sender, EventArgs e)
        {
            EmailSeqNo++;
        }

        private void StopSending_Invoked(object sender, ExternalDataEventArgs e)
        {
            Delay = TimeSpan.MaxValue;
        }
    }

}

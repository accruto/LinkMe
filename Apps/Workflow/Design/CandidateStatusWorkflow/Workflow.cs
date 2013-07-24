using System;
using System.Workflow.Activities;

namespace LinkMe.Workflow.Design.CandidateStatusWorkflow
{
    public sealed partial class Workflow : SequentialWorkflowActivity
    {
        public Guid CandidateId { get; set; }
        public State State { get; set; }
        public bool IgnoreTimeoutOnce { get; set; }
        public bool UseAvailableNowLongTimeout { get; set; }

        public TimeSpan ActivelyLookingConfirmationTimeout { get; set; }
        public TimeSpan ActivelyLookingResponseTimeout { get; set; }
        public TimeSpan AvailableNowConfirmationTimeout { get; set; }
        public TimeSpan AvailableNowResponseTimeout { get; set; }

        public Workflow()
        {
            InitializeComponent();
        }

        private void StatusChanged_Invoked(object sender, ExternalDataEventArgs e)
        {
            State = ((StateEventArgs)e).State;
        }

        private void ExecuteSetStatePassive(object sender, EventArgs e)
        {
            State = State.Passive;
        }

        private void ExecuteSetStateAvailableNow(object sender, EventArgs e)
        {
            State = State.AvailableNow;
        }

        private void ExecuteSetStateActivelyLooking(object sender, EventArgs e)
        {
            State = State.ActivelyLooking;
        }

        private void ExecuteSetAvailableNowLongTimeout(object sender, EventArgs e)
        {
            UseAvailableNowLongTimeout = true;
        }

        private void ExecuteSetAvailableNowShortTimeout(object sender, EventArgs e)
        {
            UseAvailableNowLongTimeout = false;
        }

        private void ExecuteSetActivelyLookingTimeoutZero(object sender, EventArgs e)
        {
            ActivelyLookingConfirmationTimeout = TimeSpan.Zero;
            IgnoreTimeoutOnce = false;
        }

        private void ExecuteSetAvailableNowTimeoutZero(object sender, EventArgs e)
        {
            AvailableNowConfirmationTimeout = TimeSpan.Zero;
            IgnoreTimeoutOnce = false;
        }
    }
}
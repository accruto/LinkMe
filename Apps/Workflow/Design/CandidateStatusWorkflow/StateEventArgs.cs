using System;
using System.Workflow.Activities;

namespace LinkMe.Workflow.Design.CandidateStatusWorkflow
{
    [Serializable]
    public class StateEventArgs
        : ExternalDataEventArgs
	{
        public State State { get; private set; }

        public StateEventArgs(Guid instanceId, State state)
            : base(instanceId)
        {
            State = state;
        }
	}
}

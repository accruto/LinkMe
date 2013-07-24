using System;

namespace LinkMe.Workflow.Design.CandidateStatusWorkflow
{
    [Serializable]
	public enum State
	{
        Passive,
        ActivelyLooking,
        AvailableNow
	}
}

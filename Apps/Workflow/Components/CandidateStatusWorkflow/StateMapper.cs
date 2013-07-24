using System;
using LinkMe.Domain;
using LinkMe.Workflow.Design.CandidateStatusWorkflow;

namespace LinkMe.Workflow.Components.CandidateStatusWorkflow
{
	static class StateMapper
	{
        public static CandidateStatus ToCandidateStatus(this State state)
        {
            switch (state)
            {
                case State.Passive:
                    return CandidateStatus.OpenToOffers;

                case State.ActivelyLooking:
                    return CandidateStatus.ActivelyLooking;

                case State.AvailableNow:
                    return CandidateStatus.AvailableNow;

                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }

        public static State ToState(this CandidateStatus status)
        {
            switch (status)
            {
                case CandidateStatus.Unspecified:
                case CandidateStatus.NotLooking:
                case CandidateStatus.OpenToOffers:
                    return State.Passive;

                case CandidateStatus.ActivelyLooking:
                    return State.ActivelyLooking;

                case CandidateStatus.AvailableNow:
                    return State.AvailableNow;

                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }
	}
}

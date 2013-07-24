using System;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Communications.Settings;

namespace LinkMe.Workflow.PeriodicWorkflow
{
    public static class Mapper
    {
        public static Frequency SuggestedJobsFrequency(this CandidateStatus status)
        {
            switch (status)
            {
                case CandidateStatus.Unspecified:
                case CandidateStatus.NotLooking:
                    return Frequency.Monthly;

                case CandidateStatus.OpenToOffers:
                    return Frequency.Weekly;

                case CandidateStatus.ActivelyLooking:
                case CandidateStatus.AvailableNow:
                    return Frequency.Daily;

                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }
    }
}

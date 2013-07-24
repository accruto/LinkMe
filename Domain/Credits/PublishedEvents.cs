using System;

namespace LinkMe.Domain.Credits
{
    public static class PublishedEvents
    {
        public const string CreditExercised = "LinkMe.Domain.Credits.CreditExercised";
    }

    public class CreditExercisedEventArgs
        : EventArgs
    {
        public readonly Guid CreditId;
        public readonly Guid OwnerId;
        public readonly bool AllocationAdjusted;

        public CreditExercisedEventArgs(Guid creditId, Guid ownerId, bool allocationAdjusted)
        {
            CreditId = creditId;
            OwnerId = ownerId;
            AllocationAdjusted = allocationAdjusted;
        }
    }
}

using System;

namespace LinkMe.Domain.Roles.JobAds
{
    public static class PublishedEvents
    {
        public const string JobAdOpened = "LinkMe.Domain.Roles.JobAds.JobAds.JobAdOpened";
        public const string JobAdUpdated = "LinkMe.Domain.Roles.JobAds.JobAds.JobAdUpdated";
        public const string JobAdClosed = "LinkMe.Domain.Roles.JobAds.JobAdClosed";
    }

    public class JobAdEventArgs
        : EventArgs
    {
        public Guid JobAdId { get; private set; }

        public JobAdEventArgs(Guid jobAdId)
        {
            JobAdId = jobAdId;
        }
    }

    public class JobAdOpenedEventArgs
        : JobAdEventArgs
    {
        public JobAdStatus PreviousStatus { get; private set; }

        public JobAdOpenedEventArgs(Guid jobAdId, JobAdStatus previousStatus)
            : base(jobAdId)
        {
            PreviousStatus = previousStatus;
        }
    }

    public class JobAdClosedEventArgs
        : JobAdEventArgs
    {
        public Guid? ClosedById { get; private set; }

        public JobAdClosedEventArgs(Guid jobAdId, Guid? closedById)
            : base(jobAdId)
        {
            ClosedById = closedById;
        }
    }
}

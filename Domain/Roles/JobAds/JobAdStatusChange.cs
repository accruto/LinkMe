using System;

namespace LinkMe.Domain.Roles.JobAds
{
    public class JobAdStatusChange
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public JobAdStatus PreviousStatus { get; set; }
        public JobAdStatus NewStatus { get; set; }
    }
}

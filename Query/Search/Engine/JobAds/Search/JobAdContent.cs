using System;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Query.Search.Engine.JobAds.Search
{
    public abstract class JobAdContent
        : Content
    {
        public JobAd JobAd { get; set; }
        public DateTime? LastRefreshTime { get; set; }
        public EmployerContent Employer { get; set; }
    }

    public class JobAdSearchContent
        : JobAdContent
    {
        public override bool IsSearchable
        {
            get { return JobAd.Status == JobAdStatus.Open; }
        }
    }
}
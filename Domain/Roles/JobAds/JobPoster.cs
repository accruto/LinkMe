using System;

namespace LinkMe.Domain.Roles.JobAds
{
    public class JobPoster
    {
        public JobPoster()
        {
            // This is on by default.

            ShowSuggestedCandidates = true;
        }

        public Guid Id { get; set; }
        public bool ShowSuggestedCandidates { get; set; }
        public bool SendSuggestedCandidates { get; set; }
    }
}
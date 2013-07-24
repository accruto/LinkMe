using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Resumes;

namespace LinkMe.Apps.Management.Areas.Communications.Models.Members
{
    public class NewsletterModel
        : CommunicationsModel
    {
        public Member Member { get; set; }
        public Candidate Candidate { get; set; }
        public Resume Resume { get; set; }
        public int TotalJobAds { get; set; }
        public int TotalViewed { get; set; }
        public int ProfilePercentComplete { get; set; }
        public IList<JobAd> SuggestedJobs { get; set; }
    }
}
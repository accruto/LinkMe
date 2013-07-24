using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.JobAds;
using System;

namespace LinkMe.Apps.Management.Areas.Communications.Models.Members
{
    public class JobSearchModel
    {
        public string Description { get; set; }
        public int TotalMatches { get; set; }
    }

    public class ReengagementModel
        : CommunicationsModel
    {
        public Member Member { get; set; }
        public Candidate Candidate { get; set; }
        public string ActivationCode { get; set; }
        public int ProfilePercentComplete { get; set; }
        public IList<JobAd> SuggestedJobs { get; set; }
        public IList<Guid> FeaturedJobs { get; set; }
        public JobSearchModel JobSearch { get; set; }
        public int TotalContactsLastWeek { get; set; }
        public int TotalContactsLastMonth { get; set; }
        public int TotalViewed { get; set; }
    }
}

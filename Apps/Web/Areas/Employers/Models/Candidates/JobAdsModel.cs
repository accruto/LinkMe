using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Web.Areas.Employers.Models.Candidates
{
    public class JobAdDataModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public JobAdStatus Status { get; set; }
        public ApplicantCountsModel ApplicantCounts { get; set; }
        public IDictionary<Guid, InternalApplication> Applications { get; set; }
    }

    public class JobAdsModel
    {
        public IList<JobAd> OpenJobAds { get; set; }
        public IList<JobAd> ClosedJobAds { get; set; }
        public IDictionary<Guid, ApplicantCountsModel> ApplicantCounts { get; set; }
    }
}

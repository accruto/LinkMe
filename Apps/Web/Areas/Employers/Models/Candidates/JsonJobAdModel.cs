using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Web.Areas.Employers.Models.Candidates
{
    public class ApplicantCountsModel
    {
        public int ShortListed { get; set; }
        public int New { get; set; }
        public int Rejected { get; set; }
    }

    public class JobAdApplicantsModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public ApplicantCountsModel ApplicantCounts { get; set; }
    }

    public class JsonJobAdModel
        : JsonResponseModel
    {
        public JobAdApplicantsModel JobAd { get; set; }
    }

    public class JsonJobAdsModel
        : JsonResponseModel
    {
        public IList<JobAdApplicantsModel> JobAds { get; set; }
    }
}

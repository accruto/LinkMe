using System;

namespace LinkMe.Web.Areas.Members.Models.JobAds
{
    public class SimilarJobsListModel
        : JobAdSearchListModel
    {
        public Guid JobAdId { get; set; }
    }
}

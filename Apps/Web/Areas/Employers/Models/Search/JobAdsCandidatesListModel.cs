using LinkMe.Domain.Roles.Contenders;
using LinkMe.Web.Areas.Employers.Models.Candidates;

namespace LinkMe.Web.Areas.Employers.Models.Search
{
    public class SuggestedCandidatesListModel
        : SearchListModel
    {
        public JobAdDataModel JobAd { get; set; }
    }

    public class ManageCandidatesListModel
        : CandidateListModel
    {
        public JobAdDataModel JobAd { get; set; }
        public ApplicantStatus ApplicantStatus { get; set; }
    }
}
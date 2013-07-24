using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Domain;
using LinkMe.Web.Areas.Members.Models.Profiles;

namespace LinkMe.Web.Areas.Members.Views.Status
{
    public class StatusUpdated
        : ViewPage<StatusUpdatedModel>
    {
        protected string GetCssIconClass(CandidateStatus status)
        {
            switch (status)
            {
                case CandidateStatus.ActivelyLooking:
                    return "actively-looking";
                    
                case CandidateStatus.AvailableNow:
                    return "available-now";
                    
                case CandidateStatus.OpenToOffers:
                    return "open-to-offers";

                default:
                    return "not-looking";
            }
        }
    }
}
using LinkMe.Apps.Asp.Mvc.Models;

namespace LinkMe.Web.Areas.Administrators.Models.Campaigns
{
    public class CampaignDeleteSummaryModel
    {
        private readonly Pagination _pagination;
        private readonly CampaignSummaryModel _campaignSummary;

        public CampaignDeleteSummaryModel(Pagination pagination, CampaignSummaryModel campaignSummary)
        {
            _pagination = pagination;
            _campaignSummary = campaignSummary;
        }

        public Pagination Pagination
        {
            get { return _pagination; }
        }

        public CampaignSummaryModel CampaignSummary
        {
            get { return _campaignSummary; }
        }
    }
}
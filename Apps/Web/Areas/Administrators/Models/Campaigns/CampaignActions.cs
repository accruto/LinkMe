using LinkMe.Domain.Roles.Communications.Campaigns;

namespace LinkMe.Web.Areas.Administrators.Models.Campaigns
{
    public class CampaignActions
    {
        private readonly Campaign _campaign;

        public CampaignActions(Campaign campaign)
        {
            _campaign = campaign;
        }

        public Campaign Campaign
        {
            get { return _campaign; }
        }
    }
}
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Roles.Communications.Campaigns;

namespace LinkMe.Web.Areas.Administrators.Models.Campaigns
{
    public abstract class CampaignCriteriaModel
    {
        private readonly Campaign _campaign;
        private readonly Criteria _criteria;
        private readonly bool _isReadOnly;

        protected CampaignCriteriaModel(Campaign campaign, Criteria criteria, bool isReadOnly)
        {
            _campaign = campaign;
            _criteria = criteria;
            _isReadOnly = isReadOnly;
        }

        public Campaign Campaign
        {
            get { return _campaign; }
        }

        public Criteria Criteria
        {
            get { return _criteria; }
        }

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }
    }
}
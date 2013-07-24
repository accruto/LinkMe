using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Roles.Communications.Campaigns;

namespace LinkMe.Web.Areas.Administrators.Models.Campaigns
{
    public class CampaignCriteriaResultsModel
    {
        private readonly CampaignCriteriaModel _campaignCriteria;
        private readonly IList<RegisteredUser> _users;

        public CampaignCriteriaResultsModel(CampaignCriteriaModel campaignCriteria, IList<RegisteredUser> users)
        {
            _campaignCriteria = campaignCriteria;
            _users = users;
        }

        public Campaign Campaign
        {
            get { return _campaignCriteria.Campaign; }
        }

        public Criteria Criteria
        {
            get { return _campaignCriteria.Criteria; }
        }

        public IList<RegisteredUser> Users
        {
            get { return _users; }
        }
    }
}
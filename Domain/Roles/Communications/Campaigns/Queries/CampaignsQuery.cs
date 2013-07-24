using System;
using System.Collections.Generic;
using LinkMe.Domain.Criterias;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Roles.Communications.Campaigns.Queries
{
    public class CampaignsQuery
        : ICampaignsQuery
    {
        private readonly ICampaignsRepository _repository;

        public CampaignsQuery(ICampaignsRepository repository)
        {
            _repository = repository;
        }

        Campaign ICampaignsQuery.GetCampaign(Guid id)
        {
            return _repository.GetCampaign(id);
        }

        Campaign ICampaignsQuery.GetCampaign(string name)
        {
            return _repository.GetCampaign(name);
        }

        RangeResult<Campaign> ICampaignsQuery.GetCampaigns(CampaignCategory? category, Range range)
        {
            return _repository.GetCampaigns(category, range);
        }

        IList<Campaign> ICampaignsQuery.GetCampaigns(CampaignCategory? category, CampaignStatus status)
        {
            return _repository.GetCampaigns(category, status);
        }

        Template ICampaignsQuery.GetTemplate(Guid id)
        {
            return _repository.GetTemplate(id);
        }

        Criteria ICampaignsQuery.GetCriteria(Guid campaignId)
        {
            return _repository.GetCriteria(campaignId);
        }
    }
}
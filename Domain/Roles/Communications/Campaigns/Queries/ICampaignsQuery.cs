using System;
using System.Collections.Generic;
using LinkMe.Domain.Criterias;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Roles.Communications.Campaigns.Queries
{
    public interface ICampaignsQuery
    {
        Campaign GetCampaign(Guid id);
        Campaign GetCampaign(string name);
        RangeResult<Campaign> GetCampaigns(CampaignCategory? category, Range range);
        IList<Campaign> GetCampaigns(CampaignCategory? category, CampaignStatus status);

        Template GetTemplate(Guid campaignId);
        Criteria GetCriteria(Guid campaignId);
    }
}

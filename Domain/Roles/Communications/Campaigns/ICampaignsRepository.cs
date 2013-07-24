using System;
using System.Collections.Generic;
using LinkMe.Domain.Criterias;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Roles.Communications.Campaigns
{
    public interface ICampaignsRepository
    {
        void CreateCampaign(Campaign campaign);
        void UpdateCampaign(Campaign campaign);
        void UpdateStatus(Guid id, CampaignStatus status);

        Campaign GetCampaign(Guid id);
        Campaign GetCampaign(string name);
        RangeResult<Campaign> GetCampaigns(CampaignCategory? category, Range range);
        IList<Campaign> GetCampaigns(CampaignCategory? category, CampaignStatus status);

        void CreateTemplate(Guid id, Template template);
        void UpdateTemplate(Guid id, Template template);
        Template GetTemplate(Guid id);

        void UpdateCriteria(Guid id, Criteria criteria);
        Criteria GetCriteria(Guid id);
    }
}

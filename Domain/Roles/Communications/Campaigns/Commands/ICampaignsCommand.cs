using System;
using LinkMe.Domain.Criterias;

namespace LinkMe.Domain.Roles.Communications.Campaigns.Commands
{
    public interface ICampaignsCommand
    {
        void CreateCampaign(Campaign campaign);
        void UpdateCampaign(Campaign campaign);
        void UpdateStatus(Campaign campaign, CampaignStatus status);

        void CreateTemplate(Guid campaignId, Template template);
        void UpdateTemplate(Guid campaignId, Template template);
        void UpdateCriteria(Guid campaignId, Criteria criteria);
    }
}
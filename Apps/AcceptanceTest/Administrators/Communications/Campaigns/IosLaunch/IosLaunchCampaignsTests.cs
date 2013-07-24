using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns.IosLaunch
{
    public abstract class IosLaunchCampaignsTests
        : CampaignsTests
    {
        protected Campaign CreateCampaign(Administrator administrator)
        {
            var campaign = new Campaign
            {
                Id = new Guid("{83B9911D-0AE8-4550-B64A-F0D2A97B2380}"),
                Category = CampaignCategory.Employer,
                Name = "iOS Launch",
                Query = null,
                Status = CampaignStatus.Draft,
                CommunicationCategoryId = _settingsQuery.GetCategory("Campaign").Id,
                CommunicationDefinitionId = _settingsQuery.GetDefinition("IosLaunchEmail").Id,
                CreatedBy = administrator.Id,
            };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }
    }
}

using System;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns.Reengagement
{
    public abstract class ReengagementCampaignsTests
        : CampaignsTests
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();

        protected Campaign CreateCampaign(Administrator administrator)
        {
            var campaign = new Campaign
            {
                Id = new Guid("{B3DAC669-5BEF-4C76-93F5-BD06F6C1AFB2}"),
                Category = CampaignCategory.Member,
                Name = "Member Reengagement",
                Query = null,
                Status = CampaignStatus.Draft,
                CommunicationCategoryId = _settingsQuery.GetCategory("Campaign").Id,
                CommunicationDefinitionId = _settingsQuery.GetDefinition("ReengagementEmail").Id,
                CreatedBy = administrator.Id,
            };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }
    }
}
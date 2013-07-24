using System;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns.Edm
{
    public abstract class EdmCampaignsTests
        : CampaignsTests
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();

        protected Campaign CreateCampaign(Administrator administrator)
        {
            var campaign = new Campaign
                               {
                                   Id = new Guid("{60D5D73C-CB5B-4214-AE54-6F6723945D54}"),
                                   Category = CampaignCategory.Member,
                                   Name = "EDM",
                                   Query = null,
                                   Status = CampaignStatus.Draft,
                                   CommunicationCategoryId = _settingsQuery.GetCategory("Campaign").Id,
                                   CommunicationDefinitionId = _settingsQuery.GetDefinition("EdmEmail").Id,
                                   CreatedBy = administrator.Id,
                               };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }
    }
}
using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Framework.Communications;

namespace LinkMe.Apps.Agents.Communications.Campaigns.Emails
{
    public interface ICampaignEmailsCommand
    {
        CampaignEmail CreateEmail(Campaign campaign, ICommunicationUser to);
        Communication GeneratePreview(CampaignEmail email, Guid campaignId);
        int Send(IEnumerable<CampaignEmail> emails, Guid campaignId, bool isTest);
    }
}

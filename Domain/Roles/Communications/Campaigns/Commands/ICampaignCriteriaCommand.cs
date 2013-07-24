using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Criterias;

namespace LinkMe.Domain.Roles.Communications.Campaigns.Commands
{
    public interface ICampaignCriteriaCommand
    {
        IList<RegisteredUser> Match(CampaignCategory category, Criteria criteria);
        IList<RegisteredUser> Match(CampaignCategory category, string query);
    }
}
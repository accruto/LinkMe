using System;
using System.Collections.Generic;

namespace LinkMe.Query.Search.Communications.Campaigns
{
    public interface ICampaignQueriesRepository
    {
        IList<Guid> GetUserIds(string query);
    }
}

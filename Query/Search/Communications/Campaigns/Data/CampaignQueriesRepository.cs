using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Query.Search.Communications.Campaigns.Data
{
    public class CampaignQueriesRepository
        : Repository, ICampaignQueriesRepository
    {
        public CampaignQueriesRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<Guid> ICampaignQueriesRepository.GetUserIds(string query)
        {
            using (var dc = CreateDataContext())
            {
                return dc.ExecuteQuery<Guid>(query).ToList();
            }
        }

        private CampaignsDataContext CreateDataContext()
        {
            return CreateContext().AsReadOnly();
        }

        private CampaignsDataContext CreateContext()
        {
            return CreateContext(c => new CampaignsDataContext(c));
        }
    }
}

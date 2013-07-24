using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Affiliations.Communities.Queries
{
    public interface ICommunitiesQuery
    {
        IList<Community> GetCommunities();
        Community GetCommunity(Guid id);
        Community GetCommunity(string name);
    }
}
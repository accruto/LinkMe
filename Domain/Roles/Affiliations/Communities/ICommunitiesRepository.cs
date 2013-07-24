using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Affiliations.Communities
{
    public interface ICommunitiesRepository
    {
        void CreateCommunity(Community community);
        void UpdateCommunity(Community community);
        IList<Community> GetCommunities();
        Community GetCommunity(Guid id);
        Community GetCommunity(string name);
    }
}

using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Affiliations.Communities.Queries
{
    public class CommunitiesQuery
        : ICommunitiesQuery
    {
        private readonly ICommunitiesRepository _repository;

        public CommunitiesQuery(ICommunitiesRepository repository)
        {
            _repository = repository;
        }

        IList<Community> ICommunitiesQuery.GetCommunities()
        {
            return _repository.GetCommunities();
        }

        Community ICommunitiesQuery.GetCommunity(Guid id)
        {
            return _repository.GetCommunity(id);
        }

        Community ICommunitiesQuery.GetCommunity(string name)
        {
            return _repository.GetCommunity(name);
        }
    }
}
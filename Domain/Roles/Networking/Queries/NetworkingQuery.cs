using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Networking.Queries
{
    public class NetworkingQuery
        : INetworkingQuery
    {
        private readonly INetworkingRepository _repository;

        public NetworkingQuery(INetworkingRepository repository)
        {
            _repository = repository;
        }

        bool INetworkingQuery.AreFirstDegreeLinked(Guid fromId, Guid toId)
        {
            return _repository.AreFirstDegreeLinked(fromId, toId);
        }

        IList<Guid> INetworkingQuery.GetFirstDegreeLinks(Guid fromId)
        {
            return _repository.GetFirstDegreeLinks(fromId);
        }
    }
}
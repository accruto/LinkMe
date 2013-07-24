using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Networking.Queries
{
    public interface INetworkingQuery
    {
        bool AreFirstDegreeLinked(Guid fromId, Guid toId);
        IList<Guid> GetFirstDegreeLinks(Guid fromId);
    }
}

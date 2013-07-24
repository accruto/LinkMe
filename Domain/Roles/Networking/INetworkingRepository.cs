using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Invitations;

namespace LinkMe.Domain.Roles.Networking
{
    public interface INetworkingRepository
        : IInvitationsRepository<NetworkingInvitation>
    {
        bool AreFirstDegreeLinked(Guid fromId, Guid toId);
        IList<Guid> GetFirstDegreeLinks(Guid fromId);

        void CreateFirstDegreeLink(Guid fromId, Guid toId);
        void DeleteFirstDegreeLink(Guid fromId, Guid toId);
        void IgnoreSecondDegreeLink(Guid fromId, Guid toId);
    }
}

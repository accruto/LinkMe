using System;

namespace LinkMe.Domain.Roles.Networking.Commands
{
    public interface INetworkingCommand
    {
        void CreateFirstDegreeLink(Guid fromId, Guid toId);
        void DeleteFirstDegreeLink(Guid fromId, Guid toId);
        void IgnoreSecondDegreeLink(Guid fromId, Guid toId);
    }
}
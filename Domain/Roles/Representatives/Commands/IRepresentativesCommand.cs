using System;

namespace LinkMe.Domain.Roles.Representatives.Commands
{
    public interface IRepresentativesCommand
    {
        void CreateRepresentative(Guid representeeId, Guid representativeId);
        void DeleteRepresentative(Guid representeeId, Guid representativeId);
    }
}
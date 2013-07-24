using System;

namespace LinkMe.Domain.Credits.Commands
{
    public interface IAllocationsCommand
    {
        void CreateAllocation(Allocation allocation);
        void Deallocate(Guid id);
    }
}
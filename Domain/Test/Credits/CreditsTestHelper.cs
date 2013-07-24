using System;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;

namespace LinkMe.Domain.Test.Credits
{
    public static class CreditsTestExtensions
    {
        public static void DeallocateAll(this IAllocationsCommand allocationsCommand, IAllocationsQuery allocationsQuery, Guid ownerId)
        {
            foreach (var allocation in allocationsQuery.GetActiveAllocations(ownerId))
                allocationsCommand.Deallocate(allocation.Id);
        }
    }
}

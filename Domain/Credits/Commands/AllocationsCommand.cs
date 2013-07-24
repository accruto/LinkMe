using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Credits.Commands
{
    public class AllocationsCommand
        : IAllocationsCommand
    {
        private readonly ICreditsRepository _repository;

        public AllocationsCommand(ICreditsRepository repository)
        {
            _repository = repository;
        }

        void IAllocationsCommand.CreateAllocation(Allocation allocation)
        {
            // Make sure the expiry date is in fact a date.

            if (allocation.ExpiryDate != null)
                allocation.ExpiryDate = allocation.ExpiryDate.Value.Date;
            Prepare(allocation);

            _repository.CreateAllocation(allocation);
        }

        void IAllocationsCommand.Deallocate(Guid allocationId)
        {
            var allocation = _repository.GetAllocation(allocationId);
            if (allocation != null)
            {
                allocation.DeallocatedTime = DateTime.Now;
                _repository.UpdateAllocation(allocation);
            }
        }

        private static void Prepare(Allocation allocation)
        {
            allocation.Prepare();

            // Set the remaining quantity to the initial quantity when creating.

            allocation.RemainingQuantity = allocation.InitialQuantity;
            allocation.DeallocatedTime = null;
        }
    }
}
using System;

namespace LinkMe.Domain.Credits.Data
{
    internal static class Mappings
    {
        public enum AdjustmentType
        {
            OldAddition = 0,
            OldRemoval = 1,
            Addition = 2,
            Removal = 3,
            Modification = 4,
        }

        public static Credit Map(this CreditEntity entity)
        {
            // Create the specific type.

            var credit = CreateCredit(entity.name);
            if (credit != null)
            {
                credit.Id = entity.id;
                credit.Name = entity.name;
                credit.ShortDescription = entity.displayName;
                credit.Description = entity.description;
            }
            return credit;
        }

        public static Allocation Map(this CreditAllocationEntity entity)
        {
            return new Allocation
            {
                Id = entity.id,
                OwnerId = entity.ownerId,
                CreditId = entity.creditId,
                InitialQuantity = entity.initialQuantity,
                RemainingQuantity = entity.quantity,
                CreatedTime = entity.createdTime,
                ExpiryDate = entity.expiryDate,
                DeallocatedTime = entity.deallocatedTime,
                ReferenceId = entity.referenceId,
            };
        }

        public static CreditAllocationEntity Map(this Allocation allocation)
        {
            var entity = new CreditAllocationEntity { id = allocation.Id };
            allocation.MapTo(entity);
            return entity;
        }

        public static void MapTo(this Allocation allocation, CreditAllocationEntity entity)
        {
            entity.ownerId = allocation.OwnerId;
            entity.creditId = allocation.CreditId;
            entity.initialQuantity = allocation.InitialQuantity;
            entity.quantity = allocation.RemainingQuantity;
            entity.createdTime = allocation.CreatedTime;
            entity.expiryDate = allocation.ExpiryDate;
            entity.deallocatedTime = allocation.DeallocatedTime;
            entity.referenceId = allocation.ReferenceId;
        }

        public static CandidateAccessPurchaseEntity Map(this ExercisedCredit exercisedCredit)
        {
            return new CandidateAccessPurchaseEntity
            {
                id = exercisedCredit.Id,
                purchaseTime = exercisedCredit.Time,
                searcherId = exercisedCredit.ExercisedById,
                candidateId = exercisedCredit.ExercisedOnId,
                referenceId = exercisedCredit.ReferenceId,
                allocationId = exercisedCredit.AllocationId,
                adjustedAllocation = exercisedCredit.AdjustedAllocation,
            };
        }

        public static ExercisedCredit Map(this CandidateAccessPurchaseEntity entity, Guid creditId)
        {
            return new ExercisedCredit
            {
                Id = entity.id,
                Time = entity.purchaseTime,
                CreditId = creditId,
                ExercisedById = entity.searcherId,
                ExercisedOnId = entity.candidateId,
                ReferenceId = entity.referenceId,
                AllocationId = entity.allocationId,
                AdjustedAllocation = entity.adjustedAllocation,
            };
        }

        private static Credit CreateCredit(string name)
        {
            switch (name)
            {
                case "ContactCredit":
                    return new ContactCredit();

                case "ApplicantCredit":
                    return new ApplicantCredit();

                case "JobAdCredit":
                    return new JobAdCredit();

                default:
                    return null;
            }
        }
    }
}

using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Credits
{
    [Serializable]
    public class Allocation
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [IsSet]
        public Guid OwnerId { get; set; }
        [IsSet]
        public Guid CreditId { get; set; }

        [DefaultNow]
        public DateTime CreatedTime { get; set; }
        public DateTime? DeallocatedTime { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public int? InitialQuantity { get; set; }
        public int? RemainingQuantity { get; set; }

        public Guid? ReferenceId { get; set; }

        public bool HasExpired
        {
            get { return ExpiryDate == null ? false : ExpiryDate.Value < DateTime.Today; }
        }

        public bool NeverExpires
        {
            get { return ExpiryDate == null; }
        }

        public bool IsUnlimited
        {
            get { return RemainingQuantity == null; }
        }

        public bool IsActive
        {
            get { return !IsDeallocated && !HasExpired && (IsUnlimited || RemainingQuantity.Value > 0); }
        }

        public bool IsDeallocated
        {
            get { return DeallocatedTime == null ? false : DeallocatedTime.Value < DateTime.Now; }
        }
    }
}
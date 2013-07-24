using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Orders
{
    public abstract class CouponDiscount
    {
        public abstract decimal GetDiscount(decimal price);
    }

    public class PercentageCouponDiscount
        : CouponDiscount
    {
        public decimal Percentage { get; set; }

        public override decimal GetDiscount(decimal price)
        {
            // Reduce to 2 decimal places.

            return Math.Round(price * Percentage, 2);
        }

        public override bool Equals(object obj)
        {
            if (obj is PercentageCouponDiscount)
                return Equals(obj as PercentageCouponDiscount);
            return false;
        }

        public bool Equals(PercentageCouponDiscount other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.Percentage == Percentage;
        }

        public override int GetHashCode()
        {
            return Percentage.GetHashCode();
        }
    }

    public class FixedCouponDiscount
        : CouponDiscount
    {
        public decimal Amount { get; set; }

        public override decimal GetDiscount(decimal price)
        {
            return Math.Round(Amount, 2);
        }

        public override bool Equals(object obj)
        {
            if (obj is FixedCouponDiscount)
                return Equals(obj as FixedCouponDiscount);
            return false;
        }

        public bool Equals(FixedCouponDiscount other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.Amount == Amount;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
    }

    public class Coupon
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public CouponDiscount Discount { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool CanBeUsedOnce { get; set; }
        public IList<Guid> ProductIds { get; set; }
        public IList<Guid> RedeemerIds { get; set; }
    }
}

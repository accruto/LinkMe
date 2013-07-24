using System;
using LinkMe.Domain.Products;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Roles.Orders
{
    public abstract class AdjustmentAmount
    {
    }

    public class PercentageAdjustmentAmount
        : AdjustmentAmount
    {
        public decimal PercentageChange { get; set; }
    }

    public class FixedAdjustmentAmount
        : AdjustmentAmount
    {
        public decimal FixedChange { get; set; }
    }

    public abstract class OrderAdjustment
        : IPersistableAdjustment
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public decimal InitialPrice { get; set; }
        public decimal AdjustedPrice { get; set; }

        Guid? IPersistableAdjustment.ReferenceId
        {
            get { return GetReferenceId(); }
            set { SetReferenceId(value); }
        }

        string IPersistableAdjustment.Code
        {
            get { return GetCode(); }
            set { SetCode(value); }
        }

        AdjustmentAmount IPersistableAdjustment.Amount
        {
            get
            {
                return GetAmount();
            }
            set
            {
                if (value is PercentageAdjustmentAmount)
                    SetAmount((PercentageAdjustmentAmount)value);
                else
                    SetAmount((FixedAdjustmentAmount)value);
            }
        }

        protected virtual Guid? GetReferenceId()
        {
            return null;
        }

        protected virtual void SetReferenceId(Guid? value)
        {
        }

        protected virtual string GetCode()
        {
            return null;
        }

        protected virtual void SetCode(string value)
        {
        }

        protected abstract AdjustmentAmount GetAmount();

        protected virtual void SetAmount(PercentageAdjustmentAmount value)
        {
        }

        protected virtual void SetAmount(FixedAdjustmentAmount value)
        {
        }
    }

    public static class AdjustmentAmountExtensions
    {
        public static AdjustmentAmount CreateAdjustmentAmount(this CouponDiscount discount)
        {
            if (discount is PercentageCouponDiscount)
                return new PercentageAdjustmentAmount { PercentageChange = ((PercentageCouponDiscount)discount).Percentage };
            return new FixedAdjustmentAmount { FixedChange = ((FixedCouponDiscount)discount).Amount };
        }
    }

    public abstract class DiscountAdjustment
        : OrderAdjustment
    {
        public decimal Percentage { get; set; }

        protected override AdjustmentAmount GetAmount()
        {
            return new PercentageAdjustmentAmount { PercentageChange = Percentage };
        }

        protected override void SetAmount(PercentageAdjustmentAmount value)
        {
            Percentage = value.PercentageChange;
        }
    }

    public class CouponAdjustment
        : OrderAdjustment
    {
        public string CouponCode { get; set; }
        public Guid? ProductId { get; set; }
        public AdjustmentAmount Amount { get; set; }

        protected override string GetCode()
        {
            return CouponCode;
        }

        protected override void SetCode(string value)
        {
            CouponCode = value;
        }

        protected override AdjustmentAmount GetAmount()
        {
            return Amount;
        }

        protected override void SetAmount(PercentageAdjustmentAmount value)
        {
            Amount = value;
        }

        protected override void SetAmount(FixedAdjustmentAmount value)
        {
            Amount = value;
        }

        protected override Guid? GetReferenceId()
        {
            return ProductId;
        }

        protected override void SetReferenceId(Guid? value)
        {
            ProductId = value;
        }
    }

    public class BundleAdjustment
        : DiscountAdjustment
    {
    }

    public class TaxAdjustment
        : OrderAdjustment
    {
        public decimal TaxRate { get; set; }

        protected override AdjustmentAmount GetAmount()
        {
            return new PercentageAdjustmentAmount { PercentageChange = TaxRate };
        }

        protected override void SetAmount(PercentageAdjustmentAmount value)
        {
            TaxRate = value.PercentageChange;
        }
    }

    public class SurchargeAdjustment
        : OrderAdjustment
    {
        public decimal Surcharge { get; set; }
        public CreditCardType CreditCardType { get; set; }

        protected override string GetCode()
        {
            return CreditCardType.ToString();
        }

        protected override void SetCode(string value)
        {
            CreditCardType = (CreditCardType) Enum.Parse(typeof(CreditCardType), value, true);
        }

        protected override AdjustmentAmount GetAmount()
        {
            return new PercentageAdjustmentAmount { PercentageChange = Surcharge };
        }

        protected override void SetAmount(PercentageAdjustmentAmount value)
        {
            Surcharge = value.PercentageChange;
        }
    }
}

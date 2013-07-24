using System;

namespace LinkMe.Domain.Roles.Orders.Affiliations
{
    public class AdjustmentPersister
        : IAdjustmentPersister
    {
        string IAdjustmentPersister.GetAdjustmentType(OrderAdjustment adjustment)
        {
            return adjustment.GetType().Name;
        }

        string IAdjustmentPersister.GetAdjustmentType(Discount discount)
        {
            if (discount is BundleDiscount)
                return typeof(BundleAdjustment).Name;
            if (discount is VecciDiscount)
                return typeof(VecciDiscountAdjustment).Name;
            return typeof(DiscountAdjustment).Name;
        }

        string IAdjustmentPersister.GetAdjustmentType(Coupon coupon)
        {
            return typeof(CouponAdjustment).Name;
        }

        OrderAdjustment IAdjustmentPersister.CreateAdjustment(string type)
        {
            if (type == typeof(BundleAdjustment).Name)
                return new BundleAdjustment();
            if (type == typeof(VecciDiscountAdjustment).Name)
                return new VecciDiscountAdjustment();
            if (type == typeof(TaxAdjustment).Name)
                return new TaxAdjustment();
            if (type == typeof(SurchargeAdjustment).Name)
                return new SurchargeAdjustment();
            if (type == typeof(CouponAdjustment).Name)
                return new CouponAdjustment();
            return null;
        }
    }
}
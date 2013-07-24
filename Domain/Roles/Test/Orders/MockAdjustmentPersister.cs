using System;
using LinkMe.Domain.Roles.Orders;

namespace LinkMe.Domain.Roles.Test.Orders
{
    public class MockAdjustmentPersister
        : IAdjustmentPersister
    {
        string IAdjustmentPersister.GetAdjustmentType(OrderAdjustment adjustment)
        {
            return adjustment.GetType().Name;
        }

        string IAdjustmentPersister.GetAdjustmentType(Discount discount)
        {
            return typeof (BundleAdjustment).Name;
        }

        string IAdjustmentPersister.GetAdjustmentType(Coupon coupon)
        {
            return typeof (CouponAdjustment).Name;
        }

        OrderAdjustment IAdjustmentPersister.CreateAdjustment(string type)
        {
            if (type == typeof(BundleAdjustment).Name)
                return new BundleAdjustment();
            if (type == typeof(TaxAdjustment).Name)
                return new TaxAdjustment();
            if (type == typeof(SurchargeAdjustment).Name)
                return new SurchargeAdjustment();
            if (type == typeof(CouponAdjustment).Name)
                return new CouponAdjustment();
            throw new NotImplementedException();
        }
    }
}

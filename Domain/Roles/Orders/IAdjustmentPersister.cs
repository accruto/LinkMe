using System;

namespace LinkMe.Domain.Roles.Orders
{
    public interface IPersistableAdjustment
    {
        Guid? ReferenceId { get; set; }
        string Code { get; set; }
        AdjustmentAmount Amount { get; set; }
    }

    public interface IAdjustmentPersister
    {
        string GetAdjustmentType(OrderAdjustment adjustment);
        string GetAdjustmentType(Discount discount);
        string GetAdjustmentType(Coupon coupon);
        OrderAdjustment CreateAdjustment(string type);
    }
}

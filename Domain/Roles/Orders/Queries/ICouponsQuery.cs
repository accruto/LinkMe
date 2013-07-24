using System;

namespace LinkMe.Domain.Roles.Orders.Queries
{
    public interface ICouponsQuery
    {
        Coupon GetCoupon(Guid id);
        Coupon GetCoupon(string code);
    }
}

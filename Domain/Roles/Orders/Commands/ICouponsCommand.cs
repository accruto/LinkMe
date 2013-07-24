using System;

namespace LinkMe.Domain.Roles.Orders.Commands
{
    public interface ICouponsCommand
    {
        void CreateCoupon(Coupon coupon);
        void EnableCoupon(Guid id);
        void DisableCoupon(Guid id);
    }
}

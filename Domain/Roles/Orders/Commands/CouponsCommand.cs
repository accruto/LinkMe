using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Orders.Commands
{
    public class CouponsCommand
        : ICouponsCommand
    {
        private readonly IOrdersRepository _repository;

        public CouponsCommand(IOrdersRepository repository)
        {
            _repository = repository;
        }

        void ICouponsCommand.CreateCoupon(Coupon coupon)
        {
            coupon.Prepare();
            coupon.Validate();
            _repository.CreateCoupon(coupon);
        }

        void ICouponsCommand.EnableCoupon(Guid id)
        {
            _repository.EnableCoupon(id);
        }

        void ICouponsCommand.DisableCoupon(Guid id)
        {
            _repository.DisableCoupon(id);
        }
    }
}

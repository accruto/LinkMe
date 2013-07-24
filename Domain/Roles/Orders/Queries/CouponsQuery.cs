using System;

namespace LinkMe.Domain.Roles.Orders.Queries
{
    public class CouponsQuery
        : ICouponsQuery
    {
        private readonly IOrdersRepository _repository;

        public CouponsQuery(IOrdersRepository repository)
        {
            _repository = repository;
        }

        Coupon ICouponsQuery.GetCoupon(Guid id)
        {
            return _repository.GetCoupon(id);
        }

        Coupon ICouponsQuery.GetCoupon(string code)
        {
            return _repository.GetCoupon(code);
        }
    }
}

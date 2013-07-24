using System;
using System.Collections.Generic;
using LinkMe.Domain.Products;

namespace LinkMe.Domain.Roles.Orders.Commands
{
    public interface IOrderPricesCommand
    {
        void SetOrderPrices(Order order, IEnumerable<Guid> productIds, Coupon coupon, IEnumerable<Discount> discounts, CreditCardType? creditCardType);
    }
}
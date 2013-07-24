using System;
using System.Collections.Generic;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;

namespace LinkMe.Domain.Users.Employers.Orders.Commands
{
    public interface IEmployerOrdersCommand
    {
        Order PrepareOrder(IEnumerable<Guid> productIds, Coupon coupon, Discount discount, CreditCardType creditCardType);
        PurchaseReceipt PurchaseOrder(Guid ownerId, Order order, Purchaser purchaser, CreditCard creditCard);
    }
}

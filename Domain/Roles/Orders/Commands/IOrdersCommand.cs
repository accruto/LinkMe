using System;
using System.Collections.Generic;
using LinkMe.Domain.Products;

namespace LinkMe.Domain.Roles.Orders.Commands
{
    public interface IOrdersCommand
    {
        void ValidateCoupon(Coupon coupon, Guid? redeemerId, IEnumerable<Guid> productIds);
        Order PrepareOrder(IEnumerable<Guid> productIds, Coupon coupon, IEnumerable<Discount> discounts, CreditCardType? creditCardType);
        PurchaseReceipt RecordOrder(Guid ownerId, Order order, Purchaser purchaser, AppleReceipt receipt);
        PurchaseReceipt PurchaseOrder(Guid ownerId, Order order, Purchaser purchaser, CreditCard creditCard);
        RefundReceipt RefundOrder(Guid orderId);
    }
}
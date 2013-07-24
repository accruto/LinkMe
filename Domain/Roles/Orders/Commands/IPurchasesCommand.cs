using System;
using LinkMe.Domain.Products;

namespace LinkMe.Domain.Roles.Orders.Commands
{
    public interface IPurchasesCommand
    {
        CreditCardReceipt PurchaseOrder(Order order, Purchaser purchaser, CreditCard creditCard);
        RefundReceipt RefundOrder(Guid orderId, string externalTransactionId);
    }
}
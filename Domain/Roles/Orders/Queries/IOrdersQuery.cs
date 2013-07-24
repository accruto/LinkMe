using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Orders.Queries
{
    public interface IOrdersQuery
    {
        Order GetOrder(Guid id);
        Order GetOrder(string confirmationCode);
        Order GetPurchasedOrder(Guid id);
        IList<Order> GetOrders(Guid ownerId);
        IList<Order> GetOrders(IEnumerable<Guid> orderIds);
        IList<Order> GetPurchasedOrders(Guid ownerId);

        PurchaseReceipt GetPurchaseReceipt(Guid orderId);
        IList<Receipt> GetReceipts(Guid orderId);
    }
}
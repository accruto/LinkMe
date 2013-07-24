using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Orders.Queries
{
    public class OrdersQuery
        : IOrdersQuery
    {
        private readonly IOrdersRepository _repository;
        
        public OrdersQuery(IOrdersRepository repository)
        {
            _repository = repository;
        }

        Order IOrdersQuery.GetOrder(Guid id)
        {
            return _repository.GetOrder(id);
        }

        Order IOrdersQuery.GetOrder(string confirmationCode)
        {
            return _repository.GetOrder(confirmationCode);
        }

        Order IOrdersQuery.GetPurchasedOrder(Guid id)
        {
            return _repository.GetPurchasedOrder(id);
        }

        IList<Order> IOrdersQuery.GetOrders(Guid ownerId)
        {
            return _repository.GetOrders(ownerId);
        }

        IList<Order> IOrdersQuery.GetOrders(IEnumerable<Guid> orderIds)
        {
            return _repository.GetOrders(orderIds);
        }

        IList<Order> IOrdersQuery.GetPurchasedOrders(Guid ownerId)
        {
            return _repository.GetPurchasedOrders(ownerId);
        }

        PurchaseReceipt IOrdersQuery.GetPurchaseReceipt(Guid orderId)
        {
            return _repository.GetPurchaseReceipt(orderId);
        }

        IList<Receipt> IOrdersQuery.GetReceipts(Guid orderId)
        {
            return _repository.GetReceipts(orderId);
        }
    }
}
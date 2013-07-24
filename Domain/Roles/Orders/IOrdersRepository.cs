using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Orders
{
    public interface IOrdersRepository
    {
        void CreateOrder(Order order);
        Order GetOrder(Guid id);
        Order GetOrder(string confirmationCode);
        Order GetPurchasedOrder(Guid id);
        IList<Order> GetOrders(Guid orderId);
        IList<Order> GetOrders(IEnumerable<Guid> orderIds);
        IList<Order> GetPurchasedOrders(Guid ownerId);
        bool DoesOrderAdjustmentExist(OrderAdjustment adjustment);

        void CreateReceipt(Guid orderId, Receipt receipt);
        PurchaseReceipt GetPurchaseReceipt(Guid orderId);
        IList<Receipt> GetReceipts(Guid orderId);

        void CreatePurchaseRequest(Guid orderId, string transactionId, string provider, PurchaseRequest request);
        void CreatePurchaseResponse(Guid orderId, string transactionId, PurchaseResponse response);
        IList<PurchaseTransaction> GetPurchaseTransactions(Guid orderId);

        void CreateCoupon(Coupon coupon);
        Coupon GetCoupon(Guid id);
        Coupon GetCoupon(string code);
        void EnableCoupon(Guid id);
        void DisableCoupon(Guid id);
    }
}

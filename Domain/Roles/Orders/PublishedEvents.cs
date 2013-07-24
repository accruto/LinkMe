using System;

namespace LinkMe.Domain.Roles.Orders
{
    public static class PublishedEvents
    {
        public const string OrderPurchased = "LinkMe.Domain.Roles.Orders.OrderPurchased";
        public const string OrderRefunded = "LinkMe.Domain.Roles.Orders.OrderRefunded";
    }

    public class OrderPurchasedEventArgs
        : EventArgs
    {
        public readonly Order Order;
        public readonly PurchaseReceipt Receipt;

        public OrderPurchasedEventArgs(Order order, PurchaseReceipt receipt)
        {
            Order = order;
            Receipt = receipt;
        }
    }

    public class OrderRefundedEventArgs
        : EventArgs
    {
        public readonly Order Order;
        public readonly RefundReceipt Receipt;

        public OrderRefundedEventArgs(Order order, RefundReceipt receipt)
        {
            Order = order;
            Receipt = receipt;
        }
    }
}
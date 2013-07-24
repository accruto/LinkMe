using LinkMe.Domain.Roles.Orders;

namespace LinkMe.Apps.Agents.Domain.Roles.Orders.Handlers
{
    public interface IOrdersHandler
    {
        void OnOrderPurchased(Order order, PurchaseReceipt receipt);
    }
}
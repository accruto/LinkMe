using System;

namespace LinkMe.Domain.Roles.Orders.Commands
{
    public interface IPurchaseTransactionsCommand
    {
        void CreatePurchaseRequest(Guid orderId, string transactionId, string provider, PurchaseRequest request);
        void CreatePurchaseResponse(Guid orderId, string transactionId, PurchaseResponse response);
    }
}

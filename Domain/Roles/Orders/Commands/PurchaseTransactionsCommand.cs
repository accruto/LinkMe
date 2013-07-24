using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Orders.Commands
{
    public class PurchaseTransactionsCommand
        : IPurchaseTransactionsCommand
    {
        private readonly IOrdersRepository _repository;

        public PurchaseTransactionsCommand(IOrdersRepository repository)
        {
            _repository = repository;
        }

        void IPurchaseTransactionsCommand.CreatePurchaseRequest(Guid orderId, string transactionId, string provider, PurchaseRequest request)
        {
            request.Prepare();
            request.Validate();
            _repository.CreatePurchaseRequest(orderId, transactionId, provider, request);
        }

        void IPurchaseTransactionsCommand.CreatePurchaseResponse(Guid orderId, string transactionId, PurchaseResponse response)
        {
            response.Prepare();
            response.Validate();
            _repository.CreatePurchaseResponse(orderId, transactionId, response);
        }
    }
}

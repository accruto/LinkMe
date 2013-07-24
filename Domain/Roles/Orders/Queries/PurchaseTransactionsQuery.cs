using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Orders.Queries
{
    public class PurchaseTransactionsQuery
        : IPurchaseTransactionsQuery
    {
        private readonly IOrdersRepository _repository;

        public PurchaseTransactionsQuery(IOrdersRepository repository)
        {
            _repository = repository;
        }

        IList<PurchaseTransaction> IPurchaseTransactionsQuery.GetPurchaseTransactions(Guid orderId)
        {
            return _repository.GetPurchaseTransactions(orderId);
        }
    }
}

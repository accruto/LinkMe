using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Orders.Queries
{
    public interface IPurchaseTransactionsQuery
    {
        IList<PurchaseTransaction> GetPurchaseTransactions(Guid orderId);
    }
}

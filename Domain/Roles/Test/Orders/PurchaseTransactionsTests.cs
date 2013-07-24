using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Orders
{
    [TestClass]
    public class PurchaseTransactionsTests
        : TestClass
    {
        private readonly IPurchaseTransactionsCommand _purchaseTransactionsCommand = Resolve<IPurchaseTransactionsCommand>();
        private readonly IPurchaseTransactionsQuery _purchaseTransactionsQuery = Resolve<IPurchaseTransactionsQuery>();

        private const string Provider = "SecurePay";
        private const string RequestMessage = "<request></request>";
        private const string ResponseMessage = "<response></response>";

        [TestInitialize]
        public void PurchaseTransactionsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestPurchase()
        {
            var orderId = Guid.NewGuid();
            var transactionId = Guid.NewGuid().ToString();

            // Request.

            var request = new PurchaseRequest {Time = DateTime.Now, Message = RequestMessage};
            _purchaseTransactionsCommand.CreatePurchaseRequest(orderId, transactionId, Provider, request);
            AssertTransaction(orderId, transactionId, Provider, request, null, _purchaseTransactionsQuery.GetPurchaseTransactions(orderId));

            // Response.

            var response = new PurchaseResponse {Time = DateTime.Now, Message = ResponseMessage};
            _purchaseTransactionsCommand.CreatePurchaseResponse(orderId, transactionId, response);
            AssertTransaction(orderId, transactionId, Provider, request, response, _purchaseTransactionsQuery.GetPurchaseTransactions(orderId));
        }

        private static void AssertTransaction(Guid orderId, string transactionId, string provider, PurchaseRequest request, PurchaseResponse response, IList<PurchaseTransaction> transactions)
        {
            Assert.AreEqual(1, transactions.Count);
            var transaction = transactions[0];
            Assert.AreEqual(orderId, transaction.OrderId);
            Assert.AreEqual(transactionId, transaction.TransactionId);
            Assert.AreEqual(provider, transaction.Provider);
            Assert.AreEqual(request.Message, transaction.Request.Message);
            if (response == null)
                Assert.IsNull(transaction.Response);
            else
                Assert.AreEqual(response.Message, transaction.Response.Message);
        }
    }
}

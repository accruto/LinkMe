using System;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class StubPurchasesCommand
        : IPurchasesCommand
    {
        CreditCardReceipt IPurchasesCommand.PurchaseOrder(Order order, Purchaser purchaser, CreditCard creditCard)
        {
            // Save a copy.

            var receipt = new CreditCardReceipt
                              {
                                  Id = Guid.NewGuid(),
                                  Time = DateTime.Now,
                                  ExternalTransactionId = Guid.NewGuid().ToString(),
                                  ExternalTransactionTime = DateTime.Now,
                                  CreditCard = new CreditCardSummary
                                                   {
                                                       Pan = GetPan(creditCard.CardNumber),
                                                       Type = CreditCardType.Visa,
                                                   }
                              };

            return receipt;
        }

        RefundReceipt IPurchasesCommand.RefundOrder(Guid orderId, string externalTransactionId)
        {
            // Find it.

            var receipt = new RefundReceipt
                              {
                                  Id = Guid.NewGuid(),
                                  Time = DateTime.Now,
                                  ExternalTransactionId = Guid.NewGuid().ToString(),
                                  ExternalTransactionTime = DateTime.Now,
                              };

            return receipt;
        }

        private static string GetPan(string cardNumber)
        {
            return cardNumber.Substring(0, 6) + "..." + cardNumber.Substring(cardNumber.Length - 3, 3);
        }
    }
}
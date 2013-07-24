using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Roles.Test.Orders
{
    public class MockPurchasesCommand
        : IPurchasesCommand
    {
        private readonly IAdjustmentPersister _adjustmentPersister;
        private readonly IDictionary<Guid, Tuple<Order, IList<Receipt>>> _orders = new Dictionary<Guid, Tuple<Order, IList<Receipt>>>();

        public MockPurchasesCommand(IAdjustmentPersister adjustmentPersister)
        {
            _adjustmentPersister = adjustmentPersister;
        }

        CreditCardReceipt IPurchasesCommand.PurchaseOrder(Order order, Purchaser purchaser, CreditCard creditCard)
        {
            // Save a copy.

            var copiedOrder = Copy(order, _adjustmentPersister);

            var receipt = new CreditCardReceipt
            {
                Id = Guid.NewGuid(),
                Time = DateTime.Now,
                ExternalTransactionId = Guid.NewGuid().ToString(),
                ExternalTransactionTime = DateTime.Now,
                CreditCard = new CreditCardSummary
                {
                    Pan = creditCard.CardNumber.GetCreditCardPan(),
                    Type = CreditCardType.Visa,
                }
            };

            _orders[copiedOrder.Id] = new Tuple<Order, IList<Receipt>>(copiedOrder, new List<Receipt> {receipt});
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

            _orders[orderId].Item2.Add(receipt);
            return receipt;
        }

        private static Order Copy(Order order, IAdjustmentPersister adjustmentPersister)
        {
            return new Order
            {
                Id = order.Id,
                Price = order.Price,
                AdjustedPrice = order.AdjustedPrice,
                Currency = order.Currency,
                Items = Copy(order.Items),
                Adjustments = Copy(order.Adjustments, adjustmentPersister),
            };
        }

        private static IList<OrderItem> Copy(IEnumerable<OrderItem> items)
        {
            return (from i in items select Copy(i)).ToList();
        }

        private static OrderItem Copy(OrderItem item)
        {
            return new OrderItem
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Price = item.Price,
                Currency = item.Currency
            };
        }

        private static IList<OrderAdjustment> Copy(IEnumerable<OrderAdjustment> adjustments, IAdjustmentPersister adjustmentPersister)
        {
            return (from a in adjustments select Copy(a, adjustmentPersister)).ToList();
        }

        private static OrderAdjustment Copy(OrderAdjustment adjustment, IAdjustmentPersister adjustmentPersister)
        {
            var newAdjustment = adjustmentPersister.CreateAdjustment(adjustmentPersister.GetAdjustmentType(adjustment));
            newAdjustment.Id = adjustment.Id;
            newAdjustment.InitialPrice = adjustment.InitialPrice;
            newAdjustment.AdjustedPrice = adjustment.AdjustedPrice;

            var amount = ((IPersistableAdjustment) adjustment).Amount;
            if (amount is PercentageAdjustmentAmount)
                ((IPersistableAdjustment) newAdjustment).Amount = new PercentageAdjustmentAmount { PercentageChange = ((PercentageAdjustmentAmount) amount).PercentageChange };
            else
                ((IPersistableAdjustment) newAdjustment).Amount = new FixedAdjustmentAmount { FixedChange = ((FixedAdjustmentAmount)amount).FixedChange };
            ((IPersistableAdjustment)newAdjustment).Code = ((IPersistableAdjustment)adjustment).Code;
            ((IPersistableAdjustment)newAdjustment).ReferenceId = ((IPersistableAdjustment)adjustment).ReferenceId;
            return newAdjustment;
        }
    }
}
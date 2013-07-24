using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Orders.Commands
{
    public class OrdersCommand
        : IOrdersCommand
    {
        private readonly IOrdersRepository _repository;
        private readonly IOrderPricesCommand _orderPricesCommand;
        private readonly IProductsQuery _productsQuery;
        private readonly IAllocationsCommand _allocationsCommand;
        private readonly IAllocationsQuery _allocationsQuery;
        private readonly IPurchasesCommand _purchasesCommand;

        public OrdersCommand(IOrdersRepository repository, IOrderPricesCommand orderPricesCommand, IProductsQuery productsQuery, IAllocationsCommand allocationsCommand, IAllocationsQuery allocationsQuery, IPurchasesCommand purchasesCommand)
        {
            _repository = repository;
            _orderPricesCommand = orderPricesCommand;
            _productsQuery = productsQuery;
            _allocationsCommand = allocationsCommand;
            _allocationsQuery = allocationsQuery;
            _purchasesCommand = purchasesCommand;
        }

        void IOrdersCommand.ValidateCoupon(Coupon coupon, Guid? redeemerId, IEnumerable<Guid> productIds)
        {
            if (!coupon.IsEnabled)
                throw new CouponExpiredException();

            if (coupon.ExpiryDate != null && coupon.ExpiryDate.Value <= DateTime.Now)
                throw new CouponExpiredException();

            if (coupon.CanBeUsedOnce && _repository.DoesOrderAdjustmentExist(new CouponAdjustment { CouponCode = coupon.Code }))
                throw new CouponExpiredException();

            if (IsRedeemerRestricted(coupon, redeemerId))
                throw new CouponRedeemerException();

            if (IsProductRestricted(coupon, productIds))
                throw new CouponProductException();
        }

        Order IOrdersCommand.PrepareOrder(IEnumerable<Guid> productIds, Coupon coupon, IEnumerable<Discount> discounts, CreditCardType? creditCardType)
        {
            var order = new Order { Items = new List<OrderItem>() };

            // Add items for each product.

            var aproductIds = productIds as Guid[] ?? productIds.ToArray();
            foreach (var productId in aproductIds)
            {
                var product = _productsQuery.GetProduct(productId);
                order.Items.Add(new OrderItem { ProductId = productId, Price = product.Price, Currency = product.Currency });
            }

            // Set the price.

            _orderPricesCommand.SetOrderPrices(order, aproductIds, coupon, discounts, creditCardType);

            order.Prepare();
            order.Validate();

            return order;
        }

        PurchaseReceipt IOrdersCommand.PurchaseOrder(Guid ownerId, Order order, Purchaser purchaser, CreditCard creditCard)
        {
            return PurchaseOrder(ownerId, order, purchaser, creditCard);
        }

        PurchaseReceipt IOrdersCommand.RecordOrder(Guid ownerId, Order order, Purchaser purchaser, AppleReceipt receipt)
        {
            return RecordOrder(ownerId, order, purchaser, receipt);
        }

        RefundReceipt IOrdersCommand.RefundOrder(Guid orderId)
        {
            var order = _repository.GetOrder(orderId);

            var purchaseReceipt = _repository.GetReceipts(order.Id).OfType<PurchaseReceipt>().FirstOrDefault();
            if (purchaseReceipt == null)
                return null;

            var refundReceipt = _purchasesCommand.RefundOrder(order.Id, purchaseReceipt.ExternalTransactionId);

            // Save the receipt.

            _repository.CreateReceipt(order.Id, refundReceipt);

            // Deallocate any credits associated with the purchase.

            DeallocateCredits(order);

            // Fire events.

            var handlers = OrderRefunded;
            if (handlers != null)
                handlers(this, new OrderRefundedEventArgs(order, refundReceipt));

            return refundReceipt;
        }

        [Publishes(PublishedEvents.OrderPurchased)]
        public event EventHandler<OrderPurchasedEventArgs> OrderPurchased;

        [Publishes(PublishedEvents.OrderRefunded)]
        public event EventHandler<OrderRefundedEventArgs> OrderRefunded;

        private void AllocateCredits(Guid ownerId, Product product, Guid orderId)
        {
            var now = DateTime.Now;

            foreach (var adjustment in product.CreditAdjustments)
            {
                var expiryDate = GetExpiryDate(now, adjustment.Duration);
                _allocationsCommand.CreateAllocation(new Allocation
                {
                    OwnerId = ownerId,
                    CreditId = adjustment.CreditId,
                    ExpiryDate = expiryDate,
                    InitialQuantity = adjustment.Quantity,
                    ReferenceId = orderId
                });
            }
        }

        private void DeallocateCredits(Order order)
        {
            foreach (var allocation in _allocationsQuery.GetAllocationsByReferenceId(order.Id))
                _allocationsCommand.Deallocate(allocation.Id);
        }

        private static DateTime? GetExpiryDate(DateTime now, TimeSpan? duration)
        {
            if (duration == null)
                return null;
            return now.Add(duration.Value).Date;
        }

        private PurchaseReceipt PurchaseOrder(Guid ownerId, Order order, Purchaser purchaser, CreditCard creditCard)
        {
            // Save the order.

            order.OwnerId = ownerId;
            order.PurchaserId = purchaser.Id;
            _repository.CreateOrder(order);

            // Pay for the purchase.

            var receipt = _purchasesCommand.PurchaseOrder(order, purchaser, creditCard);

            // Save the receipt.

            receipt.Prepare();
            receipt.Validate();
            _repository.CreateReceipt(order.Id, receipt);

            // Allocate the credits.

            var products = (from i in order.Items select _productsQuery.GetProduct(i.ProductId)).ToList();
            foreach (var product in products)
                AllocateCredits(order.OwnerId, product, order.Id);

            // Fire events.

            var handlers = OrderPurchased;
            if (handlers != null)
                handlers(this, new OrderPurchasedEventArgs(order, receipt));

            return receipt;
        }

        private PurchaseReceipt RecordOrder(Guid ownerId, Order order, Purchaser purchaser, AppleReceipt receipt)
        {
            // Save the order.

            order.OwnerId = ownerId;
            order.PurchaserId = purchaser.Id;
            _repository.CreateOrder(order);

            // Save the receipt.

            receipt.Prepare();
            receipt.Validate();
            _repository.CreateReceipt(order.Id, receipt);

            // Allocate the credits.

            var products = (from i in order.Items select _productsQuery.GetProduct(i.ProductId)).ToList();
            foreach (var product in products)
                AllocateCredits(order.OwnerId, product, order.Id);

            // Fire events.

            var handlers = OrderPurchased;
            if (handlers != null)
                handlers(this, new OrderPurchasedEventArgs(order, receipt));

            return receipt;
        }

        private static bool IsRedeemerRestricted(Coupon coupon, Guid? redeemerId)
        {
            if (coupon.RedeemerIds == null || coupon.RedeemerIds.Count == 0)
                return false;
            if (redeemerId == null)
                return true;
            return !coupon.RedeemerIds.Contains(redeemerId.Value);
        }

        private static bool IsProductRestricted(Coupon coupon, IEnumerable<Guid> productIds)
        {
            // There must be some overlap in the product ids to be able to use the coupon.

            if (coupon.ProductIds == null || coupon.ProductIds.Count == 0)
                return false;
            var aproductIds = productIds == null ? null : productIds as Guid[] ?? productIds.ToArray();
            if (productIds == null || aproductIds.Count() == 0)
                return true;
            return !coupon.ProductIds.Intersect(aproductIds).Any();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Products;

namespace LinkMe.Domain.Roles.Orders.Data
{
    internal static class Mappings
    {
        internal enum ReceiptType
        {
            CreditCard = 0,
            Refund = 1,
        };

        public static ProductReceiptEntity Map(this Receipt receipt, Guid orderId)
        {
            var type = GetReceiptType(receipt);
            var entity = new ProductReceiptEntity
            {
                id = receipt.Id,
                orderId = orderId,
                time = receipt.Time,
                externalTransactionId = receipt.ExternalTransactionId,
                externalTransactionTime = receipt.ExternalTransactionTime,
                type = (int)type,
            };

            switch (type)
            {
                case ReceiptType.CreditCard:
                    ((CreditCardReceipt)receipt).MapTo(entity);
                    break;
            }

            return entity;
        }

        private static void MapTo(this CreditCardReceipt receipt, ProductReceiptEntity entity)
        {
            entity.creditCardPan = receipt.CreditCard.Pan;
            entity.creditCardType = (int?)receipt.CreditCard.Type;
        }

        public static Receipt Map(this ProductReceiptEntity entity)
        {
            switch ((ReceiptType)entity.type)
            {
                case ReceiptType.CreditCard:
                    return CreateCreditCardReceipt(entity);

                default:
                    return CreateRefundReceipt(entity);
            }
        }

        private static ReceiptType GetReceiptType(Receipt receipt)
        {
            if (receipt is CreditCardReceipt)
                return ReceiptType.CreditCard;
            return ReceiptType.Refund;
        }

        private static Receipt CreateCreditCardReceipt(ProductReceiptEntity entity)
        {
            return new CreditCardReceipt
            {
                Id = entity.id,
                Time = entity.externalTransactionTime,
                ExternalTransactionId = entity.externalTransactionId,
                ExternalTransactionTime = entity.externalTransactionTime,
                CreditCard = new CreditCardSummary
                {
                    Pan = entity.creditCardPan,
                    Type = (CreditCardType)entity.creditCardType,
                }
            };
        }

        private static Receipt CreateRefundReceipt(ProductReceiptEntity entity)
        {
            return new RefundReceipt
            {
                Id = entity.id,
                Time = entity.externalTransactionTime,
                ExternalTransactionId = entity.externalTransactionId,
                ExternalTransactionTime = entity.externalTransactionTime,
            };
        }

        public static ProductOrderEntity Map(this Order order, IAdjustmentPersister adjustmentPersister)
        {
            return new ProductOrderEntity
            {
                id = order.Id,
                confirmationCode = order.ConfirmationCode,
                ownerId = order.OwnerId,
                purchaserId = order.PurchaserId,
                time = order.Time,
                priceExclTax = order.Price,
                priceInclTax = order.AdjustedPrice,
                currency = order.Currency.Iso4217Code,
                ProductOrderItemEntities = order.Items == null ? null : order.Items.Map(order.Id),
                ProductOrderAdjustmentEntities = order.Adjustments == null ? null : order.Adjustments.Map(order.Id, adjustmentPersister),
            };
        }

        public static Order Map(this ProductOrderEntity entity, IAdjustmentPersister adjustmentPersister)
        {
            return new Order
            {
                Id = entity.id,
                ConfirmationCode = entity.confirmationCode,
                OwnerId = entity.ownerId,
                PurchaserId = entity.purchaserId,
                Time = entity.time,
                Price = entity.priceExclTax,
                AdjustedPrice = entity.priceInclTax,
                Currency = Currency.GetCurrency(entity.currency),
                Items = (from i in entity.ProductOrderItemEntities select i.Map()).ToList(),
                Adjustments = (from a in entity.ProductOrderAdjustmentEntities orderby a.rank select a.Map(adjustmentPersister)).ToList(),
            };
        }

        private static EntitySet<ProductOrderItemEntity> Map(this IEnumerable<OrderItem> items, Guid orderId)
        {
            var set = new EntitySet<ProductOrderItemEntity>();
            set.AddRange(from i in items select i.Map(orderId));
            return set;
        }

        private static EntitySet<ProductOrderAdjustmentEntity> Map(this IEnumerable<OrderAdjustment> adjustments, Guid orderId, IAdjustmentPersister adjustmentPersister)
        {
            var set = new EntitySet<ProductOrderAdjustmentEntity>();
            set.AddRange(adjustments.Select((a, rank) => a.Map(rank, orderId, adjustmentPersister)));
            return set;
        }

        private static ProductOrderItemEntity Map(this OrderItem item, Guid orderId)
        {
            return new ProductOrderItemEntity
            {
                id = item.Id,
                orderId = orderId,
                productId = item.ProductId,
                price = item.Price,
                currency = item.Currency.Iso4217Code,
            };
        }

        private static OrderItem Map(this ProductOrderItemEntity entity)
        {
            return new OrderItem
            {
                Id = entity.id,
                ProductId = entity.productId,
                Price = entity.price,
                Currency = Currency.GetCurrency(entity.currency),
            };
        }

        private static ProductOrderAdjustmentEntity Map(this OrderAdjustment adjustment, int rank, Guid orderId, IAdjustmentPersister adjustmentPersister)
        {
            var entity = new ProductOrderAdjustmentEntity
            {
                id = adjustment.Id,
                rank = rank,
                orderId = orderId,
                type = adjustmentPersister.GetAdjustmentType(adjustment),
                code = ((IPersistableAdjustment) adjustment).Code,
                referenceId = ((IPersistableAdjustment) adjustment).ReferenceId,
                initialPrice = adjustment.InitialPrice,
                adjustedPrice = adjustment.AdjustedPrice,
            };

            var amount = ((IPersistableAdjustment) adjustment).Amount;
            if (amount is PercentageAdjustmentAmount)
            {
                entity.fixedAmount = null;
                entity.percentageAmount = ((PercentageAdjustmentAmount)amount).PercentageChange;
            }
            else if (amount is FixedAdjustmentAmount)
            {
                entity.fixedAmount = ((FixedAdjustmentAmount)amount).FixedChange;
                entity.percentageAmount = null;
            }

            return entity;
        }

        private static OrderAdjustment Map(this ProductOrderAdjustmentEntity entity, IAdjustmentPersister adjustmentPersister)
        {
            var adjustment = adjustmentPersister.CreateAdjustment(entity.type);
            adjustment.Id = entity.id;
            adjustment.InitialPrice = entity.initialPrice;
            adjustment.AdjustedPrice = entity.adjustedPrice;
            ((IPersistableAdjustment)adjustment).Amount = entity.percentageAmount != null
                ? (AdjustmentAmount)new PercentageAdjustmentAmount { PercentageChange = entity.percentageAmount.Value }
                : new FixedAdjustmentAmount { FixedChange = entity.fixedAmount ?? 0 };
            ((IPersistableAdjustment) adjustment).Code = entity.code;
            ((IPersistableAdjustment) adjustment).ReferenceId = entity.referenceId;
            return adjustment;
        }

        public static PurchaseTransactionEntity Map(this PurchaseRequest request, Guid orderId, string transactionId, string provider)
        {
            return new PurchaseTransactionEntity
            {
                orderId = orderId,
                transactionId = transactionId,
                provider = provider,
                requestTime = request.Time,
                requestMessage = request.Message
            };
        }

        public static PurchaseTransaction Map(this PurchaseTransactionEntity entity)
        {
            return new PurchaseTransaction
            {
                OrderId = entity.orderId,
                TransactionId = entity.transactionId,
                Provider = entity.provider,
                Request = entity.requestTime == null
                    ? null
                    : new PurchaseRequest {Time = entity.requestTime.Value, Message = entity.requestMessage},
                Response = entity.responseTime == null
                    ? null
                    : new PurchaseResponse {Time = entity.responseTime.Value, Message = entity.responseMessage},
            };
        }

        public static Coupon Map(this ProductCouponEntity entity)
        {
            return new Coupon
            {
                Id = entity.id,
                Code = entity.code,
                Discount = entity.percentageDiscount != null
                    ? (CouponDiscount) new PercentageCouponDiscount { Percentage = entity.percentageDiscount.Value }
                    : entity.fixedDiscount != null
                        ? new FixedCouponDiscount { Amount = entity.fixedDiscount ?? 0 }
                        : null,
                IsEnabled = entity.enabled,
                ExpiryDate = entity.expiryDate,
                CanBeUsedOnce = entity.canBeUsedOnce,
                ProductIds = entity.ProductCouponProductEntities.Map(),
                RedeemerIds = entity.ProductCouponRedeemerEntities.Map(),
            };
        }

        private static IList<Guid> Map(this ICollection<ProductCouponProductEntity> entities)
        {
            return entities == null || entities.Count == 0
                ? null
                : (from e in entities select e.productId).ToList();
        }

        private static IList<Guid> Map(this ICollection<ProductCouponRedeemerEntity> entities)
        {
            return entities == null || entities.Count == 0
                ? null
                : (from e in entities select e.redeemerId).ToList();
        }

        public static ProductCouponEntity Map(this Coupon coupon)
        {
            return new ProductCouponEntity
            {
                id = coupon.Id,
                code = coupon.Code,
                percentageDiscount = coupon.Discount is PercentageCouponDiscount ? ((PercentageCouponDiscount)coupon.Discount).Percentage : (decimal?) null,
                fixedDiscount = coupon.Discount is FixedCouponDiscount ? ((FixedCouponDiscount)coupon.Discount).Amount : (decimal?)null,
                enabled = coupon.IsEnabled,
                expiryDate = coupon.ExpiryDate,
                canBeUsedOnce = coupon.CanBeUsedOnce,
                ProductCouponProductEntities = coupon.ProductIds.MapProductIds(coupon.Id),
                ProductCouponRedeemerEntities = coupon.RedeemerIds.MapRedeemerIds(coupon.Id),
            };
        }

        private static EntitySet<ProductCouponProductEntity> MapProductIds(this ICollection<Guid> productIds, Guid couponId)
        {
            if (productIds == null || productIds.Count == 0)
                return null;
            var set = new EntitySet<ProductCouponProductEntity>();
            set.AddRange(from i in productIds select new ProductCouponProductEntity { couponId = couponId, productId = i });
            return set;
        }

        private static EntitySet<ProductCouponRedeemerEntity> MapRedeemerIds(this ICollection<Guid> redeemerIds, Guid couponId)
        {
            if (redeemerIds == null || redeemerIds.Count == 0)
                return null;
            var set = new EntitySet<ProductCouponRedeemerEntity>();
            set.AddRange(from i in redeemerIds select new ProductCouponRedeemerEntity { couponId = couponId, redeemerId = i });
            return set;
        }
    }
}

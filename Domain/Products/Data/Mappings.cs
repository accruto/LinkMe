using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Products.Data
{
    internal static class Mappings
    {
        public static Product Map(this ProductEntity entity)
        {
            return new Product
            {
                Id = entity.id,
                Name = entity.name,
                Description = entity.description,
                IsEnabled = entity.enabled,
                UserTypes = (UserType)entity.userTypes,
                Price = entity.price,
                Currency = Currency.GetCurrency(entity.currency),
                CreditAdjustments = (from a in entity.ProductCreditAdjustmentEntities select a.Map()).ToList(),
            };
        }

        public static ProductEntity Map(this Product product)
        {
            return new ProductEntity
            {
                id = product.Id,
                name = product.Name,
                description = product.Description,
                enabled = product.IsEnabled,
                userTypes = (int)product.UserTypes,
                price = product.Price,
                currency = product.Currency.Iso4217Code,
                ProductCreditAdjustmentEntities = product.CreditAdjustments == null ? null : product.CreditAdjustments.Map(product.Id),
            };
        }

        private static ProductCreditAdjustment Map(this ProductCreditAdjustmentEntity entity)
        {
            return new ProductCreditAdjustment
            {
                CreditId = entity.creditId,
                Quantity = entity.quantity,
                Duration = entity.duration == null ? (TimeSpan?)null : new TimeSpan(entity.duration.Value),
            };
        }

        private static ProductCreditAdjustmentEntity Map(this ProductCreditAdjustment adjustment, Guid productId)
        {
            return new ProductCreditAdjustmentEntity
            {
                productId = productId,
                creditId = adjustment.CreditId,
                quantity = adjustment.Quantity,
                duration = adjustment.Duration == null ? (long?)null : adjustment.Duration.Value.Ticks,
            };
        }

        private static EntitySet<ProductCreditAdjustmentEntity> Map(this IEnumerable<ProductCreditAdjustment> adjustments, Guid productId)
        {
            var set = new EntitySet<ProductCreditAdjustmentEntity>();
            set.AddRange(from a in adjustments select a.Map(productId));
            return set;
        }
    }
}
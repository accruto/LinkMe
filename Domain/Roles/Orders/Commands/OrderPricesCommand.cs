using System;
using System.Collections.Generic;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;

namespace LinkMe.Domain.Roles.Orders.Commands
{
    public class OrderPricesCommand
        : IOrderPricesCommand
    {
        // Should go into configuration.

        private const decimal TaxRate = (decimal) 0.1;

        private readonly ICreditCardsQuery _creditCardsQuery;
        private readonly IAdjustmentPersister _adjustmentPersister;

        public OrderPricesCommand(ICreditCardsQuery creditCardsQuery, IAdjustmentPersister adjustmentPersister)
        {
            _creditCardsQuery = creditCardsQuery;
            _adjustmentPersister = adjustmentPersister;
        }

        void IOrderPricesCommand.SetOrderPrices(Order order, IEnumerable<Guid> productIds, Coupon coupon, IEnumerable<Discount> discounts, CreditCardType? creditCardType)
        {
            order.Adjustments = new List<OrderAdjustment>();

            // Coupon if needed.

            SetPrices(order, coupon);

            // Discount if needed.

            if (discounts != null)
                AdjustDiscounts(order, discounts);

            // GST

            AdjustTax(order);

            // Credit card surcharge for Amex

            if (creditCardType != null)
            {
                var surcharge = _creditCardsQuery.GetSurcharge(creditCardType.Value);
                if (surcharge != 0)
                    AdjustSurcharge(order, surcharge, creditCardType.Value);
            }
        }

        private void SetPrices(Order order, Coupon coupon)
        {
            // The total price comes from adding up individual purchases.

            decimal price = 0;
            decimal adjustedPrice = 0;
            Currency currency = null;
            Guid? couponProductId = null;

            foreach (var item in order.Items)
            {
                var itemPrice = GetPrice(item, ref currency);
                price += itemPrice;

                // Adjust the item price if the coupon is restricted to a product.

                if (coupon != null && coupon.Discount != null && couponProductId == null && coupon.ProductIds != null && coupon.ProductIds.Contains(item.ProductId))
                {
                    adjustedPrice += itemPrice - coupon.Discount.GetDiscount(itemPrice);
                    couponProductId = item.ProductId;
                }
                else
                {
                    adjustedPrice += itemPrice;
                }
            }

            // If the coupon is not restricted to a product then adjust the entire price.

            if (coupon != null && coupon.Discount != null && (coupon.ProductIds == null || coupon.ProductIds.Count == 0))
                adjustedPrice = price - coupon.Discount.GetDiscount(price);

            order.Price = price;
            order.AdjustedPrice = adjustedPrice;
            order.Currency = currency ?? Currency.AUD;

            if (coupon != null && coupon.Discount != null && order.AdjustedPrice != order.Price)
            {
                // Record the adjustment.

                var adjustment = (CouponAdjustment) _adjustmentPersister.CreateAdjustment(_adjustmentPersister.GetAdjustmentType(coupon));
                adjustment.InitialPrice = order.Price;
                adjustment.AdjustedPrice = order.AdjustedPrice;
                adjustment.Amount = coupon.Discount.CreateAdjustmentAmount();
                adjustment.CouponCode = coupon.Code;
                adjustment.ProductId = couponProductId;
                order.Adjustments.Add(adjustment);
            }
        }

        private static decimal GetPrice(OrderItem item, ref Currency currency)
        {
            if (currency == null)
            {
                currency = item.Currency;
                return item.Price;
            }

            if (currency == item.Currency)
                return item.Price;

            // Need to convert when there are more currencies ...

            return item.Price;
        }

        private void AdjustDiscounts(Order order, IEnumerable<Discount> discounts)
        {
            foreach (var discount in discounts)
                AdjustDiscount(order, discount);
        }

        private void AdjustDiscount(Order order, Discount discount)
        {
            // Calculate the price based on the original price of the products.

            var initialPrice = order.AdjustedPrice;
            order.AdjustedPrice = Adjust(order.AdjustedPrice, order.Price, -1 * discount.Percentage);

            // Record the adjustment.

            var adjustment = (DiscountAdjustment)_adjustmentPersister.CreateAdjustment(_adjustmentPersister.GetAdjustmentType(discount));
            adjustment.InitialPrice = initialPrice;
            adjustment.AdjustedPrice = order.AdjustedPrice;
            adjustment.Percentage = discount.Percentage;
            order.Adjustments.Add(adjustment);
        }

        private static void AdjustTax(Order order)
        {
            // The tax is based on the last adjusted price.

            var initialPrice = order.AdjustedPrice;
            order.AdjustedPrice = Adjust(initialPrice, initialPrice, TaxRate);

            // Record the adjustment.

            order.Adjustments.Add(new TaxAdjustment { InitialPrice = initialPrice, AdjustedPrice = order.AdjustedPrice, TaxRate = TaxRate });
        }

        private static void AdjustSurcharge(Order order, decimal surcharge, CreditCardType creditCardType)
        {
            // Calculate the price.

            var initialPrice = order.AdjustedPrice;
            order.AdjustedPrice = Adjust(initialPrice, initialPrice, surcharge);

            // Record the adjustment.

            order.Adjustments.Add(new SurchargeAdjustment { InitialPrice = initialPrice, AdjustedPrice = order.AdjustedPrice, Surcharge = surcharge, CreditCardType = creditCardType });
        }

        private static decimal Adjust(decimal initialPrice, decimal price, decimal factor)
        {
            // Reduce to 2 decimal places.

            return Math.Round(initialPrice + price * factor, 2);
        }
    }
}
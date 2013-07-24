using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Presentation.Domain.Products;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Affiliations;

namespace LinkMe.Apps.Presentation.Domain.Roles.Orders
{
    public static class OrdersExtensions
    {
        public static string GetOrderTimeDisplayText(this DateTime time)
        {
            return time.ToShortDateString() + " " + time.ToShortTimeString();
        }

        public static string GetAdjustmentTypeString(this OrderAdjustment adjustment)
        {
            if (adjustment is TaxAdjustment)
            {
                return "tax";
            }
            var surchargeAdjustment = adjustment as SurchargeAdjustment;
            if (surchargeAdjustment != null)
            {
                if (surchargeAdjustment.CreditCardType == CreditCardType.Amex)
                    return "surcharge";
            }
            if (adjustment is DiscountAdjustment)
            {
                if (adjustment is VecciDiscountAdjustment)
                    return "vecci-discount";
                return "discount";
            }

            return string.Empty;
        }

        public static string GetAdjustmentLabelText(this Order order, OrderAdjustment adjustment, IEnumerable<Product> products)
        {
            if (adjustment is TaxAdjustment)
                return "Add GST";

            var surchargeAdjustment = adjustment as SurchargeAdjustment;
            if (surchargeAdjustment != null)
            {
                if (surchargeAdjustment.CreditCardType == CreditCardType.Amex)
                    return "Add American Express surcharge (" + surchargeAdjustment.Surcharge.ToString("P1") + ")";
            }

            var discountAdjustment = adjustment as DiscountAdjustment;
            if (discountAdjustment != null)
            {
                if (discountAdjustment is BundleAdjustment)
                    return "Less " + discountAdjustment.Percentage.ToString("P0") + " bundle discount";
                if (discountAdjustment is VecciDiscountAdjustment)
                    return "Less VECCI Member discount (" + discountAdjustment.Percentage.ToString("P0") + ")";
            }

            var couponAdjustment = adjustment as CouponAdjustment;
            if (couponAdjustment != null)
            {
                var text = couponAdjustment.Amount is PercentageAdjustmentAmount
                    ? "Less " + ((PercentageAdjustmentAmount) couponAdjustment.Amount).PercentageChange.ToString("P0") + " coupon discount"
                    : "Less " + ((FixedAdjustmentAmount) couponAdjustment.Amount).FixedChange.GetPriceDisplayText(order.Currency) + " coupon discount";

                // Add a distinguisher if the coupon was added to only part of the order.

                if (order.Items.Count > 1)
                {
                    var product = couponAdjustment.ProductId == null
                        ? null
                        : (from p in products where p.Id == couponAdjustment.ProductId.Value select p).SingleOrDefault();
                    if (product != null)
                        text += " (applied to " + product.Description + " only)";
                }

                return text;
            }
            return string.Empty;
        }
    }
}

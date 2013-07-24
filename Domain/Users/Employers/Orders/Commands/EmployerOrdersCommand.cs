using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;

namespace LinkMe.Domain.Users.Employers.Orders.Commands
{
    public class EmployerOrdersCommand
        : IEmployerOrdersCommand
    {
        private readonly IOrdersCommand _ordersCommand;
        private readonly decimal _bundleDiscount;

        public EmployerOrdersCommand(IOrdersCommand ordersCommand, int bundleDiscount)
        {
            _ordersCommand = ordersCommand;
            _bundleDiscount = ((decimal)bundleDiscount) / 100;
        }

        Order IEmployerOrdersCommand.PrepareOrder(IEnumerable<Guid> productIds, Coupon coupon, Discount discount, CreditCardType creditCardType)
        {
            // Get any applicable discount.

            var discounts = GetDiscounts(productIds, discount);

            // Prepare the order to determine prices etc.

            return _ordersCommand.PrepareOrder(productIds, coupon, discounts, creditCardType);
        }

        PurchaseReceipt IEmployerOrdersCommand.PurchaseOrder(Guid ownerId, Order order, Purchaser purchaser, CreditCard creditCard)
        {
            return _ordersCommand.PurchaseOrder(ownerId, order, purchaser, creditCard);
        }

        private IList<Discount> GetDiscounts(IEnumerable<Guid> productIds, Discount discount)
        {
            // If both products are being bought then a discount is applied.

            if (productIds == null || productIds.Count() < 2)
                return discount == null ? null : new[] {discount};

            var bundleDiscount = new BundleDiscount {Percentage = _bundleDiscount};
            return discount == null ? new[] {bundleDiscount} : new[] {bundleDiscount, discount};
        }
    }
}

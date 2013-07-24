using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Orders
{
    public class Order
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        [DefaultNewCode(true)]
        public string ConfirmationCode { get; set; }

        public Guid OwnerId { get; set; }
        public Guid PurchaserId { get; set; }

        [DefaultNow]
        public DateTime Time { get; set; }

        public decimal Price { get; set; }
        public decimal AdjustedPrice { get; set; }
        public Currency Currency { get; set; }

        [Prepare, Validate]
        public IList<OrderItem> Items { get; set; }

        [Prepare, Validate]
        public IList<OrderAdjustment> Adjustments { get; set; }
    }
}
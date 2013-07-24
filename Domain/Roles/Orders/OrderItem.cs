using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Roles.Orders
{
    public class OrderItem
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
    }
}
using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Products
{
    public class ProductCreditAdjustment
    {
        public Guid CreditId { get; set; }
        public int? Quantity { get; set; }
        public TimeSpan? Duration { get; set; }
    }

    public class Product
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public UserType UserTypes { get; set; }
        public decimal Price { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Prepare, Validate]
        public IList<ProductCreditAdjustment> CreditAdjustments { get; set; }
    }
}


using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Orders
{
    public class PurchaseTransaction
    {
        [IsSet]
        public Guid OrderId { get; set; }
        [Required]
        public string TransactionId { get; set; }
        [Required]
        public string Provider { get; set; }
        public PurchaseRequest Request { get; set; }
        public PurchaseResponse Response { get; set; }
    }
}

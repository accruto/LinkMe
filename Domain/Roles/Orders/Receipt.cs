using System;
using LinkMe.Domain.Products;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Orders
{
    public abstract class Receipt
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime Time { get; set; }
        [Required]
        public string ExternalTransactionId { get; set; }
        [IsSet]
        public DateTime ExternalTransactionTime { get; set; }
    }

    public abstract class PurchaseReceipt
        : Receipt
    {
    }

    public class CreditCardReceipt
        : PurchaseReceipt
    {
        public CreditCardSummary CreditCard { get; set; }
    }

    public class RefundReceipt
        : Receipt
    {
    }

    public class AppleReceipt
        : PurchaseReceipt
    {
    }
}
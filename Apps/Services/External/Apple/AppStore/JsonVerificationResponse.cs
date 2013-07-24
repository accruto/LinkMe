using System;
using System.Runtime.Serialization;

namespace LinkMe.Apps.Services.External.Apple.AppStore
{
    public class JsonVerificationResponse
    {
        public int Status { get; set; }
        public VerificationReceipt Receipt { get; set; }
        public string Exception { get; set; }
    }

    public class VerificationReceipt
    {
        [DataMember(Name="quantity")]
        public int Quantity { get; set;}

        [DataMember(Name="product_id")]
        public string ProductId { get; set;}

        [DataMember(Name = "transaction_id")]
        public string TransactionId { get; set;}

        [DataMember(Name = "purchase_date")]
        public DateTime? PurchaseDate { get; set;}

        [DataMember(Name = "original_transaction_id")]
        public string OriginalTransactionId { get; set;}

        [DataMember(Name = "original_purchase_date")]
        public DateTime? OriginalPurchaseDate { get; set;}

        [DataMember(Name = "app_item_id")]
        public string AppItemId { get; set;}

        [DataMember(Name = "version_external_identifier")]
        public string ApplicationRevision { get; set;}

        [DataMember(Name = "bid")]
        public string ApplicationBundleIdentifier { get; set;}

        [DataMember(Name = "bvrs")]
        public string ApplicationVersion { get; set;}
    }
}

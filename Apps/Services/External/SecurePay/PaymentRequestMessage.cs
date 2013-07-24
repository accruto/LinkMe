using System;
using System.Runtime.Serialization;
using LinkMe.Domain;
using LinkMe.Domain.Products;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    [DataContract(Namespace = "")]
    public class RequestCreditCardInfo
        : ICloneable
    {
        [Required, Numeric(13, 16)]
        public string CardNumber { get; set; }

        [Required, Numeric(3, 4)]
        public string Cvv { get; set; }

        [Required]
        public ExpiryDate ExpiryDate { get; set; }

        [DataMember(Order = 1)]
        private string cardNumber
        {
            get { return CardNumber; }
            set { CardNumber = value; }
        }

        [DataMember(Order = 2)]
        private string cvv
        {
            get { return Cvv; }
            set { Cvv = value; }
        }

        [DataMember(Order = 3)]
        private string expiryDate
        {
            get { return ExpiryDate.ToString(); }
            set { ExpiryDate = ExpiryDate.Parse(value); }
        }

        public object Clone()
        {
            var clone = new RequestCreditCardInfo();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(RequestCreditCardInfo clone)
        {
            clone.CardNumber = CardNumber;
            clone.Cvv = Cvv;
            clone.ExpiryDate = ExpiryDate.Parse(ExpiryDate.ToString());
        }
    }

    [DataContract(Namespace = "")]
    public class BuyerInfo
        : ICloneable
    {
        [Required]
        public string Ip { get; set; }
        [Required]
        public string BillingCountry { get; set; }
        [Required]
        public string DeliveryCountry { get; set; }
        [Required]
        public string EmailAddress { get; set; }
/*
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public string Town { get; set; }
*/
        [DataMember(Order = 1)]
        private string ip
        {
            get { return Ip ?? string.Empty; }
            set { Ip = value; }
        }
/*
        [DataMember(Order = 2)]
        private string firstName
        {
            get { return FirstName ?? string.Empty; }
            set { FirstName = value; }
        }

        [DataMember(Order = 3)]
        private string lastName
        {
            get { return LastName ?? string.Empty; }
            set { LastName = value; }
        }

        [DataMember(Order = 4)]
        private string zipcode
        {
            get { return ZipCode ?? string.Empty; }
            set { ZipCode = value; }
        }

        [DataMember(Order = 5)]
        private string town
        {
            get { return Town ?? string.Empty; }
            set { Town = value; }
        }
*/
        [DataMember(Order = 6)]
        private string billingCountry
        {
            get { return BillingCountry ?? string.Empty; }
            set { BillingCountry = value; }
        }

        [DataMember(Order = 7)]
        private string deliveryCountry
        {
            get { return DeliveryCountry ?? string.Empty; }
            set { DeliveryCountry = value; }
        }

        [DataMember(Order = 8)]
        private string emailAddress
        {
            get { return EmailAddress ?? string.Empty; }
            set { EmailAddress = value; }
        }

        public object Clone()
        {
            var clone = new BuyerInfo();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(BuyerInfo clone)
        {
            clone.Ip = Ip;
            clone.BillingCountry = BillingCountry;
            clone.DeliveryCountry = DeliveryCountry;
            clone.EmailAddress = EmailAddress;
        }
    }

    [DataContract(Name = "Txn", Namespace = "")]
    public abstract class PaymentRequestTxn
        : Txn
    {
        [IsSet]
        public Guid PurchaseId { get; set; }

        protected void CopyTo(PaymentRequestTxn clone)
        {
            base.CopyTo(clone);
        }
    }

    [DataContract(Name = "Txn", Namespace = "")]
    public class StandardPaymentRequestTxn
        : PaymentRequestTxn
    {
        public StandardPaymentRequestTxn()
        {
            CreditCardInfo = new RequestCreditCardInfo();
            Currency = Currency.AUD;
        }

        public override TxnType TxnType
        {
            get { return TxnType.StandardPayment; }
        }

        public Currency Currency { get; set; }

        [DataMember(Order = 1)]
        private string currency
        {
            get { return Currency.Code; }
            set { Currency = Currency.GetCurrency(value); }
        }

        [DataMember(Order = 2)]
        private string purchaseOrderNo
        {
            get { return PurchaseId.ToString(); }
            set { PurchaseId = new Guid(value); }
        }

        [DataMember(Order = 3), Prepare, Validate]
        public RequestCreditCardInfo CreditCardInfo { get; set; }

        public override object Clone()
        {
            var clone = new StandardPaymentRequestTxn();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(StandardPaymentRequestTxn clone)
        {
            base.CopyTo(clone);
            clone.Currency = Currency;
            clone.PurchaseId = PurchaseId;
            clone.CreditCardInfo = (RequestCreditCardInfo)CreditCardInfo.Clone();
        }
    }

    [DataContract(Name = "Txn", Namespace = "")]
    public class AntiFraudPaymentRequestTxn
        : PaymentRequestTxn
    {
        public AntiFraudPaymentRequestTxn()
        {
            CreditCardInfo = new RequestCreditCardInfo();
            BuyerInfo = new BuyerInfo();
            Currency = Currency.AUD;
        }

        public override TxnType TxnType
        {
            get { return TxnType.AntiFraudPayment; }
        }

        public Currency Currency { get; set; }

        [DataMember(Order = 1)]
        private string currency
        {
            get { return Currency.Code; }
            set { Currency = Currency.GetCurrency(value); }
        }

        [DataMember(Order = 2)]
        private string purchaseOrderNo
        {
            get { return PurchaseId.ToString(); }
            set { PurchaseId = new Guid(value); }
        }

        [DataMember(Order = 3), Prepare, Validate]
        public RequestCreditCardInfo CreditCardInfo { get; set; }

        [DataMember(Order = 4), Prepare, Validate]
        public BuyerInfo BuyerInfo { get; set; }

        public override object Clone()
        {
            var clone = new AntiFraudPaymentRequestTxn();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(AntiFraudPaymentRequestTxn clone)
        {
            base.CopyTo(clone);
            clone.Currency = Currency;
            clone.PurchaseId = PurchaseId;
            clone.CreditCardInfo = (RequestCreditCardInfo)CreditCardInfo.Clone();
            clone.BuyerInfo = (BuyerInfo)BuyerInfo.Clone();
        }
    }

    [DataContract(Namespace = "")]
    public class RefundRequestTxn
        : PaymentRequestTxn
    {
        public override TxnType TxnType
        {
            get { return TxnType.Refund; }
        }

        [Required]
        public string ExternalTransactionId { get; set; }

        [DataMember(Order = 1)]
        private string purchaseOrderNo
        {
            get { return PurchaseId.ToString(); }
            set { PurchaseId = new Guid(value); }
        }

        [DataMember(Order = 2)]
        private string txnID
        {
            get { return ExternalTransactionId; }
            set { ExternalTransactionId = value; }
        }

        public override object Clone()
        {
            var clone = new RefundRequestTxn();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(RefundRequestTxn clone)
        {
            base.CopyTo(clone);
            clone.PurchaseId = PurchaseId;
            clone.ExternalTransactionId = ExternalTransactionId;
        }
    }

    [DataContract(Name = "SecurePayMessage", Namespace = "")]
    public class PaymentRequestMessage<TTxn>
        : RequestMessage
        where TTxn : Txn, new()
    {
        public PaymentRequestMessage(RequestMessageInfo messageInfo, RequestMerchantInfo merchantInfo)
            : base(messageInfo, merchantInfo)
        {
            Payment = new Payment<TTxn>();
        }

        public PaymentRequestMessage()
        {
            Payment = new Payment<TTxn>();
        }

        [DataMember]
        public override RequestType RequestType
        {
            get { return RequestType.Payment; }
            protected set { }
        }

        [DataMember(Order = 1), Prepare, Validate]
        public Payment<TTxn> Payment { get; set; }

        public override object Clone()
        {
            var clone = new PaymentRequestMessage<TTxn>();
            CopyTo(clone);
            return clone;
        }

        protected void CopyTo(PaymentRequestMessage<TTxn> clone)
        {
            base.CopyTo(clone);
            clone.Payment = (Payment<TTxn>)Payment.Clone();
        }
    }
}

using System;
using System.Runtime.Serialization;
using LinkMe.Domain;
using LinkMe.Domain.Products;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    [DataContract(Namespace = "")]
    public class ResponseCreditCardInfo
        : ICloneable
    {
        public string Pan { get; set; }
        public ExpiryDate ExpiryDate { get; set; }
        public CreditCardType CardType { get; set; }
        public string CardDescription { get; set; }

        [DataMember(Order = 1)]
        internal string pan
        {
            get { return Pan; }
            set { Pan = value; }
        }

        [DataMember(Order = 2)]
        internal string expiryDate
        {
            get { return ExpiryDate.ToString(); }
            set { ExpiryDate = ExpiryDate.Parse(value); }
        }

        [DataMember(Order = 3)]
        internal string cardType
        {
            get { return Mappings.Map(CardType).ToString(); }
            set { CardType = Mappings.Map(int.Parse(value)); }
        }

        [DataMember(Order = 4)]
        internal string cardDescription
        {
            get { return CardDescription; }
            set { CardDescription = value; }
        }

        public object Clone()
        {
            var clone = new ResponseCreditCardInfo();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(ResponseCreditCardInfo clone)
        {
            clone.Pan = Pan;
            clone.ExpiryDate = ExpiryDate.Parse(ExpiryDate.ToString());
            clone.CardType = CardType;
            clone.CardDescription = CardDescription;
        }
    }

    [DataContract(Namespace = "")]
    public class FraudGuard
        : ICloneable
    {
        public int Score { get; set; }
        public string InfoIpCountry { get; set; }
        public string InfoCardCountry { get; set; }
        public bool IpCountryFail { get; set; }
        public bool MinAmountFail { get; set; }
        public bool MaxAmountFail { get; set; }
        public int OpenProxyFail { get; set; }
        public int IpCountryCardCountryFail { get; set; }
        public int IpCardFail { get; set; }
        public int IpRiskCountryFail { get; set; }
        public int IpBillingFail { get; set; }
        public int IpDeliveryFail { get; set; }
        public int BillingDeliveryFail { get; set; }
        public int FreeEmailFail { get; set; }
        public int TooManySameBank { get; set; }
        public int TooManyDeclined { get; set; }
        public int TooManySameIp { get; set; }
        public int TooManySameCard { get; set; }
        public int LowHighAmount { get; set; }
        public int TooManySameEmail { get; set; }

        [DataMember(Order = 1)]
        internal string score
        {
            get { return Score.ToString(); }
            set { Score = string.IsNullOrEmpty(value) ? 0 : int.Parse(value); }
        }

        [DataMember(Order = 2)]
        internal string infoIpCountry
        {
            get { return InfoIpCountry; }
            set { InfoIpCountry = value; }
        }
        
        [DataMember(Order = 3)]
        internal string infoCardCountry
        {
            get { return InfoCardCountry; }
            set { InfoCardCountry = value; }
        }
        
        [DataMember(Order = 4)]
        internal string ipCountryFail
        {
            get { return GetBoolFail(IpCountryFail); }
            set { IpCountryFail = SetBoolFail(value); }
        }

        [DataMember(Order = 5)]
        internal string minAmountFail
        {
            get { return GetBoolFail(MinAmountFail); }
            set { MinAmountFail = SetBoolFail(value); }
        }

        [DataMember(Order = 6)]
        internal string maxAmountFail
        {
            get { return GetBoolFail(MaxAmountFail); }
            set { MaxAmountFail = SetBoolFail(value); }
        }

        [DataMember(Order = 7)]
        internal string openProxyFail
        {
            get { return GetIntFail(OpenProxyFail); }
            set { OpenProxyFail = SetIntFail(value); }
        }

        [DataMember(Order = 8)]
        internal string ipCountryCardCountryFail
        {
            get { return GetIntFail(IpCountryCardCountryFail); }
            set { IpCountryCardCountryFail = SetIntFail(value); }
        }

        [DataMember(Order = 9)]
        internal string ipCardFail
        {
            get { return GetIntFail(IpCardFail); }
            set { IpCardFail = SetIntFail(value); }
        }

        [DataMember(Order = 10)]
        internal string ipRiskCountryFail
        {
            get { return GetIntFail(IpRiskCountryFail); }
            set { IpRiskCountryFail = SetIntFail(value); }
        }

        [DataMember(Order = 11)]
        internal string ipBillingFail
        {
            get { return GetIntFail(IpBillingFail); }
            set { IpBillingFail = SetIntFail(value); }
        }

        [DataMember(Order = 12)]
        internal string ipDeliveryFail
        {
            get { return GetIntFail(IpDeliveryFail); }
            set { IpDeliveryFail = SetIntFail(value); }
        }

        [DataMember(Order = 13)]
        internal string billingDeliveryFail
        {
            get { return GetIntFail(BillingDeliveryFail); }
            set { BillingDeliveryFail = SetIntFail(value); }
        }

        [DataMember(Order = 14)]
        internal string freeEmailFail
        {
            get { return GetIntFail(FreeEmailFail); }
            set { FreeEmailFail = SetIntFail(value); }
        }

        [DataMember(Order = 15)]
        internal string tooManySameBank
        {
            get { return GetIntFail(TooManySameBank); }
            set { TooManySameBank = SetIntFail(value); }
        }

        [DataMember(Order = 16)]
        internal string tooManyDeclined
        {
            get { return GetIntFail(TooManyDeclined); }
            set { TooManyDeclined = SetIntFail(value); }
        }

        [DataMember(Order = 17)]
        internal string tooManySameIp
        {
            get { return GetIntFail(TooManySameIp); }
            set { TooManySameIp = SetIntFail(value); }
        }

        [DataMember(Order = 18)]
        internal string tooManySameCard
        {
            get { return GetIntFail(TooManySameCard); }
            set { TooManySameCard = SetIntFail(value); }
        }

        [DataMember(Order = 19)]
        internal string lowHighAmount
        {
            get { return GetIntFail(LowHighAmount); }
            set { LowHighAmount = SetIntFail(value); }
        }

        [DataMember(Order = 20)]
        internal string tooManySameEmail
        {
            get { return GetIntFail(TooManySameEmail); }
            set { TooManySameEmail = SetIntFail(value); }
        }

        private static string GetBoolFail(bool fail)
        {
            return fail ? "YES" : "NO";
        }

        private static bool SetBoolFail(string value)
        {
            return value == "YES";
        }

        private static string GetIntFail(int fail)
        {
            return fail.ToString();
        }

        private static int SetIntFail(string value)
        {
            int ivalue;
            return int.TryParse(value, out ivalue) ? ivalue : 0;
        }

        public object Clone()
        {
            var clone = new FraudGuard();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(FraudGuard clone)
        {
            clone.Score = Score;
            clone.InfoIpCountry = InfoIpCountry;
            clone.InfoCardCountry = InfoCardCountry;
            clone.IpCountryFail = IpCountryFail;
            clone.MinAmountFail = MinAmountFail;
            clone.MaxAmountFail = MaxAmountFail;
            clone.OpenProxyFail = OpenProxyFail;
            clone.IpCountryCardCountryFail = IpCountryCardCountryFail;
            clone.IpCardFail = IpCardFail;
            clone.IpRiskCountryFail = IpRiskCountryFail;
            clone.IpBillingFail = IpBillingFail;
            clone.IpDeliveryFail = IpDeliveryFail;
            clone.BillingDeliveryFail = BillingDeliveryFail;
            clone.FreeEmailFail = FreeEmailFail;
            clone.TooManySameBank = TooManySameBank;
            clone.TooManyDeclined = TooManyDeclined;
            clone.TooManySameIp = TooManySameIp;
            clone.TooManySameCard = TooManySameCard;
            clone.LowHighAmount = LowHighAmount;
            clone.TooManySameEmail = TooManySameEmail;
        }
    }

    [DataContract(Name = "Txn", Namespace = "")]
    public abstract class PaymentResponseTxn
        : Txn
    {
        protected PaymentResponseTxn()
        {
            CreditCardInfo = new ResponseCreditCardInfo();
            Currency = Currency.AUD;
        }

        public override TxnType TxnType
        {
            get { throw new NotImplementedException(); }
        }

        public Currency Currency { get; set; }
        public bool IsApproved { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseText { get; set; }
        public SettlementDate SettlementDate { get; set; }
        public string ExternalTransactionId { get; set; }

        [DataMember(Order = 1)]
        internal string currency
        {
            get { return Currency.Code; }
            set { Currency = Currency.GetCurrency(value); }
        }

        [DataMember(Order = 2)]
        internal string purchaseOrderNo
        {
            get { return GetPurchaseOrderNo(); }
            set { SetPurchaseOrderNo(value); }
        }

        protected abstract void SetPurchaseOrderNo(string value);
        protected abstract string GetPurchaseOrderNo();

        [DataMember(Order = 3)]
        internal string approved
        {
            get { return IsApproved ? "Yes" : "No"; }
            set { IsApproved = value == "Yes"; }
        }

        [DataMember(Order = 4)]
        internal string responseCode
        {
            get { return ResponseCode; }
            set { ResponseCode = value; }
        }

        [DataMember(Order = 5)]
        internal string responseText
        {
            get { return ResponseText; }
            set { ResponseText = value; }
        }

/*
        [DataMember(Order = 6)]
        private string thinlinkResponseCode
        {
            get { return null; }
            set { }
        }

        [DataMember(Order = 7)]
        private string thinlinkResponseText
        {
            get { return null; }
            set { }
        }
        
        [DataMember(Order = 8)]
        private string thinlinkEventStatusCode
        {
            get { return null; }
            set { }
        }
        
        [DataMember(Order = 9)]
        private string thinlinkEventStatusText
        {
            get { return null; }
            set { }
        }
        */

        [DataMember(Order = 10)]
        internal string settlementDate
        {
            get { return SettlementDate == null ? null : SettlementDate.ToString(); }
            set { SettlementDate = string.IsNullOrEmpty(value) ? null : SettlementDate.Parse(value); }
        }

        [DataMember(Order = 11)]
        internal string txnID
        {
            get { return ExternalTransactionId; }
            set { ExternalTransactionId = value; }
        }

        [DataMember(Order = 12)]
        private string preauthID { get; set; }

        [DataMember(Order = 13)]
        public ResponseCreditCardInfo CreditCardInfo { get; set; }

        protected void CopyTo(PaymentResponseTxn clone)
        {
            base.CopyTo(clone);
            clone.Currency = Currency;
            clone.IsApproved = IsApproved;
            clone.ResponseCode = ResponseCode;
            clone.ResponseText = ResponseText;
            clone.SettlementDate = SettlementDate == null ? null : SettlementDate.Parse(SettlementDate.ToString());
            clone.ExternalTransactionId = ExternalTransactionId;
            clone.CreditCardInfo = (ResponseCreditCardInfo) CreditCardInfo.Clone();
        }
    }

    [DataContract(Name = "Txn", Namespace = "")]
    public class StandardPaymentResponseTxn
        : PaymentResponseTxn
    {
        public Guid PurchaseId { get; set; }

        protected override void SetPurchaseOrderNo(string value)
        {
            PurchaseId = new Guid(value);
        }

        protected override string GetPurchaseOrderNo()
        {
            return PurchaseId.ToString();
        }

        public override object Clone()
        {
            var clone = new StandardPaymentResponseTxn();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(StandardPaymentResponseTxn clone)
        {
            base.CopyTo(clone);
            clone.PurchaseId = PurchaseId;
        }
    }

    [DataContract(Name = "Txn", Namespace = "")]
    public class AntiFraudPaymentResponseTxn
        : PaymentResponseTxn
    {
        public AntiFraudPaymentResponseTxn()
        {
            FraudGuard = new FraudGuard();
        }

        public Guid PurchaseId { get; set; }

        protected override void SetPurchaseOrderNo(string value)
        {
            PurchaseId = new Guid(value);
        }

        protected override string GetPurchaseOrderNo()
        {
            return PurchaseId.ToString();
        }

        public string AntiFraudResponseCode { get; set; }
        public string AntiFraudResponseText { get; set; }

        [DataMember(Order = 14)]
        internal string antiFraudResponseCode
        {
            get { return AntiFraudResponseCode; }
            set { AntiFraudResponseCode = value; }
        }

        [DataMember(Order = 15)]
        internal string antiFraudResponseText
        {
            get { return AntiFraudResponseText; }
            set { AntiFraudResponseText = value; }
        }

        [DataMember(Order = 16)]
        public FraudGuard FraudGuard { get; set; }

        public override object Clone()
        {
            var clone = new AntiFraudPaymentResponseTxn();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(AntiFraudPaymentResponseTxn clone)
        {
            base.CopyTo(clone);
            clone.PurchaseId = PurchaseId;
            clone.AntiFraudResponseCode = AntiFraudResponseCode;
            clone.AntiFraudResponseText = AntiFraudResponseText;
            clone.FraudGuard = (FraudGuard)FraudGuard.Clone();
        }
    }

    [DataContract(Name = "Txn", Namespace = "")]
    public class RefundResponseTxn
        : PaymentResponseTxn
    {
        public string PaymentExternalTransactionId { get; set; }

        protected override void SetPurchaseOrderNo(string value)
        {
            PaymentExternalTransactionId = value;
        }

        protected override string GetPurchaseOrderNo()
        {
            return PaymentExternalTransactionId;
        }

        public override object Clone()
        {
            var clone = new RefundResponseTxn();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(RefundResponseTxn clone)
        {
            base.CopyTo(clone);
            clone.PaymentExternalTransactionId = PaymentExternalTransactionId;
        }
    }

    [DataContract(Name = "SecurePayMessage", Namespace = "")]
    public class PaymentResponseMessage<TTxn>
        : ResponseMessage
        where TTxn : PaymentResponseTxn, new()
    {
        public PaymentResponseMessage(ResponseMessageInfo messageInfo, ResponseMerchantInfo merchantInfo)
            : base(messageInfo, merchantInfo)
        {
        }

        public PaymentResponseMessage()
        {
        }

        public override RequestType RequestType
        {
            get { return RequestType.Payment; }
            protected set { }
        }

        [DataMember(Order = 1), Prepare, Validate]
        public Payment<TTxn> Payment { get; set; }

        public override object Clone()
        {
            var clone = new PaymentResponseMessage<TTxn>();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(PaymentResponseMessage<TTxn> clone)
        {
            base.CopyTo(clone);
            clone.Payment = (Payment<TTxn>)Payment.Clone();
        }
    }
}

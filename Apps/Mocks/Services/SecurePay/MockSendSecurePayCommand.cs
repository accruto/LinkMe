using System;
using System.Net;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Domain.Products;

namespace LinkMe.Apps.Mocks.Services.SecurePay
{
    public enum FraudFailure
    {
        None,
        IpCountryCardCountry,
        IpRiskCountryFail
    }

    public class MockSendSecurePayCommand
        : ISendSecurePayCommand
    {
        public string Url { get; set; }
        public int? StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public int? ResponseCode { get; set; }
        public FraudFailure FraudFailure { get; set; }

        EchoResponseMessage ISendSecurePayCommand.Send(EchoRequestMessage requestMessage)
        {
            throw new NotImplementedException();
        }

        PaymentResponseMessage<TResponseTxn> ISendSecurePayCommand.Send<TRequestTxn, TResponseTxn>(PaymentRequestMessage<TRequestTxn> requestMessage)
        {
            if (Url != null)
                throw new WebException("The remote name could not be resolved: '" + (Url.StartsWith("http://") ? Url.Substring("http://".Length) : Url) + "'");

            if (StatusCode != null)
                return GetInvalidStatusMessage<TResponseTxn>();

            if (ResponseCode != null)
                return GetNotApprovedMessage<TRequestTxn, TResponseTxn>(requestMessage);

            if (FraudFailure != FraudFailure.None)
                return GetFraudMessage(requestMessage as PaymentRequestMessage<AntiFraudPaymentRequestTxn>) as PaymentResponseMessage<TResponseTxn>;

            return GetResponseMessage(requestMessage, GetResponseMessage<TResponseTxn>(true, 0));
        }

        private static PaymentResponseMessage<TResponseTxn> GetResponseMessage<TResponseTxn>(bool isApproved, int responseCode)
            where TResponseTxn : PaymentResponseTxn, new()
        {
            return new PaymentResponseMessage<TResponseTxn>
            {
                Status = new Status { StatusCode = 0, StatusDescription = "Normal" },
                Payment = new Payment<TResponseTxn>
                {
                    TxnList = new TxnList<TResponseTxn>
                    {
                        Txn = new TResponseTxn
                        {
                            ExternalTransactionId = Guid.NewGuid().ToString(),
                            IsApproved = isApproved,
                            ResponseCode = responseCode.ToString("D2"),
                            ResponseText = GetResponseText(responseCode),
                            SettlementDate = new SettlementDate(DateTime.Now.AddDays(1)),
                            CreditCardInfo = new ResponseCreditCardInfo
                            {
                                Pan = "444433...111",
                                CardType = CreditCardType.Visa,
                                CardDescription = CreditCardType.Visa.ToString(),
                            }
                        }
                    }
                },
                MessageInfo = { MessageTimestamp = new MessageTimestamp(DateTime.Now) }
            };
        }

        private static PaymentResponseMessage<TResponseTxn> GetResponseMessage<TRequestTxn, TResponseTxn>(PaymentRequestMessage<TRequestTxn> requestMessage, PaymentResponseMessage<TResponseTxn> responseMessage)
            where TRequestTxn : PaymentRequestTxn, new()
            where TResponseTxn : PaymentResponseTxn, new()
        {
            if (typeof(TRequestTxn) == typeof(StandardPaymentRequestTxn))
                return GetStandardResponseMessage(requestMessage as PaymentRequestMessage<StandardPaymentRequestTxn>, responseMessage as PaymentResponseMessage<StandardPaymentResponseTxn>) as PaymentResponseMessage<TResponseTxn>;
            if (typeof(TRequestTxn) == typeof(AntiFraudPaymentRequestTxn))
                return GetAntiFraudResponseMessage(requestMessage as PaymentRequestMessage<AntiFraudPaymentRequestTxn>, responseMessage as PaymentResponseMessage<AntiFraudPaymentResponseTxn>) as PaymentResponseMessage<TResponseTxn>;
            return null;
        }

        private static PaymentResponseMessage<StandardPaymentResponseTxn> GetStandardResponseMessage(PaymentRequestMessage<StandardPaymentRequestTxn> requestMessage, PaymentResponseMessage<StandardPaymentResponseTxn> responseMessage)
        {
            responseMessage.Payment.TxnList.Txn.Amount = requestMessage.Payment.TxnList.Txn.Amount;
            responseMessage.Payment.TxnList.Txn.Currency = requestMessage.Payment.TxnList.Txn.Currency;
            responseMessage.Payment.TxnList.Txn.PurchaseId = requestMessage.Payment.TxnList.Txn.PurchaseId;
            responseMessage.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = requestMessage.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate;
            return responseMessage;
        }

        private static PaymentResponseMessage<AntiFraudPaymentResponseTxn> GetAntiFraudResponseMessage(PaymentRequestMessage<AntiFraudPaymentRequestTxn> requestMessage, PaymentResponseMessage<AntiFraudPaymentResponseTxn> responseMessage)
        {
            responseMessage.Payment.TxnList.Txn.Amount = requestMessage.Payment.TxnList.Txn.Amount;
            responseMessage.Payment.TxnList.Txn.Currency = requestMessage.Payment.TxnList.Txn.Currency;
            responseMessage.Payment.TxnList.Txn.PurchaseId = requestMessage.Payment.TxnList.Txn.PurchaseId;
            responseMessage.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = requestMessage.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate;
            return responseMessage;
        }

        private PaymentResponseMessage<TResponseTxn> GetNotApprovedMessage<TRequestTxn, TResponseTxn>(PaymentRequestMessage<TRequestTxn> requestMessage)
            where TResponseTxn : PaymentResponseTxn, new()
            where TRequestTxn : PaymentRequestTxn, new()
        {
            return GetResponseMessage(requestMessage, GetResponseMessage<TResponseTxn>(false, ResponseCode.Value));
        }

        private PaymentResponseMessage<AntiFraudPaymentResponseTxn> GetFraudMessage(PaymentRequestMessage<AntiFraudPaymentRequestTxn> requestMessage)
        {
            return GetResponseMessage(requestMessage, GetResponseMessage());
        }

        private PaymentResponseMessage<AntiFraudPaymentResponseTxn> GetResponseMessage()
        {
            const int responseCode = 450;
            return new PaymentResponseMessage<AntiFraudPaymentResponseTxn>
            {
                Status = new Status { StatusCode = 0, StatusDescription = "Normal" },
                Payment = new Payment<AntiFraudPaymentResponseTxn>
                {
                    TxnList = new TxnList<AntiFraudPaymentResponseTxn>
                    {
                        Txn = new AntiFraudPaymentResponseTxn
                        {
                            ExternalTransactionId = Guid.NewGuid().ToString(),
                            IsApproved = false,
                            ResponseCode = responseCode.ToString("D2"),
                            ResponseText = GetResponseText(responseCode),
                            SettlementDate = new SettlementDate(DateTime.Now.AddDays(1)),
                            CreditCardInfo = new ResponseCreditCardInfo
                            {
                                Pan = "444433...111",
                                CardType = CreditCardType.Visa,
                                CardDescription = CreditCardType.Visa.ToString(),
                            },
                            AntiFraudResponseCode = "450",
                            AntiFraudResponseText = "Suspected fraud",
                            FraudGuard = new FraudGuard
                            {
                                Score = 100,
                                InfoIpCountry = FraudFailure == FraudFailure.IpRiskCountryFail ? "USA" : "AUS",
                                InfoCardCountry = FraudFailure == FraudFailure.IpCountryCardCountry ? "" : "AUS",
                                IpCountryCardCountryFail = FraudFailure == FraudFailure.IpCountryCardCountry ? 100 : 0,
                            },
                        }
                    }
                },
                MessageInfo = { MessageTimestamp = new MessageTimestamp(DateTime.Now) }
            };
        }

        private static string GetResponseText(int code)
        {
            switch (code)
            {
                case 0:
                    return "Approved";

                case 1:
                    return "Refer to Card Issuer";

                case 7:
                    return "Pick Up Card, Special Conditions";

                default:
                    return "????";
            }
        }

        private PaymentResponseMessage<TResponseTxn> GetInvalidStatusMessage<TResponseTxn>()
            where TResponseTxn : PaymentResponseTxn, new()
        {
            return new PaymentResponseMessage<TResponseTxn>
            {
                Status = new Status {StatusCode = StatusCode.Value, StatusDescription = StatusDescription},
            };
        }
    }
}
using System;
using LinkMe.Domain;
using LinkMe.Domain.Location;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;

namespace LinkMe.Apps.Services.External.SecurePay
{
    internal static class Mappings
    {
        public static PaymentRequestMessage<TTxn> Map<TTxn>(Order order, Purchaser purchaser, CreditCard creditCard, Country country, string merchantId, string password)
            where TTxn : Txn, new()
        {
            return typeof(TTxn) == typeof(AntiFraudPaymentRequestTxn)
                ? MapAntiFraud(order, purchaser, creditCard, country, merchantId, password) as PaymentRequestMessage<TTxn>
                : MapStandard(order, creditCard, merchantId, password) as PaymentRequestMessage<TTxn>;
        }

        private static PaymentRequestMessage<StandardPaymentRequestTxn> MapStandard(Order order, CreditCard creditCard, string merchantId, string password)
        {
            var message = new PaymentRequestMessage<StandardPaymentRequestTxn>
            {
                MerchantInfo =
                {
                    MerchantId = merchantId,
                    Password = password
                }
            };

            message.Payment.TxnList.Txn.PurchaseId = order.Id;
            message.Payment.TxnList.Txn.Amount = Map(order.AdjustedPrice, order.Currency);
            message.Payment.TxnList.Txn.Currency = order.Currency;

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = creditCard.CardNumber;
            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = creditCard.Cvv;
            message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = creditCard.ExpiryDate;

            return message;
        }

        private static PaymentRequestMessage<AntiFraudPaymentRequestTxn> MapAntiFraud(Order order, Purchaser purchaser, CreditCard creditCard, Country country, string merchantId, string password)
        {
            var message = new PaymentRequestMessage<AntiFraudPaymentRequestTxn>
            {
                MerchantInfo =
                {
                    MerchantId = merchantId,
                    Password = password
                }
            };

            message.Payment.TxnList.Txn.PurchaseId = order.Id;
            message.Payment.TxnList.Txn.Amount = Map(order.AdjustedPrice, order.Currency);
            message.Payment.TxnList.Txn.Currency = order.Currency;

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = creditCard.CardNumber;
            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = creditCard.Cvv;
            message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = creditCard.ExpiryDate;

            message.Payment.TxnList.Txn.BuyerInfo.Ip = purchaser.IpAddress;
            message.Payment.TxnList.Txn.BuyerInfo.EmailAddress = purchaser.EmailAddress;
            var mappedCountry = Map(country);
            message.Payment.TxnList.Txn.BuyerInfo.BillingCountry = mappedCountry;
            message.Payment.TxnList.Txn.BuyerInfo.DeliveryCountry = mappedCountry;

            return message;
        }

        private static int Map(decimal amount, Currency currency)
        {
            // Assume currency is AUD.

            return (int)(amount*100);
        }

        public static CreditCardReceipt Map<TTxn>(PaymentResponseMessage<TTxn> message)
            where TTxn : PaymentResponseTxn, new()
        {
            return new CreditCardReceipt
            {
                ExternalTransactionId = message.Payment.TxnList.Txn.ExternalTransactionId,
                ExternalTransactionTime = message.MessageInfo.MessageTimestamp.ToDateTime(),
                CreditCard = new CreditCardSummary
                {
                    Pan = message.Payment.TxnList.Txn.CreditCardInfo.Pan,
                    Type = message.Payment.TxnList.Txn.CreditCardInfo.CardType,
                }
            };
        }

        public static PaymentRequestMessage<RefundRequestTxn> Map(Guid orderId, string externalTransactionId, string merchantId, string password)
        {
            var message = new PaymentRequestMessage<RefundRequestTxn>
            {
                MerchantInfo =
                {
                    MerchantId = merchantId,
                    Password = password
                }
            };

            message.Payment.TxnList.Txn.PurchaseId = orderId;
            message.Payment.TxnList.Txn.ExternalTransactionId = externalTransactionId;
            return message;
        }

        public static RefundReceipt Map(PaymentResponseMessage<RefundResponseTxn> message)
        {
            return new RefundReceipt
            {
                ExternalTransactionId = message.Payment.TxnList.Txn.ExternalTransactionId,
                ExternalTransactionTime = message.MessageInfo.MessageTimestamp.ToDateTime(),
            };
        }

        public static CreditCardType Map(int type)
        {
            switch (type)
            {
                case 2:
                    return CreditCardType.Amex;

                case 6:
                    return CreditCardType.Visa;

                case 5:
                default:
                    return CreditCardType.MasterCard;
            }
        }

        public static int Map(CreditCardType type)
        {
            switch (type)
            {
                case CreditCardType.Amex:
                    return 2;

                case CreditCardType.Visa:
                    return 6;

                case CreditCardType.MasterCard:
                default:
                    return 5;
            }
        }

        private static string Map(Country country)
        {
            if (country == null)
                return string.Empty;

            switch (country.Name)
            {
                case "Australia":
                    return "AUS";

                default:
                    return string.Empty;
            }
        }
    }
}

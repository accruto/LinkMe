using System;
using System.IO;
using System.Xml;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public static class Serialization
    {
        public static string Serialize(RequestMessage message)
        {
            // Use the serializer.

            var serializer = new SecurePaySerializer(message.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, message);
                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static EchoResponseMessage Deserialize(string responseXml)
        {
            var message = new EchoResponseMessage();

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseXml);

            // Do it all by hand ...

            var xmlSecurePayMessage = xmlDocument.SelectSingleNode("/SecurePayMessage");
            if (xmlSecurePayMessage != null)
            {
                // MessageInfo

                var xmlMessageInfo = xmlSecurePayMessage.SelectSingleNode("MessageInfo");
                if (xmlMessageInfo != null)
                    Deserialize(xmlMessageInfo, message.MessageInfo);

                // MerchantInfo

                var xmlMerchantInfo = xmlSecurePayMessage.SelectSingleNode("MerchantInfo");
                if (xmlMerchantInfo != null)
                    Deserialize(xmlMerchantInfo, message.MerchantInfo);

                // Status

                var xmlStatus = xmlSecurePayMessage.SelectSingleNode("Status");
                if (xmlStatus != null)
                    Deserialize(xmlStatus, message.Status);
            }

            return message;
        }

        public static PaymentResponseMessage<TResponseTxn> Deserialize<TResponseTxn>(string responseXml)
            where TResponseTxn : PaymentResponseTxn, new()
        {
            // Would really like to just use the serializer but the <?xml declaration stuffs it up
            // and for some unknown reason including the "thinlink" elements
            // results in an invalid data error.

            //return DeserializeWithSerializer<TResponseTxn>(message);

            return DeserializeWithXmlDocument<TResponseTxn>(responseXml);
        }

        private static PaymentResponseMessage<TResponseTxn> DeserializeWithXmlDocument<TResponseTxn>(string responseXml)
            where TResponseTxn : PaymentResponseTxn, new()
        {
            var message = new PaymentResponseMessage<TResponseTxn>();

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseXml);

            // Do it all by hand ...

            var xmlSecurePayMessage = xmlDocument.SelectSingleNode("/SecurePayMessage");
            if (xmlSecurePayMessage != null)
            {
                // MessageInfo

                var xmlMessageInfo = xmlSecurePayMessage.SelectSingleNode("MessageInfo");
                if (xmlMessageInfo != null)
                    Deserialize(xmlMessageInfo, message.MessageInfo);

                // MerchantInfo

                var xmlMerchantInfo = xmlSecurePayMessage.SelectSingleNode("MerchantInfo");
                if (xmlMerchantInfo != null)
                    Deserialize(xmlMerchantInfo, message.MerchantInfo);

                // Status

                var xmlStatus = xmlSecurePayMessage.SelectSingleNode("Status");
                if (xmlStatus != null)
                    Deserialize(xmlStatus, message.Status);

                // Payment.

                message.Payment = DeserializePayment<TResponseTxn>(xmlSecurePayMessage);
            }

            return message;
        }

        private static Payment<TTxn> DeserializePayment<TTxn>(XmlNode xmlSecureMessage)
            where TTxn : PaymentResponseTxn, new()
        {
            var xmlTxn = xmlSecureMessage.SelectSingleNode("Payment/TxnList/Txn");
            if (xmlTxn == null)
                return null;

            var payment = new Payment<TTxn>();
            Deserialize(xmlTxn, payment);
            return payment;
        }

        private static void Deserialize<TTxn>(XmlNode xmlTxn, Payment<TTxn> payment)
            where TTxn : PaymentResponseTxn, new()
        {
            GetElementValue(xmlTxn, "txnType", v => payment.TxnList.Txn.txnType = v);
            GetElementValue(xmlTxn, "txnSource", v => payment.TxnList.Txn.txnSource = v);
            GetElementValue(xmlTxn, "amount", v => payment.TxnList.Txn.amount = v);
            GetElementValue(xmlTxn, "currency", v => payment.TxnList.Txn.currency = v);
            GetElementValue(xmlTxn, "purchaseOrderNo", v => payment.TxnList.Txn.purchaseOrderNo = v);
            GetElementValue(xmlTxn, "approved", v => payment.TxnList.Txn.approved = v);
            GetElementValue(xmlTxn, "responseCode", v => payment.TxnList.Txn.responseCode = v);
            GetElementValue(xmlTxn, "responseText", v => payment.TxnList.Txn.responseText = v);
            GetElementValue(xmlTxn, "settlementDate", v => payment.TxnList.Txn.settlementDate = v);
            GetElementValue(xmlTxn, "txnID", v => payment.TxnList.Txn.txnID = v);

            var xmlCreditCardInfo = xmlTxn.SelectSingleNode("CreditCardInfo");
            if (xmlCreditCardInfo != null)
            {
                payment.TxnList.Txn.CreditCardInfo = new ResponseCreditCardInfo();
                Deserialize(xmlCreditCardInfo, payment.TxnList.Txn.CreditCardInfo);
            }

            if (typeof(TTxn) == typeof(AntiFraudPaymentResponseTxn))
                Deserialize(xmlTxn, payment.TxnList.Txn as AntiFraudPaymentResponseTxn);
        }

        private static void Deserialize(XmlNode xmlTxn, AntiFraudPaymentResponseTxn txn)
        {
            GetElementValue(xmlTxn, "antiFraudResponseCode", v => txn.antiFraudResponseCode = v);
            GetElementValue(xmlTxn, "antiFraudResponseText", v => txn.antiFraudResponseText = v);

            var xmlFraudGuard = xmlTxn.SelectSingleNode("FraudGuard");
            if (xmlFraudGuard != null)
            {
                txn.FraudGuard = new FraudGuard();
                Deserialize(xmlFraudGuard, txn.FraudGuard);
            }
        }

        private static void Deserialize(XmlNode xmlFraudGuard, FraudGuard fraudGuard)
        {
            GetElementValue(xmlFraudGuard, "score", v => fraudGuard.score = v);
            GetElementValue(xmlFraudGuard, "infoIpCountry", v => fraudGuard.infoIpCountry = v);
            GetElementValue(xmlFraudGuard, "infoCardCountry", v => fraudGuard.infoCardCountry = v);
            GetElementValue(xmlFraudGuard, "ipCountryFail", v => fraudGuard.ipCountryFail = v);
            GetElementValue(xmlFraudGuard, "minAmountFail", v => fraudGuard.minAmountFail = v);
            GetElementValue(xmlFraudGuard, "maxAmountFail", v => fraudGuard.maxAmountFail = v);
            GetElementValue(xmlFraudGuard, "openProxyFail", v => fraudGuard.openProxyFail = v);
            GetElementValue(xmlFraudGuard, "IpCountryCardCountryFail", v => fraudGuard.ipCountryCardCountryFail = v);
            GetElementValue(xmlFraudGuard, "ipCardFail", v => fraudGuard.ipCardFail = v);
            GetElementValue(xmlFraudGuard, "ipRiskCountryFail", v => fraudGuard.ipRiskCountryFail = v);
            GetElementValue(xmlFraudGuard, "ipBillingFail", v => fraudGuard.ipBillingFail = v);
            GetElementValue(xmlFraudGuard, "ipDeliveryFail", v => fraudGuard.ipDeliveryFail = v);
            GetElementValue(xmlFraudGuard, "billingDeliveryFail", v => fraudGuard.billingDeliveryFail = v);
            GetElementValue(xmlFraudGuard, "freeEmailFail", v => fraudGuard.freeEmailFail = v);
            GetElementValue(xmlFraudGuard, "tooManySameBank", v => fraudGuard.tooManySameBank = v);
            GetElementValue(xmlFraudGuard, "tooManyDeclined", v => fraudGuard.tooManyDeclined = v);
            GetElementValue(xmlFraudGuard, "tooManySameIp", v => fraudGuard.tooManySameIp = v);
            GetElementValue(xmlFraudGuard, "tooManySameCard", v => fraudGuard.tooManySameCard = v);
            GetElementValue(xmlFraudGuard, "lowHighAmount", v => fraudGuard.lowHighAmount = v);
            GetElementValue(xmlFraudGuard, "tooManySameEmail", v => fraudGuard.tooManySameEmail = v);
        }

        private static void Deserialize(XmlNode xmlCreditCardInfo, ResponseCreditCardInfo creditCardInfo)
        {
            GetElementValue(xmlCreditCardInfo, "pan", v => creditCardInfo.pan = v);
            GetElementValue(xmlCreditCardInfo, "expiryDate", v => creditCardInfo.expiryDate = v);
            GetElementValue(xmlCreditCardInfo, "cardType", v => creditCardInfo.cardType = v);
            GetElementValue(xmlCreditCardInfo, "cardDescription", v => creditCardInfo.cardDescription = v);
        }

        private static void Deserialize(XmlNode xmlStatus, Status status)
        {
            GetElementValue(xmlStatus, "statusCode", v => status.statusCode = v);
            GetElementValue(xmlStatus, "statusDescription", v => status.statusDescription = v);
        }

        private static void Deserialize(XmlNode xmlMessageInfo, MessageInfo messageInfo)
        {
             GetElementValue(xmlMessageInfo, "messageID", v => messageInfo.messageID = v);
             GetElementValue(xmlMessageInfo, "messageTimestamp", v => messageInfo.messageTimestamp = v);
             GetElementValue(xmlMessageInfo, "apiVersion", v => messageInfo.apiVersion = v);
        }

        private static void Deserialize(XmlNode xmlMerchantInfo, MerchantInfo merchantInfo)
        {
            GetElementValue(xmlMerchantInfo, "merchantID", v => merchantInfo.merchantID = v);
        }

        private static void GetElementValue(XmlNode xmlNode, string name, Action<string> action)
        {
            var xmlElement = xmlNode.SelectSingleNode(name);
            if (xmlElement != null)
                action(xmlElement.InnerText);
        }

        private static PaymentResponseMessage<TResponseTxn> DeserializeWithSerializer<TResponseTxn>(string message)
            where TResponseTxn : PaymentResponseTxn, new()
        {
            var serializer = new SecurePaySerializer(typeof(PaymentResponseMessage<TResponseTxn>));
            byte[] bytes;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(message);
                }

                bytes = stream.GetBuffer();
            }

            using (var stream = new MemoryStream(bytes))
            {
                return (PaymentResponseMessage<TResponseTxn>)serializer.ReadObject(stream);
            }
        }
    }
}

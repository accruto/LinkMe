using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class SendSecurePayCommand
        : ISendSecurePayCommand
    {
        private readonly IPurchaseTransactionsCommand _purchaseTransactionsCommand;
        private static readonly EventSource EventSource = new EventSource<SendSecurePayCommand>();
        protected static readonly Encoding DefaultHttpEncoding = Encoding.GetEncoding(28591); // Latin-1
        private const string Provider = "SecurePay";
        private readonly string _url;
        private readonly bool _ignoreCents;

        public SendSecurePayCommand(IPurchaseTransactionsCommand purchaseTransactionsCommand, bool useAntiFraud, string url, string antiFraudUrl)
            : this(purchaseTransactionsCommand, useAntiFraud, url, antiFraudUrl, false)
        {
        }

        public SendSecurePayCommand(IPurchaseTransactionsCommand purchaseTransactionsCommand, bool useAntiFraud, string url, string antiFraudUrl, bool ignoreCents)
        {
            _purchaseTransactionsCommand = purchaseTransactionsCommand;
            _url = useAntiFraud ? antiFraudUrl : url;

            // The SecurePay test environment allows you to specify cents to indicate to return certain error codes.
            // In UAT we do not want this behaviour so round the cents of any price that comes in.

            _ignoreCents = ignoreCents;
        }

        EchoResponseMessage ISendSecurePayCommand.Send(EchoRequestMessage requestMessage)
        {
            const string method = "Send";

            var requestXml = Serialization.Serialize(requestMessage);
            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Sending request to SecurePay.", Event.Arg("url", _url), Event.Arg("requestXml", requestXml));

            var responseXml = Send(requestXml);

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Received response from SecurePay.", Event.Arg("url", _url), Event.Arg("responseXml", responseXml));

            return Serialization.Deserialize(responseXml);
        }

        PaymentResponseMessage<TResponseTxn> ISendSecurePayCommand.Send<TRequestTxn, TResponseTxn>(PaymentRequestMessage<TRequestTxn> requestMessage)
        {
            var originalCents = 0;
            if (_ignoreCents)
                originalCents = AdjustCents(requestMessage);

            // Create the request.

            var orderId = requestMessage.Payment.TxnList.Txn.PurchaseId;
            var transactionId = requestMessage.MessageInfo.MessageId.ToString();

            var requestXml = CreateRequestXml(orderId, transactionId, requestMessage);
            var responseXml = Send(requestXml);
            var responseMessage = CreateResponse<TResponseTxn>(orderId, transactionId, responseXml);

            if (_ignoreCents)
                AdjustCents(responseMessage, originalCents);

            return responseMessage;
        }

        private string CreateRequestXml<TRequestTxn>(Guid orderId, string transactionId, PaymentRequestMessage<TRequestTxn> requestMessage)
            where TRequestTxn : PaymentRequestTxn, new()
        {
            // Generate the complete request xml.

            var requestXml = Serialization.Serialize(requestMessage);

            try
            {
                // Record this request but anonymise any sensitive information.

                requestMessage = (PaymentRequestMessage<TRequestTxn>)requestMessage.Clone();
                requestMessage.MerchantInfo.Password = Anonymize(requestMessage.MerchantInfo.Password);
                if (requestMessage.Payment.TxnList.Txn is StandardPaymentRequestTxn)
                    Anonymize((requestMessage.Payment.TxnList.Txn as StandardPaymentRequestTxn).CreditCardInfo);
                else if (requestMessage.Payment.TxnList.Txn is AntiFraudPaymentRequestTxn)
                    Anonymize((requestMessage.Payment.TxnList.Txn as AntiFraudPaymentRequestTxn).CreditCardInfo);

                var purchaseRequest = new PurchaseRequest
                {
                    Time = DateTime.Now,
                    Message = Serialization.Serialize(requestMessage),
                };
                _purchaseTransactionsCommand.CreatePurchaseRequest(orderId, transactionId, Provider, purchaseRequest);
            }
            catch (Exception)
            {
            }

            return requestXml;
        }

        private PaymentResponseMessage<TResponseTxn> CreateResponse<TResponseTxn>(Guid orderId, string transactionId, string responseXml)
            where TResponseTxn : PaymentResponseTxn, new()
        {
            try
            {
                // Record this response.

                var purchaseResponse = new PurchaseResponse
                {
                    Time = DateTime.Now,
                    Message = responseXml,
                };
                _purchaseTransactionsCommand.CreatePurchaseResponse(orderId, transactionId, purchaseResponse);
            }
            catch (Exception)
            {
            }

            return Serialization.Deserialize<TResponseTxn>(responseXml);
        }

        private static void Anonymize(RequestCreditCardInfo creditCardInfo)
        {
            creditCardInfo.CardNumber = creditCardInfo.CardNumber.GetCreditCardPan();
            creditCardInfo.Cvv = Anonymize(creditCardInfo.Cvv);
            creditCardInfo.ExpiryDate = Anonymize(creditCardInfo.ExpiryDate);
        }

        private static ExpiryDate Anonymize(ExpiryDate value)
        {
            return new ExpiryDate();
        }

        private static string Anonymize(string value)
        {
            return value == null ? null : new string('X', value.Length);
        }

        private static void AdjustCents<TPaymentResponseTxn>(PaymentResponseMessage<TPaymentResponseTxn> message, int originalCents)
            where TPaymentResponseTxn : PaymentResponseTxn, new()
        {
            message.Payment.TxnList.Txn.Amount = originalCents;
        }

        private static int AdjustCents<TPaymentRequestTxn>(PaymentRequestMessage<TPaymentRequestTxn> requestMessage)
            where TPaymentRequestTxn : PaymentRequestTxn, new()
        {
            var originalAmount = requestMessage.Payment.TxnList.Txn.Amount;
            requestMessage.Payment.TxnList.Txn.Amount = originalAmount - (originalAmount % 100);
            return originalAmount;
        }

        private string Send(string requestXml)
        {
            var request = CreateRequest(requestXml);
            var response = SendRequest(request);
            return ReadResponse(response);
        }

        private static HttpWebResponse SendRequest(HttpWebRequest request)
        {
			try 
			{
                return (HttpWebResponse)request.GetResponse();
			} 
			catch (WebException e) 
			{ 
				if (e.Response == null)
                    throw new ApplicationException("Did not get response from '" + request.Address.AbsoluteUri + "'.", e);
                return (HttpWebResponse)e.Response;
			}
        }

        private HttpWebRequest CreateRequest(string requestXml)
        {
            var request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = requestXml.Length;

            using (var stream = request.GetRequestStream())
            {
                var buffer = Encoding.UTF8.GetBytes(requestXml);
                stream.Write(buffer, 0, buffer.Length);
            }

            // Never time out when debugging.

            if (Debugger.IsAttached)
                request.Timeout = Timeout.Infinite;
            return request;
        }

        private string ReadResponse(HttpWebResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException("Status code " + response.StatusCode + " returned from '" + _url + "'.");

            var encoding = string.IsNullOrEmpty(response.ContentEncoding)
                ? DefaultHttpEncoding
                : Encoding.GetEncoding(response.ContentEncoding);

            using (var reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using System.Threading;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Services.External.Apple.AppStore
{
    public class SendAppleCommand
        : ISendAppleCommand
    {
        private static readonly EventSource EventSource = new EventSource<SendAppleCommand>();
        protected static readonly Encoding DefaultHttpEncoding = Encoding.GetEncoding(28591); // Latin-1
        private readonly JavaScriptConverter[] _converters = new[] { new JsonVerificationJavaScriptConverter() };
        private readonly string _url;

        public SendAppleCommand(string url)
        {
            _url = url;
        }

        #region IPurchasesCommand Members

        JsonVerificationResponse ISendAppleCommand.Verify(string encodedTransactionReceipt)
        {
            const string method = "Verify";

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Sending request to Apple.", Event.Arg("transaction receipt", encodedTransactionReceipt));

            var response = Send(encodedTransactionReceipt);

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Received response from Apple.", Event.Arg("response status", response));

            //attempt to parse the response from Apple
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(_converters);
            var verificationResponse = serializer.Deserialize<JsonVerificationResponse>(response);

            if (verificationResponse.Status != 0)
            {
                EventSource.Raise(Event.Trace, method, "Verification failed", Event.Arg("verification response", response));
            }

            if (verificationResponse.Receipt != null)
            {
                verificationResponse.Receipt.PurchaseDate = verificationResponse.Receipt.PurchaseDate == null
                    ? (DateTime?) null
                    : verificationResponse.Receipt.PurchaseDate.Value.ToUniversalTime();
                verificationResponse.Receipt.OriginalPurchaseDate = verificationResponse.Receipt.OriginalPurchaseDate == null
                    ? (DateTime?) null
                    : verificationResponse.Receipt.OriginalPurchaseDate.Value.ToUniversalTime();
            }

            return verificationResponse;
       }
        #endregion

        private string Send(string encodedTransactionReceipt)
        {
            var requestModel = new JsonVerificationRequest {ReceiptData = encodedTransactionReceipt};
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(_converters);
            var verificationRequest = serializer.Serialize(requestModel);

            var request = CreateRequest(verificationRequest);
            var response = SendRequest(request);
            return ReadResponse(response);
        }

        private HttpWebRequest CreateRequest(string encodedTransactionReceipt)
        {
            var request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = encodedTransactionReceipt.Length;

            using (var stream = request.GetRequestStream())
            {
                var buffer = Encoding.UTF8.GetBytes(encodedTransactionReceipt);
                stream.Write(buffer, 0, buffer.Length);
            }

            // Never time out when debugging.

            if (Debugger.IsAttached)
                request.Timeout = Timeout.Infinite;
            return request;
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

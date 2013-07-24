using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace LinkMe.Framework.Tools.Performance.Http
{
    internal class HttpClient
    {
        private const string UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
        private const int MaxRedirects = 10;
        private static readonly Encoding Encoding = Encoding.UTF8;

        private Uri _referrerUrl;
        private readonly CookieContainer _cookies = new CookieContainer();
        private NameValueCollection _headers;

        public Uri CurrentUrl { get; private set; }

        public CookieContainer Cookies
        {
            get { return _cookies; }
        }

        public NameValueCollection Headers
        {
            get
            {
                if (_headers == null)
                    _headers = new NameValueCollection();
                return _headers;
            }
        }

        public HttpWebRequest CreateGetRequest(string url)
        {
            CurrentUrl = GetUrl(CurrentUrl, url);

            // Create and send the request.

            return CreateRequest();
        }

        public HttpWebRequest CreatePostRequest(string url, string contentType, PostData postData)
        {
            _referrerUrl = CurrentUrl;
            CurrentUrl = GetUrl(CurrentUrl, url);

            // Create and send the request.

            return CreateRequest(contentType, postData);
        }

        public HttpWebResponse Send(HttpWebRequest request)
        {
            return SendRequest(request);
        }

        public IAsyncResult BeginSend(HttpWebRequest request, AsyncCallback callback, object state)
        {
            return BeginSendRequest(request, callback, state);
        }

        private static string RemoveFragment(string url)
        {
            var pos = url.IndexOf('#');
            return pos < 0 ? url : url.Substring(0, pos);
        }

        private static Uri GetUrl(Uri currentUrl, string url)
        {
            url = RemoveFragment(url);
            return currentUrl == null ? new Uri(url) : new Uri(currentUrl, url);
        }

        private static Uri GetUrl(WebResponse response)
        {
            return new Uri(RemoveFragment(response.ResponseUri.AbsoluteUri));
        }

        private HttpWebRequest CreateRequest(string contentType, PostData postData)
        {
            var request = CreateRequest();
            request.Method = "POST";

            if (postData is PostDataValues)
                WriteRequestBody(request, (PostDataValues) postData);
            else
                WriteRequestBody(request, contentType ?? "text/plain; charset=utf-8", (RawPostData) postData);

            return request;
        }

        private HttpWebRequest CreateRequest()
        {
            var request = (HttpWebRequest)WebRequest.Create(CurrentUrl);

            request.Method = "GET";
            request.CookieContainer = _cookies;
            request.UserAgent = UserAgent;
            request.AllowAutoRedirect = true;
            request.MaximumAutomaticRedirections = MaxRedirects;

            // Add headers.

            if (_headers != null && _headers.Count > 0)
                request.Headers.Add(_headers);
            _headers = null;

            // Never time out when debugging.

            if (Debugger.IsAttached)
                request.Timeout = Timeout.Infinite;

            if (_referrerUrl != null)
                request.Referer = _referrerUrl.AbsoluteUri;

            return request;
        }

        private static void WriteRequestBody(WebRequest request, string contentType, RawPostData postData)
        {
            request.ContentType = contentType;
            var body = CreateBody(postData);
            request.ContentLength = body.Length;

            using (var writer = new BinaryWriter(request.GetRequestStream()))
            {
                writer.Write(body);
            }
        }

        private static byte[] CreateBody(RawPostData postData)
        {
            var stream = new MemoryStream();
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(Encoding.GetBytes(postData.Value));
            }

            return stream.ToArray();
        }

        private static void WriteRequestBody(WebRequest request, IEnumerable<PostValue> postData)
        {
            if (ContainsFiles(postData))
                WriteRequestBodyMultipart(request, postData);
            else
                WriteRequestBodyUrlEncoded(request, postData);
        }

        private static bool ContainsFiles(IEnumerable<PostValue> postData)
        {
            foreach (var value in (postData))
            {
                if (value is FilePostValue)
                    return true;
            }

            return false;
        }

        private static void WriteRequestBodyMultipart(WebRequest request, IEnumerable<PostValue> postData)
        {
            const string boundary = "7d63bd35a0382";
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            var body = CreateBody(postData, boundary);
            request.ContentLength = body.Length;

            using (var writer = new BinaryWriter(request.GetRequestStream()))
            {
                writer.Write(body);
            }
        }

        private static byte[] CreateBody(IEnumerable<PostValue> postData, string boundary)
        {
            var stream = new MemoryStream();
            using (var writer = new BinaryWriter(stream))
            {
                foreach (var value in postData)
                    value.Write(writer, Encoding, boundary);
                writer.Write(Encoding.GetBytes(string.Format("--{0}--\r\n", boundary)));
            }

            return stream.ToArray();
        }

        private static void WriteRequestBodyUrlEncoded(WebRequest request, IEnumerable<PostValue> postData)
        {
            request.ContentType = "application/x-www-form-urlencoded";
            var body = CreateBody(postData);
            request.ContentLength = body.Length;

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(body);
            }
        }

        private static string CreateBody(IEnumerable<PostValue> postData)
        {
            var sb = new StringBuilder();
            var first = true;

            foreach (var value in postData)
            {
                value.Write(sb, first);
                if (first)
                    first = false;
            }

            return sb.ToString();
        }

        private static string GetRedirectUrl(WebResponse response)
        {
            string location = response.Headers["Location"];
            if (location == null)
            {
                throw new ApplicationException("Expected Location header in HTTP response");
            }
            return location;
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
                    throw new ApplicationException("Failed to get HTTP response from " + request.Address.AbsoluteUri, e);
                return (HttpWebResponse)e.Response;
            }
        }

        private static IAsyncResult BeginSendRequest(HttpWebRequest request, AsyncCallback callback, object state)
        {
            try
            {
                return request.BeginGetResponse(callback, state);
            }
            catch (WebException e)
            {
                throw new ApplicationException("Failed to get HTTP response from " + request.Address.AbsoluteUri, e);
            }
        }

        public string ReadResponse(HttpWebResponse response, Stream stream)
        {
            CurrentUrl = GetUrl(response);

            // Check for bad status codes which indicate that nothing has been returned.

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new NotFoundException(CurrentUrl);

                case HttpStatusCode.Redirect:
                    throw new RedirectLoopException(GetRedirectUrl(response));
            }

            // Read the body of the response.

            string body;
            using (var reader = new StreamReader(stream))
            {
                body = reader.ReadToEnd();
            }

            // Check for bad status codes when something has been returned.

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                    throw new AspServerException(null);
                throw new BadStatusException(response.StatusCode);
            }

            return body;
        }

        /// <summary>
        /// Exception: The requested URL was not found.  Correct the URL or determine what's wrong
        /// with the server.
        /// </summary>
        public class NotFoundException : ApplicationException
        {
            public NotFoundException(Uri url)
                : base("404 Not Found for " + url)
            {
            }
        }

        /// <summary>
        /// Exception: The requested URL caused an unhandled exception on the ASP.NET server.
        /// Fix the production code so it doesn't throw the exception.
        /// </summary>
        public class AspServerException : ApplicationException
        {
            public AspServerException(string exceptionStackTrace) :
                base("Server threw an exception:\r\n" + exceptionStackTrace)
            {
            }
        }

        /// <summary>
        /// Exception: The server returned an unexpected status code.  Determine what's wrong with
        /// the server.
        /// </summary>
        public class BadStatusException : ApplicationException
        {
            /// <summary>
            /// The HTTP status code returned by the server
            /// </summary>
            public readonly HttpStatusCode Status;

            public BadStatusException(HttpStatusCode status) :
                base("Server returned error (status code: " + (int)status + ").  HTML copied to standard output.")
            {
                Status = status;
            }
        }

        /// <summary>
        /// Exception: Too many HTTP redirects were detected. Check for infinite redirection loop.
        /// </summary>
        public class RedirectLoopException : ApplicationException
        {
            /// <summary>
            /// The target URL of the failed redirect
            /// </summary>
            public readonly string TargetUrl;

            public RedirectLoopException(string targetUrl)
                : base(GetMessage(targetUrl))
            {
                TargetUrl = targetUrl;
            }

            private static string GetMessage(string targetUrl)
            {
                return string.Format(
                    "Redirect loop detected: more than {0} redirections.  Most recent redirect was to {1}",
                    MaxRedirects, targetUrl);
            }
        }
    }
}

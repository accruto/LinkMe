using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using HtmlAgilityPack;

namespace LinkMe.Apps.Asp.Test.Html
{
    /// <summary>
    /// A web client, capable of communicating with a web server.
    /// </summary>
    public class HttpClient
    {
        private const string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
        private const string MobileUserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_0 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9A334 Safari/7534.48.3";

        private const int MaxRedirects = 10;
        private static readonly Encoding Encoding = Encoding.UTF8;

        private HtmlPage _currentPage;
        private CookieContainer _cookies = new CookieContainer();
        private Uri _urlReferrer;
        private string _userAgent = DefaultUserAgent;

        public Uri CurrentUrl { get; private set; }
        public HttpStatusCode CurrentStatusCode { get; private set; }
        public WebHeaderCollection RequestHeaders { get; set; }
        public WebHeaderCollection ResponseHeaders { get; private set; }

        public CookieContainer Cookies
        {
            get { return _cookies; }
            set { _cookies = value; }
        }

        public bool UseMobileUserAgent
        {
            get { return _userAgent == MobileUserAgent; }
            set { _userAgent = value ? MobileUserAgent : DefaultUserAgent; }
        }

        public string Get(HttpStatusCode? expectedStatusCode, string url)
        {
            var response = SendRequest(expectedStatusCode, "GET", url, r => { });
            _currentPage = new HtmlPage(response);
            return response;
        }

        public string Post(HttpStatusCode? expectedStatusCode, string url, NameValueCollection formVariables, NameValueCollection fileVariables)
        {
            return SendRequest(expectedStatusCode, "POST", url, r => WriteRequest(r, formVariables ?? new NameValueCollection(), fileVariables));
        }

        public string Post(HttpStatusCode? expectedStatusCode, string url)
        {
            return SendRequest(expectedStatusCode, "POST", url, WriteRequest);
        }

        public string Post(HttpStatusCode? expectedStatusCode, string url, string contentType, string content)
        {
            return SendRequest(expectedStatusCode, "POST", url, r => WriteRequest(r, contentType, content));
        }

        public string Put(HttpStatusCode? expectedStatusCode, string url)
        {
            return SendRequest(expectedStatusCode, "PUT", url, WriteRequest);
        }

        public string Put(HttpStatusCode? expectedStatusCode, string url, string contentType, string content)
        {
            return SendRequest(expectedStatusCode, "PUT", url, r => WriteRequest(r, contentType, content));
        }

        public string Put(HttpStatusCode? expectedStatusCode, string url, NameValueCollection formVariables, NameValueCollection fileVariables)
        {
            return SendRequest(expectedStatusCode, "PUT", url, r => WriteRequest(r, formVariables ?? new NameValueCollection(), fileVariables));
        }

        public string Delete(HttpStatusCode? expectedStatusCode, string url)
        {
            return SendRequest(expectedStatusCode, "DELETE", url, WriteRequest);
        }

        public string Head(HttpStatusCode? expectedStatusCode, string url)
        {
            var response = SendRequest(expectedStatusCode, "HEAD", url, r => { });
            _currentPage = new HtmlPage(response);
            return response;
        }

        private string SendRequest(HttpStatusCode? expectedStatusCode, string method, string url, Action<HttpWebRequest> writeRequest)
        {
            _currentPage = null;
            UpdateCurrentUrl(url);
            var request = CreateRequest(method);
            writeRequest(request);
            return SendRequest(expectedStatusCode, request);
        }

        public void Submit(string formId)
        {
            var form = GetForm(formId);
            _currentPage = new HtmlPage(SendRequest(HttpStatusCode.OK, form.Method, form.Action, r => WriteRequest(r, form.FormVariables, form.FileVariables)));
        }

        internal void AddFormVariable(string formId, string name, string value, bool isFile)
        {
            GetForm(formId).AddFormVariable(name, value, isFile);
        }

        public void SetFormVariable(string formId, string name, string value, bool isFile)
        {
            GetForm(formId).SetFormVariable(name, value, isFile);
        }

        public void ClearFormVariable(string formId, string name)
        {
            GetForm(formId).ClearFormVariable(name);
        }

        public string CurrentPageText
        {
            get { return _currentPage == null ? null : _currentPage.ToString(); }
        }

        public HtmlDocument CurrentHtml
        {
            get
            {
                if (_currentPage == null)
                    throw new ApplicationException("No pages have been loaded by the browser");
                return _currentPage.HtmlDocument;
            }
        }

        private HtmlForm GetForm(string formId)
        {
            if (!string.IsNullOrEmpty(formId))
                return _currentPage.HtmlForms.Single(f => f.Id == formId);

            // Choose the only form.

            if (_currentPage.HtmlForms.Count != 1)
                throw new InvalidOperationException("There is not a single form in the page.");

            return _currentPage.HtmlForms[0];
        }

        private static string TrimFragment(string url)
        {
            var index = url.IndexOf('#');
            return index == -1 ? url : url.Substring(0, index);
        }

        private void UpdateCurrentUrl(string url)
        {
            _urlReferrer = CurrentUrl;
            url = TrimFragment(url);
            CurrentUrl = CurrentUrl == null ? new Uri(url) : new Uri(CurrentUrl, url);
        }

        private HttpWebRequest CreateRequest(string method)
        {
            var request = (HttpWebRequest)WebRequest.Create(CurrentUrl);

            request.Method = method.ToUpper();
            request.CookieContainer = _cookies;
            request.UserAgent = _userAgent;
            request.AllowAutoRedirect = true;
            request.MaximumAutomaticRedirections = MaxRedirects;
            if (_urlReferrer != null)
                request.Referer = _urlReferrer.AbsoluteUri;

            if (RequestHeaders != null)
            {
                // Use the appropriate property for some headers.

                var accept = RequestHeaders["Accept"];
                if (accept != null)
                    RequestHeaders.Remove("Accept");

                request.Headers = RequestHeaders;
                if (accept != null)
                    request.Accept = accept;

                // Once used don't use them again.

                RequestHeaders = null;
            }

            // Never time out when debugging.

            if (Debugger.IsAttached)
                request.Timeout = Timeout.Infinite;

            return request;
        }

        private static void WriteRequest(WebRequest request, string contentType, string content)
        {
            request.ContentType = contentType;
            request.ContentLength = content.Length;

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(content);
            }
        }

        private static void WriteRequest(WebRequest request, NameValueCollection formVariables, NameValueCollection fileVariables)
        {
            if (HasFile(fileVariables))
                WriteRequestMultiPart(request, formVariables, fileVariables);
            else
                WriteRequest(request, "application/x-www-form-urlencoded", GetContent(formVariables));
        }

        private static void WriteRequest(WebRequest request)
        {
            WriteRequest(request, "application/x-www-form-urlencoded", "");
        }

        private static bool HasFile(NameValueCollection fileVariables)
        {
            if (fileVariables == null)
                return false;

            // The file variable must actually have a value.

            foreach (var key in fileVariables.AllKeys)
            {
                var values = fileVariables.GetValues(key);
                if (values != null)
                {
                    foreach (var value in values)
                    {
                        if (!string.IsNullOrEmpty(value))
                            return true;
                    }
                }
            }

            return false;
        }

        private static void WriteRequestMultiPart(WebRequest request, NameValueCollection formVariables, NameValueCollection fileVariables)
        {
            const string boundary = "7d63bd35a0382";
			
            var contents = GetFormVariablesMultiPart(boundary, formVariables, fileVariables);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.ContentLength = contents.Length;

            using (var writer = new BinaryWriter(request.GetRequestStream()))
            {
                writer.Write(contents);
            }
        }

        private static byte[] GetFormVariablesMultiPart(string boundary, NameValueCollection formVariables, NameValueCollection fileVariables)
        {
            var stream = new MemoryStream();
			
            using (var writer = new BinaryWriter(stream))
            {
                // Write form variables first.

                foreach (var key in formVariables.AllKeys)
                {
                    var values = formVariables.GetValues(key);
                    if (values != null)
                    {
                        foreach (var value in values)
                        {
                            writer.Write(Encoding.GetBytes(string.Format("--{0}\r\n", boundary)));
                            writer.Write(Encoding.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n", key)));
                            writer.Write(Encoding.GetBytes("\r\n"));
                            writer.Write(Encoding.GetBytes(value));
                            writer.Write(Encoding.GetBytes("\r\n"));
                        }
                    }
                }

                // Write file variables.

                foreach (var key in fileVariables.AllKeys)
                {
                    var values = fileVariables.GetValues(key);
                    if (values != null)
                    {
                        foreach (var value in values)
                        {
                            // Only write it out if in fact there is a file.

                            if (!string.IsNullOrEmpty(value))
                            {
                                writer.Write(Encoding.GetBytes(string.Format("--{0}\r\n", boundary)));
                                writer.Write(Encoding.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n", key, value)));
                                writer.Write(Encoding.GetBytes("Content-Type: application/octet-stream"));
                                writer.Write(Encoding.GetBytes("\r\n\r\n"));
                                WriteFileContents(writer, value);
                                writer.Write(Encoding.GetBytes("\r\n"));
                            }
                        }
                    }
                }

                writer.Write(Encoding.GetBytes(string.Format("--{0}--\r\n", boundary)));
            }

            return stream.ToArray();
        }

        private static void WriteFileContents(BinaryWriter writer, string fileName)
        {
            // Load the contents from the file.

            using (var reader = new BinaryReader(File.OpenRead(fileName)))
            {
                var count = 4096;
                var buffer = new byte[count];
                while ((count = reader.Read(buffer, 0, count)) > 0)
                    writer.Write(buffer, 0, count);
            }
        }

        private static string GetContent(NameValueCollection formVariables)
        {
            var sb = new StringBuilder();

            foreach (var key in formVariables.AllKeys)
            {
                var values = formVariables.GetValues(key);
                if (values != null)
                {
                    foreach (var value in values)
                    {
                        if (sb.Length > 0)
                            sb.Append("&");
                        sb.AppendFormat("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value));
                    }
                }
            }

            return sb.ToString();
        }

        private static string GetRedirectUrl(WebResponse response)
        {
            var location = response.Headers["Location"];
            if (location == null)
                throw new ApplicationException("Expected Location header in HTTP response");
            return location;
        }

        private string SendRequest(HttpStatusCode? expectedStatusCode, HttpWebRequest request)
        {
            try 
            {
                var response = (HttpWebResponse)request.GetResponse();
                return ReadResponse(expectedStatusCode, response, null);
            } 
            catch (WebException e)
            {
                if (e.Response == null)
                    throw new ApplicationException("Failed to get HTTP response from " + request.Address.AbsoluteUri, e);
                return ReadResponse(expectedStatusCode, (HttpWebResponse)e.Response, e);
            }
        }

        private string ReadResponse(HttpStatusCode? expectedStatusCode, HttpWebResponse response, WebException ex)
        {
            CurrentUrl = new Uri(TrimFragment(response.ResponseUri.AbsoluteUri));
            CurrentStatusCode = response.StatusCode;
            ResponseHeaders = response.Headers;

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    if (expectedStatusCode == null || expectedStatusCode.Value != HttpStatusCode.NotFound)
                        throw new NotFoundException(response.ResponseUri);
                    break;

                case HttpStatusCode.MovedPermanently:
                    if (ex != null && ex.Message == "Too many automatic redirections were attempted.")
                        throw new RedirectLoopException(MaxRedirects, GetRedirectUrl(response));
                    break;

                case HttpStatusCode.Redirect:
                    throw new RedirectLoopException(MaxRedirects, GetRedirectUrl(response));

                case HttpStatusCode.InternalServerError:
                    if (expectedStatusCode == null || expectedStatusCode.Value != HttpStatusCode.InternalServerError)
                        throw new BadStatusException(response.StatusCode);
                    break;
            }

            if (expectedStatusCode != null && response.StatusCode != expectedStatusCode.Value)
            {
                if (response.StatusCode != HttpStatusCode.InternalServerError)
                    throw new BadStatusException(response.StatusCode);
            }

            string body;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                body = reader.ReadToEnd();
            }

            return body;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;

namespace LinkMe.Framework.Tools.Performance.Http
{
    public abstract class HttpTestFixture
        : ProfileTestFixture, IAsyncProfileTestFixture
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _applicationPath;

        private const int BufferSize = 32768;
        private static Application _application;
        private readonly HttpClient _client = new HttpClient();
        private string _url;
        private string _contentType;
        private NameValueCollection _formVariables;
        private IDictionary<string, string> _headers;
        private bool _isGet = true;
        private MethodInfo _endMethod;

        protected HttpTestFixture(string host, int? port, string applicationPath)
        {
            _host = host;
            _port = port ?? -1;
            _applicationPath = !string.IsNullOrEmpty(applicationPath) ? applicationPath : "/";
        }

        private class RequestState
        {
            public IAsyncTestResult Result;
            public HttpWebRequest Request;
            public HttpWebResponse Response;
            public Stream ResponseStream;
            public MemoryStream OutputStream;
            public byte[] ReadBuffer;
            public int ResponseCallbackCount;
        }

        protected override void SetUp()
        {
            base.SetUp();

            // Set up the application for the web site.

            _application = new Application(_host, _port, _applicationPath);
        }

        protected HttpContent CurrentContent { get; private set; }

        protected Url CurrentUrl
        {
            get { return new Url(_client.CurrentUrl); }
        }

        protected ApplicationUrl GetUrl(bool? secure, string path)
        {
            return new ApplicationUrl(_application, secure, path);
        }

        protected void Get(string path)
        {
            Get(null, path);
        }

        protected void Get(bool? secure, string path)
        {
            SetUrl(secure, path);
            _isGet = true;
        }

        protected void Post(bool? secure, string path)
        {
            SetUrl(secure, path);
            _isGet = false;
        }

        protected void Post(NameValueCollection formVariables)
        {
            // Post to the current form.

            _url = _client.CurrentUrl.PathAndQuery;
            _formVariables = formVariables;
            _isGet = false;
        }

        protected void Post(bool? secure, string path, NameValueCollection formVariables)
        {
            SetUrl(secure, path);
            _formVariables = formVariables;
            _isGet = false;
        }

        protected void AddHeader(string name, string value)
        {
            if (_headers == null)
                _headers = new Dictionary<string, string>();
            _headers[name] = value;
        }

        protected void SetPostData(string contentType, string value)
        {
            _contentType = contentType;
            if (CurrentContent == null)
                CurrentContent = new HttpContent(contentType, "");
            CurrentContent.RawPostData.Value = value;
        }

        protected void AddPostData(string name, string value)
        {
            CurrentContent.PostDataValues.Add(new SimplePostValue(name, value));
        }

        protected void AddFilePostData(string name, string fileName, byte[] fileContents)
        {
            CurrentContent.PostDataValues.Add(new FilePostValue(name, fileName, fileContents));
        }

        protected string GetLink(string id)
        {
            var node = CurrentContent.Document.DocumentNode.SelectSingleNode("//a[@id='" + id + "']");
            return node == null ? null : node.SelectSingleNode("@href").InnerText;
        }

        void IAsyncProfileTestFixture.Begin(IAsyncTestResult result, MethodInfo beginMethod, MethodInfo endMethod)
        {
            // Store the end method and invoke the begin right now.

            _endMethod = endMethod;
            beginMethod.Invoke(this, null);

            // Everything should now be set up.

            BeginSend(result);
        }

        protected void Send()
        {
            Prepare();

            // Send it.

            var request = _isGet ? CreateGetRequest() : CreatePostRequest();

            var timer = GetTimer();
            if (timer != null)
                timer.Start();
            var response = _client.Send(request);
            if (timer != null)
                timer.Stop();
            var content = _client.ReadResponse(response, response.GetResponseStream());

            // Process it.

            Process(content);
        }

        private void BeginSend(IAsyncTestResult result)
        {
            Prepare();

            // Create the request.

            var request = _isGet ? CreateGetRequest() : CreatePostRequest();

            // Set up RequestState.

            var state = new RequestState {Request = request, Result = result};

            // Send it.

            var timer = GetTimer();
            timer.Start();
            var innerResult = _client.BeginSend(request, ResponseCallback, state);

            // Check whether it has already been done.

            if (innerResult.CompletedSynchronously)
            {
                timer.Stop();
                var response = (HttpWebResponse) request.GetResponse();
                var content = _client.ReadResponse(response, response.GetResponseStream());

                // End it.

                Process(content);
                End(result, true);
            }
            else
            {
                // Register timeout callback.

                ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, ResponseTimeoutCallback, state, 60000, true);
            }
        }

        private void SetUrl(bool? secure, string path)
        {
            var url = GetUrl(secure, path);
            if (_client.CurrentUrl == null || url.IsAbsolute)
                _url = url.AbsoluteUri;
            else
                _url = url.PathAndQuery;
        }

        private HttpWebRequest CreateGetRequest()
        {
            return _client.CreateGetRequest(_url);
        }

        private HttpWebRequest CreatePostRequest()
        {
            var postData = PreparePost();
            return _client.CreatePostRequest(_url, _contentType, postData);
        }

        private void Prepare()
        {
            // Add headers.

            if (_headers != null)
            {
                foreach (var header in _headers)
                    _client.Headers[header.Key] = header.Value;
            }
        }

        private PostData PreparePost()
        {
            if (_formVariables != null && _formVariables.Count > 0)
            {
                var postDataValues = CurrentContent.PostDataValues;
                foreach (var key in _formVariables.AllKeys)
                {
                    var values = _formVariables.GetValues(key);
                    if (values != null)
                    {
                        foreach (var value in values)
                            postDataValues.Add(new SimplePostValue(key, value));
                    }
                }

                return postDataValues;
            }

            return CurrentContent.PostData;
        }

        private void Process(string content)
        {
            CurrentContent = new HttpContent(_contentType, content);

            // Clear everything out.

            _url = null;
            _formVariables = null;
            _headers = null;
        }

        private void ResponseCallback(IAsyncResult innerResult)
        {
            // Check whether anything needs to be done.

            if (innerResult.IsCompleted && innerResult.CompletedSynchronously)
                return;

            var state = (RequestState)innerResult.AsyncState;

            // Check that the timeout has not already fired.

            if (Interlocked.Increment(ref state.ResponseCallbackCount) != 1)
                return;
            
            try
            {
                // Get the response.

                state.Response = (HttpWebResponse)state.Request.EndGetResponse(innerResult);
                state.ResponseStream = state.Response.GetResponseStream();
                state.ReadBuffer = new byte[BufferSize];
                state.OutputStream = new MemoryStream();

                // Begin reading the response data.
                
                state.ResponseStream.BeginRead(state.ReadBuffer, 0, BufferSize, ReadCallback, state);
            }
            catch (Exception ex)
            {
                state.Result.SetComplete(ex, false);
            }
        }

        private static void ResponseTimeoutCallback(object asyncState, bool timedOut)
        {
            var state = (RequestState)asyncState;

            // Check that it is a timeout and that the response has not already been processed.

            if (!timedOut || Interlocked.Increment(ref state.ResponseCallbackCount) != 1)
                return;

            if (state.Request != null)
                state.Request.Abort();

            // Complete the request, flagging it as timed out.
            
            var ex = new ApplicationException("Timeout");
            state.Result.SetComplete(ex, false);
        }

        private void ReadCallback(IAsyncResult result)
        {
            var state = (RequestState)result.AsyncState;
            var bytesRead = state.ResponseStream.EndRead(result);

            // Check for more data.

            if (bytesRead > 0)
            {
                // Store and try to read some more.

                state.OutputStream.Write(state.ReadBuffer, 0, bytesRead);
                state.ResponseStream.BeginRead(state.ReadBuffer, 0, BufferSize, ReadCallback, state);
            }
            else
            {
                // No more data to read so stop the clock.

                GetTimer().Stop();

                // Read all the response data.

                state.ResponseStream.Close();
                state.OutputStream.Seek(0, SeekOrigin.Begin);
                var content = _client.ReadResponse(state.Response, state.OutputStream);

                // Process the response.

                Process(content);
                End(state.Result, false);
            }
        }

        private void End(IAsyncTestResult result, bool completedSynchronously)
        {
            try
            {
                // Give the test case a chance to check the response.

                _endMethod.Invoke(this, null);

                // Mark it as complete.

                result.SetComplete(completedSynchronously);
            }
            catch (Exception ex)
            {
                result.SetComplete(ex, completedSynchronously);
            }
        }
    }
}

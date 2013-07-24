using System;
using System.Net;

namespace LinkMe.Apps.Asp.Test.Html
{
    public class NotFoundException
        : ApplicationException
    {
        public NotFoundException(Uri url)
            : base("404 Not Found for " + url)
        {
        }
    }

    public class BadStatusException
        : ApplicationException
    {
        public HttpStatusCode Status { get; private set; }

        internal BadStatusException(HttpStatusCode status) :
            base("Server returned error (status code: " + (int)status + ").  HTML copied to standard output.")
        {
            Status = status;
        }
    }

    public class RedirectLoopException
        : ApplicationException
    {
        public string TargetUrl { get; private set; }

        internal RedirectLoopException(int maxRedirects, string targetUrl)
            : base(GetMessage(maxRedirects, targetUrl))
        {
            TargetUrl = targetUrl;
        }

        private static string GetMessage(int maxRedirects, string targetUrl)
        {
            return string.Format("Redirect loop detected: more than {0} redirections.  Most recent redirect was to {1}", maxRedirects, targetUrl);
        }
    }
}

using System;
using System.Collections.Specialized;
using System.Web;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Urls
{
    public static class UrlsExtensions
    {
        public static ReadOnlyUrl GetClientUrl(this HttpContextBase context)
        {
            return GetClientUrl(context, context.Request.IsSecureConnection);
        }

        public static ReadOnlyUrl GetClientUrl(this HttpContext context)
        {
            return GetClientUrl(context, context.Request.IsSecureConnection);
        }

        public static ReadOnlyUrl GetClientUrl(this HttpContextBase context, bool isSecureConnection)
        {
            var xRewritePathAndQuery = GetRewritePathAndQuery(context.Request.Headers);

            // If no rewrite has happened then the path is what is seen by the client.

            var xRewritePath = xRewritePathAndQuery.RemoveQueryString();
            if (string.IsNullOrEmpty(xRewritePath) || xRewritePath == context.Request.Path)
                return new ReadOnlyUrl(context.Request.Url);

            // Construct the client url using the original url and query string.

            return new ReadOnlyApplicationUrl(isSecureConnection, xRewritePathAndQuery);
        }

        public static ReadOnlyUrl GetClientUrl(this HttpContext context, bool isSecureConnection)
        {
            var xRewritePathAndQuery = GetRewritePathAndQuery(context.Request.Headers);

            // If no rewrite has happened then the path is what is seen by the client.

            var xRewritePath = xRewritePathAndQuery.RemoveQueryString();
            if (string.IsNullOrEmpty(xRewritePath) || xRewritePath == context.Request.Path)
                return new ReadOnlyUrl(context.Request.Url);

            // Construct the client url using the original url and query string.

            return new ReadOnlyApplicationUrl(isSecureConnection, xRewritePathAndQuery);
        }

        private static string GetRewritePathAndQuery(NameValueCollection headers)
        {
            // Check to see whether ISAPI_Rewrite has done any rewriting of URLs.

            var xRewritePathAndQuery = headers["X-Rewrite-URL"];
            if (string.IsNullOrEmpty(xRewritePathAndQuery))
            {
                // Fail completely if ISAPI_Rewrite is missing instead of infinitely redirecting.

                throw new ApplicationException("X-Rewrite-URL HTTP header was not present in the request. Make sure ISAPI_Rewrite is installed.");
            }

            return xRewritePathAndQuery;
        }
    }
}

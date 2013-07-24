using System;
using System.Web;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public static class ErrorsControllerExtensions
    {
        public static ReadOnlyUrl GetRequestedUrl(this string url)
        {
            // Validate the url.

            if (string.IsNullOrEmpty(url))
                return null;

            var requestedUrl = TryParseUrl(url);
            if (HtmlUtil.ContainsScript(requestedUrl.ToString()))
                return null;

            return requestedUrl;
        }

        public static ReadOnlyUrl GetReferrerUrl(this string url, ReadOnlyUrl requestedUrl)
        {
            // It may be external, but must not contain script.

            if (string.IsNullOrEmpty(url))
                return null;

            var referrerUrl = TryParseUrl(url);
            if (referrerUrl != null)
            {
                if (requestedUrl != null && referrerUrl.ToString().IndexOf(requestedUrl.AbsolutePath) != -1)
                    return null;
                if (HtmlUtil.ContainsScript(referrerUrl.ToString()))
                    return null;
            }

            return referrerUrl;
        }

        private static ReadOnlyUrl TryParseUrl(string url)
        {
            const int maxExceptionCount = 10;

            // Cases 4639, 5688 - sometimes the URL is "double-encoded" for some reason. Try decoding it once.

            string oldUrl = null;
            var exceptionCount = 0;
            Exception lastException;

            do
            {
                try
                {
                    oldUrl = url;
                    return new ReadOnlyApplicationUrl(url);
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    if (++exceptionCount > maxExceptionCount)
                        throw;
                }
            }
            while ((url = HttpUtility.UrlDecode(url)) != oldUrl);

            throw new ApplicationException("Failed to parse URL '" + url + "'.", lastException);
        }
    }
}
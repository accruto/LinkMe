using System;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Configuration;

namespace LinkMe.Web.Html
{
    public static class CanonicalUrlExtensions
    {
        private static readonly string NonVerticalHost = ApplicationContext.Instance.GetProperty("website.linkme.host");

        public static ReadOnlyUrl GetCanonicalUrl(this ReadOnlyUrl url)
        {
            var hasNonEmptyQueryString = url.QueryString.Count != 0;
            var hasNonVerticalHost = string.Compare(url.Host, NonVerticalHost, StringComparison.InvariantCultureIgnoreCase) != 0;

            // If already canonical then return it.

            if (!hasNonEmptyQueryString && !hasNonVerticalHost)
                return url;

            // Clean it up.

            var canonicalUrl = url.AsNonReadOnly();
            if (hasNonEmptyQueryString)
                canonicalUrl.QueryString.Clear();
            if (hasNonVerticalHost)
                canonicalUrl.Host = NonVerticalHost;
            return canonicalUrl;
        }
    }
}

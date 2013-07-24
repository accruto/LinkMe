using System;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Content
{
    public abstract class VersionedUrl
        : ReadOnlyApplicationUrl
    {
        protected VersionedUrl(string prefix, Version version, string url)
            : base(GetUrl(prefix, version, url))
        {
        }

        protected VersionedUrl(string url)
            : base(url)
        {
        }

        private static string GetUrl(string prefix, Version version, string url)
        {
            // Insert the version into the final segment of the url if it is an application url.

            if (url.Length == 0)
                return url;

            if (!url.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                return url;

            return prefix + version + Url.PathSeparatorChar + url.Substring(prefix.Length);
        }
    }
}

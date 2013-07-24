using System;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Content
{
    public class BundleUrl
        : VersionedUrl
    {
        private static readonly string BundlePrefix = Application.ApplicationPathStartChar + "/bundle/";

        public BundleUrl(Version version, string url)
            : base(BundlePrefix, version, GetUrl(url))
        {
        }

        public BundleUrl(string url)
            : base(url)
        {
        }

        private static string GetUrl(string url)
        {
            // Attach an .aspx on the end to get through IIS6 non-extension support.

            if (!url.EndsWith(".aspx"))
                return url + ".aspx";
            return url;
        }
    }
}

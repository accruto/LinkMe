using System;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Content
{
    public class ContentUrl
        : VersionedUrl
    {
        private static readonly string ContentPrefix = Application.ApplicationPathStartChar + "/content/";

        public ContentUrl(Version version, string url)
            : base(ContentPrefix, version, url)
        {
        }

        public ContentUrl(string url)
            : base(url)
        {
        }
    }
}

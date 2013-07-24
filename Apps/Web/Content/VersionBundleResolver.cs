using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Optimization;
using LinkMe.Apps.Asp.Content;
using LinkMe.Environment;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Content
{
    public class VersionBundleResolver
        : BundleResolver, IBundleResolver
    {
        private static readonly Version Version = StaticEnvironment.GetFileVersion(Assembly.GetExecutingAssembly());

        IEnumerable<string> IBundleResolver.GetBundleContents(string virtualPath)
        {
            return from u in GetBundleContents(virtualPath)
                   select (Application.ApplicationPathStartChar + new ContentUrl(Version, u).AppRelativePathAndQuery);
        }
    }
}
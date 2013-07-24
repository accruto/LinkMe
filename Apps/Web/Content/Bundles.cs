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
    public class Bundles
    {
        private static readonly Version Version = StaticEnvironment.GetFileVersion(Assembly.GetExecutingAssembly());

        protected static BundleUrl AddScriptBundle(BundleCollection bundles, string bundleUrl, params string[] contentUrls)
        {
            return AddBundle(bundles, bundleUrl, contentUrls, p => new ScriptBundle(p), c => c);
        }

        protected static BundleUrl AddScriptBundle(BundleCollection bundles, string bundleUrl, params JavaScriptReference[] references)
        {
            return AddBundle(bundles, bundleUrl, references, p => new ScriptBundle(p), c => c.Path);
        }

        protected static BundleUrl AddStyleBundle(BundleCollection bundles, string bundleUrl, params string[] contentUrls)
        {
            return AddBundle(bundles, bundleUrl, contentUrls, p => new StyleBundle(p), c => c);
        }

        protected static BundleUrl AddStyleBundle(BundleCollection bundles, string bundleUrl, params StyleSheetReference[] references)
        {
            return AddBundle(bundles, bundleUrl, references, p => new StyleBundle(p), c => c.Path);
        }

        private static BundleUrl AddBundle<TContent>(BundleCollection bundles, string bundleUrl, IEnumerable<TContent> contents, Func<string, Bundle> createBundle, Func<TContent, string> getContentUrl)
        {
            var url = new BundleUrl(Version, bundleUrl);
            var bundle = createBundle(Application.ApplicationPathStartChar + url.AppRelativePathAndQuery).Include((from c in contents select getContentUrl(c)).ToArray());
            bundles.Add(bundle);
            return url;
        }
    }
}
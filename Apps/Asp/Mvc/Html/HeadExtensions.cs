using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Content;
using LinkMe.Apps.Asp.Elements;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public static class HeadExtensions
    {
        public static string Base<THtmlHelper>(this THtmlHelper htmlHelper, ReadOnlyUrl url)
        {
            var builder = new TagBuilder("base");

            // Make sure there is no query string.

            string href;
            if (url.QueryString.Count > 0)
            {
                var hrefUrl = url.AsNonReadOnly();
                hrefUrl.QueryString.Clear();
                href = hrefUrl.AbsoluteUri;
            }
            else
            {
                href = url.AbsoluteUri;
            }

            builder.MergeAttribute("href", href);
            return builder.ToString(TagRenderMode.StartTag);
        }

        public static string MetaTags<THtmlHelper>(this THtmlHelper htmlHelper, MetaTags metaTags)
        {
            // Add an element for each name-content pair.

            var sb = new StringBuilder();
            if (metaTags != null)
            {
                foreach (var metaTag in metaTags)
                    sb.AppendLine(MetaTag(metaTag));
            }

            return sb.ToString();
        }

        public static MvcHtmlString MetaTag<THtmlHelper>(this THtmlHelper htmlHelper, MetaTag metaTag)
        {
            // Add an element for each name-content pair.

            var sb = new StringBuilder();
            sb.AppendLine(MetaTag(metaTag));
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString MetaTag<THtmlHelper>(this THtmlHelper htmlHelper, string name, string content)
        {
            // Add an element for each name-content pair.

            var sb = new StringBuilder();
            sb.AppendLine(MetaTag(name, content));
            return MvcHtmlString.Create(sb.ToString());
        }

        public static string Favicon<THtmlHelper>(this THtmlHelper htmlHelper, FaviconReference reference)
        {
            if (reference == null)
                return string.Empty;

            var builder = new TagBuilder("link");
            builder.MergeAttribute("rel", "shortcut icon");
            builder.MergeAttribute("type", "image/x-icon");
            builder.MergeAttribute("href", reference.Url.ToString());
            return builder.ToString(TagRenderMode.StartTag);
        }

        public static string CanonicalLink<THtmlHelper>(this THtmlHelper htmlHelper, ReadOnlyUrl url)
        {
            if (url == null)
                return string.Empty;

            var builder = new TagBuilder("link");
            builder.MergeAttribute("rel", "canonical");
            builder.MergeAttribute("href", url.AbsolutePath);
            return builder.ToString(TagRenderMode.StartTag);
        }

        public static string RssFeeds<THtmlHelper>(this THtmlHelper htmlHelper, IEnumerable<RssFeedReference> references)
        {
            var sb = new StringBuilder();
            if (references != null)
            {
                foreach (var reference in references)
                    sb.AppendLine(RssFeed(reference));
            }
            return sb.ToString();
        }

        public static string StyleSheets<THtmlHelper>(this THtmlHelper htmlHelper, IEnumerable<StyleSheetReference> references)
        {
            var sb = new StringBuilder();
            if (references != null)
            {
                foreach (var reference in references)
                    sb.AppendLine(StyleSheet(reference));
            }
            return sb.ToString();
        }

        public static string JavaScripts<THtmlHelper>(this THtmlHelper htmlHelper, IEnumerable<JavaScriptReference> references)
        {
            var sb = new StringBuilder();
            if (references != null)
            {
                foreach (var reference in references)
                    sb.AppendLine(JavaScript(reference));
            }
            return sb.ToString();
        }

        private static string JavaScript(JavaScriptReference reference)
        {
            var builder = new TagBuilder("script");
            builder.MergeAttribute("type", "text/javascript");
            builder.MergeAttribute("src", reference.Url.ToString());
            return builder.ToString();
        }


        private static string StyleSheet(StyleSheetReference reference)
        {
            var builder = new TagBuilder("link");
            builder.MergeAttribute("href", reference.Url.ToString());
            builder.MergeAttribute("rel", "stylesheet");
            if (!string.IsNullOrEmpty(reference.Media))
                builder.MergeAttribute("media", reference.Media);
            return builder.ToString(TagRenderMode.StartTag);
        }

        private static string RssFeed(Reference reference)
        {
            var builder = new TagBuilder("link");
            builder.MergeAttribute("rel", "alternate");
            builder.MergeAttribute("type", "application/rss+xml");
            builder.MergeAttribute("title", "RSS 2.0");
            builder.MergeAttribute("href", reference.Url.ToString());
            return builder.ToString(TagRenderMode.StartTag);
        }

        private static string MetaTag(MetaTag metaTag)
        {
            return MetaTag(metaTag.Name, metaTag.Content);
        }

        private static string MetaTag(string name, string content)
        {
            var builder = new TagBuilder("meta");
            builder.MergeAttribute("name", name);
            builder.MergeAttribute("content", content);
            return builder.ToString(TagRenderMode.StartTag);
        }
    }
}

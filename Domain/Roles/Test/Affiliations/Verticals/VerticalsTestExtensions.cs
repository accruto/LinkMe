using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Domain.Roles.Test.Affiliations.Verticals
{
    public static class VerticalsTestExtensions
    {
        public static ReadOnlyUrl GetVerticalUrl(this Vertical vertical, string path)
        {
            return vertical.Host != null ? GetVerticalHostUrl(vertical, path) : GetVerticalPathUrl(vertical, path);
        }

        public static ReadOnlyUrl GetVerticalUrl(this Vertical vertical, bool secure, string path)
        {
            return vertical.Host != null ? GetVerticalHostUrl(vertical, secure, path) : GetVerticalPathUrl(vertical, secure, path);
        }

        public static ReadOnlyUrl GetVerticalPathUrl(this Vertical vertical, string path)
        {
            return new ReadOnlyApplicationUrl("~/" + vertical.Url + "/" + path);
        }

        public static ReadOnlyUrl GetVerticalPathUrl(this Vertical vertical, bool secure, string path)
        {
            return new ReadOnlyApplicationUrl(secure, "~/" + vertical.Url + "/" + path);
        }

        public static ReadOnlyUrl GetVerticalHostUrl(this Vertical vertical, string path)
        {
            return vertical.GetVerticalHostUrl(false, path);
        }

        public static ReadOnlyUrl GetVerticalHostUrl(this Vertical vertical, bool secure, string path)
        {
            if (vertical.Host == null)
                return null;

            // Need to make sure the application path is included.

            var nonVerticalUrl = new ReadOnlyApplicationUrl("~/" + GetVerticalHostPath(path));
            return new ReadOnlyApplicationUrl(new Url((secure ? "https://localhost." : "http://localhost.") + vertical.Host), nonVerticalUrl.PathAndQuery);
        }

        public static ReadOnlyUrl GetVerticalSecondaryHostUrl(this Vertical vertical, string path)
        {
            if (vertical.SecondaryHost == null)
                return null;

            // Need to make sure the application path is included.

            var nonVerticalUrl = new ReadOnlyApplicationUrl("~/" + GetVerticalHostPath(path));
            return new ReadOnlyApplicationUrl(new Url("http://localhost." + vertical.SecondaryHost), nonVerticalUrl.PathAndQuery);
        }

        public static ReadOnlyUrl GetVerticalTertiaryHostUrl(this Vertical vertical, string path)
        {
            if (vertical.TertiaryHost == null)
                return null;

            // Need to make sure the application path is included.

            var nonVerticalUrl = new ReadOnlyApplicationUrl("~/" + GetVerticalHostPath(path));
            return new ReadOnlyApplicationUrl(new Url("http://localhost." + vertical.TertiaryHost), nonVerticalUrl.PathAndQuery);
        }

        private static string GetVerticalHostPath(string path)
        {
            if (path == null)
                return string.Empty;
            if (path.EndsWith("join.aspx") || path.EndsWith("login.aspx"))
                return path.Substring(0, path.Length - ".aspx".Length);
            return path;
        }
    }
}
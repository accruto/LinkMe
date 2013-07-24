using System;

namespace LinkMe.Framework.Utility.Urls
{
    public static class UrlExtensions
    {
        public static string ToUrlName(this string name)
        {
            return TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndWhiteSpace(name)).ToLower().Replace(' ', '-');
        }

        public static string AddUrlSegments(this string baseUrl, string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentException("The relative path must be specified.", "relativePath");

            if (string.IsNullOrEmpty(baseUrl))
                return relativePath;

            if (baseUrl[baseUrl.Length - 1] == Url.PathSeparatorChar)
            {
                // Avoid a double / if the first part ends with / and the second part starts with /.

                if (relativePath[0] == Url.PathSeparatorChar)
                    return baseUrl.Substring(0, baseUrl.Length - 1) + relativePath;
            }
            else if (relativePath[0] != Url.PathSeparatorChar)
            {
                return baseUrl + Url.PathSeparatorChar + relativePath; // Add a / in-between the two URLs when needed.
            }

            return baseUrl + relativePath;
        }

        public static string RemoveQueryString(this string url)
        {
            if (url == null)
                return null;
            var pos = url.IndexOf('?');
            return pos == -1 ? url : url.Substring(0, pos);
        }

        public static bool HasQueryString(this string url)
        {
            if (url == null)
                return false;
            return url.IndexOf(Url.QueryStringSeparatorChar) != -1;
        }

        public static bool HasUrlExtension(this string url)
        {
            if (url == null)
                return false;

            var length = url.Length;
            var index = length;
            while (--index >= 0)
            {
                var ch = url[index];
                if (ch == '.')
                    return index != (length - 1);

                if (ch == Url.PathSeparatorChar)
                    break;
            }

            return false;
        }

        public static string AddUrlFragment(this string url, string fragment)
        {
            // Do not use the Url class here, becuase that canonicalizes the URL. For the anchor to cause
            // the browser to scroll without reloading the page the URL must be EXACTLY the same as the request
            // URL, so use some simple string manipulation instead.

            var index = url.LastIndexOf('#');
            if (index > 0)
                url = url.Substring(0, index);

            return (string.IsNullOrEmpty(fragment) ? url : url + "#" + fragment.TrimStart('#'));
        }
    }
}

using System;
using System.Web;

namespace LinkMe.Apps.Asp.Cookies
{
    public static class HttpCookieCollectionExtensions
    {
        private const string DefaultPath = "/";

        public static void SetCookie(this HttpCookieCollection cookies, string name, string value, string domain, TimeSpan expires)
        {
            cookies.SetCookie(name, value, domain, DefaultPath, DateTime.Now.Add(expires));
        }

        public static void SetCookie(this HttpCookieCollection cookies, string name, string value, string domain, DateTime expires)
        {
            cookies.SetCookie(name, value, domain, DefaultPath, expires);
        }

        public static void SetCookie(this HttpCookieCollection cookies, string name, string value, TimeSpan expires)
        {
            cookies.SetCookie(name, value, null, DefaultPath, DateTime.Now.Add(expires));
        }

        public static void SetCookie(this HttpCookieCollection cookies, string name, string value, DateTime expires)
        {
            cookies.SetCookie(name, value, null, DefaultPath, expires);
        }

        public static string GetCookieValue(this HttpCookieCollection cookies, string name)
        {
            var cookie = cookies.Get(name);
            return cookie == null ? null : cookie.Value;
        }

        private static void SetCookie(this HttpCookieCollection cookies, string name, string value, string domain, string path, DateTime expires)
        {
            var cookie = new HttpCookie(name, value)
            {
                Path = path,
                Expires = expires,
                HttpOnly = true,
                Domain = domain,
            };
            cookies.Add(cookie);
        }
    }
}
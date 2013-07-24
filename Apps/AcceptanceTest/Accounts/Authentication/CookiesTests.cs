using System.Net;

namespace LinkMe.AcceptanceTest.Accounts.Authentication
{
    public abstract class CookiesTests
        : WebTestClass
    {
        protected Cookie GetCookie(string name)
        {
            var cookies = Browser.Cookies.GetCookies(Browser.CurrentUrl);
            return cookies[name];
        }
    }
}

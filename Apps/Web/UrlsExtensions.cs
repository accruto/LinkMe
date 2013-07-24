using System;
using System.Collections.Specialized;
using System.Web;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web
{
    public static class UrlsExtensions
    {
        private static ReadOnlyUrl LoggedOutHomeUrl { get { return Areas.Public.Routes.HomeRoutes.Home.GenerateUrl(); } }
        private static ReadOnlyUrl LoggedOutEmployerHomeUrl { get { return Areas.Employers.Routes.HomeRoutes.Home.GenerateUrl(); } }
        private static ReadOnlyUrl LoggedInMemberHomeUrl { get { return Areas.Members.Routes.ProfilesRoutes.Profile.GenerateUrl(); } }
        private static ReadOnlyUrl LoggedInEmployerHomeUrl { get { return SearchRoutes.Search.GenerateUrl(); } }
        private static ReadOnlyUrl LoggedInAdministratorHomeUrl { get { return Areas.Administrators.Routes.HomeRoutes.Home.GenerateUrl(); } }
        private static ReadOnlyUrl LoggedInCustodianHomeUrl { get { return Areas.Custodians.Routes.HomeRoutes.Home.GenerateUrl(); } }

        // GetHomeUrl

        public static ReadOnlyUrl GetHomeUrl(this HttpContextBase context)
        {
            return GetHomeUrl(context.HasRegisteredUserIdentity(), context.User.UserType(), null);
        }

        public static ReadOnlyUrl GetHomeUrl(this HttpContext context)
        {
            return GetHomeUrl(context.HasRegisteredUserIdentity(), context.User.UserType(), null);
        }

        public static bool IsHomeUrl(this HttpContextBase context)
        {
            return IsHomeUrl(context.GetClientUrl());
        }

        // GetReturnUrl

        public static ReadOnlyUrl GetReturnUrl(this HttpContext context)
        {
            return GetReturnUrl(context.Request.QueryString, null, context.HasRegisteredUserIdentity(), context.User.UserType());
        }

        public static ReadOnlyUrl GetReturnUrl(this HttpContextBase context)
        {
            return GetReturnUrl(context.Request.QueryString, null, context.HasRegisteredUserIdentity(), context.User.UserType());
        }

        public static ReadOnlyUrl GetReturnUrl(this HttpContext context, UserType userType)
        {
            return GetReturnUrl(context.Request.QueryString, null, context.HasRegisteredUserIdentity(), userType);
        }

        public static ReadOnlyUrl GetReturnUrl(this HttpContextBase context, UserType userType)
        {
            return GetReturnUrl(context.Request.QueryString, null, context.HasRegisteredUserIdentity(), userType);
        }

        public static ReadOnlyUrl GetReturnUrl(this HttpContext context, ReadOnlyUrl defaultUrl)
        {
            return GetReturnUrl(context.Request.QueryString, defaultUrl, context.HasRegisteredUserIdentity(), context.User.UserType());
        }

        public static ReadOnlyUrl GetReturnUrl(this HttpContextBase context, ReadOnlyUrl defaultUrl)
        {
            return GetReturnUrl(context.Request.QueryString, defaultUrl, context.HasRegisteredUserIdentity(), context.User.UserType());
        }

        public static ReadOnlyUrl GetReturnUrl(this HttpRequestBase request)
        {
            return GetReturnUrl(request.QueryString);
        }

        public static ReadOnlyUrl GetReturnUrl(this HttpRequest request)
        {
            return GetReturnUrl(request.QueryString);
        }

        // GetLoginUrl

        public static ReadOnlyUrl GetLoginUrl(this HttpContextBase context)
        {
            return GetLoginUrl(LoginsRoutes.LogIn, context.GetClientUrl());
        }

        public static ReadOnlyUrl GetLoginUrl(this HttpContext context)
        {
            return GetLoginUrl(LoginsRoutes.LogIn, context.GetClientUrl());
        }

        public static ReadOnlyUrl GetLoginUrl(this HttpContextBase context, string reason)
        {
            return GetLoginUrl(GetLoginUrl(LoginsRoutes.LogIn, context.GetClientUrl()), reason);
        }

        public static ReadOnlyUrl GetLoginUrl(this HttpContext context, RouteReference returnRoute)
        {
            return GetLoginUrl(LoginsRoutes.LogIn, returnRoute.GenerateUrl());
        }

        public static ReadOnlyUrl GetLoginUrl(this HttpContext context, ReadOnlyUrl returnUrl)
        {
            return GetLoginUrl(LoginsRoutes.LogIn, returnUrl);
        }

        public static ReadOnlyUrl GetEmployerLoginUrl(this HttpContext context)
        {
            return GetLoginUrl(AccountsRoutes.LogIn, context.GetClientUrl());
        }

        public static ReadOnlyUrl GetEmployerLoginUrl(this HttpContextBase context)
        {
            return GetLoginUrl(AccountsRoutes.LogIn, context.GetClientUrl());
        }

        public static ReadOnlyUrl GetEmployerLoginUrl(this HttpContextBase context, string reason)
        {
            return GetLoginUrl(GetLoginUrl(AccountsRoutes.LogIn, context.GetClientUrl()), reason);
        }

        public static ReadOnlyUrl GetEmployerLoginUrl(this HttpContext context, RouteReference returnRoute)
        {
            return GetLoginUrl(AccountsRoutes.LogIn, returnRoute.GenerateUrl());
        }

        public static ReadOnlyUrl GetEmployerLoginUrl(this HttpContext context, ReadOnlyUrl returnUrl)
        {
            return GetLoginUrl(AccountsRoutes.LogIn, returnUrl);
        }

        private static bool IsAllowedRedirects(this ReadOnlyUrl url)
        {
            // Check for cases 3032 (open redirects) and 4241 (XSS via links).

            return !HtmlUtil.ContainsScript(url.ToString()) && NavigationManager.IsInternalUrl(url);
        }

        private static ReadOnlyUrl GetReturnUrl(NameValueCollection queryString)
        {
            if (queryString == null)
                throw new ArgumentNullException("queryString");

            var returnUrlParameter = queryString[Apps.Asp.Constants.ReturnUrlParameter];
            if (string.IsNullOrEmpty(returnUrlParameter))
                return null;

            Url returnUrl;

            try
            {
                returnUrl = new ApplicationUrl(returnUrlParameter);
            }
            catch (UriFormatException)
            {
                returnUrl = new ApplicationUrl(HttpUtility.UrlDecode(returnUrlParameter));
            }

            return !IsAllowedRedirects(returnUrl) ? null : returnUrl;
        }

        private static ReadOnlyUrl GetReturnUrl(NameValueCollection queryString, ReadOnlyUrl defaultUrl, bool isLoggedIn, UserType userType)
        {
            // Look for returnUrl parameter in query string.

            var returnUrl = GetReturnUrl(queryString);
            if (returnUrl == null)
            {
                // Use the default if allowed.

                if (defaultUrl != null && IsAllowedRedirects(defaultUrl))
                    returnUrl = defaultUrl;
            }

            // If it is a home url make sure it goes to the right home url.

            if (returnUrl == null || IsHomeUrl(returnUrl))
                return GetHomeUrl(isLoggedIn, userType, returnUrl == null ? null : returnUrl.QueryString);
            
            return returnUrl;
        }

        private static bool IsHomeUrl(ReadOnlyUrl url)
        {
            return string.Compare(url.Path, LoggedOutHomeUrl.Path, StringComparison.InvariantCultureIgnoreCase) == 0
                || string.Compare(url.Path, LoggedOutEmployerHomeUrl.Path, StringComparison.InvariantCultureIgnoreCase) == 0
                || string.Compare(url.Path, LoggedInMemberHomeUrl.Path, StringComparison.InvariantCultureIgnoreCase) == 0
                || string.Compare(url.Path, LoggedInEmployerHomeUrl.Path, StringComparison.InvariantCultureIgnoreCase) == 0
                || string.Compare(url.Path, LoggedInAdministratorHomeUrl.Path, StringComparison.InvariantCultureIgnoreCase) == 0
                || string.Compare(url.Path, LoggedInCustodianHomeUrl.Path, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        private static ReadOnlyUrl GetHomeUrl(bool isLoggedIn, UserType userType, ReadOnlyQueryString queryString)
        {
            if (isLoggedIn)
            {
                switch (userType)
                {
                    case UserType.Member:
                        return GetHomeUrl(LoggedInMemberHomeUrl, queryString);

                    case UserType.Employer:
                        return GetHomeUrl(LoggedInEmployerHomeUrl, queryString);

                    case UserType.Administrator:
                        return GetHomeUrl(LoggedInAdministratorHomeUrl, queryString);

                    case UserType.Custodian:
                        return GetHomeUrl(LoggedInCustodianHomeUrl, queryString);
                }
            }
            else
            {
                switch (userType)
                {
                    case UserType.Employer:
                        return GetHomeUrl(LoggedOutEmployerHomeUrl, queryString);
                }
            }

            return LoggedOutHomeUrl;
        }

        private static ReadOnlyUrl GetHomeUrl(ReadOnlyUrl homeUrl, ReadOnlyQueryString queryString)
        {
            // Transfer any query string if needed.

            if (queryString == null || queryString.Count == 0)
                return homeUrl;

            var url = homeUrl.AsNonReadOnly();
            url.QueryString.Add(queryString);
            return url;
        }

        private static ReadOnlyUrl GetLoginUrl(RouteReference loginRoute, ReadOnlyUrl returnUrl)
        {
            return loginRoute.GenerateUrl(new { returnUrl = returnUrl.PathAndQuery });
        }

        private static ReadOnlyUrl GetLoginUrl(ReadOnlyUrl loginUrl, string reason)
        {
            if (string.IsNullOrEmpty(reason))
                return loginUrl;

            var url = loginUrl.AsNonReadOnly();
            url.QueryString[Apps.Asp.Constants.ReasonParameter] = reason;
            return url;
        }
    }
}
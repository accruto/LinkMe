using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Asp.Cookies;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Asp.Security
{
    public class CookieManager
        : ICookieManager
    {
        private const char UserDataSeparator = '\n';
        private const string AnonymousCookieName = "LinkMeAnon";
        private const string ExternalCookieName = "LinkMeAuthExt";
        private const string AuthenticationTimeoutMinutes = "AuthenticationTimeoutMinutes";
        private const int DefaultAuthenticationTimeoutMinutes = 1440;
        private const int DefaultAnonymousTimeoutMinutes = 100000;
        private const string UserCookieName = "user_cookie";
        private const string PasswordCookieName = "passwordCookie";

        private static readonly Random Random = new Random();
        private static readonly EventSource EventSource = new EventSource<CookieManager>();

        private readonly IDictionary<string, string> _wildcardDomains;
        private readonly int _authenticationTimeout;
        private readonly int _anonymousTimeout;

        public CookieManager(string[] wildcardDomains)
        {
            _wildcardDomains = (wildcardDomains ?? new string[0]).ToDictionary(d => d, d => "." + d);

            _authenticationTimeout = DefaultAuthenticationTimeoutMinutes;
            var setting = ConfigurationManager.AppSettings[AuthenticationTimeoutMinutes];
            if (!string.IsNullOrEmpty(setting))
                _authenticationTimeout = int.Parse(setting);

            _anonymousTimeout = DefaultAnonymousTimeoutMinutes;
            var configuration = WebConfigurationManager.OpenWebConfiguration(null);
            var systemWeb = (SystemWebSectionGroup)configuration.GetSectionGroup("system.web");
            if (systemWeb != null && systemWeb.AnonymousIdentification != null)
                _anonymousTimeout = (int) systemWeb.AnonymousIdentification.CookieTimeout.TotalMinutes;
        }

        void ICookieManager.CreateAuthenticationCookie(HttpContextBase context, IRegisteredUser user)
        {
            var issueDate = DateTime.Now;
            CreateAuthenticationCookie(context, user, false, issueDate, issueDate.AddMinutes(_authenticationTimeout));
        }

        void ICookieManager.UpdateAuthenticationCookie(HttpContextBase context, IRegisteredUser user, bool needsReset)
        {
            // Update the authentication ticket without extending its expiration time.

            var ticket = GetAuthenticationTicket(context);
            CreateAuthenticationCookie(context, user, needsReset, ticket.IssueDate, ticket.Expiration);
        }

        void ICookieManager.DeleteAuthenticationCookie(HttpContextBase context)
        {
            DeleteAuthenticationCookie(context);
        }

        AuthenticationUserData ICookieManager.ParseAuthenticationCookie(HttpContextBase context)
        {
            const string method = "ParseAuthenticationCookies";

            // Extract the forms authentication ticket.

            var ticket = GetAuthenticationTicket(context);
            if (ticket == null)
                return null;

            if (ticket.Expired)
            {
                EventSource.Raise(Event.Trace, method, "Authentication ticket has expired.", Event.Arg("Name", ticket.Name), Event.Arg("IssueDate", ticket.IssueDate), Event.Arg("Expiration", ticket.Expiration), Event.Arg("UserData", ticket.UserData));
                return null;
            }

            EventSource.Raise(Event.Trace, method, "Authentication ticket found.", Event.Arg("Name", ticket.Name));

            // Pull apart the user data and validate it all.

            return ParseAuthenticationUserData(new Guid(ticket.Name), ticket.UserData);
        }

        AnonymousUserData ICookieManager.ParseAnonymousCookie(HttpContextBase context)
        {
            var userId = GetAnonymousUserId(context);
            if (userId == null)
                return null;

            var ticket = GetAnonymousTicket(context);
            return ticket == null
                ? new AnonymousUserData { UserId = userId.Value, PreferredUserType = UserType.Anonymous }
                : ParseAnonymousUserData(userId.Value, ticket.UserData);
        }

        void ICookieManager.UpdateAnonymousCookie(HttpContextBase context, IAnonymousUser user, UserType preferredUserType)
        {
            CreateAnonymousCookie(context, user, preferredUserType);
        }

        ExternalUserData ICookieManager.ParseExternalCookie(HttpContextBase context)
        {
            var cookie = context.Request.Cookies[ExternalCookieName];
            return cookie == null ? null : ExternalCookieData.ParseCookieValue(cookie.Value);
        }

        void ICookieManager.DeleteExternalCookie(HttpContextBase context, string domain)
        {
            ExpireCookie(context, domain, ExternalCookieName);
        }

        void ICookieManager.CreatePersistantUserCookie(HttpContextBase context, UserType userType, LoginCredentials credentials, AuthenticationStatus status)
        {
            const int hoursInWeek = 24 * 7;

            // Set the user cookie.

            var domain = GetDomain(context.Request.Url.Host);
            context.Response.Cookies.SetCookie(UserCookieName, credentials.LoginId, domain, new TimeSpan(hoursInWeek, 0, 0));

            // Set the password cookie.

            var persistPassword = !(status == AuthenticationStatus.AuthenticatedWithOverridePassword || userType == UserType.Administrator);
            if (!persistPassword)
            {
                ExpireCookie(context, domain, PasswordCookieName);
                if (domain != null)
                    ExpireCookie(context, null, PasswordCookieName);
            }
            else if (credentials.Password.Length < 6 || credentials.Password.Substring(0, 5) != "sha1|")
            {
                // Compute the SHA1 sum of the hashed password, prefixed by a random salt and the expiry.

                var salt = ToBytes(Random.Next());
                var utf8PasswordHash = Encoding.UTF8.GetBytes(LoginCredentials.HashToString(credentials.Password));

                var expiry = DateTime.Now.ToUniversalTime().AddDays(7);
                var binaryExpiry = ToBytes(expiry.ToBinary());

                var sha1 = SHA1.Create();
                sha1.TransformBlock(salt, 0, salt.Length, salt, 0);
                sha1.TransformBlock(utf8PasswordHash, 0, utf8PasswordHash.Length, utf8PasswordHash, 0);
                sha1.TransformFinalBlock(binaryExpiry, 0, binaryExpiry.Length);

                context.Response.Cookies.SetCookie(PasswordCookieName, string.Format("sha1|{0}|{1:x}|{2}", Convert.ToBase64String(salt), expiry.ToBinary(), Convert.ToBase64String(sha1.Hash)), domain, expiry);
            }
        }

        void ICookieManager.DeletePersistantUserCookie(HttpContextBase context)
        {
            var domain = GetDomain(context.Request.Url.Host);
            ExpireCookie(context, domain, UserCookieName);
            ExpireCookie(context, domain, PasswordCookieName);

            if (domain != null)
            {
                ExpireCookie(context, null, UserCookieName);
                ExpireCookie(context, null, PasswordCookieName);
            }
        }

        LoginCredentials ICookieManager.ParsePersistantUserCookie(HttpContextBase context)
        {
            var loginId = context.Request.Cookies.GetCookieValue(UserCookieName);
            var password = context.Request.Cookies.GetCookieValue(PasswordCookieName);
            return new LoginCredentials { LoginId = loginId, Password = password };
        }

        private static Guid? GetAnonymousUserId(HttpContextBase context)
        {
            var anonymousId = context.Request.AnonymousID;
            if (string.IsNullOrEmpty(context.Request.AnonymousID))
                return null;

            try
            {
                return new Guid(anonymousId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static FormsAuthenticationTicket GetAnonymousTicket(HttpContextBase context)
        {
            // Using a forms authentication ticket so the information can be encrypted etc but will use a different cookie.

            var cookie = context.Request.Cookies[AnonymousCookieName];
            if (cookie == null || cookie.Value == null)
                return null;

            try
            {
                return FormsAuthentication.Decrypt(cookie.Value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string CreateAnonymousUserData(UserType preferredUserType)
        {
            return ((int)preferredUserType).ToString();
        }

        private static AnonymousUserData ParseAnonymousUserData(Guid userId, string userData)
        {
            var parts = userData.Split(UserDataSeparator);
            if (parts.Length == 1)
            {
                return new AnonymousUserData
                {
                    UserId = userId,
                    PreferredUserType = (UserType)int.Parse(parts[0]),
                };
            }

            return new AnonymousUserData
            {
                UserId = userId,
                PreferredUserType = UserType.Anonymous,
            };
        }

        private void CreateAuthenticationCookie(HttpContextBase context, IRegisteredUser user, bool needsReset, DateTime issueDate, DateTime expiration)
        {
            var domain = GetDomain(context.Request.Url.Host);

            var ticket = new FormsAuthenticationTicket(
                1,
                user.Id.ToString("n"),
                issueDate,
                expiration,
                false,
                CreateAuthenticationUserData(user, needsReset),
                FormsAuthentication.FormsCookiePath);

            // Create the cookie.

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                HttpOnly = true,
                Secure = false,
                Path = FormsAuthentication.FormsCookiePath,
                Domain = domain,
            };

            // Expire any existing cookies before adding the new one.

            if (domain != null)
                ExpireCookie(context, null, FormsAuthentication.FormsCookieName);
            context.Response.Cookies.Add(cookie);
        }

        private void DeleteAuthenticationCookie(HttpContextBase context)
        {
            FormsAuthentication.SignOut();

            // Delete the cookies because the SignOut does not take into account domains etc.

            var domain = GetDomain(context.Request.Url.Host);
            ExpireCookie(context, domain, FormsAuthentication.FormsCookieName);
            if (domain != null)
                ExpireCookie(context, null, FormsAuthentication.FormsCookieName);
        }

        private string GetDomain(string host)
        {
            foreach (var wildcardDomain in _wildcardDomains)
            {
                if (host == wildcardDomain.Key || host.EndsWith(wildcardDomain.Value))
                    return wildcardDomain.Value;
            }

            return null;
        }

        private FormsAuthenticationTicket GetAuthenticationTicket(HttpContextBase context)
        {
            const string method = "GetAuthenticationTicket";

            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null && cookie.Value != null)
            {
                try
                {
                    return FormsAuthentication.Decrypt(cookie.Value);
                }
                catch (Exception e)
                {
                    EventSource.Raise(Event.Error, method, "Unable to decrypt the authentication ticket.", e, null, Event.Arg("Value", cookie.Value));
                    DeleteAuthenticationCookie(context);
                    throw;
                }
            }

            return null;
        }

        private void CreateAnonymousCookie(HttpContextBase context, IHasId<Guid> user, UserType preferredUserType)
        {
            var expires = new TimeSpan(0, 0, _anonymousTimeout, 0);

            var ticket = new FormsAuthenticationTicket(
                1,
                user.Id.ToString("n"),
                DateTime.Now,
                DateTime.Now.Add(expires),
                true,
                CreateAnonymousUserData(preferredUserType),
                FormsAuthentication.FormsCookiePath);

            // Expire any existing cookies before adding the new one.

            context.Response.Cookies.Remove(AnonymousCookieName);
            context.Response.Cookies.SetCookie(AnonymousCookieName, FormsAuthentication.Encrypt(ticket), GetDomain(context.Request.Url.Host), expires);
        }

        private static string CreateAuthenticationUserData(IRegisteredUser user, bool needsReset)
        {
            return ((int)user.UserType).ToString()
                + UserDataSeparator + user.FullName
                + UserDataSeparator + needsReset
                + UserDataSeparator + user.IsActivated;
        }

        private static AuthenticationUserData ParseAuthenticationUserData(Guid userId, string userData)
        {
            // parts[4] was EmailVerified but that was always the same as IsActivated so ignore now.
            // parts[5] was checkKey which was used with the double check cookie but no more.

            var parts = userData.Split(UserDataSeparator);
            if (parts.Length == 4 || parts.Length == 5 || parts.Length == 6)
            {
                return new AuthenticationUserData
                {
                    UserId = userId,
                    UserType = (UserType)int.Parse(parts[0]),
                    FullName = parts[1],
                    NeedsReset = bool.Parse(parts[2]),
                    IsActivated = bool.Parse(parts[3]),
                };
            }

            return new AuthenticationUserData
            {
                UserId = userId,
                UserType = UserType.Anonymous,
                FullName = null,
                NeedsReset = false,
                IsActivated = false,
            };
        }

        private static byte[] ToBytes(int n)
        {
            var bytes = new byte[4];
            for (var i = 0; i < 4; ++i)
                bytes[i] = (byte)(n >> (8 * i) & 255);
            return bytes;
        }

        private static byte[] ToBytes(long n)
        {
            var bytes = new byte[8];
            for (var i = 0; i < 8; ++i)
                bytes[i] = (byte)(n >> (8 * i) & 255);
            return bytes;
        }

        private static void ExpireCookie(HttpContextBase context, string domain, string name)
        {
            var cookie = new HttpCookie(name) { Domain = domain, Expires = DateTime.Now.AddYears(-1) };
            context.Response.Cookies.Add(cookie);
        }
    }
}

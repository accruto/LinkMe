using System.Web;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;

namespace LinkMe.Apps.Asp.Security
{
    public class DevAuthenticationManager
        : IDevAuthenticationManager
    {
        private const string SessionKey = "IsDevUser";
        private readonly bool _isAlwaysLoggedIn;
        private readonly string _passwordHash;

        public DevAuthenticationManager(bool isAlwaysLoggedIn, string passwordHash)
        {
            _isAlwaysLoggedIn = isAlwaysLoggedIn;
            _passwordHash = passwordHash;
        }

        AuthenticationStatus IDevAuthenticationManager.AuthenticateUser(string password)
        {
            // For test case purposes also check directly against the hash.

            return LoginCredentials.HashToString(password) == _passwordHash || password == _passwordHash
                ? AuthenticationStatus.Authenticated
                : AuthenticationStatus.Failed;
        }

        void IDevAuthenticationManager.LogIn(HttpContextBase context)
        {
            LogIn(context);
        }

        void IDevAuthenticationManager.LogIn(HttpContext context)
        {
            LogIn(new HttpContextWrapper(context));
        }

        bool IDevAuthenticationManager.IsLoggedIn(HttpContextBase context)
        {
            return IsLoggedIn(context);
        }

        bool IDevAuthenticationManager.IsLoggedIn(HttpContext context)
        {
            return IsLoggedIn(new HttpContextWrapper(context));
        }

        private static void LogIn(HttpContextBase context)
        {
            if (context.Session != null)
                context.Session[SessionKey] = true;
        }

        private bool IsLoggedIn(HttpContextBase context)
        {
            if (_isAlwaysLoggedIn)
                return true;

            // Check the session.

            if (context.Session == null)
                return false;
            var value = context.Session[SessionKey];
            if (value == null)
                return false;
            if (value is bool)
                return (bool)value;
            return false;
        }
    }
}

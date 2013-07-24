using System.Web;
using LinkMe.Apps.Agents.Security.Commands;

namespace LinkMe.Apps.Asp.Security
{
    public interface IDevAuthenticationManager
    {
        AuthenticationStatus AuthenticateUser(string password);

        void LogIn(HttpContextBase context);
        void LogIn(HttpContext context);

        bool IsLoggedIn(HttpContextBase context);
        bool IsLoggedIn(HttpContext context);
    }
}

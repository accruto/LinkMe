using System.Web;
using System.Web.SessionState;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Asp.Security
{
    public interface IAuthenticationManager
    {
        void AuthenticateRequest(HttpContextBase context);
        void PostAuthenticateRequest(HttpContextBase context);

        void LogIn(HttpContextBase context, IRegisteredUser user, AuthenticationStatus status);
        void LogOut(HttpContextBase context);

        void EndSession(HttpSessionState session);

        void UpdateUser(HttpContextBase context, IRegisteredUser user, bool needsReset);
        RegisteredUser GetUser(HttpContextBase context);

        void UpdateUser(HttpContextBase context, IAnonymousUser user, UserType preferredUserType);
    }
}

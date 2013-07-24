using System.Web;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration.LinkedIn;

namespace LinkMe.Apps.Asp.Security
{
    public interface IAccountsManager
    {
        AuthenticationResult TryAutoLogIn(HttpContextBase context);
        AuthenticationResult LogIn(HttpContextBase context, Login login);
        void LogOut(HttpContextBase context);

        Member Join(HttpContextBase context, MemberAccount account, AccountLoginCredentials credentials, bool requiresActivation);
        Employer Join(HttpContextBase context, EmployerAccount account, AccountLoginCredentials credentials);
        Employer Join(HttpContextBase context, EmployerAccount account, LinkedInProfile profile);
    }
}

using System.Web;
using LinkMe.Domain.Roles.Integration;

namespace LinkMe.Apps.Services.Security
{
    public interface IServiceAuthenticationManager
    {
        IntegratorUser AuthenticateRequest(HttpContext context, IntegratorPermissions permissions);
        IntegratorUser AuthenticateRequest(HttpContextBase context, IntegratorPermissions permissions);
        IntegratorUser AuthenticateRequest(string userName, string password, IntegratorPermissions permissions);
    }
}

using System.Web;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Apps.Services.Security
{
    public class ServiceAuthenticationManager
        : IServiceAuthenticationManager
    {
        private readonly IIntegrationQuery _integrationQuery;

        private const string UserNameHeader = "X-LinkMeUsername";
        private const string UserNameQueryString = "LinkMeUsername";
        private const string PasswordHeader = "X-LinkMePassword";
        private const string PasswordQueryString = "LinkMePassword";
        private const string NoUserNameError = "No username was specified in the HTTP request.";
        private const string NoPasswordError = "No password was specified in the HTTP request.";
        private const string UnknownUserError = "Web service authorization failed: unknown user '{0}'.";
        private const string IncorrectPasswordError = "Web service authorization failed: the password for user '{0}' is incorrect.";
        private const string PermissionDeniedError = "Web service authorization failed: user '{0}' does not have permission to access the requested service.";

        public ServiceAuthenticationManager(IIntegrationQuery integrationQuery)
        {
            _integrationQuery = integrationQuery;
        }

        IntegratorUser IServiceAuthenticationManager.AuthenticateRequest(HttpContext context, IntegratorPermissions permissions)
        {
            var request = new HttpRequestWrapper(context.Request);
            return AuthenticateRequest(GetUserName(request), GetPassword(request), permissions);
        }

        IntegratorUser IServiceAuthenticationManager.AuthenticateRequest(HttpContextBase context, IntegratorPermissions permissions)
        {
            return AuthenticateRequest(GetUserName(context.Request), GetPassword(context.Request), permissions);
        }

        IntegratorUser IServiceAuthenticationManager.AuthenticateRequest(string userName, string password, IntegratorPermissions permissions)
        {
            return AuthenticateRequest(userName, password, permissions);
        }

        private static string GetUserName(HttpRequestBase request)
        {
            var userName = request.Headers[UserNameHeader];
            return string.IsNullOrEmpty(userName) ? request.QueryString[UserNameQueryString] : userName;
        }

        private static string GetPassword(HttpRequestBase request)
        {
            var password = request.Headers[PasswordHeader];
            return string.IsNullOrEmpty(password) ? request.QueryString[PasswordQueryString] : password;
        }

        private IntegratorUser AuthenticateRequest(string userName, string password, IntegratorPermissions permissions)
        {
            if (string.IsNullOrEmpty(userName))
                throw new UserException(NoUserNameError);

            if (string.IsNullOrEmpty(password))
                throw new UserException(NoPasswordError);

            var user = _integrationQuery.GetIntegratorUser(userName);
            if (user == null)
                throw new UserException(string.Format(UnknownUserError, userName));

            var passwordHash = LoginCredentials.HashToString(password);
            if (passwordHash != user.PasswordHash)
                throw new UserException(string.Format(IncorrectPasswordError, userName));

            if (!user.Permissions.IsFlagSet(permissions))
                throw new UserException(string.Format(PermissionDeniedError, user.LoginId));

            return user;
        }
    }
}

using System;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Queries;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Agents.Security.Commands
{
    public class ExternalAuthenticationCommand
        : IExternalAuthenticationCommand
    {
        private static readonly EventSource EventSource = new EventSource<ExternalAuthenticationCommand>();
        private readonly IExternalCredentialsQuery _externalCredentialsQuery;
        private readonly IUsersQuery _usersQuery;

        public ExternalAuthenticationCommand(IExternalCredentialsQuery externalCredentialsQuery, IUsersQuery usersQuery)
        {
            _externalCredentialsQuery = externalCredentialsQuery;
            _usersQuery = usersQuery;
        }

        AuthenticationResult IExternalAuthenticationCommand.AuthenticateUser(ExternalCredentials credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");

            // Get the user.

            var user = GetUser(credentials.ProviderId, credentials.ExternalId);
            if (user == null)
                return CreateFailedResult(null);

            // Get the stored credentials and authenticate.

            var storedCredentials = _externalCredentialsQuery.GetCredentials(user.Id, credentials.ProviderId);
            return storedCredentials == null
                ? CreateFailedResult(user)
                : AuthenticateUser(user, storedCredentials, credentials);
        }

        private IRegisteredUser GetUser(Guid providerId, string externalId)
        {
            if (externalId == null)
                throw new ArgumentNullException("externalId");
            var userId = _externalCredentialsQuery.GetUserId(providerId, externalId);
            return userId != null ? _usersQuery.GetUser(userId.Value) : null;
        }

        private static AuthenticationResult AuthenticateUser(IRegisteredUser user, ExternalCredentials storedCredentials, ExternalCredentials credentials)
        {
            const string method = "AuthenticateUser";

            // If the password hash has already been determined then use that.

            if (storedCredentials.ProviderId == credentials.ProviderId && storedCredentials.ExternalId == credentials.ExternalId)
                return CreateResult(user, GetAuthenticationStatus(user, storedCredentials));

            EventSource.Raise(Event.Trace, method, string.Format("External authentication failed for user {0} ({1}).", storedCredentials.ExternalId, user.Id));
            return CreateFailedResult(user);
        }

        private static AuthenticationStatus GetAuthenticationStatus(IUserAccount account, ExternalCredentials credentials)
        {
            const string method = "AuthenticateUserFlags";

            AuthenticationStatus status;
            if (!account.IsEnabled)
                status = AuthenticationStatus.Disabled;
            else if (!account.IsActivated)
                status = AuthenticationStatus.Deactivated;
            else
                status = AuthenticationStatus.Authenticated;

            EventSource.Raise(Event.Trace, method, "User verification for externalId '" + credentials.ExternalId + "' = " + status);
            return status;
        }

        private static AuthenticationResult CreateResult(IRegisteredUser user, AuthenticationStatus status)
        {
            return new AuthenticationResult
            {
                User = user,
                Status = status,
            };
        }

        private static AuthenticationResult CreateFailedResult(IRegisteredUser user)
        {
            return new AuthenticationResult
            {
                User = user,
                Status = AuthenticationStatus.Failed,
            };
        }
    }
}
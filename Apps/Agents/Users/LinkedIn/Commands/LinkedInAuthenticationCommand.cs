using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration.LinkedIn.Queries;
using LinkMe.Domain.Users.Queries;

namespace LinkMe.Apps.Agents.Users.LinkedIn.Commands
{
    public class LinkedInAuthenticationCommand
        : ILinkedInAuthenticationCommand
    {
        private readonly ILinkedInQuery _linkedInQuery;
        private readonly IUsersQuery _usersQuery;

        public LinkedInAuthenticationCommand(ILinkedInQuery linkedInQuery, IUsersQuery usersQuery)
        {
            _linkedInQuery = linkedInQuery;
            _usersQuery = usersQuery;
        }

        AuthenticationResult ILinkedInAuthenticationCommand.AuthenticateUser(string linkedInId)
        {
            var profile = _linkedInQuery.GetProfile(linkedInId);
            if (profile == null)
                return new AuthenticationResult { Status = AuthenticationStatus.Failed };

            var user = _usersQuery.GetUser(profile.UserId);
            if (user == null)
                return new AuthenticationResult { Status = AuthenticationStatus.Failed };

            // Only support employers for now.

            if (user.UserType != UserType.Employer)
                return new AuthenticationResult { Status = AuthenticationStatus.Failed };

            return new AuthenticationResult
            {
                User = user,
                Status = GetAuthenticationStatus(user),
            };
        }

        private static AuthenticationStatus GetAuthenticationStatus(IUserAccount account)
        {
            if (!account.IsEnabled)
                return AuthenticationStatus.Disabled;
            if (!account.IsActivated)
                return AuthenticationStatus.Deactivated;
            return AuthenticationStatus.Authenticated;
        }
    }
}

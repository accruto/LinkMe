using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Agents.Security.Commands
{
    public class LoginAuthenticationCommand
        : ILoginAuthenticationCommand
    {
        private static readonly EventSource EventSource = new EventSource<LoginAuthenticationCommand>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IUsersQuery _usersQuery;
        private readonly bool _overridePasswordEnabled;
        private readonly string _overridePasswordHash;
        private readonly bool _obfuscateEmailAddresses;

        public LoginAuthenticationCommand(ILoginCredentialsQuery loginCredentialsQuery, IUsersQuery usersQuery, bool overridePasswordEnabled, string overridePasswordHash, bool obfuscateEmailAddresses)
        {
            _loginCredentialsQuery = loginCredentialsQuery;
            _usersQuery = usersQuery;
            _overridePasswordEnabled = overridePasswordEnabled;
            _overridePasswordHash = overridePasswordHash;
            _obfuscateEmailAddresses = obfuscateEmailAddresses;
        }

        AuthenticationResult ILoginAuthenticationCommand.AuthenticateUser(LoginCredentials credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");
            credentials.LoginId = CleanLoginId(credentials.LoginId);

            // Get the user.

            var user = GetUser(credentials.LoginId);
            if (user == null)
                return CreateFailedResult(null);

            // Get the stored and authenticate.

            var storedCredentials = _loginCredentialsQuery.GetCredentials(user.Id);
            return storedCredentials == null
                ? CreateFailedResult(user)
                : AuthenticateUser(user, storedCredentials, credentials);
        }

        private static string CleanLoginId(string loginId)
        {
            // Copy/paste sometimes leaves trailing spaces etc so remove everything before and after.

            return loginId.Trim();
        }

        private IRegisteredUser GetUser(string loginId)
        {
            if (loginId == null)
                throw new ArgumentNullException("loginId");

            loginId = CleanLoginId(loginId);
            if (loginId.Length == 0)
                throw new ArgumentException("The loginId is an empty string.", "loginId");

            var userId = _loginCredentialsQuery.GetUserId(loginId);
            if (userId != null)
                return _usersQuery.GetUser(userId.Value);

            // If the user can't be found with the loginId try parsing it as their id.

            userId = ParseUtil.TryParseGuid(loginId);
            if (userId != null)
            {
                var user = _usersQuery.GetUser(userId.Value);
                if (user != null)
                    return user;
            }

            // In those environments where the loginId can be obfuscated use that version,
            // eg someone+gmail.com@test.linkme.net.au instead of someone@gmail.com.

            if (_obfuscateEmailAddresses && loginId.IndexOf('@') != -1)
            {
                userId = _loginCredentialsQuery.GetUserId(MiscUtils.ObfuscateEmailAddress(loginId));
                if (userId != null)
                    return _usersQuery.GetUser(userId.Value);
            }

            return null;
        }

        private AuthenticationResult AuthenticateUser(IRegisteredUser user, LoginCredentials storedCredentials, LoginCredentials credentials)
        {
            const string method = "AuthenticateUser";

            // If the password hash has already been determined then use that.

            string passwordHash;
            if (!string.IsNullOrEmpty(credentials.PasswordHash))
            {
                passwordHash = credentials.PasswordHash;
            }
            else
            {
                // Hash the password and check.

                if (credentials.Password.Length >= 5 && credentials.Password.Substring(0, 5) == "sha1|")
                {
                    return Sha1PasswordMatches(storedCredentials, credentials.Password)
                        ? CreateResult(user, GetAuthenticationStatus(user, storedCredentials))
                        : CreateFailedResult(user);
                }

                passwordHash = LoginCredentials.HashToString(credentials.Password);
            }

            var result = AuthenticateUser(user, storedCredentials, passwordHash);
            if (result.Status != AuthenticationStatus.Failed)
                return result;

            // A better fix for 4246: if the original password doesn't work try trimming spaces from the end.

            if (!string.IsNullOrEmpty(credentials.Password))
            {
                var trimmed = credentials.Password.TrimEnd(' ');
                if (trimmed != credentials.Password)
                {
                    result = AuthenticateUser(user, storedCredentials, LoginCredentials.HashToString(trimmed));
                    if (result.Status != AuthenticationStatus.Failed)
                        return result;
                }
            }

            // Check to see whether the use of the override password is enabled.

            if (!_overridePasswordEnabled)
            {
                EventSource.Raise(Event.Trace, method, string.Format("Login failed for user {0} ({1}) (override password disabled).", storedCredentials.LoginId, user.Id));
                return result;
            }

            // Check against the override password.

            if (_overridePasswordHash != passwordHash)
            {
                EventSource.Raise(Event.Trace, method, string.Format("Login failed for user {0} ({1}).", storedCredentials.LoginId, user.Id));
                return result;
            }

            // An override login does not check the user flags.

            EventSource.Raise(Event.Trace, method, string.Format("User {0} ({1}) has logged in using override password!", storedCredentials.LoginId, user.Id));
            return new AuthenticationResult { Status = AuthenticationStatus.AuthenticatedWithOverridePassword, User = user };
        }

        private static AuthenticationResult AuthenticateUser(IRegisteredUser user, LoginCredentials credentials, string passwordHash)
        {
            return credentials.PasswordHash == passwordHash
                ? CreateResult(user, GetAuthenticationStatus(user, credentials))
                : CreateFailedResult(user);
        }

        private static AuthenticationStatus GetAuthenticationStatus(IUserAccount account, LoginCredentials userCredentials)
        {
            const string method = "AuthenticateUserFlags";

            AuthenticationStatus status;
            if (!account.IsEnabled)
                status = AuthenticationStatus.Disabled;
            else if (userCredentials.MustChangePassword)
                status = AuthenticationStatus.AuthenticatedMustChangePassword;
            else if (!account.IsActivated)
                status = AuthenticationStatus.Deactivated;
            else
                status = AuthenticationStatus.Authenticated;

            EventSource.Raise(Event.Trace, method, "User verification for loginId '" + userCredentials.LoginId + "' = " + status);
            return status;
        }

        private static byte[] ToBytes(long n)
        {
            var bytes = new byte[8];
            for (int i = 0; i < 8; ++i)
            {
                bytes[i] = (byte)(n >> (8 * i) & 255);
            }
            return bytes;
        }

        private static bool Sha1PasswordMatches(LoginCredentials credentials, string passwordToCheck)
        {
            const string method = "Sha1PasswordMatches";

            try
            {
                var parts = passwordToCheck.Split('|');

                var salt = Convert.FromBase64String(parts[1]);
                var binExpiry = ToBytes(long.Parse(parts[2], NumberStyles.HexNumber));
                var utf8PasswordHash = Encoding.UTF8.GetBytes(credentials.PasswordHash);

                var sha1 = SHA1.Create();
                sha1.TransformBlock(salt, 0, salt.Length, salt, 0);
                sha1.TransformBlock(utf8PasswordHash, 0, utf8PasswordHash.Length, utf8PasswordHash, 0);
                sha1.TransformFinalBlock(binExpiry, 0, binExpiry.Length);
                return (Convert.ToBase64String(sha1.Hash) == parts[3]);
            }
            catch (Exception ex)
            {
                // Fail if there is any problem at all with the password cookie.

                EventSource.Raise(Event.Error, method, "Failed to check SHA1 password coookie.", ex);
                return false;
            }
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
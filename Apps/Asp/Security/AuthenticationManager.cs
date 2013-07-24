using System;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Users.Sessions;
using LinkMe.Apps.Agents.Users.Sessions.Commands;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Queries;

namespace LinkMe.Apps.Asp.Security
{
	public class AuthenticationManager
        : IAuthenticationManager
	{
	    private readonly IUsersQuery _usersQuery;
	    private readonly IUserSessionsCommand _userSessionsCommand;
        private readonly ICookieManager _cookieManager;

	    public AuthenticationManager(IUsersQuery usersQuery, IUserSessionsCommand userSessionsCommand, ICookieManager cookieManager)
        {
            _usersQuery = usersQuery;
            _userSessionsCommand = userSessionsCommand;
            _cookieManager = cookieManager;
        }

        void IAuthenticationManager.AuthenticateRequest(HttpContextBase context)
        {
            // Parse the authentication cookies, and if there are none then let it pass.
            // It is expected that any code dependent on authentication will check for it.

            var authenticationUserData = _cookieManager.ParseAuthenticationCookie(context);
            if (authenticationUserData != null)
                context.User = CreateRegisteredUserPrincipal(authenticationUserData);
        }

        void IAuthenticationManager.PostAuthenticateRequest(HttpContextBase context)
	    {
            // At this point the anonymous processing has now happened so set up the anonymous user. 

            if (!context.Request.IsAuthenticated)
            {
                var anonymousUserData = _cookieManager.ParseAnonymousCookie(context);
                if (anonymousUserData != null)
                    context.User = CreateAnonymousUserPrincipal(anonymousUserData);
            }
	    }

	    void IAuthenticationManager.LogIn(HttpContextBase context, IRegisteredUser user, AuthenticationStatus status)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            // Do the HTTP stuff.

            _cookieManager.CreateAuthenticationCookie(context, user);

            // Do the ASP.NET stuff.

            var identity = new RegisteredUserIdentity(user.Id, user.UserType, user.IsActivated)
            {
                FullName = user.FullName,
                NeedsReset = false,
                User = user
            };

            // Record it.

            _userSessionsCommand.CreateUserLogin(new UserLogin
            {
                UserId = user.Id,
                SessionId = GetSessionId(user.Id, context.Request.UserHostAddress, context.Session),
                IpAddress = context.Request.UserHostAddress,
                AuthenticationStatus = status
            });
            context.User = new RegisteredUserPrincipal(identity);
        }

	    void IAuthenticationManager.LogOut(HttpContextBase context)
        {
            var userId = GetUserId(context.Session);

            LogOut(context);

            // Set the principal to a generic not-logged in.

	        context.User = new GenericPrincipal(new GenericIdentity((userId ?? Guid.NewGuid()).ToString()), new[] { UserType.Anonymous.ToString() });

            // Record it.

            if (userId != null)
                _userSessionsCommand.CreateUserLogout(new UserLogout { UserId = userId.Value, SessionId = context.Session.SessionID, IpAddress = context.Request.UserHostAddress });
        }

        void IAuthenticationManager.EndSession(HttpSessionState session)
	    {
            var userId = GetUserId(session);
            if (userId != null)
            {
                var ipAddress = GetIpAddress(session);
                _userSessionsCommand.CreateUserSessionEnd(new UserSessionEnd {UserId = userId.Value, SessionId = session.SessionID, IpAddress = ipAddress});
            }
	    }

        void IAuthenticationManager.UpdateUser(HttpContextBase context, IRegisteredUser user, bool needsReset)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            // Update the ASP.NET stuff.

            var identity = (RegisteredUserIdentity)context.User.Identity;
            if (identity.Id != user.Id)
                throw new ArgumentException("The user is not the same as the currently authenticated user.");
            identity.FullName = user.FullName;
            identity.NeedsReset = needsReset;
            identity.User = user;

            // Update the cookies.

            try
            {
                _cookieManager.UpdateAuthenticationCookie(context, user, identity.NeedsReset);
            }
            catch (Exception)
            {
                NavigationManager.Redirect(NavigationManager.GetLogOutUrl());
            }
        }

        RegisteredUser IAuthenticationManager.GetUser(HttpContextBase context)
        {
            var identity = context.GetRegisteredUserIdentity();
            if (identity == null)
                return null;

            // Lazy load.

            var user = identity.User as RegisteredUser;
            if (user == null)
            {
                user = _usersQuery.GetUser(identity.Id);
                if (user == null)
                {
                    // Someone is asking for the logged in user but there isn't one.
                    // Just throw them straight out.

                    LogOut(context);
                    return null;
                }

                identity.User = user;
            }

            return user;
        }

        void IAuthenticationManager.UpdateUser(HttpContextBase context, IAnonymousUser user, UserType preferredUserType)
	    {
            if (user == null)
                throw new ArgumentNullException("user");

            // Update the ASP.NET stuff.

            var identity = context.User.Identity as AnonymousUserIdentity;
            if (identity == null)
                return;

            if (identity.Id != user.Id)
                throw new ArgumentException("The user is not the same as the current anonymous user.");

            identity.PreferredUserType = preferredUserType;

            // Update the cookies.

            _cookieManager.UpdateAnonymousCookie(context, user, identity.PreferredUserType);
	    }

	    private static RegisteredUserPrincipal CreateRegisteredUserPrincipal(AuthenticationUserData userData)
        {
            // Set up the user with everything we know.

            var identity = new RegisteredUserIdentity(userData.UserId, userData.UserType, userData.IsActivated)
            {
                FullName = userData.FullName,
                NeedsReset = userData.NeedsReset
            };
            return new RegisteredUserPrincipal(identity);
        }

        private static AnonymousUserPrincipal CreateAnonymousUserPrincipal(AnonymousUserData userData)
        {
            // Set up the user with everything we know.

            var identity = new AnonymousUserIdentity(userData.UserId) { PreferredUserType = userData.PreferredUserType };
            return new AnonymousUserPrincipal(identity);
        }

        private void LogOut(HttpContextBase context)
        {
            _cookieManager.DeleteAuthenticationCookie(context);
        }

	    private static string GetSessionId(Guid userId, string ipAddress, HttpSessionStateBase session)
        {
            if (session == null)
                return null;

            // Make sure something is saved in the session as that fixes the id.

            session[SessionKeys.UserId] = userId;
            session[SessionKeys.IpAddress] = ipAddress;
            return session.SessionID;
        }

        private static Guid? GetUserId(HttpSessionState session)
        {
            var userId = session[SessionKeys.UserId];
            if (userId is Guid)
                return (Guid) userId;
            return null;
        }

        private static Guid? GetUserId(HttpSessionStateBase session)
        {
            var userId = session[SessionKeys.UserId];
            if (userId is Guid)
                return (Guid)userId;
            return null;
        }

        private static string GetIpAddress(HttpSessionState session)
        {
            return session[SessionKeys.IpAddress] as string;
        }
    }
}

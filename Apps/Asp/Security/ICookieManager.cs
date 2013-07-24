using System;
using System.Web;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Asp.Security
{
    public class AuthenticationUserData
    {
        public Guid UserId;
        public UserType UserType;
        public string FullName;
        public bool NeedsReset;
        public bool IsActivated;
    }

    public class AnonymousUserData
    {
        public Guid UserId;
        public UserType PreferredUserType;
    }

    public class ExternalUserData
    {
        public string ExternalId;
        public string EmailAddress;
        public string FirstName;
        public string LastName;
    }

    public interface ICookieManager
    {
        void CreateAuthenticationCookie(HttpContextBase context, IRegisteredUser user);
        void UpdateAuthenticationCookie(HttpContextBase context, IRegisteredUser user, bool needsReset);
        void DeleteAuthenticationCookie(HttpContextBase context);
        AuthenticationUserData ParseAuthenticationCookie(HttpContextBase context);

        AnonymousUserData ParseAnonymousCookie(HttpContextBase context);
        void UpdateAnonymousCookie(HttpContextBase context, IAnonymousUser user, UserType preferredUserType);

        ExternalUserData ParseExternalCookie(HttpContextBase context);
        void DeleteExternalCookie(HttpContextBase context, string domain);

        void CreatePersistantUserCookie(HttpContextBase context, UserType userType, LoginCredentials credentials, AuthenticationStatus status);
        void DeletePersistantUserCookie(HttpContextBase context);
        LoginCredentials ParsePersistantUserCookie(HttpContextBase context);
    }
}

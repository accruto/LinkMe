using System;
using System.Security.Principal;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Security
{
    public static class UserIdentityExtensions
    {
        public static Guid? Id(this IIdentity identity)
        {
            return identity is RegisteredUserIdentity ? ((RegisteredUserIdentity)identity).Id : (Guid?)null;
        }

        public static UserType UserType(this IIdentity identity)
        {
            return identity is RegisteredUserIdentity ? ((RegisteredUserIdentity)identity).UserType : LinkMe.Domain.Contacts.UserType.Anonymous;
        }

        public static string FullName(this IIdentity identity)
        {
            return identity is RegisteredUserIdentity ? ((RegisteredUserIdentity)identity).FullName : null;
        }

        public static bool IsActivated(this IIdentity identity)
        {
            return identity is RegisteredUserIdentity ? ((RegisteredUserIdentity)identity).IsActivated : false;
        }

        public static bool NeedsReset(this IIdentity identity)
        {
            return identity is RegisteredUserIdentity ? ((RegisteredUserIdentity)identity).NeedsReset : false;
        }

        public static UserType PreferredUserType(this IIdentity identity)
        {
            return identity is RegisteredUserIdentity
                ? ((RegisteredUserIdentity) identity).UserType
                : identity is AnonymousUserIdentity
                    ? ((AnonymousUserIdentity) identity).PreferredUserType
                    : LinkMe.Domain.Contacts.UserType.Anonymous;
        }
    }
}
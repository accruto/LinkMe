using System;
using System.Security.Principal;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Security
{
    public static class UserPrincipalExtensions
    {
        public static bool IsAuthenticated(this IPrincipal principal)
        {
            return principal == null ? false : principal.Identity.IsAuthenticated;
        }

        public static Guid? Id(this IPrincipal principal)
        {
            return principal == null ? null : principal.Identity.Id();
        }

        public static UserType UserType(this IPrincipal principal)
        {
            return principal == null ? LinkMe.Domain.Contacts.UserType.Anonymous : principal.Identity.UserType();
        }

        public static string FullName(this IPrincipal principal)
        {
            return principal == null ? null : principal.Identity.FullName();
        }

        public static bool IsActivated(this IPrincipal principal)
        {
            return principal == null ? false : principal.Identity.IsActivated();
        }

        public static bool NeedsReset(this IPrincipal principal)
        {
            return principal == null ? false : principal.Identity.NeedsReset();
        }

        public static UserType PreferredUserType(this IPrincipal principal)
        {
            return principal == null ? LinkMe.Domain.Contacts.UserType.Anonymous : principal.Identity.PreferredUserType();
        }
    }
}
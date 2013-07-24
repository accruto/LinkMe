using System.Web;
using LinkMe.Apps.Agents.Security;

namespace LinkMe.Apps.Asp.Security
{
    public static class RegisteredUserIdentityExtensions
    {
        public static RegisteredUserIdentity GetRegisteredUserIdentity(this HttpContext context)
        {
            return context != null && context.User != null ? context.User.Identity as RegisteredUserIdentity : null;
        }

        public static RegisteredUserIdentity GetRegisteredUserIdentity(this HttpContextBase context)
        {
            return context != null && context.User != null ? context.User.Identity as RegisteredUserIdentity : null;
        }

        public static bool HasRegisteredUserIdentity(this HttpContext context)
        {
            return context.GetRegisteredUserIdentity() != null;
        }

        public static bool HasRegisteredUserIdentity(this HttpContextBase context)
        {
            return context.GetRegisteredUserIdentity() != null;
        }
    }
}

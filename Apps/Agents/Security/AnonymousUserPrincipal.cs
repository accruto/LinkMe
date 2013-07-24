using System.Security.Principal;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Security
{
    public class AnonymousUserPrincipal
        : GenericPrincipal
    {
        public AnonymousUserPrincipal(AnonymousUserIdentity identity)
            : base(identity, new [] {UserType.Anonymous.ToString()})
        {
        }
    }
}
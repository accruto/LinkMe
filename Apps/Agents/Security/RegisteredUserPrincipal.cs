using System.Security.Principal;

namespace LinkMe.Apps.Agents.Security
{
    public class RegisteredUserPrincipal
        : GenericPrincipal
    {
        public RegisteredUserPrincipal(RegisteredUserIdentity identity)
            : base(identity, new [] {identity.UserType.ToString()})
        {
        }
    }
}
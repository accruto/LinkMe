using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Handlers;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Apps.Agents.Subscribers
{
    public class SecuritySubscriber
    {
        private readonly ISecurityHandler _securityHandler;

        public SecuritySubscriber(ISecurityHandler securityHandler)
        {
            _securityHandler = securityHandler;
        }

        [SubscribesTo(PublishedEvents.PasswordReset)]
        public void OnPasswordReset(object sender, PasswordResetEventArgs args)
        {
            _securityHandler.OnPasswordReset(args.IsGenerated, args.UserId, args.LoginId, args.Password);
        }
    }
}
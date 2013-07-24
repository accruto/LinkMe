using System;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Users.Sessions
{
    public abstract class UserSessionActivity
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [IsSet]
        public Guid UserId { get; set; }
        public string SessionId { get; set; }
        [DefaultNow]
        public DateTime Time { get; set; }
        public string IpAddress { get; set; }
    }

    public class UserLogin
        : UserSessionActivity
    {
        public AuthenticationStatus AuthenticationStatus { get; set; }
    }

    public class UserLogout
        : UserSessionActivity
    {
    }

    public class UserSessionEnd
        : UserSessionActivity
    {
    }
}

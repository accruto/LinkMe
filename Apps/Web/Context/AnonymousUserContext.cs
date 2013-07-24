using System;
using System.Web;

namespace LinkMe.Web.Context
{
    public class AnonymousUserContext
        : UserContext
    {
        private static class SessionKeys
        {
            public static readonly string RequestUserId = typeof(AnonymousUserContext).FullName + ".RequestUserId";
            public static readonly string HasLoggedOut = typeof(AnonymousUserContext).FullName + ".HasLoggedOut";
        }

        public AnonymousUserContext(HttpContextBase context)
            : base(context)
        {
        }

        public AnonymousUserContext(HttpContext context)
            : base(context)
        {
        }

        public Guid? RequestUserId
        {
            get { return Session.GetGuid(SessionKeys.RequestUserId); }
            set { Session.Set(SessionKeys.RequestUserId, value); }
        }

        public bool HasLoggedOut
        {
            get { return Session.GetBoolean(SessionKeys.HasLoggedOut); }
            set { Session.Set(SessionKeys.HasLoggedOut, value); }
        }
    }

    public static class AnonymousUserContextExtensions
    {
        public static AnonymousUserContext GetAnonymousUserContext(this HttpContextBase context)
        {
            return new AnonymousUserContext(context);
        }
    }
}
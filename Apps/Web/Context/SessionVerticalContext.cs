using System;
using System.Web;
using LinkMe.Apps.Agents.Context;
using LinkMe.Domain.Roles.Affiliations.Verticals;

namespace LinkMe.Web.Context
{
    internal class SessionVerticalContext
        : IVerticalContext
    {
        private const string VerticalId = "verticalId";

        void IVerticalContext.Set(Vertical vertical)
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                if (vertical == null)
                    session[VerticalId] = null;
                else
                    session[VerticalId] = vertical.Id;
            }
        }

        void IVerticalContext.Reset()
        {
            var session = HttpContext.Current.Session;
            if (session != null)
                session[VerticalId] = null;
        }

        Guid? IVerticalContext.Id
        {
            get
            {
                var session = HttpContext.Current.Session;
                return session == null ? null : session[VerticalId] as Guid?;
            }
        }
    }
}

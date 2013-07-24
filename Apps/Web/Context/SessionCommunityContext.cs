using System;
using System.Web;
using LinkMe.Apps.Agents.Context;
using LinkMe.Domain.Roles.Affiliations.Communities;

namespace LinkMe.Web.Context
{
    internal class SessionCommunityContext
        : ICommunityContext
    {
        public void Set(Community community)
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                if (community == null)
                    session["communityId"] = null;
                else
                    session["communityId"] = community.Id;
            }
        }

        public void Reset()
        {
            var session = HttpContext.Current.Session;
            if (session != null)
                session["communityId"] = null;
        }

        public bool IsSet
        {
            get { return Id != null; }
        }

        public Guid? Id
        {
            get
            {
                var session = HttpContext.Current.Session;
                return session == null ? null : session["communityId"] as Guid?;
            }
        }
    }
}

using System.Web;
using LinkMe.Apps.Agents.Users;
using LinkMe.Apps.Agents.Users.Members.Queries;
using LinkMe.Domain.Contacts;

namespace LinkMe.Web.Areas.Members.Models.JobAds
{
    public class Prompts
    {
        private readonly HttpSessionStateBase _session;
        private readonly IVisitorStatusQuery _visitorStatusQuery;

        private static class SessionKeys
        {
            public static readonly string Views = typeof(Prompts).FullName + ".Views";
            public static readonly string Applications = typeof(Prompts).FullName + ".Applications";
            public static readonly string OccasionalPrompts = typeof(Prompts).FullName + ".OccasionalPrompts";
            public static readonly string CasualPrompts = typeof(Prompts).FullName + ".CasualPrompts";
        }

        public Prompts(HttpSessionStateBase session, IVisitorStatusQuery visitorStatusQuery)
        {
            _session = session;
            _visitorStatusQuery = visitorStatusQuery;
        }

        public void AddView()
        {
            var views = GetValue(SessionKeys.Views);
            _session[SessionKeys.Views] = views + 1;
        }

        public void AddApplication()
        {
            var applications = GetValue(SessionKeys.Applications);
            _session[SessionKeys.Applications] = applications + 1;
        }

        public VisitorStatus GetVisitorStatus(IMember member)
        {
            // A logged in user does not get any prompts.

            if (member != null)
                return new VisitorStatus { Frequency = VisitorFrequency.Heavy, ShouldPrompt = false };

            var views = GetValue(SessionKeys.Views);
            var applications = GetValue(SessionKeys.Applications);
            var occasionalPrompts = GetValue(SessionKeys.OccasionalPrompts);
            var casualPrompts = GetValue(SessionKeys.CasualPrompts);
            var status = _visitorStatusQuery.GetVisitorStatus(views, applications, occasionalPrompts, casualPrompts);

            // Keep track of how many times the prompts have been shown.

            if (status.ShouldPrompt)
            {
                switch (status.Frequency)
                {
                    case VisitorFrequency.Occasional:
                        _session[SessionKeys.OccasionalPrompts] = occasionalPrompts + 1;
                        break;

                    case VisitorFrequency.Casual:
                        _session[SessionKeys.CasualPrompts] = casualPrompts + 1;
                        break;
                }
            }

            return status;
        }

        private int GetValue(string key)
        {
            var value = _session[key];
            return value is int ? (int) value : 0;
        }
    }
}
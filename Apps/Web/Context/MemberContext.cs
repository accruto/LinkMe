using System;
using System.Web;
using LinkMe.Apps.Agents.Profiles;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.Resources;
using LinkMe.Web.Models;

namespace LinkMe.Web.Context
{
    public abstract class JobAdsNavigation
        : Navigation
    {
        protected JobAdsNavigation(PresentationModel presentation)
            : base(presentation)
        {
        }
    }

    public class JobAdSearchNavigation
        : JobAdsNavigation
    {
        private readonly JobAdSearchCriteria _criteria;

        public JobAdSearchNavigation(JobAdSearchCriteria criteria, PresentationModel presentation)
            : base(presentation)
        {
            _criteria = criteria.Clone();
        }

        public JobAdSearchCriteria Criteria
        {
            get { return _criteria.Clone(); }
        }
    }

    public class ResourcesSearchNavigation
        : Navigation
    {
        private readonly ResourceSearchCriteria _criteria;

        public ResourcesSearchNavigation(ResourceSearchCriteria criteria, PresentationModel presentation)
            : base(presentation)
        {
            _criteria = criteria.Clone();
        }

        public ResourceSearchCriteria Criteria
        {
            get { return _criteria.Clone(); }
        }
    }

    public class MemberContext
        : UserContext
    {
        private static class SessionKeys
        {
            public static readonly string CurrentSearch = typeof(MemberContext).FullName + ".CurrentSearch";
            public static readonly string IsNotFirstSearch = typeof(MemberContext).FullName + ".IsNotFirstSearch";
            public static readonly string IsNewSearch = typeof(MemberContext).FullName + ".IsNewSearch";
            public static readonly string SavedSearchName = typeof(MemberContext).FullName + ".SavedSearchName";
            public static readonly string CurrentJobAds = typeof(MemberContext).FullName + ".CurrentJobAds";
            public static readonly string CurrentResourcesSearch = typeof(MemberContext).FullName + ".ResourcesSearch";
        }

        private static class ProfileKeys
        {
            public const string MemberProfile = "MemberProfile";
        }

        public MemberContext(HttpContextBase context)
            : base(context)
        {
        }

        public JobAdSearchNavigation CurrentSearch
        {
            set { Session.Set(SessionKeys.CurrentSearch, value); }
            get { return Session.Get<JobAdSearchNavigation>(SessionKeys.CurrentSearch); }
        }

        public JobAdsNavigation CurrentJobAds
        {
            set { Session.Set(SessionKeys.CurrentJobAds, value); }
            get { return Session.Get<JobAdsNavigation>(SessionKeys.CurrentJobAds); }
        }

        public ResourcesSearchNavigation CurrentResourcesSearch
        {
            set { Session.Set(SessionKeys.CurrentResourcesSearch, value); }
            get { return Session.Get<ResourcesSearchNavigation>(SessionKeys.CurrentResourcesSearch); }
        }

        public bool IsNewSearch
        {
            set { Session.Set(SessionKeys.IsNewSearch, value); }
            get { return Session.GetBoolean(SessionKeys.IsNewSearch); }
        }

        public bool IsFirstSearch
        {
            set { Session.Set(SessionKeys.IsNotFirstSearch, !value); }
            get { return !Session.GetBoolean(SessionKeys.IsNotFirstSearch); }
        }

        public string SavedSearchName
        {
            set { Session.Set(SessionKeys.SavedSearchName, value); }
            get { return Session.Get<string>(SessionKeys.SavedSearchName); }
        }

        public void HideUpdateStatusReminder()
        {
            var memberProfile = Profile.MemberProfile;
            if (!memberProfile.UpdateStatusReminder.Hide)
            {
                memberProfile.UpdateStatusReminder.Hide = true;
                Profile.MemberProfile = memberProfile;
            }
        }

        public void HideUpdatedTermsReminder()
        {
            var memberProfile = Profile.MemberProfile;
            if (!memberProfile.UpdatedTermsReminder.Hide)
            {
                memberProfile.UpdatedTermsReminder.Hide = true;
                Profile.MemberProfile = memberProfile;
            }
        }

        public bool ShowUpdatedTermsReminder()
        {
            return ShowUpdatedTermsReminder(ProfileKeys.MemberProfile, UserType.Member);
        }

        public bool ShowUpdateStatusPrompt()
        {
            var memberProfile = Profile.MemberProfile;
            if (!ShowUpdateStatusPrompt(memberProfile))
                return false;

            // Save that it has now been shown in response to this call.

            memberProfile.UpdateStatusReminder.FirstShownTime = DateTime.Now;
            Profile.MemberProfile = memberProfile;
            return true;
        }

        public bool ShowUpdateStatusReminder()
        {
            var memberProfile = Profile.MemberProfile;

            // Don't show it if it has been hidden ...

            if (memberProfile.UpdateStatusReminder.Hide)
                return false;

            // ... or the prompt should be shown ...

            if (ShowUpdateStatusPrompt(memberProfile))
                return false;

            // ... or it has already been shown for more than a month.

            if (memberProfile.UpdateStatusReminder.FirstShownTime != null
                && memberProfile.UpdateStatusReminder.FirstShownTime.Value < DateTime.Now.AddMonths(-1))
                return false;

            // Otherwise show it.

            return true;
        }

        private static bool ShowUpdateStatusPrompt(MemberProfile profile)
        {
            // Show it if it has not been hidden or if it has not already been shown.

            return !profile.UpdateStatusReminder.Hide && profile.UpdateStatusReminder.FirstShownTime == null;
        }
    }
}
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Guests;
using LinkMe.Web.Members.Friends;
using LinkMe.Web.UI.Registered.Networkers;
using ResourcesRoutes=LinkMe.Web.Areas.Members.Routes.ResourcesRoutes;

namespace LinkMe.Web.Views.Shared
{
    public class MemberNav
        : ViewUserControl
    {
        protected static ReadOnlyUrl ProfileUrl { get { return ProfilesRoutes.Profile.GenerateUrl(); } }
        protected static readonly ReadOnlyUrl DiaryUrl = NavigationManager.GetUrlForPage<Diary>();

        protected static ReadOnlyUrl AnonymousProfileUrl { get { return Areas.Public.Routes.HomeRoutes.GuestsProfile.GenerateUrl(); } }
        
        protected static readonly ReadOnlyUrl JobsUrl = SearchRoutes.Search.GenerateUrl();
        protected static readonly ReadOnlyUrl ApplicationsUrl = NavigationManager.GetUrlForPage<PreviousApplications>();
        protected static readonly ReadOnlyUrl PreviousSearchesUrl = SearchRoutes.RecentSearches.GenerateUrl();
        protected static readonly ReadOnlyUrl SuggestedJobsUrl = JobAdsRoutes.Suggested.GenerateUrl();
        protected static readonly ReadOnlyUrl BrowseJobsUrl = new ReadOnlyApplicationUrl("~/jobs");

        protected static readonly ReadOnlyUrl AnonymousApplicationsUrl = NavigationManager.GetUrlForPage<MyJobApplications>();
        protected static readonly ReadOnlyUrl AnonymousPreviousSearchesUrl = SearchRoutes.RecentSearches.GenerateUrl();

        protected static readonly ReadOnlyUrl FriendsUrl = NavigationManager.GetUrlForPage<ViewFriends>();
        protected static readonly ReadOnlyUrl FindFriendsUrl = NavigationManager.GetUrlForPage<FindFriends>();
        protected static readonly ReadOnlyUrl InviteFriendsUrl = NavigationManager.GetUrlForPage<InviteFriends>();
        protected static readonly ReadOnlyUrl RepresentativeUrl = NavigationManager.GetUrlForPage<ViewRepresentative>();
        protected static readonly ReadOnlyUrl InvitationsUrl = NavigationManager.GetUrlForPage<Invitations>();

        protected static readonly ReadOnlyUrl AnonymousFriendsUrl = NavigationManager.GetUrlForPage<Friends>();

        protected static ReadOnlyUrl ResourcesUrl { get { return ResourcesRoutes.Resources.GenerateUrl(); } }
    }
}

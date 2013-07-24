using System.Web;
using LinkMe.Apps.Agents.Featured;
using LinkMe.Apps.Asp.Caches;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Areas.Shared;
using LinkMe.Web.UI.Registered.Employers;
using AccountsRoutes=LinkMe.Web.Areas.Public.Routes.LoginsRoutes;
using HomeRoutes=LinkMe.Web.Areas.Employers.Routes.HomeRoutes;
using ResourcesRoutes=LinkMe.Web.Areas.Employers.Routes.ResourcesRoutes;

namespace LinkMe.Web.UI.Controls.Common.Navs
{
    public class EmployerHeaderUserControl
        : LinkMeUserControl
    {
        // This is used to grab the total candidates and is somewhat of a, hopefully, short-term hack.
        // This control is only used on the non-logged-in employer home page.  When this last page is
        // converted to MVC then the cache manager can be passed to the appropriate controller.

        private readonly ICacheManager _cacheManager = Container.Current.Resolve<ICacheManager>("cache.home");

        protected int Members { get; private set; }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            Members = _cacheManager.GetCachedItem<FeaturedStatistics>(HttpContext.Current.Cache, CacheKeys.FeaturedStatistics).Members;
        }

        protected static ReadOnlyUrl AboutUrl { get { return SupportRoutes.AboutUs.GenerateUrl(); } }
        protected static ReadOnlyUrl PrivacyUrl { get { return SupportRoutes.Privacy.GenerateUrl(); } }

        protected static ReadOnlyUrl HomeUrl { get { return SearchRoutes.Search.GenerateUrl(); } }
        protected static ReadOnlyUrl AnonymousHomeUrl { get { return HomeRoutes.Home.GenerateUrl(); } }
        protected static ReadOnlyUrl MemberHomeUrl { get { return Areas.Public.Routes.HomeRoutes.Home.GenerateUrl(new { ignorePreferred = true }); } }
        
        protected static ReadOnlyUrl AccountUrl { get { return SettingsRoutes.Settings.GenerateUrl(); } }
        protected static ReadOnlyUrl AnonymousAccountUrl { get { return HttpContext.Current.GetEmployerLoginUrl(SettingsRoutes.Settings); } }

        protected static ReadOnlyUrl LogInUrl { get { return HttpContext.Current.GetEmployerLoginUrl(); } }
        protected static ReadOnlyUrl LogOutUrl { get { return AccountsRoutes.LogOut.GenerateUrl(); } }
        protected static ReadOnlyUrl JoinUrl { get { return Areas.Employers.Routes.AccountsRoutes.Join.GenerateUrl(); } }
        
        // Candidate search

        protected static ReadOnlyUrl SearchCandidatesUrl { get { return SearchRoutes.Search.GenerateUrl(); } }
        protected static readonly ReadOnlyUrl SavedResumeSearchesUrl = NavigationManager.GetUrlForPage<SavedResumeSearches>();
        protected static readonly ReadOnlyUrl SuggestedCandidatesUrl = NavigationManager.GetUrlForPage<EmployerJobAdsSuggestions>();
        protected static readonly ReadOnlyUrl SavedResumeSearchAlertsUrl = NavigationManager.GetUrlForPage<SavedResumeSearchAlerts>();
        protected static ReadOnlyUrl FlaggedCandidatesUrl { get { return CandidatesRoutes.FlagList.GenerateUrl(); } }
        protected static ReadOnlyUrl ManageFoldersUrl { get { return CandidatesRoutes.Folders.GenerateUrl(); } }
        protected static ReadOnlyUrl BlockListsUrl { get { return CandidatesRoutes.PermanentBlockList.GenerateUrl(); } }

        protected static ReadOnlyUrl AnonymousSavedResumeSearchesUrl { get { return HttpContext.Current.GetLoginUrl(NavigationManager.GetUrlForPage<SavedResumeSearches>()); } }
        protected static ReadOnlyUrl AnonymousSuggestedCandidatesUrl { get { return HttpContext.Current.GetLoginUrl(NavigationManager.GetUrlForPage<EmployerJobAdsSuggestions>()); } }
        protected static ReadOnlyUrl AnonymousSavedResumeSearchAlertsUrl { get { return HttpContext.Current.GetLoginUrl(NavigationManager.GetUrlForPage<SavedResumeSearchAlerts>()); } }
        protected static ReadOnlyUrl AnonymousFlaggedCandidatesUrl { get { return HttpContext.Current.GetLoginUrl(CandidatesRoutes.FlagList); } }
        protected static ReadOnlyUrl AnonymousManageFoldersUrl { get { return HttpContext.Current.GetLoginUrl(CandidatesRoutes.Folders); } }
        protected static ReadOnlyUrl AnonymousBlocklistsUrl { get { return HttpContext.Current.GetLoginUrl(CandidatesRoutes.PermanentBlockList); } }

        // Job ads

        protected static ReadOnlyUrl NewJobAdUrl { get { return JobAdsRoutes.JobAd.GenerateUrl(); } }
        protected static readonly ReadOnlyUrl OpenJobAdsUrl = NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Open");
        protected static readonly ReadOnlyUrl DraftJobAdsUrl = NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Draft");
        protected static readonly ReadOnlyUrl ClosedJobAdsUrl = NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Closed");

        protected static ReadOnlyUrl AnonymousNewJobAdUrl { get { return HttpContext.Current.GetLoginUrl(JobAdsRoutes.JobAd); } }
        protected static ReadOnlyUrl AnonymousOpenJobAdsUrl { get { return HttpContext.Current.GetLoginUrl(NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Open")); } }
        protected static ReadOnlyUrl AnonymousDraftJobAdsUrl { get { return HttpContext.Current.GetLoginUrl(NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Draft")); } }
        protected static ReadOnlyUrl AnonymousClosedJobAdsUrl { get { return HttpContext.Current.GetLoginUrl(NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Closed")); } }

        // Purchase

        protected static ReadOnlyUrl NewOrderUrl { get { return ProductsRoutes.NewOrder.GenerateUrl(); } }
        
        // Resources

        protected static ReadOnlyUrl ResourcesUrl { get { return ResourcesRoutes.Resources.GenerateUrl(); } }
    }
}

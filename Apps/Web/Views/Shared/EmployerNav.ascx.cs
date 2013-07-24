using System.Web;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.UI.Registered.Employers;

namespace LinkMe.Web.Views.Shared
{
    public class EmployerNav
        : ViewUserControl
    {
        // Candidate search

        protected static ReadOnlyUrl SearchCandidatesUrl { get { return SearchRoutes.Search.GenerateUrl(); } }
        protected static readonly ReadOnlyUrl SavedResumeSearchesUrl = NavigationManager.GetUrlForPage<SavedResumeSearches>();
        protected static readonly ReadOnlyUrl SuggestedCandidatesUrl = NavigationManager.GetUrlForPage<EmployerJobAdsSuggestions>();
        protected static readonly ReadOnlyUrl SavedResumeSearchAlertsUrl = NavigationManager.GetUrlForPage<SavedResumeSearchAlerts>();
        protected static ReadOnlyUrl FlaggedCandidatesUrl { get { return CandidatesRoutes.FlagList.GenerateUrl(); } }
        protected static ReadOnlyUrl ManageFoldersUrl { get { return CandidatesRoutes.Folders.GenerateUrl(); } }
        protected static ReadOnlyUrl BlockListsUrl { get { return CandidatesRoutes.PermanentBlockList.GenerateUrl(); } }

        protected static ReadOnlyUrl AnonymousSavedResumeSearchesUrl { get { return HttpContext.Current.GetEmployerLoginUrl(NavigationManager.GetUrlForPage<SavedResumeSearches>()); } }
        protected static ReadOnlyUrl AnonymousSuggestedCandidatesUrl { get { return HttpContext.Current.GetEmployerLoginUrl(NavigationManager.GetUrlForPage<EmployerJobAdsSuggestions>()); } }
        protected static ReadOnlyUrl AnonymousSavedResumeSearchAlertsUrl { get { return HttpContext.Current.GetEmployerLoginUrl(NavigationManager.GetUrlForPage<SavedResumeSearchAlerts>()); } }
        protected static ReadOnlyUrl AnonymousFlaggedCandidatesUrl { get { return HttpContext.Current.GetEmployerLoginUrl(CandidatesRoutes.FlagList); } }
        protected static ReadOnlyUrl AnonymousManageFoldersUrl { get { return HttpContext.Current.GetEmployerLoginUrl(CandidatesRoutes.Folders); } }
        protected static ReadOnlyUrl AnonymousBlocklistsUrl { get { return HttpContext.Current.GetEmployerLoginUrl(CandidatesRoutes.PermanentBlockList); } }

        // Job ads

        protected static ReadOnlyUrl NewJobAdUrl { get { return JobAdsRoutes.JobAd.GenerateUrl(); } }
        protected static readonly ReadOnlyUrl OpenJobAdsUrl = NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Open");
        protected static readonly ReadOnlyUrl DraftJobAdsUrl = NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Draft");
        protected static readonly ReadOnlyUrl ClosedJobAdsUrl = NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Closed");

        protected static ReadOnlyUrl AnonymousOpenJobAdsUrl { get { return HttpContext.Current.GetEmployerLoginUrl(NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Open")); } }
        protected static ReadOnlyUrl AnonymousDraftJobAdsUrl { get { return HttpContext.Current.GetEmployerLoginUrl(NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Draft")); } }
        protected static ReadOnlyUrl AnonymousClosedJobAdsUrl { get { return HttpContext.Current.GetEmployerLoginUrl(NavigationManager.GetUrlForPage<EmployerJobAds>("mode", "Closed")); } }

        // Account

        protected static ReadOnlyUrl AccountUrl { get { return SettingsRoutes.Settings.GenerateUrl(); } }
        protected static ReadOnlyUrl AnonymousAccountUrl { get { return HttpContext.Current.GetEmployerLoginUrl(SettingsRoutes.Settings); } }

        // Purchase

        protected static ReadOnlyUrl NewOrderUrl { get { return ProductsRoutes.NewOrder.GenerateUrl(); } }

        // Resources

        protected static ReadOnlyUrl ResourcesUrl { get { return ResourcesRoutes.Resources.GenerateUrl(); } }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Featured;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Caches;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Query.Search.JobAds;
using LinkMe.Web.Areas.Public.Models.Home;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Areas.Shared;
using LinkMe.Web.Areas.Shared.Models;
using LinkMe.Web.Controllers;
using EmployerHomeRoutes = LinkMe.Web.Areas.Employers.Routes.HomeRoutes;
using MemberJoin = LinkMe.Web.Models.Accounts.MemberJoin;

namespace LinkMe.Web.Areas.Public.Controllers.Home
{
    [EnsureNotAuthorized]
    public class HomeController
        : PublicLoginJoinController
    {
        private readonly IIndustriesQuery _industriesQuery;
        private readonly ICacheManager _cacheManager;
        private readonly int _featuredItems;
        private readonly ILocationQuery _locationQuery;
        private readonly IResourcesQuery _resourcesQuery;

        public HomeController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery, IIndustriesQuery industriesQuery, ICacheManager cacheManager, int featuredItems, ILocationQuery locationQuery, IResourcesQuery resourcesQuery)
            : base(accountsManager, loginCredentialsQuery, faqsQuery)
        {
            _industriesQuery = industriesQuery;
            _cacheManager = cacheManager;
            _featuredItems = featuredItems;
            _locationQuery = locationQuery;
            _resourcesQuery = resourcesQuery;
        }

        [EnsureHttp, HttpGet]
        public ActionResult Home(bool? ignorePreferred)
        {
            var preferredUserType = HttpContext.User.PreferredUserType();
            if ((ignorePreferred == null || !ignorePreferred.Value) && preferredUserType == UserType.Employer)
                return RedirectToRoute(EmployerHomeRoutes.Home);

            return View(CreateHomeModel(preferredUserType, null, null, false));
        }

        [EnsureHttp, HttpHead]
        public ActionResult Home()
        {
            return View(CreateHomeModel(UserType.Anonymous, null, null, false));
        }

        [HttpPost, ButtonClicked("Login")]
        public ActionResult Home(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            var result = TryLogIn(loginModel, rememberMe, r => null, () => new HomeErrorHandler(HttpContext.GetLoginUrl(), Accounts.Routes.AccountsRoutes.NewPassword.GenerateUrl()));
            return result ?? View(CreateHomeModel(UserType.Anonymous, loginModel, null, false));
        }

        [HttpPost, ButtonClicked("Join")]
        public ActionResult Home(MemberJoin joinModel, [Bind(Include = "AcceptTerms")] CheckBoxValue acceptTerms)
        {
            var result = TryJoin(joinModel, acceptTerms, () => new HomeErrorHandler(HttpContext.GetLoginUrl(), Accounts.Routes.AccountsRoutes.NewPassword.GenerateUrl()));
            return result ?? View(CreateHomeModel(UserType.Anonymous, null, joinModel, acceptTerms != null && acceptTerms.IsChecked));
        }

        public ActionResult Partners(string pcode)
        {
            // ~/partners/t2 was once used to as a landing url.  Not sure if still in use but still support it.

            var clientUrl = HttpContext.GetClientUrl();
            var homeUrl = HomeRoutes.Home.GenerateUrl().AsNonReadOnly();
            homeUrl.QueryString.Add(clientUrl.QueryString);
            homeUrl.QueryString[Apps.Asp.Referrals.Constants.PromoCodeParameter] = pcode;
            return RedirectToUrl(homeUrl);
        }

        private HomeModel CreateHomeModel(UserType preferredUserType, Login login, MemberJoin join, bool acceptTerms)
        {
            var country = ActivityContext.Location.Country;

            var qna = _resourcesQuery.GetQnA(_resourcesQuery.GetFeaturedQnAs().Single().ResourceId);

            return new HomeModel
            {
                PreferredUserType = preferredUserType,
                Login = login,
                Join = join,
                AcceptTerms = acceptTerms,
                Reference = new ReferenceModel
                {
                    MinSalary = JobAdSearchCriteria.MinSalary,
                    MaxSalary = JobAdSearchCriteria.MaxSalary,
                    StepSalary = JobAdSearchCriteria.StepSalary,
                    MinHourlySalary = JobAdSearchCriteria.MinHourlySalary,
                    MaxHourlySalary = JobAdSearchCriteria.MaxHourlySalary,
                    StepHourlySalary = JobAdSearchCriteria.StepHourlySalary,
                    Industries = GetIndustries(),
                    FeaturedStatistics = GetFeaturedStatistics(),
                    FeaturedEmployers = GetFeaturedEmployers(),
                    FeaturedJobAds = GetFeaturedJobAds(),
                    FeaturedCandidateSearches = GetFeaturedCandidateSearches(),
                    Countries = _locationQuery.GetCountries(),
                    CountrySubdivisions = (from s in _locationQuery.GetCountrySubdivisions(country) where !s.IsCountry select s).ToList(),
                    Regions = _locationQuery.GetRegions(country),
                    DefaultCountry = country,
                    FeaturedAnsweredQuestion = qna,
                    Categories = _resourcesQuery.GetCategories(),
                }
            };
        }

        private FeaturedStatistics GetFeaturedStatistics()
        {
            return _cacheManager.GetCachedItem<FeaturedStatistics>(HttpContext.Cache, CacheKeys.FeaturedStatistics);
        }

        private IList<FeaturedEmployerModel> GetFeaturedEmployers()
        {
            return _cacheManager.GetCachedItem<IList<FeaturedEmployerModel>>(HttpContext.Cache, CacheKeys.FeaturedEmployers);
        }

        private IList<FeaturedLinkModel> GetFeaturedJobAds()
        {
            // Return a random sample of all featured job ads.

            return _cacheManager.GetCachedItem<IList<FeaturedLinkModel>>(HttpContext.Cache, CacheKeys.FeaturedJobAds).Randomise().Take(_featuredItems).ToList();
        }

        private IList<FeaturedLinkModel> GetFeaturedCandidateSearches()
        {
            // Return a random sample of all featured candidate searches.

            return _cacheManager.GetCachedItem<IList<FeaturedLinkModel>>(HttpContext.Cache, CacheKeys.FeaturedCandidateSearches).Randomise().Take(_featuredItems).ToList();
        }

        private IList<Industry> GetIndustries()
        {
            // Other industries come last.

            var other = _industriesQuery.GetIndustry("Other");
            return (from i in _industriesQuery.GetIndustries()
                    where i.Name != "Other"
                    orderby i.Name
                    select i)
                    .Concat(new[] { other }).ToList();
        }
    }
}

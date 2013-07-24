using System.Web.Mvc;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.Home;
using LinkMe.Web.Areas.Employers.Models.Search;
using LinkMe.Web.Controllers;
using LinkMe.Domain.Location.Queries;
using MemberHomeRoutes = LinkMe.Web.Areas.Public.Routes.HomeRoutes;

namespace LinkMe.Web.Areas.Employers.Controllers.Home
{
    [EnsureNotAuthorized]
    public class HomeController
        : EmployersLoginJoinController
    {
        private readonly ILocationQuery _locationQuery;
        private const string AppStoreUrl = "http://itunes.apple.com/us/app/candidate-connect/id490840013?mt=8";

        public HomeController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery, ILocationQuery locationQuery)
            : base(accountsManager, loginCredentialsQuery, faqsQuery)
        {
            _locationQuery = locationQuery;
        }

        [EnsureHttps]
        public ActionResult Home(bool? ignorePreferred)
        {
            // Look for redirect to other home page.

            if ((ignorePreferred == null || !ignorePreferred.Value) && HttpContext.User.PreferredUserType() == UserType.Member)
                return RedirectToRoute(MemberHomeRoutes.Home);

            return View(CreateHomeModel(null, false));
        }

        [EnsureHttps, HttpPost]
        public ActionResult Home(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            var result = TryLogIn(loginModel, rememberMe);
            return result ?? View(CreateHomeModel(loginModel, false));
        }

        public ActionResult Features()
        {
            return View(CreateHomeModel(null, false));
        }

        public ActionResult CandidateConnect()
        {
            return View(CreateHomeModel(null, false));
        }

        private HomeModel CreateHomeModel(Login login, bool acceptTerms)
        {
            return new HomeModel
            {
                Login = login,
                AcceptTerms = acceptTerms,
                Reference = new ReferenceModel
                {
                    KeywordLocationSearch = new KeywordLocationSearchModel
                    {
                        Criteria = new MemberSearchCriteria
                        {
                            Distance = MemberSearchCriteria.DefaultDistance,
                            Location = _locationQuery.ResolveLocation(ActivityContext.Location.Country, null),
                        },
                        CanSearchByName = false,
                        Distances = Reference.Distances,
                        DefaultDistance = MemberSearchCriteria.DefaultDistance,
                        Countries = _locationQuery.GetCountries(),
                        DefaultCountry = ActivityContext.Location.Country,
                    },
                },
                AppStoreUrl = AppStoreUrl,
            };
        }
    }
}

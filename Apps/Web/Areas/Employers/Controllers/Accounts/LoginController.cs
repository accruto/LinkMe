using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Web.Areas.Employers.Models.Accounts;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Controllers;
using EmployerJoin = LinkMe.Web.Models.Accounts.EmployerJoin;

namespace LinkMe.Web.Areas.Employers.Controllers.Accounts
{
    [EnsureNotAuthorized]
    public class LoginController
        : EmployersLoginJoinController
    {
        private readonly IIndustriesQuery _industriesQuery;

        public LoginController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery, IIndustriesQuery industriesQuery)
            : base(accountsManager, loginCredentialsQuery, faqsQuery)
        {
            _industriesQuery = industriesQuery;
        }

        [EnsureHttps]
        public ActionResult LogIn()
        {
            return AccountView(new Login { LoginId = GetLoginId() }, null, false);
        }

        [EnsureHttps]
        public ActionResult Join()
        {
            return AccountView(null, new EmployerJoin(), false);
        }

        [EnsureHttps, HttpPost, ActionName("LogIn"), ButtonClicked("Login")]
        public ActionResult LogInLogIn(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            return LogIn(loginModel, rememberMe);
        }

        [EnsureHttps, HttpPost, ActionName("Join"), ButtonClicked("Login")]
        public ActionResult JoinLogIn(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            return LogIn(loginModel, rememberMe);
        }

        [EnsureHttps, HttpPost, ActionName("LogIn"), ButtonClicked("Join")]
        public ActionResult LogInJoin(EmployerJoin joinModel, [Bind(Include = "AcceptTerms")] CheckBoxValue acceptTerms)
        {
            return Join(joinModel, acceptTerms);
        }

        [EnsureHttps, HttpPost, ActionName("Join"), ButtonClicked("Join")]
        public ActionResult JoinJoin(EmployerJoin joinModel, [Bind(Include = "AcceptTerms")] CheckBoxValue acceptTerms)
        {
            return Join(joinModel, acceptTerms);
        }

        [EnsureHttps, HttpPost, ActionName("LogIn"), ButtonClicked("Purchase")]
        public ActionResult LogInPurchase()
        {
            return Purchase();
        }

        [EnsureHttps, HttpPost, ActionName("Join"), ButtonClicked("Purchase")]
        public ActionResult JoinPurchase()
        {
            return Purchase();
        }

        private ActionResult LogIn(Login login, CheckBoxValue rememberMe)
        {
            var result = TryLogIn(login, rememberMe);
            return result ?? AccountView(login, null, false);
        }

        private ActionResult Join(EmployerJoin join, CheckBoxValue acceptTerms)
        {
            var result = TryJoin(join, acceptTerms);
            return result ?? AccountView(null, join, acceptTerms != null && acceptTerms.IsChecked);
        }

        private ActionResult Purchase()
        {
            return RedirectToRoute(ProductsRoutes.NewOrder);
        }

        private ActionResult AccountView(Login login, EmployerJoin join, bool acceptTerms)
        {
            return View("Account", new AccountModel
            {
                Login = login,
                Join = join ?? new EmployerJoin(),
                AcceptTerms = acceptTerms,
                Industries = _industriesQuery.GetIndustries()
            });
        }
    }
}
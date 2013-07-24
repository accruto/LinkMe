using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Web.Areas.Public.Models.Logins;
using LinkMe.Web.Controllers;
using MemberJoin = LinkMe.Web.Models.Accounts.MemberJoin;

namespace LinkMe.Web.Areas.Public.Controllers.Logins
{
    [EnsureNotAuthorized]
    public class LoginController
        : PublicLoginJoinController
    {
        public LoginController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery)
            : base(accountsManager, loginCredentialsQuery, faqsQuery)
        {
        }

        [EnsureHttps]
        public ActionResult LogIn(LoginReason? reason)
        {
            return View(new Login { LoginId = GetLoginId() }, reason, null, false);
        }

        [EnsureHttps, HttpPost, ButtonClicked("Login")]
        public ActionResult LogIn(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            var result = TryLogIn(loginModel, rememberMe);
            return result ?? View(loginModel, null, null, false);
        }

        [EnsureHttps, HttpPost, ButtonClicked("Join")]
        public ActionResult LogIn(MemberJoin joinModel, [Bind(Include = "AcceptTerms")] CheckBoxValue acceptTerms)
        {
            var result = TryJoin(joinModel, acceptTerms);
            return result ?? View(null, null, joinModel, acceptTerms != null && acceptTerms.IsChecked);
        }

        private ActionResult View(Login login, LoginReason? reason, MemberJoin join, bool acceptTerms)
        {
            return View("Account", new LoginModel
            {
                Login = login ?? new Login(),
                Reason = reason,
                Join = join ?? new MemberJoin(),
                AcceptTerms = acceptTerms,
            });
        }
    }
}
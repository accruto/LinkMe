using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Areas.Accounts.Models;

namespace LinkMe.Web.Areas.Accounts.Controllers
{
    public class LoginApiController
        : ApiController
    {
        private readonly IAccountsManager _accountsManager;

        public LoginApiController(IAccountsManager accountsManager)
        {
            _accountsManager = accountsManager;
        }

        [ApiEnsureNotAuthorized, HttpPost]
        public ActionResult LogIn(LoginModel loginModel)
        {
            try
            {
                var login = loginModel == null ? new Login() : new Login { LoginId = loginModel.LoginId, Password = loginModel.Password, RememberMe = loginModel.RememberMe };
                var result = _accountsManager.LogIn(HttpContext, login);

                switch (result.Status)
                {
                    case AuthenticationStatus.Disabled:
                        throw new UserDisabledException();

                    case AuthenticationStatus.Failed:
                        throw new AuthenticationFailedException();
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized, HttpPost]
        public ActionResult LogOut()
        {
            try
            {
                _accountsManager.LogOut(HttpContext);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }
    }
}

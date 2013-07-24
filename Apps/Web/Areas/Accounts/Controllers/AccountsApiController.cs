using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Web.Areas.Accounts.Controllers
{
    public class AccountsApiController
        : ApiController
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AccountsApiController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost]
        public ActionResult PreferredUserType(UserType userType)
        {
            try
            {
                if (CurrentAnonymousUser != null)
                    _authenticationManager.UpdateUser(HttpContext, CurrentAnonymousUser, userType);
                return Json(new JsonResponseModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }
    }
}
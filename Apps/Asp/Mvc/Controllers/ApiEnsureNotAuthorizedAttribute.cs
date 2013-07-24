using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public class ApiEnsureNotAuthorizedAttribute
        : AuthorizedAttribute
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Check that the current user is not set.

            var user = filterContext.Controller.ViewData.GetCurrentRegisteredUser();
            if (user != null)
                SetAlreadyAuthorized(filterContext);
        }

        private static void SetAlreadyAuthorized(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.SkipAuthorization = true;

            var exception = new AlreadyAuthorizedException();
            var model = new JsonResponseModel
            {
                Success = false,
                Errors = new List<JsonError>
                {
                    new JsonError
                    {
                        Code = ((IErrorHandler)new StandardErrorHandler()).GetErrorCode(exception),
                        Message = ((IErrorHandler)new StandardErrorHandler()).FormatErrorMessage(exception),
                    },
                }
            };

            filterContext.Result = new JsonResponseResult(model, JsonRequestBehavior.AllowGet, null);
        }
    }
}

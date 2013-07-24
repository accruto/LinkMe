using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    public class ErrorsApiController
        : ApiController
    {
        public ActionResult ServerError()
        {
            var exception = new ServerErrorException();

            // Need to populate errors.

            var errorHandler = (IErrorHandler)new StandardErrorHandler();
            var message = errorHandler.FormatErrorMessage(exception);
            var errorCode = errorHandler.GetErrorCode(exception);

            return Json(new JsonResponseModel
            {
                Success = false,
                Errors = new List<JsonError> { new JsonError { Code = errorCode, Message = message } }
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NotFound(string requestedUrl)
        {
            var url = requestedUrl.GetRequestedUrl();
            var exception = new UrlNotFoundException(url == null ? null : url.AbsoluteUri);

            // Need to populate errors.

            var errorHandler = (IErrorHandler)new StandardErrorHandler();
            var message = errorHandler.FormatErrorMessage(exception);
            var errorCode = errorHandler.GetErrorCode(exception);

            return Json(new JsonResponseModel
            {
                Success = false,
                Errors = new List<JsonError> { new JsonError { Code = errorCode, Message = message } }
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
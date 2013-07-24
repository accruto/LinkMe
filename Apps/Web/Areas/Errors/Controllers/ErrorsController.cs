using System.Net;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Web.Areas.Errors.Models.Errors;

namespace LinkMe.Web.Areas.Errors.Controllers
{
    [NoCache]
    public class ErrorsController
        : ViewController
    {
        private const string ErrorReportSessionKey = "ErrorReport";
        private readonly IDevAuthenticationManager _devAuthenticationManager;

        public ErrorsController(IDevAuthenticationManager devAuthenticationManager)
        {
            _devAuthenticationManager = devAuthenticationManager;
        }

        public ActionResult ServerError(ErrorReport report, bool? hideDetails)
        {
            // Show details only if the user is allowed to.

            var showDetails = _devAuthenticationManager.IsLoggedIn(HttpContext)
                && (hideDetails != null && !hideDetails.Value);

            // Save the error report in the session in case it is needed later or pull it out if it is already there.

            if (report != null && report.Exception != null && Session != null)
                Session[ErrorReportSessionKey] = report;
            else if (showDetails && Session != null)
                report = Session[ErrorReportSessionKey] as ErrorReport;

            return ServerError(report, showDetails);
        }

        [HttpPost]
        public ActionResult ServerError(string password)
        {
            return _devAuthenticationManager.AuthenticateUser(password) == AuthenticationStatus.Authenticated
                ? ServerError(Session[ErrorReportSessionKey] as ErrorReport, _devAuthenticationManager.IsLoggedIn(HttpContext))
                : ServerError(null, false);
        }

        public ActionResult NotFound(string requestedUrl, string referrerUrl)
        {
            var url = requestedUrl.GetRequestedUrl();
            return NotFound(new NotFoundModel
            {
                RequestedUrl = url,
                ReferrerUrl = referrerUrl.GetReferrerUrl(url),
            });
        }

        public ActionResult ObjectNotFound(string type, string propertyName, string propertyValue)
        {
            var model = new ObjectNotFoundModel { Type = type, PropertyName = propertyName, PropertyValue = propertyValue };
            if (!string.IsNullOrEmpty(model.PropertyName))
                ModelState.AddModelError(model.PropertyName, "The " + model.Type + " with " + model.PropertyName + " '" + model.PropertyValue + "' cannot be found.");
            else
                ModelState.AddModelError("The " + model.Type + " cannot be found.");
            return NotFound(model);
        }

        private ActionResult NotFound(object model)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View(model);
        }

        private ActionResult ServerError(ErrorReport report, bool showDetails)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View(new ServerErrorModel
            {
                ShowDetails = showDetails,
                Report = report
            });
        }
    }
}
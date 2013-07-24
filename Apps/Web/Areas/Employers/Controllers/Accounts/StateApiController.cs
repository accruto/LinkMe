using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Web.Areas.Employers.Controllers.Accounts
{
    public class StateApiController
        : EmployersApiController
    {
        private static readonly EventSource EventSource = new EventSource<StateApiController>();

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult HideCreditReminder()
        {
            const string method = "HideCreditReminder";

            try
            {
                EmployerContext.HideCreditReminder();
            }
            catch (Exception ex)
            {
                // Log an error but ignore for now.

                EventSource.Raise(Event.Error, method, ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult HideBulkCreditReminder()
        {
            const string method = "HideBulkCreditReminder";

            try
            {
                EmployerContext.HideBulkCreditReminder();
            }
            catch (Exception ex)
            {
                // Log an error but ignore for now.

                EventSource.Raise(Event.Error, method, ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }
    }
}

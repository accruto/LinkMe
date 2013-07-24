using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Employers
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class EmployerCreditsApiController
        : ApiController
    {
        private readonly IEmployersQuery _employersQuery;
        private readonly IEmployerAllocationsCommand _employerAllocationsCommand;

        public EmployerCreditsApiController(IEmployersQuery employersQuery, IEmployerAllocationsCommand employerAllocationsCommand)
        {
            _employersQuery = employersQuery;
            _employerAllocationsCommand = employerAllocationsCommand;
        }

        [HttpPost]
        public ActionResult Deallocate(Guid id, Guid allocationId)
        {
            try
            {
                var employer = _employersQuery.GetEmployer(id);
                if (employer == null)
                    return JsonNotFound("employer");

                _employerAllocationsCommand.Deallocate(allocationId);
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
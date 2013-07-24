using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.Recruiters;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Organisations
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class OrganisationsApiController
        : ApiController
    {
        private readonly IExecuteOrganisationSearchCommand _executeOrganisationSearchCommand;
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly IAllocationsCommand _allocationsCommand;
        private const int DefaultMaxResults = 10;

        public OrganisationsApiController(IExecuteOrganisationSearchCommand executeOrganisationSearchCommand, IOrganisationsQuery organisationsQuery, IAllocationsCommand allocationsCommand)
        {
            _executeOrganisationSearchCommand = executeOrganisationSearchCommand;
            _organisationsQuery = organisationsQuery;
            _allocationsCommand = allocationsCommand;
        }

        public ActionResult FindPartialMatchedOrganisations(string name, int? maxResults)
        {
            var partialMatches = _executeOrganisationSearchCommand.GetOrganisationFullNames(name, maxResults ?? DefaultMaxResults);
            return Json(partialMatches.ToArray());
        }

        [HttpPost]
        public ActionResult Deallocate(Guid id, Guid allocationId)
        {
            try
            {
                var organisation = _organisationsQuery.GetOrganisation(id);
                if (organisation == null)
                    return JsonNotFound("organisation");

                _allocationsCommand.Deallocate(allocationId);
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
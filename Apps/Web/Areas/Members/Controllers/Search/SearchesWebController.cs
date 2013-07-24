using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Anonymous.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.Web.Areas.Members.Models.Search;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.Search
{
    public class SearchesWebController
        : SearchesController
    {
        private readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand;
        private readonly IAnonymousUsersQuery _anonymousUsersQuery;

        public SearchesWebController(IJobAdSearchesQuery jobAdSearchesQuery, IJobAdSearchAlertsQuery jobAdSearchAlertsQuery, IJobAdSearchAlertsCommand jobAdSearchAlertsCommand, IAnonymousUsersQuery anonymousUsersQuery)
            : base(jobAdSearchesQuery, jobAdSearchAlertsQuery)
        {
            _jobAdSearchAlertsCommand = jobAdSearchAlertsCommand;
            _anonymousUsersQuery = anonymousUsersQuery;
        }

        [EnsureAuthorized(UserType.Member)]
        public ActionResult RecentSearches()
        {
            var pagination = new Pagination();
            PreparePaginationModel(pagination);
            var results = GetRecentSearchesModel(pagination);
            return View("Searches", results);
        }

        [EnsureAuthorized(UserType.Member)]
        public ActionResult PartialRecentSearches(Pagination pagination)
        {
            PreparePaginationModel(pagination);
            var results = GetRecentSearchesModel(pagination);
            return PartialView("Recent", results);
        }

        [EnsureAuthorized(UserType.Member)]
        public ActionResult SavedSearches()
        {
            var pagination = new Pagination();
            PreparePaginationModel(pagination);
            var results = GetSavedSearchesModel(pagination);
            return View("Searches", results);
        }

        [EnsureAuthorized(UserType.Member)]
        public ActionResult PartialSavedSearches(Pagination pagination)
        {
            PreparePaginationModel(pagination);
            var results = GetSavedSearchesModel(pagination);
            return PartialView("Saved", results);
        }

        [EnsureNotAuthorized]
        public ActionResult DeleteSearch(Guid searchId)
        {
            var model = new DeleteSearchModel();

            try
            {
                model.Search = GetSearch(searchId);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(model);
        }

        [HttpPost, EnsureNotAuthorized, ActionName("DeleteSearch")]
        public ActionResult PostDeleteSearch(Guid searchId)
        {
            // Used to 'unsubscribe' from an anonymous user's alert.

            var model = new DeleteSearchModel();

            try
            {
                model.Search = GetSearch(searchId);

                // Given that an anonymous user could be responding to an email alert on different devices
                // and that they don't have to sign in get the owner directly from the search itself.

                var owner = _anonymousUsersQuery.GetContact(model.Search.OwnerId);
                if (owner == null)
                    throw new ValidationErrorsException(new NotFoundValidationError("alert id", searchId));

                // Delete it.

                _jobAdSearchAlertsCommand.DeleteJobAdSearch(owner.Id, searchId);
                model.HasDeleted = true;

                ModelState.AddModelConfirmation("You have now been unsubscribed from this job alert.");
                return View(model);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(model);
        }
    }
}

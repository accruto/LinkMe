using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Anonymous.Commands;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.Web.Areas.Members.Models;

namespace LinkMe.Web.Areas.Members.Controllers.Search
{
    public class SearchesApiController
        : MembersApiController
    {
        private readonly IJobAdSearchesCommand _jobAdSearchesCommand;
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery;
        private readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand;
        private readonly IAnonymousUsersCommand _anonymousUsersCommand;

        public SearchesApiController(IJobAdSearchesCommand jobAdSearchesCommand, IJobAdSearchesQuery jobAdSearchesQuery, IJobAdSearchAlertsCommand jobAdSearchAlertsCommand, IAnonymousUsersCommand anonymousUsersCommand)
        {
            _jobAdSearchesCommand = jobAdSearchesCommand;
            _jobAdSearchesQuery = jobAdSearchesQuery;
            _jobAdSearchAlertsCommand = jobAdSearchAlertsCommand;
            _anonymousUsersCommand = anonymousUsersCommand;
        }

        [HttpPost]
        public ActionResult SaveSearch(ContactDetailsModel contactDetails)
        {
            try
            {
                // Save the current search.

                var currentSearch = MemberContext.CurrentSearch;
                if (currentSearch == null)
                    return JsonNotFound("current search");

                // Look for a current user.

                var registeredUser = CurrentRegisteredUser;
                if (registeredUser != null)
                {
                    var search = new JobAdSearch { Criteria = currentSearch.Criteria.Clone() };
                    _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(registeredUser.Id, search, DateTime.Now);
                }
                else
                {
                    var anonymousUser = CurrentAnonymousUser;
                    if (anonymousUser != null)
                    {
                        // The owner of the alert is the contact for the anonymous user.

                        var contact = _anonymousUsersCommand.CreateContact(anonymousUser, new ContactDetails { EmailAddress = contactDetails.EmailAddress, FirstName = contactDetails.FirstName, LastName = contactDetails.LastName });
                        var search = new JobAdSearch { Criteria = currentSearch.Criteria.Clone() };
                        _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(contact.Id, search, DateTime.Now);
                    }
                }

                return Json(new JsonResponseModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult DeleteSearch(Guid searchId)
        {
            try
            {
                _jobAdSearchAlertsCommand.DeleteJobAdSearch(CurrentMember.Id, searchId);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult DeleteSearchAlert(Guid searchId)
        {
            try
            {
                _jobAdSearchAlertsCommand.DeleteJobAdSearchAlert(CurrentMember.Id, searchId);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult RenameSearch(Guid searchId, string newName)
        {
            try
            {
                var search = _jobAdSearchesQuery.GetJobAdSearch(searchId);
                if (search == null)
                    return JsonNotFound("search");

                search.Name = newName;
                _jobAdSearchesCommand.UpdateJobAdSearch(CurrentMember.Id, search);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult CreateSearchFromCurrentSearch(string name, bool createAlert)
        {
            try
            {
                var currentSearch = MemberContext.CurrentSearch;
                if (currentSearch == null)
                    return JsonNotFound("current search");

                var search = new JobAdSearch
                {
                    Criteria = currentSearch.Criteria,
                    Name = name,
                };

                if (createAlert)
                    _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(CurrentMember.Id, search, DateTime.Now);
                else
                    _jobAdSearchesCommand.CreateJobAdSearch(CurrentMember.Id, search);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult CreateAlertFromSavedSearch(Guid id)
        {
            try
            {
                var search = _jobAdSearchesQuery.GetJobAdSearch(id);
                if (search == null)
                    return JsonNotFound("search");

                _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(CurrentMember.Id, search.Id, DateTime.Now);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult CreateAlertFromPreviousSearch(Guid id)
        {
            try
            {
                var execution = _jobAdSearchesQuery.GetJobAdSearchExecution(id);
                if (execution == null)
                    return JsonNotFound("search");

                var search = new JobAdSearch { Criteria = execution.Criteria.Clone() };
                _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(CurrentMember.Id, search, DateTime.Now);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }
    }
}

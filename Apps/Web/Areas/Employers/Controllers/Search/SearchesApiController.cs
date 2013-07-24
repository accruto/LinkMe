using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.Members;

namespace LinkMe.Web.Areas.Employers.Controllers.Search
{
    public class SearchesApiController
        : EmployersApiController
    {
        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand;
        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery;

        public SearchesApiController(IMemberSearchAlertsCommand memberSearchAlertsCommand, IMemberSearchAlertsQuery memberSearchAlertsQuery)
        {
            _memberSearchAlertsCommand = memberSearchAlertsCommand;
            _memberSearchAlertsQuery = memberSearchAlertsQuery;
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult SaveSearch(string name, bool isAlert)
        {
            try
            {
                var employer = CurrentEmployer;

                var context = EmployerContext;
                var currentSearch = context.CurrentSearch;
                if (currentSearch != null)
                {
                    // Check the name against the current saved search.

                    if (!string.IsNullOrEmpty(context.SavedSearchName) && context.SavedSearchName == name)
                    {
                        // Trying to do an update.

                        var search = _memberSearchAlertsQuery.GetMemberSearch(CurrentEmployer.Id, name);

                        // If not found then create it or update as needed.

                        if (search == null)
                        {
                            CreateSearch(employer, name, isAlert, currentSearch.Criteria);
                            context.SavedSearchName = name;
                        }
                        else
                        {
                            search.Criteria = currentSearch.Criteria.Clone();
                            _memberSearchAlertsCommand.UpdateMemberSearch(employer, search, new Tuple<AlertType, bool>(AlertType.Email, isAlert));
                        }
                    }
                    else
                    {
                        // Creating new search.

                        CreateSearch(employer, name, isAlert, currentSearch.Criteria);
                        context.SavedSearchName = name;
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

        private void CreateSearch(IUser employer, string name, bool isAlert, MemberSearchCriteria criteria)
        {
            // Create the search.

            var search = new MemberSearch
            {
                Name = name,
                OwnerId = employer.Id,
                Criteria = criteria.Clone(),
            };

            if (isAlert)
                _memberSearchAlertsCommand.CreateMemberSearchAlert(employer, search, AlertType.Email);
            else
                _memberSearchAlertsCommand.CreateMemberSearch(employer, search);
        }
    }
}
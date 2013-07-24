using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    public class FlagListsApiController
        : EmployersApiController
    {
        private readonly ICandidateListsCommand _candidateListsCommand;
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery;
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand;

        public FlagListsApiController(ICandidateListsCommand candidateListsCommand, ICandidateFlagListsQuery candidateFlagListsQuery, IExecuteMemberSearchCommand executeMemberSearchCommand)
        {
            _candidateListsCommand = candidateListsCommand;
            _candidateFlagListsQuery = candidateFlagListsQuery;
            _executeMemberSearchCommand = executeMemberSearchCommand;
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult AddCandidates(Guid[] candidateIds)
        {
            try
            {
                // Look for the flaglist.

                var employer = CurrentEmployer;
                var flagList = _candidateFlagListsQuery.GetFlagList(employer);

                // Add candidates.

                var count = _candidateListsCommand.AddCandidatesToFlagList(employer, flagList, candidateIds);
                return Json(new JsonCountModel { Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult RemoveCandidates(Guid[] candidateIds)
        {
            try
            {
                // Look for the flaglist.

                var employer = CurrentEmployer;
                var flagList = _candidateFlagListsQuery.GetFlagList(employer);

                // Remove candidates.

                var count = _candidateListsCommand.RemoveCandidatesFromFlagList(employer, flagList, candidateIds);
                return Json(new JsonCountModel { Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult RemoveAllCandidates()
        {
            try
            {
                // Look for the flaglist.

                var employer = CurrentEmployer;
                var flagList = _candidateFlagListsQuery.GetFlagList(employer);

                // Remove candidates.

                var count = _candidateListsCommand.RemoveAllCandidatesFromFlagList(employer, flagList);
                return Json(new JsonCountModel { Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult RemoveCurrentCandidates()
        {
            try
            {
                // Look for the flaglist.

                var employer = CurrentEmployer;

                int count;
                var currentSearch = EmployerContext.CurrentSearch;

                if (currentSearch != null)
                {
                    var flagList = _candidateFlagListsQuery.GetFlagList(employer);

                    // Search for candidates in the current search and flag list.

                    var criteria = (MemberSearchCriteria)((ICloneable)currentSearch.Criteria).Clone();
                    var execution = _executeMemberSearchCommand.SearchFlagged(employer, criteria, null);

                    // Remove those returned from the flag list.

                    count = _candidateListsCommand.RemoveCandidatesFromFlagList(employer, flagList, execution.Results.MemberIds);
                }
                else
                {
                    count = _candidateFlagListsQuery.GetFlaggedCount(employer);
                }

                return Json(new JsonCountModel { Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }
    }
}

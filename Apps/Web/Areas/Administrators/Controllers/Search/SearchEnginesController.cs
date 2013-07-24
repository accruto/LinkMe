using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Web.Areas.Administrators.Models.Search;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Search
{
    [EnsureAuthorized(UserType.Administrator)]
    public class SearchEnginesController
        : AdministratorsController
    {
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand;
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;

        public SearchEnginesController(IExecuteMemberSearchCommand executeMemberSearchCommand, IExecuteJobAdSearchCommand executeJobAdSearchCommand, ILoginCredentialsQuery loginCredentialsQuery)
        {
            _executeMemberSearchCommand = executeMemberSearchCommand;
            _executeJobAdSearchCommand = executeJobAdSearchCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
        }

        public ActionResult Search()
        {
            try
            {
                return View(CreateModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(new SearchEnginesModel { MemberSearch = new MemberSearchEngineModel(), JobAdSearch = new JobAdSearchEngineModel() });
        }

        [HttpPost]
        public ActionResult Search(string loginId, Guid? jobAdId)
        {
            try
            {
                var model = CreateModel();

                // Members.

                if (!string.IsNullOrEmpty(loginId))
                {
                    model.MemberSearch.LoginId = loginId;
                    model.MemberSearch.IsIndexed = IsSearchable(loginId);
                }

                // Job ads.

                if (jobAdId != null)
                {
                    model.JobAdSearch.JobAdId = jobAdId;
                    model.JobAdSearch.IsIndexed = IsSearchable(jobAdId.Value);
                }

                return View(model);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(new SearchEnginesModel { MemberSearch = new MemberSearchEngineModel(), JobAdSearch = new JobAdSearchEngineModel() });
        }

        private SearchEnginesModel CreateModel()
        {
            return new SearchEnginesModel
            {
                MemberSearch = new MemberSearchEngineModel
                {
                    TotalMembers = _executeMemberSearchCommand.Search(null, new MemberSearchCriteria(), new Range(0, 1)).Results.TotalMatches,
                },
                JobAdSearch = new JobAdSearchEngineModel
                {
                    TotalJobAds = _executeJobAdSearchCommand.Search(null, new JobAdSearchCriteria(), new Range(0, 1)).Results.TotalMatches,
                }
            };
        }

        private bool IsSearchable(Guid jobAdId)
        {
            return _executeJobAdSearchCommand.IsSearchable(jobAdId);
        }

        private bool IsSearchable(string loginId)
        {
            var userId = _loginCredentialsQuery.GetUserId(loginId) ?? GetUserId(loginId);
            if (userId != null)
                return _executeMemberSearchCommand.IsSearchable(userId.Value);
            return false;
        }

        private static Guid? GetUserId(string loginId)
        {
            try
            {
                return new Guid(loginId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

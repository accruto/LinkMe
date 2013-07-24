using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.Search
{
    public class SearchMobileController
        : JobAdSearchListController
    {
        private readonly IJobAdSearchesCommand _jobAdSearchesCommand;
        private readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand;

        public SearchMobileController(IJobAdFoldersCommand jobAdFoldersCommand, IExecuteJobAdSearchCommand executeJobAdSearchCommand, IJobAdSearchesCommand jobAdSearchesCommand, IJobAdSearchAlertsCommand jobAdSearchAlertsCommand, IJobAdsQuery jobAdsQuery, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IJobAdBlockListsQuery jobAdBlockListsQuery, IEmployersQuery employersQuery)
            : base(executeJobAdSearchCommand, jobAdsQuery, jobAdFlagListsQuery, memberJobAdViewsQuery, jobAdProcessingQuery, jobAdFoldersQuery, jobAdFoldersCommand, jobAdBlockListsQuery, employersQuery)
        {
            _jobAdSearchesCommand = jobAdSearchesCommand;
            _jobAdSearchAlertsCommand = jobAdSearchAlertsCommand;
        }

        [EnsureAuthorized(UserType.Member, Reason = "SaveSearch")]
        public ActionResult SaveSearch()
        {
            var search = MemberContext.CurrentSearch;
            return search == null
                ? View("NoCurrentSearch")
                : View(MemberContext.CurrentSearch.Criteria);
        }

        [EnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult SaveSearch(string name, bool createAlert)
        {
            try
            {
                var currentSearch = MemberContext.CurrentSearch;
                if (currentSearch == null)
                    return NotFound("current search");

                var search = new JobAdSearch
                {
                    Criteria = currentSearch.Criteria,
                    Name = name,
                };

                if (createAlert)
                    _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(CurrentMember.Id, search, DateTime.Now);
                else
                    _jobAdSearchesCommand.CreateJobAdSearch(CurrentMember.Id, search);

                return RedirectToReturnUrlWithConfirmation("SHOW SAVED NOTIFICATION");
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(MemberContext.CurrentSearch.Criteria);
        }
    }
}

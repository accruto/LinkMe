using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class FlagListsApiController
        : MembersApiController
    {
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand;
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery;
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;

        public FlagListsApiController(IMemberJobAdListsCommand memberJobAdListsCommand, IJobAdFlagListsQuery jobAdFlagListsQuery, IExecuteJobAdSearchCommand executeJobAdSearchCommand)
        {
            _memberJobAdListsCommand = memberJobAdListsCommand;
            _jobAdFlagListsQuery = jobAdFlagListsQuery;
            _executeJobAdSearchCommand = executeJobAdSearchCommand;
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult AddJobAds(Guid[] jobAdIds)
        {
            try
            {
                // Look for the flaglist.

                var member = CurrentMember;
                var flagList = _jobAdFlagListsQuery.GetFlagList(member);

                // Add jobads.

                var count = _memberJobAdListsCommand.AddJobAdsToFlagList(member, flagList, jobAdIds);
                return Json(new JsonCountModel { Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult RemoveJobAds(Guid[] jobAdIds)
        {
            try
            {
                // Look for the flaglist.

                var member = CurrentMember;
                var flagList = _jobAdFlagListsQuery.GetFlagList(member);

                // Remove jobads.

                var count = _memberJobAdListsCommand.RemoveJobAdsFromFlagList(member, flagList, jobAdIds);
                return Json(new JsonCountModel { Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult RemoveAllJobAds()
        {
            try
            {
                // Look for the flaglist.

                var member = CurrentMember;
                var flagList = _jobAdFlagListsQuery.GetFlagList(member);

                // Remove jobAds.

                var count = _memberJobAdListsCommand.RemoveAllJobAdsFromFlagList(member, flagList);
                return Json(new JsonCountModel { Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult RemoveCurrentJobAds()
        {
            try
            {
                // Look for the flaglist.

                var member = CurrentMember;

                int count;
                var currentSearch = MemberContext.CurrentSearch;

                if (currentSearch != null)
                {
                    var flagList = _jobAdFlagListsQuery.GetFlagList(member);

                    // Search for jobads in the current search and flag list.

                    var criteria = (JobAdSearchCriteria)((ICloneable)currentSearch.Criteria).Clone();
                    var execution = _executeJobAdSearchCommand.SearchFlagged(member, criteria, null);

                    // Remove those returned from the flag list.

                    count = _memberJobAdListsCommand.RemoveJobAdsFromFlagList(member, flagList, execution.Results.JobAdIds);
                }
                else
                {
                    count = _jobAdFlagListsQuery.GetFlaggedCount(member);
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

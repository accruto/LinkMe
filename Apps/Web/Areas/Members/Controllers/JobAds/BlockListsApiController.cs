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

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class BlockListsApiController
        : MembersApiController
    {
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand;
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery;

        public BlockListsApiController(IMemberJobAdListsCommand memberJobAdListsCommand, IJobAdBlockListsQuery jobAdBlockListsQuery)
        {
            _memberJobAdListsCommand = memberJobAdListsCommand;
            _jobAdBlockListsQuery = jobAdBlockListsQuery;
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult BlockJobAds(Guid[] jobAdIds)
        {
            try
            {
                // Look for the blocklist.

                var member = CurrentMember;
                var blockList = _jobAdBlockListsQuery.GetBlockList(member);
                if (blockList == null)
                    return JsonNotFound("blocklist");

                // Add jobads.

                var count = _memberJobAdListsCommand.AddJobAdsToBlockList(member, blockList, jobAdIds);
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
                // Look for the blocklist.

                var member = CurrentMember;
                var blockList = _jobAdBlockListsQuery.GetBlockList(member);
                if (blockList == null)
                    return JsonNotFound("blocklist");

                // Remove jobads.

                var count = _memberJobAdListsCommand.RemoveJobAdsFromBlockList(member, blockList, jobAdIds);
                return Json(new JsonCountModel { Count = count});
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult UnblockJobAds(Guid[] jobAdIds)
        {
            try
            {
                // Look for the blockList.

                var member = CurrentMember;
                var blockList = _jobAdBlockListsQuery.GetBlockList(member);

                // Remove jobads.

                var count = _memberJobAdListsCommand.RemoveJobAdsFromBlockList(member, blockList, jobAdIds);
                return Json(new JsonCountModel { Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult UnblockAllJobAds()
        {
            try
            {
                // Look for the blockList.

                var member = CurrentMember;
                var blockList = _jobAdBlockListsQuery.GetBlockList(member);

                // Remove jobads.

                var count = _memberJobAdListsCommand.RemoveAllJobAdsFromBlockList(member, blockList);
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

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
    public class FoldersApiController
        : MembersApiController
    {
        private readonly IJobAdFoldersCommand _jobAdFoldersCommand;
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery;
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand;

        public FoldersApiController(IJobAdFoldersCommand jobAdFoldersCommand, IJobAdFoldersQuery jobAdFoldersQuery, IMemberJobAdListsCommand memberJobAdListsCommand)
        {
            _jobAdFoldersCommand = jobAdFoldersCommand;
            _jobAdFoldersQuery = jobAdFoldersQuery;
            _memberJobAdListsCommand = memberJobAdListsCommand;
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult AddJobAds(Guid folderId, Guid[] jobAdIds)
        {
            try
            {
                // Look for the folder.

                var member = CurrentMember;
                var folder = _jobAdFoldersQuery.GetFolder(member, folderId);
                if (folder == null)
                    return JsonNotFound("folder");

                // Add jobads.

                var count = _memberJobAdListsCommand.AddJobAdsToFolder(member, folder, jobAdIds);
                return Json(new JsonListCountModel { Id = folderId, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult AddMobileJobAds(Guid[] jobAdIds)
        {
            try
            {
                // Look for the folder.

                var member = CurrentMember;
                var folder = _jobAdFoldersQuery.GetMobileFolder(member);
                if (folder == null)
                    return JsonNotFound("folder");

                // Add jobads.

                var count = _memberJobAdListsCommand.AddJobAdsToFolder(member, folder, jobAdIds);
                return Json(new JsonListCountModel { Id = folder.Id, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult RemoveAllJobAds(Guid folderId)
        {
            try
            {
                // Look for the folder.

                var member = CurrentMember;
                var folder = _jobAdFoldersQuery.GetFolder(member, folderId);
                if (folder == null)
                    return JsonNotFound("folder");

                var count = _memberJobAdListsCommand.RemoveAllJobAdsFromFolder(member, folder);
                return Json(new JsonListCountModel { Id = folderId, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult RemoveJobAds(Guid folderId, Guid[] jobAdIds)
        {
            try
            {
                // Look for the folder.

                var member = CurrentMember;
                var folder = _jobAdFoldersQuery.GetFolder(member, folderId);
                if (folder == null)
                    return JsonNotFound("folder");

                // Remove jobads.

                var count = _memberJobAdListsCommand.RemoveJobAdsFromFolder(member, folder, jobAdIds);
                return Json(new JsonListCountModel { Id = folderId, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult RemoveMobileJobAds(Guid[] jobAdIds)
        {
            try
            {
                // Look for the folder.

                var member = CurrentMember;
                var folder = _jobAdFoldersQuery.GetMobileFolder(member);
                if (folder == null)
                    return JsonNotFound("folder");

                // Remove jobads.

                var count = _memberJobAdListsCommand.RemoveJobAdsFromFolder(member, folder, jobAdIds);
                return Json(new JsonListCountModel { Id = folder.Id, Count = count });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [ApiEnsureAuthorized(UserType.Member), HttpPost]
        public ActionResult RenameFolder(Guid folderId, string name)
        {
            try
            {
                // Rename the folder.

                var member = CurrentMember;
                var folder = _jobAdFoldersQuery.GetFolder(member, folderId);
                if (folder == null)
                    return JsonNotFound("folder");

                _jobAdFoldersCommand.RenameFolder(member, folder, name);

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

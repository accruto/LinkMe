using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.JobAds;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    [EnsureHttps, EnsureAuthorized(UserType.Member)]
    public class FoldersWebController
        : FoldersController
    {
        public FoldersWebController(IExecuteJobAdSortCommand executeJobAdSortCommand, IJobAdFoldersCommand jobAdFoldersCommand, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdProcessingQuery jobAdProcessingQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery, IJobAdsQuery jobAdsQuery, IJobAdBlockListsQuery jobAdBlockListsQuery, IEmployersQuery employersQuery, IIndustriesQuery industriesQuery)
            : base(executeJobAdSortCommand, jobAdFoldersCommand, jobAdFoldersQuery, jobAdFlagListsQuery, memberJobAdViewsQuery, jobAdProcessingQuery, communitiesQuery, verticalsQuery, jobAdsQuery, jobAdBlockListsQuery, employersQuery, industriesQuery)
        {
        }

        public ActionResult Folders()
        {
            var member = CurrentMember;

            // Get the folders and their counts.

            var folderData = new Dictionary<Guid, FolderDataModel>();

            // Flag list.

            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            var count = _jobAdFlagListsQuery.GetFlaggedCount(member);
            folderData[flagList.Id] = new FolderDataModel { Count = count, CanRename = false };

            // Folders.

            var folders = _jobAdFoldersQuery.GetFolders(member);
            var counts = _jobAdFoldersQuery.GetInFolderCounts(member);
            var lastUsedTimes = _jobAdFoldersQuery.GetLastUsedTimes(member);

            foreach (var folder in folders)
            {
                folderData[folder.Id] = new FolderDataModel
                {
                    Count = GetCount(folder.Id, counts),
                    CanRename = _jobAdFoldersCommand.CanRenameFolder(member, folder),
                };
            }

            var comparer = new FolderComparer(lastUsedTimes);

            return View(new FoldersModel
            {
                FlagList = flagList,
                MobileFolder = folders.Single(f => f.FolderType == FolderType.Mobile),
                PrivateFolders = folders.Where(f => f.FolderType == FolderType.Private).OrderBy(f => f, comparer).ToList(),
                FolderData = folderData,
            });
        }

        public ActionResult Folder(Guid folderId, JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var model = FolderResults(folderId, sortCriteria, presentation);
            if (model == null)
                return NotFound("folder", "id", folderId);

            MemberContext.CurrentJobAds = new FolderNavigation(folderId, presentation);
            return View(model);
        }

        public ActionResult PartialFolder(Guid folderId, JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var model = FolderResults(folderId, sortCriteria, presentation);
            if (model == null)
                return NotFound("folder", "id", folderId);

            MemberContext.CurrentJobAds = new FolderNavigation(folderId, presentation);
            return PartialView("JobAdList", model);
        }
    }
}

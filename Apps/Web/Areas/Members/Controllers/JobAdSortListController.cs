using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Search;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Controllers.JobAds;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.JobAds;

namespace LinkMe.Web.Areas.Members.Controllers
{
    public abstract class JobAdSortListController
        : JobAdListController
    {
        protected readonly IExecuteJobAdSortCommand _executeJobAdSortCommand;
        protected readonly IJobAdFoldersQuery _jobAdFoldersQuery;
        protected readonly IJobAdFoldersCommand _jobAdFoldersCommand;
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery;
        private readonly IIndustriesQuery _industriesQuery;

        protected JobAdSortListController(IExecuteJobAdSortCommand executeJobAdSortCommand, IJobAdsQuery jobAdsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFoldersCommand jobAdFoldersCommand, IJobAdBlockListsQuery jobAdBlockListsQuery, IIndustriesQuery industriesQuery, IEmployersQuery employersQuery)
            : base(jobAdsQuery, memberJobAdViewsQuery, jobAdFlagListsQuery, jobAdProcessingQuery, employersQuery)
        {
            _executeJobAdSortCommand = executeJobAdSortCommand;
            _jobAdFoldersQuery = jobAdFoldersQuery;
            _jobAdFoldersCommand = jobAdFoldersCommand;
            _jobAdBlockListsQuery = jobAdBlockListsQuery;
            _industriesQuery = industriesQuery;
        }

        protected TListModel Sort<TListModel>(IMember member, JobAdSearchSortCriteria criteria, JobAdsPresentationModel presentation, JobAdListType listType, Func<IMember, JobAdSearchSortCriteria, Range, JobAdSortExecution> sort)
            where TListModel : JobAdSortListModel, new()
        {
            presentation = PreparePresentationModel(presentation);

            // Search.

            var execution = sort(member, criteria, presentation.Pagination.ToRange());

            var model = new TListModel
            {
                ListType = listType,
                Criteria = execution.Criteria,
                Presentation = presentation,
                Results = new JobAdListResultsModel
                {
                    TotalJobAds = execution.Results.TotalMatches,
                    JobAdIds = execution.Results.JobAdIds,
                    JobAds = GetMemberJobAdViews(member, execution.Results.JobAdIds),
                },
                Industries = _industriesQuery.GetIndustries(),
            };

            return model;
        }

        protected FoldersModel GetFoldersModel()
        {
            var member = CurrentMember;

            if (member == null) return null;

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

            return new FoldersModel
            {
                FlagList = flagList,
                MobileFolder = folders.Single(f => f.FolderType == FolderType.Mobile),
                PrivateFolders = folders.Where(f => f.FolderType == FolderType.Private).OrderBy(f => f, comparer).ToList(),
                FolderData = folderData,
            };
        }

        protected BlockListsModel GetBlockListsModel()
        {
            var member = CurrentMember;
            if (member == null)
                return null;

            var blockList = _jobAdBlockListsQuery.GetBlockList(member);
            if (blockList == null)
                return null;

            var count = _jobAdBlockListsQuery.GetBlockedCount(member);
            return new BlockListsModel
            {
                BlockList = blockList,
                BlockListCount = new Tuple<Guid, int>(blockList.Id, count),
            };
        }

        protected static int GetCount(Guid folderId, IDictionary<Guid, int> counts)
        {
            int count;
            return counts.TryGetValue(folderId, out count) ? count : 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Presentation.Domain.Search;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Query.Search;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Controllers.JobAds;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.JobAds;
using LinkMe.Web.Areas.Members.Models.Search;

namespace LinkMe.Web.Areas.Members.Controllers
{
    public abstract class JobAdSearchListController
        : JobAdListController
    {
        protected readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;

        private readonly IJobAdFoldersCommand _jobAdFoldersCommand;

        private readonly IJobAdFoldersQuery _jobAdFoldersQuery;
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery;

        protected static readonly RecencyModel[] Recencies = (from d in Reference.RecencyDays select new RecencyModel { Days = d, Description = new TimeSpan(d, 0, 0, 0).GetRecencyDisplayText() }).ToArray();

        protected JobAdSearchListController(IExecuteJobAdSearchCommand executeJobAdSearchCommand, IJobAdsQuery jobAdsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFoldersCommand jobAdFoldersCommand, IJobAdBlockListsQuery jobAdBlockListsQuery, IEmployersQuery employersQuery)
            : base(jobAdsQuery, memberJobAdViewsQuery, jobAdFlagListsQuery, jobAdProcessingQuery, employersQuery)
        {
            _jobAdFoldersCommand = jobAdFoldersCommand;
            _executeJobAdSearchCommand = executeJobAdSearchCommand;

            _jobAdFoldersQuery = jobAdFoldersQuery;
            _jobAdBlockListsQuery = jobAdBlockListsQuery;
        }

        protected TListModel Search<TListModel>(IMember member, JobAdSearchCriteria criteria, JobAdsPresentationModel presentation, JobAdListType listType)
            where TListModel : JobAdSearchListModel, new()
        {
            presentation = PreparePresentationModel(presentation);

            // Search.

            var execution = _executeJobAdSearchCommand.Search(member, criteria, presentation.Pagination.ToRange());

            var model = new TListModel
            {
                Criteria = execution.Criteria,
                Presentation = presentation,
                ListType = listType,
                Results = new JobAdListResultsModel
                {
                    TotalJobAds = execution.Results.TotalMatches,
                    IndustryHits = execution.Results.IndustryHits.ToDictionary(i => i.Key.ToString(), i => i.Value),
                    JobTypeHits = GetEnumHitsDictionary(execution.Results.JobTypeHits, JobTypes.All, JobTypes.None),
                    JobAdIds = execution.Results.JobAdIds,
                    JobAds = GetMemberJobAdViews(member, execution.Results.JobAdIds),
                },
                Folders = GetFoldersModel(),
                BlockLists = GetBlockListsModel(),
            };
                
            return model;
        }

        protected TListModel SuggestedJobs<TListModel>(IMember member, JobAdsPresentationModel presentation)
            where TListModel : JobAdSearchListModel, new()
        {
            presentation = PreparePresentationModel(presentation);

            // Search.

            var range = presentation.Pagination.ToRange();
            var execution = _executeJobAdSearchCommand.SearchSuggested(member, null, presentation.Pagination.ToRange());

            var model = new TListModel
            {
                Criteria = execution.Criteria,
                Presentation = presentation,
                ListType = JobAdListType.SuggestedJobs,
                Results = new JobAdListResultsModel
                {
                    TotalJobAds = execution.Results.TotalMatches,
                    JobAdIds = execution.Results.JobAdIds.SelectRange(range).ToList(),
                    JobAds = GetMemberJobAdViews(member, execution.Results.JobAdIds.SelectRange(range)),
                },
                Folders = GetFoldersModel(),
                BlockLists = GetBlockListsModel(),
            };

            return model;
        }

        protected TListModel SimilarJobs<TListModel>(IMember member, Guid jobAdId, JobAdsPresentationModel presentation)
            where TListModel : JobAdSearchListModel, new()
        {
            presentation = PreparePresentationModel(presentation);

            // Search.

            var range = presentation.Pagination.ToRange();
            var results = _executeJobAdSearchCommand.SearchSimilar(member, jobAdId, presentation.Pagination.ToRange());

            return new TListModel
            {
                Criteria = null,
                Presentation = presentation,
                ListType = JobAdListType.SimilarJobs,
                Results = new JobAdListResultsModel
                {
                    TotalJobAds = results.Results.TotalMatches,
                    JobAdIds = results.Results.JobAdIds.SelectRange(range).ToList(),
                    JobAds = GetMemberJobAdViews(member, results.Results.JobAdIds.SelectRange(range)),
                },
                Folders = GetFoldersModel(),
                BlockLists = GetBlockListsModel(),
            };
        }

        private static IDictionary<string, int> GetEnumHitsDictionary<T>(IEnumerable<KeyValuePair<T, int>> hitList, params T[] ignore)
        {
            // The list may not have counts for all values so ensure they are set to 0.

            var hits = new Dictionary<string, int>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                if (!ignore.Contains((T) value))
                    hits[value.ToString()] = 0;
            }

            foreach (var hit in hitList)
            {
                if (!ignore.Contains(hit.Key))
                    hits[hit.Key.ToString()] = hit.Value;
            }

            return hits;
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

            if (member == null) return null;

            var blockList = _jobAdBlockListsQuery.GetBlockList(member);
            if (blockList == null) return null;

            var blockListEntriesCount = _jobAdBlockListsQuery.GetBlockedCount(member);

            return new BlockListsModel
            {
                BlockList = blockList,
                BlockListCount = new Tuple<Guid, int> (blockList.Id, blockListEntriesCount),
            };
        }

        private static int GetCount(Guid folderId, IDictionary<Guid, int> counts)
        {
            int count;
            return counts.TryGetValue(folderId, out count) ? count : 0;
        }
    }
}

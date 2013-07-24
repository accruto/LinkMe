using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using LinkMe.Web.Context;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    [EnsureHttps, EnsureAuthorized(UserType.Employer)]
    public class FoldersController
        : CandidateListController
    {
        private readonly ICandidateFoldersCommand _candidateFoldersCommand;
        private readonly ICandidateFoldersQuery _candidateFoldersQuery;
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;

        private static readonly MemberSortOrder[] SortOrders = new[] { MemberSortOrder.DateUpdated, MemberSortOrder.Flagged, MemberSortOrder.FirstName }.OrderBy(o => o.ToString()).ToArray();

        public FoldersController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery, ICandidateFoldersCommand candidateFoldersCommand, ICandidateFoldersQuery candidateFoldersQuery, ICandidateFlagListsQuery candidateFlagListsQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery)
            : base(executeMemberSearchCommand, employerMemberViewsQuery, memberStatusQuery)
        {
            _candidateFoldersCommand = candidateFoldersCommand;
            _candidateFoldersQuery = candidateFoldersQuery;
            _candidateFlagListsQuery = candidateFlagListsQuery;
            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;
        }

        public ActionResult Folders()
        {
            var employer = CurrentEmployer;

            // Get the folders and their counts.

            var folderData = new Dictionary<Guid, FolderDataModel>();

            // Flag list.

            var flagList = _candidateFlagListsQuery.GetFlagList(employer);
            var count = _candidateFlagListsQuery.GetFlaggedCount(employer);
            folderData[flagList.Id] = new FolderDataModel { Count = count, CanDelete = false, CanRename = false };

            // Folders.

            var folders = _candidateFoldersQuery.GetFolders(employer);
            var counts = _candidateFoldersQuery.GetInFolderCounts(employer);
            var lastUsedTimes = _candidateFoldersQuery.GetLastUsedTimes(employer);

            foreach (var folder in folders)
            {
                folderData[folder.Id] = new FolderDataModel
                {
                    Count = GetCount(folder.Id, counts),
                    CanRename = _candidateFoldersCommand.CanRenameFolder(employer, folder),
                    CanDelete = _candidateFoldersCommand.CanDeleteFolder(employer, folder),
                };
            }

            var comparer = new FolderComparer(lastUsedTimes);

            return View(new FoldersModel
            {
                FlagList = flagList,
                ShortlistFolder = (from f in folders where f.FolderType == FolderType.Shortlist select f).SingleOrDefault(),
                PrivateFolders = (from f in folders where f.FolderType == FolderType.Private || f.FolderType == FolderType.Mobile select f).OrderBy(f => f, comparer).ToList(),
                SharedFolders = (from f in folders where f.FolderType == FolderType.Shared select f).OrderBy(f => f, comparer).ToList(),
                FolderData = folderData,
            });
        }

        public ActionResult Folder(Guid folderId, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            var model = FolderResults(folderId, sortCriteria, presentation);
            if (model == null)
                return NotFound("folder", "id", folderId);

            EmployerContext.CurrentCandidates = new FolderNavigation(folderId, presentation);
            return View(model);
        }

        public ActionResult PartialFolder(Guid folderId, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            var model = FolderResults(folderId, sortCriteria, presentation);
            if (model == null)
                return NotFound("folder", "id", folderId);

            EmployerContext.CurrentCandidates = new FolderNavigation(folderId, presentation);
            return PartialView("CandidateList", model);
        }

        private FolderListModel FolderResults(Guid folderId, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            var employer = CurrentEmployer;
            var folder = _candidateFoldersQuery.GetFolder(employer, folderId);
            if (folder == null)
                return null;

            // Do a search to get the candidates in this folder.

            var model = Search(employer, folderId, sortCriteria, presentation);
            model.Folder = folder;
            model.FolderData = new FolderDataModel
            {
                Count = model.Results.TotalCandidates,
                CanDelete = _candidateFoldersCommand.CanDeleteFolder(employer, folder),
                CanRename = _candidateFoldersCommand.CanRenameFolder(employer, folder),
            };
            model.SortOrders = SortOrders;
            model.Communities = _communitiesQuery.GetCommunities().ToDictionary(c => c.Id, c => c);
            model.Verticals = _verticalsQuery.GetVerticals().ToDictionary(v => v.Id, v => v);
            model.CurrentSearch = EmployerContext.CurrentSearch;

            return model;
        }

        private FolderListModel Search(IEmployer employer, Guid folderId, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            return Search<FolderListModel>(employer, presentation, (e, r) => _executeMemberSearchCommand.SearchFolder(e, folderId, sortCriteria, r));
        }

        private static int GetCount(Guid folderId, IDictionary<Guid, int> counts)
        {
            int count;
            return counts.TryGetValue(folderId, out count) ? count : 0;
        }
    }
}

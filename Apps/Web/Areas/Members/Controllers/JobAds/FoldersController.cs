using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.JobAds;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public abstract class FoldersController
        : JobAdSortListController
    {
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;

        private static readonly JobAdSortOrder[] SortOrders = new[] { JobAdSortOrder.CreatedTime, JobAdSortOrder.JobType, JobAdSortOrder.Salary, JobAdSortOrder.Flagged }.OrderBy(o => o.ToString()).ToArray();
        private const JobAdSortOrder DefaultSortOrder = JobAdSortCriteria.DefaultSortOrder;

        protected FoldersController(IExecuteJobAdSortCommand executeJobAdSortCommand, IJobAdFoldersCommand jobAdFoldersCommand, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdProcessingQuery jobAdProcessingQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery, IJobAdsQuery jobAdsQuery, IJobAdBlockListsQuery jobAdBlockListsQuery, IEmployersQuery employersQuery, IIndustriesQuery industriesQuery)
            : base(executeJobAdSortCommand, jobAdsQuery, jobAdFlagListsQuery, memberJobAdViewsQuery, jobAdProcessingQuery, jobAdFoldersQuery, jobAdFoldersCommand, jobAdBlockListsQuery, industriesQuery, employersQuery)
        {
            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;
        }

        protected FolderListModel FolderResults(Guid folderId, JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var member = CurrentMember;
            var folder = _jobAdFoldersQuery.GetFolder(member, folderId);
            if (folder == null)
                return null;

            // Do a search to get the jobads in this folder.

            var model = Sort(member, folderId, sortCriteria, presentation);
            model.Folder = folder;
            model.Folders = GetFoldersModel();
            model.BlockLists = GetBlockListsModel();
            model.SortOrders = SortOrders;
            model.Communities = _communitiesQuery.GetCommunities().ToDictionary(c => c.Id, c => c);
            model.Verticals = _verticalsQuery.GetVerticals().ToDictionary(v => v.Id, v => v);
            return model;
        }

        private FolderListModel Sort(IMember member, Guid folderId, JobAdSearchSortCriteria criteria, JobAdsPresentationModel presentation)
        {
            return Sort<FolderListModel>(
                member,
                criteria ?? new JobAdSearchSortCriteria { SortOrder = DefaultSortOrder },
                presentation,
                JobAdListType.Folder,
                (m, c, r) => _executeJobAdSortCommand.SortFolder(m, folderId, c, r));
        }
    }
}

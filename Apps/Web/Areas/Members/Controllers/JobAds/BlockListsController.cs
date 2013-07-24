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
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.JobAds;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    [EnsureHttps, EnsureAuthorized(UserType.Member)]
    public class BlockListsController
        : JobAdSortListController
    {
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;

        private static readonly JobAdSortOrder[] SortOrders = new[] { JobAdSortOrder.CreatedTime, JobAdSortOrder.JobType, JobAdSortOrder.Salary }.OrderBy(o => o.ToString()).ToArray();
        private const JobAdSortOrder DefaultSortOrder = JobAdSortCriteria.DefaultSortOrder;

        public BlockListsController(IExecuteJobAdSortCommand executeJobAdSortCommand, IJobAdsQuery jobAdsQuery, IJobAdBlockListsQuery jobAdBlockListsQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IJobAdFoldersCommand jobAdFoldersCommand, IJobAdFoldersQuery jobAdFoldersQuery, IEmployersQuery employersQuery, IIndustriesQuery industriesQuery)
            : base(executeJobAdSortCommand, jobAdsQuery, jobAdFlagListsQuery, memberJobAdViewsQuery, jobAdProcessingQuery, jobAdFoldersQuery, jobAdFoldersCommand, jobAdBlockListsQuery, industriesQuery, employersQuery)
        {
            _jobAdBlockListsQuery = jobAdBlockListsQuery;
            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;
        }

        public ActionResult BlockList(JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var model = BlockListResults(sortCriteria, presentation);
            if (model == null)
                return NotFound("blocklist");

            MemberContext.CurrentJobAds = new BlockListNavigation(presentation);
            return View("BlockList", model);
        }

        public ActionResult PartialBlockList(JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var model = BlockListResults(sortCriteria, presentation);
            if (model == null)
                return NotFound("blocklist");

            MemberContext.CurrentJobAds = new BlockListNavigation(presentation);
            return PartialView("JobAdList", model);
        }

        private BlockListListModel BlockListResults(JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var member = CurrentMember;
            var blockList = _jobAdBlockListsQuery.GetBlockList(member);

            if (blockList == null)
                return null;

            // Do a search to get the jobads in this blockList.

            var model = Sort(member, sortCriteria, presentation);
            model.BlockList = blockList;
            model.Folders = GetFoldersModel();
            model.BlockLists = GetBlockListsModel();
            model.SortOrders = SortOrders;
            model.Communities = _communitiesQuery.GetCommunities().ToDictionary(c => c.Id, c => c);
            model.Verticals = _verticalsQuery.GetVerticals().ToDictionary(v => v.Id, v => v);
            return model;
        }

        private BlockListListModel Sort(IMember member, JobAdSearchSortCriteria criteria, JobAdsPresentationModel presentation)
        {
            return Sort<BlockListListModel>(member, criteria ?? new JobAdSearchSortCriteria { SortOrder = DefaultSortOrder }, presentation, JobAdListType.BlockList, (m, c, r) => _executeJobAdSortCommand.SortBlocked(m, c, r));
        }
    }
}

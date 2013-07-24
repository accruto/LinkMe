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
    public class FlagListsController
        : JobAdSortListController
    {
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;

        private static readonly JobAdSortOrder[] SortOrders = new[] { JobAdSortOrder.CreatedTime, JobAdSortOrder.JobType, JobAdSortOrder.Salary }.OrderBy(o => o.ToString()).ToArray();
        private const JobAdSortOrder DefaultSortOrder = JobAdSortCriteria.DefaultSortOrder;

        public FlagListsController(IExecuteJobAdSortCommand executeJobAdSortCommand, IJobAdsQuery jobAdsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdProcessingQuery jobAdProcessingQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFoldersCommand jobAdFoldersCommand, IJobAdBlockListsQuery jobAdBlockListsQuery, IEmployersQuery employersQuery, IIndustriesQuery industriesQuery)
            : base(executeJobAdSortCommand, jobAdsQuery, jobAdFlagListsQuery, memberJobAdViewsQuery, jobAdProcessingQuery, jobAdFoldersQuery, jobAdFoldersCommand, jobAdBlockListsQuery, industriesQuery, employersQuery)
        {
            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;
        }

        public ActionResult FlagList(JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var model = FlagListResults(sortCriteria, presentation);
            if (model == null)
                return NotFound("flaglist");

            MemberContext.CurrentJobAds = new FlagListNavigation(presentation);
            return View(model);
        }

        public ActionResult PartialFlagList(JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var model = FlagListResults(sortCriteria, presentation);
            if (model == null)
                return NotFound("flaglist");

            MemberContext.CurrentJobAds = new FlagListNavigation(presentation);
            return PartialView("JobAdList", model);
        }

        private FlagListListModel FlagListResults(JobAdSearchSortCriteria sortCriteria, JobAdsPresentationModel presentation)
        {
            var member = CurrentMember;
            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            if (flagList == null)
                return null;

            // Do a search to get the jobads in this flagList.

            var model = Sort(member, sortCriteria, presentation);
            model.FlagList = flagList;
            model.Folders = GetFoldersModel();
            model.BlockLists = GetBlockListsModel();
            model.SortOrders = SortOrders;
            model.Communities = _communitiesQuery.GetCommunities().ToDictionary(c => c.Id, c => c);
            model.Verticals = _verticalsQuery.GetVerticals().ToDictionary(v => v.Id, v => v);
            return model;
        }

        private FlagListListModel Sort(IMember member, JobAdSearchSortCriteria criteria, JobAdsPresentationModel presentation)
        {
            return Sort<FlagListListModel>(member, criteria ?? new JobAdSearchSortCriteria { SortOrder = DefaultSortOrder }, presentation, JobAdListType.FlagList, (m, c, r) => _executeJobAdSortCommand.SortFlagged(m, c, r));
        }
    }
}

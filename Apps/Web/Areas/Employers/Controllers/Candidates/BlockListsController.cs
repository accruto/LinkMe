using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Users.Employers.Candidates;
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
    public class BlockListsController
        : CandidateListController
    {
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;

        private static readonly MemberSortOrder[] SortOrders = new[] { MemberSortOrder.DateUpdated, MemberSortOrder.FirstName }.OrderBy(o => o.ToString()).ToArray();

        public BlockListsController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery, ICandidateBlockListsQuery candidateBlockListsQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery)
            : base(executeMemberSearchCommand, employerMemberViewsQuery, memberStatusQuery)
        {
            _candidateBlockListsQuery = candidateBlockListsQuery;
            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;
        }

        public ActionResult TemporaryBlockList(MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            return BlockList(BlockListType.Temporary, sortCriteria, presentation);
        }

        public ActionResult PermanentBlockList(MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            return BlockList(BlockListType.Permanent, sortCriteria, presentation);
        }

        public ActionResult TemporaryPartialBlockList(MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            return PartialBlockList(BlockListType.Temporary, sortCriteria, presentation);
        }

        public ActionResult PermanentPartialBlockList(MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            return PartialBlockList(BlockListType.Permanent, sortCriteria, presentation);
        }

        private ActionResult BlockList(BlockListType blockListType, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            var model = BlockListResults(blockListType, sortCriteria, presentation);
            if (model == null)
                return NotFound("blocklist", "type", blockListType);

            EmployerContext.CurrentCandidates = new BlockListNavigation(blockListType, presentation);
            return View("BlockList", model);
        }

        private ActionResult PartialBlockList(BlockListType blockListType, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            var model = BlockListResults(blockListType, sortCriteria, presentation);
            if (model == null)
                return NotFound("blocklist", "type", blockListType);

            EmployerContext.CurrentCandidates = new BlockListNavigation(blockListType, presentation);
            return PartialView("CandidateList", model);
        }

        private BlockListListModel BlockListResults(BlockListType blockListType, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            var employer = CurrentEmployer;
            var blockList = blockListType == BlockListType.Temporary
                ? _candidateBlockListsQuery.GetTemporaryBlockList(employer)
                : _candidateBlockListsQuery.GetPermanentBlockList(employer);

            if (blockList == null)
                return null;

            // Do a search to get the candidates in this blockList.

            var model = Search(employer, blockList.Id, sortCriteria, presentation);
            model.BlockList = blockList;
            model.SortOrders = SortOrders;
            model.Communities = _communitiesQuery.GetCommunities().ToDictionary(c => c.Id, c => c);
            model.Verticals = _verticalsQuery.GetVerticals().ToDictionary(v => v.Id, v => v);
            model.CurrentSearch = EmployerContext.CurrentSearch;
            return model;
        }

        private BlockListListModel Search(IEmployer employer, Guid blockListId, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            return Search<BlockListListModel>(employer, presentation, (e, r) => _executeMemberSearchCommand.SearchBlockList(e, blockListId, sortCriteria, r));
        }
    }
}

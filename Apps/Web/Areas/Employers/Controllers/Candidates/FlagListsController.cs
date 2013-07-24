using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
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
    public class FlagListsController
        : CandidateListController
    {
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;

        private static readonly MemberSortOrder[] SortOrders = new[] { MemberSortOrder.DateUpdated, MemberSortOrder.FirstName }.OrderBy(o => o.ToString()).ToArray();

        public FlagListsController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery, ICandidateFlagListsQuery candidateFlagListsQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery)
            : base(executeMemberSearchCommand, employerMemberViewsQuery, memberStatusQuery)
        {
            _candidateFlagListsQuery = candidateFlagListsQuery;
            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;
        }

        public ActionResult FlagList(MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            var model = FlagListResults(sortCriteria, presentation);
            if (model == null)
                return NotFound("flaglist");

            EmployerContext.CurrentCandidates = new FlagListNavigation(presentation);
            return View(model);
        }

        public ActionResult PartialFlagList(MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            var model = FlagListResults(sortCriteria, presentation);
            if (model == null)
                return NotFound("flaglist");

            EmployerContext.CurrentCandidates = new FlagListNavigation(presentation);
            return PartialView("CandidateList", model);
        }

        private FlagListListModel FlagListResults(MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            var employer = CurrentEmployer;
            var flagList = _candidateFlagListsQuery.GetFlagList(employer);
            if (flagList == null)
                return null;

            // Do a search to get the candidates in this flagList.

            var model = Search(employer, sortCriteria, presentation);
            model.FlagList = flagList;
            model.SortOrders = SortOrders;
            model.Communities = _communitiesQuery.GetCommunities().ToDictionary(c => c.Id, c => c);
            model.Verticals = _verticalsQuery.GetVerticals().ToDictionary(v => v.Id, v => v);
            model.CurrentSearch = EmployerContext.CurrentSearch;
            return model;
        }

        private FlagListListModel Search(IEmployer employer, MemberSearchSortCriteria sortCriteria, CandidatesPresentationModel presentation)
        {
            return Search<FlagListListModel>(employer, presentation, (e, r) => _executeMemberSearchCommand.SearchFlagged(e, sortCriteria, r));
        }
    }
}

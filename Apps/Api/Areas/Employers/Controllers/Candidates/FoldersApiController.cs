using System;
using System.Web.Mvc;
using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;

namespace LinkMe.Apps.Api.Areas.Employers.Controllers.Candidates
{
    public class FoldersApiController
        : CandidateListApiController
    {
        private readonly ICandidateFoldersQuery _candidateFoldersQuery;
        private readonly ICandidateListsCommand _candidateListsCommand;

        private const MemberSortOrder DefaultSortOrder = MemberSortOrder.DateUpdated;

        public FoldersApiController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery, ICandidateFoldersQuery candidateFoldersQuery, ICandidateListsCommand candidateListsCommand)
            : base(executeMemberSearchCommand, employerMemberViewsQuery, memberStatusQuery)
        {
            _candidateFoldersQuery = candidateFoldersQuery;
            _candidateListsCommand = candidateListsCommand;
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpGet]
        public ActionResult MobileCandidates(Pagination pagination)
        {
            try
            {
                var employer = CurrentEmployer;
                var folder = _candidateFoldersQuery.GetMobileFolder(employer);

                return Json(Search(employer, folder.Id, pagination), JsonRequestBehavior.AllowGet);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel(), JsonRequestBehavior.AllowGet);
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPut]
        public ActionResult MobileCandidate(Guid candidateId)
        {
            try
            {
                var employer = CurrentEmployer;
                var folder = _candidateFoldersQuery.GetMobileFolder(employer);
                _candidateListsCommand.AddCandidateToFolder(employer, folder, candidateId);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), ActionName("MobileCandidate"), HttpDelete]
        public ActionResult RemoveMobileCandidate(Guid candidateId)
        {
            try
            {
                var employer = CurrentEmployer;
                var folder = _candidateFoldersQuery.GetMobileFolder(employer);
                _candidateListsCommand.RemoveCandidateFromFolder(employer, folder, candidateId);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private CandidatesResponseModel Search(IEmployer employer, Guid folderId, Pagination pagination)
        {
            return Search(employer, e => _executeMemberSearchCommand.SearchFolder(e, folderId, new MemberSearchSortCriteria { SortOrder = DefaultSortOrder, ReverseSortOrder = true }, Prepare(pagination).ToRange()));
        }
    }
}
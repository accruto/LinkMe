using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.Members.Commands;

namespace LinkMe.Apps.Api.Areas.Employers.Controllers.Candidates
{
    public class CandidatesApiController
        : CandidateListApiController
    {
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand;
        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand;

        public CandidatesApiController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsCommand employerMemberViewsCommand, IMemberSearchAlertsCommand memberSearchAlertsCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery)
            : base(executeMemberSearchCommand, employerMemberViewsQuery, memberStatusQuery)
        {
            _employerMemberViewsCommand = employerMemberViewsCommand;
            _memberSearchAlertsCommand = memberSearchAlertsCommand;
        }

        [HttpGet]
        public ActionResult Candidate(Guid candidateId)
        {
            try
            {
                var employer = CurrentEmployer;
                var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, candidateId);
                if (view == null)
                    return JsonNotFound("candidate", JsonRequestBehavior.AllowGet);

                // Mark them as viewed.

                _employerMemberViewsCommand.ViewMember(ActivityContext.Channel.App, employer, view);

                // unbadge them

                if (employer != null)
                    _memberSearchAlertsCommand.MarkAsViewed(employer.Id, candidateId);

                // Re-get so the correct flags are set.
                view = _employerMemberViewsQuery.GetEmployerMemberView(employer, candidateId);

                return Json(new CandidateResponseModel { Candidate = GetCandidateModel(view) }, JsonRequestBehavior.AllowGet);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel(), JsonRequestBehavior.AllowGet);
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult Unlock(Guid candidateId)
        {
            return AccessMember(candidateId, MemberAccessReason.Unlock);
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPost]
        public ActionResult PhoneNumbers(Guid candidateId)
        {
            return AccessMember(candidateId, MemberAccessReason.PhoneNumberViewed);
        }

        private ActionResult AccessMember(Guid candidateId, MemberAccessReason reason)
        {
            try
            {
                var employer = CurrentEmployer;
                var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, candidateId);
                if (view == null)
                    return JsonNotFound("candidate", JsonRequestBehavior.AllowGet);

                // Access the member.

                _employerMemberViewsCommand.AccessMember(ActivityContext.Channel.App, employer, view, reason);

                // Accessing will change what the employer can see so get the view again.

                view = _employerMemberViewsQuery.GetEmployerMemberView(employer, candidateId);
                return Json(new CandidateResponseModel { Candidate = GetCandidateModel(view) }, JsonRequestBehavior.AllowGet);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel(), JsonRequestBehavior.AllowGet);
        }
    }
}
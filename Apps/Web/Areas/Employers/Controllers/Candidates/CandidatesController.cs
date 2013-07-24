using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Users.Sessions.Queries;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Context;
using LinkMe.Web.Controllers;
using LinkMe.Web.Mvc;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    public class CandidatesController
        : EmployersController
    {
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;
        private readonly IMemberStatusQuery _memberStatusQuery;
        private readonly IUserSessionsQuery _userSessionsQuery;
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery;

        public CandidatesController(IEmployerMemberViewsCommand employerMemberViewsCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery, IUserSessionsQuery userSessionsQuery, IJobAdApplicantsQuery jobAdApplicantsQuery)
        {
            _employerMemberViewsCommand = employerMemberViewsCommand;
            _employerMemberViewsQuery = employerMemberViewsQuery;
            _memberStatusQuery = memberStatusQuery;
            _userSessionsQuery = userSessionsQuery;
            _jobAdApplicantsQuery = jobAdApplicantsQuery;
        }

        public ActionResult Resumes(Guid? currentCandidateId, Guid[] candidateIds, Guid? memberId)
        {
            // Old-style redirect from ViewCandidate.

            if ((candidateIds == null || candidateIds.Length == 0) && memberId != null)
                candidateIds = new[] { memberId.Value };
            if (candidateIds == null)
                return RedirectToRoute(SearchRoutes.Search);

            return Resumes(currentCandidateId, candidateIds);
        }

        public ActionResult Candidate(Guid candidateId)
        {
            return Resumes(candidateId, new[] { candidateId });
        }

        public ActionResult PartialResume(Guid candidateId)
        {
            var employer = CurrentEmployer;
            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, candidateId);

            var currentCandidate = GetCurrentCandidate(candidateId, view);

            // Mark them as viewed.

            _employerMemberViewsCommand.ViewMember(ActivityContext.Channel.App, employer, currentCandidate.View);

            return PartialView("Resume", new ViewCandidatesModel
            {
                LastUpdatedTimes = GetLastUpdatedTimes(view),
                CurrentCandidate = currentCandidate,
                CurrentCandidates = EmployerContext.CurrentCandidates,
            });
        }

        public ActionResult ResumeDetail(Guid candidateId)
        {
            var employer = CurrentEmployer;
            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, candidateId);

            var currentCandidate = GetCurrentCandidate(candidateId, view);

            // Mark them as viewed.

            _employerMemberViewsCommand.ViewMember(ActivityContext.Channel.App, employer, currentCandidate.View);

            return View(currentCandidate);
        }

        [EnsureHttps, EnsureAuthorized(UserType.Employer)]
        public ActionResult Credits()
        {
            ViewData.SetCreditAllocations(CurrentEmployer);
            return PartialView("CreditSummary");
        }

        private ActionResult Resumes(Guid? currentCandidateId, IList<Guid> candidateIds)
        {
            // Get all views.

            var employer = CurrentEmployer;
            var views = _employerMemberViewsQuery.GetEmployerMemberViews(employer, candidateIds);

            // Filter candidates.

            candidateIds = GetFilteredCandidates(candidateIds, views);
            if (candidateIds.Count == 0)
                return NotFound("candidate");

            // Check url.

            if (candidateIds.Count == 1)
            {
                var result = EnsureUrl(views[candidateIds[0]].GenerateCandidateUrl());
                if (result != null)
                    return result;
            }

            // Get the details for the current candidate.

            currentCandidateId = currentCandidateId == null && candidateIds.Count > 0 ? candidateIds[0] : currentCandidateId;
            var currentCandidate = GetCurrentCandidate(currentCandidateId, views[currentCandidateId]);

            // Mark the current candidate as viewed.

            if (currentCandidateId != null)
            {
                _employerMemberViewsCommand.ViewMember(ActivityContext.Channel.App, employer, views[currentCandidateId]);
                EmployerContext.Viewings = EmployerContext.Viewings + 1;
            }

            return View("Resumes", new ViewCandidatesModel
            {
                CandidateIds = candidateIds,
                Views = views,
                LastUpdatedTimes = GetLastUpdatedTimes(candidateIds, views),
                CurrentCandidate = currentCandidate,
                CurrentCandidates = EmployerContext.CurrentCandidates,
            });
        }

        private static IList<Guid> GetFilteredCandidates(IEnumerable<Guid> candidateIds, ProfessionalViewCollection<EmployerMemberView> views)
        {
            // Need to filter out candidates who should not be shown.

            return (from i in candidateIds
                    let v = views[i]
                    where v.IsEnabled
                    && v.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume)
                    select i).ToList();
        }

        private ViewCandidateModel GetCurrentCandidate(Guid? currentCandidateId, EmployerMemberView view)
        {
            var currentCandidate = new ViewCandidateModel
            {
                View = view,
                LastLoginTime = currentCandidateId == null ? null : _userSessionsQuery.GetLastLoginTime(currentCandidateId.Value),
                CurrentSearch = EmployerContext.CurrentSearch,
            };

            if (currentCandidateId.HasValue && EmployerContext.CurrentCandidates is ManageCandidatesNavigation)
            {
                var jobAdId = ((ManageCandidatesNavigation)EmployerContext.CurrentCandidates).JobAdId;
                currentCandidate.CurrentApplication = _jobAdApplicantsQuery.GetApplication(currentCandidateId.Value, jobAdId);
                currentCandidate.ApplicantStatus = currentCandidate.CurrentApplication == null
                    ? (ApplicantStatus?) null
                    : _jobAdApplicantsQuery.GetApplicantStatus(currentCandidate.CurrentApplication.Id);
            }

            return currentCandidate;
        }

        private IDictionary<Guid, DateTime> GetLastUpdatedTimes(IEnumerable<Guid> candidateIds, ProfessionalViewCollection<EmployerMemberView> views)
        {
            return (from i in candidateIds
                    let view = views[i]
                    select new
                    {
                        id = i,
                        lastUpdatedTime = _memberStatusQuery.GetLastUpdatedTime(view, view, view.Resume)
                    }).ToDictionary(x => x.id, x => x.lastUpdatedTime);
        }

        private IDictionary<Guid, DateTime> GetLastUpdatedTimes(EmployerMemberView view)
        {
            return new Dictionary<Guid, DateTime>
            {
                { view.Id, _memberStatusQuery.GetLastUpdatedTime(view, view, view.Resume) },
            };
        }
    }
}
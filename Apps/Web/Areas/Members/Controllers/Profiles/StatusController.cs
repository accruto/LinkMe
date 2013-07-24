using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Domain.Roles.Candidates.Commands;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Areas.Members.Models.Profiles;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Members.Controllers.Profiles
{
    public class StatusController
        : MembersController
    {
        private readonly ICandidatesCommand _candidatesCommand;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly ICandidateStatusCommand _candidateStatusCommand;

        private const int ActivelyLookingConfirmationDays = 30;
        private const int AvailableNowConfirmationDays = 7;

        public StatusController(ICandidatesCommand candidatesCommand, ICandidatesQuery candidatesQuery, ICandidateStatusCommand candidateStatusCommand)
        {
            _candidatesCommand = candidatesCommand;
            _candidatesQuery = candidatesQuery;
            _candidateStatusCommand = candidateStatusCommand;
        }

        [HttpGet]
        public ActionResult UpdateStatus(CandidateStatus? status)
        {
            var currentStatus = Defaults.CandidateStatus;

            try
            {
                var memberId = GetMemberId();

                // If there is no member then redirect to login to get a member.

                if (memberId == null)
                    return RedirectToLoginUrl();
                currentStatus = GetStatus(memberId.Value);

                // Determine which act is being performed.

                if (status != null && status.Value != CandidateStatus.Unspecified)
                {
                    if (currentStatus == status.Value)
                    {
                        // Status is being confirmed.

                        ConfirmStatus(memberId.Value, currentStatus);
                        return View("StatusUpdated", GetStatusUpdatedModel(null, currentStatus));
                    }
                    
                    // Status is being updated.

                    UpdateStatus(memberId.Value, status.Value);
                    return View("StatusUpdated", GetStatusUpdatedModel(currentStatus, status.Value));
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // User is being given option to change.

            return View(new StatusModel { Status = currentStatus });
        }

        [HttpPost, ActionName("UpdateStatus")]
        public ActionResult PostUpdateStatus(CandidateStatus? status)
        {
            // Simply redirect to the equivalent GET.

            var url = HttpContext.GetClientUrl().AsNonReadOnly();
            if (status != null)
                url.QueryString["status"] = status.Value.ToString();

            return RedirectToUrl(url);
        }

        private Guid? GetMemberId()
        {
            // If there is a logged in member then use that.

            var member = CurrentMember;
            if (member != null)
                return member.Id;

            // If accessing through an email this may have been set up so use it.

            return new AnonymousUserContext(HttpContext).RequestUserId;
        }

        private CandidateStatus GetStatus(Guid memberId)
        {
            var candidate = _candidatesQuery.GetCandidate(memberId);
            return candidate.Status;
        }

        private void ConfirmStatus(Guid memberId, CandidateStatus status)
        {
            _candidateStatusCommand.ConfirmStatus(memberId, status);
            //return RedirectToRoute(ProfilesRoutes.Profile, new { succInfoType = "confirmStatus" });
            //throw new NotImplementedException();
        }

        private void UpdateStatus(Guid memberId, CandidateStatus status)
        {
            var candidate = _candidatesQuery.GetCandidate(memberId);
            candidate.Status = status;
            _candidatesCommand.UpdateCandidate(candidate);

            //return RedirectToRoute(ProfilesRoutes.Profile, new { succInfoType = "updateStatus" });
        }

        private static StatusUpdatedModel GetStatusUpdatedModel(CandidateStatus? previousStatus, CandidateStatus newStatus)
        {
            return new StatusUpdatedModel
            {
                PreviousStatus = previousStatus,
                NewStatus = newStatus,
                ConfirmationDays = new Dictionary<CandidateStatus, int?>
                {
                    { CandidateStatus.AvailableNow, AvailableNowConfirmationDays },
                    { CandidateStatus.ActivelyLooking, ActivelyLookingConfirmationDays },
                    { CandidateStatus.NotLooking, null },
                    { CandidateStatus.OpenToOffers, null },
                },
            };
        }
    }
}
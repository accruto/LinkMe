using System;
using System.Collections.Generic;
using LinkMe.Apps.Pageflows;

namespace LinkMe.Web.Areas.Public.Models.Join
{
    public class JoinPageflow
        : Pageflow
    {
        private readonly PageflowStep _joinStep = new PageflowStep("Join");
        private readonly PageflowStep _personalDetailsStep = new PageflowStep("PersonalDetails");
        private readonly PageflowStep _jobDetailsStep = new PageflowStep("JobDetails");
        private readonly PageflowStep _activateStep = new PageflowStep("Activate");

        public Guid? FriendInvitationId { get; set; }
        public bool IsUploadingResume { get; set; }
        public Guid? FileReferenceId { get; set; }
        public Guid? ParsedResumeId { get; set; }
        public bool IsResumeValid { get; set; }
        public bool? SendSuggestedJobs { get; set; }
        public bool HasInitialMember { get; set; }

        protected override IList<PageflowStep> GetSteps()
        {
            return new[] { _joinStep, _personalDetailsStep, _jobDetailsStep, _activateStep };
        }

        protected override PageflowStep MoveToNext(PageflowStep currentStep)
        {
            switch (currentStep.Name)
            {
                case "Join":
                    return _personalDetailsStep;

                case "PersonalDetails":
                    return _jobDetailsStep;

                case "JobDetails":
                    return _activateStep;

                default:
                    return null;
            }
        }

        protected override PageflowStep MoveToPrevious(PageflowStep currentStep)
        {
            switch (currentStep.Name)
            {
                case "PersonalDetails":
                    return _joinStep;

                case "JobDetails":
                    return _personalDetailsStep;

                case "Activate":
                    return _jobDetailsStep;

                default:
                    return null;
            }
        }
    }
}

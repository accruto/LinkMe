using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.Views;

namespace LinkMe.Domain.Users.Employers.Credits.Commands
{
    public class EmployerCreditsCommand
        : EmployerCreditsComponent, IEmployerCreditsCommand
    {
        private readonly IExercisedCreditsCommand _exercisedCreditsCommand;
        private readonly IExercisedCreditsQuery _exercisedCreditsQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IRecruitersQuery _recruitersQuery;
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery;
        private readonly int _maxCreditsPerJobAd;

        public EmployerCreditsCommand(ICreditsQuery creditsQuery, IAllocationsQuery allocationsQuery, IExercisedCreditsCommand exercisedCreditsCommand, IExercisedCreditsQuery exercisedCreditsQuery, IEmployersQuery employersQuery, IRecruitersQuery recruitersQuery, IJobAdApplicantsQuery jobAdApplicantsQuery, int maxCreditsPerJobAd)
            : base(creditsQuery, allocationsQuery)
        {
            _exercisedCreditsCommand = exercisedCreditsCommand;
            _exercisedCreditsQuery = exercisedCreditsQuery;
            _employersQuery = employersQuery;
            _recruitersQuery = recruitersQuery;
            _jobAdApplicantsQuery = jobAdApplicantsQuery;
            _maxCreditsPerJobAd = maxCreditsPerJobAd;
        }

        Guid? IEmployerCreditsCommand.ExerciseContactCredit(IEmployer employer, ProfessionalView view)
        {
            // No employer automatically means not enough credits.

            if (employer == null)
                throw new InsufficientCreditsException { Available = 0, Required = 1 };

            return ExerciseContactCredits(employer.Id, new[] { view }, view.ContactCredits)[view.Id];
        }

        void IEmployerCreditsCommand.CheckCanExerciseContactCredit(IEmployer employer, ProfessionalView view)
        {
            // No employer automatically means not enough credits.

            if (employer == null)
                throw new InsufficientCreditsException { Available = 0, Required = 1 };

            CheckCanExerciseContactCredits(new[] { view }, view.ContactCredits);
        }

        IDictionary<Guid, Guid?> IEmployerCreditsCommand.ExerciseContactCredits(IEmployer employer, ProfessionalViews views)
        {
            // No employer automatically means not enough credits.

            if (employer == null)
                throw new InsufficientCreditsException { Available = 0, Required = views.Count() };

            return ExerciseContactCredits(employer.Id, views, views.ContactCredits);
        }

        void IEmployerCreditsCommand.CheckCanExerciseContactCredits(IEmployer employer, ProfessionalViews views)
        {
            // No employer automatically means not enough credits.

            if (employer == null)
                throw new InsufficientCreditsException { Available = 0, Required = views.Count() };

            CheckCanExerciseContactCredits(views, views.ContactCredits);
        }

        Guid? IEmployerCreditsCommand.ExerciseApplicantCredit(InternalApplication application, IJobAd jobAd)
        {
            var hierarchyPath = _recruitersQuery.GetOrganisationHierarchyPath(jobAd.PosterId);

            // If the employer has already purchased this applicant or they have already applied then don't exercise a credit
            // because they already have access.

            if (_exercisedCreditsQuery.HasExercisedCredit<ContactCredit>(hierarchyPath, application.ApplicantId))
                return ExerciseCredit(hierarchyPath, false, application, jobAd);

            // If the poster has unlimited credits or zero credits then nothing to do.

            var allocation = GetEffectiveActiveAllocation<ApplicantCredit>(hierarchyPath);
            if (allocation.RemainingQuantity == null || allocation.RemainingQuantity == 0)
                return ExerciseCredit(hierarchyPath, false, application, jobAd);

            // If the applicant has applied for another job ad from this employer then don't exercise
            // - one credit used for each applicant for each poster.
            // The application that triggered this event will be returned so look for more than 1 applications.

            var employer = _employersQuery.GetEmployer(jobAd.PosterId);
            if (employer == null)
                return ExerciseCredit(hierarchyPath, false, application, jobAd);

            if (_jobAdApplicantsQuery.GetApplications(employer, application.ApplicantId).Count > 1)
                return ExerciseCredit(hierarchyPath, false, application, jobAd);
            
            // Check whether the job ad threshold has already been passed.

            var applicantList = _jobAdApplicantsQuery.GetApplicantList(employer, jobAd);
            if (applicantList == null)
                return ExerciseCredit(hierarchyPath, false, application, jobAd);
            var applicants = _jobAdApplicantsQuery.GetApplicantCount(employer, applicantList);
            if (applicants > _maxCreditsPerJobAd)
                return ExerciseCredit(hierarchyPath, false, application, jobAd);
            
            // Need to adjust an allocation.

            return ExerciseCredit(hierarchyPath, true, application, jobAd);
        }

        Guid? IEmployerCreditsCommand.ExerciseJobAdCredit(JobAdEntry jobAd)
        {
            if (jobAd.Status != JobAdStatus.Draft)
                return null;

            // Check has both job ad and applicant credits.

            var hierarchyPath = _recruitersQuery.GetOrganisationHierarchyPath(jobAd.PosterId);

            var jobAdCreditId = _creditsQuery.GetCredit<JobAdCredit>().Id;
            var applicantCreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id;
            var allocations = GetEffectiveActiveAllocations(hierarchyPath, new[] { jobAdCreditId, applicantCreditId });
            if (allocations == null || allocations.Any(a => a.Value.RemainingQuantity == 0))
                throw new InsufficientCreditsException { Available = 0, Required = 1 };

            // Exercise.

            var credit = _creditsQuery.GetCredit<JobAdCredit>();
            return _exercisedCreditsCommand.ExerciseCredit(credit.Id, hierarchyPath, true, jobAd.PosterId, null, jobAd.Id);
        }

        private IDictionary<Guid, Guid?> ExerciseContactCredits(Guid employerId, IEnumerable<ProfessionalView> views, int? contactCredits)
        {
            // Split by statuses.

            var statuses = GetStatuses(views);

            // If there are any that cannot contact at this stage then simply reject.  It is really up to the caller to ensure that this does not happen.

            if (statuses[CanContactStatus.No].Count > 0 || statuses[CanContactStatus.YesIfHadCredit].Count > 0)
                throw new InsufficientCreditsException { Available = contactCredits, Required = statuses[CanContactStatus.No].Count + statuses[CanContactStatus.YesWithCredit].Count + statuses[CanContactStatus.YesIfHadCredit].Count };

            // Check the number of credits available.

            if (statuses[CanContactStatus.YesWithCredit].Count > 0)
            {
                // Really you can't get this combination but just throw just in case.

                if (contactCredits == null)
                    throw new InsufficientCreditsException { Available = contactCredits, Required = statuses[CanContactStatus.YesWithCredit].Count };

                // Check that there are enough.

                if (contactCredits.Value < statuses[CanContactStatus.YesWithCredit].Count)
                    throw new InsufficientCreditsException { Available = contactCredits, Required = statuses[CanContactStatus.YesWithCredit].Count };
            }

            // Exercise credits as necessary.

            var hierarchyPath = _recruitersQuery.GetOrganisationHierarchyPath(employerId);

            // For those who need a credit used adjust allocations.

            var yesWithCredits = statuses[CanContactStatus.YesWithCredit];
            var exercisedCredits = yesWithCredits.Count > 0
                ? ExerciseCredits(hierarchyPath, true, employerId, from y in yesWithCredits select y.Id)
                : new Dictionary<Guid, Guid?>();

            var yesWithoutCredits = statuses[CanContactStatus.YesWithoutCredit];
            if (yesWithoutCredits.Count > 0)
            {
                // For those who don't need a credit used only exercise a credit for those who have not been contacted.

                var hasContacted = _exercisedCreditsQuery.HasExercisedCredits<ContactCredit>(hierarchyPath, from y in yesWithoutCredits select y.Id);
                foreach (var id in hasContacted)
                    exercisedCredits[id] = null;

                var hasNotContacted = (from y in yesWithoutCredits select y.Id).Except(hasContacted);
                if (hasNotContacted.Any())
                {
                    var hasNotContactedExercisedCreditIds = ExerciseCredits(hierarchyPath, false, employerId, hasNotContacted);
                    foreach (var id in hasNotContacted)
                        exercisedCredits[id] = hasNotContactedExercisedCreditIds[id];
                }
            }

            return exercisedCredits;
        }

        private static void CheckCanExerciseContactCredits(IEnumerable<ProfessionalView> views, int? contactCredits)
        {
            // Split by statuses.

            var statuses = GetStatuses(views);

            // If there are any that cannot contact at this stage then simply reject.  It is really up to the caller to ensure that this does not happen.

            if (statuses[CanContactStatus.No].Count > 0 || statuses[CanContactStatus.YesIfHadCredit].Count > 0)
                throw new InsufficientCreditsException { Available = contactCredits, Required = statuses[CanContactStatus.No].Count + statuses[CanContactStatus.YesWithCredit].Count + statuses[CanContactStatus.YesIfHadCredit].Count };

            // Check the number of credits available.

            if (statuses[CanContactStatus.YesWithCredit].Count > 0)
            {
                // Really you can't get this combination but just throw just in case.

                if (contactCredits == null)
                    throw new InsufficientCreditsException { Available = contactCredits, Required = statuses[CanContactStatus.YesWithCredit].Count };

                // Check that there are enough.

                if (contactCredits.Value < statuses[CanContactStatus.YesWithCredit].Count)
                    throw new InsufficientCreditsException { Available = contactCredits, Required = statuses[CanContactStatus.YesWithCredit].Count };
            }
        }

        private static IDictionary<CanContactStatus, IList<ProfessionalView>> GetStatuses(IEnumerable<ProfessionalView> views)
        {
            var nos = new List<ProfessionalView>();
            var yesWithCredits = new List<ProfessionalView>();
            var yesWithoutCredits = new List<ProfessionalView>();
            var yesIfHasCredits = new List<ProfessionalView>();

            foreach (var view in views)
            {
                switch (view.CanContact())
                {
                    case CanContactStatus.No:
                        nos.Add(view);
                        break;

                    case CanContactStatus.YesWithCredit:
                        yesWithCredits.Add(view);
                        break;

                    case CanContactStatus.YesWithoutCredit:
                        yesWithoutCredits.Add(view);
                        break;

                    case CanContactStatus.YesIfHadCredit:
                        yesIfHasCredits.Add(view);
                        break;
                }
            }

            return new Dictionary<CanContactStatus, IList<ProfessionalView>>
            {
                {CanContactStatus.No, nos},
                {CanContactStatus.YesWithCredit, yesWithCredits},
                {CanContactStatus.YesWithoutCredit, yesWithoutCredits},
                {CanContactStatus.YesIfHadCredit, yesIfHasCredits},
            };
        }

        private Guid? ExerciseCredit(HierarchyPath hierarchyPath, bool adjustAllocation, Application application, IJobAd jobAd)
        {
            var credit = _creditsQuery.GetCredit<ApplicantCredit>();
            return _exercisedCreditsCommand.ExerciseCredit(credit.Id, hierarchyPath, adjustAllocation, jobAd.PosterId, application.ApplicantId, application.Id);
        }

        private IDictionary<Guid, Guid?> ExerciseCredits(HierarchyPath hierarchyPath, bool adjustAllocation, Guid employerId, IEnumerable<Guid> memberIds)
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            return _exercisedCreditsCommand.ExerciseCredits(credit.Id, hierarchyPath, adjustAllocation, employerId, memberIds, null).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
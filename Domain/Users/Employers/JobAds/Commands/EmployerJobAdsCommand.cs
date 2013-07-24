using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Employers.JobAds.Commands
{
    public class EmployerJobAdsCommand
        : IEmployerJobAdsCommand
    {
        private readonly IJobAdsCommand _jobAdsCommand;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IEmployerCreditsCommand _employerCreditsCommand;
        private readonly int _dailyLimit;
        private readonly int _weeklyLimit;

        public EmployerJobAdsCommand(IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IEmployerCreditsCommand employerCreditsCommand, int dailyLimit, int weeklyLimit)
        {
            _jobAdsCommand = jobAdsCommand;
            _jobAdsQuery = jobAdsQuery;
            _employerCreditsCommand = employerCreditsCommand;
            _dailyLimit = dailyLimit;
            _weeklyLimit = weeklyLimit;
        }

        void IEmployerJobAdsCommand.CreateJobAd(IEmployer employer, JobAd jobAd)
        {
            jobAd.PosterId = employer.Id;
            _jobAdsCommand.CreateJobAd(jobAd);
        }

        void IEmployerJobAdsCommand.UpdateJobAd(IEmployer employer, JobAd jobAd)
        {
            if (!CanAccess(employer, jobAd))
                throw new JobAdPermissionsException(employer, jobAd.Id);

            _jobAdsCommand.UpdateJobAd(jobAd);
        }

        void IEmployerJobAdsCommand.OpenJobAd(IEmployer employer, JobAd jobAd, bool checkLimits)
        {
            if (!CanAccess(employer, jobAd))
                throw new JobAdPermissionsException(employer, jobAd.Id);

            // Check limits.

            if (checkLimits)
                CheckLimits(employer.Id);

            // Exercise a credit.

            _employerCreditsCommand.ExerciseJobAdCredit(jobAd);

            // Open the ad.

            _jobAdsCommand.OpenJobAd(jobAd);
        }

        void IEmployerJobAdsCommand.CloseJobAd(IEmployer employer, JobAdEntry jobAd)
        {
            if (!CanAccess(employer, jobAd))
                throw new JobAdPermissionsException(employer, jobAd.Id);
            _jobAdsCommand.CloseJobAd(jobAd);
        }

        void IEmployerJobAdsCommand.DeleteJobAd(IEmployer employer, JobAdEntry jobAd)
        {
            if (!CanAccess(employer, jobAd))
                throw new JobAdPermissionsException(employer, jobAd.Id);

            _jobAdsCommand.DeleteJobAd(jobAd);
        }

        void IEmployerJobAdsCommand.TransferJobAd(IEmployer employer, JobAdEntry jobAd)
        {
            _jobAdsCommand.TransferJobAd(employer.Id, jobAd);
        }

        private static bool CanAccess(IHasId<Guid> employer, IJobAd entry)
        {
            if (employer == null)
                return false;
            return employer.Id == entry.PosterId;
        }

        private void CheckLimits(Guid employerId)
        {
            var counts = _jobAdsQuery.GetOpenJobAdCounts(employerId);

            // Check whether the current count + the new job ad puts them over the limit.

            if (counts.Item1 + 1 > _dailyLimit || counts.Item2 + 1 > _weeklyLimit)
                throw new TooManyJobAdsException();
        }
    }
}
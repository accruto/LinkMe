using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Anonymous.JobAds.Commands
{
    public class AnonymousJobAdsCommand
        : IAnonymousJobAdsCommand
    {
        private readonly IJobPostersCommand _jobPostersCommand;
        private readonly IJobAdsCommand _jobAdsCommand;

        public AnonymousJobAdsCommand(IJobPostersCommand jobPostersCommand, IJobAdsCommand jobAdsCommand)
        {
            _jobPostersCommand = jobPostersCommand;
            _jobAdsCommand = jobAdsCommand;
        }

        void IAnonymousJobAdsCommand.CreateJobAd(AnonymousUser user, JobAd jobAd)
        {
            var poster = _jobPostersCommand.GetJobPoster(user.Id);
            if (poster == null)
            {
                poster = new JobPoster { Id = user.Id };
                _jobPostersCommand.CreateJobPoster(poster);
            }

            jobAd.PosterId = poster.Id;
            _jobAdsCommand.CreateJobAd(jobAd);
        }

        void IAnonymousJobAdsCommand.UpdateJobAd(AnonymousUser user, JobAd jobAd)
        {
            if (!CanAccess(user, jobAd))
                throw new JobAdPermissionsException(user, jobAd.Id);

            _jobAdsCommand.UpdateJobAd(jobAd);
        }

        private static bool CanAccess(IHasId<Guid> user, IJobAd entry)
        {
            if (user == null)
                return false;
            return user.Id == entry.PosterId;
        }
    }
}
